using KeywordDrivenFramework.CommonUtilities;
using KeywordDrivenFramework.ReportReader;
using NUnit.Framework;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Base;

namespace TestFramework.TestCases
{
    [TestFixture]
    public class SearchMobileTest:BaseTest
    {
        [Test,TestCaseSource("getData")]
        public void searchMobile(Dictionary<string, string> data)
        {
            ExtentTestManager.CreateParentTest(GetType().Name + '-' + data["Browser"].ToString());
            if (DataUtility.isSkip(xls, Enum.TestCaseName.SearchMobileTest.ToString()) || data["Runmode"].Equals("N"))
            {
                //test.Log(LogStatus.Skip, "Skipping the test as runmode is NO");
                Assert.Ignore("Skipping the test as runmode is NO");
            }
            app = new Keywords();

            //  test.Log(LogStatus.Info, "Starting " + testName);

            app.executeKeywords(Enum.TestCaseName.SearchMobileTest.ToString(), xls, data);
            ExtentManager.Instance.Flush();
        }
        //Data Source
        public static object[] getData()
        {
            return DataUtility.getData(xls, Enum.TestCaseName.SearchMobileTest.ToString());
        }

       
    }
}
