using Newtonsoft.Json;

namespace Library
{
    public enum ActionType
    {
        Damage = 0,
        Heal = 1,
        Draw = 2,
        ManaAdd = 3
    };

    public class Action
    {
        public Action(int value, ActionType type)
        {
            Value = value;
            Type = type;
        }
        [JsonProperty("value", Required = Required.Always)]
        public int Value { get; private set; }

        [JsonProperty("type", Required = Required.Always)]
        public ActionType Type { get; private set; }
    }
}