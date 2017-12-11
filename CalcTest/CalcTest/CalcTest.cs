using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using System.Collections;

namespace CalcTest
{
    [TestClass]
    public class CalcTest
    {
        [TestMethod]
        public void TestStringToMatches()
        {
            string inputString = @"(1+2*2,5)/(3-4)+105";
            MatchCollection actualResult = ConsoleCalcC.Program.StringToMatches(inputString);

            Assert.AreEqual(15, actualResult.Count);
            Assert.AreEqual("(", actualResult[0].ToString());
            Assert.AreEqual("1", actualResult[1].ToString());
            Assert.AreEqual("+", actualResult[2].ToString());
            Assert.AreEqual("2", actualResult[3].ToString());
            Assert.AreEqual("*", actualResult[4].ToString());
            Assert.AreEqual("2,5", actualResult[5].ToString());
            Assert.AreEqual(")", actualResult[6].ToString());
            Assert.AreEqual("/", actualResult[7].ToString());
            Assert.AreEqual("(", actualResult[8].ToString());
            Assert.AreEqual("3", actualResult[9].ToString());
            Assert.AreEqual("-", actualResult[10].ToString());
            Assert.AreEqual("4", actualResult[11].ToString());
            Assert.AreEqual(")", actualResult[12].ToString());
            Assert.AreEqual("+", actualResult[13].ToString());
            Assert.AreEqual("105", actualResult[14].ToString());
        }

        [TestMethod]
        public void TestGetOperatorPrioritet_Subtraction()
        {
            string symbol = @"-";
            int expectedResult = 1;
            int actualResult = ConsoleCalcC.Program.GetOperatorPrioritet(symbol);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetOperatorPrioritet_Division()
        {
            string symbol = @"/";
            int expectedResult = 2;
            int actualResult = ConsoleCalcC.Program.GetOperatorPrioritet(symbol);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetOperatorPrioritet_NegativeOperation()
        {
            string symbol = @"E";

            try
            {
                ConsoleCalcC.Program.GetOperatorPrioritet(symbol);
            }
            catch (Exception)
            {
                Assert.Fail("Ошибка исключения для недопустимого символа операции");
                return;
            }

            Assert.Fail("Нет исключения на неправильные исходные данные");
        }

        [TestMethod]
        public void TestBackPolskRec()
        {
            string inputString = @"(8 + 2 * 5) / (1 + 3 * 2,5 - 4)";
            string pattern = @"(\d+\,?\d*)|(\(|\))|(\+|\-|\*|\/)";
            Regex newReg = new Regex(pattern);
            MatchCollection inputMatchCollection = newReg.Matches(inputString);
            ArrayList actualResult = ConsoleCalcC.Program.BackPolskRec(inputMatchCollection);

            var expectedResult = new ArrayList();
            expectedResult.Add("8");
            expectedResult.Add("2");
            expectedResult.Add("5");
            expectedResult.Add("*");
            expectedResult.Add("+");
            expectedResult.Add("1");
            expectedResult.Add("3");
            expectedResult.Add("2,5");
            expectedResult.Add("*");
            expectedResult.Add("+");
            expectedResult.Add("4");
            expectedResult.Add("-");
            expectedResult.Add("/");
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestBackPolskRec_Wrong()
        {
            string inputString = @"(8-(2+2)(";
            string pattern = @"(\d+\,?\d*)|(\(|\))|(\+|\-|\*|\/)";
            Regex newReg = new Regex(pattern);
            MatchCollection inputMatchCollection = newReg.Matches(inputString);

            try
            {
                ConsoleCalcC.Program.BackPolskRec(inputMatchCollection);
            }
            catch (Exception)
            {
                Assert.Fail("Ошибка исключения для недопустимого выражения");
                return;
            }

            Assert.Fail("Нет исключения на неправильные исходные данные");
        }

        [TestMethod]
        public void TestFunctionCalcInClassSymbol()
        {
            var SymbolAddition = new ConsoleCalcC.classSymbol("+", 1, new ConsoleCalcC.OperationAddition());
            float expectedResult = 3;
            float actualResult = SymbolAddition.Calc(1, 2);
           
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetOperatorCalc_OperationAddition()
        {
            string op = @"+";
            float x = 1;
            float y = 2;
            float expectedResult = 3;
            float actualResult = ConsoleCalcC.Program.GetOperatorCalc(op, x, y);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetOperatorCalc_NegativeOperation()
        {
            string op = @"E";
            float x = 1;
            float y = 2;

            try
            {
                ConsoleCalcC.Program.GetOperatorCalc(op, x, y);
            }
            catch (Exception)
            {
                Assert.Fail("Ошибка исключения для недопустимой операции");
                return;
            }

            Assert.Fail("Нет исключения на неправильные исходные данные");
        }

        [TestMethod]
        public void TestCalc()
        {
            var inputArrayList = new ArrayList();
            inputArrayList.Add("8");
            inputArrayList.Add("2");
            inputArrayList.Add("5");
            inputArrayList.Add("*");
            inputArrayList.Add("+");
            inputArrayList.Add("1");
            inputArrayList.Add("3");
            inputArrayList.Add("2,5");
            inputArrayList.Add("*");
            inputArrayList.Add("+");
            inputArrayList.Add("4");
            inputArrayList.Add("-");
            inputArrayList.Add("/");

            float expectedResult = 4;
            float actualResult = ConsoleCalcC.Program.Calc(inputArrayList);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
