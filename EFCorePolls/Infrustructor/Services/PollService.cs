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
        private readonly IOptionRepository _optionRepo;

        public PollService()
        {
            _pollRepo = new PollRepository();
            _questionRepo = new QuestionRepository();
            _voteRepo = new VoteRepository();
            _optionRepo = new OptionRepository();
        }

        public ResultDto CreatePoll(string title, string questionText, string option1, string option2, string option3, string option4)
        {
            if (string.IsNullOrWhiteSpace(title))
                return new ResultDto { IsSuccess = false, Message = "Title is required." };

            if (string.IsNullOrWhiteSpace(questionText))
                return new ResultDto { IsSuccess = false, Message = "Question is required." };

            try
            {
                var existingPoll = _pollRepo.GetPollByTitle(title);

                Poll poll;
                if (existingPoll != null)
                {
                    poll = existingPoll;
                }
                else
                {
                    poll = new Poll { Title = title };
                    _pollRepo.CreatePoll(poll);
                }

                var question = new Question
                {
                    Text = questionText,
                    PollId = poll.Id
                };
                _questionRepo.CreateQuestion(question);

                var options = new List<Option>
                {
                    new Option { Text = option1, QuestionId = question.Id },
                    new Option { Text = option2, QuestionId = question.Id },
                    new Option { Text = option3, QuestionId = question.Id },
                    new Option { Text = option4, QuestionId = question.Id }
                };
                _optionRepo.CreateOptionList(options);

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = existingPoll == null
                        ? "Poll created successfully."
                        : "Question added to existing poll successfully."
                };
            }
            catch (Exception ex)
            {
                return new ResultDto { IsSuccess = false, Message = $"Error creating poll: {ex.Message}" };
            }
        }


        public ResultDto DeletePoll(int pollId)
        {
            var hasVotes = _voteRepo.AnyUserHasVoted(pollId);
            if (hasVotes)
                return new ResultDto { IsSuccess = false, Message = "Cannot delete the poll because votes already exist." };

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

        public List<ShowQuestionTextDto> ShowQuestionText(int pollId)
        {
            return _questionRepo.GetQuestion(pollId);
        }

        public List<ShowQuestionsDto> ShowPolls()
        {
            return _pollRepo.ShowPolls();
        }

        public ResultDto Vote(int questionId, int selectedOptionNumber, int userId, string username)
        {
            var options = _questionRepo.GetOptionsFromQuestion(questionId);
            if (options == null || options.Count < selectedOptionNumber || selectedOptionNumber < 1)
                return new ResultDto { IsSuccess = false, Message = "Invalid option number." };

            var selectedOption = options[selectedOptionNumber - 1];

            if (_voteRepo.UserHasVoted(questionId, userId))
                return new ResultDto { IsSuccess = false, Message = "You have already voted in this poll." };

            var vote = new Vote
            {
                OptionId = selectedOption.Id,
                UserId = userId,
                UserName = username,
                VotedAt = DateTime.Now
            };

            _voteRepo.AddVote(vote);

            return new ResultDto { IsSuccess = true, Message = "Vote recorded successfully." };
        }



    }
}
