using OneOf;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlutoRover.Instructions
{
    public class Instruction
    {
        public Pluto Pluto { get; }
        public Rover Rover { get; }
        private readonly string _instruction;
        public Instruction(Pluto pluto, Rover rover, string instruction)
        {
            Pluto = pluto;
            Rover = rover;
            _instruction = instruction;
        }

        public OneOf<TurnInstructions, MoveInstructions, string> GetInstruction()
        {
            if(Enum.TryParse<TurnInstructions>(_instruction, true, out var turnInstruction)) return turnInstruction;
            if(Enum.TryParse<MoveInstructions>(_instruction, true, out var moveInstruction)) return moveInstruction;
            return "Invalid Instruction";
        }
    }
}
