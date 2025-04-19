using System;
using System.Data;
using System.Data.SqlClient;

class UniversityDatabaseApp
{
    static string connectionString = @"Server=DESKTOP-9062C6O;Database=UniversityDB;Integrated Security=True;TrustServerCertificate=True;";

    static void Main()
    {
        Console.WriteLine("Управление базой данных университета");
        Console.WriteLine("====================================");

        try
        {
            // 1. Заполнение таблицы начальными значениями (с проверкой существования)
            InsertDefaultData();

            // 2. Добавление новых записей (с проверкой существования)
            int newFacultyId = GetOrCreateFaculty("Факультет искусств", "Здание 5", "Смирнова А.А.");

            if (newFacultyId > 0)
            {
                int newDepartmentId = GetOrCreateDepartment("Кафедра дизайна", newFacultyId, "Ковалева Е.В.", "505");

                if (newDepartmentId > 0)
                {
                    AddStudentIfNotExists("Екатерина", "Соколова", new DateTime(2003, 7, 12), "ДИЗ-201", newDepartmentId);
                }
            }

            // 3. Обновление данных существующих записей
            UpdateStudentGroupIfExists(1, "ИТ-102");
            UpdateProfessorPositionIfExists(1, "Профессор");

            // 4. Удаление данных (с проверкой существования)
            DeleteStudentIfExists(3);
            DeleteDepartmentIfExists(2);

            // 5. Вывод информации
            DisplayAllStudents();
            DisplayAllDepartments();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        Console.WriteLine("\nОперации завершены. Нажмите любую клавишу для выхода.");
        Console.ReadKey();
    }

    static void InsertDefaultData()
    {
        // Факультеты
        GetOrCreateFaculty("Факультет информационных технологий", "Здание 1", "Иванов И.И.");
        GetOrCreateFaculty("Экономический факультет", "Здание 2", "Петрова П.П.");
        GetOrCreateFaculty("Юридический факультет", "Здание 3", "Сидоров С.С.");
        GetOrCreateFaculty("Факультет естественных наук", "Здание 4", "Кузнецова А.В.");

        // Кафедры
        int itFacultyId = GetFacultyId("Факультет информационных технологий");
        if (itFacultyId > 0)
        {
            GetOrCreateDepartment("Кафедра программирования", itFacultyId, "Смирнов А.А.", "101");
            GetOrCreateDepartment("Кафедра сетевых технологий", itFacultyId, "Кузнецов В.В.", "102");
        }

        int econFacultyId = GetFacultyId("Экономический факультет");
        if (econFacultyId > 0)
        {
            GetOrCreateDepartment("Кафедра экономики", econFacultyId, "Васильева Е.Е.", "201");
        }

        int lawFacultyId = GetFacultyId("Юридический факультет");
        if (lawFacultyId > 0)
        {
            GetOrCreateDepartment("Кафедра гражданского права", lawFacultyId, "Михайлов Д.Д.", "301");
        }

        // Студенты
        int progDeptId = GetDepartmentId("Кафедра программирования");
        if (progDeptId > 0)
        {
            AddStudentIfNotExists("Иван", "Иванов", new DateTime(2000, 5, 15), "ИТ-101", progDeptId);
            AddStudentIfNotExists("Мария", "Петрова", new DateTime(2001, 3, 22), "ИТ-101", progDeptId);
        }

        int econDeptId = GetDepartmentId("Кафедра экономики");
        if (econDeptId > 0)
        {
            AddStudentIfNotExists("Дмитрий", "Сидоров", new DateTime(1999, 11, 10), "ЭК-201", econDeptId);
        }
    }

    static int GetOrCreateFaculty(string name, string building, string dean)
    {
        int facultyId = GetFacultyId(name);
        if (facultyId > 0)
        {
            Console.WriteLine($"Факультет '{name}' уже существует (ID: {facultyId})");
            return facultyId;
        }

        string query = $@"
        INSERT INTO Faculties (FacultyName, Building, DeanName)
        OUTPUT INSERTED.FacultyID
        VALUES ('{name.Replace("'", "''")}', '{building.Replace("'", "''")}', '{dean.Replace("'", "''")}')";

        try
        {
            facultyId = ExecuteScalar<int>(query);
            Console.WriteLine($"✅ Добавлен новый факультет '{name}' (ID: {facultyId})");
            return facultyId;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Ошибка при добавлении факультета: {ex.Message}");
            return -1;
        }
    }

    static int GetOrCreateDepartment(string name, int facultyId, string head, string room)
    {
        if (!FacultyExists(facultyId))
        {
            Console.WriteLine($"❌ Факультет с ID {facultyId} не существует");
            return -1;
        }

        int deptId = GetDepartmentId(name);
        if (deptId > 0)
        {
            Console.WriteLine($"Кафедра '{name}' уже существует (ID: {deptId})");
            return deptId;
        }

        string query = $@"
        INSERT INTO Departments (DepartmentName, FacultyID, HeadName, RoomNumber)
        OUTPUT INSERTED.DepartmentID
        VALUES ('{name.Replace("'", "''")}', {facultyId}, '{head.Replace("'", "''")}', '{room}')";

        try
        {
            deptId = ExecuteScalar<int>(query);
            Console.WriteLine($"✅ Добавлена новая кафедра '{name}' (ID: {deptId})");
            return deptId;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Ошибка при добавлении кафедры: {ex.Message}");
            return -1;
        }
    }

    static void AddStudentIfNotExists(string firstName, string lastName, DateTime birthDate, string groupName, int departmentId)
    {
        if (!DepartmentExists(departmentId))
        {
            Console.WriteLine($"❌ Кафедра с ID {departmentId} не существует");
            return;
        }

        string checkQuery = $@"
        SELECT StudentID FROM Students 
        WHERE FirstName = '{firstName.Replace("'", "''")}' 
          AND LastName = '{lastName.Replace("'", "''")}'
          AND BirthDate = '{birthDate:yyyy-MM-dd}'
          AND DepartmentID = {departmentId}";

        if (ExecuteScalar<int>(checkQuery) > 0)
        {
            Console.WriteLine($"Студент {firstName} {lastName} уже существует");
            return;
        }

        string insertQuery = $@"
        INSERT INTO Students (FirstName, LastName, BirthDate, GroupName, DepartmentID, AdmissionYear)
        VALUES ('{firstName.Replace("'", "''")}', '{lastName.Replace("'", "''")}', 
                '{birthDate:yyyy-MM-dd}', '{groupName}', {departmentId}, {DateTime.Now.Year})";

        int rows = ExecuteNonQuery(insertQuery);
        Console.WriteLine($"✅ Добавлен новый студент {firstName} {lastName} ({rows} строк)");
    }

    static void UpdateStudentGroupIfExists(int studentId, string groupName)
    {
        if (!StudentExists(studentId))
        {
            Console.WriteLine($"❌ Студент с ID {studentId} не существует");
            return;
        }

        string query = $@"
        UPDATE Students
        SET GroupName = '{groupName}'
        WHERE StudentID = {studentId}";

        int rows = ExecuteNonQuery(query);
        Console.WriteLine($"✅ Обновлена группа для студента ID {studentId} ({rows} строк)");
    }

    static void UpdateProfessorPositionIfExists(int professorId, string newPosition)
    {
        if (!ProfessorExists(professorId))
        {
            Console.WriteLine($"❌ Преподаватель с ID {professorId} не существует");
            return;
        }

        string query = $@"
        UPDATE Professors
        SET Position = '{newPosition.Replace("'", "''")}'
        WHERE ProfessorID = {professorId}";

        int rows = ExecuteNonQuery(query);
        Console.WriteLine($"✅ Обновлена должность преподавателя ID {professorId} ({rows} строк)");
    }

    static void DeleteStudentIfExists(int studentId)
    {
        if (!StudentExists(studentId))
        {
            Console.WriteLine($"❌ Студент с ID {studentId} не существует");
            return;
        }

        string query = $@"
        DELETE FROM Students
        WHERE StudentID = {studentId}";

        int rows = ExecuteNonQuery(query);
        Console.WriteLine($"✅ Удален студент ID {studentId} ({rows} строк)");
    }

    static void DeleteDepartmentIfExists(int departmentId)
    {
        if (!DepartmentExists(departmentId))
        {
            Console.WriteLine($"❌ Кафедра с ID {departmentId} не существует");
            return;
        }

        // Удаляем связанных студентов
        string deleteStudentsQuery = $@"
        DELETE FROM Students
        WHERE DepartmentID = {departmentId}";

        int studentRows = ExecuteNonQuery(deleteStudentsQuery);
        Console.WriteLine($"Удалено студентов: {studentRows}");

        // Удаляем кафедру
        string deleteDeptQuery = $@"
        DELETE FROM Departments
        WHERE DepartmentID = {departmentId}";

        int deptRows = ExecuteNonQuery(deleteDeptQuery);
        Console.WriteLine($"✅ Удалена кафедра ID {departmentId} ({deptRows} строк)");
    }

    // Методы проверки существования записей
    static bool FacultyExists(int facultyId)
    {
        string query = $"SELECT 1 FROM Faculties WHERE FacultyID = {facultyId}";
        object result = ExecuteScalar<object>(query);
        return result != null;
    }

    static bool DepartmentExists(int departmentId)
    {
        string query = $"SELECT 1 FROM Departments WHERE DepartmentID = {departmentId}";
        object result = ExecuteScalar<object>(query);
        return result != null;
    }

    static bool StudentExists(int studentId)
    {
        string query = $"SELECT 1 FROM Students WHERE StudentID = {studentId}";
        object result = ExecuteScalar<object>(query);
        return result != null;
    }

    static bool ProfessorExists(int professorId)
    {
        string query = $"SELECT 1 FROM Professors WHERE ProfessorID = {professorId}";
        object result = ExecuteScalar<object>(query);
        return result != null;
    }

    // Методы получения ID
    static int GetFacultyId(string name)
    {
        string query = $"SELECT FacultyID FROM Faculties WHERE FacultyName = '{name.Replace("'", "''")}'";
        object result = ExecuteScalar<object>(query);
        return result != null ? Convert.ToInt32(result) : -1;
    }

    static int GetDepartmentId(string name)
    {
        string query = $"SELECT DepartmentID FROM Departments WHERE DepartmentName = '{name.Replace("'", "''")}'";
        object result = ExecuteScalar<object>(query);
        return result != null ? Convert.ToInt32(result) : -1;
    }

    // Методы вывода информации
    static void DisplayAllStudents()
    {
        string query = @"
        SELECT s.StudentID, s.FirstName, s.LastName, s.BirthDate, s.GroupName, 
               d.DepartmentName, f.FacultyName
        FROM Students s
        JOIN Departments d ON s.DepartmentID = d.DepartmentID
        JOIN Faculties f ON d.FacultyID = f.FacultyID
        ORDER BY s.StudentID";

        Console.WriteLine("\nСписок всех студентов:");
        Console.WriteLine("ID | Имя | Фамилия | Дата рождения | Группа | Кафедра | Факультет");
        Console.WriteLine("--------------------------------------------------------------");

        using (SqlConnection connection = new SqlConnection(connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader["StudentID"]} | {reader["FirstName"]} | {reader["LastName"]} | " +
                                $"{((DateTime)reader["BirthDate"]).ToShortDateString()} | {reader["GroupName"]} | " +
                                $"{reader["DepartmentName"]} | {reader["FacultyName"]}");
            }
        }
    }

    static void DisplayAllDepartments()
    {
        string query = @"
        SELECT d.DepartmentID, d.DepartmentName, d.RoomNumber, 
               f.FacultyName, d.HeadName
        FROM Departments d
        JOIN Faculties f ON d.FacultyID = f.FacultyID
        ORDER BY d.DepartmentID";

        Console.WriteLine("\nСписок всех кафедр:");
        Console.WriteLine("ID | Название | Аудитория | Факультет | Заведующий");
        Console.WriteLine("--------------------------------------------------");

        using (SqlConnection connection = new SqlConnection(connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader["DepartmentID"]} | {reader["DepartmentName"]} | " +
                                $"{reader["RoomNumber"]} | {reader["FacultyName"]} | " +
                                $"{reader["HeadName"]}");
            }
        }
    }

    // Универсальные методы выполнения запросов
    static int ExecuteNonQuery(string query)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            connection.Open();
            return command.ExecuteNonQuery();
        }
    }

    static T ExecuteScalar<T>(string query)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            connection.Open();
            object result = command.ExecuteScalar();
            return (result == null || result == DBNull.Value) ? default(T) : (T)Convert.ChangeType(result, typeof(T));
        }
    }
}