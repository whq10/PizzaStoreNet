using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizaStore.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public bool ActivationState { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipEndDate { get; set; }
        public string Tel { get; set; }
        public string StoreNum { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        //public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}