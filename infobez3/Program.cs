namespace infobez3;

class Program
{
    static string Decrypt(string encryptedText)
    {
        List<List<char>> matrix = CreateMatrixForDecryption(encryptedText);
        string plaintext = "";

        int numRows = matrix.Count;
        int numCols = matrix.Max(row => row.Count);

        for (int i = 0; i < numCols; i++)
        {
            for (int j = 0; j < numRows; j++)
            {
                if (i < matrix[j].Count)
                {
                    plaintext += matrix[j][i];
                }
            }
        }

        return plaintext;
    }

    static List<List<char>> CreateMatrixForDecryption(string encryptedText)
    {
        List<List<char>> matrix = new List<List<char>>();
        int currentIndex = 0;

        // Determine the number of rows needed
        int numRows = (int)Math.Ceiling((double)encryptedText.Length / 6);

        for (int i = 0; i < numRows; i++)
        {
            List<char> row = new List<char>();

            for (int j = 0; j < 6; j++)
            {
                if (currentIndex < encryptedText.Length)
                {
                    row.Add(encryptedText[currentIndex]);
                    currentIndex++;
                }
            }

            matrix.Add(row);
        }

        return matrix;
    }
    static string Sipher(List<List<char>> matrix)
    {
        string sipher = "";
        List<char> next_char = new List<char>();
        while(matrix.Count != 0)
        {
            for (int i = 0; i < 6; i++)
            {
                int counter = matrix.Count;
                if(counter > i)
                {
                    next_char = matrix[i];
                    if (next_char.Count != 0)
                    {
                        sipher += next_char[0];
                        next_char.RemoveAt(0);
                    }
                    else
                    {
                        matrix.RemoveAt(0);
                        break;
                    }
                }
                else
                {
                    i = 0;
                    next_char = matrix[i];
                    if (next_char.Count != 0)
                    {
                        sipher += next_char[0];
                        next_char.RemoveAt(0);
                    }
                    else
                    {
                        matrix.RemoveAt(0);
                        break;
                    }
                }
                
            }
        }
        return sipher;
    }
    static List<List<char>> CreateMatrix(List<string> text)
    {
        List<List<char>> sipher_symbols = new List<List<char>>();
step1:
        int counter = 1;
extra_step:
        for(int i = 0; i < text.Count; i++)
        {
            List<char> next_chars = new List<char>();
            if(counter < 7)
            {
                for (int j = 0; j < counter; j++)
                {
                    next_chars.Add(char.Parse(text[0]));
                    text.Remove(text[0]);
                    
                }
                if(next_chars.Count != 0)
                {
                    counter++;
                    sipher_symbols.Add(next_chars);
                }
            }
            else
            {
                int j = 5;
step2:
                List<char> next_chars2 = new List<char>();
                while(j > 0)
                {
                    if(text.Count > 0)
                    {
                        next_chars2.Add(char.Parse(text[0]));
                        text.Remove(text[0]);
                        j--;
                    }
                    else
                    {
                        sipher_symbols.Add(next_chars2);
                        goto final;
                    }
                }
                j = next_chars2.Count - 1;
                counter--;
                sipher_symbols.Add(next_chars2);
                if(j == 0 && text.Count != 0)
                {
                    goto step1;
                }
                else if(text.Count != 0)
                {
                    goto step2;
                }
                else
                {
                    break;
                }
            }
            
        }
        if(text.Count != 0)
        {
            goto extra_step;
        }
final:
        return sipher_symbols;
    }
    static void Main(string[] args)
    {
        string path = "/Users/hyperion/Documents/original_text.txt";
        List<string> text = new List<string>();
        //File.ReadAllText(string path)
        using (StreamReader reader = new StreamReader(path))
        {
            while (!reader.EndOfStream)
            {
                int nextChar = reader.Read();
                if (nextChar != -1)
                {
                    char character = (char)nextChar;
                    if(character.ToString() != "," && character.ToString() != ".")
                    {
                        text.Add(character.ToString().ToUpper());
                    }
                    
                }
            }
            
        }
        List<List<char>> result = CreateMatrix(text);
        string sipher = Sipher(result);
        string desipher = Decrypt(sipher);
        Console.Write($"Вывести зашифрованный текс: ");
        int num = Convert.ToInt16(Console.ReadLine());
        if(num == 1)
        {
            Console.WriteLine(sipher);
        }
        else if(num == 2)
        {
            Console.WriteLine(desipher);
        }
        else
        {
            Console.WriteLine("Не верная команда.");
        }

    }
}

