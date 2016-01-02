using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;


namespace SmartTransferServer_V2._0
{

    /// <summary>
    /// Avaible categories..
    /// </summary>
    public enum Categories { MUSIC, IMAGES, VIDEOS, EBOOKS, OTHERS };

    /// <summary>
    /// This class manages the server xml-file.
    /// This file is the configuration-file.
    /// </summary>
    public class XmlManager
    {
        /// <summary>
        /// This is the path of the config path
        /// </summary>
        private const string CONFIG_PATH = "config.xml";
        /// <summary>
        /// The name of the root-node
        /// </summary>
        private const string ROOT_NODE = "DSERVER";
        private const string SERVER_PW_TAG_NAME = "PASSWORD";
        /// <summary>
        /// The xml-tree
        /// </summary>
        XmlDocument xmldoc;
        /// <summary>
        /// Reference on the root element from xmldoc
        /// </summary>
        XmlNode root;
        /// <summary>
        /// Inititialize the XmlManager
        /// </summary>
        public XmlManager()
        {
            this.xmldoc = new XmlDocument();
            if (File.Exists(CONFIG_PATH))
            {
                this.xmldoc.Load(CONFIG_PATH);
                this.root = this.xmldoc.DocumentElement;
            }
            else
            {
                this.root = this.xmldoc.CreateElement(ROOT_NODE);
                xmldoc.AppendChild(root);
                initRootChilds();
            }

        }
        /// <summary>
        /// If there isnt any xml-file(first start)
        /// </summary>
        private void initRootChilds()
        {
            for (int i = 0; i < Enum.GetValues(typeof(Categories)).Length; i++)
            {
                XmlNode child = this.xmldoc.CreateElement(Enum.GetName(typeof(Categories), i));
                this.root.AppendChild(child);
            }
        }
        public void addServerPassword(string pw)
        {
            XmlNode pwChild = this.xmldoc.CreateElement(SERVER_PW_TAG_NAME);
            pwChild.InnerText = pw;
            this.root.AppendChild(pwChild);
        }

        public string readServerPassword()
        {
            XmlNode node = root.SelectSingleNode(SERVER_PW_TAG_NAME);
            return node.InnerText;
        }
        /// <summary>
        /// This method adds a child to a specific category.
        /// </summary>
        /// <param name="category">MUSIC or IMAGES or VIDEOS or EBOOKS or OTHERS</param>
        /// <param name="childValue">string-value</param>
        public void addChildToCategory(Categories category, String childValue)
        {
            XmlNode node = root.SelectSingleNode(category.ToString());
            int childAmount = node.ChildNodes.Count;
            XmlNode nChild = this.xmldoc.CreateElement("value" + childAmount);
            nChild.InnerText = childValue;
            //nChild.
            node.AppendChild(nChild);
        }
        /// <summary>
        /// Get all childs
        /// </summary>
        /// <returns></returns>
        public List<String> getAllChilds()
        {
            List<String> allChilds = new List<string>();
            for (int i = 0; i < Enum.GetValues(typeof(Categories)).Length; i++)
            {
                String category = Enum.GetName(typeof(Categories), i).ToString();
                allChilds.AddRange(getAllChildsFrom((Categories)Enum.Parse(typeof(Categories), category)));
            }
            return allChilds;
        }
        /// <summary>
        /// Get all childs of a category
        /// </summary>
        /// <param name="category">From which category?</param>
        /// <returns>A string-list of childs</returns>
        public List<String> getAllChildsFrom(Categories category)
        {
            List<String> allChilds = new List<string>();
            XmlNode node = root.SelectSingleNode(category.ToString());
            foreach (XmlNode n in node.ChildNodes)
            {
                allChilds.Add(n.InnerText);
            }
            return allChilds;
        }
        /// <summary>
        /// Deletes a child from a category..
        /// </summary>
        /// <param name="category">From which category?</param>
        /// <param name="childValue">Which child?</param>
        public void deleteChildFromCategory(Categories category, String childValue)
        {
            XmlNode node = root.SelectSingleNode(category.ToString());
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                if (node.ChildNodes[i].InnerText == childValue)
                {
                    node.RemoveChild(node.ChildNodes[i]);
                    break;
                }
            }
        }
        /// <summary>
        /// Saves the xml
        /// </summary>
        public void saveXml()
        {
            
            this.xmldoc.Save(CONFIG_PATH);

            
        }

        public void deleteXml()
        {
            
            System.IO.File.Delete(CONFIG_PATH);
            
        }
       

    }
}
