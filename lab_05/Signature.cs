using System;
using System.IO;
using System.Security.Cryptography;

namespace DigitalSignature
{
    public class Signature
    {
        static private byte[] secret = {
            59,  4,   248, 102, 77,
            97,  142, 201, 210, 12,
            224, 93,  25,  41,  100,
            197, 213, 134, 130, 135,
        };

        static public void Check(string fileName, string keyFile)
        {
            byte[] bytes = File.ReadAllBytes(fileName);

            string rsaParams = File.ReadAllText(keyFile);
            RSA rsa = RSA.Create();
            rsa.FromXmlString(rsaParams);

            RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            rsaDeformatter.SetHashAlgorithm("SHA1");

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
            rsaFormatter.SetHashAlgorithm("SHA1");

            byte[] signedBytes = rsaFormatter.CreateSignature(secret);
            File.WriteAllBytes(outFile, signedBytes);

            string RSAKeyInfo = rsa.ToXmlString(false); 
            File.WriteAllText(keyFile, RSAKeyInfo);
        }
    }
}
