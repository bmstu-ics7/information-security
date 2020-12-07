using System;
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
            Dictionary<byte, List<byte>> table =
                JsonConvert.DeserializeObject<Dictionary<byte, List<byte>>>(File.ReadAllText(_tableName));

            byte[] bytes = File.ReadAllBytes(_fileName);
        }
    }
}
