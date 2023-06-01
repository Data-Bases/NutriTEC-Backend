using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Nest;
using NutriTEc_Backend.Dtos;
using NutriTEc_Backend.Helpers;
using NutriTEc_Backend.DataModel;
using NutriTEc_Backend.Repository.Interface;
using System.Collections.Generic;
<<<<<<< HEAD
using Npgsql;
using System;
=======
using Microsoft.Data.SqlClient;
using Npgsql;
using System.Linq;
using NutriTEc_Backend.Models;
using Microsoft.IdentityModel.Tokens;
>>>>>>> f62ae96398460b5c584351073b6fdd8c8edd2f77

namespace NutriTEc_Backend.Repository
{
    public class NutriTEcRepository : INutriTEcRepository
    {
        private readonly NutriTecContext _context;
        private readonly IMapper _mapper;

        public NutriTEcRepository(NutriTecContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public List<VitaminDto> GetAllVitamins()
        {

            var vitaminsDto = new List<VitaminDto>();
            var vitamin = _context.Vitamins.FromSqlRaw($"Select * from Vitamin").ToList();

            vitamin.ForEach(v => vitaminsDto.Add(new VitaminDto
            {
                Id = v.Id,
                Name = v.Name,
            }
            ));

            return vitaminsDto;
        }

        /*
         * Credentials
         */

        public UserCredentialsDto GetUserByEmail(string email)
        {

            var user = _context.Usercredentials.FirstOrDefault(p => p.Email == email);

            if (user == null)
            {
                return new UserCredentialsDto();
            }

            var userName = string.Empty;

            if ( user.Usertype.Equals("P"))
            {
                userName = _context.Patients.FirstOrDefault(p => p.Id == user.Id).Name;

            }else if (user.Usertype.Equals("N"))
            {
                userName = _context.Nutritionists.FirstOrDefault(p => p.Id == user.Id).Name;
            }


            var userDto = new UserCredentialsDto()
            {
                Id = user.Id,
                Email = email,
                Name = userName,
                Password = user.Password,
                UserType = user.Usertype,
            };

            return userDto;
        }

        /*
         * Admin
         */

        public Result AdminSignUp (AdminDto admin)
        {
            admin.Password = PassowordHelper.EncodePasswordMD5(admin.Password).ToLower();

            var adminUserToInsert = new Administrator()
            {
                Email = admin.Email,
                Password = admin.Password,
            };

            try
            {
                _context.Administrators.Add(adminUserToInsert);

                _context.SaveChanges();
                return Result.Created;


            }catch (Exception ex)
            {
                return Result.Error;
            }
        }

        /*
         * Nutri
         */

        public Result NutriSignUp (NutriDto nutri)
        {
            nutri.Password = PassowordHelper.EncodePasswordMD5(nutri.Password).ToLower();

            var nutriToInsert = new Nutritionist()
            {
                Email = nutri.Email,
                Password = nutri.Password,
                Name = nutri.Name,
                Lastname1 = nutri.Lastname1,
                Lastname2 = (string.IsNullOrEmpty(nutri.Lastname2)) ? null : nutri.Lastname2,
                Age = nutri.Age,
                Birthdate = nutri.Birthdate,
                Weight = (nutri.Weight == null ) ? null : nutri.Weight,
                Imc = (nutri.Imc == null) ? null : nutri.Imc,
                Nutritionistcode = nutri.Nutritionistcode,
                Cardnumber = (nutri.Cardnumber == null) ? 0 : nutri.Cardnumber,
                Province = nutri.Province,
                Canton = nutri.Canton,
                District = nutri.District,
                Picture = nutri.Picture,
                Adminid = nutri.Adminid,
                Chargetypeid = nutri.Chargetypeid,
            };

            try
            {
                _context.Nutritionists.Add(nutriToInsert);

                _context.SaveChanges();
                return Result.Created;


            }
            catch (Exception ex)
            {
                return Result.Error;
            }
        }

        public List<PatientIdDto> GetPatientsByNutriId(int nutriId)
        {
            var patientsDto = _context.Patients
                .Where(p => p.Nutriid == nutriId)
                .Select(p => new PatientIdDto
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();

            return patientsDto;
        }

        /*
         * Patient
         */

        public Result PatientSignUp (PatientDto patient)
        {
            patient.Password = PassowordHelper.EncodePasswordMD5(patient.Password).ToLower();

            var patientToInsert = new Patient()
            {
                Name = patient.Name,
                Email = patient.Email,
                Lastname1 = patient.Lastname1,
                Lastname2 = patient.Lastname2,
                Age = patient.Age,
                Birthdate = patient.Birthdate,
                Password = patient.Password,
                Country = patient.Country,
                Caloriesintake = (patient.Caloriesintake == null) ? null : patient.Caloriesintake, 
                Nutriid = (patient.Nutriid == null) ? null : patient.Nutriid,
            };

            try
            {
                _context.Patients.Add(patientToInsert);

                _context.SaveChanges();
                return Result.Created;


            }
            catch (Exception ex)
            {
                return Result.Error;
            }
        }

        /*
<<<<<<< HEAD
         * Product 
         */
        public List<ProductDto> GetAllProducts()
        {
            var productsDto = _context.Products
                .Select(p => new ProductDto
                {
                    Barcode = p.Barcode,
                    Name = p.Name
                })
                .ToList();

            return productsDto;
        }

        public ProductInformationDto GetProductByBarcode(int barcode)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.Barcode == barcode);

            if (product == null)
            {
                return null;
            }

            var productInformationDto = new ProductInformationDto
            {
                Barcode = product.Barcode,
                Name = product.Name,
                Description = product.Descripcion, // creo que en la DB está mal escrito
                PortionSize = product.Portionsize,
                Energy = product.Energy,
                Fat = product.Fat,
                Sodium = product.Sodium,
                Carbs = product.Carbs,
                Protein = product.Protein,
                Calcium = product.Calcium,
                Iron = product.Iron,
                IsApproved = (product.Isapproved == null) ? null : product.Isapproved
            };

            return productInformationDto;
        }

