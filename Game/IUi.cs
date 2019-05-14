using System.Collections.Generic;
using Library;

namespace Game
{
    public enum PlayerAction
    {
        Undefined = -1,
        EndTurn = 100,
        EndGame = 101
    };
    public interface IUi
    {
        int SelectAction(string player, Dictionary<string, int> cards, int mana, int health, int opHealth);
        void ShowNoMana(string name);
        void ShowMessage(string message);
        void EndGame(string winner);
        void IncorrectChoice(int index);
        void CardPlayed(string player, string description);
        void PlayerDamaged(string player, int value);
        void PlayerHealed(string player, int value);
        void PlayerDrawn(string player, int value);
        void GotMana(string player, int value);
        void StartGame(string player);
        bool IsTestActions();
        void ShowEndTurn();
    }
}