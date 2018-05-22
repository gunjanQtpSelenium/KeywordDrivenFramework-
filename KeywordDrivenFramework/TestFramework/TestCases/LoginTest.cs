using KeywordDrivenFramework.CommonRepository;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Base;

namespace TestFramework.TestCases
{
    [TestFixture]
    public class LoginTest : BaseTest
    {

        [Test, TestCaseSource("getData")]
        public void LoginTestScenario(Dictionary<string, string> data)
        {
            //test = rep.StartTest("FilterMobile Test");
            if (DataUtility.isSkip(xls, Enum.TestCaseName.LoginTest.ToString()) || data["Runmode"].Equals("N"))
            {
                //test.Log(LogStatus.Skip, "Skipping the test as runmode is NO");
                Assert.Ignore("Skipping the test as runmode is NO");
            }
             app = new Keywords(test);

            //  test.Log(LogStatus.Info, "Starting " + testName);

            app.executeKeywords(Enum.TestCaseName.LoginTest.ToString(), xls, data);
            //   test.Log(LogStatus.Info, "Executing keywords");
            // add screenshot
            //app.getGenericKeywords().reportFailure("xxxxxxxx");

            //  test.Log(LogStatus.Pass, "Test Passed");
            //   app.getGenericKeywords().takeScreenshot();
        }

        //Data Source
        public static object[] getData()
        {
            return DataUtility.getData(xls, Enum.TestCaseName.LoginTest.ToString());
        }
    }
}
