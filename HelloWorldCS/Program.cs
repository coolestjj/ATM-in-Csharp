using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        class Records
        {
            public string AccountId;
            public string User; 
            public decimal Balance;
        }

        private static List<Records> records = new List<Records>();

        private static void CreateAccount(List<Records> records)
        {
            var random = new Random();
            Console.Write("Name: ");
            var user = Console.ReadLine();
            var accountId = new string(
                Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 12)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            Records record = new Records()
            {
                AccountId = accountId,
                User = user,
                Balance = 0
            };
            records.Add(record);
            Console.WriteLine("Account Created.");
            Console.WriteLine("Account ID: " + accountId);
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            // Clear the console and Call Main again
            Console.Clear();
            Main(null);
        }   

        private static void AccountController(List<Records> records)
        {
            var systemShutdown = false;
            while (!systemShutdown)
            {
                Console.WriteLine("Enter Account ID: ");
                var input = Console.ReadLine();
                foreach (var account in records)
                {
                    if (input != null && input.Equals(account.AccountId))
                    {
                        Console.WriteLine($"Welcome {account.User}. Your Current Balance: {account.Balance}");
                        var atmModeClose = false;
                        while (!atmModeClose)
                        {
                            Console.WriteLine("Options:");
                            Console.WriteLine("1. View Balance");
                            Console.WriteLine("2. Deposit");
                            Console.WriteLine("3. Withdraw");
                            Console.WriteLine("4. Change Account");
                            Console.WriteLine("5. Quit");
                            Console.Write("Enter Operation: ");
                            input = Console.ReadLine(); 
                            switch (input)
                            {
                                case "1":
                                    Console.Clear();
                                    Console.WriteLine("Balance: " + account.Balance);
                                    break;
                                case "2":
                                    Console.Write("Enter Quantity: ");
                                    input = Console.ReadLine();
                                    Console.Clear();
                                    Console.WriteLine($"Depositing {input} To Account...");
                                    if (decimal.TryParse(input, out var deposit))
                                    {
                                        account.Balance += deposit;
                                        Console.WriteLine($"New Balance: {account.Balance}");
                                        break;
                                    }
                                    Console.Clear();
                                    Console.WriteLine("Error: Please Specify Quantity For Deposit");
                                    break;
                                case "3":
                                    Console.Write("Enter Quantity: ");
                                    input = Console.ReadLine();
                                    if (decimal.TryParse(input, out var withdraw))
                                    {
                                        if (withdraw <= account.Balance)
                                        {
                                            Console.Clear();
                                            Console.WriteLine($"Withdrawing {input} From Account...");
                                            account.Balance -= withdraw;
                                            Console.WriteLine($"New Balance: {account.Balance}");
                                            break;
                                        }

                                        Console.Clear();
                                        Console.WriteLine("Error: insufficient funds");
                                        break;
                                    }
                                    Console.Clear();
                                    Console.WriteLine("Error: Please Specify Quantity For Withdraw");
                                    break;
                                case "4":
                                    Console.Clear();
                                    Console.WriteLine("Logging out...");
                                    atmModeClose = true;
                                    break;
                                case "5":
                                    atmModeClose = true;
                                    systemShutdown = true;
                                    Console.WriteLine("System shutdown, bye");
                                    break;
                            }
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to ATM console app");
            Console.WriteLine("Options:");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Log In");
            Console.WriteLine("3. Shutdown");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    CreateAccount(records);
                    break;
                case "2":
                    AccountController(records);
                    break;
                case "3":
                    Console.WriteLine("Shutting down...");
                    break;
            }
        }
    }
}
