namespace Sevriukoff.Gwalt.Infrastructure;

public class FileContentType
{
    public string FileType { get; private set; }
    public string FileExtension { get; private set; }

    public FileContentType(string contentType)
    {
        ParseContentType(contentType);
    }

    private void ParseContentType(string contentType)
    {
        if (!string.IsNullOrEmpty(contentType) && contentType.Contains('/'))
        {
            string[] parts = contentType.Split('/');
            
            FileType = parts[0].Trim();
            FileExtension = parts[1].Trim();
        }
        else
        {
            FileType = "Unknown";
            FileExtension = "";
        }
    }

    public override string ToString()
        => $"{FileType}/{FileExtension}";
}