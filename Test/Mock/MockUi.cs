using System.Collections.Generic;
using Game;

namespace Test.Mock
{
    public enum Code
    {
        Message = 102,
        NoMana = 103,
        Damage = 104,
        Heal = 105,
        Draw = 106,
        ManaAdd = 107
    }
    public class MockUi : IUi
    {
        public MockUi(bool isRealActions) => IsRealActions = isRealActions;
        public MockUi(bool isRealActions, int code)
        {
            IsRealActions = isRealActions;
            SelectedAction = code;
        }
        private int testCode = 0;
        public int TestCode
        {
            get
            {
                return testCode;
            }
            private set
            {
                if (testCode == 0)
                {
                    testCode = value;
                    SelectedAction = (int)PlayerAction.EndGame;
                }
            }
        }
        public int SelectedAction { get; private set; } = (int)PlayerAction.EndGame;
        public bool IsRealActions { get; private set; }
        public void CardPlayed(string player, string description)
        {
        }
        public void EndGame(string winner) => TestCode = (int)PlayerAction.EndGame;
        public void GotMana(string player, int value) => TestCode = (int)Code.ManaAdd;
        public void IncorrectChoice(int index) => TestCode = (int)PlayerAction.Undefined;
        public void PlayerDamaged(string player, int value) => TestCode = (int)Code.Damage;
        public void PlayerDrawn(string player, int value) => TestCode = (int)Code.Draw;
        public void PlayerHealed(string player, int value) => TestCode = (int)Code.Heal;
        public int SelectAction(string player, Dictionary<string, int> cards, int mana, int health, int opHealth) => SelectedAction;

        public void ShowMessage(string message) => TestCode = (int)Code.Message;

        public void ShowNoMana(string name) => TestCode = (int)Code.NoMana;
        public void StartGame(string player)
        {
        }
        public bool IsTestActions() => !IsRealActions;
        public void ShowEndTurn()
        {
            if (!IsRealActions)
            {
                TestCode = (int)PlayerAction.EndTurn;
            }
        }
    }
}