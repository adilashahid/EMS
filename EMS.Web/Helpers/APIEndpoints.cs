namespace EMS.Web.Helpers;

public class APIEndpoints
{
    private static string baseURL = "https://localhost:7141/";
    public static class Students
    {
        public static string GetStudents = $"{baseURL}api/StudentApi/GetStudents";
        public static string CreateStudent = $"{baseURL}api/StudentApi/CreateStudent";
        public static string UpdateStudent = $"{baseURL}api/StudentApi/UpdateStudent";
        public static string DeleteStudent(int rollNo) => $"{baseURL}api/StudentApi/DeleteStudent/{rollNo}";
        public static string GetStudentsById(int rollNo) => $"{baseURL}api/StudentApi/GetStudentsByIdAsnc/{rollNo}";
    }
}
