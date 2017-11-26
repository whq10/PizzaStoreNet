using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizaStore.Models
{
    public class MailNotifyInfo
    {
        public string From
        {
            get;
            set;
        }

        public string To
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Subject
        {
            get;
            set;
        }

        public string EmailAddress
        {
            get;
            set;
        }

        public string TextBody
        {
            get;
            set;
        }
    }
}