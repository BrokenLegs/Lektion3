using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lektion3.Util
{
    public static class ExtensionMethods
    {
        public static int WordCount(this string myString)
        {
            return myString.Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries ).Length;
        }
    }
}
