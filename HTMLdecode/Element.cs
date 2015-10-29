/*! HTMLdecode (http://kionier.com/) | (c) 2015 Kionier | Licensed GNU GPL/LGPL http://www.gnu.org/licenses/ */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTMLdecode
{
    public class Element
    {
        private String _Name;
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private String _Value;
        public String Value
        {
            get { return _Value; }
            set { _Value = value; processParameters(); }
        }
        public List<Parameter> Parameters { get; set; }
        public List<Element> Elements { get; set; }
        private HTMLdecode _HTMLdecode;

        public Element(HTMLdecode htmlDecode)
        {
            _HTMLdecode = htmlDecode;
            Elements = new List<Element>();
            Parameters = new List<Parameter>();
        }

        public Element()
        {
            Elements = new List<Element>();
            Parameters = new List<Parameter>();

        }

        public Element(HTMLdecode htmlDecode, Tag tag)
        {
            _HTMLdecode = htmlDecode;
            Elements = new List<Element>();
            Parameters = new List<Parameter>();
            Name = tag.Name;
            Value = tag.Value;
            this.processParameters();
        }

        private void processParameters()
        {
            if (this.Name != null & this.Value != null & this.Value != "" & this.Name != "!--" & this.Name != "script" & this.Name != "" & this._HTMLdecode != null)
            {
                String parStage = "";
                char qouteType = ' ';
                int i = 0;
                int k = 0;
                Parameter parameter = new Parameter();
                while (i < this._Value.Length)
                {
                    if (parStage == "")
                    {
                        if (this._Value[i] == '<')
                        {
                            parStage = "tag";
                        }
                        else if (this._Value[i] == ' ' | this._Value[i] == '\r' | this._Value[i] == '\n'
                            | this._Value[i] == '\t')
                        {
                        }
                        else if (this._Value[i] == '>')
                        {
                            break;
                        }
                        else
                        {
                            parStage = "par";
                            k = i;
                        }
                    }
                    else if (parStage == "tag")
                    {
                        if (this._Value[i] == ' ' | this._Value[i] == '\r' | this._Value[i] == '\n' | this._Value[i] == '\t')
                        {
                            parStage = "";
                        }
                        if (this._Value[i] == '>')
                        {
                            break;
                        }

                    }
                    else if (parStage == "par")
                    {
                        if (this._Value[i] == ' ' | this._Value[i] == '\r' | this._Value[i] == '\n'
                            | this._Value[i] == '\t' | this._Value[i] == '=')
                        {
                            parStage = "eq";
                            parameter = new Parameter();
                            parameter.Name = this._Value.Substring(k, i - k).ToLower();
                        }

                    }
                    else if (parStage == "eq")
                    {
                        if (this._Value[i] == '"' | this._Value[i] == '\'')
                        {
                            k = i + 1;
                            parStage = "value";
                            qouteType = this._Value[i];

                        }
                    }
                    else if (parStage == "value")
                    {
                        if (this._Value[i] == qouteType)
                        {
                            parameter.Value = this._Value.Substring(k, i - k);
                            parStage = "";
                            this.Parameters.Add(parameter);
                            this._HTMLdecode.Parameters.Add(parameter);
                        }
                    }
                    i++;
                }
            }
        }

        public override string ToString()
        {
            return Name + "; " + Value;
        }

        public string GetInnerText()
        {
            StringBuilder sb = new StringBuilder();
            this.GetInnerText(sb);
            return sb.ToString();
        }

        public void GetInnerText(StringBuilder sb)
        {
            if (this.Name == "")
            {
                sb.Append(this.Value);
            }

            foreach (Element e in Elements)
            {
                e.GetInnerText(sb);
            }
        }

        public List<Parameter> GetAllParameter()
        {
            List<Parameter> p = new List<Parameter>();
            p.AddRange(this.Parameters);
            foreach (Element e in this.Elements)
            {
                p.AddRange(e.GetAllParameter());
            }

            return p;
        }

        public List<Element> GetElement(string name, Boolean child, params KeyValuePair<string, string>[] prop)
        {
            List<Element> elements = this.Elements.Where(e => e.Name == name.ToUpper()).ToList();
            foreach (KeyValuePair<string,string> kp in prop)
            {
                elements = elements.Where(e => e.Parameters.Count(p => p.Name.ToLower() == kp.Key & p.Value.ToLower() == kp.Value) > 0).ToList();
            }

            if (child)
            {
                foreach(Element element in elements)
                {
                    elements.AddRange(element.GetElement(name, child, prop));
                }
            }

            return elements;
        }
    }
}
