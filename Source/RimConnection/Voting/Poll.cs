using Newtonsoft.Json;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimConnection.Voting
{
    public class Poll
    {
        public Poll(List<VoteOption> customVoteOptions, int expireInSeconds = 120)
        {
            voteId = Guid.NewGuid().ToString();
            this.expireInSeconds = expireInSeconds;
            this.voteOptions = customVoteOptions;
        }

        public string voteId { get; set; }

        public int expireInSeconds { get; set; }

        public List<VoteOption> voteOptions { get; set; }

        public bool finished = false;

        public void Execute(string winner)
        {
            VoteOption voteOption = voteOptions.Find((inc) => inc.validCommand == winner);

            if (voteOption == null)
            {
                Log.Error($"Tried to set winner of Poll {voteId} but could not find id of winner");
                return;
            }

            Log.Message($"Fire incident {voteOption.firingIncident.def.LabelCap}");

            this.finished = true;
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class VoteOption
    {
        public VoteOption(string title, FiringIncident firingIncident)
        {
            validCommand = Guid.NewGuid().ToString();
            amount = 10;
            this.title = title;
            this.description = title;
            this.firingIncident = firingIncident;
        }

        [JsonProperty]
        public string validCommand { get; set; }

        [JsonProperty]
        public int amount { get; set; }

        [JsonProperty]
        public string title { get; set; }

        [JsonProperty]
        public string description { get; set; }

        public FiringIncident firingIncident;
    }
}
