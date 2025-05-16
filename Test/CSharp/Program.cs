using System;
using System.Diagnostics;

class Program {
    static void Main(string[] args) {
        Console.Write("Enter first number: ");
        string num1 = Console.ReadLine();

        Console.Write("Enter operator (+, -, *, /): ");
        string op = Console.ReadLine();

        Console.Write("Enter second number: ");
        string num2 = Console.ReadLine();

        Console.Write("Method test for reverseString sort: ");
        string str = Console.ReadLine();

        // Set up the Java process
        var process = new Process();
        process.StartInfo.FileName = "java";
        process.StartInfo.Arguments = $"Calculator {num1} {op} {num2} {str}";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        // Set the working directory to the Java file's location
        process.StartInfo.WorkingDirectory = ".."; // assumes Calculator.class is in parent

        try {
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (!string.IsNullOrEmpty(error)) {
                Console.WriteLine("Java Error: " + error);
            } else {
                Console.WriteLine("Java Output: " + output.Trim());
            }
        } catch (Exception ex) {
            Console.WriteLine("Could not start Java process: " + ex.Message);
        }
    }
}
