/*! HTMLdecode (http://kionier.com/) | (c) 2015 Kionier | Licensed GNU GPL/LGPL http://www.gnu.org/licenses/ */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace HTMLdecode
{
    public class HTMLdecode
    {
        public List<Element> Elements { get; private set; }
        public List<Parameter> Parameters { get; private set; }
        public List<Tag> Tags { get; private set; }
        public Element RootElement 
        { 
            get {return _RootElement;}
            private set { _RootElement = value; }
        }
        private Element _RootElement = new Element();
        public String HTML { get; private set; }

        public HTMLdecode()
        {
            Elements = new List<Element>();
            Parameters = new List<Parameter>();
        }

        public void Startprocess(string htmlSnippet)
        {
            HTML = htmlSnippet.Trim();           
            
            #region Extract tags

            int i = 0;
            int k = 0;
            int j = 0;
            String tagType = "";
            Tag.EnumFlavor flavor = Tag.EnumFlavor.Text;
            Boolean inQoute = false;
            char qouteType = ' ';
            List<Tag> tags = new List<Tag>();
            Tag tag;

            while(i< HTML.Length)
            {

                if (tagType != "" & tagType != "text" & tagType != "commit")
                {
                    if (inQoute == false && (HTML[i] == '\"' | HTML[i] == '\''))
                    {
                        qouteType = HTML[i];
                        inQoute = true;
                    }
                    else if (inQoute == true && HTML[i] == qouteType)
                    {
                        inQoute = false;
                    }
                }
                
                if (tagType == "")
                {                    
                    if (HTML.Substring(i, 4) == "<!--")
                    {
                        tagType = "commit";
                        flavor = Tag.EnumFlavor.Commit;

                        k = i;
                    }
                    else if (HTML.Substring(i, 7).ToLower() == "<script")
                    {
                        tagType = "script";
                        flavor = Tag.EnumFlavor.Script;
                        k = i;
                    }
                    else if (HTML[i] == '<')
                    {
                        tagType = "tag";
                        flavor = Tag.EnumFlavor.Open;
                        k = i;
                    }
                    else if (tagType == "")
                    {
                        tagType = "text";
                        flavor = Tag.EnumFlavor.Text;
                        k = i;
                    }

                }
                else if (tagType == "tag" && inQoute == false)
                {
                    if (HTML[i] == '>')
                    {
                        tagType = "";
                        tag = new Tag();
                        tag.Value=HTML.Substring(k, i - k + 1);
                        j = tag.Value.Length-1;
                        foreach (char c in new List<char> { '\n', '\r', ' ', '>', ':' })
                        {
                            if (tag.Value.IndexOf(c) != -1)
                            {
                                j= Math.Min(tag.Value.IndexOf(c), j);
                            }
                        }
                        tag.Name = tag.Value.Substring(1, j - 1).ToUpper();
                        tag.Flavor = flavor;
                        tags.Add(tag);
                    }
                }
                else if (tagType == "script" && inQoute == false)
                {
                    if (HTML.Substring(i, 9) == "</script>")
                    {
                        tagType = "";
                        tag = new Tag();
                        tag.Name = "SCRIPT";
                        tag.Value=HTML.Substring(k, i - k + 9);
                        tag.Flavor = Tag.EnumFlavor.Script;
                        tags.Add(tag);
                        i += 8;
                    }
                }
                else if (tagType == "commit")
                {
                    if (HTML.Substring(i, 3) == "-->")
                    {
                        tagType = "";
                        tag = new Tag();
                        tag.Name = "!--";
                        tag.Value=HTML.Substring(k, i - k + 3);
                        tag.Flavor = Tag.EnumFlavor.Commit;
                        tags.Add(tag);
                        i += 2;
                    }
                }
                else if (tagType == "text")
                {
                    if (HTML[i] == '<')
                    {
                        tagType = "";
                        tag = new Tag();
                        tag.Name = "";
                        tag.Value=HTML.Substring(k, i - k);
                        tag.Flavor = Tag.EnumFlavor.Text;
                        tags.Add(tag);
                        k = i;
                        i--;
                    }
                }
                
                i++;
            }
            
            #endregion

            tags.ForEach(delegate(Tag t)
            {
                if (t.Flavor == Tag.EnumFlavor.Open)
                {
                    if (t.Value.StartsWith("</"))
                    {
                        t.Flavor = Tag.EnumFlavor.Close;
                    }
                    else if(t.Value.EndsWith("/>"))
                    {
                        t.Flavor = Tag.EnumFlavor.Self;
                    }
                }
            });


            if (tags.Count > 0)
            {
                int start = 0;
                Tag doctype = tags.FirstOrDefault(t => t.Name == "!DOCTYPE");
                if (doctype is Tag)
                {
                    start = tags.IndexOf(doctype) + 1;
                    Element element = new Element(this, doctype);
                    this._RootElement.Elements.Add(element);
                    
                }

                processElement(this._RootElement, tags.Skip(start));
            }

            this.Tags = tags;
        }

        private void processElement(Element parent, IEnumerable<Tag> tags)
        {
            for (int i = 0; i < tags.Count(); i++)
            {
                Tag iTag = tags.ElementAt(i);
                if (iTag.Flavor == Tag.EnumFlavor.Open)
                {
                    Element element = new Element(this, iTag);
                    parent.Elements.Add(element);
                    this.Elements.Add(element);
                    IEnumerable<Tag> subTags = tags.Skip(i+1);
                    Tag eTag = subTags.FirstOrDefault(t => t.Name == "/" + iTag.Name);
                    int c = subTags.TakeWhile(t => t != eTag).Count();
                    c = subTags.Take(c).Count(t => t.Name == iTag.Name)+1;
                    eTag = subTags.FirstOrDefault(t => t.Name == "/" + iTag.Name && --c == 0);
                    c = subTags.TakeWhile(t => t != eTag).Count();
                    subTags = subTags.Take(c);
                    if (subTags.Count() > 0) processElement(element, subTags);
                    i = i + c;

                }
                else if (iTag.Flavor != Tag.EnumFlavor.Close) 
                {
                    Element element = new Element(this, iTag);
                    parent.Elements.Add(element);
                    this.Elements.Add(element);
                }
            }
        }

        private void processElement(Element parent, List<Tag> tags, int start, int end)
        {
            Element element;
            string tagType = "";
            
            
            for (int i = start; i <= end; i++)
            {
                if (tagType == "")
                {
                    if (tags[i].Name == "!--" | tags[i].Name == "SCRIPT")
                    {
                        element = new Element(this);
                        element.Name = tags[i].Name;
                        element.Value = tags[i].Value;
                        this.Elements.Add(element);
                        parent.Elements.Add(element);
                        if (i + 1 < end)
                        {
                            processElement(parent, tags, i + 1, end);
                        }
                        return;

                    }
                    else if (tags[i].Name == "")
                    {
                        tagType ="text";
                    }
                    else
                    {
                        tagType = "tag";
                    }
                }
                else if (tagType == "tag")
                {
                    if ("/" + tags[start].Name == tags[i].Name)
                    {
                        element = new Element(this);
                        element.Name = tags[start].Name;
                        element.Value = tags[start].Value;
                        this.Elements.Add(element);
                        parent.Elements.Add(element);
                        if (start+1 <= i - 1)
                        {
                            processElement(element, tags, start+1,i-1 );
                        }
                        if (i + 1 <= end)
                        {
                            processElement(parent, tags, i + 1, end);
                        }
                        return;

                    }
                    else if (i == end)
                    {
                        element = new Element(this);
                        element.Name = tags[i].Name;
                        element.Value = tags[i].Value;
                        this.Elements.Add(element);
                        parent.Elements.Add(element);
                        if (start + 1 <= end)
                        {
                            processElement(parent, tags, start + 1, end);
                        }
                        return;
                    }

                }
                else if (tagType == "text")
                {
                    if (tags[i].Name != "")
                    {
                        i--;
                        element = new Element(this);
                        element.Name = tags[i].Name;
                        element.Value = tags[i].Value;
                        this.Elements.Add(element);
                        parent.Elements.Add(element);
                        if (i + 1 < end)
                        {
                            processElement(parent, tags, i + 1, end);
                        }
                        return;
                    }
                }
            }
        }
    }
}
