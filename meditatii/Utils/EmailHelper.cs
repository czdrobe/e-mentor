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
            if (code == "")
            {
                return 0;
            }
            var base64EncodedBytes = System.Convert.FromBase64String(HttpUtility.UrlDecode(code));
            var codetext = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            codetext = codetext.Substring(before.Length);
            codetext = codetext.Substring(0, codetext.Length - after.Length);

            return Convert.ToInt32(codetext);
        }

        public static string GetDurationName(int duration)
        {

            string result = "";

            if (duration == 60)
            {
                result = "o ora";
            }
            else if (duration == 90)
            {
                result = "o ora si 30 de minute";
            }
            else if (duration == 120)
            {
                result = "doua ore";
            }
            else if (duration == 150)
            {
                result = "doua ore si 30 de minute";
            }
            else if (duration == 180)
            {
                result = "trei ore";
            }

            return result;
        }
    }
}