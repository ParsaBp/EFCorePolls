using EFCorePolls.Contract.IRepozitory;
using EFCorePolls.DTO;
using EFCorePolls.Entity;
using EFCorePolls.Infrustructor.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePolls.Infrustructor.Services
{
    public class PollService
    {
        private readonly IPollRepository _pollRepo;
        private readonly IQuestionRepository _questionRepo;
        private readonly IVoteRepository _voteRepo;

        public PollService()
        {
            _pollRepo = new PollRepository();
            _questionRepo = new QuestionRepository();
            _voteRepo = new VoteRepository();
        }

        public ResultDto CreatePoll(string title, string questionText, string option1, string option2, string option3, string option4)
        {
            if (string.IsNullOrWhiteSpace(title))
                return new ResultDto { IsSuccess = false, Message = "Title is required." };

            if (string.IsNullOrWhiteSpace(questionText))
                return new ResultDto { IsSuccess = false, Message = "Question is required." };

            var options = new List<Option>
            {
                new Option { Text = option1 },
                new Option { Text = option2 },
                new Option { Text = option3 },
                new Option { Text = option4 }
            };

            var question = new Entity.Question
            {
                Text = questionText,
                Options = options
            };

            var poll = new Poll
            {
                Title = title,
                Questions = new List<Entity.Question> { question }
            };

            try
            {
                _pollRepo.CreatePoll(poll);
                return new ResultDto { IsSuccess = true, Message = "Poll created successfully." };
            }
            catch (Exception ex)
            {
                return new ResultDto { IsSuccess = false, Message = $"Error creating poll: {ex.Message}" };
            }
        }



        public ResultDto DeletePoll(int pollId)
        {
            var hasVotes = _voteRepo.AnyUserHasVoted(pollId);
            if (!hasVotes)
                return new ResultDto { IsSuccess = false, Message = "Can not delete the poll because nobody has voted." };

            return _pollRepo.Delete(pollId);
        }

        public List<PollResultDto> ShowPollResult(int pollId)
        {
            var pollResults = _pollRepo.ShowPollResult(pollId);
            int totalVotes = pollResults.Sum(p => p.VoteCount);

            if (totalVotes == 0)
                return pollResults;

            foreach (var result in pollResults)
                result.Percentage = (result.VoteCount * 100.0) / totalVotes;

            return pollResults;
        }
        // ---------------- USER SECTION ----------------

        // 1. List available polls
        public List<ShowQuestionsDto> ShowPolls()
        {
            return _pollRepo.ShowPolls();
        }

        // 2. Vote in a poll (user chooses an option 1–4)
        public ResultDto Vote(int pollId, int questionId, int selectedOptionNumber, int userId, string username)
        {


            if (!_pollRepo.PollExists(pollId))
                return new ResultDto { IsSuccess = false, Message = "Poll does not exist." };

            // Check if already voted
            if (_voteRepo.UserHasVoted(pollId, userId))
                return new ResultDto { IsSuccess = false, Message = "You have already voted in this poll." };

            // Map 1–4 to actual option
            var options = _questionRepo.GetOptionsFromQuestion(questionId);
            if (options == null || options.Count < selectedOptionNumber || selectedOptionNumber < 1)
                return new ResultDto { IsSuccess = false, Message = "Invalid option number." };

            var selectedOption = options[selectedOptionNumber - 1];

            // Create and save vote
            var vote = new Vote
            {
                PollId = pollId,
                OptionId = selectedOption.Id,
                QuestionId = questionId,
                UserId = userId,
                UserName = username,
                VotedAt = DateTime.Now
            };

            _voteRepo.AddVote(vote);
            return new ResultDto { IsSuccess = true, Message = "Vote recorded successfully." };
        }



    }
}
