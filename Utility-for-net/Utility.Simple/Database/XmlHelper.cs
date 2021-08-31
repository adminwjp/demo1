using System;
using System.Collections.Generic;
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System.Runtime.Serialization;
#endif
using System.Text;
using System.Threading;
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System.Xml;
#endif
#if !(NET20 || NET30)
using System.Xml.Linq;
#endif 


namespace Utility.Collections
{

    /// <summary>
    /// 链表 操作 方式
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// 单链表
        /// </summary>
        Single=0x0,
        /// <summary>
        /// 双 链表
        /// </summary>
        Double=0x1,

        /// <summary>
        /// 循环 链表 
        /// </summary>
        Loop=0x2
    }

    /// <summary>
    /// 链表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ArrayNode<T>
 #if !(NET10 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)

        :ISerializable, IDeserializationCallback
#endif
    {
        private object syncObject;
        /// <summary>
        /// 头部
        /// </summary>
        private Node<T> head;
        /// <summary>
        /// 尾部
        /// </summary>
        private Node<T> foot;
        /// <summary>
        /// 单链表
        /// </summary>
        private NodeType nodeType = NodeType.Single;
        /// <summary>
        /// 长度
        /// </summary>
        private int  size;
        private int version;
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
        private SerializationInfo siInfo;
#endif

        private const string VersionName = "Version";

        private const string CountName = "Count";

        private const string ValuesName = "Data";

        public ArrayNode():this(NodeType.Single)
        {

        }
        public ArrayNode(NodeType nodeType):this(null,nodeType)
        {
           
        }

        public ArrayNode(Node<T> head, NodeType nodeType)
        {
            this.nodeType = nodeType;
            this.head = head;
        }
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="context"></param>
        protected ArrayNode(SerializationInfo serializationInfo,StreamingContext context)
        {

        }
#endif
        public object SyncObject
        {
            get {

                if (syncObject == null)
                {
                    Interlocked.CompareExchange(ref syncObject, new object(), null);
                }
                return syncObject;
            }
        }

        /// <summary>
        /// 根据 索引获取
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Node<T> this[int index]
        {
            get
            {
                var tIndex = index < -1 ? index + size : index;
                if(tIndex < size&& tIndex > -1)
                {
                    var current = head;
                    int i = 0;
                    do
                    {
                        if (i == tIndex)
                        {
                            return current;
                        }
                        current = current.Next;
                        i++;
                    } while (current.Next != null);
                }
                return null;
            }
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        public void Add(Node<T> data)
        {
            if (nodeType ==  NodeType.Single)
            {
                if (head == null)
                {
                    head = data;
                }
                if (foot == null)
                {
                    foot = data;
                }
                size++;
                return;
            }
            if (nodeType == NodeType.Double)
            {
                if (head == null)
                {
                    head = data;
                }
                if (foot == null)
                {
                    foot = data;
                }
                size++;
                return;
            }
            if (nodeType == NodeType.Loop)
            {
                if (head == null)
                {
                    head = data;
                }
                if (foot == null)
                {
                    foot = data;
                }
                size++;
                return;
            }

        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {

        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Add(int index,T item)
        {

        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        public void Add(int index,Node<T> data)
        {
         
        }
        /// <summary>
        ///复制
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(T[] array,int index)
        {

        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="sender"></param>
        public void OnDeserialization(object sender)
        {
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
            if (siInfo == null)
            {
                return;
            }
            int version1 = siInfo.GetInt32(VersionName);
            if (siInfo.GetInt32(CountName) != 0)
            {
                T[] datas = (T[])siInfo.GetValue(ValuesName, typeof(T[]));
                for (int i = 0; i < datas.Length; i++)
                {
                    Add(datas[i]);
                }
            }
            else
            {
                head = null;
            }
            siInfo = null;
            version = version1;
#endif
        }
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
        /// <summary>
        /// 获取 序列化 值
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                return;
            }
            info.AddValue(CountName,size);
            info.AddValue(VersionName, version);
            if (size != 0)
            {
                T[] objs = new T[size];
                CopyTo(objs, 0);
                info.AddValue(ValuesName, objs, typeof(T[]));
            }
        }
#endif
    }

    public class Node<T>
    {

        internal T Value;
        private Node<T> pre;
        private Node<T> data;
        private  Node<T> next;

        public Node<T> Pre => pre;
        public Node<T> Data => data;

        public Node<T> Next => next;
        public Node(Node<T> data) : this(data, default(T))
        {
        }
        public Node(Node<T> data, T value)
        {
            this.data = data;
            this.Value = value;
        }
        internal void Invalidate()
        {
            pre = null;
            data = null;
            next = null;
        }
    }
}

namespace Utility.Xml
{
    public class DocumentEntity
    {

    }
    public class ElementEntity : NodeEntity
    {

        public ElementEntity()
        {

        }


        public ElementEntity(string name) : base(name)
        {

        }
    }

    public class ElementCollectionEntity : Utility.Collections.Array<ElementEntity>
    {

    }
    public class NodeCollectionEntity : Utility.Collections.Array<NodeEntity>
    {

    }

    public class NodeEntity
    {
        private bool hasNodes;
        private bool hasAttributes;

        public NodeEntity()
        {

        }


        public NodeEntity(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public void AddComment(string comment)
        {
            this.Comment = comment;
        }
        public bool HasAttributes { get => hasAttributes ? hasAttributes : Attributes != null && Attributes.Count > 0; set => hasAttributes = value; }
        public AttributeCollectionEntity Attributes { get; set; }
        public bool HasNodes { get => hasNodes ? hasNodes : Nodes != null && Nodes.Count > 0; set => hasNodes = value; }
        public NodeCollectionEntity Nodes { get; set; }
        public string Comment { get; set; }
        public string Value { get; set; }
        public bool NameEqual(string name)
        {
            return name.ToLower().Equals(Name.ToLower());
        }
        public void Add(NodeEntity nodeEntity)
        {
            Nodes = Nodes ?? new NodeCollectionEntity();
            Nodes.Add(nodeEntity);
        }
        public void Add(AttributeEntity attributeEntity)
        {
            Attributes = Attributes ?? new AttributeCollectionEntity();
            Attributes.Add(attributeEntity);
        }

        public string GetValue(string name, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {
            if (Attributes == null) return string.Empty;
            for (int i = 0; i < Attributes.Count; i++)
            {
                if (string.Equals(name, Attributes[i].Name, comparisonType)) return Attributes[i].Value;
            }
            return string.Empty;
        }

        public bool Equals(NodeEntity other)
        {
            throw new NotImplementedException();
        }
    }
    //Array hashset 去重 
    public class AttributeEntity:IEquatable<AttributeEntity>
    {
        public AttributeEntity()
        {

        }
        public AttributeEntity(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }

        public bool Equals(AttributeEntity other)
        {
            return other != null && other.Name == this.Name;
        }
    }
    public class AttributeCollectionEntity : Utility.Collections.Array<AttributeEntity>
    {
        //hashset:要么 实现 IEquatable<AttributeEntity> 要么 实现 IEqualityComparer<AttributeEntity> linq xml attribute 不能有重复属性 
        public AttributeCollectionEntity(): base(EqualityComparer<AttributeEntity>.Default)
        {
        }
    }
    /// <summary>
    /// 统一 xml 操作 linq xml  or xml docment 操作方法不同 不然代码 写好几遍 类 哈
    /// </summary>
    public class XmlHelper
    {
#if !(NET20 || NET30)
        private class InnerXmlLinq
        {
            public XDocument Document { get; set; }
            public XElement Root => Document.Root;
            public ElementEntity Element { get; set; }
            public void Ininial()
            {
                Element = new ElementEntity() { };
                Attribute(Root, Element);
                Cursion(Root, Element);
            }
            public void Write()
            {
                XElement element = new XElement(Element.Name);
                Document.Add(element);
                CursionWrite(element, Element);
            }
            private void CursionWrite<Entity>(XElement element, Entity entity) where Entity : NodeEntity, new()
            {
                try
                {

                    if (!string.IsNullOrEmpty(entity.Comment))
                    {
                        element.AddBeforeSelf(new XComment(entity.Comment));
                    }
                    if (!string.IsNullOrEmpty(entity.Value))
                    {
                        element.Value=entity.Value;
                    }
                    if (entity.HasAttributes)
                    {
                        foreach (var item in entity.Attributes)
                        {
                            //xml linq 不支持 值为 null 否则 异常 
                            if (!string.IsNullOrEmpty(item.Value))
                            {
                                element.Add(new XAttribute(item.Name, item.Value));
                            }
                        }
                    }
                    if (entity.HasNodes)
                    {
                        foreach (var item in entity.Nodes)
                        {
                            XElement element1 = new XElement(item.Name);
                            element.Add(element1);
                            CursionWrite(element1, item);
                        }
                    }
                }
                catch (Exception e)
                {

                    throw;
                }

            }
            protected void Cursion<Entity>(XElement element, Entity entity) where Entity : NodeEntity, new()
            {
                entity.HasNodes = element.HasElements;
                if (element.HasElements)
                {
                    Entity entity1 = new Entity();
                    entity.Add(entity1);
                    foreach (XNode item in element.Nodes())
                    {
                        if(item is XElement  element1)
                        {
                            if (!string.IsNullOrEmpty(element1.Value))
                            {
                                entity1.Value = element1.Value;
                            }
                            Attribute(element1, entity1);
                            Cursion(element1, entity1);
                            if(item.PreviousNode !=null&& item.PreviousNode is XComment comment)
                            {
                                entity1.Comment = comment.Value;
                            }
                        }
                    }
                }
            }

            private void Attribute<Entity>(XElement element, Entity entity) where Entity : NodeEntity, new()
            {
                entity.Name = element.Name.ToString();
                entity.HasAttributes = element.HasAttributes;
                if (element.HasAttributes)
                    return;
                foreach (XAttribute attribute in element.Attributes())
                {
                    entity.Add(new AttributeEntity() { Name = attribute.Name.ToString(), Value = attribute.Value });
                }
            }


        }

#endif

#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
        private class InnerXmlDocment
        {
            public readonly XmlWriterSettings Settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
            public XmlDocument Document { get; set; }
            public XmlElement Root => Document.DocumentElement;
            public ElementEntity Element { get; set; }

            public void Ininial()
            {
                Element = new ElementEntity() { };
                Attribute(Root, Element);
                Cursion(Root, Element);
            }
            public void Write()
            {
                if(!string.IsNullOrEmpty(Element.Name))
                {
                    // Document.Normalize();
                    XmlElement element = Document.CreateElement(Element.Name);
                    Document.AppendChild(element);
                    CursionWrite(element, Element);
                }
            }
            private void CursionWrite<Entity>(XmlElement element, Entity entity) where Entity : NodeEntity, new()
            {
                if (!string.IsNullOrEmpty(entity.Comment))
                {
                    var comment = Document.CreateComment(entity.Comment);
                    element.ParentNode.InsertBefore(comment,element);
                }
                if (!string.IsNullOrEmpty(entity.Value))
                {
                    element.Value=entity.Value;
                }
                if (entity.HasAttributes)
                {
                    foreach (var item in entity.Attributes)
                    {
                        element.SetAttribute(item.Name, item.Value);
                    }
                }
                if (entity.HasNodes)
                {
                    foreach (var item in entity.Nodes)
                    {
                        if (!string.IsNullOrEmpty(item.Name))
                        {
                            XmlElement element1 = Document.CreateElement(item.Name);
                            element.AppendChild(element1);
                            CursionWrite(element1, item);
                        }
                    }
                }
            }
            protected void Cursion<Entity>(XmlElement element, Entity entity) where Entity : NodeEntity, new()
            {
                entity.HasNodes = element.HasChildNodes;
                if (element.HasChildNodes)
                {
                    entity.Nodes = new NodeCollectionEntity();
                    Entity entity1 = new Entity();
                    entity.Nodes.Add(entity1);
                    foreach (XmlNode item in element.ChildNodes)
                    {
                        if(item.NodeType== XmlNodeType.Element)
                        {
                            XmlElement xmlElement = (XmlElement)item;
                            if (!string.IsNullOrEmpty(xmlElement.Value))
                            {
                                entity1.Value = xmlElement.Value;
                            }
                            Attribute(xmlElement, entity1);
                            Cursion(xmlElement, entity1);
                            if(xmlElement.ParentNode!=null&&xmlElement.ParentNode.NodeType== XmlNodeType.Comment)
                            {
                                XmlComment comment = (XmlComment)xmlElement.ParentNode;
                                entity1.Comment = comment.Value;
                            }
                        }
                    }
                }
            }

            private void Attribute<Entity>(XmlElement element, Entity entity) where Entity : NodeEntity, new()
            {
                entity.Name = element.Name;
                entity.HasAttributes = element.HasAttributes;
                if (element.HasAttributes)
                    return;
                foreach (XmlAttribute attribute in element.Attributes)
                {
                    entity.Add(new AttributeEntity() { Name = attribute.Name, Value = attribute.Value });
                }
            }
        }


#endif
        public bool IsXmlDocment { get; set; } = true;

        public ElementEntity Element { get; set; }
  

        public void Initial(string path)
        {
            if (!IsXmlDocment)
            {
#if !(NET20 || NET30)
                InnerXmlLinq innerXmlLinq = new InnerXmlLinq();
                innerXmlLinq.Document = XDocument.Load(path);
                innerXmlLinq.Ininial();
#endif
            }
            else
            {
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
                InnerXmlDocment innerXmlDocment = new InnerXmlDocment();
                innerXmlDocment.Document = new XmlDocument();
                innerXmlDocment.Document.Load(path);
                innerXmlDocment.Ininial();
#endif
            }
        }
        public string ToXml()
        {
            if (!IsXmlDocment)
            {
#if !(NET20 || NET30)
                InnerXmlLinq innerXmlLinq = new InnerXmlLinq();
                innerXmlLinq.Document = new XDocument();
                innerXmlLinq.Element = Element;
                innerXmlLinq.Write();
                return innerXmlLinq.Document.Root.IsEmpty? string.Empty : innerXmlLinq.Document.Root.ToString();
#else
                return string.Empty;
#endif

            }
            else
            {
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
                InnerXmlDocment innerXmlDocment = new InnerXmlDocment();
                innerXmlDocment.Document = new XmlDocument();
                innerXmlDocment.Element = Element;
                innerXmlDocment.Write();
                StringBuilder sb = new StringBuilder();
                using (XmlWriter writer = XmlWriter.Create(sb, innerXmlDocment.Settings))
                {
                    innerXmlDocment.Document.Save(writer);
                }
                return sb.ToString();
                //return innerXmlDocment.Document.OuterXml;
#else
                return string.Empty;
#endif
            }
        }
    }
}
