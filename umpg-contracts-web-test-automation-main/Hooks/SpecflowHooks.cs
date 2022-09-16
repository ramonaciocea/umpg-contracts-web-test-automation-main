
using System;
using BoDi;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using umpg_contracts_web_test_automation_main.Reports;
using umpg_contracts_web_test_automation_main.WebDriverFactory;



namespace umpg_contracts_web_test_automation_main.Hooks
{
    [Binding]
    public sealed class SpecflowHooks
    {
        private static IWebDriver _driver;
        private readonly IObjectContainer _objectContainer;

        public SpecflowHooks(IObjectContainer objectContainer) => _objectContainer = objectContainer;
        

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            try
            {
                AppSettings.GetSettings();
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to set up test run parameters from appsettings.json");
            }

            GenerateReport.InitializeReport();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            GenerateReport.AddTestToReport(featureContext,"BeforeFeature");
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            GenerateReport.AddTestToReport(scenarioContext, "BeforeScenario");

            _driver = new WebDriverAutomation().GetWebDriver();
            _objectContainer.RegisterInstanceAs(_driver, typeof(IWebDriver));
            
        }

        [AfterStep]
        public static void AfterStep(ScenarioContext scenarioContext)
        {
            GenerateReport.AddStepToReport(_driver, scenarioContext);
        }


        [AfterScenario]
        public void AfterScenario()
        {
             _driver?.Quit();
        }


        [AfterTestRun]
        public static void AfterTestRun()
        {
            GenerateReport.FlushReport();
        }

    }
}
