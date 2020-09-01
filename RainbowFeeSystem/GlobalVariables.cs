using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RainbowFeeSystem
{
    public class GlobalVariables
    {
        // parameterless constructor required for static class
        static void Globals() { GlobalLong = 0000; } // default value

        // public get, and private set for strict access control
        public static long GlobalLong { get; private set; }
 
        // GlobalInt can be changed only via this method
        public static void SetGlobalLong(long newInt)
        {
            GlobalLong = newInt;
        }
    }
}