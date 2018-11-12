using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DigitalSignatureSample2
{
    public class DigitalSignatureHelper
    {
        private readonly CertHelper _certHelper = new CertHelper();

        public string GetDigitalSignature(string text, string certSubject)
        {
            var sign = Sign(text, certSubject);
            var signstr = System.Convert.ToBase64String(sign);
            return signstr;
        }

        public bool VerifySignature(string text, string signature, string publicKeyCertSubject)
        {
            byte[] bytes = System.Convert.FromBase64String(signature);
            return VerifySignature(text, bytes, publicKeyCertSubject);
        }

        public bool VerifySignature(string text, byte[] signature, string certSubject)
        {
            var cert = _certHelper.GetCertFromStore(certSubject, StoreName.My);
            return VerifySignature(text, signature, cert);
        }

        private static bool VerifySignature(string text, byte[] signature, X509Certificate2 publicKeyCert)
        {
            RSACryptoServiceProvider csp = (RSACryptoServiceProvider) publicKeyCert.PublicKey.Key;

            // Hash the data
            SHA1Managed sha1 = new SHA1Managed();

            UnicodeEncoding encoding = new UnicodeEncoding();

            byte[] data = encoding.GetBytes(text);

            byte[] hash = sha1.ComputeHash(data);


            // Verify the signature with the hash

            return csp.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA1"), signature);
        }

        public byte[] Sign(string text, string certSubject)
        {
            var cert = _certHelper.GetCertFromStore(certSubject, StoreName.My);
            return Sign(text, cert);
        }

        private static byte[] Sign(string text, X509Certificate2 signingCert)
        {
            var csp = (RSACryptoServiceProvider)signingCert.PrivateKey;
            if (csp == null)
            {
                throw new Exception("No valid cert was found");
            }

            // Hash the data
            SHA1Managed sha1 = new SHA1Managed();

            UnicodeEncoding encoding = new UnicodeEncoding();

            byte[] data = encoding.GetBytes(text);

            byte[] hash = sha1.ComputeHash(data);

            // Sign the hash

            return csp.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));
        }
    }
}
