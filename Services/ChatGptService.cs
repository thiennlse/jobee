// Services/ChatGptService.cs
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Mscc.GenerativeAI;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Text;

namespace Services;

public class ChatGptService : IChatGpt
{
    private readonly IConfiguration _configuration;
    private readonly IConfigurationSection _section;

    public ChatGptService(IConfiguration configuration, IConfigurationSection section)
    {
        _configuration = configuration;
        _section = _configuration.GetSection("OpenAI");
    }

    public async Task<string> GradeCV(string cvContent)
    {
        var googleAI = new GoogleAI(_section.GetSection("SecretKey").Value);
        var prompt = $"Hãy đánh giá về nội dung CV này và đưa ra ý kiến {cvContent}";
        var model = new GenerativeModel();
        var response  = model.GenerateContent(prompt).Result;
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

        using (PdfDocument document = PdfReader.Open(tempFilePath, PdfDocumentOpenMode.ReadOnly))
        {
            for (int i = 0; i < document.PageCount; i++)
            {
                var page = document.Pages[i];
                text.AppendLine($"Page {i + 1}: (Nội dung không thể trích xuất bằng PdfSharp)");
            }
        }
        File.Delete(tempFilePath);
        return Task.FromResult(text.ToString());
    }
}