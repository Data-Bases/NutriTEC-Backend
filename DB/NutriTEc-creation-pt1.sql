-- Nutritionist
CREATE TABLE Nutritionist
(
	Id SERIAL NOT NULL,
	Email VARCHAR(100) UNIQUE NOT NULL,
	Password VARCHAR(100) NOT NULL,
	Name VARCHAR(100) NOT NULL,
	LastName1 VARCHAR(100) NOT NULL,
	LastName2 VARCHAR(100),
	Age INT NOT NULL,
    BirthDate DATE NOT NULL,
	Weight INT,
	IMC INT,
	NutritionistCode INT NOT NULL,
	CardNumber INT,
	Province VARCHAR(100) NOT NULL,
	Canton VARCHAR(100) NOT NULL,
	District VARCHAR(100) NOT NULL,
	Picture VARCHAR(10485760) NOT NULL,
	AdminId INT NOT NULL,
	ChargeTypeId INT NOT NULL,

	PRIMARY KEY (Id)
);

-- Product
CREATE TABLE Product
(
	Barcode INT NOT NULL,
	Name VARCHAR(100) NOT NULL,
	Descripcion VARCHAR(100) NOT NULL,
	PortionSize FLOAT NOT NULL,
    Energy FLOAT NOT NULL,
    Fat FLOAT NOT NULL,
    Sodium FLOAT NOT NULL,
    Carbs FLOAT NOT NULL,
    Protein FLOAT NOT NULL,
    Calcium FLOAT NOT NULL,
    Iron FLOAT NOT NULL,
    IsApproved BOOLEAN,

	PRIMARY KEY (Barcode)
);

-- Patient
CREATE TABLE Patient
(
	Id SERIAL NOT NULL,
    NutriId INT,
	Email VARCHAR(100) UNIQUE NOT NULL,
	Name VARCHAR(100) NOT NULL,
	LastName1 VARCHAR(100) NOT NULL,
	LastName2 VARCHAR(100),
	Age INT NOT NULL,
    BirthDate DATE NOT NULL,
	Password VARCHAR(100) NOT NULL,
	Country VARCHAR(100) NOT NULL,
	CaloriesIntake INT,

	PRIMARY KEY (Id)
);

-- Recipe
CREATE TABLE Recipe
(
	Id SERIAL NOT NULL,
	Name VARCHAR(100) UNIQUE NOT NULL,

	PRIMARY KEY (Id)
);

-- Plan
CREATE TABLE Plan
(
	Id SERIAL NOT NULL,
	NutriId INT NOT NULL,
	Name VARCHAR(250) NOT NULL,

	PRIMARY KEY (Id)
);

-- Vitamin
CREATE TABLE Vitamin
(
	Id SERIAL NOT NULL,
	Name VARCHAR(100) NOT NULL,
    Amount FLOAT NOT NULL,

	PRIMARY KEY (Id)
);

-- Admin
CREATE TABLE Administrator
(
	Id SERIAL NOT NULL,
	Email VARCHAR(100) UNIQUE NOT NULL,
	Password VARCHAR(100) NOT NULL,

	PRIMARY KEY (Id)
);

-- ChargeType
CREATE TABLE ChargeType
(
	Id SERIAL NOT NULL,
	Nombre VARCHAR(100) UNIQUE NOT NULL,

	PRIMARY KEY (Id)
);

-- Measurements
CREATE TABLE Measurements
(
	Id SERIAL NOT NULL,
	PatientId INT NOT NULL,
	Height FLOAT NOT NULL,
    FatPercentage FLOAT NOT NULL,
    MusclePercentage FLOAT NOT NULL,
	Weight FLOAT NOT NULL,
    Waist FLOAT NOT NULL,
    Neck FLOAT NOT NULL,
    Hips FLOAT NOT NULL,
    RevisionDate DATE NOT NULL,

	PRIMARY KEY (Id)
);

-- PlanPatient
CREATE TABLE PlanPatient
(
	Id SERIAL NOT NULL,
	PlanId INT NOT NULL,
	PatientId INT NOT NULL,
    InitialDate DATE NOT NULL,
    EndDate DATE NOT NULL,

	PRIMARY KEY (Id)
);

-- PatientRecipe
CREATE TABLE PatientRecipe
(
	Id SERIAL NOT NULL,
	RecipeId INT NOT NULL,
	PatientId INT NOT NULL,
    MealTime VARCHAR(50) NOT NULL,
    ConsumeDate DATE NOT NULL,
	Servings FLOAT NOT NULL,

	PRIMARY KEY (Id)
);

-- ProductRecipe
CREATE TABLE ProductRecipe
(
	Id SERIAL NOT NULL,
	ProductBarcode INT NOT NULL,
    RecipeId INT NOT NULL,
	Servings FLOAT NOT NULL,

	PRIMARY KEY (Id)
);

-- PlanProducts
CREATE TABLE PlanProduct
(
	Id SERIAL NOT NULL,
	ProductBarcode INT NOT NULL,
    PlanId INT NOT NULL,
	Servings FLOAT NOT NULL,
    MealTime VARCHAR(50) NOT NULL,
    ConsumeWeekDay VARCHAR(50) NOT NULL,

	PRIMARY KEY (Id)
);

-- PlanRecipe
CREATE TABLE PlanRecipe
(
	Id SERIAL NOT NULL,
	RecipeId INT NOT NULL,
    PlanId INT NOT NULL,
	Servings FLOAT NOT NULL,
    MealTime VARCHAR(50) NOT NULL,
    ConsumeWeekDay VARCHAR(50) NOT NULL,

	PRIMARY KEY (Id)
);

-- ProductVitamin
CREATE TABLE ProductVitamin
(
    Id SERIAL NOT NULL,
	ProductBarcode INT NOT NULL,
    VitaminId INT NOT NULL,

	PRIMARY KEY (Id)
);

-- PatientProduct
CREATE TABLE PatientProduct
(
	Id SERIAL NOT NULL,
	ProductBarcode INT NOT NULL,
    PatientId INT NOT NULL,
    MealTime VARCHAR(50) NOT NULL,
	ConsumeDate DATE NOT NULL,
	Servings FLOAT NOT NULL,

	PRIMARY KEY (Id)
);