using System;
using System.Collections.Generic;

namespace Robo_Simulation
{
    public class RoboSimulation
    {
        public static void Main()
        {
            try
            {
                List<string> lstCommands = new List<string>();
                string command;
                do
                {
                    Console.WriteLine();
                    command = Console.ReadLine().ToUpper();
                    lstCommands.Add(command);

                }
                while (command != "REPORT");
                Execute(lstCommands);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Execute(List<string> lstCommands)
        {
            try
            {
                Console.WriteLine(Commands.CmdReport(lstCommands));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
