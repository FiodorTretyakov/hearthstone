using System.Collections.Generic;
using Newtonsoft.Json;

namespace Library
{
    public enum CardType
    {
        Default = 0,
        Legendary = 1
    };
    public class Card
    {
        private int count = 1;
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
            }
        }
        public Card()
        {
        }
        public Card(int id, string name, int cost, List<Action> actions, CardType type, string message, string description)
        {
            Id = id;
            Name = name;
            Cost = cost;
            Actions = actions;
            Type = type;
            Message = message;
            Description = description;
        }
        [JsonProperty("id", Required = Required.Always)]
        public int Id { get; private set; }
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; private set; }

        [JsonProperty("cost", Required = Required.Always)]
        public int Cost { get; private set; }

        [JsonProperty("actions", Required = Required.Always)]
        public List<Action> Actions { get; private set; }

        [JsonProperty("type")]
        public CardType Type { get; private set; }

        [JsonProperty("message")]
        public string Message { get; private set; }

        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; private set; }

        [JsonProperty("number", Required = Required.Always)]
        public int Number { get; private set; }
    }
}