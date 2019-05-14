using System;
using System.Collections.Generic;
using System.Linq;
using Library;

namespace Game
{
    public class Battle
    {
        public const int PlayersNumber = 2;
        private IUi ui;
        private IEnvironment gameEnvironment;
        private IList<Player> players = new List<Player>();
        public int CurrentPlayerIndex { get; private set; }
        public int OpponentIndex
        {
            get
            {
                return (CurrentPlayerIndex + 1) % PlayersNumber;
            }
        }
        public Battle(IUi u, IEnvironment env)
        {
            ui = u;
            gameEnvironment = env;

            var i = 0;
            while (i < PlayersNumber)
            {
                var player = new Player(i, $"Player{i + 1}");
                player.Die += PlayerDie;
                player.Damaged += PlayerDamaged;
                players.Add(player);
                i++;
            }

            CurrentPlayerIndex = new Random().Next(PlayersNumber);
            ui.StartGame(players[CurrentPlayerIndex].Name);

            if (!ui.IsTestActions())
            {
                PlayTurn(players[CurrentPlayerIndex].NextTurn());
            }
        }
        private Player SwitchPlayer()
        {
            CurrentPlayerIndex = OpponentIndex;
            return players[CurrentPlayerIndex];
        }
        private void PlayTurn(Hand hand) => DoAction(ui.SelectAction(players[CurrentPlayerIndex].Name, hand.GetCardsDetails(), players[CurrentPlayerIndex].CurrentMana, players[CurrentPlayerIndex].Health, players[OpponentIndex].Health), hand);
        private void PlayerDamaged(object sender, EventArgs e) => ui.PlayerDamaged(((Player)sender).Name, ((DamagedEventArgs)e).Value);
        private void PlayerDie(object sender, EventArgs e)
        {
            ui.EndGame(players.First(p => p.Id != ((Player)sender).Id).Name);
            gameEnvironment.Finish();
        }
        public void DoAction(int index, Hand hand)
        {
            switch (index)
            {
                case (int)PlayerAction.EndTurn:
                    {
                        ui.ShowEndTurn();
                        PlayTurn(SwitchPlayer().NextTurn());
                        return;
                    }
                case (int)PlayerAction.EndGame:
                    {
                        PlayerDie(players[CurrentPlayerIndex], null);
                        return;
                    }
            }

            if (index >= 1 && index <= hand.Size)
            {
                var adjustedIndex = index - 1;
                var card = hand.GetCard(adjustedIndex);
                if (card.Cost > players[CurrentPlayerIndex].CurrentMana)
                {
                    ui.ShowNoMana(card.Description);
                    PlayTurn(hand);
                }
                else
                {
                    hand = players[CurrentPlayerIndex].PlayCard(adjustedIndex);
                    ui.CardPlayed(players[CurrentPlayerIndex].Name, card.Description);

                    if (!string.IsNullOrWhiteSpace(card.Message))
                    {
                        ui.ShowMessage(card.Message);
                    }
                    foreach (var action in card.Actions)
                    {
                        switch (action.Type)
                        {
                            case ActionType.Damage:
                                {
                                    players[OpponentIndex].GetDamage(action.Value);
                                    ui.PlayerDamaged(players[OpponentIndex].Name, action.Value);
                                    break;
                                }
                            case ActionType.Heal:
                                {
                                    players[CurrentPlayerIndex].Heal(action.Value);
                                    ui.PlayerHealed(players[CurrentPlayerIndex].Name, action.Value);
                                    break;
                                }
                            case ActionType.Draw:
                                {
                                    players[CurrentPlayerIndex].Draw(action.Value);
                                    ui.PlayerDrawn(players[CurrentPlayerIndex].Name, action.Value);
                                    break;
                                }
                            case ActionType.ManaAdd:
                                {
                                    players[CurrentPlayerIndex].AddMana(action.Value);
                                    ui.GotMana(players[CurrentPlayerIndex].Name, action.Value);
                                    break;
                                }
                        }
                    }
                    PlayTurn(hand);
                }
            }
            else
            {
                ui.IncorrectChoice(index);
                PlayTurn(hand);
            }
        }
    }
}