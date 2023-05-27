/*
 * Lets take a look at how we can make a simple console-based game in c#. The game we're going to be making together is hangman. If you've never played hangman, don't worry because we're about to play it together.
 * 
 * Now that we've played hangman and you all know a bit about coding in c#, take time to write pseudo-code on how you think we could get this game to work. It should look something like this
 * 
 * 1. Make a list of words
 * 2. Choose a random word from that list
 * 3. Write something in the console representing how many letters are in the word. So if the word was "cheese" the console would write "_ _ _ _ _ _" or whatever symbol we decide.
 * 4. Let the user type in a letter, and store it in a variable
 * 5. If the user guesses correctly, put that letter with the hyphens
 * 6. If the user guesses incorrectly, draw the board with the hangman design, and print out the letters that have been incorrectly guessed so far
 * 7. If the user guesses correctly and the word is complete, end the game, otherwise continue lines 4-6
 * 8. If the user guesses incorrectly and the hangman is fully drawn, end the game 
 * 
 * Make sense so far? Now let's look at what we'd do in real code.
*/

using Microsoft.VisualBasic;

class StringCharManipulation
{
    public Random random = new Random();

    public string GetRandom(List<string> list)
    {
        List<string> desiredList = new List<string>();

        foreach (string word in list)
        {
            if (word.Length >= 6)
            {
                desiredList.Add(word);
            }
        }
        return desiredList[random.Next(0, desiredList.Count - 1)];
    } //gets the random word by searching through the list made by the text doc

    public List<char> ToCharList(string word)
    {
        List<char> list = new List<char>();
        foreach (char letter in word)
        {
            list.Add(letter);
        }
        return list;
    } // makes a list out of a string

    public List<char> ToListOfSymbols(string word, char symbols)
    {
        List<char> list = new List<char>();
        foreach (char c in word)
        {
            list.Add(symbols);
        }
        return list;
    } // makes a list of symbols for each letter of a word

    public void PrintList(List<char> list)
    {
        foreach (char c in list)
        {
            Console.Write(c);
            Console.Write(' ');
        }
        Console.WriteLine("\n");
    }  // prints a list of chars seperated by a space

    public int CharListCount(List<char> listToCheck, char symbolToCount)
    {
        int count = 0;
        foreach (char c in listToCheck)
        {
            if (c == symbolToCount)
            {
                count++;
            }
        }
        return count;
    } // counts how many chars are in a list of chars

    

}

class Hangman
{
    public List<string> listOfWords = File.ReadLines("C:\\Users\\Caius Cooke\\Source\\Repos\\caiuscooke\\Hangman\\Words.txt").ToList();
    public StringCharManipulation conversion = new StringCharManipulation();
    public Random random = new Random();

