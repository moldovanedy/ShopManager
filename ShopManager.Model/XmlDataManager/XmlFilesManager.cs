using System.Xml;

namespace ShopManager.Controller.XmlDataManager
{
    public static class XmlFilesManager
    {
        public static bool WriteXml(XmlDocument xmlDocument, string path)
        {
            try
            {
                xmlDocument.Save(path);
                return true;
            }
            catch (XmlException ex)
            {
                Logger.LogError(ex.Message);
                return false;
            }
        }

        public static XmlDocument ReadXml(string path)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(path);
                return xmlDocument;
            }
            catch (XmlException ex)
            {
                Logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
