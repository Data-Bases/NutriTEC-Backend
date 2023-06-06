-- DateTime : 2023-06-03T22:45:26.650Z

-- Administrator
INSERT INTO Administrator (Email, Password) VALUES
('admin@example.com', md5('pepe'));

-- ChargeTypes
INSERT INTO ChargeType (Nombre) VALUES
('Weekly'),
('Monthly'),
('Anual');

-- Nutritionist
INSERT INTO Nutritionist (Email, Password, Name, LastName1, LastName2, BirthDate, Weight, IMC, NutritionistCode, CardNumber, Province, Canton, District, Picture, AdminId, ChargeTypeId) VALUES
('diana@example.com', md5('pepe'), 'Diana', 'Mejias', 'Hernandez', '2001-08-08', 60, 23, 12345, 123456, 'Puntarenas', 'Buenos Aires', 'Buenos Aires', 'picture.jpg', 1, 1),
('geo@example.com', md5('pepe'), 'Geovanny', 'Garcia', 'Downing', '2001-12-07', 60, 25, 67890, 321654, 'Guanacaste', 'Liberia', 'Liberia', 'avatar.jpg', 1, 2),
('vale@example.com',md5('pepe'), 'Valesska', 'Blanco', 'Montoya', '2001-05-22', 60, 20, 54321, 147258, 'Cartago', 'Cartago', 'Oriental', 'photo.jpg', 1, 3),
('ram@example.com', md5('pepe'), 'Ramses', 'Gutierrez', null, '2003-04-08', 60, 20, 98765, 963852, 'San Jose', 'Perez Zeledon', 'Centro', 'headshot.jpg', 1, 2),
('martinez@example.com', md5('pepe'), 'Andres', 'Martinez', null, '2001-04-25', 60, 21, 24680, 987654, 'Cartago', 'Cartago', 'Cartago', 'profile.jpg', 1, 1);

-- Patients with nutri
INSERT INTO Patient (NutriId, Email, Name, LastName1, LastName2, BirthDate, Password, Country, CaloriesIntake) VALUES
(1, 'juan@example.com', 'Juan', 'Navarro', 'Navarro', '2001-08-02',md5('pepe'), 'Canada', 1800),
(2, 'reii@example.com', 'Rashell', 'Aguilar', 'Caballero', '2001-07-10', md5('pepe'), 'United States', 2000),
(3, 'bobby@example.com', 'Bobby', 'Mejias', 'Hernandez', '2006-12-28', md5('pepe'), 'Mexico', 1600),
(4, 'chloe@example.com', 'Chloe', 'Mejias', 'Hernandez', '2011-11-20', md5('pepe'), 'Canada', 1800),
(5, 'pepe@example.com', 'Pepe', 'Blanco', 'Montoya', '2013-04-12', md5('pepe'), 'Canada', 1500),
(1, 'will@example.com', 'Wilberth', 'Mejias', 'Cruz', '1972-12-31', md5('pepe'), 'Costa Rica', 1800),
(2, 'anna@example.com', 'Anna', 'Barrrantes', 'Leiva', '1995-01-20', md5('pepe'), 'Costa Rica', 1800),
(3, 'katia@example.com', 'Katia', 'Navarro', 'Hernandez', '1970-10-23', md5('pepe'), 'Costa Rica', 1800); 

-- Patient without nutri
INSERT INTO Patient (Email, Name, LastName1, LastName2, BirthDate, Password, Country, CaloriesIntake) VALUES
('raquel@example.com', 'Raquel', 'Navarro', 'Navarro', '1990-05-20', md5('pepe'), 'Berlin', 1800),
('meli@example.com', 'Meli', 'Hernandez', 'Ovares',  '1996-11-20', md5('pepe'), 'Costa Rica', 1800),
('pri@example.com', 'Pri', 'Sanchez', 'Hernandez',  '2005-08-13', md5('pepe'), 'Costa Rica', 1800);

-- Measurements
INSERT INTO Measurements(PatientId, Height, FatPercentage, MusclePercentage, Weight, Waist, Neck, Hips, RevisionDate) VALUES
(1, 170, 25, 30, 70, 80, 35, 90, '2023-01-10'),
(1, 172, 26, 30, 70, 84, 33, 91, '2023-01-11'),
(1, 180, 27, 31, 78, 81, 34, 92, '2023-01-12'),
(2, 180, 20, 35, 80, 85, 38, 95, '2023-02-15'),
(3, 165, 30, 25, 60, 75, 32, 85, '2023-03-20'),
(4, 175, 22, 32, 75, 82, 37, 92, '2023-04-25'),
(5, 160, 28, 28, 65, 78, 34, 88, '2023-05-01');

