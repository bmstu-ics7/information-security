using System;
using System.IO;
using System.Security.Cryptography;

namespace DigitalSignature
{
    public class Signature
    {
        static private byte[] secret = {
            95,  14,  200, 68,  204, 42,  214, 198,
            177, 111, 78,  43,  173, 113, 124, 194,
            4,   89,  220, 195, 44,  62,  76,  220,
            10,  120, 99,  77,  78,  254, 55,  27,
        };

        static public void Check(string fileName, string keyFile)
        {
            byte[] bytes = File.ReadAllBytes(fileName);

            string rsaParams = File.ReadAllText(keyFile);
            RSA rsa = RSA.Create();
            rsa.FromXmlString(rsaParams);

            RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            rsaDeformatter.SetHashAlgorithm("SHA256");

            if (rsaDeformatter.VerifySignature(secret, bytes))
            {
                Console.WriteLine("The signature is valid.");
            }
            else
            {
                Console.WriteLine("The signature is not valid.");
            }
        }

        static public void Create(string outFile, string keyFile)
        {
            RSA rsa = RSA.Create();
            RSAPKCS1SignatureFormatter rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
            rsaFormatter.SetHashAlgorithm("SHA256");

            byte[] signedBytes = rsaFormatter.CreateSignature(secret);
            File.WriteAllBytes(outFile, signedBytes);

            string RSAKeyInfo = rsa.ToXmlString(false);
            File.WriteAllText(keyFile, RSAKeyInfo);
        }
    }
}
