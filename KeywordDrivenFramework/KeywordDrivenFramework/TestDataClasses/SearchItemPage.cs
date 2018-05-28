using AventStack.ExtentReports;
using KeywordDrivenFramework.CommonUtilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeywordDrivenFramework.TestDataClasses
{
    public class SearchItemPage : Keywords
    {
        public SearchItemPage(ExtentTest test) : base(test)
        {

        }
        public string searchMobileAndSelect(string itemName)
        {
            bool found = false;
            int index = -1;
            IList<IWebElement> mobilesBeforeScroll = null;
            IList<IWebElement> mobilesAfterScroll = null;
            while (!found)
            {
                mobilesBeforeScroll = driver.FindElements(By.XPath("//a[@class='a-link-normal s-access-detail-page  s-color-twister-title-link a-text-normal']"));
                Console.WriteLine(mobilesBeforeScroll.Count);
                int y_Last = mobilesBeforeScroll[mobilesBeforeScroll.Count - 1].Location.Y;
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("window.scrollTo(0," + y_Last + ")");
                threadWait(3000);
                mobilesAfterScroll = driver.FindElements(By.XPath("//a[@class='a-link-normal s-access-detail-page  s-color-twister-title-link a-text-normal']"));
                Console.WriteLine(mobilesBeforeScroll.Count);

                Console.WriteLine(mobilesAfterScroll.Count);
                if (mobilesAfterScroll.Count == mobilesBeforeScroll.Count)
                {
                    return "FAIL - Product not found";
                }
                for (int i = 0; i < mobilesBeforeScroll.Count; i++)
                {
                    if (mobilesBeforeScroll[i].Text.StartsWith(itemName))
                    {
                        //found
                        index = i;
                        Console.WriteLine(mobilesBeforeScroll[i].Text);
                        found = true;
                    }
                }
            }
            //found 
            int y = mobilesBeforeScroll[index].Location.Y;
            IJavaScriptExecutor js1 = (IJavaScriptExecutor)driver;

            js1.ExecuteScript("window.scrollTo(0," + (y - 200) + ")");
            mobilesBeforeScroll[index].Click();
            //string itemText = driver.FindElements(By.XPath("//h2[@class='a-size-medium s-inline  s-access-title  a-text-normal']")).Text;
            //if (!itemText.StartsWith(itemName))
            //    return "FAIL - Item name did not match " + itemText;

            //int cartDimension = driver.FindElement(By.XPath(ConfigurationManager.AppSettings["addToCart_xpath"])).Location.Y;

            //js1.ExecuteScript("window.scrollTo(0," + (cartDimension - 200) + ")");
           // getElement("addToCart_xpath").Click();

            return "";
        }

    }
}
