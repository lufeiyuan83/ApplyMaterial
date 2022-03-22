using FineUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP
{
    public partial class BasePage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            var pm = PageManager.Instance;
            if (pm != null)
            {
                HttpCookie langCookie = Request.Cookies["Language"];
                if (langCookie != null)
                {
                    string langValue = langCookie.Value;
                    pm.Language = (Language)Enum.Parse(typeof(Language), langValue, true);
                }
            }
            base.OnInit(e);
        }
        protected override void InitializeCulture()
        {
            if (Request.Cookies["Language"] != null)
            {
                string strLanguage = Request.Cookies["Language"].Value.ToString();
                if (!string.IsNullOrEmpty(strLanguage))
                {
                    switch(strLanguage.ToLower())
                    {
                        case "en":
                            strLanguage = "en-US";
                            break;
                        case "zh_tw":
                            strLanguage = "zh-TW";
                            break;
                        default:
                            strLanguage = "zh-CN";
                            break;
                    }
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(strLanguage);
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(strLanguage);
                }
            }
            else
            {
                HttpCookie lan = new HttpCookie("Language", "zh_CN");
                Request.Cookies.Add(lan);
            }
        }
    }
}