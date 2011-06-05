using System;
using System.Collections.Generic;
using System.Text;

namespace GSMMODEM
{
    public class GsmModemException : Exception
    {
        public GsmModemException() : base() { }
        public GsmModemException(String str) : base(str) { }
    }
}
