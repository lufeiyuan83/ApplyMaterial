using System;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;

namespace Util
{
    public class MailSending : IDisposable
    {
        public static void SendMailUseGmail()
        {
            //System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            //msg.From = new MailAddress("caalh-st-lu.fy@mail.foxconn.com");
            //msg.To.Add("caalh-st-lu.fy@mail.foxconn.com");
            //msg.Subject = "Subject";
            //msg.SubjectEncoding = System.Text.Encoding.UTF8;
            //msg.Body = "Body";
            //msg.BodyEncoding = System.Text.Encoding.UTF8;
            //msg.IsBodyHtml = false;
            //msg.Priority = MailPriority.High;
            //SmtpClient client = new SmtpClient("10.134.28.95",25);
            //client.Credentials = new System.Net.NetworkCredential("caalh-st-lu.fy@mail.foxconn.com", "40826503qQ");
            //client.EnableSsl = true;
            //object userState = msg;
            //try
            //{
            //    //client.SendAsync(msg, userState);
            //    client.Send(msg);
            //}
            //catch (System.Net.Mail.SmtpException ex)
            //{
            //    int i = 0;
            //    i++;
            //}

            try
            {
                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress("caalh-st-lu.fy@mail.foxconn.com");
                    message.To.Add("caalh-st-lu.fy@mail.foxconn.com");
                    message.Subject = "subject";
                    message.SubjectEncoding = Encoding.UTF8;
                    message.Body = "Body";
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    using (SmtpClient sc = new SmtpClient("10.134.28.95", 25))
                    {
                        sc.UseDefaultCredentials = true;
                        //sc.Credentials = new System.Net.NetworkCredential("caalh-st-lu.fy@mail.foxconn.com", "40826503qQ");//caalh-st-lu.fy@mail.foxconn.com
                        sc.Credentials = new System.Net.NetworkCredential("caalh-st-lu.fy", "40826503qQ");//caalh-st-lu.fy@mail.foxconn.com
                        sc.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                        sc.Send(message);
                    }
                }
            }
            catch
            {
            }
        }
        public static bool SendMail()
        {
            try
            {
                using (System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("caalh-st-lu.fy@mail.foxconn.com", "caalh-st-lu.fy@mail.foxconn.com", "abc", "hello"))
                {
                    message.BodyEncoding = System.Text.Encoding.UTF8;
                    message.IsBodyHtml = true;
                    using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("Relay.aei.com"))
                    {
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential("caalh-st-lu.fy", "40826503Qq");
                        client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                        client.Send(message);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="m_From">From</param>
        /// <param name="m_To">To</param>
        /// <param name="m_CC">CC</param>
        /// <param name="m_Subject">Subject</param>
        /// <param name="m_Body">Body</param>
        /// <returns>是否发送成功</returns>
        public static bool Send(string m_From, string m_To, string m_CC, string m_Subject, string m_Body)
        {
            string[] mailToList = m_To.Trim().Split(';');
            if (mailToList != null && mailToList.Length > 0)
            {
                try
                {
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(m_From);
                    for (int i = 0; i < mailToList.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(mailToList[i]))
                            message.To.Add(mailToList[i]);
                    }
                    string[] mailCCList = m_CC.Trim().Split(';');
                    if (mailCCList != null && mailCCList.Length > 0)
                    {
                        for (int i = 0; i < mailCCList.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(mailCCList[i]))
                                message.CC.Add(mailCCList[i]);
                        }
                    }
                    message.Subject = m_Subject;
                    message.SubjectEncoding = Encoding.UTF8;
                    message.Body = m_Body;
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    //string attachment = "Certificate Mandatory.csv|Certificate Reminder.csv|Certificate Warning.csv";//添加附件
                    //string[] array = attachment.Split('|');
                    //for (int i = 0; i < array.Length; i++)
                    //{       //添加多个附件
                    //    message.Attachments.Add(new Attachment(array[i]));
                    //}
                    using (SmtpClient sc = new SmtpClient("asiapostoffice.singapore.ii-viasia.net", 25))
                    {
                        sc.UseDefaultCredentials = false;
                        sc.Credentials = new System.Net.NetworkCredential("Photonics.HR.PLD", "qwer@1234");//fis.notification@aei.com
                        sc.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                        sc.Send(message);
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }
        public static string Post(string Url,string jsonParas)
        {
            HttpWebRequest request =(HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType="application/x-www-form-urlencoded";
            
            byte[] payload=Encoding.UTF8.GetBytes(jsonParas);
            Stream writer;
            try{
                writer= request.GetRequestStream();
            }
            catch
            {
                writer=null;
            }
            writer.Write(payload,0,payload.Length);
            writer.Close();
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch(WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }
            Stream s = response.GetResponseStream();
            StreamReader sRead = new StreamReader(s);
            string postContent = sRead.ReadToEnd();
            sRead.Close();

            return postContent;
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="m_To">To</param>
        /// <param name="m_CC">CC</param>
        /// <param name="m_Subject">Subject</param>
        /// <param name="m_Body">Body</param>
        /// <returns>是否发送成功</returns>
        public static bool Send(string m_To, string m_CC, string m_Subject, string m_Body)
        {
            try
            {
                return true;
                //string strData = "{\"system_name\":\"Postboy\",\"mail_title\":\"" + m_Subject + "\",\"toMail\":\"" + m_To + "\",\"cc\":\"" + m_CC + "\",\"content\":\"" + m_Body + "\",\"html_status\":\"False\",\"attachments\":\"C:\\1.xml\",\"errors_type\":\"Material ID\"}";
                //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(@"http://10.129.4.95:9090/data_manage/");
                //request.Method = "POST";
                //request.ContentType = "application/x-www-form-urlencoded";
                ////request.Headers["attachment"] = @"C:\1.xml";
                ////request.Headers["filename"] = @"C:\1.xml";
                //List<string> lst = new List<string>();
                //for (int i = 0; i < request.Headers.Count; i++)
                //{
                //    lst.Add(request.Headers[i]);
                //}
                //byte[] byteToPost = Encoding.UTF8.GetBytes(strData);
                //using (Stream reqStream = request.GetRequestStream())
                //{
                //    reqStream.Write(byteToPost, 0, byteToPost.Length);
                //}
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //string responseResult = "";
                //if (response != null && response.StatusCode == HttpStatusCode.OK)
                //{
                //    StreamReader sRead;
                //    using (sRead = new StreamReader(response.GetResponseStream()))
                //    {
                //        responseResult = sRead.ReadToEnd();
                //    }
                //    sRead.Close();
                //}
                //response.Close();
                //return responseResult.ToUpper().Contains("TRUE");
            }
            catch
            {
                return false;
            }
            //Microsoft.Office.Interop.Outlook.Application objOLook = new Microsoft.Office.Interop.Outlook.Application();
            //Microsoft.Office.Interop.Outlook.MailItem objMItem = (Microsoft.Office.Interop.Outlook.MailItem)objOLook.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
            //objMItem.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML;
            //objMItem.Subject = m_Subject;
            //objMItem.HTMLBody = m_Body;
            //objMItem.To = m_To;
            //objMItem.CC = m_CC;
            //try
            //{
            //    objMItem.Send();
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
            //finally
            //{
            //    objMItem = null;
            //}
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="m_To">To</param>
        /// <param name="m_CC">CC</param>
        /// <param name="m_Subject">Subject</param>
        /// <param name="m_Body">Body</param>
        /// <param name="m_DeleteAfterSubmit">发送后是否删除发件箱的该邮件</param>
        /// <returns>是否发送成功</returns>
        //public static bool Send(string m_To, string m_CC, string m_Subject, string m_Body, bool m_DeleteAfterSubmit)
        //{
        //    //Microsoft.Office.Interop.Outlook.Application objOLook = new Microsoft.Office.Interop.Outlook.Application();
        //    //Microsoft.Office.Interop.Outlook.MailItem objMItem = (Microsoft.Office.Interop.Outlook.MailItem)objOLook.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
        //    //objMItem.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML;
        //    //objMItem.Subject = m_Subject;
        //    //objMItem.HTMLBody = m_Body;
        //    //objMItem.To = m_To;
        //    //objMItem.CC = m_CC;
        //    //objMItem.DeleteAfterSubmit = m_DeleteAfterSubmit;
        //    //try
        //    //{
        //    //    objMItem.Send();
        //        return true;
        //    //}
        //    //catch
        //    //{
        //    //    return false;
        //    //}
        //    //finally
        //    //{
        //    //    objMItem = null;
        //    //}
        //}
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="m_To">To</param>
        /// <param name="m_CC">CC</param>
        /// <param name="m_Subject">Subject</param>
        /// <param name="m_Body">Body</param>
        /// <param name="m_Attachments">Attachments</param>
        /// <returns>是否发送成功</returns>
        //public static bool Send(string m_To, string m_CC, string m_Subject, string m_Body, List<string> m_Attachments)
        //{
        //    //Microsoft.Office.Interop.Outlook.Application objOLook = new Microsoft.Office.Interop.Outlook.Application();
        //    ////Microsoft.Office.Interop.Outlook.MailItem objMItem = objOLook.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

        //    //Microsoft.Office.Interop.Outlook._MailItem objMItem = objOLook.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
        //    //objMItem.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML;
        //    //objMItem.Subject = m_Subject;

        //    //objMItem.HTMLBody = m_Body;
        //    //objMItem.To = m_To;
        //    //long intSize = 0;
        //    //if (m_Attachments != null)
        //    //{
        //    //    for (int i = 0; i < m_Attachments.Count; i++)
        //    //    {
        //    //        System.IO.FileInfo file = new System.IO.FileInfo(m_Attachments[i]);
        //    //        intSize += file.Length;
        //    //        objMItem.Attachments.Add(m_Attachments[i]);
        //    //    }
        //    //}

        //    //try
        //    //{
        //    //    System.Threading.Thread.Sleep(Convert.ToInt32(intSize / (1024 * 5)));
        //    //    objMItem.Send();
        //        return true;
        //    //}
        //    //catch
        //    //{
        //    //    return false;
        //    //}
        //    //finally
        //    //{
        //    //    objMItem = null;
        //    //}
        //}
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="m_To">To</param>
        /// <param name="m_CC">CC</param>
        /// <param name="m_Subject">Subject</param>
        /// <param name="m_Body">Body</param>
        /// <returns>是否发送成功</returns>
        //public static bool Send(string m_To, string m_CC, string m_Subject, string m_Body)
        //{
        //    string[] mailToList = m_To.Trim().Split(';');
        //    if (mailToList != null && mailToList.Length > 0)
        //    {
        //        try
        //        {
        //            MailMessage message = new MailMessage();
        //            message.From = new MailAddress("Fly.Lu@ii-vi.com");
        //            for (int i = 0; i < mailToList.Length; i++)
        //            {
        //                if (!string.IsNullOrEmpty(mailToList[i]))
        //                    message.To.Add(mailToList[i]);
        //            }
        //            string[] mailCCList = m_CC.Trim().Split(';');
        //            if (mailCCList != null && mailCCList.Length > 0)
        //            {
        //                for (int i = 0; i < mailCCList.Length; i++)
        //                {
        //                    if (!string.IsNullOrEmpty(mailCCList[i]))
        //                        message.CC.Add(mailCCList[i]);
        //                }
        //            }
        //            message.Subject = m_Subject;
        //            message.SubjectEncoding = Encoding.UTF8;
        //            message.Body = m_Body;
        //            message.BodyEncoding = Encoding.UTF8;
        //            message.IsBodyHtml = true;
        //            //string attachment = "Certificate Mandatory.csv|Certificate Reminder.csv|Certificate Warning.csv";//添加附件
        //            //string[] array = attachment.Split('|');
        //            //for (int i = 0; i < array.Length; i++)
        //            //{       //添加多个附件
        //            //    message.Attachments.Add(new Attachment(array[i]));
        //            //}
        //            SmtpClient sc = new SmtpClient("asiapostoffice.singapore.ii-viasia.net", 25);
        //            sc.UseDefaultCredentials = false;
        //            sc.Credentials = new System.Net.NetworkCredential("Fly.lu", "uiop@321");//"Fly.lu","abcd@1234");//"Photonics.HR.PLD", "qwer@1234");//fis.notification@aei.com
        //            sc.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

        //            sc.Send(message);
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //        return false;
        //}
        //public static void PlainText()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    mail.From = new MailAddress("Fly.Lu@aei.com");
        //    mail.To.Add("Fly.Lu@aei.com");

        //    //set the content
        //    mail.Subject = "This is an email";
        //    mail.Body = "this is the body content of the email.";

        //    //send the message
        //    SmtpClient smtp = new SmtpClient("Relay.aei.com");
        //    smtp.Send(mail);
        //}
        //public static void HtmlEmail()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    mail.From = new MailAddress("Fly.Lu@aei.com");
        //    mail.To.Add("Fly.Lu@aei.com");

        //    //set the content
        //    mail.Subject = "This is an email";
        //    mail.Body = "this is a sample body with html in it. <b>This is bold</b> <font color=#336699>This is blue</font>";
        //    mail.IsBodyHtml = true;

        //    //send the message
        //    SmtpClient smtp = new SmtpClient("Relay.aei.com");
        //    smtp.Send(mail);
        //}

        //public static void MultiPartMime()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    mail.From = new MailAddress("Fly.Lu@aei.com");
        //    mail.To.Add("Fly.Lu@aei.com");

        //    //set the content
        //    mail.Subject = "This is an email";

        //    //first we create the Plain Text part
        //    AlternateView plainView = AlternateView.CreateAlternateViewFromString("This is my plain text content, viewable by those clients that don't support html", null, "text/plain");
        //    //then we create the Html part
        //    AlternateView htmlView = AlternateView.CreateAlternateViewFromString("<b>this is bold text, and viewable by those mail clients that support html</b>", null, "text/html");
        //    mail.AlternateViews.Add(plainView);
        //    mail.AlternateViews.Add(htmlView);


        //    //send the message
        //    SmtpClient smtp = new SmtpClient("Relay.aei.com"); //specify the mail server address
        //    smtp.Send(mail);
        //}

        //public static void FriendlyFromName()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    //to specify a friendly 'from' name, we use a different ctor
        //    mail.From = new MailAddress("Fly.Lu@aei.com", "Steve James");
        //    mail.To.Add("Fly.Lu@aei.com");

        //    //set the content
        //    mail.Subject = "This is an email";
        //    mail.Body = "this is the body content of the email.";

        //    //send the message
        //    SmtpClient smtp = new SmtpClient("Relay.aei.com");
        //    smtp.Send(mail);

        //}

        //public static void FriendlyToName()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    //to specify a friendly 'from' name, we use a different ctor
        //    mail.From = new MailAddress("Fly.Lu@aei.com", "Steve James");

        //    //since the To,Cc, and Bcc accept addresses, we can use the same technique as the From address
        //    mail.To.Add(new MailAddress("Fly.Lu@aei.com", "Beth Jones"));
        //    mail.CC.Add(new MailAddress("donna@yourcompany.com", "Donna Summers"));
        //    mail.Bcc.Add(new MailAddress("bob@yourcompany.com", "Bob Smith"));

        //    //set the content
        //    mail.Subject = "This is an email";
        //    mail.Body = "this is the body content of the email.";

        //    //send the message
        //    SmtpClient smtp = new SmtpClient("Relay.aei.com");
        //    smtp.Send(mail);
        //}

        //public static void MultipleRecipients()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    //to specify a friendly 'from' name, we use a different ctor
        //    mail.From = new MailAddress("Fly.Lu@aei.com", "Steve James");

        //    //since the To,Cc, and Bcc accept addresses, we can use the same technique as the From address
        //    //since the To, Cc, and Bcc properties are collections, to add multiple addreses, we simply call .Add(...) multple times
        //    mail.To.Add("Fly.Lu@aei.com");
        //    mail.To.Add("you2@yourcompany.com");
        //    mail.CC.Add("cc1@yourcompany.com");
        //    mail.CC.Add("cc2@yourcompany.com");
        //    mail.Bcc.Add("blindcc1@yourcompany.com");
        //    mail.Bcc.Add("blindcc2@yourcompany.com");

        //    //set the content
        //    mail.Subject = "This is an email";
        //    mail.Body = "this is the body content of the email.";

        //    //send the message
        //    SmtpClient smtp = new SmtpClient("Relay.aei.com");
        //    smtp.Send(mail);
        //}

        //public static void FriendlyNonAsciiName()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    //to specify a friendly non ascii name, we use a different ctor. 
        //    //A ctor that accepts an encoding that matches the text of the name
        //    mail.From = new MailAddress("Fly.Lu@aei.com", "Steve 豣irk", Encoding.GetEncoding("iso-8859-1"));
        //    mail.To.Add("Fly.Lu@aei.com");

        //    //set the content
        //    mail.Subject = "This is an email";
        //    mail.Body = "this is the body content of the email.";

        //    //send the message
        //    SmtpClient smtp = new SmtpClient("Relay.aei.com");
        //    smtp.Send(mail);

        //}

        //public static void SetPriority()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    mail.From = new MailAddress("Fly.Lu@aei.com");
        //    mail.To.Add("Fly.Lu@aei.com");

        //    //set the content
        //    mail.Subject = "This is an email";
        //    mail.Body = "this is the body content of the email.";

        //    //specify the priority of the mail message
        //    mail.Priority = MailPriority.High;

        //    //send the message
        //    SmtpClient smtp = new SmtpClient("Relay.aei.com");
        //    smtp.Send(mail);
        //}

        //public static void SetTheReplyToHeader()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    mail.From = new MailAddress("Fly.Lu@aei.com");
        //    mail.To.Add("Fly.Lu@aei.com");

        //    //set the content
        //    mail.Subject = "This is an email";
        //    mail.Body = "this is the body content of the email.";

        //    //specify the priority of the mail message
        //    mail.ReplyTo = new MailAddress("SomeOtherAddress@mycompany.com");

        //    //send the message
        //    SmtpClient smtp = new SmtpClient("Relay.aei.com");
        //    smtp.Send(mail);
        //}

        //public static void CustomHeaders()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    mail.From = new MailAddress("Fly.Lu@aei.com");
        //    mail.To.Add("Fly.Lu@aei.com");

        //    //set the content
        //    mail.Subject = "This is an email";
        //    mail.Body = "this is the body content of the email.";

        //    //to add custom headers, we use the Headers.Add(...) method to add headers to the 
        //    //.Headers collection
        //    mail.Headers.Add("X-Company", "My Company");
        //    mail.Headers.Add("X-Location", "Hong Kong");


        //    //send the message
        //    SmtpClient smtp = new SmtpClient("Relay.aei.com");
        //    smtp.Send(mail);
        //}
        //public static void ReadReceipts()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    mail.From = new MailAddress("Fly.Lu@aei.com");
        //    mail.To.Add("Fly.Lu@aei.com");

        //    //set the content
        //    mail.Subject = "This is an email";
        //    mail.Body = "this is the body content of the email.";

        //    //To request a read receipt, we need add a custom header named 'Disposition-Notification-To'
        //    //in this example, read receipts will go back to 'someaddress@mydomain.com'
        //    //it's important to note that read receipts will only be sent by those mail clients that 
        //    //a) support them
        //    //and
        //    //b)have them enabled.
        //    mail.Headers.Add("Disposition-Notification-To", "<someaddress@mydomain.com>");


        //    //send the message
        //    SmtpClient smtp = new SmtpClient("Relay.aei.com");
        //    smtp.Send(mail);
        //}

        //public static void AttachmentFromFile()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    mail.From = new MailAddress("Fly.Lu@aei.com");
        //    mail.To.Add("Fly.Lu@aei.com");

        //    //set the content
        //    mail.Subject = "This is an email";
        //    mail.Body = "this content is in the body";

        //    //add an attachment from the filesystem
        //    mail.Attachments.Add(new Attachment("c:\\temp\\example.txt"));

        //    //to add additional attachments, simply call .Add(...) again
        //    mail.Attachments.Add(new Attachment("c:\\temp\\example2.txt"));
        //    mail.Attachments.Add(new Attachment("c:\\temp\\example3.txt"));

        //    //send the message
        //    SmtpClient smtp = new SmtpClient("Relay.aei.com");
        //    smtp.Send(mail);

        //}

        //public static void AttachmentFromStream()
        //{

        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    mail.From = new MailAddress("Fly.Lu@aei.com");
        //    mail.To.Add("Fly.Lu@aei.com");

        //    //set the content
        //    mail.Subject = "This is an email";
        //    mail.Body = "this content is in the body";

        //    //Get some binary data
        //    byte[] data = GetData();

        //    //save the data to a memory stream
        //    MemoryStream ms = new MemoryStream(data);

        //    //create the attachment from a stream. Be sure to name the data with a file and 
        //    //media type that is respective of the data
        //    mail.Attachments.Add(new Attachment(ms, "example.txt", "text/plain"));

        //    //send the message
        //    SmtpClient smtp = new SmtpClient("Relay.aei.com");
        //    smtp.Send(mail);
        //}
        //public static byte[] GetData()
        //{
        //    //this method just returns some binary data.
        //    //it could come from anywhere, such as Sql Server
        //    string s = "this is some text";
        //    byte[] data = Encoding.ASCII.GetBytes(s);
        //    return data;
        //}

        //public static void LoadFromConfig()
        //{
        //    //the from address, along with the server properties will be set in the app.config,
        //    //thus we don't need to specify them in code

        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    mail.To.Add("Fly.Lu@aei.com");

        //    //set the content
        //    mail.Subject = "This is an email";
        //    mail.Body = "this is the body content of the email.";

        //    //send the message
        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Send(mail);

        //}

        //public static void Authenticate()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    mail.From = new MailAddress("Fly.Lu@aei.com");
        //    mail.To.Add("Fly.Lu@aei.com");

        //    //set the content
        //    mail.Subject = "This is an email";
        //    mail.Body = "this is the body content of the email.";

        //    //send the message
        //    SmtpClient smtp = new SmtpClient("Relay.aei.com");

        //    //to authenticate we set the username and password properites on the SmtpClient
        //    smtp.Credentials = new NetworkCredential("username", "secret");
        //    smtp.Send(mail);

        //}

        //public static void ChangePort()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    mail.From = new MailAddress("Fly.Lu@aei.com");
        //    mail.To.Add("Fly.Lu@aei.com");

        //    //set the content
        //    mail.Subject = "This is an email";
        //    mail.Body = "this is the body content of the email.";

        //    //send the message
        //    SmtpClient smtp = new SmtpClient("Relay.aei.com");

        //    //to change the port (default is 25), we set the port property
        //    smtp.Port = 587;
        //    smtp.Send(mail);
        //}

        //public static void EmbedImages()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    mail.From = new MailAddress("Fly.Lu@aei.com");
        //    mail.To.Add("Fly.Lu@aei.com");

        //    //set the content
        //    mail.Subject = "This is an email";

        //    //first we create the Plain Text part
        //    AlternateView plainView = AlternateView.CreateAlternateViewFromString("This is my plain text content, viewable by those clients that don't support html", null, "text/plain");

        //    //then we create the Html part
        //    //to embed images, we need to use the prefix 'cid' in the img src value
        //    //the cid value will map to the Content-Id of a Linked resource.
        //    //thus <img src='cid:companylogo'> will map to a LinkedResource with a ContentId of 'companylogo'
        //    AlternateView htmlView = AlternateView.CreateAlternateViewFromString("Here is an embedded image.<img src=cid:companylogo>", null, "text/html");

        //    //create the LinkedResource (embedded image)
        //    LinkedResource logo = new LinkedResource("c:\\temp\\logo.gif");
        //    logo.ContentId = "companylogo";
        //    //add the LinkedResource to the appropriate view
        //    htmlView.LinkedResources.Add(logo);

        //    //add the views
        //    mail.AlternateViews.Add(plainView);
        //    mail.AlternateViews.Add(htmlView);


        //    //send the message
        //    SmtpClient smtp = new SmtpClient("Relay.aei.com"); //specify the mail server address
        //    smtp.Send(mail);
        //}

        //public static void SSL()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    mail.From = new MailAddress("Fly.Lu@aei.com");
        //    mail.To.Add("Fly.Lu@aei.com");

        //    //set the content
        //    mail.Subject = "This is an email";
        //    mail.Body = "this is the body content of the email.";

        //    //Port 587 is another SMTP port
        //    SmtpClient smtp = new SmtpClient("127.0.01", 587);
        //    smtp.EnableSsl = true;
        //    smtp.Send(mail);
        //}

        //public static void SendAsync()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    mail.From = new MailAddress("Fly.Lu@aei.com");
        //    mail.To.Add("Fly.Lu@aei.com");

        //    //set the content
        //    mail.Subject = "This is an email";
        //    mail.Body = "this is the body content of the email.";

        //    //send the message
        //    SmtpClient smtp = new SmtpClient("Relay.aei.com"); //specify the mail server address
        //    //the userstate can be any object. The object can be accessed in the callback method
        //    //in this example, we will just use the MailMessage object.
        //    object userState = mail;

        //    //wire up the event for when the Async send is completed
        //    smtp.SendCompleted += new SendCompletedEventHandler(SmtpClient_OnCompleted);

        //    smtp.SendAsync(mail, userState);
        //}
        //public static void SmtpClient_OnCompleted(object sender, AsyncCompletedEventArgs e)
        //{
        //    //Get the Original MailMessage object
        //    MailMessage mail = (MailMessage)e.UserState;

        //    //write out the subject
        //    string subject = mail.Subject;

        //    if (e.Cancelled)
        //    {
        //        Console.WriteLine("Send canceled for mail with subject [{0}].", subject);
        //    }
        //    if (e.Error != null)
        //    {
        //        Console.WriteLine("Error {1} occurred when sending mail [{0}] ", subject, e.Error.ToString());
        //    }
        //    else
        //    {
        //        Console.WriteLine("Message [{0}] sent.", subject);
        //    }
        //}

        //public static void PickupDirectory()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    mail.From = new MailAddress("Fly.Lu@aei.com");
        //    mail.To.Add("Fly.Lu@aei.com");

        //    //set the content
        //    mail.Subject = "This is an email";
        //    mail.Body = "this is the body content of the email.";

        //    //if we are using the IIS SMTP Service, we can write the message
        //    //directly to the PickupDirectory, and bypass the Network layer
        //    SmtpClient smtp = new SmtpClient();
        //    smtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
        //    smtp.Send(mail);
        //}

        //public static void EmailWebPage()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    mail.From = new MailAddress("Fly.Lu@aei.com");
        //    mail.To.Add("Fly.Lu@aei.com");

        //    //set the content
        //    mail.Subject = "This is an email";

        //    //screen scrape the html
        //    string html = ScreenScrapeHtml("http://localhost/example.htm");
        //    mail.Body = html;
        //    mail.IsBodyHtml = true;

        //    //send the message
        //    SmtpClient smtp = new SmtpClient("Relay.aei.com");
        //    smtp.Send(mail);

        //}
        //public static string ScreenScrapeHtml(string url)
        //{
        //    WebRequest objRequest = System.Net.HttpWebRequest.Create(url);
        //    StreamReader sr = new StreamReader(objRequest.GetResponse().GetResponseStream());
        //    string result = sr.ReadToEnd();
        //    sr.Close();
        //    return result;
        //}

        //public static void NonAsciiMail()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    mail.From = new MailAddress("Fly.Lu@aei.com");
        //    mail.To.Add("Fly.Lu@aei.com");

        //    //set the content
        //    mail.Subject = "This is an email";

        //    //to send non-ascii content, we need to set the encoding that matches the 
        //    //string characterset.
        //    //In this example we use the ISO-8859-1 characterset
        //    mail.Body = "this text has some ISO-8859-1 characters: 庖涨";
        //    mail.BodyEncoding = Encoding.GetEncoding("iso-8859-1");

        //    //send the message
        //    SmtpClient smtp = new SmtpClient("Relay.aei.com");
        //    smtp.Send(mail);

        //}

        //public static void InnerExceptions()
        //{
        //    //create the mail message
        //    MailMessage mail = new MailMessage();

        //    //set the addresses
        //    mail.From = new MailAddress("Fly.Lu@aei.com");
        //    mail.To.Add("him@hiscompany.com");

        //    //set the content
        //    mail.Subject = "This is an email";
        //    mail.Body = "this is the body content of the email.";

        //    //send the message
        //    SmtpClient smtp = new SmtpClient("Relay.aei.com");
        //    try
        //    {
        //        smtp.Send(mail);
        //    }
        //    catch (Exception ex)
        //    {
        //        Exception ex2 = ex;
        //        string errorMessage = string.Empty;
        //        while (ex2 != null)
        //        {
        //            errorMessage += ex2.ToString();
        //            ex2 = ex2.InnerException;
        //        }

        //        Console.WriteLine(errorMessage);
        //    }
        //}
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }
    }
}
