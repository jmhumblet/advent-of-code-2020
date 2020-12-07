using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode2020
{
    public class Day1
    {
        [Fact]
        public void Test1()
        {
            var result = Find(new[] { 2018, 2 });
            result.Should().Be(4036);
        }

        [Fact]
        public void Test2()
        {
            var result = Find(new[] { 2017, 3 });
            result.Should().Be(6051);
        }

        private int Find(int[] numbers)
        {
            return numbers[0] * numbers[1];
        }
    }
}
