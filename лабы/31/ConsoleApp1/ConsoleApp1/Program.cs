using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            MyApplication app = new MyApplication();
            app.Run();
        }
    }

public class MyApplication
    {
        private MyDataBase _database;
        private string _dbFilePath;

        public MyApplication()
        {
            _database = new MyDataBase();
        }

        public void Run()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Главное меню:");
                Console.WriteLine("1. Подключиться к базе данных");
                Console.WriteLine("2. Добавить файл в базу данных");
                Console.WriteLine("3. Просмотреть все файлы");
                Console.WriteLine("4. Отключиться от базы данных");
                Console.WriteLine("5. Выход");
                Console.Write("Выберите пункт меню: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ConnectToDatabase();
                        break;
                    case "2":
                        AddFile();
                        break;
                    case "3":
                        ViewFiles();
                        break;
                    case "4":
                        DisconnectFromDatabase();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ConnectToDatabase()
        {
            Console.Write("Введите путь к файлу базы данных Access: ");
            _dbFilePath = Console.ReadLine();

            Console.WriteLine("Выберите тип подключения:");
            Console.WriteLine("1. SqlConnection");
            Console.WriteLine("2. OleDbConnection");
            Console.WriteLine("3. OdbcConnection");
            Console.Write("Ваш выбор: ");
            string connectionTypeChoice = Console.ReadLine();

            string connectionType;
            string connectionString;

            switch (connectionTypeChoice)
            {
                case "1":
                    connectionType = "SqlConnection";
                    connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={_dbFilePath};Integrated Security=True";
                    break;
                case "2":
                    connectionType = "OleDbConnection";
                    connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={_dbFilePath};Persist Security Info=False;";
                    break;
                case "3":
                    connectionType = "OdbcConnection";
                    connectionString = $"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={_dbFilePath};";
                    break;
                default:
                    Console.WriteLine("Неверный выбор типа подключения.");
                    return;
            }

            try
            {
                _database.SetConnectionType(connectionType, connectionString);
                _database.Connect();
                Console.WriteLine("Подключение успешно установлено!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подключения: {ex.Message}");
            }
            Console.ReadKey();
        }

        private void AddFile()
        {
            Console.Write("Введите имя файла (до 30 символов): ");
            string fileName = Console.ReadLine();

            if (fileName.Length > 30)
            {
                Console.WriteLine("Имя файла слишком длинное!");
                Console.ReadKey();
                return;
            }

            Console.Write("Введите размер файла (целое число): ");
            if (!int.TryParse(Console.ReadLine(), out int fileSize))
            {
                Console.WriteLine("Некорректный размер файла!");
                Console.ReadKey();
                return;
            }

            Console.Write("Введите дату создания (дд.мм.гггг): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime creationDate))
            {
                Console.WriteLine("Некорректная дата!");
                Console.ReadKey();
                return;
            }

            Console.Write("Введите время создания (чч:мм): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime creationTime))
            {
                Console.WriteLine("Некорректное время!");
                Console.ReadKey();
                return;
            }

            // Объединяем дату и время
            DateTime fullDateTime = creationDate.Date + creationTime.TimeOfDay;

            string query = $"INSERT INTO Files (FileName, FileSize, CreationDate, CreationTime) VALUES ('{fileName}', {fileSize}, '{fullDateTime:dd.MM.yyyy}', '{fullDateTime:HH:mm}')";

            try
            {
                int rowsAffected = _database.ExecuteInsert(query);
                Console.WriteLine($"Добавлено {rowsAffected} записей.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении файла: {ex.Message}");
            }
            Console.ReadKey();
        }

        private void ViewFiles()
        {
            string query = "SELECT FileName, FileSize, CreationDate, CreationTime FROM Files";

            try
            {
                DataTable files = _database.ExecuteSelect(query);

                Console.WriteLine("\nСписок файлов:");
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("| Имя файла          | Размер | Дата       | Время  |");
                Console.WriteLine("----------------------------------------------------------");

                foreach (DataRow row in files.Rows)
                {
                    // Форматируем дату и время
                    DateTime dateValue;
                    DateTime timeValue;

                    string formattedDate = row["CreationDate"].ToString();
                    string formattedTime = row["CreationTime"].ToString();

                    if (DateTime.TryParse(row["CreationDate"].ToString(), out dateValue))
                    {
                        formattedDate = dateValue.ToString("dd.MM.yyyy");
                    }

                    if (DateTime.TryParse(row["CreationTime"].ToString(), out timeValue))
                    {
                        formattedTime = timeValue.ToString("HH:mm");
                    }

                    Console.WriteLine($"| {row["FileName"].ToString().PadRight(19)} | {row["FileSize"].ToString().PadRight(6)} | {formattedDate.PadRight(10)} | {formattedTime.PadRight(5)} |");
                }

                Console.WriteLine("----------------------------------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении списка файлов: {ex.Message}");
            }
            Console.ReadKey();
        }

        private void DisconnectFromDatabase()
        {
            try
            {
                _database.Disconnect();
                Console.WriteLine("Отключение от базы данных выполнено успешно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при отключении: {ex.Message}");
            }
            Console.ReadKey();
        }
    }
public class MyDataBase
    {
        private IDbConnection _connection;
        private DatabaseType _dbType;
        private string _connectionString;

        private enum DatabaseType
        {
            SqlConnection,
            OleDbConnection,
            OdbcConnection
        }

        // Конструктор
        public MyDataBase()
        {
            _connection = null;
        }

        // Метод для установки типа подключения
        public void SetConnectionType(string dbType, string connectionString)
        {
            _connectionString = connectionString;

            switch (dbType.ToLower())
            {
                case "sqlconnection":
                    _dbType = DatabaseType.SqlConnection;
                    break;
                case "oledbconnection":
                    _dbType = DatabaseType.OleDbConnection;
                    break;
                case "odbcconnection":
                    _dbType = DatabaseType.OdbcConnection;
                    break;
                default:
                    throw new ArgumentException("Неизвестный тип подключения");
            }
        }

        // Метод для подключения к базе данных
        public void Connect()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                return;
            }

            switch (_dbType)
            {
                case DatabaseType.SqlConnection:
                    _connection = new SqlConnection(_connectionString);
                    break;
                case DatabaseType.OleDbConnection:
                    _connection = new OleDbConnection(_connectionString);
                    break;
                case DatabaseType.OdbcConnection:
                    _connection = new OdbcConnection(_connectionString);
                    break;
            }

            _connection.Open();
        }

        // Метод для отключения от базы данных
        public void Disconnect()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        // Метод для выполнения запроса на вставку
        public int ExecuteInsert(string query)
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                throw new InvalidOperationException("Соединение не установлено");
            }

            using (IDbCommand command = CreateCommand(query))
            {
                return command.ExecuteNonQuery();
            }
        }

        // Метод для выполнения запроса на выборку
        public DataTable ExecuteSelect(string query)
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                throw new InvalidOperationException("Соединение не установлено");
            }

            using (IDbCommand command = CreateCommand(query))
            {
                using (IDataReader reader = command.ExecuteReader())
                {
                    DataTable result = new DataTable();
                    result.Load(reader);
                    return result;
                }
            }
        }

        // Вспомогательный метод для создания команды
        private IDbCommand CreateCommand(string query)
        {
            IDbCommand command = _connection.CreateCommand();
            command.CommandText = query;
            return command;
        }
    }

}
