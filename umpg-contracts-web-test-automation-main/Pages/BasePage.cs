using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using AventStack.ExtentReports;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow;

namespace umpg_contracts_web_test_automation_main.Pages
{
    [Binding]
    public class BasePage
    {
        protected readonly IWebDriver Driver;
        protected readonly WebDriverWait Wait;
        protected readonly DefaultWait<IWebDriver> FluentWait;
        protected Actions Actions => new Actions(Driver);


        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, AppSettings.WaitingTimeoutInSeconds);

            FluentWait = new DefaultWait<IWebDriver>(driver)
            {
                Timeout = TimeSpan.FromSeconds(10),
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            FluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            FluentWait.IgnoreExceptionTypes(typeof(TimeoutException));
        }
        
        public void WaitUntilPageIsReady()
        {
            var javaScriptExecutor = Driver as IJavaScriptExecutor;
            var wait = new WebDriverWait(Driver, AppSettings.WaitingTimeoutInSeconds);

            try
            {
                Func<IWebDriver, bool> readyCondition = webDriver => (bool)javaScriptExecutor.ExecuteScript("return (document.readyState == 'complete')");
                wait.Until(readyCondition);
            }
            catch (InvalidOperationException)
            {
                wait.Until(wd => javaScriptExecutor.ExecuteScript("return document.readyState").ToString() == "complete");
            }
        }

        public void DragAndDrop(IWebElement fromElem, IWebElement toElem, int destinationOffsetX = 0, int destinationOffsetY = 0)
        {
            var action = Actions.ClickAndHold(fromElem)
                .MoveToElement(toElem)
                .MoveByOffset(destinationOffsetX, destinationOffsetY)
                .Build();
            //added action twice in order for the UI to detect mouse moves
            action.Perform();
            action.Perform();
            var action2 = Actions.Release().Build();
            action2.Perform();
            Thread.Sleep(300);
        }

        protected IWebElement WaitUntilVisible(By elementSpecifier)
        {
            var element = Wait.Until<IWebElement>(Driver =>
            {
                try
                {
                    var elementToBeDisplayed = Driver.FindElement(elementSpecifier);
                    if (elementToBeDisplayed.Displayed)
                    {
                        return elementToBeDisplayed;
                    }
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });
            return element;
        }
    }

}
