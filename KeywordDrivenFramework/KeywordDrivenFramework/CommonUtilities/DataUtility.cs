using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeywordDrivenFramework.CommonUtilities
{
    public class DataUtility
    {
        /// <summary>
        /// Desc:Method is used to get excel data from Data Sheet
        /// </summary>
        /// <param name="xls"></param>
        /// <param name="testName"></param>
        /// <returns></returns>
        public static object[] getData(ExcelReader xls, string testName)
        {
            //reads data for only testCaseName

            string sheetName = Enum.Sheets.Data.ToString();

            int testStartRowNum = 1;

            while (!xls.getCellData(sheetName, 0, testStartRowNum).Equals(testName))
            {
                testStartRowNum++;
            }
            int colStartRowNum = 1 + testStartRowNum;
            int dataStartRowNum = 2 + testStartRowNum;

            //calculate rows of data
            int rows = 0;
            while (!xls.getCellData(sheetName, 0, dataStartRowNum + rows).Equals(""))
            {
                rows++;
            }

            //calculate total cols of data
            int cols = 0;
            while (!xls.getCellData(sheetName, cols, colStartRowNum).Equals(""))
            {
                cols++;
            }
            Console.WriteLine("Total cols :" + cols);

            //read the data

            object[][] data = new object[rows][];
            int dataRow = 0;
            Dictionary<string, string> table = null;

            for (int rNum = dataStartRowNum; rNum < dataStartRowNum + rows; rNum++)
            {
                data[rNum - dataStartRowNum] = new object[1];
                table = new Dictionary<string, string>();
                for (int cNum = 0; cNum < cols; cNum++)
                {
                    string key = xls.getCellData(sheetName, cNum, colStartRowNum);
                    string value = xls.getCellData(sheetName, cNum, rNum);
                    table.Add(key, value);
                }
                data[dataRow][0] = table;
                dataRow++;
            }
            return data;
        }

        /// <summary>
        ///Desc:Method is used to skip the testcase
        /// </summary>
        /// <param name="xls"></param>
        /// <param name="testName"></param>
        /// <returns></returns>
        public static bool isSkip(ExcelReader xls, string testName)
        {
            string TestCaseSheet = Enum.Sheets.TestCases.ToString();
            int rows = xls.getRowCount(TestCaseSheet);
            for (int rNum = 2; rNum <= rows; rNum++)
            {
                string tcid = xls.getCellData(TestCaseSheet, Enum.TestCasesColumn.TC_Id.ToString(), rNum);
                if (tcid.Equals(testName))
                {
                    string runmode = xls.getCellData(TestCaseSheet, Enum.TestCasesColumn.RunMode.ToString(), rNum);
                    if (runmode.Equals("Y"))
                        return false;
                    else
                        return true;
                }
            }
            return true;
        }

        /// <summary>
        /// Desc:Method is used to get locators data
        /// </summary>
        /// <param name="xls"></param>
        /// <param name="locatorName"></param>
        /// <returns></returns>
        public static Dictionary<string, string> locatorData(ExcelReader xls, string locatorName)
        {
            string LocatorSheet = Enum.Sheets.Locators.ToString();
            int rows = xls.getRowCount(LocatorSheet);
            Dictionary<string, string> table = new Dictionary<string, string>();
            for (int rNum = 2; rNum <= rows; rNum++)
            {
                string xcelLocatorName = xls.getCellData(LocatorSheet, Enum.LocatorsColumn.LocatorName.ToString(), rNum);
                if (xcelLocatorName.Equals(locatorName))
                {
                    string locatorValueKey = xls.getCellData(LocatorSheet, Enum.LocatorsColumn.LocatorType.ToString(), rNum);
                    string locatorValue = xls.getCellData(LocatorSheet, Enum.LocatorsColumn.LocatorValue.ToString(), rNum);
                    table.Add(locatorValueKey, locatorValue);
                }
                return table;
            }
            return table;
        }
    }
}
