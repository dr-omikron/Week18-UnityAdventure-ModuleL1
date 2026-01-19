using System;
using System.Collections.Generic;
using System.Text;
using Random = UnityEngine.Random;

namespace _Project.Develop.Runtime.Gameplay.Services
{
    public class SymbolsSequenceGenerator
    {
        public string Generate(List<char> symbols, int sequenceLenght)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < sequenceLenght; i++)
            {
                char symbol = symbols[Random.Range(0, symbols.Count)];

                if(symbol == ' ')
                    throw new ArgumentException($"Blank symbol in {nameof(symbols)}");

                result.Append(symbol);
            }

            return result.ToString();
        }
    }
}
