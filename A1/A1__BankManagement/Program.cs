using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace BankManagementSystem
{
    public class Account
    {
        private string firstName;
        private string lastName;
        private string address;
        private int phone;
        private string email;
        private int accountNo;
        private double balance;
        private List<string> history;

        public Account(string firstName, string lastName, string address, int phone, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.phone = phone;
            this.email = email;
            this.accountNo = getLargestAccountNo();
            this.balance = 0.0;
            this.history = new List<string> { };
        }

        public string getFirstName()
        {
            return this.firstName;
        }

        public string getLastName()
        {
            return this.lastName;
        }

        public string getAddress()
        {
            return this.address;
        }

        public int getPhone()
        {
            return this.phone;
        }

        public string getEmail()
        {
            return this.email;
        }

        public int getAccountNo()
        {
            return this.accountNo;
        }
        public double getBalance()
        {
            return this.balance;
        }

        private int getLargestAccountNo()
        {
            List<int> numArray = new List<int> { };

            FileInfo[] accountNumArray = getAllAccountFiles();

            // If don't exist any account then start with min number 100001
            if (accountNumArray.Length == 0)
            {
                return 100001;
            }

            // Check the lastest account number and create a new one based on that
            foreach (FileInfo accountNum in accountNumArray)
            {
                string name = accountNum.Name.Split(".")[0];
                numArray.Add(int.Parse(name));
            }
            int[] result = numArray.ToArray();
            Array.Sort(result);
            return numArray.Last() + 1;
        }

        public static FileInfo[] getAllAccountFiles()
        {
            // Store existing names (account numbers) into FileInfo list
            DirectoryInfo accountsDirecoty = new DirectoryInfo(BMS.getProjectDirectory() + "\\Accounts");
            FileInfo[] accountNumArray = accountsDirecoty.GetFiles("*.txt");
            return accountNumArray;
        }

        public void writeNewAccount()
        {
            string[] history = this.history.ToArray();
            int linesNum = 7 + history.Length;
            string[] accountFile = new string[linesNum];
            accountFile[0] = "First Name|" + this.firstName;
            accountFile[1] = "Last Name|" + this.lastName;
            accountFile[2] = "Address|" + this.address;
            accountFile[3] = "Phone|" + this.phone;
            accountFile[4] = "Email|" + this.email;
            accountFile[5] = "AccountNo|" + this.accountNo;
            accountFile[6] = "Balance|" + this.balance;
            for (int i=7; i<accountFile.Length; i++)
            {
                accountFile[i] = history[i - 7];
            }

            // Write collection of account detail to a file named by account no
            string accountFileName = BMS.getProjectDirectory() + $"\\Accounts\\{this.accountNo}.txt";
            File.WriteAllLines(accountFileName, accountFile);
        }
    }

    class BMS
    {
        static void Main(string[] args)
        {
            loginMenu();
        }

        //  <==============================================================================================================>
        //  <                                           Task.1 Login Menu                                                  >
        //  <==============================================================================================================>
        public static void loginMenu()
        {
            Console.Clear();

            // Get user name and password
            Console.WriteLine("+========================================+");
            Console.WriteLine("|    WELCOME TO SIMPLE BANKING SYSTEM    |");
            Console.WriteLine("|            LOGIN TO START              |");
            Console.WriteLine("|  User Name:                            |");
            Console.WriteLine("|  Password:                             |");
            Console.WriteLine("+========================================+");

            // Let user fulfill required infomation
            userFulfillInfo();
        }

        public static void userFulfillInfo()
        {
            // Go back to user name and let user input
            Console.SetCursorPosition(14, 3);
            string userName = Console.ReadLine();

            // Go back to password and make it double blind
            Console.SetCursorPosition(13, 4);
            string password = readPassword();

            // Set cursor position to bottom left to make last split line looks good
            Console.SetCursorPosition(0, 7);

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
            string projectDirectory = getProjectDirectory();
            StreamReader logInfos = new StreamReader(projectDirectory + "\\login.txt");
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

        public static string getProjectDirectory()
        {
            return Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        }

        //  <==============================================================================================================>
        //  <                                                Task.2 Main Menu                                              >
        //  <==============================================================================================================>
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
                Console.SetCursorPosition(30, 11);
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

        //  <==============================================================================================================>
        //  <                                          Task.3 Create a new account                                         >
        //  <==============================================================================================================>
        public static void createAccount()
        {
            Account newAccount = constructAccount();
            createFieldCheck(newAccount);
        }

        private static void printCreateAccountMenu()
        {
            Console.Clear();
            Console.WriteLine("+======================================================+");
            Console.WriteLine("|                 CREATE A NEW ACCOUNT                 |");
            Console.WriteLine("|------------------------------------------------------|");
            Console.WriteLine("|                   ENTER THE DETAILS                  |");
            Console.WriteLine("|                                                      |");
            Console.WriteLine("|   First Name:                                        |");
            Console.WriteLine("|   Last Name:                                         |");
            Console.WriteLine("|   Address:                                           |");
            Console.WriteLine("|   Phone:                                             |");
            Console.WriteLine("|   Email:                                             |");
            Console.WriteLine("+======================================================+");
        }

        private static Account constructAccount()
        {
            // Print the create account menu
            printCreateAccountMenu();
            Console.SetCursorPosition(16, 5);
            string firstName = Console.ReadLine();

            Console.SetCursorPosition(15, 6);
            string lastName = Console.ReadLine();

            Console.SetCursorPosition(13, 7);
            string address = Console.ReadLine();

            Console.SetCursorPosition(11, 8);
            int phone = int.Parse(Console.ReadLine());

            Console.SetCursorPosition(11, 9);
            string email = Console.ReadLine();

            // Check email format
            if (checkValidEmail(email))
            {
                Account account = new Account(firstName, lastName, address, phone, email);
                return account;
            }

            // Detect invalid email, show warning and let user re-enter
            Console.SetCursorPosition(0, 13);
            Console.WriteLine("Please re-enter a valid email address."); // Try later: keep other 4 propeties only refresh email textbox
            Console.ReadKey();
            return constructAccount();
        }

        private static bool checkValidEmail(string email)
        {
            // Email must contains a '@'
            // Should end with gmail.com or outlook.com or uts.edu.au (student.uts.edu.au used for test)
            string pattern = @".+@(gmail\.com|outlook\.com|uts\.edu\.au|student\.uts\.edu\.au)";
            bool result = Regex.IsMatch(email, pattern);
            return result;
        }

        private static void createFieldCheck(Account account)
        {
            Console.WriteLine("");
            Console.WriteLine("Is the information correct (y/n)?");
            if (Console.ReadLine() == "y")
            {
                account.writeNewAccount();
                Console.WriteLine("");
                Console.WriteLine("Account Created! Details will be provided via email.");
                Console.WriteLine($"Account number is: {account.getAccountNo()}");
                emailForCreateSuccess(account);
            }
            else
            {
                createAccount();
            }
        }

        private static void emailForCreateSuccess(Account account)
        {
            // Set message
            MailMessage announcement = new MailMessage();
            announcement.From = new MailAddress("mingchenothin9@gmail.com");
            announcement.To.Add(account.getEmail());
            announcement.Subject = "[13476080 A1] Announcement for account successful created";
            announcement.Body = generateEmailBody(account);

            // Set smtp
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("mingchenothin9@gmail.com", "glrbgxichaeuieso");

            // Use SMTP to send email
            try
            {
                smtpClient.Send(announcement);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught in CreateMessageWithAttachment(): {0}",
                    e.ToString());
            }
            Console.ReadKey();
            mainMenu();
        }

        private static string generateEmailBody(Account account)
        {
            string body = "FirstName: " + account.getFirstName() + "\n" + "LastName: " + account.getLastName() + "\n" +
                "Address: " + account.getAddress() + "\n" + "Phone: " + account.getPhone().ToString() + "\n" +
                "Email: " + account.getEmail() + "\n" + "Account Number: " + account.getAccountNo().ToString() + "\n";
            return body;
        }


        //  <==============================================================================================================>
        //  <                                          Task.4 Search for an account                                        >
        //  <==============================================================================================================>
        public delegate void voidDelegate();
        public delegate void delegateAsParameter(voidDelegate methodName);
        public delegate void oneStringDelegate(string str);

        public static void searchAccount()
        {
            Console.Clear();
            Console.WriteLine("+======================================================+");
            Console.WriteLine("|                   SEARCH AN ACCOUNT                  |");
            Console.WriteLine("|------------------------------------------------------|");
            Console.WriteLine("|                   ENTER THE DETAILS                  |");
            Console.WriteLine("|                                                      |");
            Console.WriteLine("|     Account Number:                                  |");
            Console.WriteLine("+======================================================+");

            // Let user input account no
            Console.SetCursorPosition(22, 5);
            string accountNoToSearch = Console.ReadLine();

            // Create delegate
            voidDelegate searchMethod = new voidDelegate(searchAccount);
            oneStringDelegate getDetails = new oneStringDelegate(getDetailByPath);
            voidDelegate searchCheck = new voidDelegate(checkAnotherAccountForSearch);

            // Set cursor to bottom and check account
            Console.SetCursorPosition(0, 7);
            Console.WriteLine("");
            checkAccountNoSearch(accountNoToSearch, searchMethod, searchCheck, getDetails);
        }

        private static void checkAccountNoSearch(string accountNo, voidDelegate currentTaskMethod, voidDelegate checkMethod, oneStringDelegate getMethod)
        {
            
            // First check valid input
            try
            {
                long isInteger = long.Parse(accountNo);
            }
            catch(Exception e)
            {
                Console.WriteLine("Account number must be integer, please re-enter.");
                Console.ReadKey();
                currentTaskMethod();
            }
            if (accountNo.Length > 10)
            {
                Console.WriteLine("Account number should be less than 10 characters, please re-enter.");
                Console.ReadKey();
                currentTaskMethod();
            }
            
            // Try to find match file
            string[] paths = Directory.GetFiles(getProjectDirectory() + "\\Accounts", $"{accountNo}.txt");
            if(paths.Length == 0)
            {
                Console.WriteLine("Account not found!");
                searchAccount();
            }
            else
            {
                getMethod(paths[0]);
            }
        }

        private static void getDetailByPath(string path)
        {
            string[] lines = File.ReadAllLines(path);
            Console.WriteLine("Account found!");
            Console.WriteLine("+======================================================+");
            Console.WriteLine("|                    ACCOUNT DETAILS                   |");
            Console.WriteLine("|------------------------------------------------------|");
            Console.WriteLine("|                                                      |");
            Console.WriteLine("|   Account No:                                        |");
            Console.WriteLine("|   Account Balance:                                   |");
            Console.WriteLine("|   First Name:                                        |");
            Console.WriteLine("|   Last Name:                                         |");
            Console.WriteLine("|   Address:                                           |");
            Console.WriteLine("|   Phone:                                             |");
            Console.WriteLine("|   Email:                                             |");
            Console.WriteLine("+======================================================+");
            
            // get all detail
            string firstName = lines[0].Split('|')[1];
            string lastName = lines[1].Split('|')[1];
            string address = lines[2].Split('|')[1];
            string phone = lines[3].Split('|')[1];
            string email = lines[4].Split('|')[1];
            string accountNo = lines[5].Split('|')[1];
            string balance = lines[6].Split('|')[1];

            // Fulfill detail to console
            Console.SetCursorPosition(16, 13);
            Console.WriteLine(accountNo);
            Console.SetCursorPosition(21, 14);
            Console.WriteLine(balance);
            Console.SetCursorPosition(16, 15);
            Console.WriteLine(firstName);
            Console.SetCursorPosition(15, 16);
            Console.WriteLine(lastName);
            Console.SetCursorPosition(13, 17);
            Console.WriteLine(address);
            Console.SetCursorPosition(11, 18);
            Console.WriteLine(phone);
            Console.SetCursorPosition(11, 19);
            Console.WriteLine(email);

            // Ask for another search
            Console.SetCursorPosition(0, 21);
            Console.WriteLine("");
            checkAnotherAccountForSearch();
        }

        private static void checkAnotherAccountForSearch()
        {
            Console.WriteLine("Check another account (y/n)?");
            string userChoice = Console.ReadLine();
            if (userChoice == "y")
            {
                searchAccount();
            }
            else
            {
                mainMenu();
            }
        }

        //  <==============================================================================================================>
        //  <                                                Task.5 Deposit                                                >
        //  <==============================================================================================================>
        public static void deposit()
        {
            Console.Clear();
            Console.WriteLine("+======================================================+");
            Console.WriteLine("|                        DEPOSIT                       |");
            Console.WriteLine("|------------------------------------------------------|");
            Console.WriteLine("|                  ENTER THE DETAILS                   |");
            Console.WriteLine("|                                                      |");
            Console.WriteLine("|    Account Number:                                   |");
            Console.WriteLine("|    Amount: $                                         |");
            Console.WriteLine("+======================================================+");

            // Let user input account no
            Console.SetCursorPosition(21, 5);
            string accountNoToSearch = Console.ReadLine();

            // Create delegate
            voidDelegate depositMethod = new voidDelegate(deposit);
            delegateAsParameter depositCheck = new delegateAsParameter(checkAnotherForDepositeWithdraw);
            oneStringDelegate getDeposit = new oneStringDelegate(depositByPath);

            // Set cursor to bottom and check account
            Console.SetCursorPosition(0, 8);
            checkAccountNo(accountNoToSearch, depositMethod, depositCheck, getDeposit);
        }

        private static void checkAccountNo(string accountNo, voidDelegate currentTaskMethod, delegateAsParameter checkMethod, oneStringDelegate getMethod)
        {

            // First check valid input
            try
            {
                long isInteger = long.Parse(accountNo);
            }
            catch (Exception e)
            {
                Console.WriteLine("Account number must be integer, please re-enter.");
                Console.ReadKey();
                currentTaskMethod();
            }
            if (accountNo.Length > 10)
            {
                Console.WriteLine("Account number should be less than 10 characters, please re-enter.");
                Console.ReadKey();
                currentTaskMethod();
            }

            // Try to find match file
            string[] paths = Directory.GetFiles(getProjectDirectory() + "\\Accounts", $"{accountNo}.txt");
            if (paths.Length == 0)
            {
                Console.WriteLine("Account not found!");
                checkMethod(currentTaskMethod);
            }
            else
            {
                getMethod(paths[0]);
            }
        }

        private static void depositByPath(string path)
        {
            // Get user input amount to deposit
            Console.WriteLine("Account found! Enter the amount...");
            Console.SetCursorPosition(14, 6);
            double amountToDeposit = double.Parse(Console.ReadLine());

            // Find current balance and date
            string[] lines = File.ReadAllLines(path);
            double currentBalance = double.Parse(lines[6].Split('|')[1]);
            double newBalance = amountToDeposit + currentBalance;
            string accountNo = lines[5].Split('|')[1];
            string date = DateTime.Now.Date.ToString();
            string month = DateTime.Now.Month.ToString();
            string year = DateTime.Now.Year.ToString();
            string accountFileName = getProjectDirectory() + $"\\Accounts\\{accountNo}.txt";

            // Update account file
            Array.Resize(ref lines, lines.Length + 1);
            lines[6] = "Balance|" + newBalance;
            lines[lines.Length - 1] = $"{date}.{month}.{year}|Deposit|{currentBalance}|{amountToDeposit}";
            File.WriteAllLines(accountFileName, lines);

            // Success and back to main menu
            Console.SetCursorPosition(0, 9);
            Console.WriteLine("Deposit successfull!");
            Console.ReadKey();
            mainMenu();
        }

        private static void checkAnotherForDepositeWithdraw(voidDelegate currentMethod)
        {
            Console.WriteLine("Retry (y/n)?");
            string userChoice = Console.ReadLine();
            if (userChoice == "y")
            {
                currentMethod();
            }
            else
            {
                mainMenu();
            }
        }

        //  <==============================================================================================================>
        //  <                                               Task.6 Withdraw                                                >
        //  <==============================================================================================================>
        public static void withdraw()
        {
            Console.Clear();
            Console.WriteLine("+======================================================+");
            Console.WriteLine("|                       WITHDRAW                       |");
            Console.WriteLine("|------------------------------------------------------|");
            Console.WriteLine("|                  ENTER THE DETAILS                   |");
            Console.WriteLine("|                                                      |");
            Console.WriteLine("|    Account Number:                                   |");
            Console.WriteLine("|    Amount: $                                         |");
            Console.WriteLine("+======================================================+");

            // Let user input account no
            Console.SetCursorPosition(21, 5);
            string accountNoToSearch = Console.ReadLine();

            // Create delegate
            voidDelegate withdrawMethod = new voidDelegate(withdraw);
            delegateAsParameter withdrawCheck = new delegateAsParameter(checkAnotherForDepositeWithdraw);
            oneStringDelegate getWithdraw = new oneStringDelegate(withdrawByPath);

            // Set cursor to bottom and check account
            Console.SetCursorPosition(0, 8);
            checkAccountNo(accountNoToSearch, withdrawMethod, withdrawCheck, getWithdraw);
        }

        private static void withdrawByPath(string path)
        {
            // Get user input amount to deposit
            Console.WriteLine("Account found! Enter the amount...");
            Console.SetCursorPosition(14, 6);
            double amountToWithdraw = double.Parse(Console.ReadLine());
            Console.SetCursorPosition(0, 9);

            // Find current balance and date
            string[] lines = File.ReadAllLines(path);
            double currentBalance = double.Parse(lines[6].Split('|')[1]);
            if (currentBalance < amountToWithdraw)
            {
                Console.WriteLine("Withdraw failed. Account balance lower than withdraw amount.");
                Console.ReadKey();
                withdraw();
            }
            else
            {
                double newBalance = currentBalance - amountToWithdraw;
                string accountNo = lines[5].Split('|')[1];
                string date = DateTime.Now.Date.ToString();
                string month = DateTime.Now.Month.ToString();
                string year = DateTime.Now.Year.ToString();
                string accountFileName = getProjectDirectory() + $"\\Accounts\\{accountNo}.txt";

                // Update account file
                Array.Resize(ref lines, lines.Length + 1);
                lines[6] = "Balance|" + newBalance;
                lines[lines.Length - 1] = $"{date}.{month}.{year}|Withdraw|{currentBalance}|{amountToWithdraw}";
                File.WriteAllLines(accountFileName, lines);

                // Success and back to main menu
                Console.WriteLine("Withdraw successfull!");
                Console.ReadKey();
                mainMenu();
            }
        }

        //  <==============================================================================================================>
        //  <                                         Task.7 Account statement                                             >
        //  <==============================================================================================================>
        public static void accountStatement()
        {
            Console.Clear();
            Console.WriteLine("+======================================================+");
            Console.WriteLine("|                      STATEMENT                       |");
            Console.WriteLine("|------------------------------------------------------|");
            Console.WriteLine("|                  ENTER THE DETAILS                   |");
            Console.WriteLine("|                                                      |");
            Console.WriteLine("|    Account Number:                                   |");
            Console.WriteLine("+======================================================+");

            // Let user input account no
            Console.SetCursorPosition(21, 5);
            string accountNoToSearch = Console.ReadLine();
        }

        //  <==============================================================================================================>
        //  <                                         Task.8 Delete an account                                             >
        //  <==============================================================================================================>
        public static void deleteAccount()
        {
            Console.Clear();
            Console.WriteLine("+======================================================+");
            Console.WriteLine("|                  DELETE AN ACCOUNT                   |");
            Console.WriteLine("|------------------------------------------------------|");
            Console.WriteLine("|                  ENTER THE DETAILS                   |");
            Console.WriteLine("|                                                      |");
            Console.WriteLine("|    Account Number:                                   |");
            Console.WriteLine("+======================================================+");

            // Let user input account no
            Console.SetCursorPosition(21, 5);
            string accountNoToSearch = Console.ReadLine();
        }

        //  <==============================================================================================================>
        //  <                                               Task.9 Exit                                                    >
        //  <==============================================================================================================>
        public static void exitBMS()
        {
            Console.Clear();
            Console.WriteLine("Exiting the simple banking management system.");
            Console.WriteLine("Thank you for using!");
            Environment.Exit(0);
        }
    }
}
