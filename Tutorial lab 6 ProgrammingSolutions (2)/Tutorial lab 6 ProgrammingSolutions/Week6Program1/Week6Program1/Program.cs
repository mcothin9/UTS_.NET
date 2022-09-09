using System;
using System.Linq;

/*
Write a program to Create Student a Result management System with the following specifications:
1. There are two classes, Person (Abstract base class), Student(derived class from person)
2. There is one Interface for Generating the Results

Please refer to the instructions document for the class diagrams and Instructions.

    */

namespace Week6LabProgram
{
    // Create an Abstract class person
    public abstract class Person
    {
        // Declare data members/properties
        public string name { get; set; }
        public string address { get; set; }

        // Constructor
        public Person(string personName, string personAddress)
        {
            name = personName;
            address = personAddress;
        }          
    }

    // Create an Interface IResults for creating marksheet
    public interface IResults
    {
        // Declare the methods to be inplemented in the class

        // Method to get the marks for 5 subjects
        void GetMarks();
        // Method to calculate the final grade (Pass/Fail)
        string CalculateResult();
        // Method to Display the MarkSheet
        void DisplayResult();
    }

    // Create the Student class by inheriting the Person Class and implementing the IResults interface
    public class Student:Person, IResults
    {
        // Data members specific to a Student
        private string standard;
        private string roll;
        private double[] mark;

        // Student Constructor, which also initializes the base class properties
        public Student(string name, string address, string standard, string roll) : base(name, address) 
        {
            this.standard = standard;
            this.roll = roll;
            this.mark = new double[5];
        }
        
        // Implement the GetMarks() method
        public void GetMarks()
        {
            // Accept the marks for 5 subjects and store it in an array
            for(int loopVar=0; loopVar<5; loopVar++)
            {
                Console.Write("Enter Marks for Subject-{0}:", loopVar+1);
                string userInput = Console.ReadLine();
                this.mark[loopVar] = Convert.ToDouble(userInput);
            }
        }
        // Implement the CalculateResult() method
        public string CalculateResult()
        {
            // Calculate the sum of the marks obtained
            double totalMarksObtained = this.mark.Sum();
            // Find the grade and return it
            // Averge <40, grade = Fail
            // Else Pass
            if (totalMarksObtained / 5 < 40)
                return "Fail";
            else
                return "Pass";
        }

        // Implement the DisplayResult() method to generate the Marksheet
        public void DisplayResult()
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("\t\tMark Sheet");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Name: {0}", this.name);
            Console.WriteLine("Class: {0}", this.standard);
            Console.WriteLine("Roll: {0}", this.roll);
            Console.WriteLine("Address: {0}", this.address);
            Console.WriteLine("\nMarks Obtained:");
            for(int loopVar= 0; loopVar<5; loopVar++)
            {
                Console.WriteLine("Subject-{0}: {1}", loopVar, this.mark[loopVar]+1);
            }
            Console.WriteLine("\nAverage Marks: {0}", this.mark.Average());
            Console.WriteLine("Final Grade: {0}", this.CalculateResult());
            Console.WriteLine("---------------------------------------------");
        }

    }
    class Week6Program1
    {
        static void Main(string[] args)
        {
            // Create a Student object
            Student s1 = new Student("George Woolsworth", "Ultimo, Sydney 2007, Australia", "V", "1004");
            // Get the student's marks
            s1.GetMarks();
            // Generate the Marks Sheet
            s1.DisplayResult();


            // Accept a key press from user.
            Console.ReadKey();
        }
    }
}

/* Test Case:

Enter Marks for Subject-1:56
Enter Marks for Subject-2:42
Enter Marks for Subject-3:89
Enter Marks for Subject-4:69
Enter Marks for Subject-5:95

---------------------------------------------
                Mark Sheet
---------------------------------------------
Name: George Woolsworth
Class: V
Roll: 1004
Address: Ultimo, Sydney 2007, Australia

Marks Obtained:
Subject-0: 57
Subject-1: 43
Subject-2: 90
Subject-3: 70
Subject-4: 96

Average Marks: 70.2
Final Grade: Pass
---------------------------------------------

    
    */
