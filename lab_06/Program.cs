using System;
using CommandLine;

namespace CompressHuffman
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunProgram);
        }

        static void RunProgram(Options opts)
        {
            try
            {
                if (!opts.Decompress)
                {
                    new HuffmanCompress(opts.File, opts.Table).Execute();
                }
                else
                {
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
