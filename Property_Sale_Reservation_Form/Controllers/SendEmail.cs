using System;
using System.Data;
using System.Configuration;
using System.Web;

using System.Net.Mail;


namespace Property_Sale_Reservation_Form.Controllers
{
    /// <summary>
    /// Summary description for EmailUtils
    /// </summary>
    public class EmailUtils
    {
        public EmailUtils()
        {
        }


        private string _ErrMessage;

        public string ErrMessage
        {
            get
            {
                return _ErrMessage;
            }
            set
            {
                _ErrMessage = value;
            }
        }
        private string _SMTPServer;

        public string SMTPServer
        {
            get
            {
                return _SMTPServer;
            }
            set
            {
                _SMTPServer = value;
            }
        }
        private string _TestEmail;

        public string TestEmail
        {
            get
            {
                return _TestEmail;
            }
            set
            {
                _TestEmail = value;
            }
        }
        private bool _TestMode;

        public bool TestMode
        {
            get
            {
                return _TestMode;
            }
            set
            {
                _TestMode = value;
            }
        }

        public string EmailPreviewText(string strTo, string strFrom, string strSubject, string strBody, string strAttachment)
        {
            string strTemp = "";
            strTemp += "<table width='100%' cellpadding='3' cellspacing='1'>";
            strTemp += "<tr>";
            strTemp += "<td class='formleft'>To:</td>";
            strTemp += "<td class='formright'>" + strTo + "</td>";
            strTemp += "</tr>";
            strTemp += "<tr>";
            strTemp += "<td class='formleft'>From:</td>";
            strTemp += "<td class='formright'>" + strFrom + "</td>";
            strTemp += "</tr>";
            strTemp += "<tr>";
            strTemp += "<td class='formleft'>Subject:</td>";
            strTemp += "<td class='formright'>" + strSubject + "</td>";
            strTemp += "</tr>";
            strTemp += "<tr>";
            strTemp += "<td class='normal' colspan='2'>" + Helpers.CRToBR(strBody) + "</td>";
            strTemp += "</tr>";
            strTemp += "</table>";
            return strTemp;
        }

        public bool SendEmail(string strTo, string strFrom, string strSubject, string strBody, string strAttachment)
        {
            return SendEmail(strTo, strFrom, "", strSubject, strBody, strAttachment);
        }
        public bool SendEmail(string strTo, string strFrom, string strCC, string strSubject, string strBody, string strAttachment)
        {
            SmtpClient smtpClient = new SmtpClient();

            MailMessage objMM;
            objMM = new MailMessage();

            if (strAttachment != "")
            {
                strAttachment = FileUtils.GetPhysicalPath(strAttachment);
                Attachment Attach = new Attachment(strAttachment);
                objMM.Attachments.Add(Attach);
            }

            string strTestFileName = "";

            FileUtils objFile = new FileUtils();

            string strSMTPServer = Helpers.NullToString(SMTPServer);

            if (strSMTPServer == "")
            {
                strSMTPServer = AppOptions.GetOption("SMTPServer");
            }

            bool boolOK = false;

            if (strSMTPServer == "")
            {
                
                return boolOK;
            }

            //Logging.LogMessage("--EMAIL--");
            //Logging.LogMessage("Subject:" + strSubject);
            //Logging.LogMessage(strBody);
            //Logging.LogMessage("From:" + strFrom);
            //Logging.LogMessage("To:" + strTo);

            objMM.IsBodyHtml = Helpers.IsHTMLEmail(strBody);

            if (Helpers.NullToString(TestEmail) != "")
            {
                objMM.To.Add(TestEmail);
            }
            else
            {
                objMM.To.Add(strTo);
            }

            if (strCC != "")
            {
                objMM.CC.Add(strCC);
            }

            MailAddress fromAddress = new MailAddress(strFrom);

            objMM.From = fromAddress;
            objMM.Priority = MailPriority.Normal;
            objMM.Subject = strSubject;
            objMM.Body = strBody;
            if (TestMode)
            {
                strTestFileName = Helpers.GetConfigSetting("appVirtualRoot") + "EmailDump/" + strTo + ".txt";
                //Logging.LogMessage("Filename:" + strTestFileName);

                try
                {
                    objFile.SaveTextToFile(strSubject + strBody, HttpContext.Current.Server.MapPath(strTestFileName), "");
                }
                catch (Exception Err)
                {
                  
                }

            }
            else
            {
                try
                {
                    smtpClient.Host = strSMTPServer;
                    smtpClient.Send(objMM);
                    boolOK = true;
                }
                catch (Exception Err)
                {
                    while (!((Err.InnerException == null)))
                    {
                        ErrMessage += Err.InnerException.ToString() + "<br/><br/>";
                        Err = Err.InnerException;
                    }
                    boolOK = false;
                 
                }
            }
            return boolOK;
        }

        public static void SendEmail(string strSubject, string strBody)
        {
            string strTo = AppOptions.GetOption("ToEmail");
            string strFrom = AppOptions.GetOption("FromEmail");
            string strCC = AppOptions.GetOption("CCEmail");

            EmailUtils objEmail = new EmailUtils();
            objEmail.SendEmail(strTo, strFrom, strCC, strSubject, strBody, "");
        }

    }
}
