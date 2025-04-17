using Feedbacks.Entities.Forms;
using Feedbacks.ViewModels.Services;

namespace Feedbacks.Repos.QuestionManagement
{
    public interface IQuestionRepo
    {
        public Task<QuestionViewModel> AddQuestionAsync(QuestionViewModel questionViewModel);
        public Task<QuestionViewModel> UpdateQuestionASync(QuestionViewModel questionViewModel);
        public Task<string> DeleteQuestionASync(string id);


        public Task<List<Question>> GetAllQuestionsAsync(string serviceId);
        public Task<Question> GetQuestionByIdAsync(string id);
        public Task<List<Question>> GetQuestionsByTypeAsync(int type);

    }
}
