namespace task05;

using System;
using System.Reflection;
using System.Collections.Generic;

public class ClassAnalyzer
{
    private Type _type;

    public ClassAnalyzer(Type type)
    {
        _type = type;
    }
    public IEnumerable<string> GetPublicMethods()
    {
        var method = _type.GetMethods();
        return method.Select(s => s.Name);
    }
    public IEnumerable<string> GetMethodParams(string methodname)
    {
        var method = _type?.GetMethod(methodname);
        return method?.GetParameters().Select(s => s.Name!)??Enumerable.Empty<string>();
    }
    public IEnumerable<string> GetAllFields()
    {
        var fields = _type.GetFields(BindingFlags.NonPublic|BindingFlags.Public|BindingFlags.Instance);
        return fields.Select(s => s.Name);
    }
    public IEnumerable<string> GetProperties()
    {
        var properties = _type.GetProperties();
        return properties.Select(s => s.Name);
    }
    public bool HasAttribute<T>() where T : Attribute
    {
        return Attribute.IsDefined(_type,typeof(T));
    }
}
