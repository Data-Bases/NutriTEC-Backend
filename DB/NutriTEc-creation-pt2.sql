-- Constraint
-- Unique measurement per day
ALTER TABLE Measurements
ADD UNIQUE(PatientId, RevisionDate);
-- Foreign Keys
-- Product-ProductRecipe
ALTER TABLE ProductRecipe
ADD CONSTRAINT ProductRecipe_Barcode FOREIGN KEY (ProductBarcode) REFERENCES Product (Barcode);
-- Recipe-ProductRecipe
ALTER TABLE ProductRecipe
ADD CONSTRAINT ProductRecipe_RecipeId FOREIGN KEY (RecipeId) REFERENCES Recipe (Id);
-- Patient-PatientRecipe
ALTER TABLE PatientRecipe
ADD CONSTRAINT PatientRecipe_PatientId FOREIGN KEY (PatientId) REFERENCES Patient (Id);
-- Recipe-PatientRecipe
ALTER TABLE PatientRecipe
ADD CONSTRAINT PatientRecipe_RecipeId FOREIGN KEY (RecipeId) REFERENCES Recipe (Id);
-- Patient-Nutritionist
ALTER TABLE Patient
ADD CONSTRAINT Patient_NutriId FOREIGN KEY (NutriId) REFERENCES Nutritionist (Id);
-- PlanPatient-Plan
ALTER TABLE PlanPatient
ADD CONSTRAINT PlanPatient_PlanId FOREIGN KEY (PlanId) REFERENCES Plan (Id);
-- PlanPatient-Patient
ALTER TABLE PlanPatient
ADD CONSTRAINT PlanPatient_PatientId FOREIGN KEY (PatientId) REFERENCES Patient (Id);
-- Measurements-Patients
ALTER TABLE Measurements
ADD CONSTRAINT Measurements_PatientId FOREIGN KEY (PatientId) REFERENCES Patient (Id);
-- Plan-Nutritionist
ALTER TABLE Plan
ADD CONSTRAINT Plan_NutriId FOREIGN KEY (NutriId) REFERENCES Nutritionist (Id);
-- PlanProduct-Product
ALTER TABLE PlanProduct
ADD CONSTRAINT PlanProduct_ProductBarcode FOREIGN KEY (ProductBarcode) REFERENCES Product (Barcode);
-- PlanProduct-Plan
ALTER TABLE PlanProduct
ADD CONSTRAINT PlanProduct_PlanId FOREIGN KEY (PlanId) REFERENCES Plan (Id);
-- PlanProduct-Recipe
ALTER TABLE PlanRecipe
ADD CONSTRAINT PlanRecipe_RecipeId FOREIGN KEY (RecipeId) REFERENCES Recipe (Id);
-- PlanProduct-Plan
ALTER TABLE PlanRecipe
ADD CONSTRAINT PlanRecipe_PlanId FOREIGN KEY (PlanId) REFERENCES Plan (Id);
-- ProductVitamin-Product
ALTER TABLE ProductVitamin
ADD CONSTRAINT ProductVitamin_ProductBarcode FOREIGN KEY (ProductBarcode) REFERENCES Product (Barcode);
-- ProductVitamin-Vitamin
ALTER TABLE ProductVitamin
ADD CONSTRAINT ProductVitamin_Vitamin FOREIGN KEY (VitaminId) REFERENCES Vitamin (Id);
-- PatientProduct-Product
ALTER TABLE PatientProduct
ADD CONSTRAINT PatientProduct_ProductBarcode FOREIGN KEY (ProductBarcode) REFERENCES Product (Barcode);
-- PatientProduct-Patient
ALTER TABLE PatientProduct
ADD CONSTRAINT PatientProduct_PatientId FOREIGN KEY (PatientId) REFERENCES Patient (Id);
--Nutritionist-Admin
ALTER TABLE Nutritionist
ADD CONSTRAINT Nutritionist_AdminEmail FOREIGN KEY (AdminId) REFERENCES Administrator(Id);
--Nutritionist-ChargeType
ALTER TABLE Nutritionist
ADD CONSTRAINT Nutritionist_ChargeTypeId FOREIGN KEY (ChargeTypeId) REFERENCES ChargeType(Id);
ALTER TABLE Plan
ADD CONSTRAINT unique_plan UNIQUE (Name, NutriId);
ALTER TABLE PlanPatient
ADD CONSTRAINT unique_plan_patient UNIQUE (PatientId, InitialDate);
-- Fuctions
/*
 This trigger function that checks if the user with the specified ID exists in the "UserCredentials" view. 
 If not, it raises an exception, otherwise, it allows the operation to proceed.
 */
