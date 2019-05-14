using System;
using System.Collections.Generic;
using System.Linq;
using Library;

namespace Game
{
    public class Player
    {
        public const int MaxMana = 10;
        public const int MaxHealth = 30;
        public const int DrawEmptyDamage = 1;
        private int health = MaxHealth;
        public int Health
        {
            get
            {
                return health;
            }
            private set
            {
                if (value <= 0)
                {
                    if (Die != null)
                    {
                        Die(this, null);
                    }
                }
                if (value > MaxHealth)
                {
                    value = MaxHealth;
                }
                health = value;
            }
        }
        private Deck deck;
        private Hand hand;
        public int Mana { get; private set; } = 0;
        private int currentMana = 0;
        public int CurrentMana
        {
            get
            {
                return currentMana;
            }
            private set
            {
                if (value < 0)
                {
                    value = 0;
                }
                if (value > MaxMana)
                {
                    value = MaxMana;
                }
                currentMana = value;
            }
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public event EventHandler Die;
        public event EventHandler Damaged;
        public Player(int id, string name)
        {
            Id = id;
            Name = name;
            deck = new Deck();
            hand = new Hand(deck);
        }

        public Hand NextTurn()
        {
            if (deck.Count() > 0)
            {
                hand.Draw(1);
            }
            else
            {
                Damaged(this, new DamagedEventArgs(DrawEmptyDamage));
                Health -= DrawEmptyDamage;
            }
            if (Mana < MaxMana)
            {
                Mana++;
            }
            CurrentMana = Mana;

            return hand;
        }
        public Hand PlayCard(int index)
        {
            CurrentMana -= hand.PlayCard(index);
            return hand;
        }
        public void GetDamage(int n) => Health -= n;
        public void Heal(int n) => Health += n;
        public void AddMana(int n) => CurrentMana += n;
        public void Draw(int value) => hand.Draw(value);
    }
}