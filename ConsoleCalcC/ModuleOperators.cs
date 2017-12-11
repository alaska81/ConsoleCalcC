using System.Collections;

namespace ConsoleCalcC
{
    class ModuleOperators
    {
        public static string pattern()
        {
            return @"(\d+\,?\d*)|(\(|\))|(\+|\-|\*|\/)";
        }

        public static ArrayList Operators()
        {
            ArrayList operatorsArray = new ArrayList();
            operatorsArray.Add(new classSymbol("(", 0));
            operatorsArray.Add(new classSymbol(")", 0));
            operatorsArray.Add(new classSymbol("+", 1, new OperationAddition()));
            operatorsArray.Add(new classSymbol("-", 1, new OperationSubtraction()));
            operatorsArray.Add(new classSymbol("*", 2, new OperationMultiplication()));
            operatorsArray.Add(new classSymbol("/", 2, new OperationDivision()));

            return operatorsArray;
        }

    }

    public abstract class BaseOperation
    {
        public abstract float Result(float x, float y);

    }

    public class OperationAddition : BaseOperation
    {
        public override float Result(float x, float y)
        {
            return x + y;
        }
    }

    public class OperationSubtraction : BaseOperation
    {
        public override float Result(float x, float y)
        {
            return y - x;
        }
    }

    public class OperationMultiplication : BaseOperation
    {
        public override float Result(float x, float y)
        {
            return x * y;
        }
    }

    public class OperationDivision : BaseOperation
    {
        public override float Result(float x, float y)
        {
            return y / x;
        }
    }
}
