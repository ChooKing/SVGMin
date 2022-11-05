using System.IO;

namespace SVGMin.Models;

public class SVGFile
{
    private readonly string _name;
    private string? _contents;
    private string _minified;
    
    public SVGFile(string name)
    {
        this._name = name;
    }

    public string ReadFile()
    {
        using (var sr = new StreamReader(_name))
        {
            _contents = sr.ReadToEnd();
        }

        return _contents;
    }

    public void Minify()
    {
        
    }

    public string Contents
    {
        get => _contents ?? "";
    }
}