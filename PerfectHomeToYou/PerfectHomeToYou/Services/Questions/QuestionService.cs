using System.Linq;
using System.Collections.Generic;

using PerfectHomeToYou.Data;
using PerfectHomeToYou.Data.Models;
using PerfectHomeToYou.Services.Questions.Models;

namespace PerfectHomeToYou.Services.Questions
{
    public class QuestionService : IQuestionService
    {
        private readonly PerfectHomeToYouDbContext context;

        public QuestionService(PerfectHomeToYouDbContext context)
            => this.context = context;

        public int Create(int clientId, int apartmentId, string message)
        {
            var questionData = new Question
            {
                ClientId = clientId,
                ApartmentId = apartmentId,
                Message = message
            };

            this.context.Questions.Add(questionData);
            this.context.SaveChanges();

            return questionData.Id;
        }

        public bool Delete(int questionId)
        {
            var questionData = this.context.Questions.Find(questionId);

            if (questionData == null)
            {
                return false;
            }

            this.context.Questions.Remove(questionData);
            this.context.SaveChanges();

            return true;
        }

        public IEnumerable<QuestionDetailsServiceModel> ByUser(string userId)
            => this.context
                .Questions
                .Where(c => c.Client.UserId == userId)
                .Select(q => new QuestionDetailsServiceModel
                {
                    Id = q.Id,
                    ApartmentType = q.Apartment.ApartmentType,
                    CityName = q.Apartment.City.Name,
                    NeighborhoodName =q.Apartment.Neighborhood.Name,
                    Price = q.Apartment.Price,
                    RentOrSale = q.Apartment.RentOrSale,
                    DateOfCreation = q.DateOfCreation,
                    Message = q.Message
                })
                .ToList();
    }
}
