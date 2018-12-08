using System;

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
        public Rover()
        {
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
    }
}