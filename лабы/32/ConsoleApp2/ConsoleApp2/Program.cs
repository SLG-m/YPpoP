using System;
using System.Data.SqlClient;
//using Microsoft.Data.SqlClient;

namespace DatabaseApp
{
    class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("=== Управление базой данных FilesDB ===");

                // Инициализация подключения
                var dbConnection = new DatabaseConnection("(local)", "FilesDB");
                dbConnection.TestConnection();

                // Создание базы данных и таблиц
                var creator = new DatabaseCreator(dbConnection);
                creator.CreateDatabase();
                creator.CreateTables();

                // Работа с данными
                var repository = new FileRepository(dbConnection);
                repository.SeedDefaultData();

                // Демонстрация работы
                Console.WriteLine("\nДемонстрация операций:");

                // Добавление нового файла
                Console.WriteLine("\n1. Добавление нового файла:");
                repository.AddFile("report.pdf", 2500, DateTime.Now, DateTime.Now);

                // Обновление файла
                Console.WriteLine("\n2. Обновление файла с ID=1:");
                repository.UpdateFile(1, "updated_file.txt", 1500);

                // Удаление файла
                Console.WriteLine("\n3. Удаление файла с ID=2:");
                repository.DeleteFile(2);

                // Вывод всех файлов
                Console.WriteLine("\n4. Список всех файлов в базе:");
                repository.DisplayAllFiles();

