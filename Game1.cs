using System;
using System.Collections.Generic;
using System.Linq;

public class Player
{
    public string Name { get; private set; }
    public int RollResult { get; set; }

    public Player(string name)
    {
        Name = name;
    }
}

public class Dice
{
    private int sides;
    private Random random = new Random();

    public Dice(int sides)
    {
        this.sides = Math.Max(sides, 2); // Ensure at least 2 sides
    }

    public int Roll()
    {
        return random.Next(1, sides + 1);
    }
}

public class Game
{
    private List<Player> players = new List<Player>();
    private Dice dice;

    public Game(int sides)
    {
        dice = new Dice(sides);
    }

    public void AddPlayer(string name)
    {
        players.Add(new Player(name));
    }

    public void PlayRound()
    {
        foreach (var player in players)
        {
            player.RollResult = dice.Roll();
            Console.WriteLine($"{player.Name} rolled: {player.RollResult}");
        }
    }

    public void ShowWinner()
    {
        var highestRoll = players.Max(p => p.RollResult);
        var winners = players.Where(p => p.RollResult == highestRoll).Select(p => p.Name);

        Console.WriteLine("\nWinner(s):");
        foreach (var winner in winners)
        {
            Console.WriteLine(winner);
        }
    }
}

class RollTheDiceGame
{
    static void Main(string[] args)
    {
        bool playAgain = true;
        while (playAgain)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Roll The Dice Game:");

            int numberOfPlayers = GetNumberOfPlayers();
            int sides = GetDiceSides();
            Game game = new Game(sides);

            for (int i = 1; i <= numberOfPlayers; i++)
            {
                string playerName = GetPlayerName(i);
                game.AddPlayer(playerName);
            }

            Console.WriteLine();
            game.PlayRound();
            game.ShowWinner();

            Console.WriteLine("\nPlay again? (yes/no)");
            playAgain = Console.ReadLine().Trim().ToLower() == "yes";
        }
        Console.WriteLine("Thank you for playing. Goodbye!");
    }

    static int GetNumberOfPlayers()
    {
        int numberOfPlayers;
        do
        {
            Console.Write("Enter the number of players: ");
        } while (!int.TryParse(Console.ReadLine(), out numberOfPlayers) || numberOfPlayers < 1);
        return numberOfPlayers;
    }

    static string GetPlayerName(int playerNumber)
    {
        string playerName;
        do
        {
            Console.Write($"Enter name for Player {playerNumber}: ");
            playerName = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(playerName));
        return playerName;
    }

    static int GetDiceSides()
    {
        int sides;
        Console.Write("Enter the number of sides for the dice (6 is default): ");
        while (!int.TryParse(Console.ReadLine(), out sides) || sides < 1)
        {
            Console.Write("Invalid input. Please enter a positive integer for the number of sides: ");
        }
        return sides;
    }
}
