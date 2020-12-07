using System;
using System.Collections.Generic;

namespace CompressHuffman
{
    public class Tree
    {
        public class Node
        {
            public byte? Symbol = null;
            public uint Count;

            public Node Left = null;
            public Node Right = null;

            public void GetCode(Dictionary<byte, List<byte>> table, List<byte> code)
            {
                if (Symbol != null)
                {
                    table[Symbol.Value] = new List<byte>(code);
                }

                if (Left != null)
                {
                    code.Add(0);
                    Left.GetCode(table, code);
                    code.Remove(code[code.Count - 1]);
                }

                if (Right != null)
                {
                    code.Add(1);
                    Right.GetCode(table, code);
                    code.Remove(code[code.Count - 1]);
                }
            }
        }

        private Node _head;

        public Tree(Node head)
        {
            _head = head;
        }

        public Dictionary<byte, List<byte>> GetTable()
        {
            var code = new List<byte>();
            var table = new Dictionary<byte, List<byte>>();
            _head.GetCode(table, code);
            return table;
        }
    }
}
