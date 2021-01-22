using OdontoApp.Models.AccessCode;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models.ViewModel
{
    [NotMapped]
    public class UserCode
    {
        public ApplicationUser Usuario { get; set; }
        public AccessCode.AccessCode AcessCode { get; set; }
    }
}
