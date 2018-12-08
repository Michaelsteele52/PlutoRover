using System;
using System.Security.Cryptography.X509Certificates;
using FluentAssertions;
using NUnit.Framework;

namespace PlutoRover.Tests
{
    [TestFixture]
    public class PlutoRover_Should
    {
        private Rover _rover;
        private Pluto _pluto;
        private readonly int XSize = 100;
        private readonly int YSize = 100;
        [SetUp]
        public void SetUp()
        {
            _rover = new Rover(0,0);
            _pluto = new Pluto(XSize, YSize);
        }

        [Test]
        public void RejectBadInstructions()
        {
            var result = _rover.Instruct("BadInput", _pluto);
            result.Should().Be("Invalid Instruction");
        }

        [Test]
        public void NotCarryOutNextInstructionsIfBadInput()
        {
            var result = _rover.ExecuteInstructions("BadInput", _pluto);
            result.Should().Be("Invalid Instruction");
        }

        [TestCase("L","W")]
        [TestCase("R","E")]
        public void Turn(string input, string output)
        {
            _rover.Turn(input);
            _rover.CurrentDirection.Should().Be(output);
        }

        [TestCase("F", 1)]
        [TestCase("B", 99)]
        public void Move(string instruction, int position)
        {
            _rover.Move(instruction, _pluto);
            _rover.PosX.Should().Be(0);
            _rover.PosY.Should().Be(position);
        }

        [TestCase("R", "F", 0, 1)]
        [TestCase("R", "B", 0, 99)]
        [TestCase("L", "F", 0, 99)]
        [TestCase("L", "B", 0, 1)]
        public void TurnAndMove(string turnInstruction, string moveInstruction, int yPosition, int xPosition)
        {
            _rover.Turn(turnInstruction);
            _rover.Move(moveInstruction, _pluto);
            _rover.PosY.Should().Be(yPosition);
            _rover.PosX.Should().Be(xPosition);
        }

        [Test]
        public void StopIfObstacle()
        {
            _pluto.Grid[0][1].Obstacle = true;
            var result = _rover.Move("F", _pluto);
            result.Should().Be("Obstacle");
        }

        [Test]
        public void NotCarryOutNextInstructionIfObstacle()
        {
            _pluto.Grid[0][1].Obstacle = true;
            var result = _rover.ExecuteInstructions("F", _pluto);
            result.Should().Be("Obstacle");
        }
    }
}
