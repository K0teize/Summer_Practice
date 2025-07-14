namespace task08;

using System.Reflection;
using CommandLib;
using FileSystemCommands;

public class CommandRunner
{
    public static void Main()
    {
        var assembly = Assembly.LoadFrom("FileSystemCommands.dll");

        var testDir1 = Path.Combine(Path.GetTempPath(), "TestDir1");
        Directory.CreateDirectory(testDir1);
        File.WriteAllText(Path.Combine(testDir1, "test1.txt"), "Hello");
        File.WriteAllText(Path.Combine(testDir1, "test2.txt"), "World");
        Type? sizeCommandType = assembly.GetType("FileSystemCommands.DirectorySizeCommand");

        if (sizeCommandType != null && Activator.CreateInstance(sizeCommandType, testDir1) is DirectorySizeCommand sizeCommand)
        {
            sizeCommand.Execute();
            Console.WriteLine(sizeCommand.size);
        }

        var testDir2 = Path.Combine(Path.GetTempPath(), "TestDir2");
        Directory.CreateDirectory(testDir2);
        File.WriteAllText(Path.Combine(testDir2, "file1.txt"), "Text");
        File.WriteAllText(Path.Combine(testDir2, "file2.txt"), "Log");
        Type? fileCommandType = assembly.GetType("FileSystemCommands.FindFilesCommand");

        if (fileCommandType != null && Activator.CreateInstance(fileCommandType, testDir2, "*.txt") is FindFilesCommand filesCommand)
        {
            filesCommand.Execute();
            foreach (var file in filesCommand.files)
            {
                Console.WriteLine(file);
            }
        }
    }
    
}
