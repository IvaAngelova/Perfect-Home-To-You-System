using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PerfectHomeToYou.Infrastructure;
using PerfectHomeToYou.Models.Questions;
using PerfectHomeToYou.Services.Clients;
using PerfectHomeToYou.Services.Questions;

using static PerfectHomeToYou.WebConstants;
using static PerfectHomeToYou.Areas.Admin.AdminConstants;

namespace PerfectHomeToYou.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IClientService clients;
        private readonly IQuestionService questions;

        public QuestionsController(IClientService clients, IQuestionService questions)
        {
            this.clients = clients;
            this.questions = questions;
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(int id, QuestionFormModel question)
        {
            var clientId = this.clients.IdByUser(this.User.Id());

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            this.questions.Create(clientId, id, question.Message);

            TempData[GlobalMessageKey] = "Successfully added question!";

            return RedirectToAction(nameof(Add));
        }

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var question = this.questions.Delete(id);

            if (!question)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(MyQuestions));
        }

        [Authorize]
        public IActionResult MyQuestions()
        {
            var myquestions = this.questions
                .ByUser(this.User.Id());

            return View(myquestions);
        }
    }
}
