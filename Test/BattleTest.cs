using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game;
using Library;
using Test.Mock;

namespace Test
{
    [TestClass]
    public class BattleTest
    {
        [TestMethod]
        public void InitBattle()
        {
            var battle = new Battle(new MockUi(true), new MockEnvironment());
            Assert.IsTrue(battle.CurrentPlayerIndex < Battle.PlayersNumber);
        }
        [TestMethod]
        public void DrawDamage()
        {
            var ui = new MockUi(true, (int)PlayerAction.EndTurn);
            var battle = new Battle(ui, new MockEnvironment());
            Assert.AreEqual((int)Code.Damage, ui.TestCode);
        }
        [TestMethod]
        public void Opponent()
        {
            var battle = new Battle(new MockUi(true), new MockEnvironment());
            Assert.AreNotSame(battle.OpponentIndex, battle.CurrentPlayerIndex);
        }

        [DataTestMethod]
        [DataRow(PlayerAction.Undefined)]
        [DataRow(PlayerAction.EndTurn)]
        [DataRow(PlayerAction.EndGame)]
        public void CheckAction(PlayerAction action)
        {
            var ui = new MockUi(false);
            var battle = new Battle(ui, new MockEnvironment());
            var code = (int)action;
            battle.DoAction(code, new Hand(new Deck()));
            Assert.AreEqual(code, ui.TestCode);
        }
        [TestMethod]
        public void ShowMessage()
        {
            var ui = new MockUi(false);
            var battle = new Battle(ui, new MockEnvironment());

            var code = (int)Code.Message;
            battle.DoAction(1, new Hand(new Deck(
                Enumerable.Repeat(new Card(1, "name", 0, new List<Action>(), CardType.Legendary, "Message", "Description"), Hand.InitSize).ToList())));
            Assert.AreEqual(code, ui.TestCode);
        }
        [TestMethod]
        public void ShowNoMana()
        {
            var ui = new MockUi(false);
            var battle = new Battle(ui, new MockEnvironment());

            var code = (int)Code.NoMana;
            battle.DoAction(1, new Hand(new Deck(
                Enumerable.Repeat(new Card(1, "name", 100, new List<Action>(), CardType.Default, string.Empty, "Description"), Hand.InitSize).ToList())));
            Assert.AreEqual(code, ui.TestCode);
        }

        [DataTestMethod]
        [DataRow(ActionType.Damage, Code.Damage)]
        [DataRow(ActionType.Heal, Code.Heal)]
        [DataRow(ActionType.ManaAdd, Code.ManaAdd)]
        [DataRow(ActionType.Draw, Code.Draw)]
        public void BattleAction(ActionType type, Code testCode)
        {
            var ui = new MockUi(false);
            var battle = new Battle(ui, new MockEnvironment());

            var code = (int)testCode;
            battle.DoAction(1, new Hand(new Deck(
                Enumerable.Repeat(new Card(1, "name", 0, new List<Action> { { new Action(1, type) } }, CardType.Default, string.Empty, "Description"), Hand.InitSize).ToList())));
            Assert.AreEqual(code, ui.TestCode);
        }
    }
}