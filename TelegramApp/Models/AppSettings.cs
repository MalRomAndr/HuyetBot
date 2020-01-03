using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelegramApp.Models
{
    public static class AppSettings
    {
        public static string Url { get; set; }  = "https://telegramapp.azurewebsites.net:443/{0}";

		public static string Name { get; set; } = "HuyetBot";

        public static string Key { get; set; }  = "000000000000000000000000000000000";
    }
}