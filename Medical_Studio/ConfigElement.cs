using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Medical_Studio
{
    public class ConfigElement
    {
        public string ConfigName { get; set; } = "";
        public string ConfigPath { get; set; } = "";

        public static readonly string DictionaryFilePattern = "Configs_{0}.xml";

        public static Dictionary<string, ConfigElement> LoadConfigsDictionary(string filename, out string selectedConfig)
        {
            XmlDocument xmlDocument = new XmlDocument();

            if (!System.IO.File.Exists(filename))
                throw new Exception("Xml file not exists!");

            xmlDocument.Load(filename);

            XmlElement xRoot = xmlDocument.DocumentElement;

            Dictionary<string, ConfigElement> dict = GetEmptyDictionary();// new Dictionary<string, ConfigElement>();
            selectedConfig = "0_0";

            if(xRoot != null)
            {
                selectedConfig = xRoot.GetAttribute("SelectedConfig") ?? "0_0";

                foreach(XmlElement xNode in xRoot)
                {
                    try
                    {
                        XmlNode xAttr = xNode.Attributes.GetNamedItem("Key");
                        if (xAttr != null && dict.ContainsKey(xAttr.Value ?? ""))
                        {
                            ConfigElement confElement = dict[xAttr.Value];

                            xAttr = xNode.Attributes.GetNamedItem("ConfigPath");
                            if(xAttr != null)
                            {
                                confElement.ConfigPath = xAttr.Value ?? "";
                            }
                            xAttr = xNode.Attributes.GetNamedItem("ConfigName");
                            if (xAttr != null)
                            {
                                confElement.ConfigName = xAttr.Value ?? "";
                            }
                        }
                    }
                    catch (Exception) { }
                }
            }

            return dict;
        }

        public static void SaveConfigsDictionary(Dictionary<string, ConfigElement> dictionary, string filename, string selectedConfig)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "utf-8", string.Empty));
            XmlElement xRoot = xmlDocument.CreateElement("configs");
            XmlAttribute xAttrSelectedConfig = xmlDocument.CreateAttribute("SelectedConfig");
            xAttrSelectedConfig.Value = selectedConfig;
            xRoot.Attributes.Append(xAttrSelectedConfig);
            xmlDocument.AppendChild(xRoot);

            foreach(string key in dictionary.Keys)
            {
                XmlNode xNode = xmlDocument.CreateNode(XmlNodeType.Element, "config", xmlDocument.NamespaceURI);
                XmlAttribute xAttr = xmlDocument.CreateAttribute("Key");
                xAttr.Value = key;
                xNode.Attributes.Append(xAttr);

                xAttr = xmlDocument.CreateAttribute("ConfigName");
                xAttr.Value = dictionary[key].ConfigName;
                xNode.Attributes.Append(xAttr);

                xAttr = xmlDocument.CreateAttribute("ConfigPath");
                xAttr.Value = dictionary[key].ConfigPath;
                xNode.Attributes.Append(xAttr);

                xRoot.AppendChild(xNode);
            }

            System.IO.FileInfo fi = new System.IO.FileInfo(filename);
            fi.Directory.Create();
            xmlDocument.Save(filename);
        }

        public static Dictionary<string, ConfigElement> GetEmptyDictionary()
        {
            Dictionary<string, ConfigElement> dict = new Dictionary<string, ConfigElement>();

            string[] defaultNames = new string[] { "Phaco", "Vitreo", "User" };
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    ConfigElement configElement = new ConfigElement();
                    configElement.ConfigName = String.Format("{0} {1}", defaultNames[i], j);
                    configElement.ConfigPath = "";
                    dict.Add(String.Format("{0}_{1}", i, j), configElement);
                }
            }

            return dict;
        }
    }


}
