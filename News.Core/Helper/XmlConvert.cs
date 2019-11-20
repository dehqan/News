using System.IO;
using System.Xml.Serialization;

namespace News.Core.Helper
{
    public static class XmlConvert
    {
        public static string SerializeObject<T>(T objectToSerialize) where T : class
        {
            var xmlSerializer = new XmlSerializer(objectToSerialize.GetType());
            using var textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, objectToSerialize);
            return textWriter.ToString();
        }

        public static T DeserializeObject<T>(string input) where T : class
        {
            var ser = new XmlSerializer(typeof(T));
            using var sr = new StringReader(input);
            return (T)ser.Deserialize(sr);
        }
    }
}
