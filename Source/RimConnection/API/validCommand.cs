using System;
using System.Collections.Generic;

#pragma warning disable IDE1006 // Naming Styles
namespace RimConnection
{
    public class ValidCommand
    {
        public string actionHash { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string prefix { get; set; }
        public bool shouldShowAmount { get; set; }
        public int localCooldownMs { get; set; }
        public int globalCooldownMs { get; set; }
        public int costSilverStore { get; set; }
        public string bitStoreSKU { get; set; }

        public CommandOption toCommandOption()
        {
            return new CommandOption()
            {
                actionHash = actionHash,
                localCooldownMs = localCooldownMs,
                globalCooldownMs = globalCooldownMs,
                costSilverStore = costSilverStore
            };
        }
    }

    public class ValidCommandPayloadGenerator
    {
        private int chunkSize = 1000;
        private int totalPackets = 0;
        private int packetNumber = 0;
        public ValidCommandPayloadGenerator(List<ValidCommand> commands)
        {
            validCommands = commands;

            var list = new List<List<ValidCommand>>();
            for (int i = 0; i < commands.Count; i += chunkSize)
            {
                totalPackets += 1;
                list.Add(commands.GetRange(i, Math.Min(chunkSize, commands.Count - i)));
            }

            chunkedValidCommands = list;
        }
        public List<List<ValidCommand>> chunkedValidCommands { get; set; }
        public List<ValidCommand> validCommands { get; set; }

        public bool isThereAnotherChunk()
        {
            return packetNumber < totalPackets;
        }

        public ValidCommandListPostPayload getNextChunk()
        {
            List<ValidCommand> commands = chunkedValidCommands[packetNumber];

            var payload = new ValidCommandListPostPayload
            {
                validCommands = commands,
                totalPackets = this.totalPackets,
                packetNumber = packetNumber
            };

            packetNumber += 1;
            return payload;
        }

    }

    public class ValidCommandListPostPayload
    {
        public List<ValidCommand> validCommands { get; set; }
        public int totalPackets { get; set; }
        public int packetNumber { get; set; }
    }

    public class ValidCommandGetResponse
    {
        public List<CommandOption> commands { get; set; }
    }
}