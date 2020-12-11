using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode2020
{
    public class Day1
    {
        [Fact]
        public void NotFound()
        {
            new Day1Resolver().Resolve(new[] { 0, 0 }).Should().BeNull();
        }

        [Fact]
        public void Test1()
        {
            var result = new Day1Resolver().Resolve(new[] { 2018, 2 });
            result.Should().Be(4036);
        }

        [Fact]
        public void Test2()
        {
            var result = new Day1Resolver().Resolve(new[] { 2017, 3 });
            result.Should().Be(6051);
        }
    }

    public class Day1Resolver
    {
        public int? Resolve(int[] numbers)
        {
            var f = FindNumbers(numbers);

            return (f.HasValue)
                ? Multiply(f.Value.one, f.Value.two)
                : null;
        }

        private (int one, int two)? FindNumbers(int[] numbers)
        {
            return numbers[0] + numbers[1] == 2020 ? (numbers[0], numbers[1]) : null;
        }

        private static int Multiply(int one, int two)
        {
            return one * two;
        }
    }
}
