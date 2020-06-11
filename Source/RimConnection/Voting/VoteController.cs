using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimConnection.Voting
{
    public class VoteController : GameComponent
    {
        public VoteController(Game game)
        {

        }

        public void RegisterNewPoll(Poll poll)
        {
            if (Polls.Contains(poll) || (Polls.Count != 0 && allowOnlyOnePoll))
            {
                return;
            }

            Polls.Add(poll);

            RimConnectAPI.PostPoll(poll);

            Log.Message($"Registered Poll with Id: {poll.voteId}");
        }

        public void ExecutePollWinner(string voteId, string optionId)
        {
            // For Testing Purposes lets just finish any poll

            if (finishAllPolls)
            {
                foreach (Poll p in Polls)
                {
                    p.Execute(p.voteOptions.RandomElement().identifier);
                }

                return;
            }

            Poll poll = Polls.Find((p) => p.voteId == voteId);

            if (poll != null)
            {
                VoteOption option = poll.voteOptions.Find((o) => o.identifier == optionId);

                if (option != null)
                {
                    option.firingIncident.def.Worker.TryExecute(option.firingIncident.parms);
                }
            }
        }

        List<Poll> Polls { get; set; } = new List<Poll>();

        // Testing options
        bool allowOnlyOnePoll = true;
        bool finishAllPolls = true;
    }
}
