using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace OnlineBanking.Customer.Lib.Model
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
                        XmlElement root = doc.CreateElement("user");
                        doc.AppendChild(root);
                        doc.Save(pathDocument);
                        return doc;
                    }
                }
                else
                {
                    using (Stream stream = file.Create())
                    {
                        XmlElement root = doc.CreateElement("user");
                        doc.AppendChild(root);
                    }
                    doc.Save(pathDocument);
                    return doc;
                }
            }
            else { throw new FileNotFoundException(); }
        }

        public void CreateUser (User user)
        {
            string guidUser = Guid.NewGuid().ToString();
            pathDocument = pathDocument + @"\" + guidUser + ".xml";

            XmlDocument doc = GetDocument();
            XmlElement xUser = doc.CreateElement("user");

            XmlElement xUserID = doc.CreateElement("userId");
            xUserID.InnerText = Guid.NewGuid().ToString();
            xUser.AppendChild(xUserID);

            XmlElement xEmail = doc.CreateElement("email");
            xEmail.InnerText = user.Email;
            xUser.AppendChild(xEmail);

            XmlElement xLogin= doc.CreateElement("login");
            xLogin.InnerText = user.Login;
            xUser.AppendChild(xLogin);

            XmlElement xPassword = doc.CreateElement("password");
            xPassword.InnerText = user.Password;
            xUser.AppendChild(xPassword);

            doc.DocumentElement.AppendChild(xUser);
            doc.Save(pathDocument);




        }
    }
}
