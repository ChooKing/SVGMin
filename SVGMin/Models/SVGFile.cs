using System;
using System.IO;
using System.Text;

namespace SVGMin.Models;
enum States
{
    Copy, BuildPdStr, BuildNum
}
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
        if (_contents.Length > 0)
        {
            StringBuilder convStr = new StringBuilder();
            StringBuilder pdStr = new StringBuilder();
            StringBuilder numStr = new StringBuilder();
            States state = States.Copy;
            const string pathStr = " d=\"";

            foreach (var c in _contents)
            {

                if (c == ' ' && state != States.BuildNum)
                {
                    pdStr.Append(c);
                    state = States.BuildPdStr;
                }
                else if (pathStr.StartsWith(pdStr.ToString() + c))
                {
                    pdStr.Append(c);
                }
                else if (state == States.BuildPdStr && !pathStr.StartsWith(pdStr.ToString() + c))
                {
                    state = States.Copy;
                    pdStr.Clear();
                }
                if (state == States.BuildNum)
                {
                    if ("0123456789.".Contains(c))
                    {
                        numStr.Append(c);
                    }
                    else
                    {
                        if (numStr.Length > 0)
                        {
                            convStr.Append(Math.Round(Double.Parse(numStr.ToString())).ToString());
                            numStr.Clear();
                        }
                        convStr.Append(c);
                        if (c == '"')
                        {
                            state = States.Copy;
                        }

                    }

                }
                else
                {
                    convStr.Append(c);
                }


                if (pathStr == pdStr.ToString())
                {
                    state = States.BuildNum;
                    pdStr.Clear();
                }


            }
            _minified = convStr.ToString();
        }
    }

    public string Contents
    {
        get => _contents ?? "";
    }

    public string Minified
    {
        get => _minified ?? "";
    }
}