using OpenQA.Selenium;

namespace umpg_contracts_web_test_automation_main.Elements
{
    public static class HomePageLocators
    {
        //Draft New Contract Panel
        public static By DraftNewPageElement = By.XPath("//span[contains(text(),'Draft New Contract')]");
        public static By YTElement = By.XPath("//yt-formatted-string[contains(text(),'Explore')]");
        // public static By YTAcceptAllElement = By.XPath("//yt-formatted-string[contains(text(),'Accept all') and contains(@class,'ytd-button-renderer')]");
        public static By YTAcceptAllElement = By.XPath("//button[contains(@aria-label,'Accept')]");
    }
}
