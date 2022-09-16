using System;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using umpg_contracts_web_test_automation_main.Elements;
using umpg_contracts_web_test_automation_main.Pages;

namespace umpg_contracts_web_test_automation_main.StepDefinitions
{
    [Binding]
    public sealed class HomeStepsDefinitions
    {
        private IWebDriver _driver;
        HomePage _homePage;

        public HomeStepsDefinitions(IWebDriver driver)
        {
            _driver = driver;
             _homePage = new HomePage(_driver);
        }

        [Given("I navigate to application")]
        public void GivenINavigateToApplication()
        {
            _driver.Navigate().GoToUrl(AppSettings.GetStartingUrl());
        }

        [Given("the first number is (.*)")]
        public void GivenTheFirstNumberIs(int number)
        {
            _homePage.ClickMe(HomePageLocators.YTAcceptAllElement);
        }

        [Given("the second number is (.*)")]
        public void GivenTheSecondNumberIs(int number)
        {
            _homePage.ClickMe(HomePageLocators.YTElement);
        }

        [Given("the third number is (.*)")]
        public void GivenTheThirdNumberIs(int number)
        {
            ScenarioContext.StepIsPending();
        }

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            Console.WriteLine("Step 5");
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(int result)
        {

            Console.WriteLine("Step 5");
           
            Assert.Less(result,2,"Not Equal");

        }
    }
}