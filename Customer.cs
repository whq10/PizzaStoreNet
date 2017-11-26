using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizaStore.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public string Pin { get; set; }
        public string CardNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public PostCodes PostCode { get; set; }
        public int PointsAvailable { get; set; }
        public int PointsCurrentMonth { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Status { get; set; }
        public virtual ICollection<RedeemableItem> RedeemableItems { get; set; }
    }
}