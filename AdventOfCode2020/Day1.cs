using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace AdventOfCode2020
{
    public class Day1
    {
        [Fact]
        public void NotFound()
        {
            new Day1Resolver(new FinderOfTwo()).Resolve(new[] { 0, 0 }).Should().BeNull();
        }

        [Fact]
        public void Test1()
        {
            var result = new Day1Resolver(new FinderOfTwo()).Resolve(new[] { 2018, 2 });
            result.Should().Be(4036);
        }

        [Fact]
        public void Test2()
        {
            var result = new Day1Resolver(new FinderOfTwo()).Resolve(new[] { 2017, 3 });
            result.Should().Be(6051);
        }

        [Fact]
        public void Resolve()
        {
            var result = new Day1Resolver(new FinderOfTwo()).Resolve(File.ReadAllLines("./input/input1.txt").Select(l => Convert.ToInt32(l)).ToArray());
            result.Should().Be(1016131);
        }


        [Fact]
        public void ResolvePartTwo()
        {
            var result = new Day1Resolver(new FinderOfThree()).Resolve(File.ReadAllLines("./input/input1.txt").Select(l => Convert.ToInt32(l)).ToArray());
            result.Should().Be(276432018);
        }
    }

    public class FinderOfTwoTests
    {
        [Fact]
        public void NotFound()
        {
            new FinderOfTwo().Find(new int[] { 0, 0 }).Should().BeNull();
        }

        [Fact]
        public void FoundInFirstPlace()
        {
            new FinderOfTwo().Find(new int[] { 1010, 1010 }).Should().BeEquivalentTo(new[] { 1010, 1010 });
        }

        [Fact]
        public void FoundInLastPlace()
        {
            new FinderOfTwo().Find(new int[] { 0, 1010, 1010 }).Should().BeEquivalentTo(new[] { 1010, 1010 });
        }
    }

    public class FinderOfThreeTests
    {
        [Fact]
        public void NotFound()
        {
            new FinderOfThree().Find(new int[] { 0, 0, 0 }).Should().BeNull();
        }

        [Fact]
        public void FoundInFirstPlace()
        {
            new FinderOfThree().Find(new int[] { 0, 2020, 0 }).Should().BeEquivalentTo(new[] { 0, 2020, 0 });
        }

        [Fact]
        public void FoundInLastPlace()
        {
            new FinderOfThree().Find(new int[] { 1, 0, 0, 2020 }).Should().BeEquivalentTo(new[] { 0,0,2020 });
        }
    }

    public interface IFinder
    {
        public int[] Find(int[] input);
    }

    public class FinderOfTwo : IFinder
    {
        public int[] Find(int[] input)
        {
            var hashset = new HashSet<int>();

            for (int i = 0; i < input.Length; i++)
            {
                var complement = 2020 - input[i];
                if (hashset.Contains(complement))
                {
                    return new[] { complement, input[i] };
                }
                hashset.Add(input[i]);
            }

            return null;
        }
    }

    public class Day1Resolver
    {
        private IFinder finder;

        public Day1Resolver(IFinder finder)
        {
            this.finder = finder;
        }

        public int? Resolve(int[] numbers)
        {
            var f = this.finder.Find(numbers);

            return f != null
                ? Multiply(f)
                : null;
        }

        private static int Multiply(int[] numbers)
        {
            return numbers.Aggregate(1, (aggregate, next) => aggregate * next);
        }
    }
}
