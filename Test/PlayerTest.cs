using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game;
using Library;

namespace Test
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void DamagePlayer()
        {
            var player = new Player(0, "Player");
            Assert.AreEqual(Player.MaxHealth, player.Health);
            var value = 5;
            player.GetDamage(value);
            Assert.AreEqual(player.Health, Player.MaxHealth - value);
        }
        [TestMethod]
        public void HealPlayer()
        {
            var player = new Player(0, "Player");
            var damage = 10;
            var heal = 2;
            player.GetDamage(damage);
            player.Heal(heal);
            Assert.AreEqual(Player.MaxHealth - damage + heal, player.Health);
        }
        [TestMethod]
        public void HealPlayerAboveMax()
        {
            var player = new Player(0, "Player");
            player.GetDamage(1);
            player.Heal(2);
            Assert.AreEqual(Player.MaxHealth, player.Health);
        }
        [TestMethod]
        public void PlayerDie()
        {
            var player = new Player(0, "Player");
            player.Die += PlayerDie;
            player.GetDamage(25);
            player.GetDamage(15);
            Assert.IsTrue(isPlayerDied);
        }
        private bool isPlayerDied;
        private void PlayerDie(object sender, EventArgs e) => isPlayerDied = true;
        [TestMethod]
        public void PlayerDamaged()
        {
            var player = new Player(0, "Player");
            player.Damaged += PlayerDamaged;

            var i = 0;
            while (i < Deck.MaxSize - Hand.InitSize + 1)
            {
                player.NextTurn();
                i++;
            }
            Assert.IsTrue(isPlayerDamaged);
        }
        private bool isPlayerDamaged;
        private void PlayerDamaged(object sender, EventArgs e) => isPlayerDamaged = true;
        [TestMethod]
        public void GetMana()
        {
            var player = new Player(0, "Player");
            Assert.AreEqual(player.CurrentMana, 0);
            var value = 1;
            player.AddMana(value);
            Assert.AreEqual(value, player.CurrentMana);
        }
        [TestMethod]
        public void ManaOverMax()
        {
            var player = new Player(0, "Player");
            Assert.AreEqual(0, player.CurrentMana);
            var value = 11;
            player.AddMana(value);
            Assert.AreEqual(Player.MaxMana, player.CurrentMana);
        }
        [TestMethod]
        public void NextTurn()
        {
            var player = new Player(0, "Player");
            player.Damaged += PlayerDamaged;
            var overdraft = 10;
            for (var i = 0; i < Deck.MaxSize - Hand.InitSize + overdraft; i++)
            {
                var hand = player.NextTurn();
                Assert.AreEqual(i > 5 ? Hand.MaxSize : i + Hand.InitSize + 1, hand.Count);
            }
            Assert.AreEqual(Player.MaxHealth - overdraft, player.Health);
            Assert.AreEqual(Player.MaxMana, player.CurrentMana);
            Assert.AreEqual(Player.MaxMana, player.Mana);
        }
        [TestMethod]
        public void PlayCard()
        {
            var player = new Player(0, "Player");
            var mana = 6;
            for (var i = 0; i < mana; i++)
            {
                var hand = player.NextTurn();
                if (i + 1 == mana)
                {
                    var cost = hand.GetCard(0).Cost;
                    _ = player.PlayCard(0);
                    Assert.AreEqual(mana - cost, player.CurrentMana);
                }
            }
        }
    }
}