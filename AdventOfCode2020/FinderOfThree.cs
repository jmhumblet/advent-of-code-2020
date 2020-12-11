using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    internal class FinderOfThree : IFinder
    {
        public FinderOfThree()
        {

        }

        public int[] Find(int[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = i+1; j < input.Length; j++)
                {
                    for (int k = j+1; k < input.Length; k++)
                    {
                        if (input[i] + input[j] + input[k] == 2020)
                        {
                            return new[] { input[i], input[j], input[k] };
                        }
                    }
                }
            }

            return null;
        }
    }
}