using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library;

namespace Test
{
    [TestClass]
    public class HandTest
    {
        [TestMethod]
        public void HandInitSize() => Assert.AreEqual(Hand.InitSize, new Hand(new Deck()).Count);
        [TestMethod]
        public void GetCard()
        {
            var card = new Hand(new Deck()).GetCard(0);
            Assert.IsNotNull(card);
            Assert.IsFalse(string.IsNullOrWhiteSpace(card.Description));
            Assert.IsNotNull(card.Id);
            Assert.IsFalse(string.IsNullOrWhiteSpace(card.Name));
            Assert.IsNotNull(card.Actions);
            Assert.IsTrue(card.Actions.Count > 0);
        }
        [TestMethod]
        public void GetDetails() => Assert.IsTrue(new Hand(new Deck()).GetCardsDetails().All(d => !string.IsNullOrWhiteSpace(d.Key) && d.Value > 0));
        [TestMethod]
        public void HandMaxSize()
        {
            var deck = new Deck();
            var hand = new Hand(deck);
            hand.Draw(deck.Count());
            Assert.AreEqual(Hand.MaxSize, hand.Count);
        }
        [TestMethod]
        public void GetCost()
        {
            var hand = new Hand(new Deck());
            Assert.AreEqual(hand.GetCard(0).Cost, hand.PlayCard(0));
        }
        [TestMethod]
        public void DrawCard()
        {
            var deck = new Deck();
            var hand = new Hand(deck);
            hand.Draw(deck.Count());

            Assert.AreEqual(0, deck.Count());

            var cardsCount = 0;
            var i = 0;
            while (cardsCount < Hand.MaxSize)
            {
                cardsCount += hand.GetCard(i).Count;
                i++;
            }
            Assert.AreEqual(Hand.MaxSize, cardsCount);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => hand.GetCard(i));

        }
        [TestMethod]
        public void GetSize()
        {
            var hand = new Hand(new Deck());
            
            var count = 0;
            for (var i = 0; i < hand.Size; i++)
            {
                count += hand.GetCard(i).Count;
            }

            Assert.AreEqual(count, hand.Count);
        }
        [TestMethod]
        public void PlayCard()
        {
            var deck = new Deck();
            var hand = new Hand(deck);
            hand.Draw(deck.Count());

            var index = Enumerable.Range(0, Hand.MaxSize - 1).First(i => hand.GetCard(i).Count > 1);
            var card = hand.GetCard(index);
            var count = card.Count;

            while (card.Count > 0)
            {
                hand.PlayCard(index);
            }

            Assert.AreEqual(0, card.Count);
            Assert.AreNotEqual(card.Id, hand.GetCard(index).Id);
            Assert.AreEqual(Hand.MaxSize - count, hand.Count);
        }
    }
}