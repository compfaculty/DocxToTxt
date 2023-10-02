using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

var convertor = new DocxToTxt();
try
{
    convertor.Run(args[0], null);
}
catch (Exception)
{
    Console.WriteLine("Error: looks docx file is missing or not found, i.e. DocxToTxt.exe \"d:\\myfile.docx\"");
}

public sealed class DocxToTxt
{
    private void SaveToTxt(string? content, string savePath)
    {
        File.WriteAllText(savePath, content);
    }

    private void ConvertDocxToTxt(string docxPath, string txtPath)
    {
        string content = ReadFromDocxText(docxPath);
        SaveToTxt(content, txtPath);
    }

    public string ReadFromDocxText(string filePath)
    {
        StringBuilder documentText = new StringBuilder();

        using (var doc = WordprocessingDocument.Open(filePath, false))
        {
            Body body = doc.MainDocumentPart!.Document.Body ?? throw new Exception("doc failed");

            foreach (var para in body.Elements())
            {
                documentText.AppendLine(para.InnerText);
            }
        }

        return documentText.ToString();
    }


    public void Run(string docxPath, string? txtPath)
    {
        txtPath ??= docxPath + ".txt";
        ConvertDocxToTxt(docxPath, txtPath);
        Console.WriteLine($"Conversion completed! Check \"{txtPath}\"");
    }
}