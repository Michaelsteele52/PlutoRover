using PlutoRover.Instructions;

namespace PlutoRover.Strategies
{
    public class TurnOperation : IInstructionOperation
    {
        public string Execute(Instruction instruction)
        {
            var instructionEnum = instruction.GetInstruction().AsT0;

            var change = instructionEnum.Equals(TurnInstructions.R) ? 1 : -1;

            instruction.Rover.ChangeDirection(change);
            return "Turned";
        }
    }
}
