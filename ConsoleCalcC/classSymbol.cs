using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalcC
{
    public class classSymbol
    {
        string _Symbol;
	    int _Prioritet;
        BaseOperation _Operation;
	    
        public string Symbol 
        {
		    get { return _Symbol; }
	    }

	    public int Prioritet 
        {
		    get { return _Prioritet; }
	    }

	    public classSymbol(string Symbol, int Prioritet)
	    {
		    _Symbol = Symbol;
		    _Prioritet = Prioritet;
	    }

        public classSymbol(string Symbol, int Prioritet, BaseOperation Operation)
	    {
		    _Symbol = Symbol;
		    _Prioritet = Prioritet;
		    _Operation = Operation;
	    }
    
	    public float Calc(float x, float y)
	    {
		    return _Operation.Result(x, y);
	    }

    }
}
