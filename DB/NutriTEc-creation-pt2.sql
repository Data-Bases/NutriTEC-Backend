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
