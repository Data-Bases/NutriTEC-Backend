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

-- Patient-Nutricionist
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

-- Plan-Nutricionist
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

ALTER TABLE Nutritionist
ADD CONSTRAINT Nutritionist_AdminEmail
FOREIGN KEY (AdminEmail) REFERENCES Administrator(Email);

ALTER TABLE Nutritionist
ADD CONSTRAINT Nutritionist_ChargeTypeId
FOREIGN KEY (ChargeTypeId) REFERENCES ChargeType(Id);