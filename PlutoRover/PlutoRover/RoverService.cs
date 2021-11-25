using System;
using System.Collections.Generic;
using System.Text;
using PlutoRover.Instructions;
using PlutoRover.Strategies;

namespace PlutoRover
{
    public class RoverService
    {
        public static string ExecuteInstructions(Rover rover, string instructions, Pluto pluto)
        {
            var result = "";
            foreach (var c in instructions)
            {
                result = Instruct(rover, c.ToString(), pluto);
                if (result == "Invalid Instruction")
                {
                    return result;
                }

                if (result == "IsObstacle")
                {
                    return result;
                }
            }
            return result;
        }

        private static string Instruct(Rover rover, string instruction, Pluto pluto)
        {
            if (Enum.TryParse<TurnInstructions>(instruction, out var turnInstruction)) return TurnStrategy.Turn(rover, turnInstruction);

            if (Enum.TryParse<MoveInstructions>(instruction, out var moveInstruction)) return MovementStrategy.Move(rover, moveInstruction, pluto);

            return "Invalid Instruction";
        }
    }
}
