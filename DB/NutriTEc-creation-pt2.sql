-- Constraint

-- Unique measurement per day
ALTER TABLE Measurements
ADD UNIQUE(PatientId, RevisionDate);

-- Foreign Keys

-- Product-ProductRecipe
ALTER TABLE ProductRecipe
ADD CONSTRAINT ProductRecipe_Barcode
FOREIGN KEY (ProductBarcode) REFERENCES Product (Barcode);

-- Recipe-ProductRecipe
ALTER TABLE ProductRecipe
ADD CONSTRAINT ProductRecipe_RecipeId
FOREIGN KEY (RecipeId) REFERENCES Recipe (Id);

-- Patient-PatientRecipe
ALTER TABLE PatientRecipe
ADD CONSTRAINT PatientRecipe_PatientId
FOREIGN KEY (PatientId) REFERENCES Patient (Id);

-- Recipe-PatientRecipe
ALTER TABLE PatientRecipe
ADD CONSTRAINT PatientRecipe_RecipeId
FOREIGN KEY (RecipeId) REFERENCES Patient (Id);

-- Patient-Nutritionist
ALTER TABLE Patient
ADD CONSTRAINT Patient_NutriId
FOREIGN KEY (NutriId) REFERENCES Nutritionist (Id);

-- PlanPatient-Plan
ALTER TABLE PlanPatient
ADD CONSTRAINT PlanPatient_PlanId
FOREIGN KEY (PlanId) REFERENCES Plan (Id);

-- PlanPatient-Patient
ALTER TABLE PlanPatient
ADD CONSTRAINT PlanPatient_PatientId
FOREIGN KEY (PatientId) REFERENCES Patient (Id);

-- Measurements-Patients
ALTER TABLE Measurements
ADD CONSTRAINT Measurements_PatientId
FOREIGN KEY (PatientId) REFERENCES Patient (Id);

-- Plan-Nutritionist
ALTER TABLE Plan
ADD CONSTRAINT Plan_NutriId
FOREIGN KEY (NutriId) REFERENCES Nutritionist (Id);

-- PlanProduct-Product
ALTER TABLE PlanProduct
ADD CONSTRAINT PlanProduct_ProductBarcode
FOREIGN KEY (ProductBarcode) REFERENCES Product (Barcode);

-- PlanProduct-Plan
ALTER TABLE PlanProduct
ADD CONSTRAINT PlanProduct_PlanId
FOREIGN KEY (PlanId) REFERENCES Plan (Id);

-- ProductVitamin-Product
ALTER TABLE ProductVitamin
ADD CONSTRAINT ProductVitamin_ProductBarcode
FOREIGN KEY (ProductBarcode) REFERENCES Product (Barcode);

-- ProductVitamin-Vitamin
ALTER TABLE ProductVitamin
ADD CONSTRAINT ProductVitamin_Vitamin
FOREIGN KEY (VitaminId) REFERENCES Vitamin (Id);

-- PatientProduct-Product
ALTER TABLE PatientProduct
ADD CONSTRAINT PatientProduct_ProductBarcode
FOREIGN KEY (ProductBarcode) REFERENCES Product (Barcode);

-- PatientProduct-Patient
ALTER TABLE PatientProduct
ADD CONSTRAINT PatientProduct_PatientId
FOREIGN KEY (PatientId) REFERENCES  Patient (Id);

--Nutritionist-Admin
ALTER TABLE Nutritionist
ADD CONSTRAINT Nutritionist_AdminEmail
FOREIGN KEY (AdminId) REFERENCES Administrator(Id);

--Nutritionist-ChargeType
ALTER TABLE Nutritionist
ADD CONSTRAINT Nutritionist_ChargeTypeId
FOREIGN KEY (ChargeTypeId) REFERENCES ChargeType(Id);

-- Views
CREATE VIEW UserCredentials AS
SELECT Id, Email, Password, 'P' AS UserType FROM Patient
UNION ALL
SELECT Id, Email, Password, 'N' AS UserType FROM Nutritionist
UNION ALL
SELECT Id, Email, Password, 'A' AS UserType FROM Administrator;


