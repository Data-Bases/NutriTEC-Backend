using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NutriTEc_Backend.Dtos;
using NutriTEc_Backend.Repository.DataModel;
using NutriTEc_Backend.Repository.Interface;
using System.Collections.Generic;

namespace NutriTEc_Backend.Repository
{
    public class NutriTEcRepository : INutriTEcRepository
    {
        private readonly NutriTecContext _context;

        public NutriTEcRepository(NutriTecContext context)
        {
            _context = context;
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
    }
}
