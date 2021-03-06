// --------------------------------------------------------------------------
//  <copyright file="Program.cs" company="Omnicell Inc.">
//      Copyright (c) 2016 Omnicell Inc. All rights reserved.
// 
//      Reproduction or transmission in whole or in part, in 
//      any form or by any means, electronic, mechanical, or otherwise, is 
//      prohibited without the prior written consent of the copyright owner.
//  </copyright>
// --------------------------------------------------------------------------

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
            ConsoleKeyInfo cki;
            do
            {
                string command = prompt ? InputPrompt("action") : args[0].ToLower();
                switch (command)
                {
                    case "genkeycerts":
                        Console.WriteLine("not implimented.");
                        break;

                    case "sign":
                        string phrase = prompt && args.Length < 1 ? InputPrompt("phrase") : args[1].ToLower();
                        string certName = prompt && args.Length < 2 ? InputPrompt("keyCertName") : args[3].ToLower();
                        var helper1 = new DigitalSignatureHelper();
                        var sign = helper1.GetDigitalSignature(phrase, certName);
                        Console.WriteLine(sign);
                        break;

                    case "verify":

                        string phrase2 = prompt && args.Length < 1 ? InputPrompt("phrase") : args[1].ToLower();
                        string signature = prompt && args.Length < 2 ? InputPrompt("signature") : args[2].ToLower();
                        string certName2 = prompt && args.Length < 3 ? InputPrompt("keyCertName") : args[3].ToLower();
                        var helper2 = new DigitalSignatureHelper();
                        var result = helper2.VerifySignature(phrase2, signature, certName2);
                        Console.WriteLine(result);
                        break;

                    case "signAndverify":

                        string phrase3 = prompt && args.Length < 1 ? InputPrompt("phrase") : args[1].ToLower();
                        string certName3 = prompt && args.Length < 2 ? InputPrompt("keyCertName") : args[3].ToLower();
                        var helper3 = new DigitalSignatureHelper();
                        var sign3 = helper3.GetDigitalSignature(phrase3, certName3);
                        var result3 = helper3.VerifySignature(phrase3, sign3, certName3);
                        Console.WriteLine(result3);
                        break;

                    case "installcert":

                        var helper4 = new DigitalSignatureHelper();
                        helper4.InstallCertificate();
                        break;

                    default:
                        Console.WriteLine("invalid action");
                        break;
                }

                Console.WriteLine("Enter Any key to retry. Esc to exit.");
                cki = Console.ReadKey();
            } while (cki.Key != ConsoleKey.Escape);
        }

        private static string InputPrompt(string key)
        {
            Console.Write($"{key}: ");
            var value = Console.ReadLine();
            return value;
        }
    }
}