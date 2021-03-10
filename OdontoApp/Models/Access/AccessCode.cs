using OdontoApp.Libraries.Lang;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models.Access
{
    public class AccessKey
    {
        [Key]
        public string Key { get; set; }
        public string KeyType { get; set; }
        public DateTime DataGerada { get; set; }
        
        
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
