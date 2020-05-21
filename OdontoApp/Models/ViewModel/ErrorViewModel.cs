using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models.ViewModel
{
    [NotMapped]
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}