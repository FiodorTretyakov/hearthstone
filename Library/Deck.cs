using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Library
{
    public class Deck
    {
        public const int MaxSize = 30;
        private readonly Stack<Card> deck;
        public Deck() => deck = new Stack<Card>(Shuffle(Create(Deserialize())));
        public Deck(List<Card> cards) => deck = new Stack<Card>(Shuffle(Create(cards)));
        public int Count() => deck.Count;
        public Card Draw() => deck.Pop();
        public static IList<Card> Deserialize() => JsonConvert.DeserializeObject<IList<Card>>(File.ReadAllText("deck.json"));
        public static IList<Card> Create(IList<Card> cards)
        {
            var cardsToAdd = new List<Card>();
            foreach (var card in cards)
            {
                if (card.Number > 1)
                {
                    for (var i = 1; i < card.Number; i++)
                    {
                        cardsToAdd.Add(new Card(card.Id, card.Name, card.Cost, card.Actions, card.Type, card.Message, card.Description));
                    }
                }
            }
            return cards.Concat(cardsToAdd).ToList();
        }
        public static IList<Card> Shuffle(IList<Card> cards)
        {
            var shuffled = new List<Card>();

            var random = new Random();
            while (cards.Count > 0)
            {
                var i = random.Next(cards.Count);
                shuffled.Add(cards[i]);
                cards.RemoveAt(i);
            }

            if (shuffled.Count() > MaxSize)
            {
                throw new ArgumentOutOfRangeException($"Deck can't be more {MaxSize} cards");
            }

            return shuffled;
        }
    }
}