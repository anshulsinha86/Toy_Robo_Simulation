using System;
using System.Collections.Generic;
using System.Linq;

namespace Robo_Simulation
{
    public static class Directions
    {
        public const string North = "NORTH";
        public const string South = "SOUTH";
        public const string East = "EAST";
        public const string West = "WEST";

    }

    public static class Instructions
    {
        public const string Place = "PLACE";
        public const string Move = "MOVE";
        public const string Left = "LEFT";
        public const string Right = "RIGHT";
        public const string Report = "Report";
        public const string Message = "Place the toy on the table.";
        public const string IncorrectPlace = "Incorrect parameters in PLACE command.";
    }

    public class Commands
    {
        public static int currentX;
        public static int currentY;
        public static int nextX;
        public static int nextY;
        public static string currentDirection;
        public static int tableSize = 5;

        /// <summary>
        /// This method is used to predict the next cell for the toy to move based on the direction provided in the previous command.
        /// </summary>
        /// <param name="direction"></param>
        private static void DecodeDirection(string direction)
        {
            switch (direction)
            {
                case Directions.North:
                    nextX = currentX;
                    nextY = currentY + 1;
                    break;
                case Directions.South:
                    nextX = currentX;
                    nextY = currentY - 1;
                    break;
                case Directions.East:
                    nextX = currentX + 1;
                    nextY = currentY;
                    break;
                case Directions.West:
                    nextX = currentX - 1;
                    nextY = currentY;
                    break;
                default:
                    nextX = currentX;
                    nextY = currentY;
                    break;
            }
        }

        /// <summary>
        /// This method is is used to set the parameter passed from the main class to the globally declared variables in Commands class.
        /// </summary>
        /// <param name="positionX"></param>
        /// <param name="positionY"></param>
        /// <param name="direction"></param>
        public static void CmdPlace (int positionX, int positionY, string direction)
        {
            if (TableSizeCheck(positionX, positionY))
            {
                currentX = positionX;
                currentY = positionY;
                currentDirection = direction;
            }
        }

        /// <summary>
        /// This command moves the toy to the next cell based on the direction and position passed in the PLACE command.
        /// </summary>
        public static void CmdMove()
        {
            DecodeDirection(currentDirection);

            if (TableSizeCheck(nextX, nextY))
            {
                currentX = nextX;
                currentY = nextY;
            }
        }

        /// <summary>
        /// This command changes the direction, the toy is facing, towards left of the current direction.
        /// </summary>
        public static void CmdLeft()
        {
            switch (currentDirection)
            {
                case Directions.North:
                    currentDirection = Directions.West;
                    break;
                case Directions.South:
                    currentDirection = Directions.East;
                    break;
                case Directions.West:
                    currentDirection = Directions.South;
                    break;
                case Directions.East:
                    currentDirection = Directions.North;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// This command changes the direction, the toy is facing, towards right of the current direction
        /// </summary>
        public static void CmdRight()
        {
            switch (currentDirection)
            {
                case Directions.North:
                    currentDirection = Directions.East;
                    break;
                case Directions.South:
                    currentDirection = Directions.West;
                    break;
                case Directions.West:
                    currentDirection = Directions.North;
                    break;
                case Directions.East:
                    currentDirection = Directions.South;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// this command is used to finalize and execute all the commands passed together.
        /// </summary>
        /// <param name="lstCommands"></param>
        /// <returns></returns>
        public static string CmdReport(List<string> lstCommands)
        {
            // Checks if the toy is placed on the table.
            if (lstCommands.Any(cmd => cmd.Contains(Instructions.Place)))
            {
                foreach (string command in lstCommands)
                {
                    if (command.Split(' ')[0].Contains(Instructions.Place))
                    {
                        string[] splitInput = command.Split(' ');

                        //Checks if the number of place parameters are correct.
                        if (splitInput[1].Split(',').Length == 3)
                        {
                            int positionX = Convert.ToInt32(splitInput[1].Split(',')[0]);
                            int positionY = Convert.ToInt32(splitInput[1].Split(',')[1]);
                            string direction = splitInput[1].Split(',')[2];
                            if (TableSizeCheck(positionX, positionY))
                            {
                                CmdPlace(positionX, positionY, direction);
                            }
                            else
                            {
                                return Instructions.Message;
                            }
                        }
                        else
                            return Instructions.IncorrectPlace;
                    }
                    
                    if (command != Instructions.Report)
                    {
                        switch (command)
                        {
                            case Instructions.Move:
                                CmdMove();
                                break;
                            case Instructions.Left:
                                CmdLeft();
                                break;
                            case Instructions.Right:
                                CmdRight();
                                break;
                            default:
                                break;
                        }
                    }
                    
                }
                return string.Format("{0},{1},{2}", currentX, currentY, currentDirection);
            }
            else
            {
                return Instructions.Message;
            }
        }

        /// <summary>
        /// This method is implemented to check the table size, so that the toy doesn't fall off it.
        /// </summary>
        /// <param name="positionX"></param>
        /// <param name="positionY"></param>
        /// <returns></returns>
        private static bool TableSizeCheck(int positionX, int positionY)
        {
            if ((positionX >= 0 && positionX < tableSize) && (positionY >= 0 && positionY < tableSize))
                return true;
            else
                return false;

        }
    }
}