CREATE OR REPLACE FUNCTION check_email_exists() RETURNS TRIGGER AS $$ BEGIN IF EXISTS (
        SELECT 1
        FROM UserCredentials
        WHERE Email = NEW.Email
    ) THEN RAISE EXCEPTION 'Email already exists in UserCredentials view.';
END IF;
RETURN NEW;
END;
$$ LANGUAGE plpgsql;
/*
 This trigger function that checks if the patient with the specified ID exists in the "Patient" table. 
 If not, it raises an exception, otherwise, it allows the operation to proceed.
 */
CREATE OR REPLACE FUNCTION check_patient_exists() RETURNS TRIGGER AS $$ BEGIN IF NOT EXISTS (
        SELECT 1
        FROM Patient
        WHERE Id = NEW.PatientId
    ) THEN RAISE EXCEPTION 'Patient with ID % does not exist.',
    NEW.PatientId;
END IF;
RETURN NEW;
END;
$$ LANGUAGE plpgsql;
/*
 The function will insert the recipe into the "recipe" table 
 and return the ID of the newly created recipe.
 */
CREATE OR REPLACE FUNCTION create_recipe(recipe_name varchar(100)) RETURNS int AS $$
DECLARE recipe_id int;
BEGIN -- Insert the recipe
INSERT INTO recipe (name)
VALUES (recipe_name)
RETURNING id INTO recipe_id;
RETURN recipe_id;
END;
$$ LANGUAGE plpgsql;
/*
 This function is used to get the discount and charge amount based on the charge type 
 and total number of patients.
 The function utilizes the "get_total_patients" function to calculate the total number 
 of patients associated with the nutritionist. 
 */
CREATE OR REPLACE FUNCTION calculate_recipe_nutrients(recipe_id int) RETURNS TABLE (
        TotalEnergy float,
        TotalSodium float,
        TotalCarbs float,
        TotalProtein float,
        TotalCalcium float,
        TotalFat float,
        TotalIron float
    ) AS $$ BEGIN RETURN QUERY
SELECT SUM(PR.energy),
    SUM(PR.sodium),
    SUM(PR.carbs),
    SUM(PR.protein),
    SUM(PR.calcium),
    SUM(PR.fat),
    SUM(PR.iron)
FROM products_in_recipe as PR
WHERE PR.recipeid = recipe_id
GROUP BY recipename;
END;
$$ LANGUAGE 'plpgsql';
/*
 This function providing the administrator ID, and retrieve 
 the payroll (the discount and charge amount based on the charge type and total number of patients) 
 information for the nutritionist.The function utilizes the "get_total_patients" function 
 to calculate the total number of patients associated with the nutritionist
 */
CREATE OR REPLACE FUNCTION payroll(admin_id int) RETURNS TABLE (
        ChargeType int,
        NutriEmail varchar(100),
        FullName text,
        CardNumber varchar(20),
        TotalAmount int,
        Discount float,
        ChargeAmount float
    ) AS $$
DECLARE total_patients int;
BEGIN RETURN QUERY
SELECT N.ChargeTypeId,
    N.Email,
    CONCAT(N.Name, ' ', N.LastName1, ' ', N.LastName2),
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
END;
$$ LANGUAGE 'plpgsql';
/* 
 The function calculates the charge amount and discounted amount based on the provided charge 
 type and total number of patients.
 */
CREATE OR REPLACE FUNCTION amount_to_charge(
        total_patients int,
        charge_type int,
        OUT charge_amount float,
        OUT discounted_amount float
    ) AS $$ BEGIN IF charge_type = 1 THEN charge_amount := total_patients;
discounted_amount := 0;
ELSIF charge_type = 2 THEN charge_amount := 0.95 * total_patients;
discounted_amount := 5;
ELSIF charge_type = 3 THEN charge_amount := 0.9 * total_patients;
discounted_amount := 10;
ELSE charge_amount := 0;
END IF;
END;
$$ LANGUAGE plpgsql;
/*
 The function will return the total number of patients associated with the specified nutritionist.
 */
