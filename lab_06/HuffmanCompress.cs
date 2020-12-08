using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CompressHuffman
{
    public class HuffmanCompress
    {
        private readonly string _fileName;
        private readonly string _tableName;

        private Dictionary<byte, uint> _countBytes;

        public HuffmanCompress(string fileName, string tableName)
        {
            _fileName = fileName;
            _tableName = tableName;
            _countBytes = new Dictionary<byte, uint>(256);
        }

        public void Execute(string outFile)
        {
            byte[] bytes = File.ReadAllBytes(_fileName);
            foreach (byte b in bytes)
            {
                if (_countBytes.ContainsKey(b))
                    _countBytes[b]++;
                else
                    _countBytes[b] = 1;
            }

            List<Tree.Node> nodes = new List<Tree.Node>();
            foreach (KeyValuePair<byte, uint> symbol in _countBytes)
            {
                Tree.Node node = new Tree.Node();
                node.Count = symbol.Value;
                node.S = symbol.Key;
                nodes.Add(node);
            }

            while (nodes.Count > 1)
            {
                nodes.Sort((f, s) => f.Count.Value.CompareTo(s.Count.Value));

                Tree.Node left = nodes[0];
                Tree.Node right = nodes[1];
                nodes.Remove(left);
                nodes.Remove(right);

                Tree.Node node = new Tree.Node();
                node.Count = left.Count + right.Count;
                node.L = left;
                node.R = right;

                nodes.Add(node);
            }

            Tree tree = new Tree(nodes[0]);
            var table = tree.GetTable();

            List<byte> zip = new List<byte>();
            foreach (byte sb in bytes)
            {
                List<bool> kek = new List<bool>();
                foreach (byte b in table[sb])
                {
                    kek.Add(b == 1);
                    zip.Add(b);
                }
            }

            List<byte> zipWrite = new List<byte>();
            for (int i = 0; i < zip.Count; i += 8)
            {
                byte b = 0;
                for (int j = 0; j < 8; ++j)
                {
                    b <<= 1;

                    if (i + j < zip.Count)
                        b += zip[i + j];
                    else
                        b += (byte)0;
                }
                zipWrite.Add(b);
            }

            File.WriteAllBytes(outFile, zipWrite.ToArray());

            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(_tableName))
            {
                serializer.Serialize(sw, tree.H);
            }
        }
    }
}
