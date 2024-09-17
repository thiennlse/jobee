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

        public async Task<InterviewQuestion> getQuestionByID(int id)
        {
            return await _dbContext.InterviewQuestions.SingleOrDefaultAsync(q => q.QuestionId.Equals(id));
        }
    }
}