CREATE OR REPLACE FUNCTION get_total_patients(nutri_id int) RETURNS int AS $$
DECLARE total_patients int;
BEGIN
SELECT COUNT(P.Id) INTO total_patients
FROM nutritionist as N
    JOIN patient as P on P.NutriId = N.Id
WHERE N.Id = nutri_id;
RETURN total_patients;
END;
$$ LANGUAGE 'plpgsql';
/*
 The function will return a table with the calculated nutritional information 
 for the specified product and servings.
 */
CREATE OR REPLACE FUNCTION calculate_product_servings(product_id int, servings_value float) RETURNS TABLE (
        Id int,
        Name varchar(100),
        PortionSize float,
        Servings float,
        Energy float,
        Fat float,
        Sodium float,
        Carbs float,
        Protein float,
        Calcium float,
        Iron float
    ) AS $$
DECLARE product_servings float;
BEGIN product_servings := servings_value;
RETURN QUERY
SELECT P.Barcode,
    P.name,
    P.portionsize,
    product_servings,
    product_servings * P.energy,
    product_servings * P.fat,
    product_servings * P.sodium,
    product_servings * P.carbs,
    product_servings * P.protein,
    product_servings * P.calcium,
    product_servings * P.iron
FROM Product as P
WHERE P.Barcode = product_id;
END;
$$ LANGUAGE 'plpgsql';
/*
 The function will return a table with the consumed recipe information for the specified patient 
 and date. The function also utilizes another function called "calculate_recipe_nutrients" 
 to calculate the total energy of the recipe based on its ID.
 */
CREATE FUNCTION get_consumed_recipe(patient_id int, date_consumed date) RETURNS TABLE(
    Id int,
    Name varchar(100),
    Servings float,
    Energy float,
    Mealtime varchar(50)
) AS $$ BEGIN RETURN QUERY
SELECT PR.Id,
    PR.Name,
    PR.Servings,
    (
        SELECT TotalEnergy * PR.Servings
        FROM calculate_recipe_nutrients(PR.Id)
    ),
    PR.Mealtime
FROM patient_recipe as PR
WHERE PR.PatientId = patient_id
    and PR.ConsumeDate = date_consumed;
END;
$$ LANGUAGE 'plpgsql';
/*
 The function will return a table with the consumed product information 
 for the specified patient and date.
 */
CREATE FUNCTION get_consumed_product(patient_id int, date_consumed date) RETURNS TABLE(
    Id int,
    Name varchar(100),
    Servings float,
    Energy float,
    Mealtime varchar(50)
) AS $$ BEGIN RETURN QUERY
SELECT PP.Barcode,
    PP.Name,
    PP.Servings,
    PP.Energy,
    PP.Mealtime
FROM patient_products as PP
WHERE PP.PatientId = patient_id
    and PP.ConsumeDate = date_consumed;
END;
$$ LANGUAGE 'plpgsql';
/*
 The function will return a table with the requested measurements data within 
 the specified date range for the given patient.
 */
CREATE OR REPLACE FUNCTION get_patient_measurements(
        patient_id INT,
        start_date DATE,
        end_date DATE
    ) RETURNS TABLE (
        Height FLOAT,
        FatPercentage FLOAT,
        MusclePercentage FLOAT,
        Weight FLOAT,
        Waist FLOAT,
        Neck FLOAT,
        Hips FLOAT,
        RevisionDate date
    ) AS $$ BEGIN RETURN QUERY
SELECT M.Height,
    M.FatPercentage,
    M.MusclePercentage,
    M.Weight,
    M.Waist,
    M.Neck,
    M.Hips,
    M.RevisionDate
FROM Measurements AS M
WHERE M.PatientId = patient_id
    AND M.RevisionDate >= start_date
    AND M.RevisionDate <= end_date;
RETURN;
END;
$$ LANGUAGE plpgsql;
/*
 This function create a new plan record in the "Plan" table with the specified name 
 and nutritionist ID. 
 The function will return the ID of the newly created plan.
 */
CREATE OR REPLACE FUNCTION create_plan(plan_name varchar(250), nutri_id int) RETURNS int AS $$
DECLARE plan_id int;
BEGIN -- Insert the recipe
INSERT INTO Plan (NutriId, Name)
VALUES (nutri_id, plan_name)
RETURNING id INTO plan_id;
RETURN plan_id;
END;
$$ LANGUAGE plpgsql;
/*
 This function return a table with the requested recipe plan information based 
 on the provided plan ID and weekday. The function also utilizes another 
 function called "calculate_recipe_nutrients" to calculate the total energy 
 of the recipe based on its ID.
 */
