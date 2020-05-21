using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models.ViewModel
{
    [NotMapped]
    public class ClienteCodigo
    {
        public Usuario Cliente { get; set; }
        public CodigoAcesso.CodigoAcesso CodigoAcesso { get; set; }
    }
}
