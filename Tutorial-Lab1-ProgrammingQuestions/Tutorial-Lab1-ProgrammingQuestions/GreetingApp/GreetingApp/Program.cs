using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
Write a program to create a GreetingApp
Accept a Name from the user and
Display the name with a Greeting message
*/

namespace Week1LabProgram
{
    class GreetingApp
    {
        static void Main(string[] args)
        {
            // Write the code to Ask the name of the person to greet
            // Hint: use Console.Readline() to accept user input

            //1. Display a message "What is your name?" 
            Console.WriteLine("What is your name?"); //Complete the below code


            //2. Accept the user input and store it in a String variable userName
            string userName = Console.ReadLine(); // Write the code here, use the hint given above


            // Write the code to Display the name of the person with a greeting message
            // Message can be : Hi {userName}, Good Afternoon/Evening!
            // Hint: use Console.Writeline()
            Console.WriteLine($"Hi {userName}, Good Afternoon!");


            // Write the code to accept a single key entry from user
            Console.ReadKey();
        }
    }
}

/*
Test Case:

Expected output:

What is your Name?
George

Hi George Good Afternoon!


*/