CREATE FUNCTION get_recipe_plan(plan_id int, weekday varchar(50)) RETURNS TABLE(
    Id int,
    Name varchar(100),
    Servings float,
    Energy float,
    Mealtime varchar(50)
) AS $$ BEGIN RETURN QUERY
SELECT R.Id,
    R.Name,
    PR.Servings,
    (
        SELECT TotalEnergy * PR.Servings
        FROM calculate_recipe_nutrients(R.Id)
    ),
    PR.Mealtime
FROM Recipe as R
    JOIN Planrecipe as PR ON R.Id = PR.RecipeId
WHERE PR.PlanId = plan_id
    and PR.ConsumeWeekDay = weekday;
END;
$$ LANGUAGE 'plpgsql';
/*
 This function takes two parameters: "plan_id" of type integer and "weekday" of type varchar(50).
 The function returns a table with columns: Id (integer), Name (varchar), Servings (float), 
 Energy (float), and Mealtime (varchar).
 */
CREATE FUNCTION get_product_plan(plan_id int, weekday varchar(50)) RETURNS TABLE(
    Id int,
    Name varchar(100),
    Servings float,
    Energy float,
    Mealtime varchar(50)
) AS $$ BEGIN RETURN QUERY
SELECT P.Barcode,
    P.Name,
    PP.Servings,
    PP.Servings * P.Energy,
    PP.Mealtime
FROM Product as P
    JOIN Planproduct as PP ON P.Barcode = PP.ProductBarcode
WHERE PP.PlanId = plan_id
    and PP.ConsumeWeekDay = weekday;
END;
$$ LANGUAGE 'plpgsql';
/*
 This function it is used as a trigger function,the trigger function is designed 
 to prevent inserting or updating rows with a "BirthDate" value that is in the future.
 
 Outputs:
 If the "BirthDate" value of the new or updated row is greater than the current date,
 an exception is raised with the message "BirthDate" cannot be in the future."
 If the "BirthDate" value is valid (not in the future), 
 the trigger function returns the new row data.
 */
CREATE OR REPLACE FUNCTION prevent_future_birthdates() RETURNS TRIGGER AS $$ BEGIN IF NEW.BirthDate > CURRENT_DATE THEN RAISE EXCEPTION 'BirthDate cannot be in the future.';
END IF;
RETURN NEW;
END;
$$ LANGUAGE plpgsql;
/*
 This function it is used as a trigger function,the trigger function is designed 
 to prevent inserting or updating rows with a "RevisionDate" value that is in the future.
 
 Outputs:
 If the "RevisionDate" value of the new or updated row is greater than the current date,
 an exception is raised with the message "RevisionDate" cannot be in the future."
 If the "RevisionDate" value is valid (not in the future), 
 the trigger function returns the new row data.
 */
CREATE OR REPLACE FUNCTION prevent_future_revisiondates() RETURNS TRIGGER AS $$ BEGIN IF NEW.RevisionDate > CURRENT_DATE THEN RAISE EXCEPTION 'RevisionDate cannot be in the future.';
END IF;
RETURN NEW;
END;
$$ LANGUAGE plpgsql;
/*
 This function it is used as a trigger function,the trigger function is designed 
 to prevent inserting or updating rows with a "InitialDate" value that is in the future.
 
 Outputs:
 If the "InitialDate" value of the new or updated row is greater than the current date,
 an exception is raised with the message "InitialDate cannot be in the future."
 If the "InitialDate" value is valid (not in the future), 
 the trigger function returns the new row data.
 */
CREATE OR REPLACE FUNCTION prevent_future_initialdates() RETURNS TRIGGER AS $$ BEGIN IF NEW.InitialDate > CURRENT_DATE THEN RAISE EXCEPTION 'InitialDate cannot be in the future.';
END IF;
RETURN NEW;
END;
$$ LANGUAGE plpgsql;
/*
 This function it is used as a trigger function,the trigger function is designed 
 to prevent inserting or updating rows with a "ConsumeDate" value that is in the future.
 
 Outputs:
 If the "ConsumeDate" value of the new or updated row is greater than the current date,
 an exception is raised with the message "ConsumeDate cannot be in the future."
 If the "ConsumeDate" value is valid (not in the future), 
 the trigger function returns the new row data.
 */
