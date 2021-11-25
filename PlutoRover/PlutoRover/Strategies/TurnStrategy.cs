using System;
using System.Collections.Generic;
using System.Text;
using PlutoRover.Instructions;

namespace PlutoRover.Strategies
{
    public static class TurnStrategy
    {
        public static string Turn(Rover rover, TurnInstructions instruction)
        {
            var change = instruction.Equals(TurnInstructions.R) ? 1 : -1;

            rover.ChangeDirection(change);
            return "Turned";
        }
    }
}
