using System;Add commentMore actions
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace haffman32
{
    public class Koder
    {
        public Node Root;
        private string _origenle;
        private string _compressed;
        private Dictionary<char, int> _frequencyTable;

        public Koder(string path)
        {
            _origenle = File.ReadAllText(path);
            _frequencyTable = new Dictionary<char, int>();
            _compressed = "";
            Root = null;
        }

        public void Compressed()
        {
            List<Node> nodes = new List<Node>();

            foreach (char ch in _origenle)
            {
                if (_frequencyTable.ContainsKey(ch))
                    _frequencyTable[ch]++;
                else
                    _frequencyTable.Add(ch, 1);
            }

            foreach (char item in _frequencyTable.Keys)
            {
                nodes.Add(new Node(_frequencyTable[item], item.ToString()));
            }

            SortListNode(nodes);

            int idNodeOne = 0;
            int idNodeTwo = 1;
            Node nodeOne;
            Node nodeTwo;
            Node Root = null;

            while (nodes.Count > 0)
            {
                if (nodes.Count > 1)
                {
                    nodeOne = nodes[idNodeOne];
                    nodeTwo = nodes[idNodeTwo];
                    Root = new Node(0, "");
                    Root.Add(nodeOne);
                    Root.Add(nodeTwo);
                    Console.WriteLine();
                    Console.WriteLine($"({Root.Symbol}) - ({Root.Mass})");
                    nodes.Remove(nodeOne);
                    nodes.Remove(nodeTwo);
                    nodes.Add(Root);
                }
                else
                {
                    nodeOne = nodes[idNodeOne];
                    Root.Add(nodeOne);
                    nodes.Remove(nodeOne);
                }

                OffsetRoot(nodes);
            }

            _compressed += CompressedFrequencyTable(_frequencyTable);
            string compressedText = CompressedString(_compressed);


            for (int i = 0; i < _origenle.Length; i++)
            {
                _compressed += Root.GetKey(_frequencyTable[_origenle[i]], _origenle[i].ToString());
            }

            Console.WriteLine(_compressed);

            byte[] compressed = GetByteArray(_compressed);

            for (int i = 0; i < compressed.Length; i++)
            {
                Console.Write(Convert.ToString(Convert.ToChar(compressed[i])));
            }

            BinaryWriter binary = new BinaryWriter(File.Open("test.txt", FileMode.Create));
            binary.Write(compressed);
        }

        private byte[] GetByteArray(string bets)
        {
            List<byte> data = new List<byte>();
            byte bit = 0;

            for (int i = 0; i < bets.Length; i++)
            {
                int idBit = bets[i] - '0';
                bit = (byte)(bit | idBit);

                if (i % 8 == 0)
                {
                    data.Add(bit);
                    bit = 0;
                }
                else
                {
                    bit = (byte)(bit << 1);
                }
            }
            byte[] keys = data.ToArray();
            return keys;
        }

        private string CompressedFrequencyTable(Dictionary<char, int> frequencyTable)
        {
            string text = "";

            foreach (char key in frequencyTable.Keys)
                text += key + frequencyTable[key];

            text += '\n';
            return text;
        }

        private string CompressedString(string text)
        {
            string compressionString = "";
            char simbleNumber = '|';
            char simblSeparatar = ':';
            int countSimple = 1;


            for (int i = 0; i <text.Length-1; i++)
            {
                if (text[i] == text[i + 1])
                {
                    countSimple++;
                }
                else
                {
                    compressionString += $"{countSimple}{simbleNumber}{text[i]}{simblSeparatar}";
                    countSimple = 1;
                }
            }
            return compressionString;

        }

        private void SortListNode(List<Node> nodes)
        {
            Node node;

            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes.Count - 1; j++)
                {
                    if (nodes[j].Mass > nodes[j + 1].Mass)
                    {
                        node = nodes[j];
                        nodes[j] = nodes[j + 1];
                        nodes[j + 1] = node;
                    }
                }
            }
        }

        private void OffsetRoot(List<Node> nodes)
        {
            int idRoot = nodes.Count - 1;
            Node node = null;

            for (int i = 0; i < nodes.Count - 1; i++)
            {
                node = nodes[idRoot];
                nodes[idRoot] = nodes[idRoot - 1];
                nodes[idRoot - 1] = node;
                idRoot--;
            }
        }
    }
}
