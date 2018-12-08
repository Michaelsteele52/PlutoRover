﻿using System;
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
        public void Move(string instuction, int position)
        {
            _rover.Move(instuction);
            _rover.PosX.Should().Be(0);
            _rover.PosY.Should().Be(position);
        }
    }
}