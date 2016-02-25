using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Software_Development_Assignment_1
{
    class Program
    {
        public static void Main(string[] args)
        {
            bool programEnd = false;

            do
            {
                
            //Create Variables
            int userChoice, sentenceCount = 0, vowelCount = 0, consonantCount = 0, upperCaseCount = 0, lowerCaseCount = 0, totalLetterCount;
            string userInput = "", userInput2 = "";
            bool endTextInput = false;
            int[] freq = new int[(int)char.MaxValue]; // Array to store frequencies.
            char userExit = 'y';

            
            

                //Error Handling loop for user input
                do
                {
                    
                    //Ask user for their decision
                    Console.WriteLine("1. Do you want to enter text via the keyboard?");
                    Console.WriteLine("\n2. Do you want to read text in the text from a file?");
                    Console.WriteLine("\nPlease press '1' for option 1 or '2' for option 2");

                    //Allow user to input their choice
                    try
                    {
                        userChoice = Convert.ToInt32(Console.ReadLine());
                    }
                    //Gives user an error if a number is not entered
                    catch (Exception)
                    {
                        Console.WriteLine("\nERROR: You entered a letter or symbol!");
                        userChoice = 3;
                        
                    }
                    //If the user enters either '1' or '2', break from the do..while loop
                    if (userChoice == 1 || userChoice == 2)
                        break;
                    //If the user enters something other than '1' or '2'
                    else
                        Console.WriteLine("\nERROR: You didn't press '1' or '2', Please try again \n");



                } while (userChoice != 1 && userChoice != 2); //Go back to ask user decision if input is NOT '1' or '2'


                //Switch statement
                switch (userChoice)
                {
                    case 1:
                        
                        Console.WriteLine("\nPlease enter one or more sentences of text,\npress 'Enter' to add a new sentence, \nafter the last sentence please add a * to the end");
                        while (endTextInput == false)
                        {
                            
                            
                            userInput = Console.ReadLine();
                            

                            userInput2 = userInput2 + ' ' + userInput;

                            longWords(userInput2);

                            foreach (char c in userInput)
                            {
                                
                                if (c == '*')
                                    endTextInput = true;

                            }

                            if (userInput.Length == 0 || userInput == "*" || userInput == " ")
                                sentenceCount--;

                            sentenceCount++;
                            
                        }



                        break;


                    case 2:
                        
                        userInput2 = File.ReadAllText(@"..\..\..\myText.txt");
                        Console.WriteLine("Contents of myText.txt:\n{0}", userInput2);
                        foreach (char c in userInput2)
                        {
                            if (c == '.' || c == '!' || c == '?')
                                sentenceCount++;

                        }

                        longWords(userInput2);


                        break;


                }

                // Foreach loop - Calculate number of vowels, consonants, upper and lower case.
                foreach (char c in userInput2)
                {
                    if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u' || c == 'A' || c == 'E' || c == 'I' || c == 'O' || c == 'U')
                        vowelCount++;
                    if (c == 'b' || c == 'c' || c == 'd' || c == 'f' || c == 'g' || c == 'h' || c == 'j' || c == 'k' || c == 'l' || c == 'm' || c == 'n' || c == 'p' || c == 'q' || c == 'r' || c == 's' || c == 't' || c == 'v' || c == 'w' || c == 'x' || c == 'y' || c == 'z' || c == 'B' || c == 'C' || c == 'D' || c == 'F' || c == 'G' || c == 'H' || c == 'J' || c == 'K' || c == 'L' || c == 'M' || c == 'N' || c == 'P' || c == 'Q' || c == 'R' || c == 'S' || c == 'T' || c == 'V' || c == 'W' || c == 'X' || c == 'Y' || c == 'Z')
                        consonantCount++;
                    if (char.IsUpper(c))
                        upperCaseCount++;
                    if (char.IsLower(c))
                        lowerCaseCount++;
                    if (c == '*')
                        continue;
                    if (c == ' ')
                        continue;
                    freq[(int)c]++;

                }

                //Total Letter Count
                totalLetterCount = lowerCaseCount + upperCaseCount;



                //Output analysis of text
                Console.WriteLine("\nNumber of sentences entered: {0}", sentenceCount);
                Console.WriteLine("Number of vowels: {0}", vowelCount);
                Console.WriteLine("Number of consonants: {0}", consonantCount);
                Console.WriteLine("Number of uppercase letters: {0}", upperCaseCount);
                Console.WriteLine("Number of lowercase letters: {0}", lowerCaseCount);
                Console.WriteLine("Mood of text: {0}", moodOfText(userInput2));






                //Frequency of letters
                for (int i = 0; i < (int)char.MaxValue; i++)
                {
                    if (freq[i] > 0 &&
                    char.IsLetter((char)i))
                    {

                        Console.WriteLine("Letter: {0}  Frequency: {1:f2}%", (char)i, ((double)freq[i] / totalLetterCount) * 100);
                    }
                }

                int loopCount = 1;
                do
                {

                    Console.WriteLine("This is the ending do while loop, iteration: {0}", loopCount);
                //Ask user if they would like to quit or continue
                Console.WriteLine("Would you like to perform more text analysis? Y/N");
                
                    try
                    {
                        userExit = Convert.ToChar(Console.ReadLine());
                    }

                    catch (Exception)
                    {
                        Console.WriteLine("ERROR: Invalid Input!");
                    }

                    if (userExit == 'y' || userExit == 'Y')
                    {
                        break;
                    }

                    if (userExit == 'n' || userExit == 'N')
                    {
                        programEnd = true;
                        break;
                    }

                    else
                        Console.WriteLine("ERROR: You did not enter 'y' or 'n'");
                    loopCount++;

                } while (userExit != 'y' || userExit != 'n' || userExit != 'N' || userExit != 'Y');

            } while (programEnd == false);
        }

        //This method stores any words longer than 7 letters
        public static void longWords(string userInput2)
        {
            string longWordStore = "";

            string[] wordArray = Regex.Split(userInput2, @"\W+");
            //Calulates length of word, if the word is greater than 7, it gets saved.
            foreach (string word in wordArray)
            {
                if (word.Length > 7)
                    longWordStore = longWordStore + word + " ";
                else
                    continue;
            }
           
            File.WriteAllText(@"..\..\..\longWords.txt", longWordStore); //Save the long words to this location

        }

        public static string moodOfText(string userInput2)
        {
            string mood = "";
            int postiveCounter = 0, negativeCounter = 0;
            string[] wordArray = Regex.Split(userInput2, @"\W+");
            string[] positiveWords = File.ReadAllLines(@"..\..\..\postiveWords.txt");
            string[] negativeWords = File.ReadAllLines(@"..\..\..\negativeWords.txt");

            //Gathers all words which exists in both the users input and the postive text file
            var listPositive = wordArray.Intersect(positiveWords);
            //Gathers all words which exists in both the users input and the postive text file
            var listNegative = wordArray.Intersect(negativeWords);

            //Each negative word found the negative counter is incresed by 1
            foreach (string s in listNegative)
                negativeCounter++;

            //Each positve word found the poisitve counter is increased by 1
            foreach (string s in listPositive)
                postiveCounter++;
            

            //Checks to see which counter has the highest value
            if (postiveCounter > negativeCounter)
                mood = "Happy";
            if (negativeCounter > postiveCounter)
                mood = "Unhappy";
            if (negativeCounter == postiveCounter)
                mood = "Neutral";

            return mood;
          
        }

        
    

        
    }
}

