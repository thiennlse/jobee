using BusinessObject.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class QuestionServices : IQuestionServices
    {
        private IQuestionRepository _repo;

        public QuestionServices(IQuestionRepository repo)
        {
            _repo = repo;
        }

        public async Task addQuestion(InterviewQuestion question)
        {
            await _repo.addQuestion(question);
        }

        public async Task<List<InterviewQuestion>> getAllQuestion()
        {
            return await _repo.getAllQuestion();  
        }

        public async Task<List<InterviewQuestion>> getQuestionByID(int id)
        {
            return await _repo.getQuestionByID(id);
        }

        public async Task<InterviewQuestion> updateQuestion(InterviewQuestion interviewQuestion)
        {
            return await _repo.updateQuestion(interviewQuestion);
        }
    }
}
