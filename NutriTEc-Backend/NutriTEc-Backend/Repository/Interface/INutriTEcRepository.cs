﻿using Nest;
using NutriTEc_Backend.Dtos;
using NutriTEc_Backend.Models;

namespace NutriTEc_Backend.Repository.Interface
{
    public interface INutriTEcRepository
    {
        List<VitaminDto> GetAllVitamins();


        /*
         * Credentials
         */
        
        /// <summary>
        /// Get Users by email to verify its credentials
        /// </summary>
        /// <param name="email"></param>
        /// <returns> User creadentials {id, name, email, password, userType} </returns>
        UserCredentialsDto GetUserByEmail(string email);


        /*
         * Admin
         */

        /// <summary>
        /// Creates Admin
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        List<AdminDto> GetAdmin();

        /*
         * Nutri
         */

        /// <summary>
        /// Nutri sign up
        /// </summary>
        /// <param name="nutri"></param>
        /// <returns>Result</returns>
        Result NutriSignUp(NutriNoAgeDto nutri);

        /// <summary>
        /// Get all the patients asociated to a nutriId
        /// </summary>
        /// <param name="nutriId"></param>
        /// <returns>A List of Patients</returns>
        List<PatientIdDto> GetPatientsByNutriId(int nutriId);

        /// <summary>
        /// Get all the plans asociated to a nutricionist
        /// </summary>
        /// <param name="nutriId"></param>
        /// <returns>List of PlanIdDtos</returns>
        List<PlanIdDto> GetNutritionistPlans(int nutriId);

        /// <summary>
        /// Get nutritionist by Id
        /// </summary>
        /// <param name="nutriId"></param>
        /// <returns></returns>
        NutriDto GetNutritionistById(int nutriId);

        /// <summary>
        /// Get patients that are not related to any nutritionist
        /// </summary>
        /// <returns></returns>
        List<PatientIdDto> GetAvailablePatients();


        /*
         * Patient
         */

        /// <summary>
        /// Patient sign up
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>Result</returns>
        Result PatientSignUp(PatientNoAgeDto patient);

        /// <summary>
        /// Asign patient to nutritionist
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="nutriId"></param>
        /// <returns></returns>
        Result AsignPatientToNutri(int patientId, int nutriId);

        /// <summary>
        /// Get patient information by id
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        PatientDto GetPatientById(int patientId);

        /// <summary>
        /// Adding a product asociated to a patient
        /// </summary>
        /// <param name="patientProductDto"></param>
        /// <returns>Result</returns>
        Result AddProductToPatient(PatientProductDto patientProductDto);

        /// <summary>
        /// 
        /// Adding a recipe to a patient
        /// </summary>
        /// <param name="patientRecipeDto"></param>
        /// <returns>Result</returns>
        Result AddRecipeToPatient(PatientRecipeDto patientRecipeDto);

        /// <summary>
        /// Asociating a plan to a patient
        /// </summary>
        /// <param name="planPatientDto"></param>
        /// <returns>Result</returns>
        Result AddPlanToPatient(PlanPatientDto planPatientDto);

        /// <summary>
        /// Registers Patient Measurements
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="measurementDto"></param>
        /// <param name="revisionDate"></param>
        /// <returns>Result</returns>
        Result RegisterPatientMeasurements(int patientId, MeasurementDto measurementDto);

        /// <summary>
        /// Getting a patients nutricionist id and name
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>The id and name of the nutricionist</returns>
        NutriIdDto GetPatientsNutritionist(int patientId);

        /// <summary>
        /// Get a list of products consumed by patients in a certain date
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="dateConsumed"></param>
        /// <returns>ConsumedByPatient</returns>
        List<ConsumedByPatient> GetConsumedRecipesByPatient(int patientId, DateTime dateConsumed);

        /// <summary>
        /// Get a list of products consumed by patients in a certain date
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="dateConsumed"></param>
        /// <returns>ConsumedByPatient</returns>
        List<ConsumedByPatient> GetConsumedProductsByPatient(int patientId, DateTime dateConsumed);

        /// <summary>
        /// Get daily consumption by a patient on a certain date
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="dateConsumed"></param>
        /// <returns>DailyConsumptionDto</returns>
        DailyConsumptionDto GetDailyConsumptionByPatient(int patientId, DateTime dateConsumed);

        /// <summary>
        /// Get the measurements of a patient on a specific time gap
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="startDate"></param>
        /// <param name="finishDate"></param>
        /// <returns>List of Measurements</returns>
        List<MeasurementFunc> GetPatientMeasurementsByDate(int patientId, DateTime startDate, DateTime finishDate);

