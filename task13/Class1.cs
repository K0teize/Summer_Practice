namespace task13;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
public class Subject
{
    public string? Name { get; set; }
    public int Grade { get; set; }
}

public class Student
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public List<Subject>? Grades { get; set; }
}

public class DateConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.Parse(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("dd.MM.yyyy"));
    }
}

public static class StudentSerializer
{
    private static readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new DateConverter() }
    };

    public static string Serialize(this Student student)
    {
        return JsonSerializer.Serialize(student, _options);
    }

    public static Student Deserialize(string json)
    {
        var student = JsonSerializer.Deserialize<Student>(json, _options);

        if (student == null || string.IsNullOrWhiteSpace(student.FirstName) || string.IsNullOrWhiteSpace(student.LastName) || student.BirthDate == default || student.Grades == null)
        {
            throw new InvalidDataException("Некорректная информация");
        }

        return student!;
    }

    public static void SaveToFile(this Student student, string filePath)
    {
        File.WriteAllText(filePath, student.Serialize());
    }

    public static Student LoadFromFile(string filePath)
    {
        return Deserialize(File.ReadAllText(filePath));
    }
}
