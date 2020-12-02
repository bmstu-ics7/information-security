using CommandLine;

namespace DigitalSignature
{
    public class Options
    {
        [Option('f', "file", Required = true, HelpText = "File for check or create.")]
        public string File { get; set; }

        [Option('s', "sign", Required = false, HelpText = "File for signature.")]
        public string Signature { get; set; }

        [Option('k', "key", Required = false, HelpText = "File with key for check or create.", Default = "key.pub")]
        public string KeyFile { get; set; }

        [Option('c', "check", Required = false, HelpText = "Set for check file.")]
        public bool Check { get; set; }
    }
}
