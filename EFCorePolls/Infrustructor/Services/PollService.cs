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
        private readonly IOptionRepository _optionRepo;
        private readonly IVoteRepository _voteRepo;
        private readonly IUserRepository _userRepo;

        public PollService(IPollRepository pollRepo, IQuestionRepository questionRepo, IOptionRepository optionRepo, IVoteRepository voteRepo, IUserRepository userRepo)
        {
            _pollRepo = pollRepo;
            _questionRepo = questionRepo;
            _optionRepo = optionRepo;
            _voteRepo = voteRepo;
            _userRepo = userRepo;
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

            var question = new Question
            {
                Text = questionText,
                Options = options
            };

            var poll = new Poll
            {
                Title = title,
                Questions = new List<Question> { question }
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
            var hasVotes = _voteRepo.UserHasVoted(pollId);
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
        public ResultDto Vote(int pollId, int questionId, int selectedOptionNumber, int userId)
        {
            // Validate user and poll existence
            if (!_userRepo.CheckUserId(userId))
                return new ResultDto { IsSuccess = false, Message = "Invalid user." };

            if (!_pollRepo.PollExists(pollId))
                return new ResultDto { IsSuccess = false, Message = "Poll does not exist." };

            // Check if already voted
            var user = _userRepo.GetUserInfoById(userId);
            if (_voteRepo.UserHasVoted(pollId, user.UserName))
                return new ResultDto { IsSuccess = false, Message = "You have already voted in this poll." };

            // Map 1–4 to actual option
            var question = _questionRepo.GetQuestionById(questionId);
            if (question == null || question.Options.Count < selectedOptionNumber || selectedOptionNumber < 1)
                return new ResultDto { IsSuccess = false, Message = "Invalid option number." };

            var selectedOption = question.Options[selectedOptionNumber - 1];

            // Create and save vote
            var vote = new Vote
            {
                PollId = pollId,
                OptionId = selectedOption.Id,
                QuestionId = questionId,
                UserId = userId,
                UserName = user.UserName,
                VotedAt = DateTime.Now
            };

            _voteRepo.AddVote(vote);
            return new ResultDto { IsSuccess = true, Message = "Vote recorded successfully." };
        }



    }
}
