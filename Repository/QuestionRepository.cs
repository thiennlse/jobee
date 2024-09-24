using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class QuestionRepository : BaseRepository<InterviewQuestion>, IQuestionRepository
    {
        private readonly DBContext _dbContext;
        private readonly BaseRepository<InterviewQuestion> _interviewQuestionRepository;
        public QuestionRepository(DBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _interviewQuestionRepository = new BaseRepository<InterviewQuestion>(dbContext);
        }

        public async Task add(InterviewQuestion entity)
        {
            await _interviewQuestionRepository.add(entity);
        }

        public async Task deleteById(int id)
        {
            await _interviewQuestionRepository.deleteById(id);
        }

        public async Task<List<InterviewQuestion>> getAll()
        {
            return await _interviewQuestionRepository.getAll();
        }

        public async Task<InterviewQuestion> getById(int id)
        {
            return await _interviewQuestionRepository.getById(id);
        }

        public async Task<InterviewQuestion> update(InterviewQuestion entity)
        {
            return await _interviewQuestionRepository.update(entity);
        }
    }
}
