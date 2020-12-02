using CommandLine;

namespace DigitalSignature
{
    public class Options
    {
        [Option('f', "file", Required = true, HelpText = "File for check or create.")]
        public string File { get; set; }

        [Option('s', "sign", Required = true, HelpText = "File for signature.")]
        public string Signature { get; set; }

        [Option('k', "key", Required = true, HelpText = "File with key for check or create.")]
        public string KeyFile { get; set; }

        [Option('c', "check", Required = false, HelpText = "Set for check file.")]
        public bool Check { get; set; }
    }
}
