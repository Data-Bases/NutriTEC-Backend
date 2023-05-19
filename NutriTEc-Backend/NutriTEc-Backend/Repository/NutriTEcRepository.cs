using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NutriTEc_Backend.Dtos;
using NutriTEc_Backend.Models;
using NutriTEc_Backend.Repository.Interface;
using System.Collections.Generic;

namespace NutriTEc_Backend.Repository
{
    public class NutriTEcRepository : INutriTEcRepository
    {
        private readonly NutriTEcDataContext _context;

        public NutriTEcRepository(NutriTEcDataContext context)
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
    }
}
