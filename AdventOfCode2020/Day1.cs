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

    public class FinderTests
    {
        [Fact]
        public void NotFound()
        {
            Finder.Find(new int[] { 0, 0 }).Should().BeNull();
        }

        [Fact]
        public void FoundInFirstPlace()
        {
            Finder.Find(new int[] { 1010, 1010 }).Should().Be((1010, 1010));
        }

        [Fact]
        public void FoundInLastPlace()
        {
            Finder.Find(new int[] { 0, 1010, 1010 }).Should().Be((1010, 1010));
        }
    }

    public static class Finder
    {
        public static (int one, int two)? Find(int[] input)
        {
            var hashset = new HashSet<int>();

            for (int i = 0; i < input.Length; i++)
            {
                var complement = 2020 - input[i];
                if (hashset.Contains(complement))
                {
                    return (complement, input[i]);
                }
                hashset.Add(input[i]);
            }

            return null;
        }
    }

    public class Day1Resolver
    {
        public int? Resolve(int[] numbers)
        {
            var f = Finder.Find(numbers);

            return f.HasValue
                ? Multiply(f.Value.one, f.Value.two)
                : null;
        }

        private static int Multiply(int one, int two)
        {
            return one * two;
        }
    }
}
