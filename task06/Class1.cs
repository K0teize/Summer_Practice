using System.Reflection;
using Microsoft.VisualBasic;

namespace task06;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method|AttributeTargets.Property)]
public class DisplayNameAttribute : Attribute
{
    public string DisplayName { get; }
    public DisplayNameAttribute(string name)
    {
        DisplayName = name;
    }
}
[AttributeUsage(AttributeTargets.Class)]
public class VersionAttribute : Attribute
{
    public int Major { get; }
    public int Minor{ get; }
    public VersionAttribute(int major, int minor)
    {
        Major = major;
        Minor = minor;
    }
}
public static class ReflectionHelper
{
    public static void PrintTypeInfo(Type type)
    {
        var displayName = type.GetCustomAttribute<DisplayNameAttribute>();
        var version = type.GetCustomAttribute<VersionAttribute>();
        var methods = type.GetMethods();
        var props = type.GetProperties();
        Console.WriteLine($"Имя класса: {displayName?.DisplayName}");
        Console.WriteLine($"Версия: {version?.Major}.{version?.Minor}");
        foreach (var method in methods)
        {
            var methodDisplayName = method.GetCustomAttribute<DisplayNameAttribute>();
            Console.WriteLine($"{method.Name}:{methodDisplayName?.DisplayName}");
        }
        foreach (var prop in props)
        {
            var propDisplayName = prop.GetCustomAttribute<DisplayNameAttribute>();
            Console.WriteLine($"{prop.Name}:{propDisplayName?.DisplayName}");
        }
    }
}
