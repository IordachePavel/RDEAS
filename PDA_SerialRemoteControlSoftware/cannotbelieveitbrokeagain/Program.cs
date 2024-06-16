using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace cannotbelieveitbrokeagain
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }
        public static bool DistinguishFromCommand(string MSG){ //Tell me if its a command or a message containing some measurements
            if (MSG.StartsWith("MCU"))//thingymagig on the remote
                return false;
            if (MSG.StartsWith("NAV"))//robot navigation circuit
                return false;
            if (MSG.StartsWith("SYS"))//Something else idk
                return false;
            if (MSG.StartsWith("TRN"))//future command translator(Intermediary component)
                return false;
            return true;
    }
    }
    
}