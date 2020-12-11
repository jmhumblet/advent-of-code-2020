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
            new Finder().Find(new[] { 0, 0 }).Should().BeNull();
        }

        [Fact]
        public void Test1()
        {
            var result = new Finder().Find(new[] { 2018, 2 });
            result.Should().Be(4036);
        }

        [Fact]
        public void Test2()
        {
            var result = new Finder().Find(new[] { 2017, 3 });
            result.Should().Be(6051);
        }
    }

    public class Finder
    {
        public int? Find(int[] numbers)
        {
            return numbers[0] + numbers[1] == 2020
                ? numbers[0] * numbers[1]
                : null;
        }
    }


}
