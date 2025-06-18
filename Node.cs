using System;Add commentMore actions
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace haffman32
{
   
        public class Node
        {
            private const string KeyLeft = "0";
            private const string KeyRight = "1";

            private Node _left;
            private Node _right;

            public Node(int length, string symbol)
            {
                Mass = length;
                Symbol = symbol;
                _left = null;
                _right = null;
            }

            public int Mass { get; private set; }

            public string Key { get; private set; }

            public string Symbol { get; private set; }

            public void Add(Node node)
            {
                if (node.Mass > Mass)
                {
                    if (_right == null)
                    {
                        _right = node;
                        System.Console.WriteLine($"правая {_right.Symbol} - {_right.Mass} - {_right.Key}");
                        Mass += _right.Mass;
                        Symbol += _right.Symbol;
                    }
                }
                else
                {
                    if (_left == null)
                    {
                        _left = node;
                        System.Console.WriteLine($"левая {_left.Symbol} - {_left.Mass} - {_left.Key}");
                        Mass += _left.Mass;
                        Symbol += _left.Symbol;
                    }
                }
            }

            public string GetKey(int mass, string symbol)
            {
                Key = "";

                if (_left.Mass == mass && _left.Symbol == symbol)
                {
                    Key += KeyLeft;
                }
                else
                {
                    if (_right.Mass == mass && _right.Symbol == symbol)
                        Key += KeyRight;
                    else
                        Key += KeyRight + _right.GetKey(mass, symbol);
                }

                return Key;
            }
        }
    }
