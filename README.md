# APCS Spring Final Project 2025

More will be added to the game.

## Java method calls in C#

In the Test folder, there is a example of Using Java as an external process. This should be the easiest method of runnning Java under C#. 

### Set Up

1. Create a folder.
2. Create your Java file.
3. In that same folder with the Java file, in VScode's terminal, type: <sub>javac _file name_.java </sub> into the terminal.
4. Once the .class file is created for that Java file inside the folder, create a new C# project or just the file if using Unity.
5. Locate the folder with command: <sub>cd _folder_</sub> down to every location from say cd Destop, cd foldername if the folder is in desktop.

**Skip 6 and just create a C# script file if you're using Unity**

6. Create a C# project using the command in VScode terminal: <sub> dotnet new console -n _file name_</sub> 
7. Use the command: <sub>cd  _file name_</sub>
8. Use: <sub>dotnet run </sub> to run the program.

  
**Finally refrence the Java file from the main folder, play with the code for more on that.** 

This can kind of method can only be used to call the main method, but it should be enough for the games, just need to make a lot of Java files. 
