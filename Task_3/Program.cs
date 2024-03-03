using System.IO;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Xml;
using System.Xml.Serialization;
using static System.Formats.Asn1.AsnWriter;

namespace Task_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string stop = ""; //Variable for stopping game
            const string fileLocation = @"../../../dataxml.xml";

            List<string> words = new List<string>
            {
            "apple", "banana", "orange", "grape", "kiwi",
            "strawberry", "pineapple", "blueberry", "peach", "watermelon",
            };

            while (!stop.Equals("Stop", StringComparison.OrdinalIgnoreCase)) //Program will continue to work until user enters 's' or 'S'.
            {
                try
                {
                    Console.WriteLine("Wordle");
                    Console.WriteLine("------");
                    Random random = new Random();
                    string randomWord = words[random.Next(0, 10)];

                    string replacedWord = new string('_', randomWord.Length);

                    int attempts = 6;
                    int check = 0;
                    int check1 = 0;
                    int points = 0;

                    Console.Write("Enter your username: ");
                    string userName = Console.ReadLine();
                    Console.WriteLine("Guess the character of the word. You have six attempts.");
                    Console.WriteLine($"Word: {replacedWord}");
                    char userInput = default;

                    for (int i = 0; i < 6; i++)
                    {
                        Console.Write($"({attempts} attempts left) Enter a single character: ");
                        userInput = char.Parse(Console.ReadLine());

                        attempts--;

                        if (randomWord.Contains(userInput))
                        {
                            check++; //This is used to check, how many times user answered right
                            for (int j = 0; j < randomWord.Length; j++)
                            {
                                if (randomWord[j] == userInput)
                                {
                                    replacedWord = replacedWord.Remove(j, 1).Insert(j, userInput.ToString());
                                }
                            }
                            Console.WriteLine($"Word: {replacedWord}");
                        }
                        else
                        {
                            Console.WriteLine($"The word doesn't contain \'{userInput}\'");
                        }
                        if (replacedWord == randomWord)
                        {
                            check1++; //This is used to check if user guessed word only by just attempts
                            Console.WriteLine("You win");
                            points = 10 + attempts;
                            break;
                        }
                    }
                    if (check == 0) //If user could't guess any letter of the word, he loses
                    {
                        Console.WriteLine("Game over. You've failed on all attempts.");
                    }
                    else if (check > 0 && check1 == 0) //If the user guessed the letter for more than one time or he didn't guess the entire word already
                    {
                        Console.WriteLine("Now, enter the entire word");
                        string userWord = Console.ReadLine();
                        if (userWord == randomWord)
                        {
                            Console.WriteLine($"Congratulations, {userName}, you win");
                            points = 5 + attempts;
                        }
                        else
                        {
                            Console.WriteLine($"Game over, {userName}. The Correct answer is \"{randomWord}\"");
                        }
                    }
                    //Save player
                    Xml.Save(userName, points);

                    //Leaderboard
                    string data = File.ReadAllText(fileLocation);

                    var x = Xml.Parse(data);
                    
                    IEnumerable<IGrouping<string, User>> groups = x
                        .GroupBy(x => x.Username.ToLower())
                        .OrderByDescending(x => x.Sum(x => x.Score))
                        .ThenBy(x => x.Key.ToLower());

                    Console.WriteLine("To see result enter R");
                    char res = char.Parse(Console.ReadLine());

                    if (res == 'R' || res == 'r')
                    {
                        Console.WriteLine("Top 10 players: ");
                        foreach (var group in groups)
                        {
                            Console.WriteLine($"{group.Key} - Points: {group.Sum(x => x.Score)}");
                        }
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("To Stop, enter stop or anything else to continue:");
                
                stop = Console.ReadLine();
                Console.ReadKey();
            }
        }
    }
}