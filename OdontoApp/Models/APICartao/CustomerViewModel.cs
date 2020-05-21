using System.ComponentModel.DataAnnotations;


namespace OdontoApp.Models.APICartao
{
    public class CustomerViewModel
    {
        [Required(ErrorMessage = "insira somente números ", AllowEmptyStrings = false)]
        [RegularExpression(@"[0-9]{1,300}$", ErrorMessage = "Informe somente números")]
        public string document_number { get; set; }
        [Required(ErrorMessage = "Email deve ser informado", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email no formato invalido")]
        public string email { get; set; }
        [Required(ErrorMessage = "Informe seu Nome ", AllowEmptyStrings = false)]
        [RegularExpression(@"[A-Za-z]{1,300}$", ErrorMessage = "Informe somente números")]
        public string name { get; set; }
        [Required(ErrorMessage = "Informe seu DD ", AllowEmptyStrings = false)]
        [RegularExpression(@"[0-9]{1,2}$", ErrorMessage = "Informe somente números")]
        [StringLength(2)]
        public string ddd { get; set; }
        [RegularExpression(@"[0-9]{1,9}$", ErrorMessage = "Informe somente números")]
        [StringLength(9)]
        public string number { get; set; }
    }
}
