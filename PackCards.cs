using System;
using System.Collections.Generic;
using System.Linq;

namespace PackCards
{
    class Program
    {
        static void Main(string[] args)
        {
            Croupier croupier = new Croupier();

            croupier.GiveCardToPlayer();
            croupier.ShowInfo();
        }
    }

    class Croupier
    {
        private Deck _deck = new Deck();
        private Player _player = new Player();
        private Random _random = new Random();

        public void GiveCardToPlayer()
        {
            Console.WriteLine($"В колоде {_deck.GetCardsCount()} карты.");

            if(int.TryParse(GetUserInput("Количество карт для игрока"), out int count))
            {
                if (count <= _deck.GetCardsCount() && count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        int index = _random.Next(_deck.GetCardsCount());

                        _player.GetCard(_deck.GetCartByIndex(index));
                        _deck.RemoveCard(index);
                    }
                }
                else
                {
                    Console.WriteLine("Недопустимое количество карт.");
                }
            }
            else
            {
                Console.WriteLine("Некорректное значение.");
            }
        }

        public void ShowInfo()
        {
            Console.Write($"В колоде осталось {_deck.GetCardsCount()} карт.\n" +
                $"У игрока имеется {_player.GetCardsCount()} карт номиналом: ");

            _player.ShowCards();
        }

        private string GetUserInput(string message)
        {
            Console.Write(message + ": ");

            return Console.ReadLine();
        }
    }

    class Player
    {
        private List<Card> _cards = new List<Card>();

        public void GetCard(Card card)
        {
            _cards.Add(card);
        }

        public int GetCardsCount()
        {
            return _cards.Count;
        }

        public void ShowCards()
        {
            foreach ( Card card in _cards)
            {
                Console.Write(card.Nominal + " ");
            }
        }
    }

    class Deck
    {
        private List<Card> _cards = new List<Card>();
        private int _maximumCards = 52;

        public Deck()
        {
            for (int i = 0; i < _maximumCards; i++)
            {
                _cards.Add(new Card(i + 1));
            }
        }

        public int GetCardsCount()
        {
            return _cards.Count;
        }

        public Card GetCartByIndex(int index)
        {
            return _cards.ElementAt(index);
        }

        public void RemoveCard(int index)
        {
            _cards.RemoveAt(index);
        }
    }

    class Card 
    { 
        public Card(int nominal)
        {
            Nominal = nominal;
        }

        public int Nominal { get; private set; }
    }
}