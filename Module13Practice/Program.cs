using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Задача 1
        Console.WriteLine("Задача 1");
        List<int> numbers = new List<int> { 10, 5, 8, 15, 7, 20, 12 };
        int secondMax = numbers.OrderByDescending(x => x).Skip(1).First();
        int position = numbers.IndexOf(secondMax);

        Console.WriteLine($"Позиция второго максимального элемента: {position}, Значение: {secondMax}");

        numbers.RemoveAll(x => x % 2 != 0);

        Console.WriteLine("Список после удаления нечетных элементов:");
        foreach (var number in numbers)
        {
            Console.WriteLine(number);
        }

        // Задача 2
        Console.WriteLine("Задача 2");
        List<double> doubles = new List<double> { 2.5, 3.8, 4.2, 5.1, 2.0 };
        double average = doubles.Average();

        Console.WriteLine("Элементы, значение которых больше среднего арифметического:");
        foreach (var number in doubles.Where(x => x > average))
        {
            Console.WriteLine(number);
        }

        // Задача 3
        Console.WriteLine("Задача 3");
        string inputFileName = @"C:\\Users\\User\\source\\repos\\Module13Practice\\input.txt";
        string outputFileName = "output.txt";
        string[] lines = File.ReadAllLines(inputFileName);
        int[] numbersFromFile = lines.Select(int.Parse).ToArray();
        File.WriteAllLines(outputFileName, numbersFromFile.Reverse().Select(x => x.ToString()));
        Console.WriteLine("Числа были записаны в обратном порядке в файле output.txt");

        // Задача 4
        Console.WriteLine("Задача 4");
        string employeesInputFileName = @"C:\Users\User\source\repos\Module13Practice\employees.txt";
        string employeesOutputFileName = "sorted_employees.txt";
        string[] employeesLines = File.ReadAllLines(employeesInputFileName);
        var under10000 = employeesLines.Where(line => int.Parse(line.Split(',')[5]) < 10000);
        var over10000 = employeesLines.Where(line => int.Parse(line.Split(',')[5]) >= 10000);
        File.WriteAllLines(employeesOutputFileName, under10000.Concat(over10000));
        Console.WriteLine("Данные о сотрудниках были сохранены в новом файле sorted_employees.txt");

        // Задача 5
        Console.WriteLine("Задача 5");
        MusicCatalog catalog = new MusicCatalog();
        catalog.AddDisk("Rock Classics");
        catalog.AddDisk("Pop Hits");

        catalog.AddSong("Rock Classics", new Song { Title = "Stairway to Heaven", Artist = "Led Zeppelin" });
        catalog.AddSong("Rock Classics", new Song { Title = "Bohemian Rhapsody", Artist = "Queen" });

        catalog.AddSong("Pop Hits", new Song { Title = "Shape of You", Artist = "Ed Sheeran" });
        catalog.AddSong("Pop Hits", new Song { Title = "Happy", Artist = "Pharrell Williams" });

        catalog.DisplayCatalog();

        Console.WriteLine("Songs by Queen:");
        foreach (var song in catalog.FindSongsByArtist("Queen"))
        {
            Console.WriteLine(song);
        }
    }
}

public enum ServiceType
{
    Credit,
    Deposit,
    Consultation
}

public class Client
{
    public string Id { get; }
    public ServiceType ServiceType { get; }

    public Client(string id, ServiceType serviceType)
    {
        Id = id;
        ServiceType = serviceType;
    }
}

public class BankService
{
    private Queue<Client> clientQueue = new Queue<Client>();

    public void EnqueueClient(Client client)
    {
        clientQueue.Enqueue(client);
    }

    public Client ServeNextClient()
    {
        return clientQueue.Count > 0 ? clientQueue.Dequeue() : null;
    }

    public void DisplayQueue()
    {
        Console.WriteLine("Текущая очередь:");

        foreach (var client in clientQueue)
        {
            Console.WriteLine($"{client.Id} - {client.ServiceType}");
        }

        Console.WriteLine();
    }
}

public class Song
{
    public string Title { get; set; }
    public string Artist { get; set; }

    public override string ToString()
    {
        return $"{Title} by {Artist}";
    }
}

public class MusicCatalog
{
    private Dictionary<string, List<Song>> catalog = new Dictionary<string, List<Song>>();

    public void AddDisk(string diskName)
    {
        catalog.Add(diskName, new List<Song>());
    }

    public void RemoveDisk(string diskName)
    {
        catalog.Remove(diskName);
    }

    public void AddSong(string diskName, Song song)
    {
        if (catalog.ContainsKey(diskName))
        {
            catalog[diskName].Add(song);
        }
    }

    public void RemoveSong(string diskName, Song song)
    {
        if (catalog.ContainsKey(diskName))
        {
            catalog[diskName].Remove(song);
        }
    }

    public void DisplayCatalog()
    {
        Console.WriteLine("Music Catalog:");

        foreach (var kvp in catalog)
        {
            Console.WriteLine($"{kvp.Key}:");

            foreach (var song in kvp.Value)
            {
                Console.WriteLine($"  {song}");
            }
        }
    }

    public IEnumerable<Song> FindSongsByArtist(string artist)
    {
        foreach (var kvp in catalog)
        {
            foreach (var song in kvp.Value)
            {
                if (song.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase))
                {
                    yield return song;
                }
            }
        }
    }
}
