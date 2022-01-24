using System;

namespace homework3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a sentence: ");
            string text = Console.ReadLine();
            //string text = "Miss Polly had a poor dolly, who was sick. She called for the talled doctor Solly to come quick. He knocked on the DOOR like a actor in the healthcare sector.";

            Console.WriteLine("Which word are you looking for? ");
            string pattern = Console.ReadLine();

            string temp_text = "", temp_text2 = "", temp_text3 = "";
            int pattern_ASCII_total = 0, text_ASCII_total = 0, counter_45 = 0, pattern_star_ASCII_total = 0, text_star_ASCII_total = 0;
            bool loop_control = true, flag = true, clear_words_flag = true, temp1_control = true, temp2_control = true, temp3_control = true, temp_star_control = true;
            char convert_space = ' ';

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != 32 && ((text[i] < 97 || text[i] > 122) && (text[i] < 65 || text[i] > 90))) //converts all characters except spaces and letters to one space
                {
                    text = text.Replace(text[i], convert_space); //two space
                }
            }

            while (text.Contains("  ") || text.Contains("   ")) //converts the resulting two and three space characters to one space
            {
                text = text.Replace("  ", " "); //two space
                text = text.Replace("   ", " "); //three space
            }

            string[] words = text.Split(' ');
            string[] clear_words_v2 = new string[words.Length];

            int lenght_counter = 0;
            for (int i = 0; i < words.Length; i++) //to print each word once
            {
                for (int j = 0; j < clear_words_v2.Length; j++)
                {
                    if (clear_words_v2[j] == words[i])
                    {
                        clear_words_flag = false;
                    }
                }
                bool not_null = string.IsNullOrEmpty(words[i]); //checks null or empty index
                if (clear_words_flag == true && not_null == false)
                {
                    clear_words_v2[lenght_counter] = words[i];
                    lenght_counter++;
                }
                clear_words_flag = true;
            }

            string[] clear_words = new string[lenght_counter];

            for (int i = 0; i < clear_words.Length; i++) //Without null and empty indexes in clear_words_v2 array
            {
                clear_words[i] = clear_words_v2[i];
            }


            for (int j = 0; j < clear_words.Length; j++)
            {
                temp_text = words[j];
                for (int i = 0; i < pattern.Length; i++)
                {
                    if (pattern[i] != 45 && pattern.Length == temp_text.Length) //if the letter is not - and the searched word and the word in the sentence are equal letters
                    {   //collects the ASCII codes of all letters in the word
                        pattern_ASCII_total += pattern[i];
                        text_ASCII_total += temp_text[i];
                    }
                    else if (pattern[i] == 45) //if the letter is -
                    {
                        for (int b = 0; b < pattern.Length; b++)
                        {
                            if (pattern[b] == 45) //check if every letter is -
                            {
                                counter_45 += 45;
                            }
                        }
                        if (counter_45 == pattern.Length * 45) //if every letter is -
                        {
                            for (int a = 0; a < clear_words.Length; a++)
                            {
                                temp_text2 = clear_words[a];
                                if (temp_text2.Length == pattern.Length) //checks for words of the same length as the searched word
                                {
                                    Console.WriteLine(temp_text2);
                                    temp2_control = false; //to check if the searched pattern exists (please see the 'if' on line 170)
                                }
                            }
                        }
                        else
                        {
                            counter_45 = 0; //counter must be 0 for next word check
                        }
                    }
                    else if (pattern[i] == 42 && pattern.Length == 1) //if searched word *, print every words
                    {
                        for (int z = 0; z < clear_words.Length; z++)
                        {
                            Console.WriteLine(clear_words[z]);
                            temp_star_control = false;
                        }
                        loop_control = false;
                    }
                    else if (pattern[i] == 42) //if the letter is *
                    {
                        for (int n = 0; n < pattern.Length; n++)
                        {
                            if (pattern[n] != 42) //collects the ASCII codes of all letters that is not *
                            {
                                pattern_star_ASCII_total += pattern[n];
                            }
                        }
                        for (int k = 0; k < clear_words.Length; k++)
                        {
                            temp_text3 = clear_words[k];
                            int m_control = 0;

                            for (int l = 0; l < pattern.Length; l++) //checks letters of the pattern
                            {
                                for (int m = m_control; m < temp_text3.Length; m++) //checks letters of the text
                                {
                                    if (temp_text3[m] == pattern[l]) //if letters of the pattern and letters of the text are the same
                                    {
                                        text_star_ASCII_total += temp_text3[m];
                                        flag = false;
                                    }
                                    if (!flag)
                                    {
                                        m_control = m + 1;
                                        flag = true;
                                        break;
                                    }
                                }
                            }
                            if (pattern_star_ASCII_total == text_star_ASCII_total && pattern_star_ASCII_total != 0) //prints words with equal ASCII codes to the screen (for *)
                            {
                                Console.WriteLine(temp_text3);
                                temp3_control = false;
                            }
                            text_star_ASCII_total = 0;
                        }
                    }

                    if (counter_45 >= pattern.Length * 45)
                    {
                        loop_control = false;
                        break;
                    }
                }

                if (loop_control == false)
                {
                    break;
                }

                if (pattern_ASCII_total == text_ASCII_total && pattern_ASCII_total != 0) //prints words with equal ASCII codes to the screen (for -)
                {
                    Console.WriteLine(temp_text);
                    temp1_control = false;
                }
                pattern_ASCII_total = 0; text_ASCII_total = 0;
            }

            if (temp1_control && temp2_control && temp3_control && temp_star_control)
            {
                Console.WriteLine("The word you searched for was not found.");
            }

            Console.ReadLine();
        }
    }
}
