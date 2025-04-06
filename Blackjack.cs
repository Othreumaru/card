using
using System;
using System.Collections.Generic;

public class Card
{
    public Rank Rank { get; set; }
    public Suit Suit { get; set; }

    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}

public enum Rank
{
    Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten,
    Jack, Queen, King, Ace
}

public enum Suit
{
    Hearts, Diamonds, Clubs, Spades
}

public class Deck
{
    private List<Card> cards = new List<Card>();

    public void Initialize()
    {
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                cards.Add(new Card { Rank = rank, Suit = suit });
            }
        }
    }

    public void Shuffle()
    {
        Random random = new Random();
        int n = cards.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            (cards[k], cards[n]) = (cards[n], cards[k]);
        }
    }

    public Card DealCard()
    {
        if (cards.Count == 0)
        {
            Initialize();
            Shuffle();
        }
        return cards[--cards.Count];
    }

    public void DisplayCards(List<Card> hand, bool showDealerHand = true)
    {
        foreach (var card in hand)
        {
            Console.WriteLine(card);
        }
        if (!showDealerHand)
        {
            Console.WriteLine("Hidden Card");
        }
    }
}

public class Player
{
    public List<Card> Hand { get; set; } = new List<Card>();
    public int Score { get; set; }

    public void ClearHand()
    {
        Hand.Clear();
        Score = 0;
    }
}

class Program
{
    static void Main(string[] args)
    {
        WelcomeMessage();

        while (true)
        {
            Deck deck = new Deck();
            Player player = new Player();
            Player dealer = new Player();

            deck.Initialize();
            deck.Shuffle();

            // Deal initial cards
            player.Hand.Add(deck.DealCard());
            dealer.Hand.Add(deck.DealCard());
            player.Hand.Add(deck.DealCard());
            dealer.Hand.Add(deck.DealCard());

            bool playerBusted = false;

            while (!playerBusted)
            {
                Console.WriteLine("\nYour hand:");
                deck.DisplayCards(player.Hand);
                Console.WriteLine($"\nTotal score: {CalculateHandValue(player.Hand)}");

                Console.WriteLine("\nDealer's hand:");
                List<Card> dealerDisplay = new List<Card>();
                dealerDisplay.Add(dealer.Hand[0]);
                dealerDisplay.Add(new Card()); // Hidden card
                deck.DisplayCards(dealerDisplay, false);

                Console.WriteLine("\nEnter 'H' to Hit or 'S' to Stand: ");
                string choice = Console.ReadLine().ToUpper();

                if (choice == "H")
                {
                    player.Hand.Add(deck.DealCard());
                    int playerScore = CalculateHandValue(player.Hand);
                    Console.WriteLine($"\nYou drew a {player.Hand[^1]}");
                    if (playerScore > 21)
                    {
                        playerBusted = true;
                        Console.WriteLine("\nBust! You lose!");
                        break;
                    }
                }
                else if (choice == "S")
                {
                    break;
        }
            }

            // Dealer's turn
            while (CalculateHandValue(dealer.Hand) < 17)
            {
                dealer.Hand.Add(deck.DealCard());
            }

            int playerScore = CalculateHandValue(player.Hand);
            int dealerScore = CalculateHandValue(dealer.Hand);

            Console.WriteLine("\nFinal hands:");
            Console.WriteLine("\nYour hand:");
            deck.DisplayCards(player.Hand, true);
            Console.WriteLine($"\nTotal score: {playerScore}");

            Console.WriteLine("\nDealer's hand:");
            deck.DisplayCards(dealer.Hand, true);
            Console.WriteLine($"\nTotal score: {dealerScore}");

            if (playerBusted)
            {
                Console.WriteLine("You busted! Dealer wins!");
            }
            else if (dealerScore > 21 || playerScore > dealerScore)
            {
                Console.WriteLine("\nYou win! Congratulations!");
            }
            else
            {
                Console.WriteLine("\nDealer wins!");
            }

            // Play again?
            Console.WriteLine("\nWould you like to play again? Enter 'Y' for Yes or 'N' for No: ");
            string playAgain = Console.ReadLine().ToUpper();
            if (playAgain != "Y")
            {
                break;
        }
    }
}

    private static int CalculateHandValue(List<Card> hand)
    {
        int value = 0;
        int numAces = 0;

        foreach (Card card in hand)
        {
            switch (card.Rank)
            {
                case Rank.Two: value += 2; break;
                case Rank.Three: value += 3; break;
                case Rank.Four: value += 4; break;
                case Rank.Five: value += 5; break;
                case Rank.Six: value += 6; break;
                case Rank.Seven: value += 7; break;
                case Rank.Eight: value += 8; break;
                case Rank.Nine: value += 9; break;
                case Rank.Ten:
                case Rank.Jack:
                case Rank.Queen:
                case Rank.King: value += 10; break;
                case Rank.Ace: numAces++; break;
            }
        }

        // Handle Aces (each Ace can be 11 or 1)
        for (int i = 0; i < numAces; i++)
        {
            if (value + 11 <= 21)
            {
                value += 11;
            }
            else
            {
                value += 1;
            }
        }

        return value;
    }

    private static void WelcomeMessage()
    {
        Console.WriteLine("Welcome to Blackjack!");
        Console.WriteLine("Try to get as close to 21 as possible without busting.");
        Console.WriteLine("Press Enter to begin...");
        Console.ReadLine();
    }
}
using System;
using System.Collections.Generic;

