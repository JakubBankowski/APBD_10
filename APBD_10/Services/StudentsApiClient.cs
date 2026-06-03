using System.Net.Http.Json;
using System.Text.Json;
using APBD_10.Models;

namespace APBD_10.Services;

public class StudentsApiClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public StudentsApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive =  true };
    }

    public async Task<IReadOnlyList<StudentDto>> GetStudentsAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<StudentDto>>("api/students", _jsonOptions);
            return response ?? new List<StudentDto>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Couldnt connect to API", ex);
        }
    }

    public async Task<StudentDto> GetStudentByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/students/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception("Student not found");
            }

            response.EnsureSuccessStatusCode();
            return (await response.Content.ReadFromJsonAsync<StudentDto>(_jsonOptions))!;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Couldnt connect to API", ex);
        }
    }

    public async Task<StudentDto> CreateStudentAsync(StudentDto student)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/students", student, _jsonOptions);
            response.EnsureSuccessStatusCode();
            return (await response.Content.ReadFromJsonAsync<StudentDto>(_jsonOptions))!;
        }
        catch(HttpRequestException ex)
        {
            throw new Exception("Couldnt connect to API", ex);
        }
    }

    public async Task<List<CourseDto>> GetCoursesAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<CourseDto>>($"api/courses", _jsonOptions);
            return response ?? new List<CourseDto>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Couldnt connect to API", ex);
        }
    }

    public async Task AssignCourseAsync(int studentId, int courseId)
    {
        try
        {
            var payload = new StudentCourseDto()
            {
                StudentId = studentId,
                CourseId = courseId
            };

            var response =
                await _httpClient.PostAsJsonAsync($"api/students/{studentId}/courses", payload, _jsonOptions);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception(errorContent);
            }
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Couldnt connect to API", ex);
        }
    }
}