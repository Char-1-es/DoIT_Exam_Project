using System.Drawing;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string stop = ""; //Variable for stopping game
            const string filelocation = @"../../../Data.csv";
            while (!stop.Equals("Stop", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    Console.WriteLine("Number guessing game");
                    Console.WriteLine("--------------------");

                    Console.WriteLine("Enter your username");
                    string username = Console.ReadLine();

                    Console.WriteLine("Difficulty levels: Easy - 1 point, medium - 5 point, hard - 10 point.");
                    Console.WriteLine("Enter 1 for easy");
                    Console.WriteLine("Enter 2 for medium");
                    Console.WriteLine("Enter 3 for hard");


                    char gameMode = char.Parse(Console.ReadLine());

                    int randomNumber = default; //Randomly generated number
                    int point = default; //Variable for counting user's point
                    int userNum = default;
                    int attempts = 10;

                    //Lets user to choose difficulty level of game
                    if (gameMode == '1')//EASY
                    {
                        randomNumber = Game.Easy();
                        Console.Write("Enter the number from 1-15, you have '10' attempts: ");
                        point = 1;
                    }
                    else if (gameMode == '2')//MEDIUM
                    {
                        randomNumber = Game.Medium();
                        Console.Write("Enter the number from 1-25, you have '10' attempts: ");
                        point = 5;
                    }
                    else if (gameMode == '3')//HARD
                    {
                        randomNumber = Game.Hard();
                        Console.Write("Enter the number from 1-50, you have '10' attempts: ");
                        point = 10;
                    }
                    else
                    {
                        throw new FormatException("Please, enter only - Easy, Medium or Hard.");
                    }

                    int count = 0;
                    //Main logic
                    for (int i = 0; i < 10; i++)
                    {
                        attempts--;
                        userNum = int.Parse(Console.ReadLine());

                        if (gameMode == '1')
                        {
                            if (userNum < 1|| userNum > 15)//Checks if userNum is in the range 1-15
                            {
                                throw new Exception("Enter a number from 0 to 15");
                            }
                        }
                        else if (gameMode == '2')
                        {
                            if (userNum < 1 || userNum > 25)//Checks if userNum is in the range 1-25
                            {
                                throw new Exception("Enter a number from 0 to 25");
                            }
                        }
                        if (userNum < 1 || userNum > 50)//Checks if userNum is in the range 1-50
                        {
                            throw new Exception("Enter a number from 0 to 50");
                        }
                        if (userNum > randomNumber)
                        {
                            Console.WriteLine($"[{userNum}] is higher than the target number");

                        }
                        else if (userNum < randomNumber)
                        {
                            Console.WriteLine($"[{userNum}] is lower than the target number");
                        }
                        else if (userNum == randomNumber)
                        {
                            File.AppendAllText(@"../../../Data.csv", $"{username},{point+attempts},{i}" + Environment.NewLine);
                            Console.WriteLine("Congratulations, you win");
                            break;
                        }
                        count++;
                        Console.Write($"You have \'{attempts}\' attempts left: ");
                    }
                    if (count == 10)
                    {
                        File.AppendAllText(filelocation, $"{username},0,10" + Environment.NewLine);
                        Console.WriteLine("Game over. You are welcome to try again!");
                    }

                    string[] data = File.ReadAllLines(filelocation);

                    var userObject = Players.Select(data);

                    //Groups objects by usernames and sums their scores
                    IEnumerable<IGrouping<string, Players>> groups = userObject
                        .GroupBy(x => x.Username.ToLower())
                        .OrderByDescending(x => x.Sum(x => x.Score))
                        .ThenBy(x => x.Key.ToLower());

                    //Prints leaderboard of 10 best player
                    Console.WriteLine("To see result enter R");
                    char res = char.Parse(Console.ReadLine());

                    if (res == 'R' || res == 'r')
                    {
                        Console.WriteLine("Results:");

                        Console.WriteLine("Top 10 players: ");
                        foreach (var group in groups)
                        {
                            Console.WriteLine($"{group.Key} - Points: {group.Sum(x => x.Score)}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("To Stop, enter stop or anything else to continue:");
                stop = Console.ReadLine();
            }
        }
    }
}