        public Result AddNewProduct(ProductInformationDto productInformationDto)
        {
            var newProduct = new Product
            {
                Barcode = productInformationDto.Barcode,
                Name = productInformationDto.Name,
                Descripcion = productInformationDto.Description,
                Portionsize = productInformationDto.PortionSize,
                Energy = productInformationDto.Energy,
                Fat = productInformationDto.Fat,
                Sodium = productInformationDto.Sodium,
                Carbs = productInformationDto.Carbs,
                Protein = productInformationDto.Protein,
                Calcium = productInformationDto.Calcium,
                Iron = productInformationDto.Iron,
                Isapproved = false
            };

            try
            {
                _context.Products.Add(newProduct);
                _context.SaveChanges();
                return Result.Created;
=======
         * Recipe
         */

        public Result CreateRecipe(RecipeXProductsDto recipe)
        {
            var productsToInsert = new List<Productrecipe>();
            try
            {
                if (recipe.Products.IsNullOrEmpty())
                {
                    return Result.Error;
                }

                var recipeId = _context.RecipeIds.FromSqlRaw($"SELECT * FROM  create_recipe('{recipe.RecipeName}')").FirstOrDefault();

                _context.SaveChanges();

                foreach (var product in recipe.Products)
                {
                    productsToInsert.Add(new Productrecipe
                    {
                        Productbarcode = product.Id,
                        Recipeid = recipeId.create_recipe,
                        Servings = product.Servings,
                    });
                }

                _context.Productrecipes.AddRange(productsToInsert);

                _context.SaveChanges();

                return Result.Created;

>>>>>>> f62ae96398460b5c584351073b6fdd8c8edd2f77
            }
            catch (Exception ex)
            {
                return Result.Error;
            }
        }

<<<<<<< HEAD
        public List<ProductDto> GetUnapprovedProducts()
        {
            var unapprovedProductsDto = _context.Products
                .Where(p => p.Isapproved == false)
                .Select(p => new ProductDto
                {
                    Barcode = p.Barcode,
                    Name = p.Name
                })
                .ToList();

            return unapprovedProductsDto;
        }
=======
        public List<RecipeDto> GetRecipes()
        {
            var recipes = new List<RecipeDto>();
            try
            {
                var recipesFromDb = _context.Recipes.ToList();
                recipesFromDb.ForEach(x => recipes.Add(
                    new RecipeDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }) );
                return recipes;
            }
            catch (Exception ex)
            {
                return new List<RecipeDto>();
            }
        }

        public RecipeInfoDto GetRecipeById(int id)
        { 
            try
            {
                var recipeNutrients = _context.RecipeNutrients.FromSqlRaw($"SELECT totalenergy, totalsodium, totalcarbs, totalprotein, totalcalcium, totalfat, totaliron  FROM calculate_recipe_nutrients({id});").AsEnumerable().FirstOrDefault();

                var productsInRecipe = _context.ProductRecipeNutrients.FromSqlRaw($"SELECT recipename, recipeid, productname, portionsize, servings, energy, fat, sodium, carbs, protein, calcium, iron FROM products_in_recipe WHERE recipeid = {id};").ToList();

                return ParseTotalNutrients(recipeNutrients, productsInRecipe);
            }
            catch (Exception ex)
            {
                return new RecipeInfoDto();
            }
        }

        private RecipeInfoDto ParseTotalNutrients(RecipeNutrients recipe,  List<ProductRecipeNutrients> productRecipes)
        {
            var productsInRecipeToReturn = new List<ProductTotalInfoDto>();

            foreach (var product in productRecipes)
            {
                var productToReturn = new ProductTotalInfoDto()
                {
                    Name = product.ProductName,
                    Portionsize = product.PortionSize,
                    Servings = product.Servings,
                    Energy = product.Energy,
                    Fat = product.Fat,
                    Sodium = product.Sodium,
                    Carbs = product.Carbs,
                    Protein = product.Protein,
                    Calcium = product.Calcium,
                    Iron = product.Iron,
                };

                productsInRecipeToReturn.Add(productToReturn);
                    
            }

            var recipeToReturn = new RecipeInfoDto
            {
                RecipeName = productRecipes[0].RecipeName,
                Energy = recipe.Totalenergy,
                Fat = recipe.Totalfat,
                Sodium = recipe.Totalsodium,
                Carbs = recipe.Totalcarbs,
                Calcium = recipe.Totalcalcium,
                Iron = recipe.Totaliron,
                Protein = recipe.Totalprotein,
                Products = productsInRecipeToReturn,
            };
            return recipeToReturn;
        }

>>>>>>> f62ae96398460b5c584351073b6fdd8c8edd2f77
    }

}
