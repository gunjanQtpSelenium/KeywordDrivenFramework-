using KeywordDrivenFramework.CommonUtilities;
//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeywordDrivenFramework.TestDataClasses
{
    public class LoginPage : Keywords
    {
        public void amazonLogin(Dictionary<string, string> testData)
        {
            ClickOnElementWhenElementFound("xpath", "//*[@id='nav-link-accountList']", "a_YourAccount");
            SendKeysForAElement("id", "ap_email", testData["Email"], "txt_Email");
            SendKeysForAElement("id", "ap_password", testData["Password"], "txt_Password");

        }

        public Enum.LogStatus verifyLogin(Dictionary<string, string> testData)
        {
            string actualResult = "";
            string username = getElement("xpath", "//*[@id='nav-link-accountList']/span[1]").Text;
            if (username.Contains(testData["CustomerName"]))
            {
                actualResult = "Success";
            }
            else
                actualResult = "Failure";

            if (!actualResult.Equals(testData["ExpectedResult"]))
                return Enum.LogStatus.Failed;
            else
                return Enum.LogStatus.Passed;
        }
    }
}
