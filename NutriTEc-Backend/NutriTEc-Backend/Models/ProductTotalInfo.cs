using NutriTEc_Backend.DataModel;

namespace NutriTEc_Backend.Models
{
    public class ProductTotalInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double Portionsize { get; set; }

        public double Servings { get; set; }

        public double Energy { get; set; }

        public double Fat { get; set; }

        public double Sodium { get; set; }

        public double Carbs { get; set; }

        public double Protein { get; set; }

        public double Calcium { get; set; }

        public double Iron { get; set; }

    }
}
