using System;
using System.IO;
using System.Security.Cryptography;

namespace DigitalSignature
{
    public class Signature
    {
        static public void Check(string fileName, string signFile, string keyFile)
        {
            string rsaParams = File.ReadAllText(keyFile);
            RSA rsa = RSA.Create();
            rsa.FromXmlString(rsaParams);

            RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            rsaDeformatter.SetHashAlgorithm("SHA512");

            byte[] hash = GenerateHash(fileName);
            byte[] sign = File.ReadAllBytes(signFile);

            if (rsaDeformatter.VerifySignature(hash, sign))
            {
                Console.WriteLine("The signature is valid.");
            }
            else
            {
                Console.WriteLine("The signature is not valid.");
            }
        }

        static public void Create(string fileName, string signFile, string keyFile)
        {
            RSA rsa = RSA.Create();
            RSAPKCS1SignatureFormatter rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
            rsaFormatter.SetHashAlgorithm("SHA512");

            byte[] hash = GenerateHash(fileName);
            byte[] signedBytes = rsaFormatter.CreateSignature(hash);
            File.WriteAllBytes(signFile, signedBytes);

            string RSAKeyInfo = rsa.ToXmlString(false);
            File.WriteAllText(keyFile, RSAKeyInfo);
        }

        static private byte[] GenerateHash(string fileName)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] bytes = File.ReadAllBytes(fileName);
            return sha512.ComputeHash(bytes);
        }
    }
}
