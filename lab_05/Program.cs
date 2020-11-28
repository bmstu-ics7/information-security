using System;
using CommandLine;

namespace DigitalSignature
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
                if (opts.Check)
                {
                    Signature.Check(opts.File, opts.KeyFile);
                }
                else
                {
                    Signature.Create(opts.File, opts.KeyFile);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
