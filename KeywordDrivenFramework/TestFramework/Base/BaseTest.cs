using AventStack.ExtentReports;
using KeywordDrivenFramework.CommonUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Base
{
    public class BaseTest
    {
        //public ExtentReports rep = ExtentManager.getInstance();
        public ExtentTest test;
        public Keywords app = null;
        public static ExcelReader xls = new ExcelReader(GeneralMethod.GetExcelPath());
    }
}
