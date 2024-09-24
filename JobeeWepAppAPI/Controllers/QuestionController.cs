using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Services;
using Services.UnitOfWork;

namespace JobeeWepAppAPI.Controllers
{
    [ApiController]
    [Route("api/Interview")]
    public class QuestionController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public QuestionController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("question")]
        public async Task<IActionResult> GetQuestionByID(int id)
        {
            if (id == 0)
            {
                var allQuestions = await _unitOfWork.QuestionRepo.getAll();
                if (allQuestions == null || !allQuestions.Any())
                {
                    return NotFound("Không tìm thấy bản ghi nào của question");
                }
                return Ok(allQuestions);
            }
            else if (id > 0)
            {
                var allQuestions = await _unitOfWork.QuestionRepo.getById(id);
                if (allQuestions == null)
                {
                    return NotFound($"Không tìm thấy question với ID {id}");
                }
                return Ok(allQuestions);
            }
            else
            {
                return BadRequest("ID không hợp lệ");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddQuestion([FromBody] InterviewQuestion question)
        {
            await _unitOfWork.QuestionRepo.add(question);
            return Created("Question created successfully", question);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateQuestion([FromBody] InterviewQuestion question)
        {
            await _unitOfWork.QuestionRepo.update(question);
            return Ok(question);
        }

        [HttpDelete("/{id}")]
        public async Task<IActionResult> DeleteQuesiton(int id)
        {
            await _unitOfWork.QuestionRepo.deleteById(id);
            return NoContent();
        }
    }
}
