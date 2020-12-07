using CommandLine;

namespace CompressHuffman
{
    public class Options
    {
        [Option('f', "file", Required = true, HelpText = "File for compress.")]
        public string File { get; set; }

        [Option('t', "table", Required = false, HelpText = "File for compress.", Default = "table.json")]
        public string Table { get; set; }

        [Option('d', "decompress", Required = false, HelpText = "Flag for enable decompress mode.")]
        public bool Decompress { get; set; }
    }
}
