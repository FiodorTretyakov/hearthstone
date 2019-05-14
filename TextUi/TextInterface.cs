using System;
using System.Collections.Generic;
using Game;

namespace TextUi
{
    public class TextInterface : IUi
    {
        public int SelectAction(string player, Dictionary<string, int> cards, int mana, int health, int opHealth)
        {
            Console.WriteLine($"{player} turn!");
            Console.WriteLine($"You have {mana} mana. Your health is {health}. Your opponent health is {opHealth}.");
            Console.WriteLine("Please, select one the following actions:");

            var i = 0;
            foreach (var card in cards)
            {
                Console.WriteLine($"{i++ + 1}: {card.Key}. Count: {card.Value}.");
            }

            if (cards.Count == 0)
            {
                Console.WriteLine($"You have no more cards in hand.");
            }

            Console.WriteLine($"{(int)PlayerAction.EndTurn}: End Turn.");
            Console.WriteLine($"{(int)PlayerAction.EndGame}: Surrender.");

            int value;
            if (int.TryParse(Console.ReadLine(), out value))
            {
                return value;
            }
            else
            {
                return (int)PlayerAction.Undefined;
            }
        }
        public void ShowNoMana(string description) => Console.WriteLine($"You have not enough mana to play card that {description}.");
        public void ShowMessage(string message) => Console.WriteLine(message);
        public void EndGame(string winner) => Console.WriteLine($"Congratulations! {winner} won!");
        public void IncorrectChoice(int index) => Console.WriteLine($"You did incorrect choice. Please, try again.");
        public void CardPlayed(string player, string description) => Console.WriteLine($"{player} played a card that {description}.");
        public void PlayerDamaged(string player, int value) => Console.WriteLine($"{player} got {value} point damage.");
        public void PlayerHealed(string player, int value) => Console.WriteLine($"{player} healed by {value} points.");
        public void PlayerDrawn(string player, int value) => Console.WriteLine($"{player} drawn {value} cards.");
        public void GotMana(string player, int value) => Console.WriteLine($"{player} got {value} mana crystals.");
        public void StartGame(string player)
        {
            Console.WriteLine($"Welcome to hearthstone!");
            Console.WriteLine($"{player} randomly selected to start the first.");
        }
        public bool IsTestActions() => false;
        public void ShowEndTurn()
        {  
        }
    }
}