public class Card
{
    public Rank Rank { get; set; }
    public Suit Suit { get; set; }

    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}

public enum Rank
{
    Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten,
    Jack, Queen, King, Ace
}

public enum Suit
{
    Hearts, Diamonds, Clubs, Spades
}

public class Deck
{
    private List<Card> cards = new List<Card>();

    public void Initialize()
    {
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                cards.Add(new Card { Rank = rank, Suit = suit });
            }
        }
    }

    public void Shuffle()
    {
        Random random = new Random();
        int n = cards.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            (cards[k], cards[n]) = (cards[n], cards[k]);
        }
    }

    public Card DealCard()
    {
        if (cards.Count == 0)
        {
            Initialize();
            Shuffle();
        }
        return cards[--cards.Count];
    }

    public void DisplayCards(List<Card> hand, bool showDealerHand = true)
    {
        foreach (var card in hand)
        {
            Console.WriteLine(card);
        }
        if (!showDealerHand)
        {
            Console.WriteLine("Hidden Card");
        }
    }
}

public class Player
{
    public List<Card> Hand { get; set; } = new List<Card>();
    public int Score { get; set; }

    public void ClearHand()
    {
        Hand.Clear();
        Score = 0;
    }
}

class Program
{
    static void Main(string[] args)
    {
        WelcomeMessage();

        while (true)
        {
            Deck deck = new Deck();
            Player player = new Player();
            Player dealer = new Player();

            deck.Initialize();
            deck.Shuffle();

            // Deal initial cards
            player.Hand.Add(deck.DealCard());
            dealer.Hand.Add(deck.DealCard());
            player.Hand.Add(deck.DealCard());
            dealer.Hand.Add(deck.DealCard());

            bool playerBusted = false;

            while (!playerBusted)
            {
                Console.WriteLine("\nYour hand:");
                deck.DisplayCards(player.Hand);
                Console.WriteLine($"\nTotal score: {CalculateHandValue(player.Hand)}");

                Console.WriteLine("\nDealer's hand:");
                List<Card> dealerDisplay = new List<Card>();
                dealerDisplay.Add(dealer.Hand[0]);
                dealerDisplay.Add(new Card()); // Hidden card
                deck.DisplayCards(dealerDisplay, false);

                Console.WriteLine("\nEnter 'H' to Hit or 'S' to Stand: ");
                string choice = Console.ReadLine().ToUpper();

                if (choice == "H")
                {
                    player.Hand.Add(deck.DealCard());
                    int playerScore = CalculateHandValue(player.Hand);
                    Console.WriteLine($"\nYou drew a {player.Hand[^1]}");
                    if (playerScore > 21)
                    {
                        playerBusted = true;
                        Console.WriteLine("\nBust! You lose!");
                        break;
                    }
                }
                else if (choice == "S")
                {
                    break;
        }
            }

            // Dealer's turn
            while (CalculateHandValue(dealer.Hand) < 17)
            {
                dealer.Hand.Add(deck.DealCard());
            }

            int playerScore = CalculateHandValue(player.Hand);
            int dealerScore = CalculateHandValue(dealer.Hand);

            Console.WriteLine("\nFinal hands:");
            Console.WriteLine("\nYour hand:");
            deck.DisplayCards(player.Hand, true);
            Console.WriteLine($"\nTotal score: {playerScore}");

            Console.WriteLine("\nDealer's hand:");
            deck.DisplayCards(dealer.Hand, true);
            Console.WriteLine($"\nTotal score: {dealerScore}");

            if (playerBusted)
            {
                Console.WriteLine("You busted! Dealer wins!");
            }
            else if (dealerScore > 21 || playerScore > dealerScore)
            {
                Console.WriteLine("\nYou win! Congratulations!");
            }
            else
            {
                Console.WriteLine("\nDealer wins!");
            }

            // Play again?
            Console.WriteLine("\nWould you like to play again? Enter 'Y' for Yes or 'N' for No: ");
            string playAgain = Console.ReadLine().ToUpper();
            if (playAgain != "Y")
            {
                break;
        }
    }
}

    private static int CalculateHandValue(List<Card> hand)
    {
        int value = 0;
        int numAces = 0;

        foreach (Card card in hand)
        {
            switch (card.Rank)
            {
                case Rank.Two: value += 2; break;
                case Rank.Three: value += 3; break;
                case Rank.Four: value += 4; break;
                case Rank.Five: value += 5; break;
                case Rank.Six: value += 6; break;
                case Rank.Seven: value += 7; break;
                case Rank.Eight: value += 8; break;
                case Rank.Nine: value += 9; break;
                case Rank.Ten:
                case Rank.Jack:
                case Rank.Queen:
                case Rank.King: value += 10; break;
                case Rank.Ace: numAces++; break;
            }
        }

        // Handle Aces (each Ace can be 11 or 1)
        for (int i = 0; i < numAces; i++)
        {
            if (value + 11 <= 21)
            {
                value += 11;
            }
            else
            {
                value += 1;
            }
        }

        return value;
    }

    private static void WelcomeMessage()
    {
        Console.WriteLine("Welcome to Blackjack!");
        Console.WriteLine("Try to get as close to 21 as possible without busting.");
        Console.WriteLine("Press Enter to begin...");
        Console.ReadLine();
    }
}
