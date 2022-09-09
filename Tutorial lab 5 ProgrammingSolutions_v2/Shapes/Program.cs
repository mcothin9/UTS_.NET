using System;
/*Write a program to define a Shape class. The Shape class will be used to create two objects: Circle and Rectangle, 
and calculate their areas.Shape class should have constructors to initialize circle(with radius) and rectangle(with height and width).

 There should be two methods to calculate the area of circle and rectangle.
 The Shape objects should be created in the class containing the main() method.Please use the below class diagram :
 
 -------------------------------
 | Shape                      |
 -------------------------------
 | - height: double			  |
 | - width: double			  |
 | - radius : double	      |
 | - pi : double (const)      |
 -------------------------------
 | + Shape(ht, wd)			   |
 | + Shape(rad)                |
 | + rectangleArea():double    |
 | + circleArea()::double      |
 | + Display(shapetype)        |
 -------------------------------
 The object should be initialized using respective constructors
 
 Example:
Radius of circle is: 4.0
Height and Width of rectangle is: 5.0 ,4.0
Area of Circle is: 50.26548245743669
Area of Rectangle is: 20.0

Hint:
1. For area of circle use pi =3.142, Area = PI * radius * radius
2. Area of rectangle = height * width
3. Check the main method before creating the shape class
 */

namespace Week4LabProgramQuestion
{
    public class Shapes
    {
        // Private fields
        public double height, width, radius;
        private const double pi = 3.142; // Define the value of pi

        //Constructor to create a Circle
        public Shapes(double rad)
        {
            radius = rad;
        }
        //Constructor to create a rectangle
        public Shapes(double ht, double wd)
        {
            height = ht;
            width = wd;
        }
        // Method to calculate the area of Rectangle
        public double RectangleArea()
        {
            return (height * width);
        }
        // Method to calculate the area of Circle
        public double CircleArea()
        {
            return (pi * radius*radius);
        }
        public void Display(char shapeType)
        {
            if (shapeType == 'r')
                Console.WriteLine("Height and Width of rectangle is: {0}, {1}", height, width);
            else if(shapeType == 'c')
                Console.WriteLine("Radius of circle is: {0}", radius);
        }
    }
    class ShapesTest
    {
        static void Main(string[] args)
        {
            // Create Shape object as per test case
            Shapes circle = new Shapes(4.0);
            Shapes rectangle = new Shapes(5.0, 4.0);

            // Display the object properties
            circle.Display('c');
            rectangle.Display('r');

            // Calculate and display the area of the objects
            Console.WriteLine("The area of circle is {0}", circle.CircleArea());
            Console.WriteLine("The area of rectangle is {0}", rectangle.RectangleArea());

            // Accept a key press from user
            Console.ReadKey();
        }
    }
}

/*
The program should produce the following result
  Test case 1:
  Initialize the circle with radius: 4
  Initialize the rectangle with ht and wd: 5, 4
  
  Expected output:
  Radius of circle is: 4.0
  Height and Width of rectangle is: 5.0 ,4.0
  Area of Circle is: 50.26548245743669
  Area of Rectangle is: 20.0

  Test case 2:
  Initialize the circle with radius: 10
  Initialize the rectangle with ht and wd: 15, 5
  Note the result
  

 */
