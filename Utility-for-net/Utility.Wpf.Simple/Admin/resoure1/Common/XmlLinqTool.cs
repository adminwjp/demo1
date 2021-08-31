using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    using System.Xml.Linq;
    public class XmlLinqTool
    {
        public static XDeclaration Declaration { get; set; } = null;

        public static void CreateXml(List<XNode> nodes,string path)
        {
            List<XElement> list = new List<XElement>();
            foreach (XNode node in nodes)
            {
                XNode nextNode = node;
                XElement firstElement = null;
                XElement currentElement = null;
                while (nextNode != null)
                {
                    XElement element = null;
                    if (nextNode.NodeAttributes!=null&& nextNode.NodeAttributes.Count>0)
                    {
                        List<XAttribute> attributes = new List<XAttribute>(nextNode.NodeAttributes.Count);
                        foreach (KeyValuePair<string,string> item in nextNode.NodeAttributes)
                        {
                            attributes.Add(new XAttribute(item.Key, item.Value));
                        }
                        element = new XElement(nextNode.NodeName,attributes);
                    }
                    else
                    {
                        element = new XElement(nextNode.NodeName);
                    }
                    firstElement = firstElement ?? element;
                    if(currentElement!=null)
                    {
                        currentElement.SetElementValue(currentElement.Name,element);
                    }
                    currentElement = element;
                    nextNode = node.NextNode;
                }
                list.Add(firstElement);
            }
            XDocument document = new XDocument(list);
            document.Declaration = Declaration?? new XDeclaration(version:"1.0",encoding:"utf-8",standalone:"yes");
            document.Save(path);
        }

    }
    public class XNode
    {
        public string NodeName { get; set; }
        public  Dictionary<string,string> NodeAttributes { get; set; }
        public XNode NextNode { get; set; }
    }
}
