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
INSERT INTO Nutritionist (Id, Email, Password, Name, LastName1, LastName2, Age, BirthDate, Weight, IMC, NutritionistCode, CardNumber, Province, Canton, District, Picture, AdminId, ChargeTypeId) VALUES
(1, 'diana@example.com', md5('pepe'), 'Diana', 'Mejias', 'Hernandez', 21, '2001-08-08', 60, 23, 12345, 123456, 'Puntarenas', 'Buenos Aires', 'Buenos Aires', 'picture.jpg', 1, 1),
(2, 'geo@example.com', md5('pepe'), 'Geovanny', 'Garcia', 'Downing', 21, '2001-12-07', 60, 25, 67890, 321654, 'Guanacaste', 'Liberia', 'Liberia', 'avatar.jpg', 1, 2),
(3, 'vale@example.com',md5('pepe'), 'Valesska', 'Blanco', 'Montoya', 22, '2001-05-22', 60, 20, 54321, 147258, 'Cartago', 'Cartago', 'Oriental', 'photo.jpg', 1, 3),
(4, 'ram@example.com', md5('pepe'), 'Ramses', 'Gutierrez', null, 20, '2003-04-08', 60, 20, 98765, 963852, 'San Jose', 'Perez Zeledon', 'Centro', 'headshot.jpg', 1, 2),
(5, 'martinez@example.com', md5('pepe'), 'Andres', 'Martinez', null, 22, '2001-04-25', 60, 21, 24680, 987654, 'Cartago', 'Cartago', 'Cartago', 'profile.jpg', 1, 1);

-- Patients
INSERT INTO Patient (Id, NutriId, Email, Name, LastName1, LastName2, Age, BirthDate, Password, Country, CaloriesIntake) VALUES
(6, 1, 'juan@example.com', 'Juan', 'Navarro', 'Navarro', 22, '2001-08-02',md5('pepe'), 'Canada', 1800),
(7, 2, 'reii@example.com', 'Rashell', 'Aguilar', 'Caballero', 21, '2001-07-10', md5('pepe'), 'United States', 2000),
(8, 3, 'bobby@example.com', 'Bobby', 'Mejias', 'Hernandez', 17, '2006-12-28', md5('pepe'), 'Mexico', 1600),
(9, 4, 'chloe@example.com', 'Chloe', 'Mejias', 'Hernandez', 12, '2011-11-20', md5('pepe'), 'Canada', 1800),
(10, 5, 'pepe@example.com', 'Pepe', 'Blanco', 'Montoya', 10, '2013-04-12', md5('pepe'), 'Canada', 1500),
(11, 1, 'will@example.com', 'Wilberth', 'Mejias', 'Cruz', 52, '1972-12-31', md5('pepe'), 'Costa Rica', 1800),
(12, 2, 'anna@example.com', 'Anna', 'Barrrantes', 'Leiva', 32, '1995-01-20', md5('pepe'), 'Costa Rica', 1800),
(13, 3, 'katia@example.com', 'Katia', 'Navarro', 'Hernandez', 52, '1970-10-23', md5('pepe'), 'Costa Rica', 1800),
(14, 4, 'raquel@example.com', 'Raquel', 'Navarro', 'Navarro', 25, '1990-05-20', md5('pepe'), 'Berlin', 1800),
(15, 5, 'meli@example.com', 'Meli', 'Hernandez', 'Ovares', 30, '1996-11-20', md5('pepe'), 'Costa Rica', 1800),
(16, 1, 'pri@example.com', 'Pri', 'Sanchez', 'Hernandez', 18, '2005-08-13', md5('pepe'), 'Costa Rica', 1800);

-- Measurements
INSERT INTO Measurements(PatientId, Height, FatPercentage, MusclePercentage, Weight, Waist, Neck, Hips, RevisionDate) VALUES
(6, 170, 25, 30, 70, 80, 35, 90, '2023-01-10'),
(7, 180, 20, 35, 80, 85, 38, 95, '2023-02-15'),
(8, 165, 30, 25, 60, 75, 32, 85, '2023-03-20'),
(9, 175, 22, 32, 75, 82, 37, 92, '2023-04-25'),
(10, 160, 28, 28, 65, 78, 34, 88, '2023-05-01');

-- Vitamin
INSERT INTO Vitamin (Name, Amount) VALUES
('Vitamin C', 50),
('Vitamin D', 10),
('Vitamin B12', 2),
('Vitamin A', 100),
('Vitamin E', 15);

-- Product
INSERT INTO Product (Barcode, Name, Descripcion, PortionSize, Energy, Fat, Sodium, Carbs, Protein, Calcium, Iron, IsApproved) VALUES
(11111, 'Apple', 'Red delicious apple', 1, 52, 0.2, 0, 14, 0.3, 6, 0.2, true),
(22222, 'Milk', 'Low-fat milk', 250, 103, 2.4, 105, 12, 8, 276, 0.1, false),
(33333, 'Chicken', 'Grilled chicken breast', 1, 100, 165, 3.6, 66, 0, 31, 0.01, false),
(44444, 'Yogurt', 'Low-fat strawberry yogurt', 150, 120, 1.5, 95, 20, 5, 200, 0.1, false),
(55555, 'Salmon', 'Grilled salmon fillet', 1, 150, 275, 15, 55, 0, 30, 0.6, false);


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
VALUES (22222, 6, 'Breakfast', '2023-06-03', 2.5),
       (44444, 6, 'Snack', '2023-06-03', 1.5),
       (11111, 6, 'Dinner', '2023-06-03', 3),
       (22222, 7, 'Breakfast', '2023-06-01', 1),
       (44444, 8, 'Snack', '2023-06-01', 1.5),
       (11111, 9, 'Dinner', '2023-06-01', 3);


-- PatientRecipe
INSERT INTO PatientRecipe (RecipeId, PatientId, MealTime, ConsumeDate, Servings)
VALUES (3, 6, 'Breakfast', '2023-06-03', 2.5),
       (2, 6, 'Lunch', '2023-06-03', 1.5),
       (2, 6, 'Dinner', '2023-06-03', 3),
       (3, 7, 'Breakfast', '2023-06-01', 2.5),
       (2, 8, 'Lunch', '2023-06-01', 1.5),
       (2, 8, 'Dinner', '2023-06-01', 3);



-- ProductVitamin
INSERT INTO ProductVitamin (ProductBarcode, VitaminId) VALUES
(11111, 1),
(11111, 2),
(22222, 3),
(33333, 4),
(44444, 5);
