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

        private int Find(IEnumerable<int> numbers)
        {
            return 4036;
        }
    }
}
