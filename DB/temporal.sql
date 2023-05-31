
CREATE OR REPLACE VIEW products_in_recipe AS
SELECT R.name as recipename, R.Id as recipeid, P.name as productname, 
P.portionsize as portionsize, PR.servings as servings, P.energy * PR.servings as energy, 
P.Fat * PR.servings as fat, P.Sodium * PR.servings as sodium, 
P.Carbs* PR.servings as carbs, P.Protein * PR.servings as protein, 
P.Calcium * PR.servings as calcium, P.Iron * PR.servings as iron
FROM (recipe as R  join productrecipe as PR on R.Id = PR.recipeid) join product as P on P.barcode = PR.productbarcode;


Select recipename, recipeid, productname, portionsize, servings, energy, fat, sodium, carbs, protein, calcium, iron from products_in_recipe where recipeid = 14;


DROP VIEW IF EXISTS products_in_recipe

create procedure calculate_recipe_nutrients(recipe_id int)
language plpgsql
as $$
declare
-- variable declaration
begin
	create temporary table temp_recipe
	(
		RecipeName varchar(100),
		TotalEnergy float, 
		TotalSodium float,
		TotalCarbs float,
		TotalProtein float,
		TotalCalcium float,
        TotalFat float,
		TotalIron float
	);
	
	insert into temp_recipe
		select
			recipename,
			SUM(PR.energy),
			SUM(PR.sodium),
			SUM(PR.carbs),
			SUM(PR.protein),
			SUM(PR.calcium),
            SUM(PR.fat),
			SUM(PR.iron)
		from products_in_recipe as PR
		where PR.recipeid = recipe_id
		group by recipename;
		
	Perform  * from temp_recipe;
	
	COMMIT;
end; $$


DROP TABLE temp_recipe
drop procedure calculate_recipe_nutrients;

SELECT * FROM temp_recipe

select * from recipe
Call calculate_recipe_nutrients(12);