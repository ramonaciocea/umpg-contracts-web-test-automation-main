using System;
using System.IO;
using System.Reflection;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using umpg_contracts_web_test_automation_main.BaseActions;


namespace umpg_contracts_web_test_automation_main.Reports
{
    public class GenerateReport
    {
        private static ExtentHtmlReporter _htmlReporter;
        private static ExtentReports _extent;
       
        private readonly ExtentTest _test;
        [ThreadStatic]
        private static ExtentTest _featureName;
        [ThreadStatic]
        private static ExtentTest _scenario;

        public void InitializeReport()
        {
            try
            {
                var reportPath = Path.Combine(Path.GetDirectoryName(
                    Environment.CurrentDirectory.Substring(0,Environment.CurrentDirectory.IndexOf("bin"))) 
                                              + "\\Reports\\");

                _htmlReporter = new ExtentHtmlReporter(reportPath);
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

        public void AddTestToReport(object context, string whereToAdd)
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

        public void AddStepToReport(IWebDriver Driver, ScenarioContext scenarioContext)
        {
            try
            {
                PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus",
                    BindingFlags.Instance | BindingFlags.Public);
                MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);

                var pendingDefinition = scenarioContext.ScenarioExecutionStatus.ToString();
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
                    var mediaEntity = Tools.CaptureScreenshotAndReturnModel(Driver, scenarioContext.ScenarioInfo.Title.Trim());
                    var stacktrace = scenarioContext.TestError.StackTrace.Replace(Environment.NewLine, "<br>");

                    var fullError = "<br><pre>" + scenarioContext.TestError.Message + "</pre><br>Stack Trace: <br>" + stacktrace + "<br>";

                    if (stepType.Equals("Given"))
                        _scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text)
                            .Fail(fullError, mediaEntity);
                    else if (stepType.Equals("When"))
                        _scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text)
                            .Fail(fullError, mediaEntity);
                    else if (stepType.Equals("Then"))
                        _scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text)
                            .Fail(fullError, mediaEntity);
                    else if (stepType.Equals("And"))
                        _scenario.CreateNode<And>(scenarioContext.StepContext.StepInfo.Text)
                            .Fail(fullError, mediaEntity);
                }

                if (pendingDefinition == "StepDefinitionPending")
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

        public void FlushReport()
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
