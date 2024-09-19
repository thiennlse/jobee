using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private DBContext _dbContext;

        public QuestionRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        List<InterviewQuestion> _interviews;

        public async Task<List<InterviewQuestion>> getAllQuestion()
        {
            return await _dbContext.InterviewQuestions.ToListAsync();
        }

        public async Task<List<InterviewQuestion>> getQuestionByID(int id)
        {
            _interviews = new List<InterviewQuestion>();
            _interviews.Add(await _dbContext.InterviewQuestions.SingleOrDefaultAsync(q => q.QuestionId.Equals(id)));
            return _interviews;
        }

        public async Task addQuestion(InterviewQuestion question)
        {
            _dbContext.InterviewQuestions.Add(question);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<InterviewQuestion> updateQuestion(InterviewQuestion interviewQuestion)
        {
            _dbContext.Entry(interviewQuestion).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return interviewQuestion;
        }
    }
}
