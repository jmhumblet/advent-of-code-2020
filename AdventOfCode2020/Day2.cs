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
            var line = Line<NumberOfOccurencesPolicy>.Parse(lineExample, NumberOfOccurencesPolicy.Parse);
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
        public void IsNumberOfOccurencesValid(string lineText, bool isValid)
        {
            var line = Line<NumberOfOccurencesPolicy>.Parse(lineText, NumberOfOccurencesPolicy.Parse);
            line.IsValid.Should().Be(isValid);
        }

        [Fact]
        public void Resolve()
        {
            File.ReadAllLines("./input/input2.txt")
                .Select(l => Line<NumberOfOccurencesPolicy>.Parse(l, NumberOfOccurencesPolicy.Parse))
                .Count(l => l.IsValid)
                .Should()
                .Be(416);
        }

        [Theory]
        [InlineData("1-1 a: a", false)]
        [InlineData("1-2 a: aa", false)]
        [InlineData("1-2 a: ab", true)]
        [InlineData("1-3 a: aab", true)]
        public void IsPlacesOfOccurencesValid(string lineText, bool isValid)
        {
            var line = Line<PlacesOfOccurencesPolicy>.Parse(lineText, PlacesOfOccurencesPolicy.Parse);
            line.IsValid.Should().Be(isValid);
        }

        [Fact]
        public void ResolvePart2()
        {
            File.ReadAllLines("./input/input2.txt")
                .Select(l => Line<PlacesOfOccurencesPolicy>.Parse(l, PlacesOfOccurencesPolicy.Parse))
                .Count(l => l.IsValid)
                .Should()
                .Be(416);
        }
    }

    internal record Line<TPolicy> where TPolicy : Policy
    {
        private Line(TPolicy policy, string password)
        {
            Policy = policy;
            Password = password;
        }

        public static Line<TPolicy>? Parse(string text, Func<string, TPolicy?> policyParser)
        {
            var parts = text.Split(':', StringSplitOptions.TrimEntries);
            var policy = policyParser.Invoke(parts[0]);

            if (policy == null)
            {
                return null;
            }

            return new Line<TPolicy>(policy, parts[1]);
        }

        public TPolicy Policy { get; }
        public string Password { get; }
        public bool IsValid
        {
            get
            {
                return Policy.IsRespectedBy(Password);
            }
        }

    }

    internal abstract record Policy
    {
        public abstract bool IsRespectedBy(string password);
    }

    internal record NumberOfOccurencesPolicy : Policy
    {
        private NumberOfOccurencesPolicy(int minimumOccurence, int maximumOccurrence, char letter)
        {
            MinimumOccurrence = minimumOccurence;
            MaximumOccurence = maximumOccurrence;
            Letter = letter;
        }

        public int MinimumOccurrence { get; }
        public int MaximumOccurence { get; }
        public char Letter { get; }

        public static NumberOfOccurencesPolicy? Parse(string policyText)
        {
            var parts = policyText.Split(new[] { '-', ' ' });

            var minimumOccurrence = Convert.ToInt32(parts[0]);
            var maximumOccurrence = Convert.ToInt32(parts[1]);
            var letter = parts[2][0];

            return new NumberOfOccurencesPolicy(minimumOccurrence, maximumOccurrence, letter); 
        }

        public override bool IsRespectedBy(string password)
        {
            var letterCount = password.Count(letter => letter == Letter);

            return letterCount >= MinimumOccurrence && letterCount <= MaximumOccurence;
        }
    }

    internal record PlacesOfOccurencesPolicy : Policy
    {
        public PlacesOfOccurencesPolicy(int firstOccurrencePosition, int secondOccurencePosition, char letter)
        {
            FirstOccurrencePosition = firstOccurrencePosition;
            SecondOccurencePosition = secondOccurencePosition;
            Letter = letter;
        }

        public int FirstOccurrencePosition { get; }
        public int SecondOccurencePosition { get; }
        public char Letter { get; }

        public static PlacesOfOccurencesPolicy? Parse(string policyText)
        {
            var parts = policyText.Split(new[] { '-', ' ' });

            var firstOccurrencePosition = Convert.ToInt32(parts[0]);
            var secondOccurencePosition = Convert.ToInt32(parts[1]);
            var letter = parts[2][0];

            return new PlacesOfOccurencesPolicy(firstOccurrencePosition, secondOccurencePosition, letter);
        }

        public override bool IsRespectedBy(string password)
        {
            return password[FirstOccurrencePosition - 1] == Letter
                ^ password[SecondOccurencePosition - 1] == Letter;
        }
    }


}
