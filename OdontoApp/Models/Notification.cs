using System;
using System.ComponentModel.DataAnnotations;

namespace OdontoApp.Models
{
    public class Notification
    {
        [Key]
        public Guid NotificationCode { get; set; } = Guid.NewGuid();
    }
}