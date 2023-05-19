using NutriTEc_Backend.Dtos;
using NutriTEc_Backend.Models;

namespace NutriTEc_Backend.Repository.Interface
{
    public interface INutriTEcRepository
    {
        List<VitaminDto> GetAllVitamins();
    }
}