CREATE OR REPLACE FUNCTION prevent_future_consumedates() RETURNS TRIGGER AS $$ BEGIN IF NEW.ConsumeDate > CURRENT_DATE THEN RAISE EXCEPTION 'ConsumeDate cannot be in the future.';
END IF;
RETURN NEW;
END;
$$ LANGUAGE plpgsql;
-- Views
/*
 This view creates a view called "UserCredentials" that combines 
 the email, password, and user type (as a code) 
 from the "Patient," "Nutritionist," and "Administrator" tables into a single result set. 
 
 It is used in a trigger function checksIfEmailExists(). 
 */
CREATE VIEW UserCredentials AS
SELECT Id,
    Email,
    Password,
    'P' AS UserType
FROM Patient
UNION ALL
SELECT Id,
    Email,
    Password,
    'N' AS UserType
FROM Nutritionist
UNION ALL
SELECT Id,
    Email,
    Password,
    'A' AS UserType
FROM Administrator;
/*
 This view combines information from the "recipe," "productrecipe," and "product" tables. 
 It includes details calculated based on the servings and the corresponding product's nutritional values.
 
 It is used by calculate_recipe_nutrients() function
 */
CREATE OR REPLACE VIEW products_in_recipe AS
SELECT R.name as recipename,
    R.Id as recipeid,
    P.name as productname,
    P.Barcode as ProductId,
    P.portionsize as portionsize,
    PR.servings as servings,
    P.energy * PR.servings as energy,
    P.Fat * PR.servings as fat,
    P.Sodium * PR.servings as sodium,
    P.Carbs * PR.servings as carbs,
    P.Protein * PR.servings as protein,
    P.Calcium * PR.servings as calcium,
    P.Iron * PR.servings as iron
FROM (
        recipe as R
        join productrecipe as PR on R.Id = PR.recipeid
    )
    join product as P on P.barcode = PR.productbarcode;
/*
 This view  combines information from the "Product" and "PatientProduct" tables. 
 It includes details such as the patient ID, product barcode, product name, servings, 
 energy (calculated based on servings and the product's energy value), 
 mealtime, and consume date for each patient's product consumption.
 
 It is used by get_consumed_product() function
 */
CREATE OR REPLACE VIEW patient_products AS
SELECT PP.PatientId,
    P.Barcode,
    P.Name,
    PP.Servings,
    PP.Servings * P.Energy AS Energy,
    PP.Mealtime,
    PP.ConsumeDate
FROM Product as P
    JOIN PatientProduct as PP ON P.Barcode = PP.ProductBarcode;
/*
 This view called "patient_recipe" that combines information from the "Recipe" and "PatientRecipe" tables.
 It includes details such as the patient ID, recipe ID, recipe name, servings, 
 energy (calculated using a custom function called "calculate_recipe_nutrients" 
 which returns the total energy of the recipe multiplied by the servings), 
 mealtime, and consume date for each patient's recipe consumption.
 
 It is used by get_consumed_recipe() function
 */
CREATE OR REPLACE VIEW patient_recipe AS
SELECT PR.PatientId,
    R.Id,
    R.Name,
    PR.Servings,
    (
        SELECT TotalEnergy * PR.Servings
        FROM calculate_recipe_nutrients(R.Id)
    ) as Energy,
    PR.Mealtime,
    PR.ConsumeDate
FROM Recipe as R
    JOIN Patientrecipe as PR ON R.Id = PR.RecipeId;
-- Stored Procedures
/*
 The "delete_product" procedure removes a product and its associated records from various tables. 
 It deletes entries from planproduct, patientproduct, productrecipe, and productvitamin tables where the 
 ProductBarcode matches the provided product_id. Additionally, it removes the product itself from the Product 
 table based on the product_id. 
 */
