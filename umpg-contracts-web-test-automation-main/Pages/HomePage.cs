using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using umpg_contracts_web_test_automation_main.Elements;


namespace umpg_contracts_web_test_automation_main.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public void ClickMe(By Element)
        {
            WaitUntilPageIsReady();
            WaitUntilVisible(Element).Click();
            Thread.Sleep(5000);
        }

    }
}
