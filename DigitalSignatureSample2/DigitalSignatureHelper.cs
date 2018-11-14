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

        public void InstallCertificate()
        {
            var base64Encoded = @"MIIKVQIBAzCCChEGCSqGSIb3DQEHAaCCCgIEggn+MIIJ+jCCBhsGCSqGSIb3DQEH
AaCCBgwEggYIMIIGBDCCBgAGCyqGSIb3DQEMCgECoIIE / jCCBPowHAYKKoZIhvcN
AQwBAzAOBAgcW2puxiIXpQICB9AEggTYMXLViaPlnOBV3 / 03VSUR0OJVb7cIfP5K
JcsPMWpAO2oK1dqtn4luYiltIb3VV0g69 + ZQ2CVqGzdpNqLyfUbG9z5J4UhyzZKk
tItjTI1csHnJrrHurqnqhGnTOVBSp5axS8AAA5b402T6VKXEi + Qf1gpyXB7PqZch
4kJkhfJ7EvK9bsY3XYINvguYKp5tQ1KM7E8VuDfMJdijfocuT7ax1ggfZXLOzAwh
7fVOjvs4ZWKB / NB992uY0wagb0JN9KZbTEGv83Tbm5FUd6YZtUp6 / gDaHFZqbmD3
4kQhcWQlOMOSaPzetQRo5ylGANUEJDvhpVd390ZD + nYVC9hMm2fv1wOxrcLFkm7A
+ AdRkHruqulOqBsHHWMg80vlNoX3DPX5c3qlIHovDGLNApoF48UNAPnxN8Aqwl7T
R7yX3nul4adLMkNcVgfdlwEkblbHQzpwMZ3l4AJxt7iJPH0KKxx1A / JKwIMpk8hC
Rm3sVKLwVG4sKxYKtCZJQQV / JttLmV2w60z / LkW4IyP6Qpmm1D / 5EG / 8ycDnohXL
vQD3TCfHUiyB14uWudd5r + 6lQSSB547LzButR3blOn2lWRk8k6NY0C7CC / mu7ARQ
mivGNH9vKHCxJ8r / Z + omsWsEs2X / WY6b / 2J4JIvwFhnaJrD28BqIUXDVcZc0dJza
        CqNOCM3GfQojXAttNt4Is5ZHVXPel7z5lUb32xT5HO4IESkuAX5TuWB1MW2kyz6t
MEr / nyA2Q98Tz0g5j5Pj + 3POO / ZTvHaT9tBFn877qNRVrbfx1njaq7tOFxXjWwgk
Zky0EbcORcZS7p6gMUu30eBwFPhdYO25kErpI / MjJBXfdutVHUyW9nXa1xCpwGF2
kBydX5MMvEQbMSMY7Qeo4pPgzy5UvPWLP7DJdUcgHjQPKRsGAGbBnDOYchSM5h30
zso1K5US6aBwuGZSLG0Lgvg6k + TocsjgP3WSClFqt8aXjn4u5Sxh0ftbecyQcleH
B2u1hQZbt00w1URmcMvdajDJZzfjGhPj1DJ2faZ5hzZJDyF6AVF + kgmaRbEudbuf
OpivAeC8ZZ5Ae5O5hhya6P0IWHlFMmKKo4pJffhEXt3sCQkVT5GR1PBWQBUXHwHQ
uAew9tsE7FVO62Y3dmO2NN9S8OTxGErlOhoEqsKmIps5m + wmpgOgVf / Q7aRPMfzW
kWjhl5TpQn8KXHr1yEDpaHAySDh0aYgqFKgCrPhBuwEEbtWxECLejJjbnMP9 / QZU
OIwGV7WPuwJ1zXdnO8wB1SiHmO9 + j + YML5NrHz1 + CWniNkW5xCcF + o6dcZzrKov5
y3ZCIh8FWMe47Kc / c0cCmQx8417EaqyOlKJSSygV4Hrq0Q8MHPEoO94skDsxAlYt
40jnxIImvwIqavsuBfRh6VMAl581Jz + 8yqquRy8 / ebu7Lb + QSqHD8wjOztsQUj60
Ms9asDW4nr66KHsEBiPIft / CmyuW9N6eFOozFMJe1F / k88qZIMiqKuc2m1LhOfEr
I5ziCFNF4jrr6M8zValaDfCq1Vb2uW / XXR7xIKhE4SuDx9bfeRMQH0HF9Va0DsCO
+ cXTHlX0P8HN8 / 6lGCeuc + MN5jaGMOND0pM1LQCJxAdaxBfZWJLjWawsWtN77GP6
3yV2SqkhQKw3VWLb1vNmWDGB7jANBgkrBgEEAYI3EQIxADATBgkqhkiG9w0BCRUx
BgQEAQAAADBdBgkqhkiG9w0BCRQxUB5OAGwAcAAtADUAZgAzADQAYwBmAGYAYQAt
AGUAMAAyADgALQA0AGMANQBjAC0AOQAzAGUAYQAtAGMAOQBkAGEANQA4ADgANQAz
ADcAYgA4MGkGCSsGAQQBgjcRATFcHloATQBpAGMAcgBvAHMAbwBmAHQAIABSAFMA
QQAgAFMAQwBoAGEAbgBuAGUAbAAgAEMAcgB5AHAAdABvAGcAcgBhAHAAaABpAGMA
IABQAHIAbwB2AGkAZABlAHIwggPXBgkqhkiG9w0BBwagggPIMIIDxAIBADCCA70G
CSqGSIb3DQEHATAcBgoqhkiG9w0BDAEGMA4ECClYw9 / 2mUjQAgIH0ICCA5ABWybI
YHenIDKgfk3xCii8sa57kqQFz / omE1tRGeyEHEJ2Wbg4pKz0enqrmNhaAEVtZ1pn
xWw2oqlH4ldSB0Sw3wCovwq2RTUhu0Qtnci1vqStqVA / 5 / UvuMrdEB3q8aG7r6q9
yjBDEVAoa + ZYvEnYAozkGaigTrmBq0wZAuB0CRcmMO4EYZLaJJ1lG74E +/ fyR0L4
UwguwGDSt1t06nsnplk4Nq9CX5CXNd9tAw6Naw4xPcLiw13ZzdEEx1DBveY / 4Gt3
jmFQGdQKUvdA7ghc1wl614kB9APH3eeiC3WE / X8NRLj5kXQAqo3rC3PQwJ2SGpmI
XSRozccv2S8SiD / 5ec0Ys0eIYGO74vkSxavjEHLzwrDAvakV9NgfMXVP0Nnnrkl3
4ip3ogXNRBIbRGIf3Ty3D7REMnn5wsTdElwcei0o8wXM55tGCgPckEb4wbIyyGBY
6nPj8yjjzcSg5joBWua4R8vha / pcShAeXs / gKFbeb4omRtCluaV / ErE9f2CM6H86
7xTBlXGCI89xGiJXqf / TI5sX2Y7VLyw9M / BspEOSSAtYSa0TqVrPjRcoy6UAOf1J
DukTZU + jb / +YALqzu7mc / qy0QFdVNf7YtYhzwEyiu45FL17SoyntaqAWYfnXInhM
p9XoX6eWd0ssbXpygphYZil0kcH2JNEayAeeqoe7bOe64k + TyyHGoCszuMABLQhx
lhGMmwMOGJXHLOaJeQKnKLAecLkIqGGVMWAEHmgk2N9lq5t4LX3rtuqCw9sVEq3H
uqrR / 3YNSV9vzFNMCvuYEo0TybritQClpe / WUhHfrOZLjie77PzTTa3w / PlWPaRs
Hywxb +/ NQ0jhMl3JuuUCnSMtUKIOVKeMpJ3sg0mC2bhd7pdUn00VcqO + BDvvDyRY
wlLnpWdeySJ8u6aYtYRcga7sIp6OmmljTDF6ik5GMO6 + yD02PBCVD / g1JWKFyMgw
gTsZ + 5WrvAqkywyMmeMM4q5PBNuw / THgYRTRdGfBmWifqq5fuhmlGlXXMss3k3dI
hadrZzTJzahw7baBlo7ofT9OgGEIOIFo3InaB3TcszRfNfKJ2Pvsd9b4cVSmGqE3
VGuRV0Iz3kWg3SoyqCnCrURYSnI6krnWngxmXv8AlOToyfEmrmBlr8ZR11bFybBk
0 / aScvQk9yrd5Xhf2J5q2IQgh4ImpkHoOM + C4RU0BvE7TjXhDpGbqbD7cZcwOzAf
MAcGBSsOAwIaBBSaM2UkWZLNvECW9i1J4FCToZgbpQQU3yAHcbm3L2v4VLYw / Cnn
EAZqDkACAgfQ";

            X509Certificate2 certificate = new X509Certificate2(
                Convert.FromBase64String(base64Encoded),
                string.Empty,
                X509KeyStorageFlags.MachineKeySet);
            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);

            store.Open(OpenFlags.ReadWrite);
            store.Add(certificate);
            store.Close();
        }
    }
}
