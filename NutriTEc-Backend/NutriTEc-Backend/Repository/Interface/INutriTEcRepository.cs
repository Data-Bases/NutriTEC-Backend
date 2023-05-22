using NutriTEc_Backend.Dtos;

namespace NutriTEc_Backend.Repository.Interface
{
    public interface INutriTEcRepository
    {
        List<VitaminDto> GetAllVitamins();

        UserCredentialsDto GetUserByEmail(string email);
    }
}
