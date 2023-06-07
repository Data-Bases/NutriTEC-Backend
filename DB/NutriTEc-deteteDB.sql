ALTER TABLE ProductRecipe DROP CONSTRAINT ProductRecipe_Barcode;
ALTER TABLE ProductRecipe DROP CONSTRAINT ProductRecipe_RecipeId;

ALTER TABLE PatientRecipe DROP CONSTRAINT PatientRecipe_PatientId;
ALTER TABLE PatientRecipe DROP CONSTRAINT PatientRecipe_RecipeId;

ALTER TABLE Patient DROP CONSTRAINT Patient_NutriId;

ALTER TABLE PlanPatient DROP CONSTRAINT PlanPatient_PlanId;
ALTER TABLE PlanPatient DROP CONSTRAINT PlanPatient_PatientId;
ALTER TABLE PlanPatient DROP CONSTRAINT unique_plan_patient;

ALTER TABLE Measurements DROP CONSTRAINT Measurements_PatientId;

ALTER TABLE Plan DROP CONSTRAINT Plan_NutriId;
ALTER TABLE Plan DROP CONSTRAINT unique_plan;

ALTER TABLE PlanProduct DROP CONSTRAINT PlanProduct_ProductBarcode;
ALTER TABLE PlanProduct DROP CONSTRAINT PlanProduct_PlanId;

ALTER TABLE PlanRecipe DROP CONSTRAINT PlanRecipe_RecipeId;
ALTER TABLE PlanRecipe DROP CONSTRAINT PlanRecipe_PlanId;

ALTER TABLE ProductVitamin DROP CONSTRAINT ProductVitamin_ProductBarcode;
ALTER TABLE ProductVitamin DROP CONSTRAINT ProductVitamin_Vitamin;

ALTER TABLE PatientProduct DROP CONSTRAINT PatientProduct_ProductBarcode;
ALTER TABLE PatientProduct DROP CONSTRAINT PatientProduct_PatientId;

ALTER TABLE Nutritionist DROP CONSTRAINT Nutritionist_AdminEmail;
ALTER TABLE Nutritionist DROP CONSTRAINT Nutritionist_ChargeTypeId;

DROP VIEW IF EXISTS UserCredentials;
DROP VIEW IF EXISTS products_in_recipe;
DROP VIEW patient_products;
DROP VIEW patient_recipe;

DROP TRIGGER CheckEmailExistsInAdministrator ON Administrator;
DROP TRIGGER CheckEmailExistsInPatient ON Patient;
DROP TRIGGER CheckEmailExistsInNutritionist ON Nutritionist;

DROP FUNCTION create_recipe(character varying);
DROP FUNCTION calculate_recipe_nutrients(int);
DROP FUNCTION get_total_patients;
DROP FUNCTION amount_to_charge;
DROP FUNCTION payroll;
DROP FUNCTION calculate_product_servings;
DROP FUNCTION get_consumed_product;
DROP FUNCTION get_consumed_recipe;
DROP FUNCTION get_patient_measurements;
DROP FUNCTION create_plan;
DROP FUNCTION get_product_plan;
DROP FUNCTION get_recipe_plan;

DROP PROCEDURE register_measurements;
DROP PROCEDURE insert_nutri;
DROP PROCEDURE insert_plan_patient;
DROP PROCEDURE delete_recipe;
DROP PROCEDURE delete_plan;
DROP PROCEDURE delete_product;


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
DROP TABLE PlanRecipe;

DROP FUNCTION IF EXISTS check_email_exists();
DROP FUNCTION IF EXISTS prevent_future_birthdates();
DROP FUNCTION IF EXISTS prevent_future_revisiondates();
DROP FUNCTION IF EXISTS prevent_future_initialdates();
DROP FUNCTION IF EXISTS prevent_future_consumedates();
