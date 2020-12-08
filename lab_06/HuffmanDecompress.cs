using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CompressHuffman
{
    public class HuffmanDecompress
    {
        private readonly string _fileName;
        private readonly string _tableName;

        public HuffmanDecompress(string fileName, string tableName)
        {
            _fileName = fileName;
            _tableName = tableName;
        }

        public void Execute(string outFile)
        {
            Tree.Node head =
                JsonConvert.DeserializeObject<Tree.Node>(File.ReadAllText(_tableName));

            byte[] bytes = File.ReadAllBytes(_fileName);
            List<bool> zip = new List<bool>();
            for (int i = bytes.Length - 1; i >= 0; --i)
            {
                byte b = bytes[i];
                for (int j = 0; j < 8; ++j)
                {
                    zip.Add(b % 2 == 1);
                    b >>= 1;
                }
            }
            zip.Reverse();

            List<byte> unzip = new List<byte>();
            Tree.Node current = head;
            foreach (bool b in zip)
            {
                if (b)
                    current = current.R;
                else
                    current = current.L;

                if (current.S != null)
                {
                    unzip.Add(current.S.Value);
                    current = head;
                }
            }

            File.WriteAllBytes(outFile, unzip.ToArray());
        }
    }
}
