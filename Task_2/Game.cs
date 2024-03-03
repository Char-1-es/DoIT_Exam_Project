namespace Task_2
{
    internal class Game
    {
        public static int Easy()
        {
            Random rand = new Random();
            int randomNumber = rand.Next(1, 15);
            return randomNumber;
        }
        public static int Medium()
        {
            Random rand = new Random();
            int randomNumber = rand.Next(1, 25);
            return randomNumber;
        }
        public static int Hard()
        {
            Random rand = new Random();
            int randomNumber = rand.Next(1, 50);
            return randomNumber;
        }
    }
}
