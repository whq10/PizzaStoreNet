using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizaStore.Models
{
    public class GroupEmail
    {
        public int ID { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Text { get; set; }
        public PostCodes PostCode { get; set; }
        public DateTime Birthdate { get; set; }
        public SendingFrequency EmailSendingFrequency { get; set; }
        public int Points { get; set; }
    }

    public enum PostCodes
    {
        R3T2P6,
        R3T5X1
    }

    public enum SendingFrequency
    {
        Weekly,
        Monthly,
        Yearly
    }

}