
using System.ComponentModel;
using System.Reflection;

Random rand = new Random();
Deck deck = new Deck();
Player player1 = new Player();
Player player2 = new Player();
deck.Deal(player1, player2);
Game.War(player1, player2);

public class Game
{
    public static void War(Player player1, Player player2)
    {
        Console.WriteLine("Game On!\n");
        while(player1.hand.Count > 0 && player2.hand.Count > 0)
        {
            Card p1card = player1.hand[0];
            Card p2card = player2.hand[0];
            Console.WriteLine($"Player One's Card is the {p1card.value} of {p2card.type}");
            Console.WriteLine($"Player Two's Card is the {p2card.value} of {p2card.type}");
            if((int)p1card.value > (int)p2card.value)
            {
                Console.WriteLine("Player One wins this round!");
                win(player1, player2);
                player1.hand.Add(player1.hand[0]);
                player1.hand.RemoveAt(0);
                Console.WriteLine($"Card Count: Player One: {player1.hand.Count}, Player 2: {player2.hand.Count}\n");
            }
            else if((int)p2card.value > (int)p1card.value)
            {
                Console.WriteLine("Player Two wins this round!");
                win(player2, player1);
                player2.hand.Add(player2.hand[0]);
                player2.hand.RemoveAt(0);
                Console.WriteLine($"Card Count: Player One: {player1.hand.Count}, Player 2: {player2.hand.Count}\n");
            }
            else if ((int)p1card.value == (int)p2card.value)
            {
                Console.WriteLine("WAR!!!");
                war(player1, player2);
                Console.WriteLine($"Card Count: Player One: {player1.hand.Count}, Player 2: {player2.hand.Count}\n");
            }
            Thread.Sleep(2000);
        }
        if (player1.hand.Count > 0)
        {
            Console.WriteLine("Player 1 Wins!");
        }
        else
        {
            Console.WriteLine("Player 2 Wins!");
        }
    }
    public static void win(Player winner, Player loser)
    {
        winner.hand.Add(loser.hand[0]);
        loser.hand.RemoveAt(0);
    }
    public static void war(Player player1, Player player2)
    {
        List<Card> p1stack = new List<Card>();
        List<Card> p2strack = new List<Card>();
        int p1total = 0;
        int p2total = 0;
        for(int i = 0; i<3; i++)
        {
            Console.WriteLine($"Player One's card: the {player1.hand[i+1].value} of {player1.hand[i+1].type}.");
            Console.WriteLine($"Player Two's card: the {player2.hand[i+1].value} of {player2.hand[i+1].type}.");
            p1total += (int)player1.hand[i+1].value;
            p2total += (int)player2.hand[i+1].value;
            Console.WriteLine($"Player One's pot total: {p1total}");
            Console.WriteLine($"Player Two's pot total: {p2total}");
        }
        if(p1total > p2total)
        {
            Console.WriteLine("Player One wins the pot!");
            for (int i = 0; i < 4; i++)
            {
                player1.hand.Add(player2.hand[0]);
                player2.hand.RemoveAt(0);
            }
        }
        else if (p2total > p1total)
        {
            Console.WriteLine("Player Two wins the pot!");
            for (int i = 0; i < 4; i++)
            {
                player2.hand.Add(player2.hand[0]);
                player1.hand.RemoveAt(0);
            }
        }
        else
        {
            Console.WriteLine("No winner. Everyone keeps their cards and the decks will be shuffled!");
            player1.hand.OrderBy(x => Guid.NewGuid()).ToList();
            player2.hand.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}

public class Deck
{
    public List<Card> deck = new List<Card>();
    public Deck()
    {
        //int[] nums = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14];
        Value[] nums = [Value.One,Value.Two, Value.Four, Value.Five, Value.Six, Value.Seven, Value.Eight, Value.Nine, Value.Ten, Value.Jack, Value.Queen, Value.King, Value.Ace];
        //string[] types = ["Diamond", "Club", "Heart", "Spade"];
        Type[] types = [Type.Spade, Type.Club, Type.Diamond, Type.Heart];
        foreach (Type type in types)
        {
            foreach (Value num in nums)
            {
                deck.Add(new Card(num, type));
            }
        }
        deck = deck.OrderBy(deck => Guid.NewGuid()).ToList();
    }
    public void Deal(Player player1, Player player2)
    {
        while(deck.Count > 0)
        {
            player1.hand.Add(deck[0]);
            deck.RemoveAt(0);
            player2.hand.Add(deck[0]);
            deck.RemoveAt(0);
        }
    }
}

public class Player
{
    public List<Card> hand = new List<Card>();
}

public class Card
{
    public Value value { get; }
    public Type type { get; }
    public Card(Value _value, Type _type)
    {
        value = _value;
        type = _type;
    }
}
public enum Value
{
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13,
    Ace = 14
}
public enum Type
{
    Spade,
    Club,
    Diamond,
    Heart
}
