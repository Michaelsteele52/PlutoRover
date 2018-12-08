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
        [SetUp]
        public void SetUp()
        {
            _rover = new Rover(0,0);
        }

        [Test]
        public void RejectBadInstructions()
        {
            var result = _rover.Instruct("BadInput");
            result.Should().Be("Invalid Instruction");
        }

        [Test]
        public void NotCarryOutNextInstructionsIfBadInput()
        {
            var result = _rover.ExecuteInstructions("BadInput");
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
            _rover.Move(instruction);
            _rover.PosX.Should().Be(0);
            _rover.PosY.Should().Be(position);
        }

        [TestCase("R", "F", 0, 1)]
        [TestCase("R", "B", 0, 99)]
        [TestCase("L", "F", 0, 99)]
        [TestCase("L", "B", 0, 1)]
        public void TurnAndMove(string turnInstruction, string moveInstruction, int xPosition, int yPosition)
        {
            _rover.Turn(turnInstruction);
            _rover.Move(moveInstruction);
            _rover.PosX.Should().Be(xPosition);
            _rover.PosY.Should().Be(yPosition);
        }
    }
}