CREATE PROCEDURE delete_product(product_id int) LANGUAGE SQL AS $$
DELETE FROM planproduct as PP
WHERE PP.ProductBarcode = product_id;
DELETE FROM patientproduct as PP
WHERE PP.ProductBarcode = product_id;
DELETE FROM productrecipe as PP
WHERE PP.ProductBarcode = product_id;
DELETE FROM productvitamin as PP
WHERE PP.ProductBarcode = product_id;
DELETE FROM Product
WHERE Product.Barcode = product_id;
$$;
/*
 The "delete_recipe" procedure is used to delete a recipe and its associated records from the database. 
 It removes records from the "planrecipe" table, "productrecipe" table, and "patientrecipe" table based on the provided recipe_id. 
 Additionally, it deletes the recipe record itself from the "Recipe" table. This procedure ensures the complete removal of a recipe 
 and its associations from multiple tables in a single operation.
 
 */
CREATE PROCEDURE delete_recipe(recipe_id int) LANGUAGE SQL AS $$
DELETE FROM planrecipe as PR
WHERE PR.RecipeId = recipe_id;
DELETE FROM productrecipe as PR
WHERE PR.RecipeId = recipe_id;
DELETE FROM patientrecipe as PR
WHERE PR.RecipeId = recipe_id;
DELETE FROM Recipe
WHERE Recipe.Id = recipe_id;
$$;
/*
The "delete_plan" procedure removes a plan and its associated records from multiple tables. 
It deletes entries from the planrecipe, planproduct, and planpatient tables where the PlanId 
matches the provided plan_id. Additionally, it deletes the plan itself from the plan table based 
on the plan_id.
*/
CREATE PROCEDURE delete_plan(plan_id int) LANGUAGE SQL AS $$
DELETE FROM planrecipe as PR
WHERE PR.PlanId = plan_id;
DELETE FROM planproduct as PR
WHERE PR.PlanId = plan_id;
DELETE FROM planpatient as PR
WHERE PR.PlanId = plan_id;
DELETE FROM plan
WHERE plan.Id = plan_id;
$$;
/*
The procedure "insert_plan_patient"  inserts a new record into the PlanPatient table. 
It checks if the initial date is a Monday and calculates the end date as 7 days after the initial date. 
The procedure then performs the insertion using the provided plan_id, patient_id, initial_date, and calculated end_date.
*/
CREATE OR REPLACE PROCEDURE insert_plan_patient(
        p_planId INT,
        p_patientId INT,
        p_initialDate DATE
    ) LANGUAGE plpgsql AS $$
DECLARE p_endDate DATE;
p_weekday INT;
BEGIN -- Get the weekday value of the initial date (0 for Sunday, 1 for Monday, and so on)
p_weekday := EXTRACT(
    ISODOW
    FROM p_initialDate
);
-- If the initial date is not Monday (1), raise an exception and stop the insertion
IF p_weekday <> 1 THEN RAISE EXCEPTION 'InitialDate must be a Monday.';
END IF;
-- Calculate the end date as 7 days after the initial date
p_endDate := p_initialDate + INTERVAL '7 days';
INSERT INTO PlanPatient (PlanId, PatientId, InitialDate, EndDate)
VALUES (p_planId, p_patientId, p_initialDate, p_endDate);
END;
$$;
/*
The procedure "insert_nutri"  inserts a new record into the Nutritionist table. 
It takes multiple parameters to populate the columns of the table, including nutriId, email, password, 
name, lastName1, lastName2, birthDate, weight, IMC, nutritionistCode, cardNumber, province, canton, district, 
picture, adminId, and chargeTypeId. The procedure performs the insertion using the provided parameter values, 
handling NULL values with the COALESCE function where applicable.
*/
CREATE OR REPLACE PROCEDURE insert_nutri(
        p_nutriId int,
        p_email VARCHAR(100),
        p_password VARCHAR(100),
        p_name VARCHAR(100),
        p_lastName1 VARCHAR(100),
        p_lastName2 VARCHAR(100),
        p_birthDate DATE,
        p_weight INT,
        p_imc INT,
        p_nutritionistCode INT,
        p_cardNumber VARCHAR(20),
        p_province VARCHAR(100),
        p_canton VARCHAR(100),
        p_district VARCHAR(100),
        p_picture VARCHAR(10485760),
        p_adminId INT,
        p_chargeTypeId INT
    ) LANGUAGE plpgsql AS $$ BEGIN
INSERT INTO Nutritionist (
        Id,
        Email,
        Password,
        Name,
        LastName1,
        LastName2,
        BirthDate,
        Weight,
        IMC,
        NutritionistCode,
        CardNumber,
        Province,
        Canton,
        District,
        Picture,
        AdminId,
        ChargeTypeId
    )