-- Vitamin
INSERT INTO Vitamin (Name, Amount) VALUES
('Vitamin C', 50),
('Vitamin D', 10),
('Vitamin B12', 2),
('Vitamin A', 100),
('Vitamin E', 15);

-- Product
INSERT INTO Product (Barcode, Name, Descripcion, PortionSize, Energy, Fat, Sodium, Carbs, Protein, Calcium, Iron, IsApproved) VALUES
(11111, 'Apple', 'Red delicious apple', 100, 52, 0.2, 0, 14, 0.3, 6, 0.2, true),
(22222, 'Milk', 'Low-fat milk', 250, 103, 2.4, 105, 12, 8, 276, 0.1, false),
(33333, 'Chicken', 'Fried chicken', 100, 100, 165, 3.6, 66, 0, 31, 0.01, false),
(44444, 'Yogurt', 'Low-fat strawberry yogurt', 150, 120, 1.5, 95, 20, 5, 200, 0.1, false),
(55555, 'Salmon', 'Grilled salmon fillet', 150, 150, 275, 15, 55, 0, 30, 0.6, false);


-- Recipe
INSERT INTO Recipe(Id, Name) VALUES (1, 'Chicken with apple'), (2, 'Salmon with apple'), (3, 'Yogurt with apple');

-- ProductRecipe 
INSERT INTO ProductRecipe(ProductBarcode, RecipeId, Servings) VALUES (33333, 1, 2.5), 
                                                                    (22222, 1, 0.5), 
                                                                    (11111, 1, 2),
                                                                    (55555, 2, 1),
                                                                    (11111, 2, 2),
                                                                    (44444, 3, 1.5),
                                                                    (11111, 3, 1);

-- PatienProduct
INSERT INTO PatientProduct (ProductBarcode, PatientId, MealTime, ConsumeDate, Servings)
VALUES (22222, 1, 'Breakfast', '2023-06-03', 2.5),
       (44444, 1, 'Snack', '2023-06-03', 1.5),
       (11111, 1, 'Dinner', '2023-06-03', 3),
       (22222, 2, 'Breakfast', '2023-06-01', 1),
       (44444, 3, 'Snack', '2023-06-01', 1.5),
       (11111, 4, 'Dinner', '2023-06-01', 3);


-- PatientRecipe
INSERT INTO PatientRecipe (RecipeId, PatientId, MealTime, ConsumeDate, Servings)
VALUES (3, 1, 'Breakfast', '2023-06-03', 2.5),
       (2, 1, 'Lunch', '2023-06-03', 1.5),
       (2, 1, 'Dinner', '2023-06-03', 3),
       (3, 2, 'Breakfast', '2023-06-01', 2.5),
       (2, 3, 'Lunch', '2023-06-01', 1.5),
       (2, 3, 'Dinner', '2023-06-01', 3);


-- Plans
INSERT INTO Plan (NutriId, Name)
VALUES (1, 'Plan Atleta Ciclismo'),
       (1, 'Plan Adelgazar'),
       (1, 'Plan Aumento Peso'),
       (3, 'Plan Aumento Muscular');

-- Insert values into the PlanProduct table
INSERT INTO PlanProduct (ProductBarcode, PlanId, Servings, MealTime, ConsumeWeekDay)
VALUES (11111, 1, 2.5, 'Breakfast', 'Monday'),
       (33333, 1, 3.0, 'Dinner', 'Monday'),
       (44444, 2, 1.5, 'Breakfast', 'Tuesday'),
       (11111, 3, 2.5, 'Breakfast', 'Wednesday'),
       (44444, 3, 1.5, 'Snack', 'Wednesday'),
       (33333, 3, 3.0, 'Dinner', 'Wednesday');

-- Insert values into the PlanRecipe table
INSERT INTO PlanRecipe (RecipeId, PlanId, Servings, MealTime, ConsumeWeekDay)
VALUES (3, 1, 2, 'Snack', 'Monday'),
       (2, 1, 1.5, 'Lunch', 'Monday'),
       (3, 1, 2, 'Breakfast', 'Tuesday'),
       (1, 1, 1.5, 'Lunch', 'Tuesday'),
       (3, 2, 3.0, 'Dinner', 'Tuesday');

