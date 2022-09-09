using System;

namespace A1__BankManagement
{
    class BankingSystem
    {
        public class Account
        {
            private int account;
            public int accountNumber
            {
                get { return account; }
                set { account = value; }
            }
            protected String password;
            internal int deposit;
            internal int withdraw;
        }

        static String CreateAccount()
        {
            String password = null;
            Console.WriteLine("Please insert a password: ");
            password = Console.ReadLine();
            return password;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Running!");
        }
    }
}
