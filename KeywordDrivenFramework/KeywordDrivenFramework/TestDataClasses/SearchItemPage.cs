using AventStack.ExtentReports;
using KeywordDrivenFramework.CommonUtilities;
using OpenQA.Selenium;
//using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeywordDrivenFramework.TestDataClasses
{
    public class SearchItemPage : Keywords
    {
        

        public Enum.LogStatus searchMobileAndSelect(ExcelReader xls, string itemName)
        {

            bool found = false;
            int index = -1;
            IList<IWebElement> mobilesBeforeScroll = null;
            IList<IWebElement> mobilesAfterScroll = null;
            while (!found)
            {
                //for (int rNum = 2; rNum <= xls.getRowCount("Keywords"); rNum++)
                //{
                //string locatorName = xls.getCellData("Keywords", Enum.KeywordsColumn.LocatorName.ToString(), rNum);
                //Dictionary<string, string> locatorData = DataUtility.locatorData(xls, locatorName);
                mobilesBeforeScroll = getElements("xpath", "//a[@class='a-link-normal s-access-detail-page  s-color-twister-title-link a-text-normal']");
                //mobilesBeforeScroll = driver.FindElements(By.XPath("//a[@class='a-link-normal s-access-detail-page  s-color-twister-title-link a-text-normal']"));

                //int y_Last = mobilesBeforeScroll[mobilesBeforeScroll.Count - 1].Location.Y;
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                //js.ExecuteScript("window.scrollTo(0," + y_Last + ")");
                //threadWait(3000);
                //for (int i = 0; i < mobilesBeforeScroll.Count; i++)
                //{
                //    mobilesBeforeScroll = getElements("xpath", "//a[@class='a-link-normal s-access-detail-page  s-color-twister-title-link a-text-normal']");
                //    //int y_Last = mobilesBeforeScroll[mobilesBeforeScroll.Count - 1].Location.Y;
                //    ////y_Last = mobilesAfterScroll[itemName].Location.Y;
                //    //js.ExecuteScript("window.scrollTo(0," + y_Last + ")");

                //    if ((mobilesBeforeScroll[i].Text.Contains(itemName)))
                //    {
                //        //y_Last = itemName.Location.Y;

                //        //found
                //        index = i;
                //        //Console.WriteLine(mobilesBeforeScroll[i].Text);
                //        found = true;
                //    }
                //    //y_Last = mobilesBeforeScroll[mobilesBeforeScroll.Count - 1].Location.Y;
                //    //js.ExecuteScript("window.scrollTo(0," + y_Last + ")");
                //    else
                //    {
                //        int y_Last = mobilesBeforeScroll[mobilesBeforeScroll.Count - 1].Location.Y;
                //        js.ExecuteScript("window.scrollTo(0," + y_Last + ")");
                //        getElement("xpath", "//*[@id='pagnNextString']").Click();
                //        mobilesAfterScroll = getElements("xpath", "//a[@class='a-link-normal s-access-detail-page  s-color-twister-title-link a-text-normal']");
                //        ////found
                //        //index = i;
                //        ////Console.WriteLine(mobilesBeforeScroll[i].Text);
                //        //found = true;
                //    }
                //}
                // getElement("xpath", "//*[@id='pagnNextString']").Click();
                // mobilesAfterScroll = getElements("xpath", "//a[@class='a-link-normal s-access-detail-page  s-color-twister-title-link a-text-normal']");

                //mobilesAfterScroll = driver.FindElements(By.XPath("//a[@class='a-link-normal s-access-detail-page  s-color-twister-title-link a-text-normal']"));
                ////Console.WriteLine(mobilesBeforeScroll.Count);

                ////Console.WriteLine(mobilesAfterScroll.Count);
                //if (mobilesAfterScroll.Count != mobilesBeforeScroll.Count)
                //{
                //    return "FAIL - Product not found";
                //}
                for (int i = 0; i < mobilesBeforeScroll.Count; i++)
                {
                    if (mobilesBeforeScroll[i].Text.Contains(itemName))
                    {
                        //found
                        index = i;
                        //Console.WriteLine(mobilesBeforeScroll[i].Text);
                        found = true;
                    }
                }
                //}
            }
            //found 
            int y = mobilesBeforeScroll[index].Location.Y;
            IJavaScriptExecutor js1 = (IJavaScriptExecutor)driver;

            js1.ExecuteScript("window.scrollTo(0," + (y - 200) + ")");
            mobilesBeforeScroll[index].Click();

            string itemText = driver.FindElement(By.XPath("//span[@id='productTitle']")).Text;
            if (!itemText.StartsWith(itemName))
                return Enum.LogStatus.Failed;

            int cartDimension = getElement("xpath", "//*[@id='add-to-cart-button']").Location.Y;

            js1.ExecuteScript("window.scrollTo(0," + (cartDimension - 200) + ")");
            getElement("xpath", "//*[@id='add-to-cart-button']").Click();

            return Enum.LogStatus.Passed;
        }

        public Enum.LogStatus verifyItemAddedToCart()
        {
            bool actualResult = IsElementVisible("xpath", "//*[@id='huc-v2-order-row-confirm-text']/h1");
            if (actualResult)
                return Enum.LogStatus.Passed;
            else
                return Enum.LogStatus.Failed;
        }

    }
}
