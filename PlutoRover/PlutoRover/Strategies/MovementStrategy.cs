using System;
using System.Collections.Generic;
using System.Text;
using PlutoRover.Instructions;

namespace PlutoRover.Strategies
{
    public static class MovementStrategy
    {
        private const string Success = "Successful Move";
        public static string Move(Rover rover, MoveInstructions instruction, Pluto pluto)
        {
            switch (rover.CurrentDirection)
            {
                case "N":
                case "S":
                    return LatitudinalMovement(rover, instruction, pluto);
                case "E":
                case "W":
                    return LongitudinalMovement(rover, instruction, pluto);
                default:
                    return "Failure";
            }
        }

        private static string LatitudinalMovement(Rover rover, MoveInstructions instruction, Pluto pluto)
        {
            var displacement = 1;
            if (rover.CurrentDirection == "S") displacement = -1;

            if (AreMovingBackwards(instruction)) displacement *= -1;

            if (pluto.Grid[rover.PosX][GetNextYCoOrd(rover, displacement, pluto)].IsObstacle) return "IsObstacle";

            rover.PosY = GetNextYCoOrd(rover, displacement, pluto);

            return Success;
        }

        private static string LongitudinalMovement(Rover rover, MoveInstructions instruction, Pluto pluto)
        {
            var displacement = 1;
            if (rover.CurrentDirection == "W") displacement = -1;

            if (AreMovingBackwards(instruction)) displacement *= -1;

            if (pluto.Grid[GetNextXCoOrd(rover, displacement, pluto)][rover.PosY].IsObstacle) return "IsObstacle";

            rover.PosX = GetNextXCoOrd(rover, displacement, pluto);

            return Success;
        }

        private static bool AreMovingBackwards(MoveInstructions instruction) => instruction.Equals(MoveInstructions.B);
        private static int GetNextXCoOrd(Rover rover, int displacement, Pluto pluto) => (rover.PosX + displacement + pluto.XSize) % pluto.XSize;
        private static int GetNextYCoOrd(Rover rover, int displacement, Pluto pluto) => (rover.PosY + displacement + pluto.YSize) % pluto.YSize;
    }
}
