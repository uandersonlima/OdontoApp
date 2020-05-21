using System.ComponentModel.DataAnnotations;


namespace OdontoApp.Models.APICartao
{
    public class Address
    {
        [Required(ErrorMessage = "Informe seu Bairro ", AllowEmptyStrings = false)]
        [RegularExpression(@"[A-Za-z]{1,300}$", ErrorMessage = "Informe somente letras")]
        public string neighborhood { get; set; }
        public string street { get; set; }
        [RegularExpression(@"[0-9]{1,4}$", ErrorMessage = "Informe somente numeros")]
        [Required(ErrorMessage = "Informe o número da casa  ", AllowEmptyStrings = false)]
        [StringLength(4)]
        public string street_number { get; set; }
        [StringLength(8)]
        [Required(ErrorMessage = "Informe o CEP de sua cidade ", AllowEmptyStrings = false)]
        [RegularExpression(@"[0-9]{1,8}$", ErrorMessage = "Informe somente numeros")]
        public string zipcode { get; set; }
    }
}
