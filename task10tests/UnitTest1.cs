namespace task10tests;
using Xunit;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using task10;

[PluginLoad]
public class NoDependencyPlugin : IPlugin
{
    public void Execute() => Console.WriteLine("NoDependencyPlugin executed.");
}

[PluginLoad("NoDependencyPlugin")]
public class DependsOnNoDependency : IPlugin
{
    public void Execute() => Console.WriteLine("DependsOnNoDependency executed.");
}

[PluginLoad("SecondCyclicPlugin")]
public class FirstCyclicPlugin : IPlugin
{
    public void Execute() => Console.WriteLine("FirstCyclicPlugin executed.");
}

[PluginLoad("FirstCyclicPlugin")]
public class SecondCyclicPlugin : IPlugin
{
    public void Execute() => Console.WriteLine("SecondCyclicPlugin executed.");
}
public class PluginLoaderTests : IDisposable
{
    private readonly string _testDirectory;

    public PluginLoaderTests()
    {
        _testDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_testDirectory);
    }

    public void Dispose()
    {
        if (Directory.Exists(_testDirectory))
        {
            Directory.Delete(_testDirectory, recursive: true);
        }
    }

    [Fact]
    public void Load_PluginWithoutDependency_ShouldExecute()
    {
        File.Copy(GetDllPath("NoDependencyPlugin"), Path.Combine(_testDirectory, "NoDependencyPlugin.dll"));
        var output = new StringWriter();
        Console.SetOut(output);

        PluginLoader.LoadPlugins(_testDirectory);

        Assert.Contains("NoDependencyPlugin executed.", output.ToString());
    }

    [Fact]
    public void Load_PluginWithDependency_ShouldExecuteInCorrectOrder()
    {
        File.Copy(GetDllPath("NoDependencyPlugin"), Path.Combine(_testDirectory, "NoDependencyPlugin.dll"));
        File.Copy(GetDllPath("DependsOnNoDependency"), Path.Combine(_testDirectory, "DependsOnNoDependency.dll"));
        var output = new StringWriter();
        Console.SetOut(output);

        PluginLoader.LoadPlugins(_testDirectory);

        var outputText = output.ToString();
        Assert.Contains("NoDependencyPlugin executed.", outputText);
        Assert.Contains("DependsOnNoDependency executed.", outputText);
        Assert.True(
            outputText.IndexOf("NoDependencyPlugin executed.") < outputText.IndexOf("DependsOnNoDependency executed."),
            "Плагин с зависимостью должен выполняться после своего зависимого плагина"
        );
    }

    [Fact]
    public void Load_CircularDependency_ShouldOutputErrorMessage()
    {
        File.Copy(GetDllPath("FirstCyclicPlugin"), Path.Combine(_testDirectory, "FirstCyclicPlugin.dll"));
        File.Copy(GetDllPath("SecondCyclicPlugin"), Path.Combine(_testDirectory, "SecondCyclicPlugin.dll"));
        var output = new StringWriter();
        Console.SetOut(output);

        PluginLoader.LoadPlugins(_testDirectory);

        Assert.Contains("Невозможно загрузить плагины.", output.ToString());
    }

    private string GetDllPath(string className)
    {
        return Assembly.GetExecutingAssembly().Location;
    }
}
