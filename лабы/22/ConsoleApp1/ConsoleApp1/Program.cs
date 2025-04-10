using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Базовый класс для всех сущностей
public abstract class Entity
{
    public int Id { get; set; }
}

// Класс команды
public class Team : Entity
{
    public string Name { get; set; }
    public List<Player> Players { get; set; } = new List<Player>();

    public override string ToString()
    {
        return $"Team: Id={Id}, Name='{Name}', Players={Players.Count}";
    }
}

// Класс игрока
public class Player : Entity
{
    public string Name { get; set; }
    public Team Team { get; set; }

    public override string ToString()
    {
        return $"Player: Id={Id}, Name='{Name}', Team='{Team?.Name ?? "None"}'";
    }
}

// Класс матча
public class Match : Entity
{
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public string Result { get; set; }
    public Team Team1 { get; set; }
    public Team Team2 { get; set; }
    public List<Player> Players { get; set; } = new List<Player>();

    public override string ToString()
    {
        return $"Match: Id={Id}, Date={Date.ToShortDateString()}, Location='{Location}', " +
               $"Result='{Result}', Teams='{Team1?.Name ?? "None"} vs {Team2?.Name ?? "None"}', " +
               $"Players={Players.Count}";
    }
}

// Базовый фабричный метод
public abstract class FootballFactory
{
    public abstract Team CreateTeam(int id, string name);
    public abstract Player CreatePlayer(int id, string name, Team team);
    public abstract Match CreateMatch(int id, DateTime date, string location, string result,
                                     Team team1, Team team2, List<Player> players);
}

// Конкретная реализация фабрики
public class ConcreteFootballFactory : FootballFactory
{
    public override Team CreateTeam(int id, string name)
    {
        return new Team { Id = id, Name = name };
    }

    public override Player CreatePlayer(int id, string name, Team team)
    {
        var player = new Player { Id = id, Name = name, Team = team };
        team?.Players.Add(player);
        return player;
    }

    public override Match CreateMatch(int id, DateTime date, string location, string result,
                                     Team team1, Team team2, List<Player> players)
    {
        return new Match
        {
            Id = id,
            Date = date,
            Location = location,
            Result = result,
            Team1 = team1,
            Team2 = team2,
            Players = players
        };
    }
}

// Класс для работы с данными чемпионата
public class Championship
{
    private readonly FootballFactory _factory;
    public List<Team> Teams { get; } = new List<Team>();
    public List<Player> Players { get; } = new List<Player>();
    public List<Match> Matches { get; } = new List<Match>();

    public Championship(FootballFactory factory)
    {
        _factory = factory;
    }

    public Team AddTeam(int id, string name)
    {
        var team = _factory.CreateTeam(id, name);
        Teams.Add(team);
        return team;
    }

    public Player AddPlayer(int id, string name, Team team)
    {
        var player = _factory.CreatePlayer(id, name, team);
        Players.Add(player);
        return player;
    }

    public Match AddMatch(int id, DateTime date, string location, string result,
                         Team team1, Team team2, List<Player> players)
    {
        var match = _factory.CreateMatch(id, date, location, result, team1, team2, players);
        Matches.Add(match);
        return match;
    }

    // Сохранение данных в файл
    public void SaveToFile(string filename)
    {
        using (var writer = new StreamWriter(filename))
        {
            // Сохраняем команды
            foreach (var team in Teams)
            {
                writer.WriteLine($"team {team.Id} {team.Name}");
            }

            // Сохраняем игроков
            foreach (var player in Players)
            {
                writer.WriteLine($"player {player.Id} {player.Name} {player.Team?.Id ?? -1}");
            }

            // Сохраняем матчи
            foreach (var match in Matches)
            {
                var playerIds = string.Join(",", match.Players.Select(p => p.Id));
                writer.WriteLine($"match {match.Id} {match.Date:yyyy-MM-dd} {match.Location} {match.Result} " +
                               $"{match.Team1?.Id ?? -1} {match.Team2?.Id ?? -1} {playerIds}");
            }
        }
    }

