using CommandLine;

namespace CompressHuffman
{
    public class Options
    {
        [Option('f', "file", Required = true, HelpText = "Input file.")]
        public string File { get; set; }

        [Option('o', "out", Required = true, HelpText = "Output file.")]
        public string Out { get; set; }

        [Option('t', "table", Required = false, HelpText = "File for table to decompress.", Default = "table.json")]
        public string Table { get; set; }

        [Option('d', "decompress", Required = false, HelpText = "Flag for enable decompress mode.")]
        public bool Decompress { get; set; }
    }
}
