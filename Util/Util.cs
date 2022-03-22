using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Util
{
    public class Util
    {
        public static string webURL = @"http://10.148.15.80";
        public static string language = "Resource_zh_CN";
        public const string strRev = "1.2.1.4";
        public static bool IsShowNotice = false;
        public static Form frmMain = null;
        public static string applicationOrg="01";
        public static string SystemCode = "";
        public static string UserName = "";
        public static int Validity = 90;
        public static string Computer = Environment.MachineName;
        public static string IP = System.Net.Dns.GetHostEntry(Environment.MachineName).AddressList[0].ToString();
        /// <summary>
        /// 将DataTable对象转换成Json字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string ConvertDataTableToJson(System.Data.DataTable dt)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 将Json字符串转换成DataTable对象
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public System.Data.DataTable ConvertJsonToDataTable(string strJson)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(strJson);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 将DataTable对象转换成XML字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ConvertDataTableToXml(DataTable dt)
        {
            try
            {
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dt.WriteXml(writer);
                string xmlstr = writer.ToString();
                writer.Close();
                return xmlstr;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 将XML字符串转换成DataSet对象
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        public static DataSet ConvertXmlToDataTable(string xmlData)
        {
            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXml(xmlData);
                return ds;
            }
            catch
            {
                return null;
            }
        }
        public static DateTime ConvertDateTime(DateTime m_dt,int m_Week)
        {
            return m_dt.AddDays((m_Week - 1) * 7 + 1 - Convert.ToInt32(m_dt.DayOfWeek)); 
        }
        public static string IntToMonth(int m_Month)
        {
            string strMonth = "";
            switch (m_Month)
            {
                case 1:
                    strMonth= "January";
                    break;
                case 2:
                    strMonth= "February";
                    break;
                case 3:
                    strMonth= "March";
                    break;
                case 4:
                    strMonth= "April";
                    break;
                case 5:
                    strMonth= "May";
                    break;
                case 6:
                    strMonth = "June";
                    break;
                case 7:
                    strMonth= "July";
                    break;
                case 8:
                    strMonth = "August";
                    break;
                case 9:
                    strMonth = "September";
                    break;
                case 10:
                    strMonth = "October";
                    break;
                case 11:
                    strMonth = "November";
                    break;
                case 12:
                    strMonth = "December";
                    break;
            }
            return strMonth;
        }
        /// <summary>
        /// 18位身份证验证
        /// </summary>
        /// <param name="m_ID">身份证号</param>
        /// <returns></returns>
        public static bool IsValidIDCard(string m_ID)
        {
            if (m_ID.Trim().Length < 18)
                return false;
            if (!IsLong(m_ID.Remove(17)) || Convert.ToInt64(m_ID.Remove(17)) < Math.Pow(10, 16) || !IsLong(m_ID.Replace('x', '0').Replace('X', '0')))
                return false;//数字验证
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(m_ID.Remove(2)) == -1)
                return false;//省份验证
            string birth = m_ID.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            //DateTime time = new DateTime();
            if (!IsDateTime(birth))
                return false;//生日验证
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = m_ID.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            Math.DivRem(sum, 11, out int y);
            if (arrVarifyCode[y] != m_ID.Substring(17, 1).ToLower())
                return false;//校验码验证
            return true;//符合GB11643-1999标准
        }
        public static bool IsDateTime(string strDateTime)
        {
            try
            {
                Convert.ToDateTime(strDateTime);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static DateTime ConvertToDateTime(string strDateTime)
        {
            if (IsDateTime(strDateTime))
                return Convert.ToDateTime(strDateTime);
            else
                return DateTime.MaxValue;
        }
        public static bool IsInt(string strInt)
        {
            try
            {
                if (!string.IsNullOrEmpty(strInt))
                {
                    Convert.ToInt32(strInt);
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsLong(string strInt)
        {
            try
            {
                if (!string.IsNullOrEmpty(strInt))
                {
                    Convert.ToInt64(strInt);
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }         
        public static bool IsDouble(string strInt)
        {
            try
            {
                if (!string.IsNullOrEmpty(strInt))
                {
                    Convert.ToDouble(strInt);
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsBool(string strBool)
        {
            try
            {
                Convert.ToBoolean(strBool);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void ShowLineNumber(DataGridView dgv, DataGridViewRowPostPaintEventArgs e)
        {
            Font font = new Font(dgv.Font.Name, dgv.Font.Size, FontStyle.Bold);
            SizeF sizeF = e.Graphics.MeasureString((e.RowIndex + 1).ToString(), font);
            string strText = (e.RowIndex + 1).ToString();
            Graphics graphics = e.Graphics;
            if (dgv.RowHeadersWidth - sizeF.Width >= 10)
            {
                Rectangle rectangle = new Rectangle(Convert.ToInt32((dgv.RowHeadersWidth - sizeF.Width) / 2), e.RowBounds.Location.Y + Convert.ToInt32((dgv.RowTemplate.Height - sizeF.Height) / 2), e.RowBounds.Width, e.RowBounds.Height);
                TextRenderer.DrawText(graphics, strText, font, rectangle, dgv.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter & TextFormatFlags.Right);
            }
            else
            {
                dgv.RowHeadersWidth = Convert.ToInt32(dgv.RowHeadersWidth + sizeF.Width);
                Rectangle rectangle = new Rectangle(Convert.ToInt32((dgv.RowHeadersWidth - sizeF.Width) / 2), e.RowBounds.Location.Y + Convert.ToInt32((dgv.RowTemplate.Height - sizeF.Height) / 2), e.RowBounds.Width, e.RowBounds.Height);
                TextRenderer.DrawText(graphics, strText, font, rectangle, dgv.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter & TextFormatFlags.Right);
            }
        }
        /// <summary>
        /// 获得周数
        /// </summary>
        /// <returns>Week</returns>
        public static int GetWeekOfYear()
        {
            int firstWeekend = 7 - Convert.ToInt32(DateTime.Parse(DateTime.Today.Year + "-1-1").DayOfWeek);
            int currentDay = DateTime.Today.DayOfYear;
            return Convert.ToInt32(Math.Ceiling((currentDay - firstWeekend) / 7.0)) + 1;
        }
        /// <summary>
        /// 异或
        /// </summary>
        /// <param name="str">str</param>
        /// <returns></returns>
        public static string XOR(string str)
        {
            char[] a = str.ToCharArray();
            for (int i = 0; i < a.Length; i++)
                a[i] = (char)(a[i] ^ 232311109);
            return new string(a);
        }
        /// <summary>
        /// 去datatble中列的Distinct
        /// </summary>
        /// <param name="SourceTable"></param>
        /// <param name="keyFields">列名，以逗号分隔</param>
        /// <returns></returns>
        public static List<string> SelectDistinct(DataTable SourceTable, string keyField)
        {
            List<string> lstDistict = new List<string>();
            if (SourceTable != null)
            {                
                //DataTable dtRet = SourceTable.Clone();//定义返回记录表
                StringBuilder sRet = new StringBuilder();//定义比较对象
                //int result = 0;//定义循环变量
                string sLastValue = "";//定义对照值
                SourceTable.Select("", keyField);//按参照列排序
                foreach (DataRow row in SourceTable.Rows)//开始比对
                {
                    sRet.Length = 0;
                    sRet.Append(row[keyField]);
                    int result = string.Compare(sRet.ToString(), sLastValue, true);//进行比较并判断比较结果
                    switch (result)
                    {
                        case 0://相同则放弃
                            break;
                        case -1://不同则加入，并将当前比较字符串赋给对照值
                        case 1:
                            if (!lstDistict.Contains(row[keyField].ToString()))
                            {
                                lstDistict.Add(row[keyField].ToString());
                                sLastValue = sRet.ToString();
                            }
                            break;
                    }
                }
            }
            return lstDistict;
        }
        /// <summary>
        /// 发送短信接口
        /// 获取短信数量接口地址(UTF8)：http://sms.webchinese.cn/web_api/SMS/?Action=SMS_Num&Uid=本站用户名&Key=接口安全秘钥
        /// </summary>
        /// <param name="m_UID">用户名</param>
        /// <param name="m_Key">Key,该key需要在注册网站上获得</param>
        /// <param name="m_ToPhone">发送到（手机）</param>
        /// <param name="m_Content">发送内容</param>
        /// <returns>发送结果</returns>
        public static string SendSMS(string m_UID, string m_Key, string m_ToPhone, string m_Content)
        {
            //短信发送后返回值 说　明 
            //    -1  没有该用户账户 
            //    -2 接口密钥不正确,不是账户登陆密码 
            //    -21 MD5接口密钥加密不正确 
            //    -3 短信数量不足 
            //    -11 该用户被禁用 
            //    -14 短信内容出现非法字符 
            //    -4 手机号格式不正确 
            //    -41 手机号码为空 
            //    -42 短信内容为空 
            //    -51 短信签名格式不正确, 接口签名格式为：【签名内容】 
            //    -6 IP限制 
            //    大于0 短信发送数量 
            string strRet = null;
            //格式 string url = @"http://utf8.sms.webchinese.cn/?Uid=FLy.Lu&key=1f243b5dd842788ec5a1&smsMob=18823886287&smsText=考勤异常通知：您有考勤异常，请登录到FIS自行查询，系统自动发送，请勿回复！ 【高意通讯有限公司】";
            string url = @"http://utf8.sms.webchinese.cn/?Uid=" + m_UID + "&key=" + m_Key + "&smsMob=" + m_ToPhone + "&smsText=" + m_Content;
            if (url == null || url.Trim().ToString() == "")
            {
                return strRet;
            }
            string targeturl = url.Trim().ToString();
            try
            {
                HttpWebRequest hr = (HttpWebRequest)WebRequest.Create(targeturl);
                hr.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
                hr.Method = "GET";
                hr.Timeout = 30 * 60 * 1000;
                WebResponse hs = hr.GetResponse();
                Stream sr = hs.GetResponseStream();
                StreamReader ser = new StreamReader(sr, Encoding.Default);
                return ser.ReadToEnd();
            }
            catch
            {
                return strRet;
            }
        }
        /// <summary>
        /// 把DataTable转化为HTML语言
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="m_Columns">Title</param>
        /// <returns></returns>
        public static string ConvertDataTableToHTML(DataTable dt, string m_Columns = "")
        {
            string strBody = "";
            strBody += "<table style='border:0px solid #cad9ea;color:#666;' border='0' cellspacing='0' cellpadding='0'>";

            strBody += "<tr>";
            if (string.IsNullOrEmpty(m_Columns))
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                    strBody += "<th style='border:1px solid #cad9ea;color:#666; padding:0 1em 0;margin:0 0 0 0'>" + dt.Columns[i].ColumnName + "</th>";

                strBody += "</tr>";
                if (dt.Columns.Contains("Color"))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        strBody += "<tr " + dt.Rows[i]["Color"].ToString() + ">";
                        for (int j = 0; j < dt.Columns.Count; j++)
                            strBody += "<td style='border:1px solid #cad9ea;color:#666; padding:0 1em 0;margin:0 0 0 0'>" + dt.Rows[i][j].ToString().Trim() + "</td>";
                        strBody += "</tr>";
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        strBody += "<tr>";
                        for (int j = 0; j < dt.Columns.Count; j++)
                            strBody += "<td style='border:1px solid #cad9ea;color:#666; padding:0 1em 0;margin:0 0 0 0'>" + dt.Rows[i][j].ToString().Trim() + "</td>";
                        strBody += "</tr>";
                    }
                }
            }
            else
            {
                string[] arrColumns = m_Columns.Split(',');
                for (int i = 0; i < arrColumns.Length; i++)
                    strBody += "<th style='border:1px solid #cad9ea;color:#666; padding:0 1em 0;margin:0 0 0 0'>" + (arrColumns[i].Trim().Contains(" ") ? arrColumns[i].Trim() .Split(' ')[1]: arrColumns[i].Trim()) + "</th>";

                strBody += "</tr>";
                if (dt.Columns.Contains("Color"))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        strBody += "<tr " + dt.Rows[i]["Color"].ToString() + ">";
                        for (int j = 0; j < arrColumns.Length; j++)
                            strBody += "<td style='border:1px solid #cad9ea;color:#666; padding:0 1em 0;margin:0 0 0 0'>" + dt.Rows[i][(arrColumns[j].Trim().Contains(" ") ? arrColumns[j].Trim().Split(' ')[0] : arrColumns[j].Trim())].ToString().Trim() + "</td>";
                        strBody += "</tr>";
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        strBody += "<tr>";
                        for (int j = 0; j < arrColumns.Length; j++)
                            strBody += "<td style='border:1px solid #cad9ea;color:#666; padding:0 1em 0;margin:0 0 0 0'>" + dt.Rows[i][(arrColumns[j].Trim().Contains(" ") ? arrColumns[j].Trim().Split(' ')[0] : arrColumns[j].Trim())].ToString().Trim() + "</td>";
                        strBody += "</tr>";
                    }
                }
            }
            strBody += "</table>";
            return strBody;
        }
        //public static void WriteLog(Exception e)
        //{
        //    Log4net log = LogFactory.GetLogger();

        //    log.Error(e.Message + e.StackTrace);
        //}
        /// <summary>
        /// 在Unix上打印标签
        /// </summary>
        /// <param name="m_SentString">传输字符串参数</param>
        /// <returns></returns>
        public static bool PrintSALabel(string m_SentString)
        {
            try
            {
                string strMappath = DateTime.Now.ToString("yyMMddHHmmss");

                StreamWriter sw = new StreamWriter(strMappath, false, Encoding.ASCII, 300);
                sw.Write(m_SentString);
                sw.Close();

                FileInfo fi1 = new FileInfo(strMappath);

                // Create FtpWebRequest object from the Uri provided
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://10.192.8.187/home/PrnShipmentLab/Prnstring"));

                // Provide the WebPermission Credintials
                reqFTP.Credentials = new NetworkCredential("root", "root");

                // By default KeepAlive is true, where the control connection is not closed
                // after a command is executed.
                reqFTP.KeepAlive = false;

                // Specify the command to be executed.
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

                // Specify the data transfer type.
                reqFTP.UseBinary = true;

                // Notify the server about the size of the uploaded file
                reqFTP.ContentLength = fi1.Length;

                // The buffer size is set to 4kb
                int buffLength = 4096;
                byte[] buff = new byte[buffLength];
                int contentLen;

                // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
                FileStream fs = fi1.OpenRead();
                // Stream to which the file to be upload is written
                Stream strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time
                contentLen = fs.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                strm.Close();
                fs.Close();
                System.Threading.Thread.Sleep(5500);
                fi1.Delete();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //public static DataTable GetOutlookInfo(bool m_Open,DataTable m_Dt,string m_UserName)
        //{
        //    //try
        //    //{
        //    //    if (!m_Open)
        //    //        return null;
        //    //    DataRow[] drs = m_Dt.Select("EmployeeID='" + m_UserName.Trim().ToUpper() + "'");
        //    //    if (drs.Length > 0)
        //    //    {
        //    //        Microsoft.Office.Interop.Outlook.Application application = new Microsoft.Office.Interop.Outlook.Application();
        //    //        Microsoft.Office.Interop.Outlook.NameSpace ns = application.Session;
        //    //        Microsoft.Office.Interop.Outlook.MAPIFolder inbox = ns.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);
        //    //        DataTable dt = new DataTable();
        //    //        dt.Columns.Add("OlDefaultFoldersType");
        //    //        dt.Columns.Add("EmployeeID");
        //    //        dt.Columns.Add("Name");
        //    //        dt.Columns.Add("SenderName");
        //    //        dt.Columns.Add("SenderEmailAddress");
        //    //        dt.Columns.Add("To");
        //    //        dt.Columns.Add("CC");
        //    //        dt.Columns.Add("BCC");
        //    //        dt.Columns.Add("Subject");
        //    //        dt.Columns.Add("TaskSubject");
        //    //        dt.Columns.Add("Body");
        //    //        dt.Columns.Add("HTMLBody");
        //    //        dt.Columns.Add("ReceivedTime");
        //    //        dt.Columns.Add("FilePath");
        //    //        foreach (Microsoft.Office.Interop.Outlook.MailItem mail in inbox.Items)
        //    //        {
        //    //            DataRow dr = dt.NewRow();
        //    //            dr["OlDefaultFoldersType"] = "olFolderInbox";
        //    //            dr["EmployeeID"] = m_UserName;
        //    //            dr["Name"] = drs[0]["Name"].ToString();
        //    //            dr["SenderName"] = mail.SenderName;
        //    //            dr["SenderEmailAddress"] = mail.SenderEmailAddress;
        //    //            dr["To"] = mail.To;
        //    //            dr["CC"] = mail.CC;
        //    //            dr["BCC"] = mail.BCC;
        //    //            dr["Subject"] = mail.Subject;
        //    //            dr["Body"] = mail.Body;
        //    //            dr["HTMLBody"] = mail.HTMLBody;
        //    //            dr["TaskSubject"] = mail.TaskSubject;
        //    //            dr["ReceivedTime"] = mail.ReceivedTime;
        //    //            dr["FilePath"] = mail.SendUsingAccount.DeliveryStore.FilePath;
        //    //            dt.Rows.Add(dr);
        //    //        }
        //    //        inbox = ns.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderDeletedItems);
        //    //        foreach (Microsoft.Office.Interop.Outlook.MailItem mail in inbox.Items)
        //    //        {
        //    //            DataRow dr = dt.NewRow();
        //    //            dr["OlDefaultFoldersType"] = "olFolderDeletedItems";
        //    //            dr["EmployeeID"] = m_UserName;
        //    //            dr["Name"] = drs[0]["Name"].ToString();
        //    //            dr["SenderName"] = mail.SenderName;
        //    //            dr["SenderEmailAddress"] = mail.SenderEmailAddress;
        //    //            dr["To"] = mail.To;
        //    //            dr["CC"] = mail.CC;
        //    //            dr["BCC"] = mail.BCC;
        //    //            dr["Subject"] = mail.Subject;
        //    //            dr["Body"] = mail.Body;
        //    //            dr["HTMLBody"] = mail.HTMLBody;
        //    //            dr["TaskSubject"] = mail.TaskSubject;
        //    //            dr["ReceivedTime"] = mail.ReceivedTime;
        //    //            dr["FilePath"] = mail.SendUsingAccount.DeliveryStore.FilePath;
        //    //            dt.Rows.Add(dr);
        //    //        }
        //    //        inbox = ns.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderSentMail);
        //    //        foreach (Microsoft.Office.Interop.Outlook.MailItem mail in inbox.Items)
        //    //        {
        //    //            DataRow dr = dt.NewRow();
        //    //            dr["OlDefaultFoldersType"] = "olFolderSentMail";
        //    //            dr["EmployeeID"] = m_UserName;
        //    //            dr["Name"] = drs[0]["Name"].ToString();
        //    //            dr["SenderName"] = mail.SenderName;
        //    //            dr["SenderEmailAddress"] = mail.SenderEmailAddress;
        //    //            dr["To"] = mail.To;
        //    //            dr["CC"] = mail.CC;
        //    //            dr["BCC"] = mail.BCC;
        //    //            dr["Subject"] = mail.Subject;
        //    //            dr["Body"] = mail.Body;
        //    //            dr["HTMLBody"] = mail.HTMLBody;
        //    //            dr["TaskSubject"] = mail.TaskSubject;
        //    //            dr["ReceivedTime"] = mail.ReceivedTime;
        //    //            dr["FilePath"] = mail.SendUsingAccount.DeliveryStore.FilePath;
        //    //            dt.Rows.Add(dr);
        //    //        }
        //    //        return dt;
        //    //    }
        //    //    else
        //    //        return null;
        //    //}
        //    //catch
        //    //{
        //        return null;
        //    //}
        //}
    }
}
