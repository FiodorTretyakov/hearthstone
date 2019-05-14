using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library;

namespace Test
{
    [TestClass]
    public class DeckTest
    {
        [TestMethod]
        public void DeckDeserialize()
        {
            var rawCards = Deck.Deserialize();
            Assert.AreEqual(9, rawCards.Count);
            Assert.AreEqual(1, rawCards.Count(c => !string.IsNullOrEmpty(c.Message)));
            Assert.IsTrue(rawCards.All(c => !string.IsNullOrEmpty(c.Description)));
            Assert.AreEqual(2, rawCards.Count(c => c.Actions.Count > 1));
            Assert.AreEqual(1, rawCards.Count(c => c.Type == CardType.Legendary));
            Assert.AreEqual(Deck.MaxSize, rawCards.AsParallel().Aggregate(0, (s, i) => i.Number + s));
        }
        [TestMethod]
        public void DeckCreate() => Assert.AreEqual(Deck.MaxSize, Deck.Create(Deck.Deserialize()).Count);
        [TestMethod]
        public void DeckShuffle()
        {
            var deck = new Deck();
            Assert.AreEqual(deck.Count(), Deck.MaxSize);
            Assert.IsNotNull(deck.Draw());

            var shuffled = new List<Card>();
            while (deck.Count() > 0)
            {
                shuffled.Add(deck.Draw());
            }

            Assert.IsTrue(shuffled.All(c => c.Count == 1));
            var cards = Deck.Create(Deck.Deserialize());
            Assert.AreNotEqual(cards.Select(c => c.Name), shuffled.Select(c => c.Name));
        }
        [TestMethod]
        public void OverDeckSize()
        {
            var cards = Deck.Create(Deck.Deserialize());
            cards.Add(new Card());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Deck.Shuffle(cards));
        }
        [TestMethod]
        public void CheckCardCount()
        {
            var cards = Deck.Deserialize();
            var card = cards.First();
            Assert.AreEqual(1, card.Count);
            card.Count++;
            Assert.AreEqual(2, card.Count);
        }
        [TestMethod]
        public void CreateWithCards() => Assert.AreEqual(1, new Deck(new List<Card> { { new Card() } }).Count());
    }
}