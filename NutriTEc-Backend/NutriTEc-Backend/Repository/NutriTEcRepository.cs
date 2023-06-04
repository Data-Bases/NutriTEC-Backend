using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Nest;
using NutriTEc_Backend.Dtos;
using NutriTEc_Backend.Helpers;
using NutriTEc_Backend.DataModel;
using NutriTEc_Backend.Repository.Interface;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Npgsql;
using System.Linq;
using NutriTEc_Backend.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using static Nest.JoinField;
using System.Collections;
using Elasticsearch.Net;
using System.ComponentModel.DataAnnotations;

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

        public Result ApproveProduct(int id)
        {
            try
            {
                var product = _context.Products.Where(x => x.Barcode == id).FirstOrDefault();
                if (product == null)
                {
                    return Result.NotFound;
                }

                product.Isapproved = true;

                _context.SaveChanges();

                return Result.Created;

            }
            catch (Exception ex)
            {
                return Result.Error;
            }
        }

        public List<PayrollReport> GetPayrollReport(int id)
        {
            try
            {
                var payroll = _context.PayrollReports.FromSqlRaw($"SELECT chargetype, nutriemail, fullname, cardnumber, totalamount, discount, chargeamount from payroll({id});").ToList();

                if (payroll == null)
                {
                    return new List<PayrollReport>();

                }

                return payroll;
            }
            catch (Exception ex)
            {
                return new List<PayrollReport>();
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
                Lastname2 = (string.IsNullOrEmpty(nutri.Lastname2)) ? "" : nutri.Lastname2,
                Age = nutri.Age,
                Birthdate = DateOnly.FromDateTime(nutri.Birthdate),
                Weight = (nutri.Weight == null ) ? 0 : nutri.Weight,
                Imc = (nutri.Imc == null) ? 0 : nutri.Imc,
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
                _context.Database.ExecuteSqlRaw($"CALL insert_nutri('{nutriToInsert.Email}', '{nutriToInsert.Password}', '{nutriToInsert.Name}', '{nutriToInsert.Lastname1}'," +
                                                                        $"'{nutriToInsert.Lastname2}', {nutriToInsert.Age}, '{nutriToInsert.Birthdate}', {nutriToInsert.Weight}, {nutriToInsert.Imc}," +
                                                                        $" {nutriToInsert.Nutritionistcode}, {nutriToInsert.Cardnumber}, '{nutriToInsert.Province}', '{nutriToInsert.Canton}', " +
                                                                        $"'{nutriToInsert.District}', '{nutriToInsert.Picture}', {nutriToInsert.Adminid}, {nutriToInsert.Chargetypeid});");

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

        public List<PlanIdDto> GetNutritionistPlans (int nutriId)
        {
            var plans = _context.Plans
                .Where(p => p.Nutriid == nutriId)
                .Select(p => new PlanIdDto
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList();

            return plans;
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
                Birthdate = DateOnly.FromDateTime(patient.Birthdate),
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

        public Result AddProductToPatient(PatientProductDto patientProductDto)
        {
            var newPatientProduct = new Patientproduct
            {
                Productbarcode = patientProductDto.ProductId,
                Patientid = patientProductDto.PatientId,
                Mealtime = patientProductDto.Mealtime,
                Consumedate = DateOnly.FromDateTime(patientProductDto.Consumedate)
            };

            try
            {
                _context.Patientproducts.Add(newPatientProduct);
                _context.SaveChanges();
                return Result.Created;
            }
            catch
            {
                return Result.Error;
            }
        }

        public Result AddRecipeToPatient(PatientRecipeDto patientRecipeDto)
        {
            var newPatientRecipe = new Patientrecipe
            {
                Recipeid = patientRecipeDto.Recipeid,
                Patientid = patientRecipeDto.Patientid,
                Mealtime = patientRecipeDto.Mealtime,
                Consumedate = DateOnly.FromDateTime(patientRecipeDto.Consumedate),
                Servings = patientRecipeDto.Servings,
            };

            try
            {
                _context.Patientrecipes.Add(newPatientRecipe);
                _context.SaveChanges();
                return Result.Created;
            }
            catch
            {
                return Result.Error;
            }
        }
        public Result AddPlanToPatient(PlanPatientDto planPatientDto)
        {
            try
            {
                _context.Database.ExecuteSqlRaw($"CALL insert_plan_patient({planPatientDto.PlanId}, {planPatientDto.PatientId}, '{planPatientDto.InitialDate.ToString("yyyy-MM-dd")}');");
                _context.SaveChanges();
                return Result.Created;
            }
            catch
            {
                return Result.Error;
            }
        }

        public DailyConsumptionDto GetDailyConsumptionByPatient(int patientId, DateTime dateConsumed)
        {
            try
            {
                var consumedRecipes = GetConsumedRecipesByPatient(patientId, dateConsumed);

                var consumedProducts = GetConsumedProductsByPatient(patientId, dateConsumed);

                var info = GetMealConsumptionClasify(consumedProducts, consumedRecipes);

                if (consumedRecipes.IsNullOrEmpty() && consumedProducts.IsNullOrEmpty())
                {
                    return new DailyConsumptionDto();
                }

                return new DailyConsumptionDto
                {
                    Date = dateConsumed,
                    TotalCalories = info.Item1,
                    Meals = info.Item2,
                };

            }
            catch (Exception ex)
            {
                return new DailyConsumptionDto();
            }
        }

        public List<ConsumedByPatient> GetConsumedRecipesByPatient(int patientId, DateTime dateConsumed)
        {
            try
            {
                var query = $"SELECT Id, Name, Servings, Energy, Mealtime FROM get_consumed_recipe({patientId}, '{DateOnly.FromDateTime(dateConsumed).ToString("yyyy-MM-dd")}');";
                var recipes = _context.ConsumedByPatient.FromSqlRaw(query).AsEnumerable().ToList();

                if (recipes.IsNullOrEmpty())
                {
                    return new List<ConsumedByPatient>();
                }

                return recipes;
            }
            catch (Exception e)
            {
                return new List<ConsumedByPatient>();
            }
        }

        public List<ConsumedByPatient> GetConsumedProductsByPatient(int patientId, DateTime dateConsumed)
        {
            try
            {
                var query = $"SELECT Id, Name, Servings, Energy, Mealtime FROM get_consumed_product({patientId}, '{DateOnly.FromDateTime(dateConsumed).ToString("yyyy-MM-dd")}');";
                var recipes = _context.ConsumedByPatient.FromSqlRaw(query).AsEnumerable().ToList();

                if (recipes.IsNullOrEmpty())
                {
                    return new List<ConsumedByPatient>();
                }

                return recipes;
            }
            catch (Exception e)
            {
                return new List<ConsumedByPatient>();
            }
        }

        public Result RegisterPatientMeasurements(int patientId, MeasurementDto measurementDto)
        {
            try
            {
                _context.Database.ExecuteSqlRaw($"CALL register_measurements({patientId}, {measurementDto.Height}, {measurementDto.Fatpercentage}, {measurementDto.Musclepercentage}, {measurementDto.Weight}, {measurementDto.Waist}, {measurementDto.Neck},{measurementDto.Hips} , '{DateOnly.FromDateTime(measurementDto.Revisiondate)}');");

                _context.SaveChanges();

                return Result.Created;
            }
            catch
            {
                return Result.Error;
            }
        }

        public NutriIdDto GetPatientsNutritionist(int patientId)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == patientId);
            var nutritionist = _context.Nutritionists.FirstOrDefault(n => n.Id == patient.Nutriid);

            var nutriIdDto = new NutriIdDto
            {
                Id = nutritionist.Id,
                Name = nutritionist.Name
            };

            return nutriIdDto;
        }

        public List<MeasurementFunc> GetPatientMeasurementsByDate(int patientId, DateTime startDate, DateTime finishDate)
        {
            try
            {
                var query = $"SELECT Height, FatPercentage, MusclePercentage, Weight, Waist, Neck, Hips, RevisionDate " +
                            $"FROM get_patient_measurements({patientId}, '{DateOnly.FromDateTime(startDate).ToString("yyyy-MM-dd")}', " +
                            $"'{DateOnly.FromDateTime(finishDate).ToString("yyyy-MM-dd")}');";
                var measurements = _context.MeasurementFunc.FromSqlRaw(query).AsEnumerable().ToList();

                if (measurements.IsNullOrEmpty())
                {
                    return new List<MeasurementFunc>();
                }

                return measurements;
            }
            catch (Exception e)
            {
                return new List<MeasurementFunc>();
            }
        }

        /*
         * Product 
         */
        public List<ProductDto> GetAllProducts()
        {
            var productsDto = _context.Products
                .Select(p => new ProductDto
                {
                    Id = p.Barcode,
                    Name = p.Name
                })
                .ToList();

            return productsDto;
        }

        public ProductInformationDto GetProductById(int id)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.Barcode == id);

            if (product == null)
            {
                return null;
            }

            var productInformationDto = new ProductInformationDto
            {
                Id = product.Barcode,
                Name = product.Name,
                Description = product.Descripcion,
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

        public ProductTotalInfo GetProductByIdAndServings(int id, double servings)
        {
            var product = _context.ProductTotalInfo.FromSqlRaw($"SELECT Name, PortionSize, Servings, Energy, Fat, Sodium, Carbs, Protein, Calcium, Iron FROM calculate_product_servings({id}, {servings})").AsEnumerable().FirstOrDefault();

            if (product == null)
            {
                return null;
            }

            return product;
        }

        public Result AddNewProduct(ProductInformationDto productInformationDto)
        {
            var newProduct = new Product
            {
                Barcode = productInformationDto.Id,
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
            }
            catch (Exception ex)
            {
                return Result.Error;
            }
        }

        public List<ProductDto> GetUnapprovedProducts()
        {
            var unapprovedProductsDto = _context.Products
                .Where(p => p.Isapproved == false)
                .Select(p => new ProductDto
                {
                    Id = p.Barcode,
                    Name = p.Name
                })
                .ToList();

            return unapprovedProductsDto;
        }


        /*
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
                
            }
            catch (Exception ex)
            {
                return Result.Error;
            }
        }   

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

        public RecipeInfoDto GetRecipeById(int id, int recipeServings)
        { 
            try
            {
                var recipeNutrients = _context.RecipeNutrients.FromSqlRaw($"SELECT totalenergy, totalsodium, totalcarbs, totalprotein, totalcalcium, totalfat, totaliron  FROM calculate_recipe_nutrients({id});").AsEnumerable().FirstOrDefault();

                var productsInRecipe = _context.ProductRecipeNutrients.FromSqlRaw($"SELECT recipename, recipeid, productname, portionsize, servings, energy, fat, sodium, carbs, protein, calcium, iron FROM products_in_recipe WHERE recipeid = {id};").ToList();

                return ParseTotalNutrients(recipeNutrients, productsInRecipe, recipeServings);
            }
            catch (Exception ex)
            {
                return new RecipeInfoDto();
            }
        }

        /*
         * Plans
         */
        
        public Result CreatePlan(PlanDto plan)
        {
            var productsToInsert = new List<Planproduct>();
            var recipeToInsert = new List<Planrecipe>();
            try
            {
                if (plan.Products.IsNullOrEmpty() && plan.Recipes.IsNullOrEmpty())
                {
                    return Result.Error;
                }

                var planId = _context.PlanIds.FromSqlRaw($"select create_plan from create_plan('{plan.PlanName}', {plan.NutriId})").FirstOrDefault();

                _context.SaveChanges();

                if (!plan.Products.IsNullOrEmpty())
                {
                    foreach (var product in plan.Products)
                    {
                        if (!Enum.IsDefined(typeof(DayOfWeek), product.ConsumeWeekDay) || !Enum.IsDefined(typeof(Mealtime), product.Mealtime))
                        {
                            return Result.Error;
                        }
                        productsToInsert.Add(new Planproduct
                        {
                            Productbarcode = product.ProductId,
                            Planid = planId.create_plan,
                            Consumeweekday = product.ConsumeWeekDay,
                            Mealtime = product.Mealtime,
                            Servings = product.Servings,
                        });
                    }

                    _context.Planproducts.AddRange(productsToInsert);
                 
                    _context.SaveChanges();
                }

                if (!plan.Recipes.IsNullOrEmpty())
                {
                    foreach (var recipe in plan.Recipes)
                    {
                        if (!Enum.IsDefined(typeof(DayOfWeek), recipe.ConsumeWeekDay) || !Enum.IsDefined(typeof(Mealtime), recipe.Mealtime))
                        {
                            return Result.Error;
                        }
                        recipeToInsert.Add(new Planrecipe
                        {
                            Recipeid = recipe.RecipeId,
                            Planid = planId.create_plan,
                            Consumeweekday = recipe.ConsumeWeekDay,
                            Mealtime = recipe.Mealtime,
                            Servings = recipe.Servings,
                        });
                    }

                    _context.Planrecipes.AddRange(recipeToInsert);

                    _context.SaveChanges();
                }


                return Result.Created;

            }
            catch (Exception ex)
            {
                return Result.Error;
            }
        }

        public DailyConsumptionPlanDto GetPlanById(int planId)
        {
            try
            {
                var planName = _context.Plans.Where(p => p.Id == planId).FirstOrDefault().Name;

                if (planName.IsNullOrEmpty())
                {
                    return new DailyConsumptionPlanDto();
                }

                var weekPlan = GetWeekPlan(planId);

                if (weekPlan.Item2.IsNullOrEmpty())
                {
                    return new DailyConsumptionPlanDto();
                }

                return new DailyConsumptionPlanDto
                {
                    PlanName = planName,
                    TotalCalories = weekPlan.Item1,
                    Meals = weekPlan.Item2,
                };

            }
            catch (Exception ex)
            {
                return new DailyConsumptionPlanDto();
            }
        }

        public DailyConsumptionPlanDto GetPlanByPatientId(int patiendId, DateTime initialDate)
        {
            try
            {
                var planPatient = _context.Planpatients.Where(p => p.Patientid == patiendId && p.Initialdate == DateOnly.FromDateTime(initialDate)).FirstOrDefault();

                if (planPatient == null)
                {
                    return new DailyConsumptionPlanDto();
                }

                var plan = _context.Plans.Where(p => p.Id == planPatient.Planid).FirstOrDefault();

                if (plan == null)
                {
                    return new DailyConsumptionPlanDto();
                }

                var planName = plan.Name;

                var weekPlan = GetWeekPlan(planPatient.Planid);

                if (weekPlan.Item2.IsNullOrEmpty())
                {
                    return new DailyConsumptionPlanDto();
                }

                return new DailyConsumptionPlanDto
                {
                    PlanName = planName,
                    TotalCalories = weekPlan.Item1,
                    Meals = weekPlan.Item2,
                };

            }
            catch (Exception ex)
            {
                return new DailyConsumptionPlanDto();
            }
        }

        /*
         * Private Methods
         */

        private RecipeInfoDto ParseTotalNutrients(RecipeNutrients recipe,  List<ProductRecipeNutrients> productRecipes, int recipeServings)
        {
            var productsInRecipeToReturn = new List<ProductTotalInfo>();

            foreach (var product in productRecipes)
            {
                var productToReturn = new ProductTotalInfo()
                {
                    Name = product.ProductName,
                    Portionsize = recipeServings * product.PortionSize,
                    Servings = recipeServings * product.Servings,
                    Energy = recipeServings * product.Energy,
                    Fat = recipeServings * product.Fat,
                    Sodium = recipeServings * product.Sodium,
                    Carbs = recipeServings * product.Carbs,
                    Protein = recipeServings * product.Protein,
                    Calcium = recipeServings * product.Calcium,
                    Iron = recipeServings * product.Iron,
                };

                productsInRecipeToReturn.Add(productToReturn);
                    
            }

            var recipeToReturn = new RecipeInfoDto
            {
                RecipeName = productRecipes[0].RecipeName,
                Energy = recipeServings * recipe.Totalenergy,
                Fat = recipeServings * recipe.Totalfat,
                Sodium = recipeServings * recipe.Totalsodium,
                Carbs = recipeServings * recipe.Totalcarbs,
                Calcium = recipeServings * recipe.Totalcalcium,
                Iron = recipeServings * recipe.Totaliron,
                Protein = recipeServings * recipe.Totalprotein,
                Products = productsInRecipeToReturn,
            };
            return recipeToReturn;
        }

        private (double, List<MealtimeDto>) GetMealConsumptionClasify(List<ConsumedByPatient> products, List<ConsumedByPatient> recipes)
        {
            double totalCalores = 0;
            double totalCaloresBreakfast = 0;
            double totalCaloresLunch = 0;
            double totalCaloresDinner = 0;
            double totalCaloresSnack = 0;

            var consumedProductInLunch = new List<ConsumedByPatientDto>();
            var consumedProductInBreakfast = new List<ConsumedByPatientDto>();
            var consumedProductInDinner = new List<ConsumedByPatientDto>();
            var consumedProductInSnack = new List<ConsumedByPatientDto>();
            var consumedRecipeInLunch = new List<ConsumedByPatientDto>();
            var consumedRecipeInBreakfast = new List<ConsumedByPatientDto>();
            var consumedRecipeInDinner = new List<ConsumedByPatientDto>();
            var consumedRecipeInSnack = new List<ConsumedByPatientDto>();

            foreach (var consumed in products)
            {
                if (consumed.Mealtime == Mealtime.Breakfast.GetStringValue())
                {
                    consumedProductInBreakfast.Add(new ConsumedByPatientDto
                    {
                        Id = consumed.Id,
                        Energy = consumed.Energy,
                        Name = consumed.Name,
                        Servings = consumed.Servings,
                    });
                    totalCaloresBreakfast += consumed.Energy;
                }
                else if (consumed.Mealtime == Mealtime.Lunch.GetStringValue())
                {
                    consumedProductInLunch.Add(new ConsumedByPatientDto
                    {
                        Id = consumed.Id,
                        Energy = consumed.Energy,
                        Name = consumed.Name,
                        Servings = consumed.Servings,
                    });
                    totalCaloresLunch += consumed.Energy;
                }
                else if (consumed.Mealtime == Mealtime.Dinner.GetStringValue())
                {
                    consumedProductInDinner.Add(new ConsumedByPatientDto
                    {
                        Id = consumed.Id,
                        Energy = consumed.Energy,
                        Name = consumed.Name,
                        Servings = consumed.Servings,
                    });
                    totalCaloresDinner += consumed.Energy;
                }
                else if (consumed.Mealtime == Mealtime.Snack.GetStringValue())
                {
                    consumedProductInSnack.Add(new ConsumedByPatientDto
                    {
                        Id = consumed.Id,
                        Energy = consumed.Energy,
                        Name = consumed.Name,
                        Servings = consumed.Servings,
                    });
                    totalCaloresSnack += consumed.Energy;
                }
                
                totalCalores += consumed.Energy;
            }

            foreach (var consumed in recipes)
            {
                if (consumed.Mealtime == Mealtime.Breakfast.GetStringValue())
                {
                    consumedRecipeInBreakfast.Add(new ConsumedByPatientDto
                    {
                        Id = consumed.Id,
                        Energy = consumed.Energy,
                        Name = consumed.Name,
                        Servings = consumed.Servings,
                    });
                    totalCaloresBreakfast += consumed.Energy;
                }
                else if (consumed.Mealtime == Mealtime.Lunch.GetStringValue())
                {
                    consumedRecipeInLunch.Add(new ConsumedByPatientDto
                    {
                        Id = consumed.Id,
                        Energy = consumed.Energy,
                        Name = consumed.Name,
                        Servings = consumed.Servings,
                    });
                    totalCaloresLunch += consumed.Energy;
                }
                else if (consumed.Mealtime == Mealtime.Dinner.GetStringValue())
                {
                    consumedRecipeInDinner.Add(new ConsumedByPatientDto
                    {
                        Id = consumed.Id,
                        Energy = consumed.Energy,
                        Name = consumed.Name,
                        Servings = consumed.Servings,
                    });
                    totalCaloresDinner += consumed.Energy;
                }
                else if (consumed.Mealtime == Mealtime.Snack.GetStringValue())
                {
                    consumedRecipeInSnack.Add(new ConsumedByPatientDto
                    {
                        Id = consumed.Id,
                        Energy = consumed.Energy,
                        Name = consumed.Name,
                        Servings = consumed.Servings,
                    });
                    totalCaloresSnack += consumed.Energy;
                }

                totalCalores += consumed.Energy;
            }

            var breakfast = new MealtimeDto()
            {
                Mealtime = Mealtime.Breakfast.GetStringValue(),
                Calories = totalCaloresBreakfast,
                Products = consumedProductInBreakfast,
                Recipes = consumedRecipeInBreakfast
            };
            var lunch = new MealtimeDto()
            {
                Mealtime = Mealtime.Lunch.GetStringValue(),
                Calories = totalCaloresLunch,
                Products = consumedProductInLunch,
                Recipes = consumedRecipeInLunch
            };
            var dinner = new MealtimeDto()
            {
                Mealtime = Mealtime.Dinner.GetStringValue(),
                Calories = totalCaloresDinner,
                Products = consumedProductInDinner,
                Recipes = consumedRecipeInDinner
            };
            var snack = new MealtimeDto()
            {
                Mealtime = Mealtime.Snack.GetStringValue(),
                Calories = totalCaloresSnack,
                Products = consumedProductInSnack,
                Recipes = consumedRecipeInSnack
            };

            return (totalCalores, new List<MealtimeDto>
            {
                breakfast, lunch, dinner, snack
            });
        }

        private (double, List<MealsPerDayDto>) GetWeekPlan(int planId)
        {
            double totalCalories = 0;

            var mealsPerDay = new List<MealsPerDayDto>();

            (double, List<MealtimeDto>) info;

            foreach (var day in Enum.GetValues(typeof(DayOfTheWeek)).Cast<DayOfTheWeek>())
            {
                info = GetWeekDayPlan(planId, day);
                var meals = info.Item2;
                mealsPerDay.Add(new MealsPerDayDto
                {
                    Calories = info.Item1,
                    DayOfTheWeek = day.GetStringValue(),
                    mealtimes = info.Item2,
                });

                totalCalories += info.Item1;
            };

            return (totalCalories, mealsPerDay);
        }

        private (double, List<MealtimeDto>) GetWeekDayPlan(int planId, DayOfTheWeek dayOfTheWeek)
        {
            var recipes = GetRecipesInPlanPerWeekday(planId, dayOfTheWeek);

            var products = GetProductsInPlanPerWeekDay(planId, dayOfTheWeek);

            var info = GetMealConsumptionClasify(products, recipes);

            return (info.Item1, info.Item2);
        }

        private List<ConsumedByPatient> GetRecipesInPlanPerWeekday(int planId, DayOfTheWeek dayOfTheWeek)
        {
            try
            {
                var query = $"SELECT Id, Name, Servings, Energy, Mealtime FROM get_recipe_plan({planId}, '{dayOfTheWeek.GetStringValue()}');";
                var recipes = _context.ConsumedByPatient.FromSqlRaw(query).AsEnumerable().ToList();

                if (recipes.IsNullOrEmpty())
                {
                    return new List<ConsumedByPatient>();
                }

                return recipes;
            }
            catch (Exception e)
            {
                return new List<ConsumedByPatient>();
            }
        }

        private List<ConsumedByPatient> GetProductsInPlanPerWeekDay(int planId, DayOfTheWeek dayOfTheWeek)
        {
            try
            {
                var query = $"SELECT Id, Name, Servings, Energy, Mealtime FROM get_product_plan({planId}, '{dayOfTheWeek.GetStringValue()}');";
                var recipes = _context.ConsumedByPatient.FromSqlRaw(query).AsEnumerable().ToList();

                if (recipes.IsNullOrEmpty())
                {
                    return new List<ConsumedByPatient>();
                }

                return recipes;
            }
            catch (Exception e)
            {
                return new List<ConsumedByPatient>();
            }
        }
    }
}
