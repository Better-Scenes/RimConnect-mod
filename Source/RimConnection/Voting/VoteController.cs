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
            if (!Polls.Contains(poll))
            {
                Polls.Add(poll);

                Log.Message($"Registered Poll with Id: {poll.voteId}");
            }
        }

        public void ExecutePollWinner(string voteId)
        {
            // For Testing Purposes lets just finish any poll

            foreach (Poll poll in Polls)
            {
                poll.Execute(poll.customVoteOptions.RandomElement().id);
            }
        }

        List<Poll> Polls { get; set; } = new List<Poll>();
    }
}
