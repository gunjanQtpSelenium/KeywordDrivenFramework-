using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeywordDrivenFramework.CommonRepository
{
    public class Enum
    {
        public enum BrowserName
        {
            ie,
            firefox,
            chrome,
            opera,
            edge
        }
        public enum LogStatus
        {
            Info,
            Inconclusive,
            Skipped,
            Passed,
            Warning,
            Failed
        }
        public enum LocatorType
        {
            id,
            xpath,
            name,
            classname,
            linktext,
            cssselector
        }
        public enum KeywordsColumn
        {
            TC_Id,
            Keyword,
            Description,
            LocatorName,
            Data,
            RunMode
        }
        public enum TestCaseName
        {
            LoginTest,
            AddToCartTest
        }
        public enum TestCasesColumn
        {
            TC_Id,
            RunMode
        }
        public enum LocatorsColumn
        {
            LocatorType,
            LocatorName,
            LocatorValue
        }

        public enum Sheets
        {
            Keywords,
            TestCases,
            Locators,
            Data
        }
    }
}
