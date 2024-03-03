using System.Reflection;

namespace Task_2
{
    internal class Players
    {
        public string Username { get; set; }
        public int Score { get; set; }
        public int Attempt { get; set; }
        public static Players[] Select(string[] data)
        {
            Players[] users = new Players[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                users[i] = Players.Parse(data[i]);
            }
            return users;
        }
        public static Players Parse(string data)
        {
            string[] csv = data.Split(',');

            if (csv.Length != 3)
            {
                throw new FormatException("Incorrect Format");
            }

            Players result = new Players();

            result.Username = csv[0];
            result.Score = int.Parse(csv[1]);
            result.Attempt = int.Parse(csv[2]);

            return result;
        }
        public override string ToString()
        {
            return $"{Username} {Score} {Attempt}";
        }
    }
}
