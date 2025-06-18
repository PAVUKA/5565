using System;Add commentMore actions
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace haffman32
{
    public class Program
    {
        static void Main(string[] args)
        {
            Koder koder =new Koder("text.txt");
            koder.Compressed();
            Decoders decoder = new Decoders("text.txt");
            decoder.Decode();
        }
    }
}
