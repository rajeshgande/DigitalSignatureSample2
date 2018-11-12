using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignatureSample2
{
    class Program
    {
        // private readonly CertGenerator _certGenerator = new CertGenerator();

        static void Main(string[] args)
        {
            bool prompt = args.Length < 1;
         
            string command = prompt ? InputPrompt("action") : args[0].ToLower();
            switch (command)
            {
                case "genkeycerts":
                    Console.WriteLine("not implimented.");
                    break;

                case "sign":
                    string sampleText = prompt ? InputPrompt("text") : args[1].ToLower();
                    var helper1 = new DigitalSignatureHelper();
                    var sign = helper1.GetDigitalSignature(sampleText, "testSigningCertificate");
                    Console.WriteLine(sign);
                    break;

                case "verify":

                    string sampleText2 = prompt && args.Length < 1 ? InputPrompt("text") : args[1].ToLower();
                    string signtext = prompt && args.Length < 2 ? InputPrompt("signature") : args[2].ToLower();
                    var helper2 = new DigitalSignatureHelper();
                    var result = helper2.VerifySignature(sampleText2, signtext, "testSigningCertificate");
                    Console.WriteLine(result);
                    break;

                case "signAndverify":

                    string sampleText3 = prompt && args.Length < 1 ? InputPrompt("text") : args[1].ToLower();
                    var helper3 = new DigitalSignatureHelper();
                    var sign3 = helper3.GetDigitalSignature(sampleText3, "testSigningCertificate");
                    var result3 = helper3.VerifySignature(sampleText3, sign3, "testSigningCertificate");
                    Console.WriteLine(result3);
                    break;

                default:
                    break;
            }

            Console.WriteLine("Enter Any key to exit...");
            Console.ReadKey();

        }

        private static string InputPrompt(string key)
        {
            Console.Write($"{key}: ");
            var value = Console.ReadLine();
            return value;
        }
    }
}
