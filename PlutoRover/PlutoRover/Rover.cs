using System;
using System.Collections.Generic;

namespace PlutoRover
{
    public class Rover
    {
        public enum TurnInstructions
        {
            L,R
        }

        public enum MoveInstructions
        {
            F,B
        }
        public string CurrentDirection;
        public readonly List<string> Directions = new List<string>(){"N","E","S","W"};
        public int PosX;
        public int PosY;
        public Rover(int posX, int posY)
        {
            CurrentDirection = Directions[0];
            PosX = posX;
            PosY = posY;
        }

        


        public string Instruct(string instruction)
        {
            if (!Enum.IsDefined(typeof(TurnInstructions), instruction))
            {
                return "Invalid Instruction";
            }
            else if(!Enum.IsDefined(typeof(MoveInstructions), instruction))
            {
                return "Invalid Instruction";
            }

            return "";
        }

        public string ExecuteInstructions(string badinput)
        {
            var result = "";
            foreach (var c in badinput)
            {
                result = Instruct(c.ToString());
                if (result == "Invalid Instruction")
                {
                    return result;
                }
            }

            return "Arrived";
        }

        public string Turn(string instruction)
        {
            var change = 0;
            if (instruction == "L")
                change = -1;
            else if (instruction == "R")
                change = 1;
            var direction = Directions.IndexOf(CurrentDirection);
            CurrentDirection = Directions[(direction + change + Directions.Count) % Directions.Count];
            return "Turned";
        }

        public void Move(string instruction)
        {
            var displacement = 0;
            if (CurrentDirection == "N" || CurrentDirection == "S")
            {
                displacement = 1;
                if (CurrentDirection == "S")
                {
                    displacement = -1;
                }

                if (instruction == "B")
                {
                    displacement *= -1;
                }

                PosY = (PosY + displacement + 100) % 100;
            }
        }
    }
}