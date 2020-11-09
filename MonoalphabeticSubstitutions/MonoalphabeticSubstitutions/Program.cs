using System;
using System.Collections.Generic;
using System.Text;

namespace MonoalphabeticSubstitutions
{
    class Program
    {
        static void Main(string[] args)
        {
            CaesarCipher message1 = new CaesarCipher("ABCDEFGHIJKLMOPQRSTUVWXYZ", "23");
            Console.WriteLine(message1);
            
            ParticularCaesar message2 = new ParticularCaesar("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            Console.WriteLine(message2);

            ROT13 message3 = new ROT13("Complex is better than complicated");
            Console.WriteLine(message3);

            SimpleSubstitution message4= new SimpleSubstitution("flee at once", "zebras");
            Console.WriteLine(message4);

            Console.ReadKey();
        }
    }

    public abstract class AlgoritmSimetric
    {
        public string input { get; set; }
        public string key { get; set; }
        public string cipher { get; set; }
        public string decrypted { get; set; }

        public abstract StringBuilder Encrypt(string input, string key);
        public abstract StringBuilder Decrypt();


        public override string ToString()
        {
            return "Text: " + this.input + "\n"
                + "key: " + this.key + "\n"
                + "cipher: " + this.cipher + "\n"
                + "decrypted: " + this.decrypted + "\n";
        }
    }

    public class CaesarCipher : AlgoritmSimetric
    {
        public CaesarCipher(string text, string shift)
        {
            this.input = text;
            this.key = shift;
            this.cipher = Encrypt(this.input, this.key).ToString();
            this.decrypted = Decrypt().ToString();
        }

        public override StringBuilder Encrypt(string input, string shift)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]))
                {
                    if (input[i] == ' ')
                    {
                        result.Append(" ");
                    }
                    else
                    {
                        char ch = (char)(((int)input[i] +
                                        int.Parse(shift) - 65) % 26 + 65);
                        result.Append(ch);
                    }
                }
                else
                {
                    if (input[i] == ' ')
                    {
                        result.Append(" ");
                    }
                    else
                    {
                        char ch = (char)(((int)input[i] +
                                        int.Parse(shift) - 97) % 26 + 97);
                        result.Append(ch);
                    }
                }
            }
            
            return result;
        }

        public override StringBuilder Decrypt()
        {
            return Encrypt(this.cipher, (26 - int.Parse(this.key)).ToString());
        }
    }

    public class ParticularCaesar : CaesarCipher
    {
        public ParticularCaesar(string text) : base(text, "3")
        {

        }
    }

    public class ROT13 : CaesarCipher
    {
        public ROT13(string input) : base(input, "13") { }
    }

    public class SimpleSubstitution : AlgoritmSimetric
    {
        public SimpleSubstitution(string input, string key)
        {
            this.input = input;
            this.key = key;
            this.cipher = Encrypt(this.input, this.key).ToString();
            this.decrypted = Decrypt().ToString();

        }

        public override StringBuilder Decrypt()
        {
            return Decipher(this.cipher, encoder);
        }

        public static string encoder = "";
        public override StringBuilder Encrypt(string input, string key)
        {
            encoder = Encode(key);

            return Cipher(input, encoder);
        }

        public string Encode(string key)
        {
            string encoded = "";
            // This array represents the 
            // 26 letters of alphabets 
            Boolean[] arr = new Boolean[26];

            // This loop inserts the keyword 
            // at the start of the encoded string 
            for (int i = 0; i < key.Length; i++)
            {
                if (key[i] >= 'A' && key[i] <= 'Z')
                {
                    // To check whether the character is inserted 
                    // earlier in the encoded string or not 
                    if (arr[key[i] - 65] == false)
                    {
                        encoded += (char)key[i];
                        arr[key[i] - 65] = true;
                    }
                }
                else if (key[i] >= 'a' && key[i] <= 'z')
                {
                    if (arr[key[i] - 97] == false)
                    {
                        encoded += (char)(key[i] - 32);
                        arr[key[i] - 97] = true;
                    }
                }
            }

            // This loop inserts the remaining 
            // characters in the encoded string. 
            for (int i = 0; i < 26; i++)
            {
                if (arr[i] == false)
                {
                    arr[i] = true;
                    encoded += (char)(i + 65);
                }
            }
            return encoded;
        }

        public StringBuilder Cipher(string input, string encoded)
        {
            StringBuilder cipher = new StringBuilder();

            // This loop ciphered the message. 
            // Spaces, special characters and numbers remain same. 
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] >= 'a' && input[i] <= 'z')
                {
                    int pos = input[i] - 97;
                    cipher.Append(encoded[pos]);
                }
                else if (input[i] >= 'A' && input[i] <= 'Z')
                {
                    int pos = input[i] - 65;
                    cipher.Append(encoded[pos]);
                }
                else
                {
                    cipher.Append(input[i]);
                }
            }
            return cipher;
        }

        public StringBuilder Decipher(string input, string encoded)
        {
            string plaintext = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            // Hold the position of every character (A-Z) 
            // from encoded string 
            Dictionary<char, int> enc = new Dictionary<char, int>();
            for (int i = 0; i < encoded.Length; i++)
            {
                enc[encoded[i]] = i;
            }

            StringBuilder decipher = new StringBuilder();

            // This loop deciphered the message. 
            // Spaces, special characters and numbers remain same. 
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] >= 'a' && input[i] <= 'z')
                {
                    int pos = enc[(char)(input[i] - 32)];
                    decipher.Append(plaintext[pos]);
                }
                else if (input[i] >= 'A' && input[i] <= 'Z')
                {
                    int pos = enc[input[i]];
                    decipher.Append(plaintext[pos]);
                }
                else
                {
                    decipher.Append(input[i]);
                }
            }
            return decipher;
        }
    }
}

