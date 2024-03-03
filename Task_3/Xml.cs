using System.Xml;
using System.Xml.Serialization;

namespace Task_3
{
    internal class Xml
    {
        public const string fileLocation = "../../../dataxml.xml";

        private List<User> data = new();
        private static readonly XmlSerializer serialize = new XmlSerializer(typeof(List<User>));

        public static List<User> Parse(string data)
        {
            List<User> result = new();

            XmlDocument xmlDocument = new();
            xmlDocument.LoadXml(data);
            XmlNodeList nodes = xmlDocument.SelectNodes("//row");

            foreach (XmlNode node in nodes)
            {
                User user = new();

                user.Username = node.SelectSingleNode("username").InnerText;
                user.Score = int.Parse(node.SelectSingleNode("score").InnerText);
                result.Add(user);
            }

            return result;
        }
        public static void Save(string userName, int points)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(fileLocation);
            XmlElement newRow = doc.CreateElement("row");
            XmlElement usernameElement = doc.CreateElement("username");
            usernameElement.InnerText = userName;
            newRow.AppendChild(usernameElement);
            XmlElement scoreElement = doc.CreateElement("score");
            scoreElement.InnerText = points.ToString();
            newRow.AppendChild(scoreElement);
            XmlElement root = doc.DocumentElement;
            XmlNode lastRow = root.LastChild;
            root.InsertAfter(newRow, lastRow);
            doc.Save(fileLocation);
        }
    }
}
