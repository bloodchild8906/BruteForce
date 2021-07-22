using System;
using System.Collections.Generic;
using System.Linq;

namespace BruteForce.Permutations
{
    public class Permutations
    {
        public List<HashSet<char>> CharCombinations { get; set; }

        private IEnumerable<char> ReplaceCharacters(char character)
        {
            var result = new HashSet<char>();
            foreach (var characterCombination in CharCombinations
                .Where(charCombinations => charCombinations.Contains(character))
                .SelectMany(charCombinations => charCombinations))
            {
                result.Add(characterCombination);
            }

            if (char.IsLetter(character))
            {
                result.Add((string.Empty + character).ToUpper()[0]);
                result.Add((string.Empty + character).ToLower()[0]);
            }

            result.Add(character);
            return result;
        }

        private IEnumerable<string> CreateList(string input, int start)
        {
            var characters = input[start - 1];
            var transformations = new List<string>();
            foreach (var character in ReplaceCharacters(characters))
            {
                if (start == input.Length)
                    transformations.Add(string.Empty + character);
                else
                {
                    using (var enumerator = CreateList(input, start + 1).GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                            transformations.Add(character + enumerator.Current);
                        enumerator.Dispose();
                    }
                }
            }

            return transformations;
        }

        public List<string> GetAllPermutations(string input)
        {
            if (CharCombinations.Count == 0)
                throw new Exception("cannot continue with empty character combination hashset");
            var enumerator = CreateList(input, 1).GetEnumerator();
            var result = new List<string>();
            while (enumerator.MoveNext())
                result.Add(enumerator.Current);
            enumerator.Dispose();
            return result;
        }
    }
}