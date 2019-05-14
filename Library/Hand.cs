using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public sealed class Hand
    {
        public const int InitSize = 3;
        public const int MaxSize = 10;
        private Deck deck;
        private List<Card> cards = new List<Card>();
        public int Size
        {
            get
            {
                return cards.Count();
            }
        }
        public int Count { get; private set; } = 0;
        public Hand(Deck d)
        {
            deck = d;
            Draw(InitSize);
        }
        public Card GetCard(int i)
        {
            if (i >= cards.Count)
            {
                throw new ArgumentOutOfRangeException($"Player has only {cards.Count} kinds of card.");
            }
            return cards[i];
        }
        public Dictionary<string, int> GetCardsDetails() => cards.ToDictionary(c => c.Description, c => c.Count);
        public int PlayCard(int i)
        {
            var cost = cards[i].Cost;
            cards[i].Count--;
            if (cards[i].Count == 0)
            {
                cards.RemoveAt(i);
            }
            Count--;

            return cost;
        }
        public void Draw(int n)
        {
            var i = 0;
            while (i < n)
            {
                var card = deck.Draw();
                card.Count = 1;
                if (Count < MaxSize)
                {
                    var updated = false;
                    for (var j = 0; j < cards.Count; j++)
                    {
                        if (cards[j].Id == card.Id)
                        {
                            cards[j].Count++;
                            updated = true;
                        }
                    }
                    if (!updated)
                    {
                        cards.Add(card);
                    }
                    Count++;
                }
                i++;
            }
        }
    }
}