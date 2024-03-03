using Task_1;
using System.Numerics;

namespace Task_1
{
    internal class Calculations
    {
        public static float Addition (float usernum1, float usernum2)
        {
            return usernum1 + usernum2;
        }
        public static float Substraction(float usernum1, float usernum2)
        {
            return usernum1 - usernum2;
        }
        public static float Multiplication(float usernum1, float usernum2)
        {
            return usernum1 * usernum2;
        }
        public static float Division(float usernum1, float usernum2)
        {
            return usernum1 / usernum2;
        }
        public static float Operation (char operation, float usernum1, float usernum2)
        {
            float result = default;
            if (operation == '+')
            {
                result = Addition(usernum1, usernum2);
            }
            else if (operation == '-')
            {
                result = Substraction(usernum1, usernum2);
            }
            else if (operation == '*')
            {
                result = Multiplication(usernum1, usernum2);
            }
            else if (operation == '/')
            {
                if (usernum2 == 0)
                {
                    throw new DivideByZeroException("Can't divide by zero");
                }
                result = Division(usernum1, usernum2);
            }
            else
            {
                throw new FormatException("Invalid character");
            }
            return result;
        }
        public static void Print (float result)
        {
            Console.WriteLine(result);
        }
    }
}

////Asking for user input
//float usernumber1 = default;
//float usernumber2 = default;
//char operation = default;
//char stop = default;

//while (char.ToLower(stop) != 's') //Program will continue to work until user enters 's' or 'S'.
//{
//    Console.WriteLine("Enter two numbers:");
//    try
//    {
//        usernumber1 = float.Parse(Console.ReadLine()!);
//        usernumber2 = float.Parse(Console.ReadLine()!);
//    }
//    catch (Exception e)
//    {
//        Console.WriteLine(e.Message);
//    }
//    Console.WriteLine("For addition, enter +; for substraction, enter -; for multiplication, enter *, for division, enter /.");
//    try
//    {
//        operation = char.Parse(Console.ReadLine()!);
//    }
//    catch (Exception e)
//    {
//        Console.WriteLine(e.Message);
//    }

//    //Actual code

//    Console.WriteLine(Calculations.Operation(operation, usernumber1, usernumber2));

//    Console.WriteLine("To stop enter s or anything else to continue:");
//    try
//    {
//        stop = char.Parse(Console.ReadLine()!);

//    }
//    catch (Exception e)
//    {

//        Console.WriteLine("Enter only single character", e);
//    }
//}