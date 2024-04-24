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
    public static class Teachers
    {
        public static string GetTeachers = $"{baseURL}api/TeacherApi/GetTeachers";
        public static string CreateTeachers = $"{baseURL}api/TeacherApi/CreateTeachers";
        public static string UpdateTeachers = $"{baseURL}api/TeacherApi/UpdateTeachers";
        public static string DeleteTeacher(int id) => $"{baseURL}api/TeacherApi/DeleteTeacher?id={id}";
        public static string GetTeacherById(int id) => $"{baseURL}api/TeacherApi/GetById/{id}";

    }
}
