using APBD_10.Models;

namespace APBD_10.Services;

public class StateContainer
{
    private readonly List<StudentDto> _students = new();

    private IReadOnlyList<StudentDto> ObservedStudents => _students.AsReadOnly();

    public event Action? OnChange;

    public void AddObserved(StudentDto student)
    {
        if (!_students.Any(x => x.Id == student.Id))
        {
            _students.Add(student);
            NotifyChange();
        }
    }

    public void RemoveObserved(int studentId)
    {
        var student = _students.FirstOrDefault(x => x.Id == studentId);
        if (student != null)
        {
            _students.Remove(student);
            NotifyChange();
        }
    }
    
    public bool IsObserved(int studentId) => _students.Any(x => x.Id == studentId);
    
    void NotifyChange() => OnChange?.Invoke();
}