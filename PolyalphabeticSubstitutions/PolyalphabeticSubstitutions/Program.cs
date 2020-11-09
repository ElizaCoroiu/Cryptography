using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyalphabeticSubstitutions
{
    class Program
    {
        static void Main(string[] args)
        {
            Vigenere message = new Vigenere("ATTACKATDAWN", "LEMON");
            Console.WriteLine(message);

            Console.ReadKey();
        }
    }

    public class Vigenere
    {
        public string Text { get; set; }
        public string Key { get; set; }
        public string Cipher { get; set; }
        public string Decipher { get; set; }

        public Vigenere(string text, string key)
        {
            this.Text = text;
            this.Key = key;
            this.Key = generateKey(this.Text, this.Key);
            this.Cipher = Encrypt(this.Text, this.Key);
            this.Decipher = Decrypt();
        }

        static string generateKey(string text, string key)
        {
            int x = text.Length;
          
            for (int i = 0; ; i++)
            {
                if (x == i)
                    i = 0;
                if (key.Length == text.Length)
                    break;
                key += (key[i]);
            }
            return key;
        }

        static string Encrypt(string str, string key)
        {
            string cipher_text = "";

            for (int i = 0; i < str.Length; i++)
            {
                // converting in range 0-25 
                int x = (str[i] + key[i]) % 26;

                // convert into alphabets(ASCII) 
                x += 'A';

                cipher_text += (char)(x);
            }
            return cipher_text;
        }

        string Decrypt()
        {
            String orig_text = "";

            for (int i = 0; i < this.Cipher.Length &&
                                    i < this.Key.Length; i++)
            {
                // converting in range 0-25 
                int x = (this.Cipher[i] -
                            this.Key[i] + 26) % 26;

                // convert into alphabets(ASCII) 
                x += 'A';
                orig_text += (char)(x);
            }
            return orig_text;
        }

        public override string ToString()
        {
            return "Text: " + this.Text + "\n"
                + "Key: " + this.Key + "\n"
                + "Cipher: " + this.Cipher + "\n"
                + "Decipher: " + this.Decipher + "\n";
        }
    }
}