        /*
         * Recipe
         */
        /// <summary>
        /// Create recipe name with products
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Result</returns>
        Result CreateRecipe(RecipeXProductsDto recipe);

        /// <summary>
        /// Get all recipes from DB
        /// </summary>
        /// <returns>A list of names and ids</returns>
        List<RecipeDto> GetRecipes();

        /// <summary>
        /// Get recipe by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RecipeInfoDto GetRecipeById(int id, double recipeServings);
        
        /// <summary>
        /// Delete recipe
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns>Result</returns>
        Result DeleteRecipe(int recipeId);

        /// <summary>
        /// Delete a product from a recipie
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="productId"></param>
        /// <returns>Result</returns>
        Result DeleteProductInRecipe(int recipeId, int productId);

        /// <summary>
        /// Edit product in recipe
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="productId"></param>
        /// <param name="servings"></param>
        /// <returns>Result</returns>
        Result EditProductInRecipe(int recipeId, int productId, double servings);

        /// <summary>
        /// Inserts a product into existing recipe
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="productId"></param>
        /// <param name="servings"></param>
        /// <returns>Result</returns>
        Result InsertProductToRecipe(int recipeId, int productId, double servings);

        /*
         * Product
         */
        /// <summary>
        /// Get All Products Barcode and Name
        /// </summary>
        /// <returns>A List of ProductDtos</returns>
        List<ProductDto> GetAllProducts();

        /// <summary>
        /// Get a Product Information by its barcode
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns>ProductInformationDto</returns>
        ProductInformationDto GetProductById(int id);

        /// <summary>
        /// Get product information by its barcode and servings
        /// </summary>
        /// <param name="id"></param>
        /// <param name="servings"></param>
        /// <returns>ProductTotalInfoDto</returns>
        ProductTotalInfo GetProductByIdAndServings(int id, double servings);

        /// <summary>
        /// Create a New Product
        /// </summary>
        /// <param name="productInformationDto"></param>
        /// <returns>Result</returns>
        Result AddNewProduct(ProductInformationDto productInformationDto);

        /// <summary>
        /// Get all the unapproved products
        /// </summary>
        /// <returns>List<ProductDto></returns>
        List<ProductDto> GetUnapprovedProducts();

        
        /// <summary>
        /// Deleting a product from a patient
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="productId"></param>
        /// <returnsResult></returns>
        Result DeleteProduct(int productId);

        /// <summary>
        /// Edit product information
        /// </summary>
        /// <param name="productInformationDto"></param>
        /// <returns></returns>
        Result EditProduct(ProductInformationDto productInformationDto);

        /*
         * Administrator
         */

        /// <summary>
        /// It update IsAprrove record in Product Relation
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Result</returns>
        Result ApproveProduct(int id);

        /// <summary>
        /// Get payroll report for admin id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Payroll report info</returns>
        List<PayrollReport> GetPayrollReport(int id);

        /*
         * Plans
         */

        /// <summary>
        /// Create plans 
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        Result CreatePlan(PlanDto plan);

        /// <summary>
        /// Get Plan total info
        /// </summary>
        /// <param name="planId"></param>
        /// <returns>DailyConsumptionPlanDto</returns>
        DailyConsumptionPlanDto GetPlanById(int planId);

        /// <summary>
        /// Get plan by patient id and initial date
        /// </summary>
        /// <param name="patiendId"></param>
        /// <param name="initialDate"></param>
        /// <returns>DailyConsumptionPlanDto</returns>
        DailyConsumptionPlanDto GetPlanByPatientId(int patiendId, DateTime initialDate);

        /// <summary>
        /// Insert a new product to plan
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        Result InsertProductToPlan(int planId, ProductInPlanDto product);

        /// <summary>
        /// Insert a new recipe to plan
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="recipe"></param>
        /// <returns></returns>
        Result InsertRecipeToPlan(int planId, RecipeInPlanDto recipe);

        /// <summary>
        /// Edit product servings in a plan
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        Result EditProductInPlan(int planId, ProductInPlanDto product);

        /// <summary>
        /// Edit recipe servings in a plan
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="recipe"></param>
        /// <returns></returns>
        Result EditRecipeInPlan(int planId, RecipeInPlanDto recipe);

        /// <summary>
        /// Delete recipe in plan
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="recipe"></param>
        /// <returns></returns>
        Result DeleteRecipeInPlan(int planId, RecipeInPlanDto recipe);

        /// <summary>
        /// Delete product in plan
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        Result DeleteProductInPlan(int planId, ProductInPlanDto product);

        /// <summary>
        /// Deletes plan
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        Result DeletePlan(int planId);
    }

}
