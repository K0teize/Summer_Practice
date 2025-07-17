namespace task13tests;
using Xunit;
using task13;
public class UnitTest1
{
    public Student _student = new Student
    {
        FirstName = "Petya",
        LastName = "Sidorov",
        BirthDate = new DateTime(2000, 1, 1),
        Grades = new List<Subject> { new Subject { Name = "Algebra", Grade = 4 } }
    };

    [Fact]
    public void Serializer_ShouldReturnCorrectJson()
    {
        var json = _student.Serialize();
        Assert.Contains("Petya", json);
        Assert.Contains("Sidorov", json);
        Assert.Contains("01.01.2000", json);
        Assert.Contains("Algebra", json);
        Assert.Contains("4", json);
    }
    [Fact]
    public void Deserializer_ShouldReturnCorrectStudent()
    {
        var json = _student.Serialize();
        var student = StudentSerializer.Deserialize(json);
        Assert.Equal(_student.FirstName, student.FirstName);
        Assert.Equal(_student.LastName, student.LastName);
        Assert.Equal(_student.BirthDate, student.BirthDate);
        Assert.Equal(_student.Grades?.Count, student.Grades?.Count);
    }
    [Fact]
    public void Deserializer_NullProp_ShouldReturnThrowInvalidDataExceptionError()
    {
        var student = new Student
        {
            FirstName = "",
            LastName = "Sidorov",
            BirthDate = new DateTime(2000, 1, 1),
            Grades = new List<Subject> { new Subject { Name = "Algebra", Grade = 4 } }
        };
        var json = student.Serialize();
        Assert.Throws<InvalidDataException>(() => StudentSerializer.Deserialize(json));
    }
    [Fact]
    public void SaveAndLoadFromFile_ValidStudent_RoundTripSuccess()
    {
        var path = "student.json";
        _student.SaveToFile(path);
        var loadedStudent = StudentSerializer.LoadFromFile(path);
        Assert.Equal(_student.FirstName, loadedStudent.FirstName);
        Assert.Equal(_student.LastName, loadedStudent.LastName);
    }
}