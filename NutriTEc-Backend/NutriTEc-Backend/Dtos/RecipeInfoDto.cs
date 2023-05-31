﻿namespace NutriTEc_Backend.Dtos
{
    public class RecipeInfoDto
    {
        public string? RecipeName { get; set; }

        public double Energy { get; set; }

        public double Fat { get; set; }

        public double Sodium { get; set; }

        public double Carbs { get; set; }

        public double Protein { get; set; }

        public double Calcium { get; set; }

        public double Iron { get; set; }
        public List<ProductTotalInfoDto> Products { get; set;}
    }
}
