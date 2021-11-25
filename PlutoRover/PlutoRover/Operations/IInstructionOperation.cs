using System;
using System.Collections.Generic;
using System.Text;
using PlutoRover.Instructions;

namespace PlutoRover.Strategies
{
    public interface IInstructionOperation
    {
        string Execute(Instruction instruction);
    }
}
