using System.Text.Json;

namespace Task_4
{
    public static class Json
    {
        public const string location = @"../../../userData.json";
        public const string loggerLocation = @"../../../userLog.json";
        public static List<Account> data = new();
        public static List<Account> Parse(string input)
        {
            List<Account> result = JsonSerializer.Deserialize<List<Account>>(input);

            if (result == null)
            {
                throw new FormatException("Invalid format while deserialization");
            }
            return result;
        }
        public static string ToJson(Account model)
        {
            string jsonObject = JsonSerializer.Serialize(model, new JsonSerializerOptions { WriteIndented = true });
            return jsonObject;
        }
        public static void AddNewUser(Account model)
        {
            data = Parse(File.ReadAllText(location));
            model.Id = data.Max(x => x.Id) + 1;
            var result = ToJson(model);
            Save(result);
        }
        public static void Save(string input)
        {
            if (!input.StartsWith("{") || !input.EndsWith("}"))
            {
                throw new FormatException("Input is not valid JSON format");
            }

            if (!File.Exists(location))
            {
                File.WriteAllText(location, "[]");
            }

            string existingJson = File.ReadAllText(location);

            if (!string.IsNullOrWhiteSpace(existingJson))
            {
                existingJson = existingJson.Trim(']');
            }

            input = $",{input}";

            File.WriteAllText(location, $"{existingJson}{input}]");
        }
        public static void SaveLog(string input)
        {
            string existingJson = File.ReadAllText(loggerLocation);

            if (!string.IsNullOrWhiteSpace(existingJson))
            {
                existingJson = existingJson.Trim(']');
            }
            File.WriteAllText(loggerLocation, $"{existingJson},\"{input}\"\n]");
        }
        public static Account NewUser()
        {
            Account account = new Account();

            Console.Write("Enter your first name: ");
            account.Firstname = Console.ReadLine();
            Console.Write("Enter your last name: ");
            account.Lastname = Console.ReadLine();
            Console.Write("Enter your identification Number: ");
            account.IdentificationNumber = Console.ReadLine();
            Console.Write("Enter password: ");
            account.Password = Console.ReadLine();
            return account;
        }
    }
}
