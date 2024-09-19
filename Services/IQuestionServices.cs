using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IQuestionServices
    {
        Task<List<InterviewQuestion>> getAllQuestion();

        Task<List<InterviewQuestion>> getQuestionByID(int id);

        Task addQuestion(InterviewQuestion question);

        Task<InterviewQuestion> updateQuestion(InterviewQuestion interviewQuestion);
    }
}
