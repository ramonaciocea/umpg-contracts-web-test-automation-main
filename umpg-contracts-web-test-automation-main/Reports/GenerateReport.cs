using System;
using System.Reflection;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using umpg_contracts_web_test_automation_main.BaseActions;


namespace umpg_contracts_web_test_automation_main.Reports
{
    // !!! Maybe generate separate reports | Better Report Logging

    public abstract class GenerateReport
    {
        private static ExtentHtmlReporter _htmlReporter;
        private static ExtentReports _extent;
       
        private static ExtentTest _test;
        private static ExtentTest _featureName;
        private static ExtentTest _scenario;

        public static void InitializeReport()
        {
            try
            {
                _htmlReporter = new ExtentHtmlReporter(AppSettings.GetReportPath());
                _htmlReporter.Config.DocumentTitle = AppSettings.GetReportTitle();
                _htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;

                _extent = new ExtentReports();
                _extent.AttachReporter(_htmlReporter);
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail,"Unable to initialize report: " + ex.Message);
            }

        }

        public static void AddTestToReport(object context, string whereToAdd)
        {
            try
            {
                switch (whereToAdd)
                {
                    case "BeforeFeature":
                        _featureName = _extent.CreateTest<Feature>(((FeatureContext) context).FeatureInfo.Title);
                        break;
                    case "BeforeScenario":
                        _scenario = _featureName.CreateNode<Scenario>(((ScenarioContext) context).ScenarioInfo.Title);
                        //if (!customText.IsNullOrEmpty())
                        //{
                        //    var m = MarkupHelper.CreateCodeBlock(customText);
                        //    var step = Scenario.CreateNode<Given>("Exception");
                        //    step.Fail(m);
                        //}
                        break;
                    default:
                        throw new Exception("Action not implemented");
                }
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, "Unable to add entry to report: " + ex.Message);
            }
        }

        public static void AddStepToReport(IWebDriver Driver, ScenarioContext scenarioContext)
        {
            try
            {
                PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus",
                    BindingFlags.Instance | BindingFlags.Public);
                MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
                object TestResult = getter.Invoke(ScenarioContext.Current, null);

                var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();

                if (scenarioContext.TestError == null)
                {
                    if (stepType.Equals("Given"))
                        _scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text);
                    else if (stepType.Equals("When"))
                        _scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text);
                    else if (stepType.Equals("Then"))
                        _scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text);
                    else if (stepType.Equals("And"))
                        _scenario.CreateNode<And>(scenarioContext.StepContext.StepInfo.Text);
                }
                else if (scenarioContext.TestError != null)
                {
                 //screenshot in the Base64 format in case the Step Is Failed
                 var mediaEntity = Tools.CaptureScreenshotAndReturnModel(Driver, scenarioContext.ScenarioInfo.Title.Trim());

                    if (stepType.Equals("Given"))
                        _scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text)
                            .Fail(scenarioContext.TestError.Message, mediaEntity);
                    else if (stepType.Equals("When"))
                        _scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text)
                            .Fail(scenarioContext.TestError.InnerException, mediaEntity);
                    else if (stepType.Equals("Then"))
                        _scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text)
                            .Fail(scenarioContext.TestError.Message, mediaEntity);
                    else if (stepType.Equals("And"))
                        _scenario.CreateNode<And>(scenarioContext.StepContext.StepInfo.Text)
                            .Fail(scenarioContext.TestError.InnerException, mediaEntity);
                }

                if (TestResult.ToString() == "StepDefinitionPending")
                {
                    if (stepType.Equals("Given"))
                        _scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text)
                            .Skip("Step Definition Pending");
                    else if (stepType.Equals("When"))
                        _scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text)
                            .Skip("Step Definition Pending");
                    else if (stepType.Equals("Then"))
                        _scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text)
                            .Skip("Step Definition Pending");
                    else if (stepType.Equals("And"))
                        _scenario.CreateNode<And>(scenarioContext.StepContext.StepInfo.Text)
                            .Skip("Step Definition Pending");
                }
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, "Unable to add step to report: " + ex.Message);
            }
        }


        public static void FlushReport()
        {
            try
            {
                _extent.Flush();
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, "Unable to Flush the report: " + ex.Message);
            }
        }

    }
}