-- Insert values into the PlanPatient table
INSERT INTO PlanPatient (PlanId, PatientId, InitialDate, EndDate)
VALUES (1, 1, '2023-05-22', '2023-05-29'),
        (2, 1, '2023-05-30', '2023-06-04'),
        (1, 2, '2023-05-30', '2023-06-04');  

-- ProductVitamin
INSERT INTO ProductVitamin (ProductBarcode, VitaminId) VALUES
(11111, 1),
(11111, 2),
(22222, 3),
(33333, 4),
(44444, 5);

-- Plan Marco Rivera

--Patient
INSERT INTO Patient (NutriId, Email, Name, LastName1, LastName2, BirthDate, Password, Country, CaloriesIntake) VALUES
(1, 'marco_rivera@example.com', 'Marco', 'Rivera', 'Meneses', '2005-05-07',md5('basesdedatos'), 'CostaRica', 2500);

--PlanPatient
INSERT INTO PlanPatient(PlanId, PatientId, InitialDate, EndDate)
VALUES (4, 12, '2023-05-22', '2023-05-29');

--Products
INSERT INTO Product (Barcode, Name, Descripcion, PortionSize, Energy, Fat, Sodium, Carbs, Protein, Calcium, Iron, IsApproved) VALUES
(11112, 'Rice', 'Long-grain white rice', 100, 130, 0.3, 0, 28, 2.7, 8, 0.4, true),
(11113, 'Beans', 'Black beans', 50, 227, 0.9, 1, 41, 15, 39, 2.6, true),
(11114, 'Chicken', 'Grilled chicken breast', 100, 165, 3.6, 64, 0, 31, 14, 0.6, true),
(11115, 'Tuna', 'Canned tuna in water', 175, 116, 0.5, 384, 0, 26, 9, 1.2, true); 

-- Recipes
INSERT INTO Recipe(Id, Name) VALUES (10, 'Gallo Pinto'), (11, 'Rice With Ckicken'), (12, 'Rice With Tuna');

--ProductRecipes
  -- Gallo Pinto
INSERT INTO ProductRecipe(ProductBarcode, RecipeId, Servings) VALUES
(11112, 10, 1),
(11113, 10, 1);

  -- Rice With Ckicken
INSERT INTO ProductRecipe(ProductBarcode, RecipeId, Servings) VALUES
(11112, 11, 1),
(11114, 11, 1);

  -- Rice With Tuna
INSERT INTO ProductRecipe(ProductBarcode, RecipeId, Servings) VALUES
(11112, 12, 1),
(11115, 12, 1);

-- PlanRecipe and PlanProduct
INSERT INTO PlanRecipe(RecipeId, PlanId, Servings, MealTime, ConsumeWeekDay) VALUES
(10, 4, 1, 'Breakfast', 'Monday'),
(11, 4, 1, 'Lunch', 'Monday'),
(12, 4, 1, 'Dinner', 'Monday'),
(10, 4, 1, 'Breakfast', 'Tuesday'),
(11, 4, 1, 'Lunch', 'Tuesday'),
(12, 4, 1, 'Dinner', 'Tuesday'),
(10, 4, 1, 'Breakfast', 'Wednesday'),
(11, 4, 1, 'Lunch', 'Wednesday'),
(12, 4, 1, 'Dinner', 'Wednesday'),
(10, 4, 1, 'Breakfast', 'Thursday'),
(11, 4, 1, 'Lunch', 'Thursday'),
(12, 4, 1, 'Dinner', 'Thursday'),
(10, 4, 1, 'Breakfast', 'Friday'),
(11, 4, 1, 'Lunch', 'Friday'),
(12, 4, 1, 'Dinner', 'Friday'),
(10, 4, 1, 'Breakfast', 'Saturday'),
(11, 4, 1, 'Lunch', 'Saturday'),
(12, 4, 1, 'Dinner', 'Saturday'),
(10, 4, 1, 'Breakfast', 'Sunday'),
(11, 4, 1, 'Lunch', 'Sunday'),
(12, 4, 1, 'Dinner', 'Sunday');
INSERT INTO PlanProduct(ProductBarcode, PlanId, Servings, MealTime, ConsumeWeekDay) VALUES
(11111, 4, 1, 'Snack', 'Monday'),
(11111, 4, 1, 'Snack', 'Tuesday'),
(11111, 4, 1, 'Snack', 'Wednesday'),
(11111, 4, 1, 'Snack', 'Thursday'),
(11111, 4, 1, 'Snack', 'Friday'),
(11111, 4, 1, 'Snack', 'Saturday'),
(11111, 4, 1, 'Snack', 'Sunday');