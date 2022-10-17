using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace umpg_contracts_web_test_automation_main.BaseActions
{
    [Binding]
    public class BaseElementAction
    {
        protected readonly IWebDriver Driver;
        protected Actions Actions => new Actions(Driver);


        public BaseElementAction(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}
