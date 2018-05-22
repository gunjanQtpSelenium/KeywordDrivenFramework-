
using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeywordDrivenFramework.CommonRepository
{
    public class GeneralMethod
    {
        public IWebDriver driver;
        public ExtentTest test;
        public GeneralMethod(ExtentTest test)
        {
            this.test = test;
        }

        public string ScenarioName = string.Empty;
        public string TestCaseName = string.Empty;
        public static string browserName = string.Empty;
        Enum.LogStatus status;
        public Enum.LogStatus openBrowser(string bType)
        {
            test.Log(Status.Info, "Opening browser- " + bType);
            try
            {
                if (ConfigurationManager.AppSettings["grid"].Equals("Y"))
                {
                    DesiredCapabilities cap = null;
                    if (bType.Equals("Mozilla"))
                    {
                        cap = DesiredCapabilities.Firefox();
                        cap.SetCapability(CapabilityType.BrowserName, "firefox");

                        cap.SetCapability(CapabilityType.Platform, "WINDOWS");
                    }
                    else if (bType.Equals("Chrome"))
                    {
                        cap = DesiredCapabilities.Chrome();
                        cap.SetCapability(CapabilityType.BrowserName, "chrome");

                        cap.SetCapability(CapabilityType.Platform, "WINDOWS");
                    }
                    driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), cap);

                }
                else
                {
                    if (bType.Equals(Enum.BrowserName.firefox))
                        driver = new FirefoxDriver();
                    else if (bType.Equals(Enum.BrowserName.chrome.ToString()))
                        driver = new ChromeDriver(GetDriversPath());
                    else if (bType.Equals(Enum.BrowserName.ie))
                        driver = new InternetExplorerDriver(GetDriversPath());
                }
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.Manage().Window.Maximize();
                return Enum.LogStatus.Passed;
            }
            catch (Exception)
            {
                return Enum.LogStatus.Failed;
            }
        }
        public Enum.LogStatus navigate()
        {
            try
            {
                test.Log(Status.Info, "Navigating to - " + ConfigurationManager.AppSettings["URL"]);
                driver.Url = ConfigurationManager.AppSettings["URL"];
                return Enum.LogStatus.Passed;
            }
            catch (Exception)
            {
                return Enum.LogStatus.Failed;
            }

        }

        public IWebElement getElement(string locatorKey, string locatorValue)
        {
            IWebElement e = null;
            try
            {
                if (locatorKey == Enum.LocatorType.id.ToString())
                    e = driver.FindElement(By.Id(locatorValue));
                else if (locatorKey == Enum.LocatorType.xpath.ToString())
                    e = driver.FindElement(By.XPath(locatorValue));
                else if (locatorKey == Enum.LocatorType.name.ToString())
                    e = driver.FindElement(By.Name(locatorValue));
            }
            catch (Exception)
            {
                //   reportFailure("Failure in element extraction ");
                Assert.Fail("Failure in element extraction " + locatorValue);
            }
            return e;
        }

        public void maximiseBrowser()
        {
            driver.Manage().Window.Maximize();
        }
        public void explicitWait(By aStringElement, int aWaitTimeInSec)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(aWaitTimeInSec));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(aStringElement));
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void threadWait(int aWaitTimeInSec)
        {
            Thread.Sleep(aWaitTimeInSec);
        }
        public bool WaitUntillElementIsVisible(String aStringType, String aStringeValue, int timeOut)
        {
            int iTimer = 0;
            while (iTimer <= timeOut)
            {
                bool status = IsElementVisible(aStringType, aStringeValue);
                if (status)
                    return true;
            }
            return false;
        }
        public void browserBackNavigation()
        {
            driver.Navigate().Back();
        }
        public void browserForwardNavigation()
        {
            driver.Navigate().Forward();
        }
        public String getAttributeOfWebelementByLocator(By aStringValue, String aAttribute)
        {
            explicitWait(aStringValue, 300);
            IWebElement element = driver.FindElement(aStringValue);
            return element.GetAttribute(aAttribute);
        }
        public IWebElement getWebElementByLocator(String aStringType, String aStringValue)
        {
            IWebElement webElement = getElement(aStringType, aStringValue);
            return webElement;
        }
        public string getTextOfWebElementByLocator(String aStringType, String aWebElementID)
        {
            return getWebElementByLocator(aStringType, aWebElementID).Text;
        }

        public Enum.LogStatus ClickOnElementWhenElementFound(String aStringType, String aStringValue, String aStringName)
        {
            test.Log(Status.Info, "Clicking on -" + aStringName);
            try
            {
                IWebElement webElement = getWebElementByLocator(aStringType, aStringValue);
                webElement.Click();
                return status = Enum.LogStatus.Passed;
            }
            catch (Exception)
            {
                return status = Enum.LogStatus.Failed;
            }
        }
        public Enum.LogStatus SendKeysForElement(String aStringType, String aStringValue, String aTestData,String aStringName)
        {
            test.Log(Status.Info, "Entering into - " + aStringName);
            try
            {
                getElement(aStringType, aStringValue).SendKeys(aTestData);
                return status = Enum.LogStatus.Passed;
            }
            catch (ElementNotVisibleException e)
            {
                Console.WriteLine(e.ToString());
                return status = Enum.LogStatus.Failed;
            }
            catch (Exception)
            {
                return status = Enum.LogStatus.Failed;
            }
        }
        public Enum.LogStatus SendKeysForWebElement(String aStringType, String aStringValue, String aTestData,String aStringName)
        {
            test.Log(Status.Info, "Entering into - " + aStringName);
            try
            {
                getElement(aStringType, aStringValue).Clear();
                getElement(aStringType, aStringValue).SendKeys(aTestData);
                return status = Enum.LogStatus.Passed;
            }
            catch (ElementNotVisibleException e)
            {
                Console.WriteLine(e.ToString());
                return status = Enum.LogStatus.Warning;
            }
            catch (Exception)
            {
                return status = Enum.LogStatus.Failed;
            }
        }
        public bool IsElementVisible(String aStringType, String aStringValue)
        {
            try
            {
                IWebElement element = getElement(aStringType, aStringValue);
                if (element.Displayed)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public bool waitUntilElementNotVisible(String aStringType, String aStringeValue, int timeOut)
        {

            int iTimer = 0;
            while (iTimer <= timeOut)
            {
                bool status = !(IsElementVisible(aStringType, aStringeValue));
                if (status)
                    return true;
            }
            return false;
        }
        public bool IsElementPresent(String aStringType, String aStringValue)
        {
            try
            {
                getElement(aStringType, aStringValue);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public String selectValueFromDropdown(String aStringValue, string value, String aStringType)
        {
            try
            {
                SelectElement oSelect = new SelectElement(getElement(aStringType, aStringValue));
                oSelect.SelectByText(value);
                return value;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
        public void mouseHover(String aStringType, String aStringValue)
        {
            Actions act = new Actions(driver);
            act.MoveToElement(getElement(aStringType, aStringValue)).Click();
        }
        public string selectValueFromDropdownStringText(String aStringType, String aStringValue, string value)
        {
            try
            {
                SelectElement oSelect = new SelectElement(getElement(aStringType, aStringValue));
                oSelect.SelectByText(value);
                return value;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
        public int selectValueStringIndex(String aStringType, String aStringValue, int index)
        {
            try
            {
                SelectElement oSelect = new SelectElement(getElement(aStringType, aStringValue));
                oSelect.SelectByIndex(index);
                return index;
            }
            catch (NoSuchElementException)
            {
                return -1;
            }
        }
        public Enum.LogStatus AssertAreEqual(String aStringType, String expected, string actual)
        {
            try
            {
                Assert.AreEqual(getTextOfWebElementByLocator(aStringType, expected), actual);
                return status = Enum.LogStatus.Passed;
            }
            catch (Exception)
            {
                return status = Enum.LogStatus.Failed;
            }
        }
        public Enum.LogStatus AssertIsTrue(String type, String eWebElement)
        {
            try
            {
                Assert.IsTrue(IsElementVisible(type, eWebElement));
                return status = Enum.LogStatus.Passed;
            }
            catch (Exception)
            {
                return status = Enum.LogStatus.Failed;
            }
        }
        public Enum.LogStatus AssertIsFalse(String type, String eWebElement)
        {
            try
            {
                Assert.IsFalse(IsElementVisible(type, eWebElement));
                return status = Enum.LogStatus.Passed;
            }
            catch (Exception)
            {
                return status = Enum.LogStatus.Failed;
            }
        }

        public static Enum.LogStatus AssertIgnore()
        {
            try
            {
                Assert.Ignore("Skipping the test");
                return Enum.LogStatus.Skipped;
            }
            catch (Exception)
            {
                return Enum.LogStatus.Skipped;
            }
        }
        public void jsClick(String aStringValue)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", aStringValue);
        }

        /// <summary>
        /// Desc:Method is used to capture the ScreenShots
        /// </summary>
        /// <returns></returns>
        public string ScreenShotCapture()
        {
            try
            {
                string filename = DateTime.Now.ToString().Replace("/", "_").Replace("-", "_").Replace(":", "_").Replace(" ", "_") + ".jpeg";
                string finalpath = @".\TestFramework\ResultReport\Screenshots\" + filename;
                ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
                Screenshot screenshot = screenshotDriver.GetScreenshot();
                screenshot.SaveAsFile(finalpath, ScreenshotImageFormat.Jpeg);
                finalpath = "Screenshots//" + filename;
                return finalpath;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Desc:Method is used to GetDrivers path
        /// </summary>
        /// <returns></returns>
        public string GetDriversPath()
        {
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string driverPath = new Uri(actualPath).LocalPath;
            driverPath = driverPath + "Driver";
            return driverPath;
        }
        /// <summary>
        /// Desc:Method is used to get generated report's path
        /// </summary>
        /// <returns></returns>
        public static string GetReportPath()
        {
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string reportPath = new Uri(actualPath).LocalPath;
            reportPath = reportPath + "ResultReport\\ExtentReport.html";
            return reportPath;
        }
        /// <summary>
        /// Desc:Method is used to Get Screenshot's Path
        /// </summary>
        /// <returns></returns>
        public static string GetScreenshotPath()
        {
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string screenshotPath = new Uri(actualPath).LocalPath;
            screenshotPath = screenshotPath + @"ResultReport\Screenshots";
            return screenshotPath;
        }
        /// <summary>
        /// Desc:Method is used to Get Report Folder's Path
        /// </summary>
        /// <returns></returns>
        public static string GetReportFolderPath()
        {
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string reportPath = new Uri(actualPath).LocalPath;
            reportPath = reportPath + "ResultReport";
            return reportPath;
        }
        /// <summary>
        /// Desc:Method is used to Get zip Folder's Path
        /// </summary>
        /// <returns></returns>
        public static string GetZipFolderPath()
        {
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string reportPath = new Uri(actualPath).LocalPath;
            reportPath = reportPath + "ZipFolder";
            return reportPath;
        }
        /// <summary>
        /// Desc:Method is used to set report into zip file
        /// </summary>
        /// <returns></returns>
        public static void createZipFile()
        {
            try
            {
                string reportPath = GetReportFolderPath();
                string zipFilePath = GetZipFolderPath();
                bool exists = System.IO.Directory.Exists(reportPath);
                if (!exists)
                    System.IO.Directory.CreateDirectory(reportPath);
                addIntoZip(reportPath, zipFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static void addIntoZip(string directoryPath, string zipFilePath)
        {
            DirectoryInfo dirZipPath = new DirectoryInfo(zipFilePath);
            foreach (FileInfo fi in dirZipPath.GetFiles())
            {
                fi.Delete();
            }
            ZipFile.CreateFromDirectory(directoryPath, Path.Combine(zipFilePath, "ExtentReport.zip"), CompressionLevel.Optimal, true);
        }
        /// <summary>
        /// Desc:Method is used to get excelsheet's path
        /// </summary>
        /// <returns></returns>
        public static string GetExcelPath()
        {
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string excelPath = new Uri(actualPath).LocalPath;
            excelPath = excelPath + "ExcelSheet\\TestData.xlsx";
            return excelPath;
        }

        /// <summary>
        /// Desc:Method is used to Open New tab
        /// </summary>
        /// <returns></returns>
        public void MethodtToOpenNewtab()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Navigate().GoToUrl("https://google.com");
        }
    }
}
