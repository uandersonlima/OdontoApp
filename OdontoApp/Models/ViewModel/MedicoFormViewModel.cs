using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoApp.Models.ViewModel
{
    [NotMapped]
    public class MedicoFormViewModel
    {
        public Medico medico { get; set; }
      
      
    }
}
