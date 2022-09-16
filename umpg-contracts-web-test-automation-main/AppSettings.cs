using System;
using Microsoft.Extensions.Configuration;

namespace umpg_contracts_web_test_automation_main
{
    public static class AppSettings
    {
        private static IConfiguration _config;

        public static void GetSettings()
        {
             _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        //Selenium
        public static string? GetBrowserName() => _config.GetSection("Selenium:BrowserName").Value;
        public static string? GetStartingUrl() => _config.GetSection("Selenium:StartingUrl").Value;
        public static string? GetBrowserMode() => _config.GetSection("Selenium:BrowserMode").Value;
        public static TimeSpan WaitingTimeoutInSeconds =>
            new TimeSpan(0, 0, int.Parse(_config.GetSection("Selenium:WaitingTimeoutInSeconds").Value));

        //Reports
        public static string? GetReportPath() => _config.GetSection("Report:ReportPath").Value;
        public static string? GetReportTitle() => _config.GetSection("Report:ReportTitle").Value;
    }
}
