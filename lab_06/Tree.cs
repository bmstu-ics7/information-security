using System.Collections.Generic;

namespace CompressHuffman
{
    public class Tree
    {
        public class Node
        {
            public uint? Count;

            public byte? S = null;

            public Node L = null;
            public Node R = null;

            public void GetCode(Dictionary<byte, List<byte>> table, List<byte> code)
            {
                Count = null;

                if (S != null)
                {
                    table[S.Value] = new List<byte>(code);
                }

                if (L != null)
                {
                    code.Add(0);
                    L.GetCode(table, new List<byte>(code));
                    code.RemoveAt(code.Count - 1);
                }

                if (R != null)
                {
                    code.Add(1);
                    R.GetCode(table, new List<byte>(code));
                    code.RemoveAt(code.Count - 1);
                }
            }
        }

        public Node H;

        public Tree(Node head)
        {
            H = head;
        }

        public Dictionary<byte, List<byte>> GetTable()
        {
            var code = new List<byte>();
            var table = new Dictionary<byte, List<byte>>();
            H.GetCode(table, code);
            return table;
        }
    }
}
