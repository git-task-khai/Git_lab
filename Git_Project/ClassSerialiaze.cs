using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Script.Serialization;

namespace ProjetcGit
{
    public static class ClassSerialiaze
    {
        public static void SerialiazeToJson<T>(ref T inObject, string inFileName)
        {
            try
            {
                JavaScriptSerializer writer = new JavaScriptSerializer();
                System.IO.StreamWriter file = new System.IO.StreamWriter(inFileName);
                file.Write(writer.Serialize(inObject));
                file.Close();
            }
            catch (Exception ex) {}
        }

        public static void DeserializationFromJson<T>(ref T inObject, string inFileName)
        {
            if (System.IO.File.Exists(inFileName))
            {
                JavaScriptSerializer reader = new JavaScriptSerializer();
                System.IO.StreamReader file = new System.IO.StreamReader(inFileName); 
                inObject = reader.Deserialize<T>(file.ReadLine());
                file.Close();
            }
        }
    }
}
