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

        /*
         * Patient
         */

        /// <summary>
        /// Patient sign up
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>Result</returns>
        Result PatientSignUp(PatientDto patient);

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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RecipeInfoDto GetRecipeById(int id);

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
    }
}
