ALTER TABLE ProductRecipe DROP CONSTRAINT ProductRecipe_Barcode;
ALTER TABLE ProductRecipe DROP CONSTRAINT ProductRecipe_RecipeId;

ALTER TABLE PatientRecipe DROP CONSTRAINT PatientRecipe_PatientId;
ALTER TABLE PatientRecipe DROP CONSTRAINT PatientRecipe_RecipeId;

ALTER TABLE Patient DROP CONSTRAINT Patient_NutriId;

ALTER TABLE PlanPatient DROP CONSTRAINT PlanPatient_PlanId;
ALTER TABLE PlanPatient DROP CONSTRAINT PlanPatient_PatientId;

ALTER TABLE Measurements DROP CONSTRAINT Measurements_PatientId;

ALTER TABLE Plan DROP CONSTRAINT Plan_NutriId;

ALTER TABLE PlanProduct DROP CONSTRAINT PlanProduct_ProductBarcode;
ALTER TABLE PlanProduct DROP CONSTRAINT PlanProduct_PlanId;

ALTER TABLE ProductVitamin DROP CONSTRAINT ProductVitamin_ProductBarcode;
ALTER TABLE ProductVitamin DROP CONSTRAINT ProductVitamin_Vitamin;

ALTER TABLE PatientProduct DROP CONSTRAINT PatientProduct_ProductBarcode;
ALTER TABLE PatientProduct DROP CONSTRAINT PatientProduct_PatientId;

ALTER TABLE Nutritionist DROP CONSTRAINT Nutritionist_AdminEmail;
ALTER TABLE Nutritionist DROP CONSTRAINT Nutritionist_ChargeTypeId;

DROP VIEW IF EXISTS UserCredentials;
DROP VIEW IF EXISTS products_in_recipe;

DROP TRIGGER CheckEmailExistsInAdministrator ON Administrator;
DROP TRIGGER CheckEmailExistsInPatient ON Patient;
DROP TRIGGER CheckEmailExistsInNutritionist ON Nutritionist;

DROP FUNCTION IF EXISTS check_email_exists();
DROP FUNCTION create_recipe(character varying);
DROP FUNCTION calculate_recipe_nutrients(int);


DROP TABLE Nutritionist;
DROP TABLE Product;
DROP TABLE Patient;
DROP TABLE Recipe;
DROP TABLE Plan;
DROP TABLE Vitamin;
DROP TABLE Administrator;
DROP TABLE ChargeType;
DROP TABLE Measurements;
DROP TABLE PlanPatient;
DROP TABLE PatientRecipe;
DROP TABLE ProductRecipe;
DROP TABLE PlanProduct;
DROP TABLE ProductVitamin;
DROP TABLE PatientProduct;

