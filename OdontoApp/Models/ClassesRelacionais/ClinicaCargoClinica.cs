
namespace OdontoApp.Models
{
    public class ClinicaCargoClinica
    {
        public int ClinicaId { get; set; }
        public Clinica Clinica { get; set; }
        public int CargoClinicaId { get; set; }
        public CargoClinica CargoClinica { get; set; }
    }
}
