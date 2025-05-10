using System;
using System.Diagnostics;
using System.IO;

public class JavaExecutor
{
    public string ExecuteJavaProgram(string input)
    {
        // Set up the process start info
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "java",  // Ensure Java is on the system PATH
            Arguments = "Test.jar",  // Path to your Java JAR file
            RedirectStandardInput = true,  // Allow sending input to Java program
            RedirectStandardOutput = true,  // Allow reading output from Java program
            UseShellExecute = false,  // Don't use the shell to start the process
            CreateNoWindow = true  // Don't create a new window
        };

        // Start the Java process
        using (Process process = Process.Start(startInfo))
        {
            // Send input to the Java program via StandardInput
            using (StreamWriter sw = process.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    sw.WriteLine(input);  // Send the input to Java
                }
            }

            // Read output from the Java program
            using (StreamReader sr = process.StandardOutput)
            {
                return sr.ReadToEnd();  // Read the result from Java
            }
        }
    }
}
