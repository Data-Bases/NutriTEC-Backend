namespace NutriTEc_Backend.Dtos
{
    public class PlanPatientDto
    {
        public int PlanId { get; set; }
        public int PatientId { get; set;}
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}