    // Загрузка данных из файла
    public void LoadFromFile(string filename)
    {
        Teams.Clear();
        Players.Clear();
        Matches.Clear();

        var lines = File.ReadAllLines(filename);

        // Первый проход: создаем все объекты без связей
        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            if (parts.Length < 2) continue;

            var type = parts[0];
            var id = int.Parse(parts[1]);

            switch (type)
            {
                case "team":
                    var name = string.Join(" ", parts.Skip(2));
                    AddTeam(id, name);
                    break;
                case "player":
                    var playerName = string.Join(" ", parts.Skip(2).Take(parts.Length - 3));
                    AddPlayer(id, playerName, null); // Команду установим позже
                    break;
                case "match":
                    var date = DateTime.Parse(parts[2]);
                    var location = parts[3];
                    var result = parts[4];
                    AddMatch(id, date, location, result, null, null, new List<Player>());
                    break;
            }
        }

        // Второй проход: устанавливаем связи
        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            if (parts.Length < 2) continue;

            var type = parts[0];
            var id = int.Parse(parts[1]);

            switch (type)
            {
                case "player":
                    var player = Players.FirstOrDefault(p => p.Id == id);
                    var teamId = int.Parse(parts[^1]);
                    if (teamId != -1)
                    {
                        player.Team = Teams.FirstOrDefault(t => t.Id == teamId);
                        player.Team?.Players.Add(player);
                    }
                    break;
                case "match":
                    var match = Matches.FirstOrDefault(m => m.Id == id);
                    var team1Id = int.Parse(parts[5]);
                    var team2Id = int.Parse(parts[6]);
                    match.Team1 = Teams.FirstOrDefault(t => t.Id == team1Id);
                    match.Team2 = Teams.FirstOrDefault(t => t.Id == team2Id);

                    if (parts.Length > 7)
                    {
                        var playerIds = parts[7].Split(',').Select(int.Parse);
                        foreach (var playerId in playerIds)
                        {
                            var matchPlayer = Players.FirstOrDefault(p => p.Id == playerId);
                            if (matchPlayer != null)
                            {
                                match.Players.Add(matchPlayer);
                            }
                        }
                    }
                    break;
            }
        }
    }
}

// Пример использования
class Program
{
    static void Main(string[] args)
    {
        var factory = new ConcreteFootballFactory();
        var championship = new Championship(factory);

        //// Создаем тестовые данные
        //var team1 = championship.AddTeam(1, "Real Madrid");
        //var team2 = championship.AddTeam(2, "Barcelona");

        //var player1 = championship.AddPlayer(1, "Player 1", team1);
        //var player2 = championship.AddPlayer(2, "Player 2", team1);
        //var player3 = championship.AddPlayer(3, "Player 3", team2);
        //var player4 = championship.AddPlayer(4, "Player 4", team2);

        //var matchPlayers = new List<Player> { player1, player2, player3, player4 };
        //var match = championship.AddMatch(1, DateTime.Now, "Madrid", "2:1", team1, team2, matchPlayers);

        //// Сохраняем данные в файл
        //championship.SaveToFile("championship.txt");

        // Загружаем данные из файла в новый объект
        var loadedChampionship = new Championship(factory);
        loadedChampionship.LoadFromFile("championship.txt");

        // Выводим загруженные данные
        Console.WriteLine("Teams:");
        foreach (var team in loadedChampionship.Teams)
        {
            Console.WriteLine(team);
        }

        Console.WriteLine("\nPlayers:");
        foreach (var player in loadedChampionship.Players)
        {
            Console.WriteLine(player);
        }

        Console.WriteLine("\nMatches:");
        foreach (var m in loadedChampionship.Matches)
        {
            Console.WriteLine(m);
        }
    }
}