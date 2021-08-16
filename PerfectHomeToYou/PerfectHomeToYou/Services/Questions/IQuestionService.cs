using PerfectHomeToYou.Services.Questions.Models;
using System.Collections.Generic;

namespace PerfectHomeToYou.Services.Questions
{
    public interface IQuestionService
    {
        int Create(int clientId, int apartmentId, string message);

        bool Delete(int questionId);

        IEnumerable<QuestionDetailsServiceModel> ByUser(string userId);
    }
}
