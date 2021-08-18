using Xunit;
using MyTested.AspNetCore.Mvc;

using PerfectHomeToYou.Controllers;
using PerfectHomeToYou.Models.Questions;

namespace PerfectHomeToYou.Test.Routing
{
    public class QuestionsControllerTest
    {
        [Fact]
        public void GetAddShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap("/Questions/Add")
               .To<QuestionsController>(c => c.Add());

        [Fact]
        public void PostAddShoulBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Questions/Add/1")
                    .WithMethod(HttpMethod.Post))
                .To<QuestionsController>(c => c.Add(1,With.Any<QuestionFormModel>()));

        [Fact]
        public void DeleteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Questions/Delete/1")
                .To<QuestionsController>(c => c.Delete(1));

        [Fact]
        public void MyQuestionsShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap("/Questions/MyQuestions")
               .To<QuestionsController>(c => c.MyQuestions());
    }
}