                Console.WriteLine("\nВсе операции выполнены успешно!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }

    public class DatabaseConnection
    {
        private readonly string _connectionString;

        public DatabaseConnection(string server, string database, bool integratedSecurity = true, string userId = null, string password = null)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = server,
                InitialCatalog = database,
                IntegratedSecurity = integratedSecurity
            };

            if (!integratedSecurity)
            {
                builder.UserID = userId;
                builder.Password = password;
            }

            _connectionString = builder.ConnectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public void TestConnection()
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Подключение к базе данных успешно установлено!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка подключения: {ex.Message}");
                    throw;
                }
            }
        }
    }

    public class DatabaseCreator
    {
        private readonly DatabaseConnection _dbConnection;

        public DatabaseCreator(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void CreateDatabase()
        {
            string masterConnectionString = _dbConnection.GetConnection().ConnectionString.Replace(
                _dbConnection.GetConnection().Database, "master");

            using (var connection = new SqlConnection(masterConnectionString))
            {
                connection.Open();

                string createDbSql = $@"
                IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = '{_dbConnection.GetConnection().Database}')
                BEGIN
                    CREATE DATABASE [{_dbConnection.GetConnection().Database}]
                    ON PRIMARY 
                    (NAME = N'{_dbConnection.GetConnection().Database}', 
                     FILENAME = N'C:\\Program Files\\Microsoft SQL Server\\MSSQL16.MSSQLSERVER\\MSSQL\\DATA\\{_dbConnection.GetConnection().Database}.mdf', 
                     SIZE = 8192KB, MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB)
                    LOG ON 
                    (NAME = N'{_dbConnection.GetConnection().Database}_log', 
                     FILENAME = N'C:\\Program Files\\Microsoft SQL Server\\MSSQL16.MSSQLSERVER\\MSSQL\\DATA\\{_dbConnection.GetConnection().Database}_log.ldf', 
                     SIZE = 8192KB, MAXSIZE = 2048GB, FILEGROWTH = 65536KB);
                END";

                ExecuteNonQuery(connection, createDbSql);
                Console.WriteLine($"База данных {_dbConnection.GetConnection().Database} создана или уже существует.");
            }
        }

        public void CreateTables()
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();

                string createTableSql = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Files')
                BEGIN
                    CREATE TABLE [dbo].[Files](
                        [Id] [int] IDENTITY(1,1) NOT NULL,
                        [FileName] [varchar](50) NOT NULL,
                        [FileSize] [int] NOT NULL,
                        [CreationDate] [date] NOT NULL,
                        [CreationTime] [datetime] NOT NULL,
                        CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED ([Id] ASC)
                    ) ON [PRIMARY];
                END";

                ExecuteNonQuery(connection, createTableSql);
                Console.WriteLine("Таблица Files создана или уже существует.");
            }
        }

        private void ExecuteNonQuery(SqlConnection connection, string sql)
        {
            using (var command = new SqlCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public class FileRepository
    {
        private readonly DatabaseConnection _dbConnection;

        public FileRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void SeedDefaultData()
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();

                string checkDataSql = "SELECT COUNT(*) FROM Files";
                using (var command = new SqlCommand(checkDataSql, connection))
                {
                    int count = (int)command.ExecuteScalar();
                    if (count > 0) return;
                }

                string insertSql = @"
                SET IDENTITY_INSERT [dbo].[Files] ON;
                INSERT [dbo].[Files] ([Id], [FileName], [FileSize], [CreationDate], [CreationTime]) 
                VALUES 
                    (1, N'q', 1, CAST(N'2020-10-10' AS Date), CAST(N'2025-04-19T08:10:00.000' AS DateTime)),
                    (2, N'qwere', 4, CAST(N'2025-04-20' AS Date), CAST(N'2025-04-20T00:45:21.453' AS DateTime)),
                    (3, N'qwe', 95, CAST(N'2025-04-21' AS Date), CAST(N'2025-04-21T07:59:00.963' AS DateTime));
                SET IDENTITY_INSERT [dbo].[Files] OFF;";

                ExecuteNonQuery(connection, insertSql);
                Console.WriteLine("Добавлены данные по умолчанию.");
            }
        }

        public void AddFile(string fileName, int fileSize, DateTime creationDate, DateTime creationTime)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();

                string insertSql = @"
                INSERT INTO [dbo].[Files] ([FileName], [FileSize], [CreationDate], [CreationTime])
                VALUES (@FileName, @FileSize, @CreationDate, @CreationTime)";

                using (var command = new SqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@FileName", fileName);
                    command.Parameters.AddWithValue("@FileSize", fileSize);
                    command.Parameters.AddWithValue("@CreationDate", creationDate.Date);
                    command.Parameters.AddWithValue("@CreationTime", creationTime);

                    command.ExecuteNonQuery();
                    Console.WriteLine($"Файл '{fileName}' успешно добавлен.");
                }
            }
        }

        public void UpdateFile(int id, string newFileName, int newFileSize)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();

                string updateSql = @"
                UPDATE [dbo].[Files]
                SET [FileName] = @FileName,
                    [FileSize] = @FileSize
                WHERE [Id] = @Id";

                using (var command = new SqlCommand(updateSql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@FileName", newFileName);
                    command.Parameters.AddWithValue("@FileSize", newFileSize);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected > 0
                        ? $"Файл с ID {id} успешно обновлен."
                        : $"Файл с ID {id} не найден.");
                }
            }
        }

        public void DeleteFile(int id)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();

                string deleteSql = "DELETE FROM [dbo].[Files] WHERE [Id] = @Id";

                using (var command = new SqlCommand(deleteSql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected > 0
                        ? $"Файл с ID {id} успешно удален."
                        : $"Файл с ID {id} не найден.");
                }
            }
        }

        public void DisplayAllFiles()
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();

                string selectSql = "SELECT * FROM [dbo].[Files] ORDER BY [Id]";

                using (var command = new SqlCommand(selectSql, connection))
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("В таблице нет данных.");
                        return;
                    }

                    Console.WriteLine("ID | Имя файла         | Размер | Дата создания  | Время создания");
                    Console.WriteLine("---------------------------------------------------------------");

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Id"],-2} | {reader["FileName"],-17} | {reader["FileSize"],-6} | " +
                                        $"{((DateTime)reader["CreationDate"]).ToShortDateString(),-13} | " +
                                        $"{((DateTime)reader["CreationTime"]).ToShortTimeString()}");
                    }
                }
            }
        }

        private void ExecuteNonQuery(SqlConnection connection, string sql)
        {
            using (var command = new SqlCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}