using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace umpg_contracts_web_test_automation_main.StepDefinitions
{
    [Binding]
    public class BaseStepsDefinitions
    {
        private IWebDriver _driver;
        public WebDriverWait _webDriverWait { get; set; }
        public IJavaScriptExecutor Js;

        public BaseStepsDefinitions(IWebDriver driver)
        {
            _driver = driver;
            _webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
        }

        public void ScrollIntoView(IWebElement WebElement)
        {
            try
            {
                Js.ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth', block: 'center'})", WebElement);
            }
            catch (Exception)
            {
                Console.WriteLine($"Unable to scroll to element.");
                
            }
        }

        public void HoverElement(IWebElement WebElement)
        {
            try
            {
                OpenQA.Selenium.Interactions.Actions action =
                    new OpenQA.Selenium.Interactions.Actions(_driver);
                action.MoveToElement(WebElement).Perform();
            }
            catch (Exception)
            {
                Console.WriteLine($"Unable to find and hover element.");
            }
        }

    }
}
