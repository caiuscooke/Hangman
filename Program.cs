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

public class Hangman
{
    private Random random = new Random();
    public List<string> words = File.ReadLines("C:\\Users\\cooke\\source\\repos\\CalculatorImproved\\Words.txt").ToList();

    public string GetRandomWord(List<string> list)
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
    }

    public List<char> GetRandomWordList(string word)
    {
        List<char> list = new List<char>();
        foreach (char letter in word)
        {
            list.Add(letter);
        }
        return list;
    }

    public List<char> GetGameBoard(string word)
    {
        List<char> list = new List<char>();
        foreach (char c in word)
        {
            list.Add('*');
        }
        return list;
    }

    public void DisplayGameBoard(List<char> list)
    {
        Console.Write("Game Board --->  ");
        foreach (char c in list)
        {
            Console.Write(c);
            Console.Write(' ');
        }
        Console.WriteLine("\n");
    }

    public int GetLettersLeft(List<char> listToCheck)
    {
        int count = 0;
        foreach (char c in listToCheck)
        {
            if (c == '*')
            {
                count++;
            }
        }
        return count;
    }

    public void DisplayIncorrectLetters(List<char> listToDisplay)
    {
        foreach (char c in listToDisplay)
        {
            Console.Write(c);
            Console.Write(" ");
        }
        Console.WriteLine("\n");
    }

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
    }

    public void MainLoop()
    {
        string randomWord = GetRandomWord(words);
        List<char> hyphenList = GetGameBoard(randomWord);
        List<char> randomWordList = GetRandomWordList(randomWord);
        int incorrectGuesses = 0;
        List<char> incorrectlyGuessedLetters = new List<char>();
        char userInput;
        bool canHint = true;

        DisplayHangMan(incorrectGuesses);

        while (true)
        {

            int lettersLeft = GetLettersLeft(hyphenList);

            int index = 0;

            DisplayGameBoard(hyphenList);

            Console.Write("Type your guess... ");
            string userInputString = Console.ReadLine();
            Console.Clear();
            if (incorrectGuesses > 2 && incorrectGuesses <= 4)
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.Clear();
            }
            else if (incorrectGuesses > 4 && incorrectGuesses <= 6)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Clear();
            }

            if (userInputString == "guess now")
            {
                DisplayHangMan(incorrectGuesses);
                DisplayIncorrectLetters(incorrectlyGuessedLetters);
                DisplayGameBoard(hyphenList);

                userInputString = Console.ReadLine();

                if (userInputString == randomWord)
                {
                    Console.WriteLine("\n" + "Congratulations! That was the correct word. You win!");
                    break;
                }
                else
                {
                    Console.WriteLine("\n" + "Too bad!!");
                    DisplayHangMan(6);
                    Console.WriteLine($"The correct word was {randomWord}.");
                    break;
                }
            }
            else if (userInputString == "exit")
            {
                Console.WriteLine("\n" + "Sorry to see you go!");
                break;
            }
            else if (userInputString == "hint" && canHint == true && lettersLeft != 1)
            {
                bool hintAcquired = false;
                while (hintAcquired == false)
                {
                    int randomIndex = random.Next(randomWord.Length - 1);
                    if (hyphenList[randomIndex] == '*')
                    {
                        Console.WriteLine(randomWord[randomIndex]);
                        hintAcquired = true;
                    }
                }
                DisplayHangMan(incorrectGuesses);

            }
            else
            {
                if (!char.TryParse(userInputString, out userInput))
                {
                    Console.WriteLine("\n" + "Type one letter at a time only. If you'd like to guess the full word, type \"guess now\" please." + "\n" + "If you'd like to quit, type \"quit\".");
                }
            }

            if (char.TryParse(userInputString, out userInput))
            {
                if (!randomWord.Contains(userInput))
                {
                    incorrectGuesses++;
                    DisplayHangMan(incorrectGuesses);
                    incorrectlyGuessedLetters.Add(userInput);
                    if (incorrectGuesses == 6)
                    {
                        Console.WriteLine("You lost! Too bad :/");
                        Console.WriteLine($"The correct word was {randomWord}");
                        break;
                    }
                }
                else
                {
                    foreach (char c in randomWordList)
                    {
                        if (userInput == c)
                        {
                            hyphenList[index] = c;
                        }
                        index++;
                    }
                    DisplayHangMan(incorrectGuesses);
                }
            }

            if (incorrectlyGuessedLetters != null)
            {
                DisplayIncorrectLetters(incorrectlyGuessedLetters);
            }

            if (!hyphenList.Contains('*'))
            {
                Console.WriteLine("\n" + "Congratulations! You guessed the whole word.");
                break;
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
        Hangman hangman = new Hangman();
        hangman.MainLoop();
        Console.ReadKey();
    }
}