using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BankManagementSystem
{
    public class User
    {
        private string userName;
        public string userPassword;

        public User(string name, string password)
        {
            this.userName = name;
            this.userPassword = password;
        }

        public static User addUser() // Not sure whether this function is required or not
        {
            string inputName = Console.ReadLine();
            string inputPassword = readPassword();
            User user = new User(inputName, inputPassword);
            return user;
        }

        public static string readPassword()
        {
            string password = "";
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                if (keyInfo.Key != ConsoleKey.Backspace) // Enter one character
                {
                    Console.Write("*");
                    password += keyInfo.KeyChar;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace) // Delete one character
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring(0, password.Length - 1);
                        int position = Console.CursorLeft;
                        Console.SetCursorPosition(position - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(position - 1, Console.CursorTop);
                    }
                }
                keyInfo = Console.ReadKey(true);
            }

            Console.WriteLine();
            return password;
        }
    }

    public class Account
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public int phone { get; set; }
        public string email { get; set; }
        public int accountNum;
        public double balance;
        public List<string> history;

        static Account(string firstName, string lastName, string address, int phone, string email)
        {
            checkValidEmail(email);
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.phone = phone;
            this.email = email;
            this.accountNum = createNewAccountNum();
            this.balance = 0;
            this.history = new List<string> { };
        }

        private bool checkValidEmail(string email)
        {
            string pattern = @".+@(gmail\.com|outlook\.com|uts\.edu\.au)";
            bool result = Regex.Match(email, pattern, RegexOptions.IgnoreCase); // Maybe have problem here
            return result.Success;
        }

        private int createNewAccountNum()
        {
            List<int> numArray = new List<int> { };

            // Store existing names (account numbers) into array
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string[] accountNumArray = Directory.GetFiles(@projectDirectory + "\\Accounts");

            // If don't exist any account then start with min number 100001
            if (!accountNumArray)
            {
                return 100001;
            }
            // Check the lastest account number and create a new one based on that
            foreach (string accountNum in accountNumArray)
            {
                numArray.Append(int.Parse(accountNum));
            }
            numArray = numArray.ToArray();
            Array.Sort(numArray);
            return numArray.Last() + 1;
        }

        public void writeNewAccount(Account account)
        {

        }
    }

    public static class BMS
    {
        static void Main(string[] args)
        {
            loginMenu();
            // int userInput = int.Parse(Console.ReadLine());
        }

        public static void loginMenu()
        {
            Console.Clear();

            // Get user name and password
            Console.WriteLine("+========================================+");
            Console.WriteLine("|    WELCOME TO SIMPLE BANKING SYSTEM    |");
            Console.WriteLine("|            LOGIN TO START              |");
            Console.Write("|  User Name: ");
            string userName = Console.ReadLine();
            Console.Write("|  Password: ");
            string password = readPassword();
            Console.WriteLine("+========================================+");

            // Check credential
            // Show appropriate error msg
            checkCredential(userName, password);
        }

        public static string readPassword()
        {
            string password = "";
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                if (keyInfo.Key != ConsoleKey.Backspace) // Enter one character
                {
                    Console.Write("*");
                    password += keyInfo.KeyChar;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace) // Delete one character
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring(0, password.Length - 1);
                        int position = Console.CursorLeft;
                        Console.SetCursorPosition(position - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(position - 1, Console.CursorTop);
                    }
                }
                keyInfo = Console.ReadKey(true);
            }

            Console.WriteLine();
            return password;
        }

        public static void checkCredential(string userName, string password)
        {
            if (checkUserName(userName) && checkPassword(password))
            {
                Console.WriteLine("Valid credentials!... Please enter");
                Console.ReadKey();
                mainMenu();
            }
            else if (!checkUserName(userName))
            {
                Console.WriteLine("Invalid user name!... Please re-enter correct user name");
                Console.ReadKey();
                loginMenu();
            }
            else if (!checkPassword(password))
            {
                Console.WriteLine("Invalid password!... Please re-enter correct password");
                Console.ReadKey();
                loginMenu();
            }
        }

        public static bool checkUserName(string userName)
        {
            string[] allNames = getUserInfo(0);
            return allNames.Contains(userName);
        }

        public static bool checkPassword(string password)
        {
            string[] allPasswords = getUserInfo(1);
            return allPasswords.Contains(password);
        }

        public static string[] getUserInfo(int index)
        {
            // Get directory of the login.txt
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            StreamReader logInfos = new StreamReader(@projectDirectory + "\\login.txt");
            List<string> requiredInfo = new List<string>();
            string data;
            string line = logInfos.ReadLine();

            // Read login.txt by lines
            while (line != null)
            {
                data = line.Split('|')[index];
                requiredInfo.Add(data);
                line = logInfos.ReadLine();
            }
            logInfos.Close();
            string[] allNames = requiredInfo.ToArray();
            return allNames;
        }

        public static void mainMenu()
        {
            Console.Clear();

            Console.WriteLine("+========================================+");
            Console.WriteLine("|    WELCOME TO SIMPLE BANKING SYSTEM    |");
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|     1. Create a new account            |");
            Console.WriteLine("|     2. Search for an account           |");
            Console.WriteLine("|     3. Deposit                         |");
            Console.WriteLine("|     4. Withdraw                        |");
            Console.WriteLine("|     5. A/C statement                   |");
            Console.WriteLine("|     6. Delete account                  |");
            Console.WriteLine("|     7. Exit                            |");
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|    Enter your choice (1-7):            |");
            Console.WriteLine("+========================================+");

            // Handle error if user enter char instead of integer
            // or enter integer not inside of 1-7
            try
            {
                int userInput = int.Parse(Console.ReadLine());
                serviceController(userInput);
            }
            catch (Exception e)
            {
                mainMenu();
            }
        }

        public static void serviceController(int userInput)
        {
            // int userInput = int.Parse(Console.ReadLine());
            switch (userInput)
            {
                case 1:
                    createAccount();
                    break;
                case 2:
                    searchAccount();
                    break;
                case 3:
                    deposit();
                    break;
                case 4:
                    withdraw();
                    break;
                case 5:
                    accountStatement();
                    break;
                case 6:
                    deleteAccount();
                    break;
                case 7:
                    exitBMS();
                    break;
                default:
                    mainMenu();
                    break;
            }
        }

        public static void createAccount()
        {
            // Print the create account form
            Console.Clear();
            Console.WriteLine("+=============================================+");
            Console.WriteLine("|             CREATE A NEW ACCOUNT            |");
            Console.WriteLine("|---------------------------------------------|");
            Console.WriteLine("|               ENTER THE DETAILS             |");
            Console.WriteLine("|                                             |");
            Console.WriteLine("|   First Name:                               |");
            Console.WriteLine("|   Last Name:                                |");
            Console.WriteLine("|   Address:                                  |");
            Console.WriteLine("|   Phone:                                    |");
            Console.WriteLine("|   Email:                                    |");
            Console.WriteLine("+=============================================+");

            Account newAccount = new Account();
        }

        public static string[] getAccounts()
        {

        }

        public static void searchAccount()
        {
            Console.Clear();
        }

        public static void deposit()
        {
            Console.Clear();
        }

        public static void withdraw()
        {
            Console.Clear();
        }

        public static void accountStatement()
        {
            Console.Clear();
        }

        public static void deleteAccount()
        {
            Console.Clear();
        }

        public static void exitBMS()
        {
            // System.Diagnostics.Process.GetCurrentProcess().Kill();
            Console.Clear();
            Environment.Exit(0);
        }
    }
}