CREATE OR REPLACE VIEW products_in_recipe AS
SELECT R.name as recipename, R.Id as recipeid, P.name as productname, 
P.portionsize as portionsize, PR.servings as servings, P.energy * PR.servings as energy, 
P.Fat * PR.servings as fat, P.Sodium * PR.servings as sodium, 
P.Carbs* PR.servings as carbs, P.Protein * PR.servings as protein, 
P.Calcium * PR.servings as calcium, P.Iron * PR.servings as iron
FROM (recipe as R  join productrecipe as PR on R.Id = PR.recipeid) join product as P on P.barcode = PR.productbarcode;



-- Triggers and Fuctions
CREATE OR REPLACE FUNCTION check_email_exists()
RETURNS TRIGGER AS $$
BEGIN
    IF EXISTS (
        SELECT 1
        FROM UserCredentials
        WHERE Email = NEW.Email
    ) THEN
        RAISE EXCEPTION 'Email already exists in UserCredentials view.';
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION create_recipe(recipe_name varchar(100))
RETURNS int AS
$$
DECLARE
    recipe_id int;
BEGIN
    -- Insert the recipe
    INSERT INTO recipe (name)
    VALUES (recipe_name)
	
	RETURNING id INTO recipe_id;
    RETURN recipe_id;
END;
$$
LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION calculate_recipe_nutrients(recipe_id int)
    RETURNS TABLE (
		TotalEnergy float, 
		TotalSodium float,
		TotalCarbs float,
		TotalProtein float,
		TotalCalcium float,
        TotalFat float,
		TotalIron float
) 
AS $$
BEGIN
    RETURN QUERY SELECT
			SUM(PR.energy),
			SUM(PR.sodium),
			SUM(PR.carbs),
			SUM(PR.protein),
			SUM(PR.calcium),
            SUM(PR.fat),
			SUM(PR.iron)
    FROM
        products_in_recipe as PR
    WHERE
        PR.recipeid = recipe_id
		GROUP BY recipename;
END; $$ 

LANGUAGE 'plpgsql';


CREATE OR REPLACE FUNCTION payroll(admin_id int)
    RETURNS TABLE (
		ChargeType int,
		NutriEmail varchar(100), 
		FullName text,
		CardNumber int,
		TotalAmount int,
		Discount float,
        ChargeAmount float
) 
AS $$
DECLARE 
	total_patients int;
BEGIN
    RETURN QUERY SELECT
		N.ChargeTypeId,
		N.Email,
		CONCAT(N.Name,' ', N.LastName1, ' ', N.LastName2),
		N.CardNumber, 
		get_total_patients(N.Id),
    	(
        SELECT discounted_amount
        FROM amount_to_charge(get_total_patients(N.Id), N.ChargeTypeId)
    	),
		(
        SELECT charge_amount
        FROM amount_to_charge(get_total_patients(N.Id), N.ChargeTypeId)
    	)
    FROM administrator as A 
	JOIN nutritionist as N on N.AdminId = A.Id
	WHERE A.Id = admin_id;
END; $$ 
LANGUAGE 'plpgsql';


CREATE OR REPLACE FUNCTION amount_to_charge(total_patients int, charge_type int, OUT charge_amount float, OUT discounted_amount float)
    AS $$
BEGIN
    IF charge_type = 1 THEN
        charge_amount := total_patients;
		discounted_amount := 0;
    ELSIF charge_type = 2 THEN
        charge_amount := 0.95 * total_patients;
		discounted_amount := 5;
    ELSIF charge_type = 3 THEN
        charge_amount := 0.9 * total_patients;
		discounted_amount := 10;
    ELSE 
        charge_amount := 0;
    END IF;  
END; 
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_total_patients(nutri_id int)
    RETURNS int AS $$
DECLARE
	total_patients int;
BEGIN
	SELECT COUNT(P.Id) INTO total_patients
	FROM nutritionist as N JOIN patient as P on P.NutriId = N.Id
	WHERE N.Id = nutri_id;
	
	RETURN total_patients;
END; $$ 
LANGUAGE 'plpgsql';

CREATE TRIGGER CheckEmailExistsInAdministrator
BEFORE INSERT ON Administrator
FOR EACH ROW
EXECUTE FUNCTION check_email_exists();

CREATE TRIGGER CheckEmailExistsInPatient
BEFORE INSERT ON Patient
FOR EACH ROW
EXECUTE FUNCTION check_email_exists();

CREATE TRIGGER CheckEmailExistsInNutritionist
BEFORE INSERT ON Nutritionist
FOR EACH ROW
EXECUTE FUNCTION check_email_exists();

