using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace meditatii.web.Utils
{
    public static class EmailHelper
    {
        public static string GetEmailTemplate(string filename)
        {
            return System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/emailtemplate/" + filename + ".txt"));
        }
    }

    public static class Security
    {
        const string before = "1978";
        const string after = "20062011";

        public static string EncryptID(int id)
        {
            var plainText = before + id.ToString() + after;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return HttpUtility.UrlEncode(System.Convert.ToBase64String(plainTextBytes));
        }

        public static int DecryptID(string code)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(HttpUtility.UrlDecode(code));
            var codetext = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            codetext = codetext.Substring(before.Length);
            codetext = codetext.Substring(0, codetext.Length - after.Length);

            return Convert.ToInt32(codetext);
        }
    }
}