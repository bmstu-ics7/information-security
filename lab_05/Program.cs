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
            if (opts.Signature == null)
            {
                opts.Signature = opts.File + ".sig";
            }

            try
            {
                if (opts.Check)
                {
                    Signature.Check(opts.File, opts.Signature, opts.KeyFile);
                }
                else
                {
                    Signature.Create(opts.File, opts.Signature, opts.KeyFile);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
