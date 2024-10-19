// Services/ChatGptService.cs
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Mscc.GenerativeAI;
using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;

namespace Services;

public class ChatGptService : IChatGpt
{
    private readonly IConfiguration _configuration;
    private readonly string _generativeApiKey; // Thay đổi để lưu trữ API Key

    public ChatGptService(IConfiguration configuration)
    {
        _configuration = configuration;
        _generativeApiKey = _configuration["GenerativeAI:ApiKey"] ?? throw new Exception("Cannot find Generative AI API Key"); // Lấy API Key từ cấu hình
    }

    public async Task<string> AskQuestion(string cvContent)
    {
        var prompt = $"Hãy đặt 5 câu hỏi ít nhất có 2 câu liên quan đến công việc và kinh nghiệm, có 2 câu về kiến thức chuyên sâu ở trong CV này:{cvContent}, trả về theo thẻ html, không trả về <!DOCTYPE html> và <html> , no yapping";
        var model = new GenerativeModel();
        model.ApiKey = _generativeApiKey;
        var response = await model.GenerateContent(prompt);

        return response.ToString();
    }

    public async Task<string> GradeCV(string cvContent)
    {
        var prompt = $"Hãy đánh giá về nội dung CV này và đưa ra ý kiến: {cvContent}, trả về theo thẻ html, không trả về <!DOCTYPE html> và <html> , no yapping";
        var model = new GenerativeModel();
        model.ApiKey = _generativeApiKey;
        var response = await model.GenerateContent(prompt); // Sử dụng model với prompt

        return response.ToString();
    }

    public Task<string> PDFToString(IFormFile file)
    {
        StringBuilder text = new StringBuilder();

        var tempFilePath = Path.GetTempFileName();
        using (var stream = new FileStream(tempFilePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        using (PdfReader reader = new PdfReader(tempFilePath))
        using (PdfDocument document = new PdfDocument(reader))
        {
            for (int i = 1; i <= document.GetNumberOfPages(); i++)
            {
                var pageText = PdfTextExtractor.GetTextFromPage(document.GetPage(i));
                text.AppendLine(pageText);
            }
        }
        File.Delete(tempFilePath);
        return Task.FromResult(text.ToString());
    }
}
