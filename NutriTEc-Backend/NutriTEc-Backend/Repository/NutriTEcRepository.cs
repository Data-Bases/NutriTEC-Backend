using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Nest;
using NutriTEc_Backend.Dtos;
using NutriTEc_Backend.Helpers;
using NutriTEc_Backend.Repository.DataModel;
using NutriTEc_Backend.Repository.Interface;
using System.Collections.Generic;

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
    }
}
