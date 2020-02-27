using System;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Test Setup");
        }

        [Test]
        public void CanAssertPass()
        {
            Assert.Pass();
        }

        [Test]
        public void CanUseFluentAssertions()
        {
            true.Should().BeTrue("true should be true");
        }
    }
}