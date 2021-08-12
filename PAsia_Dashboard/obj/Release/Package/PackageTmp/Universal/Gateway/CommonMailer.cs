using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace PAsia_Dashboard.Universal.Gateway
{
    public class CommonMailer
    {
        private DBConnection dbConn = new DBConnection();
        private DBHelper dbHelper = new DBHelper();
        private IDGenerated idGenerated = new IDGenerated();
        static void Disable_CertificateValidation()
        {
            // Disabling certificate validation can expose you to a man-in-the-middle attack
            // which may allow your encrypted message to be read by an attacker
            // https://stackoverflow.com/a/14907718/740639
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (
                    object s,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors
                )
                {
                    return true;
                };
        }

        public string SendMail(string mailTo, string subject, string body)
        {if (string.IsNullOrEmpty(mailTo))
            {
                return "Mail id is null.";
            }
            try
            {
                using (SmtpClient sc = new SmtpClient())
                {
                    using (MailMessage m = new MailMessage())
                    {
                         System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        m.From = new MailAddress("notification@pharmasia.com.bd", "Requisition Alert");
                        m.To.Add(new MailAddress(mailTo, ""));
                        m.Subject = subject;
                        m.IsBodyHtml = true;
                        m.Body = body;

                        //sc.Host = "smtp.gmail.com";
                        //sc.Port = 587;
                        //sc.Credentials = new
                        //System.Net.NetworkCredential("testsquare80@gmail.com", "Aa01520104711");

                        //sc.Host = "mail.pharmasia.com.bd";
                        //sc.Port = 587;
                        //sc.Credentials = new System.Net.NetworkCredential("notification@pharmasia.com.bd", "Pharma@IT#10"); 
                        
                        //sc.Host = "mail.x-mail.asia";
                        sc.Host = "103.17.181.20";
                        //sc.Port = 587;
                        sc.Port = 25;
                        sc.Credentials = new System.Net.NetworkCredential("notification@pharmasia.com.bd", "pharmasia@321#");
                        //sc.EnableSsl = true;
                        sc.Send(m);
                    }
                }
                //SmtpClient smtpServer = new SmtpClient("172.16.128.41")
                //{
                //    Port = 25,
                //    //Port = 587,
                //    Credentials = new NetworkCredential("notify.pdms@squaregroup.com", "SquarePharma121"),
                //    EnableSsl = true
                //};


                return "true";
            }
            catch (SmtpFailedRecipientsException ex)
            {
                return ex.Message;
                throw;
            }


        }
        //public bool SendMail(string mailTo, string subject, string body)
        //{
        //    bool sts = false;

        //    //SmtpClient smtpServer = new SmtpClient("172.16.128.39")
        //    SmtpClient smtpServer = new SmtpClient("172.16.128.41")
        //    {
        //        Port = 25,
        //        //Port = 587,
        //        Credentials = new NetworkCredential("notify.pdms@squaregroup.com", "SquarePharma121"),
        //        EnableSsl = true
        //    };

        //    SmtpClient smtpServer2 = new SmtpClient("172.16.128.40")
        //    {
        //        Port = 25,
        //        //Port = 587,
        //        Credentials = new NetworkCredential("notify.pdms@squaregroup.com", "SquarePharma121"),
        //        EnableSsl = true
        //    };

        //    SmtpClient smtpServer3 = new SmtpClient("172.16.128.39")
        //    {
        //        Port = 25,
        //        //Port = 587,
        //        Credentials = new NetworkCredential("notify.pdms@squaregroup.com", "SquarePharma121"),
        //        EnableSsl = true
        //    };


        //    char[] splitter = { ';' };
        //    var mailList = mailTo.Split(splitter);

        //    for (int i = 0; i <= mailList.Length - 1; i++)
        //    {
        //        Disable_CertificateValidation();
        //        try
        //        {
        //            smtpServer2.Send("notify.pdms@squaregroup.com", mailList[i], subject, body);
        //            sts = true;
        //        }
        //        catch (SmtpException se)
        //        {
        //            smtpServer.Send("notify.pdms@squaregroup.com", mailList[i], subject, body);
        //            sts = true;
        //        }
        //        catch
        //        {
        //            smtpServer3.Send("notify.pdms@squaregroup.com", mailList[i], subject, body);
        //            sts = true;
        //        }
        //    }

        //    return sts;
        //}


        public Boolean MailLogger(string formName, string trsId, string mailTo, string trigType, string status)
        {
            int EMPLOYEE_CODE = Convert.ToInt32(System.Web.HttpContext.Current.Session["EMPLOYEE_CODE"]);
            string ACCESS_LEVEL = System.Web.HttpContext.Current.Session["ACCESS_LEVEL"].ToString();
            string today = DateTime.Now.ToString("M/d/yyyy");
            long MaxID;
            string IUMode;
            try
            {
                MaxID = idGenerated.getMAXSL("SA_MAIL_TRACK", "ML_TRACK_ID", dbConn.SAConnStrReader("Dashboard"));
                IUMode = "I";
                string Qry = "Insert into SA_MAIL_TRACK (ML_TRACK_ID, TRACKER_NO, TRIGGER_TYPE,MAIL_TO, FORM_NAME, MAIL_TIME, MAIL_STATUS) " +
                    "Values(" + MaxID + ", '" + trsId + "','" + trigType + "','" + mailTo + "','" + formName + "',TO_DATE('" + DateTime.Now.ToString("dd/MM/yyyy") + "','dd/MM/yyyy')," +
                       "'" + status + "')";
                if (dbHelper.CmdExecute(dbConn.SAConnStrReader("Dashboard"), Qry))
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}