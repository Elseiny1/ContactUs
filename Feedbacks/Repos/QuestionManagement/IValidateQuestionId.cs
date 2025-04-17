using Feedbacks.Entities.Forms;

namespace Feedbacks.Repos.QuestionManagement
{
    public interface IValidateQuestionId
    {
        public Task<Question> GetQuestionByIdAsync(string id);

    }
}
