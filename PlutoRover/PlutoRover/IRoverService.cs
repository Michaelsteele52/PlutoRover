using System;
using System.Collections.Generic;
using System.Text;

namespace PlutoRover
{
    public interface IRoverService
    {
        string ExecuteInstructions(Rover rover, string instructions, Pluto pluto);
    }
}
