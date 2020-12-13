using FluentAssertions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace AdventOfCode2020
{
    public class Day3Tests
    {
        [Fact]
        public void ParseMap()
        {
            var mapPattern = new[] { ".#", "#." };
            var map = Map.Parse(mapPattern);
            map.Length.Should().Be(2);
            map.SquareAt(0, 0).Should().Be(Square.Open);
            map.SquareAt(0, 2).Should().Be(Square.Open);
            map.SquareAt(1, 2).Should().Be(Square.Tree);
        }

        [Fact]
        public void TobogganSlideDown()
        {
            var map = Map.Parse(new[] { ".#", "#." });
            var toboggan = new Toboggan(map, 1);
            var squares = toboggan.SlideDown();
            squares.Should().BeEquivalentTo(new[] { Square.Open, Square.Open });
        }

        [Fact]
        public void TobogganSlideDownSlopeOfMultipleRows()
        {
            var map = Map.Parse(new[] { ".#", ".#", ".#", ".#", ".#" });
            var toboggan = new Toboggan(map, new Slope(1,2));
            var squares = toboggan.SlideDown();
            squares.Count().Should().Be(3);
            squares.Should().BeEquivalentTo(new[] { Square.Open, Square.Tree, Square.Open });
        }

        [Fact]
        public void Resolve()
        {
            var map = Map.Parse(File.ReadAllLines("./input/input3.txt"));
            var toboggan = new Toboggan(map, 3);
            var squares = toboggan.SlideDown();
            squares.Count(s => s == Square.Tree).Should().Be(247);
        }

        [Fact]
        public void ResolvePart2()
        {
            var map = Map.Parse(File.ReadAllLines("./input/input3.txt"));
            var squares = new Toboggan(map, new Slope(3)).SlideDown();
            long r1 = new Toboggan(map, new Slope(1)).SlideDown().Count(s => s == Square.Tree);
            long r3 = new Toboggan(map, new Slope(3)).SlideDown().Count(s => s == Square.Tree);
            long r5 = new Toboggan(map, new Slope(5)).SlideDown().Count(s => s == Square.Tree);
            long r7 = new Toboggan(map, new Slope(7)).SlideDown().Count(s => s == Square.Tree);
            long r12 = new Toboggan(map, new Slope(1,2)).SlideDown().Count(s => s == Square.Tree);


            (r1 * r3 * r5 * r7 * r12).Should().Be(2983070376);
        }
    }

    internal record Slope(int Right, int Down = 1);

    internal class Toboggan
    {
        private Map map;
        private Slope slope;

        public Toboggan(Map map, int slope) : this(map, new Slope(slope))
        {

        }

        public Toboggan(Map map, Slope slope)
        {
            this.map = map;
            this.slope = slope;
        }



        internal IEnumerable<Square> SlideDown()
        {
            for (int hop = 0; hop * slope.Down < map.Length; hop++)
            {
                yield return map.SquareAt(hop * slope.Down, hop * slope.Right);
            }

        }
    }

    internal class Map
    {
        private TreeLine[] lines;

        public int Length => lines.Length;

        public static Map Parse(string[] mapPattern)
        {
            return new Map(mapPattern.Select(TreeLine.Parse).ToArray());
        }

        private Map(TreeLine[] lines)
        {
            this.lines = lines;
        }

        internal Square SquareAt(int line, int position)
        {
            return lines[line].SquareAt(position);
        }

        private class TreeLine
        {
            private readonly Square[] squares;

            public static TreeLine Parse(string pattern)
            {
                return new TreeLine(pattern.Select(Square.Parse));
            }

            private TreeLine(IEnumerable<Square> squares)
            {
                this.squares = squares.ToArray();
            }

            public Square SquareAt(int position)
            {
                return squares[position % squares.Length];
            }
        }
    }

    internal class Square
    {
        public static Square Open = new Square();
        public static Square Tree = new Square();

        private Square()
        {

        }

        public static Square Parse(char square)
        {
            return square.Equals('#') ? Tree : Open;
        }
    } 
}
