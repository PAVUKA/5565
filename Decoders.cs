using System;Add commentMore actions
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace haffman32
{
    public class Decoders
    {
        private string[] _original;
        private string[] _restored;

        public Decoders(string path)
        {
            RideFile(path);
        }

        public void Decode()
        {
            _restored = DecodeString();

            foreach (string text in _restored)
                File.AppendAllText("DecodeFile.txt", text + "\n");
        }

        private void RideFile(string path)
        {
            _original = File.ReadAllLines(path);
        }

        private string[] DecodeString()
        {
            string[] decodeString = new string[_original.Length];
            int number;
            int countChar = 0;
            string symbol = "";

            for (int i = 0; i < _original.Length-1; i++)
            {
                symbol = "";
                symbol += _original[i] + _original[i + 1];

                if (int.TryParse(symbol, out number))
                {
                    countChar = number;
                }
                else
                {
                    symbol += _original[i];
                    if (int.TryParse(symbol, out number))
                    {
                        countChar = number;
                    }Add commentMore actions
                    decodeString[i] += symbol;
                }
            }

            return decodeString;
        }
    }
}
