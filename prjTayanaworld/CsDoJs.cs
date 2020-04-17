using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace prjTayanaworld
{
    public class CsDoJs
    {
        #region 輸出javaScript到網頁上


        /// <summary>
        /// 輸出javaScript到網頁上
        /// </summary>
        /// <param name="JavaScript">要輸出到頁面上的JavaScript，不用加 &lt;script&gt;與 &lt;/script&gt;</param>
        /// <param name="page">就傳入Page就對了</param>
        /// <remarks></remarks>
        static public void doJavaScript(string JavaScript)
        {
            string script = "";
            string key = Guid.NewGuid().ToString();
            script += JavaScript;
            ((Page)HttpContext.Current.CurrentHandler).ClientScript.RegisterStartupScript(typeof(string), key, script, true);
        }
        #endregion

    }
}