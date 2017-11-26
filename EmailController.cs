using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Mvc.Mailer;
//using PizaStore.Mailers;
using System.Net.Mail;
//using RTE;
using System.Web.UI.WebControls;
using PizaStore.DAL;
using PizaStore.Models;

namespace PizaStore.Controllers
{
    public class EmailController : Controller
    {

        ////this is a custom function that create and initialize the editor
        //protected Editor PrepairEditor(Action<Editor> oninit)
        //{
        //    Editor editor = new Editor(System.Web.HttpContext.Current, "editor");

        //    editor.ClientFolder = "/richtexteditor/";
        //    editor.ContentCss = "/Content/example.css";
        //    //editor.ClientFolder = "/Content/richtexteditor/";
        //    //editor.ClientFolder = "/Scripts/richtexteditor/";

        //    editor.Text = "Type here";

        //    editor.AjaxPostbackUrl = Url.Action("EditorAjaxHandler");

        //    if (oninit != null) oninit(editor);

        //    //try to handle the upload/ajax requests
        //    bool isajax = editor.MvcInit();

        //    if (isajax)
        //        return editor;

        //    //load the form data if any
        //    if (this.Request.HttpMethod == "POST")
        //    {
        //        string formdata = this.Request.Form[editor.Name];
        //        if (formdata != null)
        //            editor.LoadFormData(formdata);
        //    }

        //    //render the editor to ViewBag.Editor
        //    ViewBag.Editor = editor.MvcGetString();

        //    return editor;
        //}

        //private IUserMailer _userMailer = new UserMailer();
        //public IUserMailer UserMailer
        //{
        //    get { return _userMailer; }
        //    set { _userMailer = value; }
        //}


        //// GET: /Email/

        //public ActionResult Index()
        //{
        //    return View();


        //}

        //[HttpPost]
        //public ActionResult Index(PizaStore.Models.GroupEmail email)
        //{
        //    if (email != null)
        //    {
        //        UserMailer.Welcome(email).Send();
        //        return View();
        //    }
        //    else
        //    {
        //        return View();
        //    }



        //}


        public ActionResult Index()
        {
            PizaStore.Models.GroupEmail groupEmail = new PizaStore.Models.GroupEmail();
            groupEmail.Text = "this is a test for index page.";

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public string Index(PizaStore.Models.GroupEmail modelMail)
        {
            PizaStore.DAL.SchoolContext db = new PizaStore.DAL.SchoolContext();


            MailMessage mail = new MailMessage();
            string Body = modelMail.Text;
            mail.Body = Body;
            foreach (Customer c in db.Customers)
            {
                if ((c.PostCode == modelMail.PostCode) || (c.Birthdate.Date == modelMail.Birthdate.Date && c.Birthdate.Month == modelMail.Birthdate.Month))
                //if (c.Birthdate.Date.Day == modelMail.Birthdate.Day && c.Birthdate.Date.Month == modelMail.Birthdate.Month)
                {
                    mail.To.Add(c.Email);
                    mail.Body += "Available Points:";
                    mail.Body += c.PointsAvailable.ToString();
                    mail.Body += "Redeemable Items:";
                    //foreach (RedeemableItem i in c.RedeemableItems)
                    //{
                    //    mail.Body += i.Name;
                    //    mail.Body += " : ";
                    //    mail.Body += i.Price;
                    //}

                }

            }


            mail.From = new MailAddress("whq10@sina.com");
            //mail.Subject = modelMail.Subject;


            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.sina.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            ("whq10@sina.com", "ywfwyfywxwyx_1o");// Enter seders User name and password
            smtp.EnableSsl = false;
            smtp.Send(mail);
            return "Email sent successfully.";


        }


        //// GET: /SendMailer/  
        //public ActionResult Welcome()
        //{
        //    Editor Editor3 = PrepairEditor(delegate(Editor editor)
        //    {
        //        editor.ID = editor.Name = "Editor3";
        //        editor.Height = Unit.Pixel(200);
        //        editor.Text = "Type here";
        //        editor.Skin = "office2003silver2";
        //        editor.Toolbar = "forum";
        //    });
        //    ViewBag.Editor3 = Editor3.MvcGetString();
        //    return View();
        //}

        // GET: /AutoMail/  
        public ActionResult AutoMail()
        {
            PizaStore.Models.MailNotifyInfo mailNotifyInfo = new PizaStore.Models.MailNotifyInfo();
            mailNotifyInfo.EmailAddress = "ytt10ytt10@gmail.com";
            mailNotifyInfo.Subject = "test subject";
            mailNotifyInfo.TextBody = "this is a test";

           NotificationHandler.Instance.AppendNotification(mailNotifyInfo);
           return View();
        }




    }
}
