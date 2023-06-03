CREATE PROCEDURE register_measurements(patient_id int, height float, fat_percentage float, muscle_percentage float, weight float, waist float, neck float, hips float, revision_date date)

LANGUAGE SQL

AS $$

INSERT INTO Measurements (PatientId, Height, FatPercentage, MusclePercentage, Weight, Waist, Neck, Hips, RevisionDate)
VALUES (patient_id, height, fat_percentage, muscle_percentage, weight, waist, neck, hips, revision_date);

$$;

CALL register_measurements(7, 70, 18, 10, 50, 45, 7, 90, '2020-05-05');

select * from Measurements


drop procedure register_measurements;


CREATE FUNCTION get_consumed_recipes(id_patient int, consumed_data date)
    RETURNS TABLE (
		Id int, 
		Name varchar(100),
		Servings float,
		Calories float
	)
	AS $$
	BEGIN
		RETURN QUERY SELECT
				R.Id as Id,
				R.Name as Name,
				PR.Servings as Servings,
				PR.Servings * R.Energy as Calories
		FROM recipe as R 
		JOIN patientRecipe as PR
		ON R.Id = PR.RecipeId
		WHERE PR.PatientId = id_patient;
	END; $$ 

LANGUAGE 'plpgsql';


CREATE FUNCTION get_consumed_products(id_patient int, consumed_data date)
    RETURNS TABLE (
		Id int, 
		Name varchar(100),
		Servings float,
		Calories float
	)
	AS $$
	BEGIN
		RETURN QUERY SELECT
				R.Id as Id,
				R.Name as Name,
				PR.Servings as Servings,
				PR.Servings * R.Energy as Calories
		FROM recipe as R 
		JOIN patientRecipe as PR
		ON R.Id = PR.RecipeId
		WHERE PR.PatientId = id_patient;
	END; $$ 

LANGUAGE 'plpgsql';