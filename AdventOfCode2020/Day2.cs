using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020
{
    public class Day2
    {
        private static string lineExample = "8-11 l: qllllqllklhlvtl";

        [Fact]
        public void ParseLine()
        {
            var line = Line.Parse(lineExample);
            line.Should().NotBeNull();
            line.Policy.MinimumOccurrence.Should().Be(8);
            line.Policy.MaximumOccurence.Should().Be(11);
            line.Policy.Letter.Should().Be('l');
            line.Password.Should().Be("qllllqllklhlvtl");
        }

        [Theory]
        [InlineData("1-1 a: a", true)]
        [InlineData("1-1 a: b", false)]
        [InlineData("0-1 a: b", true)]
        [InlineData("0-0 a: a", false)]
        [InlineData("0-1 a: aa", false)]
        public void IsValid(string lineText, bool isValid)
        {
            var line = Line.Parse(lineText);
            line.IsValid.Should().Be(isValid);
        }

        [Fact]
        public void Resolve()
        {
            File.ReadAllLines("./input/input2.txt")
                .Select(l => Line.Parse(l))
                .Count(l => l.IsValid)
                .Should()
                .Be(416);
        }
    }

    internal record Line
    {
        private Line(Policy policy, string password)
        {
            Policy = policy;
            Password = password;
        }

        public static Line? Parse(string text)
        {
            var parts = text.Split(':', StringSplitOptions.TrimEntries);
            var policy = Policy.Parse(parts[0]);

            if (policy == null)
            {
                return null;
            }

            return new Line(policy, parts[1]);
        }

        public Policy Policy { get; }
        public string Password { get; }
        public bool IsValid
        {
            get
            {
                return Policy.IsRespectedBy(Password);
            }
        }

    }

    internal record Policy
    {
        private Policy(int minimumOccurence, int maximumOccurrence, char letter)
        {
            MinimumOccurrence = minimumOccurence;
            MaximumOccurence = maximumOccurrence;
            Letter = letter;
        }

        public int MinimumOccurrence { get; }
        public int MaximumOccurence { get; }
        public char Letter { get; }

        internal static Policy? Parse(string policyText)
        {
            var parts = policyText.Split(new[] { '-', ' ' });

            var minimumOccurrence = Convert.ToInt32(parts[0]);
            var maximumOccurrence = Convert.ToInt32(parts[1]);
            var letter = parts[2][0];

            return new Policy(minimumOccurrence, maximumOccurrence, letter); 
        }

        internal bool IsRespectedBy(string password)
        {
            var letterCount = password.Count(letter => letter == Letter);

            return letterCount >= MinimumOccurrence && letterCount <= MaximumOccurence;
        }
    }


}