VALUES (
        p_nutriId,
        p_email,
        p_password,
        p_name,
        p_lastName1,
        COALESCE(p_lastName2, ''),
        p_birthDate,
        COALESCE(p_weight, 0),
        COALESCE(p_imc, 0),
        p_nutritionistCode,
        COALESCE(p_cardNumber, ''),
        p_province,
        p_canton,
        p_district,
        p_picture,
        p_adminId,
        p_chargeTypeId
    );
END;
$$;
/*

The "register_measurements" procedure is used to insert a new set of measurements into the Measurements table. 
It takes several parameters including patient_id, height, fat_percentage, muscle_percentage, weight, waist, neck, 
hips, and revision_date. The procedure inserts the provided values into the corresponding columns of the Measurements 
table, creating a new record.
*/
CREATE PROCEDURE register_measurements(
    patient_id int,
    height float,
    fat_percentage float,
    muscle_percentage float,
    weight float,
    waist float,
    neck float,
    hips float,
    revision_date date
) LANGUAGE SQL AS $$
INSERT INTO Measurements (
        PatientId,
        Height,
        FatPercentage,
        MusclePercentage,
        Weight,
        Waist,
        Neck,
        Hips,
        RevisionDate
    )
VALUES (
        patient_id,
        height,
        fat_percentage,
        muscle_percentage,
        weight,
        waist,
        neck,
        hips,
        revision_date
    );
$$;
-- Triggers
-- This trigger ensures that the patient ID exists before allowing the insertion into the Measurements table.
CREATE TRIGGER check_patient_exists_measurements_trigger BEFORE
INSERT ON Measurements FOR EACH ROW EXECUTE FUNCTION check_patient_exists();

-- This trigger ensures that the patient ID exists before allowing the insertion into the PatientRecipe table.
CREATE TRIGGER check_patient_exists_patientrecipe_trigger BEFORE
INSERT ON PatientRecipe FOR EACH ROW EXECUTE FUNCTION check_patient_exists();

-- This trigger ensures that the patient ID exists before allowing the insertion into the PatientProduct table.
CREATE TRIGGER check_patient_exists_patientproduct_trigger BEFORE
INSERT ON PatientProduct FOR EACH ROW EXECUTE FUNCTION check_patient_exists();

-- This trigger prevents the insertion if the email already exists in the Administrator table.
CREATE TRIGGER CheckEmailExistsInAdministrator BEFORE
INSERT ON Administrator FOR EACH ROW EXECUTE FUNCTION check_email_exists();

-- This trigger prevents the insertion if the email already exists in the Patient table.
CREATE TRIGGER CheckEmailExistsInPatient BEFORE
INSERT ON Patient FOR EACH ROW EXECUTE FUNCTION check_email_exists();

-- This trigger prevents the insertion if the email already exists in the Nutritionist table.
CREATE TRIGGER CheckEmailExistsInNutritionist BEFORE
INSERT ON Nutritionist FOR EACH ROW EXECUTE FUNCTION check_email_exists();

-- This trigger prevents the insertion if the birthdate is in the future in the Nutritionist table.
CREATE TRIGGER CheckNutritionistDate BEFORE
INSERT ON Nutritionist FOR EACH ROW EXECUTE FUNCTION prevent_future_birthdates();

-- This trigger prevents the insertion if the birthdate is in the future in the Patient table.
CREATE TRIGGER CheckPatientDate BEFORE
INSERT ON Patient FOR EACH ROW EXECUTE FUNCTION prevent_future_birthdates();

-- This trigger prevents the insertion if the revision date is in the future in the Measurements table.
CREATE TRIGGER CheckMeasurementsDate BEFORE
INSERT ON Measurements FOR EACH ROW EXECUTE FUNCTION prevent_future_revisiondates();

-- This trigger prevents the insertion if the initial date is in the future in the PlanPatient table.
CREATE TRIGGER CheckPlanPatientInitialDate BEFORE
INSERT ON PlanPatient FOR EACH ROW EXECUTE FUNCTION prevent_future_initialdates();

-- This trigger prevents the insertion if the consume date is in the future in the PatientProduct table.
CREATE TRIGGER CheckPatientProductConsumeDate BEFORE
INSERT ON PatientProduct FOR EACH ROW EXECUTE FUNCTION prevent_future_consumedates();
