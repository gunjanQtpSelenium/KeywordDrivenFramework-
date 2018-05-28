using AventStack.ExtentReports;
using KeywordDrivenFramework.ReportReader;
using KeywordDrivenFramework.TestDataClasses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeywordDrivenFramework.CommonUtilities
{
    public class Keywords : GeneralMethod
    {
        //public Keywords(ExtentTest test) : base(test)
        //{

        //}

        /// <summary>
        /// Desc:Method is used to read excel sheets and execute the keywords accordingly
        /// </summary>
        /// <param name="testUnderExecution"></param>
        /// <param name="xls"></param>
        /// <param name="testData"></param>
        public void executeKeywords(string testUnderExecution, ExcelReader xls, Dictionary<string, string> testData)
        {
            SearchItemPage sPage = new SearchItemPage();
            LoginPage lPgae = new LoginPage();
            string KeywordsSheet = Enum.Sheets.Keywords.ToString();
            int rows = xls.getRowCount(KeywordsSheet);
            // app.reportFailure("xxxxx");
            for (int rNum = 2; rNum <= rows; rNum++)
            {
                string tcid = xls.getCellData(KeywordsSheet, Enum.KeywordsColumn.TC_Id.ToString(), rNum);
                if (tcid.Equals(testUnderExecution))
                {
                    string data = null;
                    string keyword = xls.getCellData(KeywordsSheet, Enum.KeywordsColumn.Keyword.ToString(), rNum);
                    string locatorName = xls.getCellData(KeywordsSheet, Enum.KeywordsColumn.LocatorName.ToString(), rNum);
                    string key = xls.getCellData(KeywordsSheet, Enum.KeywordsColumn.Data.ToString(), rNum);
                    string runMode = xls.getCellData(KeywordsSheet, Enum.KeywordsColumn.RunMode.ToString(), rNum);
                    Dictionary<string, string> locatorData = DataUtility.locatorData(xls, locatorName);

                    if (!key.Equals(""))
                    {
                        data = testData[key];
                    }
                    // test.Log(LogStatus.Info, tcid + " -- " + keyword + " -- " + objct + " -- " + data);
                    Enum.LogStatus resultStatus = Enum.LogStatus.Passed;

                    switch (keyword)
                    {
                        case "openbrowser":
                            resultStatus = openBrowser(data);
                            break;
                        case "navigate":
                            resultStatus = navigate();
                            break;
                        case "click":
                            resultStatus = ClickOnElementWhenElementFound(locatorData.Keys.FirstOrDefault(), locatorData.Values.FirstOrDefault(), locatorName);
                            break;
                        case "input":
                            resultStatus = SendKeysForElement(locatorData.Keys.FirstOrDefault(), locatorData.Values.FirstOrDefault(), data, locatorName);
                            break;
                        case "wait":
                            resultStatus = threadWait(5000);
                            break;
                        case "hover":
                            resultStatus = mouseHoverWithoutClick(locatorData.Keys.FirstOrDefault(), locatorData.Values.FirstOrDefault(), locatorName);
                            break;
                        case "hoverClick":
                            resultStatus = mouseHover(locatorData.Keys.FirstOrDefault(), locatorData.Values.FirstOrDefault());
                            break;
                        case "jsClick":
                            resultStatus = jsClick(locatorData.Keys.FirstOrDefault(), locatorData.Values.FirstOrDefault());
                            break;
                        case "inputAndEnter":
                            resultStatus = SendKeysForAElement(locatorData.Keys.FirstOrDefault(), locatorData.Values.FirstOrDefault(), data, locatorName);
                            break;
                        case "searchMobileAndSelect":
                            sPage.searchMobileAndSelect(xls, data);
                            break;
                        case "amazonLogin":
                            lPgae.amazonLogin(testData);
                            break;
                        case "verifyLogin":
                            lPgae.verifyLogin(testData);
                            break;
                        case "verifyItemAddedToCart":
                            sPage.verifyItemAddedToCart();
                            break;

                    }
                    if (!resultStatus.Equals(Enum.LogStatus.Passed))
                    {
                        Assert.Fail(resultStatus.ToString());
                    }
                }
            }
        }
    }
}