    public void DisplayHangMan(int incorrectGuesses)
    {
        if (incorrectGuesses == 0)
        {
            Console.WriteLine("==================");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("==================");
        }
        if (incorrectGuesses == 1)
        {
            Console.WriteLine("==================");
            Console.WriteLine("|              |");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("==================");
        }
        if (incorrectGuesses == 2)
        {
            Console.WriteLine("==================");
            Console.WriteLine("|              |");
            Console.WriteLine("|              0");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("==================");
        }
        if (incorrectGuesses == 3)
        {
            Console.WriteLine("==================");
            Console.WriteLine("|              |");
            Console.WriteLine("|              0");
            Console.WriteLine("|             ~=~");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("==================");
        }
        if (incorrectGuesses == 4)
        {
            Console.WriteLine("==================");
            Console.WriteLine("|              |");
            Console.WriteLine("|              0");
            Console.WriteLine("|             ~=~");
            Console.WriteLine("|              |");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("==================");
        }
        if (incorrectGuesses == 5)
        {
            Console.WriteLine("==================");
            Console.WriteLine("|              |");
            Console.WriteLine("|              0");
            Console.WriteLine("|             ~=~");
            Console.WriteLine("|              |");
            Console.WriteLine("|             /");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("==================");
        }
        if (incorrectGuesses == 6)
        {
            Console.WriteLine("==================");
            Console.WriteLine("|              |");
            Console.WriteLine("|              0");
            Console.WriteLine("|             ~=~");
            Console.WriteLine("|              |");
            Console.WriteLine(@"|             / \");
            Console.WriteLine("|");
            Console.WriteLine("|");
            Console.WriteLine("==================");
        }
    } // displays 6 different types of hangman boards

    public void MainLoop()
    {

        string randomWord = conversion.GetRandom(listOfWords); //stores the random word
        List<char> randomWordList = conversion.ToCharList(randomWord); //creates a list out of the random word
        List<char> hyphenList = conversion.ToListOfSymbols(randomWord, '*'); //stores the list of symbols that will represent the letters in the word

        int incorrectGuesses = 0; //keeps track of how many times the word is guessed incorrectly
        List<char> incorrectlyGuessedLetters = new List<char>(); //keeps track of the incorrectly guessed letters
        char userInput; // used to store the users input as a character
        bool canHint = true; // can hint if not used, can only use the hint once


        while (true)
        {
            int lettersLeft = conversion.CharListCount(hyphenList, '*'); //gets how many letters are left everytime the game loops (for use in the hints)

            Console.Write("Type your guess... "); // prompts the user to input
            string userInputString = Console.ReadLine(); //gets the user input as a string

            if (incorrectGuesses > 2 && incorrectGuesses <= 4) //changes background color to yellow if you have 3 or 4 wrong guesses
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.Clear();
            }
            else if (incorrectGuesses > 4 && incorrectGuesses <= 6) //changes background color if you have 5 or 6 wrong guesses
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Clear();
            }

            if (userInputString == "guess now") //if the user types in guess now
            {
                userInputString = Console.ReadLine(); // read the user input as a string since it will be a full word

                if (userInputString == randomWord) // if what they typed in is the full answer
                {
                    Console.WriteLine("\n" + "Congratulations! That was the correct word. You win!"); // they win
                    break; // break the loop
                }
                else //if its not matching the answer
                {
                    Console.WriteLine("\n" + "Too bad!!"); 
                    DisplayHangMan(6); //make the score max out
                    Console.WriteLine($"The correct word was {randomWord}."); //displays the word
                    break; //ends the game
                }
            }
            else if (userInputString == "exit") //if they type in exit
            {
                Console.WriteLine("\n" + "Sorry to see you go!");
                break; //exit the game
            }

            else if (userInputString == "hint" && canHint == true && lettersLeft != 1) // if they type in hint, they havent tried before, and there's not just one letter left to guess
            {
                bool hintAcquired = false; // initialize the local variable used to break the next loop
                while (hintAcquired == false)
                {
                    int randomIndex = random.Next(randomWord.Length - 1); //get a random number that aligns with the index of the randomword
                    if (hyphenList[randomIndex] == '*') //if the randomized index in the hyphen list is a star, that means the user hasn't guessed this letter yet
                    {
                        Console.WriteLine(randomWord[randomIndex]); //print out that letter
                        hintAcquired = true; // change this variable to true to break out of the while loop
                    }
                }
            }
            else // if the user types in neither of these three things and...
            {
                if (!char.TryParse(userInputString, out userInput)) // if the user's input cant be converted to a char
                {
                    Console.WriteLine("\n" + "Type one letter at a time only. If you'd like to guess the full word, type \"guess now\" please." + "\n" + "If you'd like to quit, type \"quit\".");
                }
                else // if the users input can be converted to a char
                {
                    if (!randomWord.Contains(userInput)) //if the char input isn't in the randomWord
                    {
                        incorrectGuesses++; // increase the incorrect guesses by 1
                        incorrectlyGuessedLetters.Add(userInput); // add the char to the list that stores incorrectly guessed letters
                        if (incorrectGuesses == 6) // if you have the maximum number of incorrectly guessed letters
                        {
                            Console.WriteLine("You lost! Too bad :/");
                            Console.WriteLine($"The correct word was {randomWord}");
                            break; //exit the game loop
                        }
                    }
                    else // if the user's input IS in the random word
                    {
                        int index = 0; //resets the index searching variable to 0 everytime the game loops

                        foreach (char c in randomWordList) // iterate over each item in the list-ifyed random word
                        {
                            if (userInput == c) // if there is a match 
                            {
                                hyphenList[index] = c; // replace the letter in the symbol list to be displayed with the correctly guessed letter
                            }
                            index++; // increase the index each time it loops
                        }
                    }
                }
            }

            if (!hyphenList.Contains('*')) // after the users inputs, check if the displayed list has any symbols left, and if it doesnt then
            {
                Console.WriteLine("\n" + "Congratulations! You guessed the whole word."); // you win
                break; //exit the game
            }
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Black;
        Console.Clear();
        Console.WriteLine("Welcome to Hangman!\n");
        Console.WriteLine("Guess one letter at a time. If you get too many wrong, you lose! If you get them all right, you win!");
        Console.WriteLine("You may type \"guess now\" to guess the entire word in one line if you think you have it correct." + "\n");
        Console.WriteLine("You may also type \"exit\" to exit and \"hint\" to get a hint when you're stuck!");
        Hangman hangman = new Hangman();
        hangman.MainLoop();
        Console.ReadKey();
    }
}