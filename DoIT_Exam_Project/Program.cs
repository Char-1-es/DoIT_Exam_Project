using System.Net.NetworkInformation;

namespace Task_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Asking for user input
            float usernumber1 = default;
            float usernumber2 = default;
            char operation = default;
            string stop = "";

            while (!stop.Equals("Stop", StringComparison.OrdinalIgnoreCase)) //Program will continue to work until user enters 's' or 'S'.
            {
                try
                {
                    Console.WriteLine("Calculator");
                    Console.WriteLine("----------");

                    Console.Write("Enter the first number: ");
                    usernumber1 = float.Parse(Console.ReadLine()!);
                    Console.Write("Enter the second number: ");
                    usernumber2 = float.Parse(Console.ReadLine()!);

                    Console.WriteLine("For addition, enter +; for substraction, enter -; for multiplication, enter *, for division, enter /.");
                    operation = char.Parse(Console.ReadLine()!);

                    //Actual code

                    Console.WriteLine(Calculations.Operation(operation, usernumber1, usernumber2));

                    
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine(ex.Message);
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
