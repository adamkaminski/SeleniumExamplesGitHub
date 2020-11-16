using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace SeleniumExamples
{
    [TestClass]
    public class TestContainer
    {
        IWebDriver driver;

        //navigate to the webpage before each test
        [TestInitialize]
        public void visitWebsiteInChrome()
        {
            driver = new ChromeDriver();
            driver.Url = "https://dotnetfiddle.net";
        }

        //test to check if "Hello World" is present in Output field after clicking the Run button after initial load.
        [TestMethod]
        public void Test1()
        {
            string expectedResult = "Hello World";

            //page elements
            var runButton = driver.FindElement(By.Id("run-button"));
            var outputFieldText = driver.FindElement(By.Id("output")).Text;
            var statsLoaderBy = By.Id("stats-loader");

            //click Run button
            runButton.Click();

            //add check to make sure that loader isn't still loading
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(statsLoaderBy));

            //Validate expected result and output string are equal
            Assert.AreEqual(expectedResult, outputFieldText);
        }

        //test will add nUnit 3.12.0 to the nuGet packages of the project
        [TestMethod]
        public void Test2()
        {
            //variables
            var expectedResult = "NUnit";

            //element locators
            var nuGetPackageInput = driver.FindElement(By.CssSelector("input.new-package.form-control.input-sm"));
            var nUnitResultBy = By.LinkText("NUnit");
            var nUnitVersionResultBy = By.LinkText("3.12.0");
            var nUnitAddedResultBy = By.ClassName("package-name");

            //input the name of the package
            nuGetPackageInput.SendKeys(expectedResult);

            //wait for package name result then click
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(nUnitResultBy));
            var nUnitResult = driver.FindElement(nUnitResultBy);
            nUnitResult.Click();

            //wait for version link to appear then click
            wait.Until(ExpectedConditions.ElementToBeClickable(nUnitVersionResultBy));
            var nUnitVersionResult = driver.FindElement(nUnitVersionResultBy);
            nUnitVersionResult.Click();

            //Validate that nUnit has beed added to the project
            var addedPackageNameText = driver.FindElement(nUnitResultBy).Text;
            Assert.AreEqual(expectedResult, addedPackageNameText);
        }

        //close and quit the browser after every test
        [TestCleanup]
        public void closeDriver()
        {
            driver.Quit();
        }
    }
}