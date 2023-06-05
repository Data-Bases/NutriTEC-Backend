using Nest;
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
        Result AdminSignUp(AdminDto admin);

        /*
         * Nutri
         */

        /// <summary>
        /// Nutri sign up
        /// </summary>
        /// <param name="nutri"></param>
        /// <returns>Result</returns>
        Result NutriSignUp(NutriDto nutri);

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

        /*
         * Patient
         */

        /// <summary>
        /// Patient sign up
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>Result</returns>
        Result PatientSignUp(PatientDto patient);

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
    }

}
