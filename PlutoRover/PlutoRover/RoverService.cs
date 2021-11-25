using System;
using System.Collections.Generic;
using System.Text;
using PlutoRover.Instructions;
using PlutoRover.Strategies;

namespace PlutoRover
{
    public class RoverService : IRoverService
    {
        public string ExecuteInstructions(Rover rover, string instructions, Pluto pluto)
        {
            var result = "";
            foreach (var c in instructions)
            {
                IInstructionOperation operation = null;

                operation = Instruct(rover, c.ToString(), pluto);
                if (operation == null) return $"{c} is not a valid Instruction";
                var instruction = new Instruction(pluto, rover, c.ToString());

                result = operation.Execute(instruction);

                if (result == "IsObstacle")
                {
                    return result;
                }
            }
            return result;
        }

        private static IInstructionOperation Instruct(Rover rover, string instruction, Pluto pluto)
        {
            if (Enum.TryParse<TurnInstructions>(instruction, true, out var turnInstruction)) return new TurnOperation();

            if (Enum.TryParse<MoveInstructions>(instruction, true, out var moveInstruction)) return new MovementOperation();

            return null;
        }
    }
}
