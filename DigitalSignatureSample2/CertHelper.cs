using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace DigitalSignatureSample2
{
    public class CertHelper
    {
        public X509Certificate2 GetCertFromStore(string certName, StoreName storeName)
        {
            X509Store my = new X509Store(storeName, StoreLocation.LocalMachine);

            my.Open(OpenFlags.ReadOnly);
            RSACryptoServiceProvider csp = null;

            foreach (X509Certificate2 cert in my.Certificates)
            {

                if (cert.Subject.Contains(certName))

                {
                    return cert;
                }
            }

            throw new Exception($"certificate with name '{certName}' not found in store '{storeName}'");
        }
    }
}