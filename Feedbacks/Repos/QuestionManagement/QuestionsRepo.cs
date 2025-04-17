using Feedbacks.Entities.Forms;
using Feedbacks.Repos.ServiceManagement;
using Feedbacks.ViewModels.Services;

namespace Feedbacks.Repos.QuestionManagement
{
    public class QuestionsRepo : IQuestionRepo, IValidateQuestionId
    {
        private readonly FeedbackDbContext _context;
        private readonly IValidateServiceId _validateService;
        public QuestionsRepo(FeedbackDbContext context, IValidateServiceId validateServiceId)
        {
            _context = context;
            _validateService = validateServiceId;
        }

        #region Write Questions


        public async Task<IEnumerable<QuestionViewModel>> AddQuestionsAsync(IEnumerable<QuestionViewModel> questionViewModels)
        {
            if(questionViewModels == null)
            {
                return null;
            }

            var questionViewModelsList = new List<QuestionViewModel>();
            foreach (var questionViewModel in questionViewModels)
            {
                var question = await AddQuestionAsync(questionViewModel);
                if (question == null)
                {
                    return null;
                }
            }
            try
            {
                await _context.SaveChangesAsync();
                return questionViewModelsList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<QuestionViewModel> UpdateQuestionASync(QuestionViewModel questionViewModel)
        {
            if (questionViewModel == null)
            {
                questionViewModel.Massage = "Question not selected";
                return questionViewModel;
            }
            var question = await _context.Questions.FindAsync(questionViewModel.Id);
            if (question is null)
            {
                questionViewModel.Massage = "Question not found";
                return questionViewModel;
            }

            question.QuestionText = questionViewModel.QuestionText;
            question.QuestionType = questionViewModel.QuestionType;

            if(await IsMultibleChoices(question.QuestionType))
            {
                question.Choices = _context.Choices
                    .Where(x => x.QuestionId == questionViewModel.Id)
                    .Select(c => new Choice()).ToList();
                foreach (var choice in questionViewModel.Choices)
                {
                    var newChoice = new Choice
                    {
                        Text = choice.Text,
                        QuestionId = question.Id,
                    };
                    try
                    {
                        question.Choices.Add(newChoice);
                    }
                    catch (NotSupportedException ex)
                    {
                        questionViewModel.Massage = ex.Message;
                        return questionViewModel;
                    }
                }
            }

            try
            {
                _context.Questions.Update(question);
                await _context.SaveChangesAsync();
                questionViewModel.Massage = "Question updated successfully";
                return questionViewModel;
            }
            catch (Exception ex)
            {
                questionViewModel.Massage = ex.Message;
                return questionViewModel;
            }
        }

        public async Task<string> DeleteQuestionASync(string id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question is null)
            {
                return "Question not found";
            }
            question.IsDeleted = true;
            try
            {
                _context.Questions.Update(question);
                await _context.SaveChangesAsync();
                return "Question deleted successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion
        //Get All form's questions - Get QuestionById - Get QuestionByType
        #region Get Questions
        public async Task<List<Question>> GetAllQuestionsAsync(string serviceId)
        {
            var questions = new List<Question>();
            if (await _validateService.GetServiceByIdAsync(serviceId) == null)
            {
                return null;
            }

            try
            {
                questions = await _context.Questions
                    .Where(q => q.ServiceId == serviceId && q.IsDeleted == false)
                    .Select(q => new Question
                    {
                        Id = q.Id,
                        QuestionText = q.QuestionText,
                        QuestionType = q.QuestionType,
                    }).ToListAsync();
                return questions;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Question> GetQuestionByIdAsync(string id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question is null)
            {
                return null;
            }
            return question;
        }

        public async Task<List<Question>> GetQuestionsByTypeAsync(int type)
        {
            var questions = new List<Question>();
            try
            {
                questions = await _context.Questions
                    .Where(q => q.QuestionType == type && q.IsDeleted == false)
                    .Select(q => new Question
                    {
                        Id = q.Id,
                        QuestionText = q.QuestionText,
                        QuestionType = q.QuestionType,
                        ServiceId = q.ServiceId,
                    }).ToListAsync();
                return questions;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


        private async Task<QuestionViewModel> AddQuestionAsync(QuestionViewModel questionViewModel)
        {
            var question = new Question
            {
                Id = Guid.NewGuid().ToString(),
                QuestionText = questionViewModel.QuestionText,
                QuestionType = questionViewModel.QuestionType,
                ServiceId = questionViewModel.ServiceId,
                IsDeleted = false,
            };
            var isMultipleChoices = await IsMultibleChoices(question.QuestionType);
            if(isMultipleChoices)
            {
                question.Choices = new List<Choice>();
                foreach (var choice in questionViewModel.Choices)
                {
                    var newChoice = new Choice
                    {
                        Text = choice.Text,
                        QuestionId = question.Id,
                    };
                    try
                    {
                        question.Choices.Add(newChoice);

                    }
                    catch (NotSupportedException ex)
                    {
                        questionViewModel.Massage = ex.Message;
                        return questionViewModel;
                    }
                }
            }
            else
            {
                question.Choices = null;
            }

            try
            {
                _context.Questions.Add(question);
            }
            catch (Exception ex)
            {
                questionViewModel.Massage = ex.Message;
                return questionViewModel;
            }
            return questionViewModel;

        }
        private async Task<bool> IsMultibleChoices(int questionType)
        {
            if (questionType == 3)
            {
                return true;
            }
            return false;
        }
        
    }
}
