﻿using KeywordDrivenFramework.CommonUtilities;
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
    public class RegistrationTest : BaseTest
    {
        [Test, TestCaseSource("getData")]
        public void RegistrationTestScenario(Dictionary<string, string> data)
        {
            ExtentTestManager.CreateParentTest(GetType().Name + '-' + data["Browser"].ToString());
            try
            {
                if (DataUtility.isSkip(xls, Enum.TestCaseName.RegistrationTest.ToString()) || data["Runmode"].Equals("N"))
                {
                    Assert.Ignore("Skipping the test as runmode is NO");
                }
                app = new Keywords();
                app.executeKeywords(Enum.TestCaseName.RegistrationTest.ToString(), xls, data);
                ExtentManager.Instance.Flush();
            }
            catch (System.Exception)
            {
                Assert.Fail("");
            }
        }

        /// <summary>
        /// Desc: Method is used to get data from the excel sheet
        /// </summary>
        /// <returns></returns>
        public static object[] getData()
        {
            return DataUtility.getData(xls, Enum.TestCaseName.RegistrationTest.ToString());
        }
    }
}
