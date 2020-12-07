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

            public void GetCode(Dictionary<byte, List<bool>> table, List<bool> code)
            {
                if (Symbol != null)
                {
                    table[Symbol.Value] = new List<bool>(code);
                }

                if (Left != null)
                {
                    code.Add(false);
                    Left.GetCode(table, code);
                    code.Remove(code[code.Count - 1]);
                }

                if (Right != null)
                {
                    code.Add(true);
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

        public Dictionary<byte, List<bool>> GetTable()
        {
            var code = new List<bool>();
            var table = new Dictionary<byte, List<bool>>();
            _head.GetCode(table, code);
            return table;
        }
    }
}
