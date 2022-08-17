using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
Write a program to accept inputs fron user
as command line argument, add a greeting message
and display the output.
The Greeting App using Command Line Arguments
*/

namespace GreetingAppCLA
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Hint: The value passed as command line argument 
               are stored in args array which is a paramenter to Main()
               The values stored in args[] can be accessed by indexing args[0], arg[1], etc

               Example: First argument/value is stored in args[0] as String!
               User Console.WriteLine() and display the message simliar to the 
               Greeting app.
            */
            Console.WriteLine($"Hi {args[0]}, Good Afternoon!"); // Complete the code here

            // Write the code to accept a single key input from the user.
            Console.ReadKey();
        }
    }
}

/*
Instructions to Run the code:

    1. Open the Properties from Solution Explorer
    2. In the project property window Navigate to the "Debug" tab
    3. You will find a text box called "Command Line arguments"
    4. Here you can type the value you want to pass to the program
       Example: George or any other name
    5. Click Start!


Test Cases:

Argument: George
Output:
Hi George Good Afternoon

*/
