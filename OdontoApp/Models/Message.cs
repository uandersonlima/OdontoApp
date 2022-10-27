using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class Message
    {
        [Key]
        public string Messagecode { get; set; }
        
        [InverseProperty("SenderUser")]
        public string SenderUserId { get; set; }
        public ApplicationUser SenderUser { get; set; }

        
        [InverseProperty("ReceiverUser")]
        public string ReceiverUserId { get; set; }
        public ApplicationUser ReceiverUser { get; set; }

        public string Description { get; set; }

        public DateTime? TimeSent { get; set; }
        public DateTime? TimeDelivered { get; set; }
        public DateTime? ViewedTime { get; set; }
        public bool IsDeleted { get; set; }


   
    }
}