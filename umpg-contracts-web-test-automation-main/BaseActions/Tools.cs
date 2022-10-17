using AventStack.ExtentReports;
using OpenQA.Selenium;


namespace umpg_contracts_web_test_automation_main.BaseActions
{
    public class Tools
    {
        public static MediaEntityModelProvider CaptureScreenshotAndReturnModel(IWebDriver driver, string Name)
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, Name).Build();
        }
    }
}
