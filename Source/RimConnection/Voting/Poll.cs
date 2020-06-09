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
        public Poll(List<CustomVoteOption> customVoteOptions, int expireInSeconds = 120)
        {
            voteId = ShortGuid.NewShortGuid().Value;
            this.expireInSeconds = expireInSeconds;
            this.customVoteOptions = customVoteOptions;
        }

        public string voteId { get; set; }

        public int expireInSeconds { get; set; }

        public List<CustomVoteOption> customVoteOptions { get; set; }

        public bool finished = false;

        public void Execute(string winner)
        {
            CustomVoteOption voteOption = customVoteOptions.Find((inc) => inc.id == winner);

            if (voteOption == null)
            {
                Log.Error($"Tried to set winner of Poll {voteId} but could not find id of winner");
                return;
            }

            Log.Message($"Fire incident {voteOption.firingIncident.def.LabelCap}");

            this.finished = true;
        }
    }

    public class CustomVoteOption
    {
        public CustomVoteOption(string description, FiringIncident firingIncident)
        {
            id = ShortGuid.NewShortGuid().Value;
            this.description = description;
            this.firingIncident = firingIncident;
        }

        public string id { get; set; }

        public string description { get; set; }

        public FiringIncident firingIncident;
    }
}
