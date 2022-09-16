using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace umpg_contracts_web_test_automation_main.WebDriverFactory
{
    internal class WebDriverAutomation
    {
        private ChromeOptions? _chromeOptions;
        private FirefoxOptions? _firefoxOptions;


        public IWebDriver GetWebDriver()
        {
            try
            {
                switch (AppSettings.GetBrowserName())
                {
                    case "chrome":
                        return getChromeDriver();
                    case "firefox": 
                        return getFirefoxDriver();
                    default: 
                        throw new NotSupportedException("not supported browser: <null>");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to Initialize the driver for browser :" + AppSettings.GetBrowserName() + ex.Message);
              throw;
            }
        }

        private IWebDriver getChromeDriver()
        {
            _chromeOptions = new ChromeOptions();
            _chromeOptions.AddArguments("start-maximized");
            _chromeOptions.AddArgument("no-sandbox");
            _chromeOptions.AddArguments("--disable-gpu");
            _chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");

            if (AppSettings.GetBrowserMode().Equals("headless"))
                _chromeOptions.AddArguments("--headless");

            new DriverManager().SetUpDriver(new ChromeConfig());
          //  return new ChromeDriver(_chromeOptions) { Url = AppSettings.GetStartingUrl()};
          return new ChromeDriver(_chromeOptions);

        }

        private IWebDriver getFirefoxDriver()
        {
            new DriverManager().SetUpDriver(new FirefoxConfig());
            return new FirefoxDriver {Url = AppSettings.GetStartingUrl()};
        }
    }
}
