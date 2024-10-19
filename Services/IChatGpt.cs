using Microsoft.AspNetCore.Http;

namespace Services;

public interface IChatGpt
{
    Task<string> GradeCV(string cvContent);

    Task<string> PDFToString(IFormFile file);

    Task<string> AskQuestion(string cvContent);
}

