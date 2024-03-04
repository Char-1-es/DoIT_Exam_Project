using System;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Location of users data
            const string fileLocation = @"../../../userData.json";
            //Location of users operation history
            const string loggerLocation = @"../../../userLog.json";

            string stop = ""; //Variable 'stop' - to let user stop the program
            
            while (!stop.Equals("Stop", StringComparison.OrdinalIgnoreCase))
            {
                try
            {
                Console.WriteLine("ATM Application");
                Console.WriteLine("---------------");

                Console.WriteLine("Enter 1 to log into the system");
                Console.WriteLine("Enter 2 for registration");
                char initialChoice = char.Parse(Console.ReadLine());

                string data = File.ReadAllText(fileLocation); //data is whole text from string data
                var jsonData = Json.Parse(data); //Here we parse that data into Account object
                int number = default; 


                if (initialChoice == '1')
                {
                    Console.Write("Enter your identificationNumber: ");
                    string idnumber = Console.ReadLine();

                    if (idnumber.Length != 11 || !DigitOnly(idnumber)) //Checks if length of idnumber is 11 and if it contains only digits
                    {
                        throw new Exception("Identification number must be 11 digits long");
                    }

                    Console.Write("Enter your password: ");
                    string password = Console.ReadLine();

                    if (password.Length != 4 || !DigitOnly(idnumber)) //Checks if length of password is 11 and if it contains only digits
                        {
                        throw new Exception("Password must be 4 digits long");
                    }

                    bool userExistance = false;
                    for (int i = 0; i < jsonData.Count; i ++) 
                    { //Checks if the the idnumber and password (both entered by user) actually exists in JsonData
                        if (jsonData[i].IdentificationNumber == idnumber && jsonData[i].Password == password)
                        {
                            userExistance = true;
                            number = i; //This number indicates in which object is that idnumber and password,
                                        //this is key value that we'll use later in this app
                            break;
                        }
                    }
                    if (userExistance == false)
                    {
                        
                        Console.WriteLine("User doesn't exist");
                        Console.WriteLine("Register to continue");
                        //Registers new user
                        var register = Json.NewUser();

                        string json = File.ReadAllText(fileLocation);
                        List<Account> accounts;
                        accounts = JsonSerializer.Deserialize<List<Account>>(json);

                        if (accounts.Exists(acc => acc.IdentificationNumber == register.IdentificationNumber ||acc.Password == register.Password))
                        {//Prevents user to register with id and pass that already exist in system (In json file)
                          Console.WriteLine("Account already exists.");
                        }
                        else
                        {
                          Json.AddNewUser(register);

                          Console.WriteLine("Your account:");
                          Console.WriteLine("-------------");
                          Console.WriteLine($"Identification Number: {register.IdentificationNumber}");
                          Console.WriteLine($"Password: {register.Password}");
                          Console.WriteLine("Log in to continue operations");
                        }//Adds new user if id and pass are unique
                    }
                    if (userExistance)
                    {
                        Console.WriteLine("Enter 1 - Check your balance");
                        Console.WriteLine("Enter 2 - Fill up your balance");
                        Console.WriteLine("Enter 3 - withdrawal money");
                        Console.WriteLine("Enter 4 - See operation history");

                        Console.Write("Choose operations: ");
                        char operationChoice = char.Parse(Console.ReadLine());

                        if (operationChoice == '1')//Check Balance
                        {
                            //Checks balance in jsondata list of account object, number represents
                            //the exact object, that user is using 
                            var money = Account.CheckBalance(jsonData[number]);
                            Console.WriteLine($"Balance: {money} GEL");

                            //Logs the data of user checking the balance
                            string data1 = File.ReadAllText(loggerLocation); 
                            string logData = $"User {jsonData[number].Firstname} {jsonData[number].Lastname} -- checked balance on {DateTime.Now}.";
                            Json.SaveLog(logData);
                        }
                        else if (operationChoice == '2') //FillUp Balance
                        {
                            Console.WriteLine("Enter the amount of money");
                            //Aks user to enter the amount of money to fillup the balance
                            int money = int.Parse(Console.ReadLine());

                            //Changes the balance of the current Account object in Json
                            string jsonData2 = File.ReadAllText(fileLocation);
                            List<Account> accounts = JsonSerializer.Deserialize<List<Account>>(jsonData2);
                            if (number >= 0 && number < accounts.Count)
                            {
                                accounts[number].Balance += money; 
                            }
                            string updatedJson = JsonSerializer.Serialize(accounts, new JsonSerializerOptions { WriteIndented = true });
                            File.WriteAllText(fileLocation, updatedJson);

                            //Logs the data of user filling up the balance
                            string data1 = File.ReadAllText(loggerLocation);
                            string logData = $"User {jsonData[number].Firstname} {jsonData[number].Lastname} -- Filled up balance by {money} GEL on {DateTime.Now}.";
                            Json.SaveLog(logData);
                        }
                        else if (operationChoice == '3')
                        {
                            Console.WriteLine("Enter the amount of money");
                            //Aks user to enter the amount of money to withdraw money from the balance
                            int money = int.Parse(Console.ReadLine());

                            //Changes the balance of the current Account object in Json
                            string jsonData3 = File.ReadAllText(fileLocation);
                            List<Account> accounts = JsonSerializer.Deserialize<List<Account>>(jsonData3);
                            
                            if (number >= 0 && number < accounts.Count)
                            {
                                accounts[number].Balance -= money; 
                            }
                            string updatedJson = JsonSerializer.Serialize(accounts, new JsonSerializerOptions { WriteIndented = true });
                            File.WriteAllText(fileLocation, updatedJson);

                            //log
                            string data1 = File.ReadAllText(loggerLocation);
                            string logData = $"User {jsonData[number].Firstname} {jsonData[number].Lastname} -- Withdrew {money} GEL from balance on {DateTime.Now}.";
                            Json.SaveLog(logData);
                        }
                        else if (operationChoice == '4') //Print operation history
                        {
                            string logHistory = File.ReadAllText(loggerLocation);
                            List<string> items = JsonSerializer.Deserialize<List<string>>(logHistory);

                            foreach (var item in items)
                            {
                                if (item.Contains(jsonData[number].Firstname))
                                {
                                    Console.WriteLine(item);
                                }
                            }
                        }
                    }
                }
                else if (initialChoice == '2') //Register new user
                {
                    var register = Json.NewUser();
                    string json = File.ReadAllText(fileLocation);
                    List<Account> accounts;
                    accounts = JsonSerializer.Deserialize<List<Account>>(json);

                    if (accounts.Exists(acc => acc.IdentificationNumber == register.IdentificationNumber ||acc.Password == register.Password))
                    {//Prevents user to register with id and pass that already exist in system (In json file)
                          Console.WriteLine("Account already exists.");
                    }
                    else
                    {
                          Json.AddNewUser(register);

                          Console.WriteLine("Your account:");
                          Console.WriteLine("-------------");
                          Console.WriteLine($"Identification Number: {register.IdentificationNumber}");
                          Console.WriteLine($"Password: {register.Password}");
                          Console.WriteLine("Log in to continue operations");
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
        static bool DigitOnly(string input)
        {
            foreach (char c in input)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }
    }
}