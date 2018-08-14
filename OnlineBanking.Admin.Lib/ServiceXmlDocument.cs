using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using OnlineBanking.Admin.Lib.Model;

namespace OnlineBanking.Admin.Lib
{
    public class ServiceXmlDocument
    {
        public ServiceXmlDocument()
        {

        }
        public ServiceXmlDocument(string pathDocument)
        {
            this.pathDocument = pathDocument;
        }
        private string pathDocument { get; set; }
        public XmlDocument GetDocument()
        {
            XmlDocument doc = new XmlDocument();

            if (!string.IsNullOrEmpty(pathDocument))
            {
                FileInfo file = new FileInfo(pathDocument);
                if (file.Exists)
                {
                    doc.Load(pathDocument);
                    if (doc.HasChildNodes) //или if(doc.DocumentElement!=null)
                        return doc;
                    else
                    {
                        XmlElement root = doc.CreateElement("operators");
                        doc.AppendChild(root);
                        doc.Save(pathDocument);
                        return doc;
                    }
                }
                else
                {
                    using (Stream stream = file.Create())
                    {
                        XmlElement root = doc.CreateElement("operators");
                        doc.AppendChild(root);
                    }
                    doc.Save(pathDocument);
                    return doc;
                }
            }
            else { throw new FileNotFoundException(); }
        }
        public void CreateOperator(Operator oper)
        {
            //1 получить документ
            XmlDocument doc = GetDocument();

            if (ExistOperator(oper.Name) == false)
            {
                #region 2 создать xml для нового оператора
                XmlElement xOper = doc.CreateElement("operator");

                XmlElement xName = doc.CreateElement("name");
                xName.InnerText = oper.Name;
                xOper.AppendChild(xName);
                //<name>Beeline</name>

                XmlElement xPercent = doc.CreateElement("percent");
                xPercent.InnerText = oper.Percent.ToString();
                xOper.AppendChild(xPercent);

                XmlElement xLogo = doc.CreateElement("logo");
                xLogo.InnerText = oper.Logo;
                xOper.AppendChild(xLogo);

                XmlElement xPrefixes = doc.CreateElement("prefixes");
                foreach (Prefix pref in oper.Prefixes)
                {
                    if (ExistPrefixOperator(pref.pref) == false)
                    {
                        XmlElement xPrefix = doc.CreateElement("prefix");
                        xPrefix.InnerText = pref.pref.ToString();
                        xPrefixes.AppendChild(xPrefix);
                        //<prefixes> <prefix>777</prefx>
                    }
                    else Console.WriteLine("префикс "+pref.pref+" уже существует");
                }
                xOper.AppendChild(xPrefixes);
                #endregion

                //3 добавить xml с новым оператором в документ
                doc.DocumentElement.AppendChild(xOper);

                //4 сохранить xml документ
                doc.Save(pathDocument);
            }
            else throw new Exception("оператор " + oper.Name + " уже существует!");
        }

        private bool ExistOperator(string name)
        {
            //1 получить xml документ
            XmlDocument doc = GetDocument();
            //2 вытащить все наименования операторов
            XmlNodeList operators = doc.SelectNodes("operators/operator/name");
            foreach (XmlNode item in operators)
            {
                if (item.InnerText.ToUpper() == name.ToUpper())
                    return true;
            }
            //3 проверить 
            return false;
        }
        private bool ExistPrefixOperator(int prefix)
        {
            //1 получить xml документ
            XmlDocument doc = new XmlDocument();

            //2 вытащить все префиксы операторов
            XmlNodeList operators = doc.SelectNodes("operators/operator/prefixes/prefix");
            foreach (XmlNode item in operators)
            {
                if (item.InnerText == prefix.ToString())
                    return true;
            }
            //3 проверить 
            return false;
        }
    }
}
