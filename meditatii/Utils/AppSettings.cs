using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;

namespace meditatii.web.Utils
{
    public static class AppSettings
    {

        public static bool MobilPayIsLive
        {
            get
            {
                return Setting<string>("MobilPay.IsLive") == "1";
            }
        }

        public static string MobilPaySandboxSignature
        {
            get
            {
                return Setting<string>("MobilPay.SandboxSignature");
            }
        }

        public static string MobilPaySandboxURL
        {
            get
            {
                return Setting<string>("MobilPay.SandboxURL");
            }
        }

        public static string MobilPaySandboxCertFilePath
        {
            get
            {
                return Setting<string>("MobilPay.SandboxCertFilePath");
            }
        }

        public static string MobilPayLiveSignature
        {
            get
            {
                return Setting<string>("MobilPay.LiveSignature");
            }
        }

        public static string MobilPayLiveURL
        {
            get
            {
                return Setting<string>("MobilPay.LiveURL");
            }
        }

        public static string MobilPayLiveCertFilePath
        {
            get
            {
                return Setting<string>("MobilPay.LiveCertFilePath");
            }
        }

        public static string MobilPaySandboxPrivateKeyFileName
        {
            get
            {
                return Setting<string>("MobilPay.SandboxPrivateKeyFileName");
            }
        }

        public static string MobilPayLivePrivateKeyFileName
        {
            get
            {
                return Setting<string>("MobilPay.LivePrivateKeyFileName");
            }
        }

        public static string MobilPayReturnURL
        {
            get
            {
                return Setting<string>("MobilPay.ReturnURL");
            }
        }

        public static string MobilPayConfirmURL
        {
            get
            {
                return Setting<string>("MobilPay.ConfirmURL");
            }
        }

        private static T Setting<T>(string name)
        {
            string value = ConfigurationManager.AppSettings[name];

            if (value == null)
            {
                throw new Exception(String.Format("Could not find setting '{0}',", name));
            }

            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }
    }
}