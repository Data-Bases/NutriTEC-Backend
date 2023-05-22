using Nest;
using NutriTEc_Backend.Dtos;

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

        /*
         * Patient
         */

        /// <summary>
        /// Patient sign up
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>Result</returns>
        Result PatientSignUp(PatientDto patient);
    }
}
