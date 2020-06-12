using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test2.Models;

namespace test2.Controllers
{
    public class HomeController : Controller
    {

        string fromEmail = "tmoore@stjweb.org";
        string host = "smtp.gmail.com";
        int port = 587;
        string pw = "69755Stj!";


        public ActionResult Index()
        {

            ViewBag.Rapper = HttpContext.Request.QueryString["rapper"]; ;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.Rapper = HttpContext.Request.QueryString["rapper"]; ;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Rapper = HttpContext.Request.QueryString["rapper"]; ;

            ViewBag.TimerStart = 60;


            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(string txtName, string txtEmail, string txtPhone, string txtAddress, string txtTag, string txtFax, string txtMessage)
        {
            System.Xml.Linq.XDocument xdocFeedXML = System.Xml.Linq.XDocument.Load(HttpContext.Server.MapPath(@"../App_Data\Contacts.xml"));

            txtName = System.Web.HttpUtility.HtmlEncode((txtName ?? "").Trim());
            txtEmail = System.Web.HttpUtility.HtmlEncode((txtEmail ?? "").Trim());
            txtPhone = System.Web.HttpUtility.HtmlEncode((txtPhone ?? "").Trim());
            txtAddress = System.Web.HttpUtility.HtmlEncode((txtAddress ?? "").Trim());
            txtTag = System.Web.HttpUtility.HtmlEncode((txtTag ?? "").Trim());
            txtFax = System.Web.HttpUtility.HtmlEncode((txtFax ?? "").Trim());
            txtMessage = System.Web.HttpUtility.HtmlEncode((txtMessage ?? "").Trim());

            xdocFeedXML.Root.Add(new System.Xml.Linq.XElement("contact",
                new System.Xml.Linq.XAttribute("name", txtName),
                new System.Xml.Linq.XAttribute("email", txtEmail),
                new System.Xml.Linq.XAttribute("phone", txtPhone),
                new System.Xml.Linq.XAttribute("address", txtAddress),
                new System.Xml.Linq.XAttribute("tag", txtTag),
                new System.Xml.Linq.XAttribute("fax", txtFax),
                new System.Xml.Linq.XAttribute("message", txtMessage)
            ));

            xdocFeedXML.Save(HttpContext.Server.MapPath(@"../App_Data\Contacts.xml"));

            string[] emails = { "todd1.terry1@gmail.com", "timothydmoore@yahoo.com", "arusse9393@gmail.com" };

            for (int x = 0; x < emails.Length; x++) 
            {
                using (System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage(fromEmail,
                    emails[x], "You've got Mail!!", txtMessage))
                {
                    mm.IsBodyHtml = true;
                    mm.Bcc.Add(fromEmail);
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(host, port);
                    smtp.EnableSsl = true;
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential(fromEmail, pw);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Send(mm);
                }
            }

            return Redirect("Contact?complete=1");
        }

        public ActionResult Shopping() 
        {

            Person p = new Person();
            ViewBag.people = p.GetPeople();

            return View();
        }

    }
}