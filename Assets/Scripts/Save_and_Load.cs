using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;


public class Save_and_Load<T>
{
    public Save_and_Load() { }

    public void Save(string s, T info)
    {
        if (s != "")
        {
            FileStream SaveFile = File.Create(@"..\..\SavedTeamFiles\" + s + ".bin");
            XmlSerializer bf = new XmlSerializer(typeof(T));
            bf.Serialize(SaveFile, info);
            SaveFile.Close();
        }
    }

    public T Load(string s)
    {
        T MalleableVar;
        if (s != "")
        {
            FileStream LoadFile = File.OpenRead(@"..\..\SavedTeamFiles\" + s + ".bin");
            XmlSerializer bf = new XmlSerializer(typeof(T));
            MalleableVar = (T)bf.Deserialize(LoadFile);
            LoadFile.Close();
        }
        else
        {
            FileStream LoadFile = File.OpenRead(@"..\..\SavedTeamFiles\Empty.XML");
            XmlSerializer bf = new XmlSerializer(typeof(T));
            MalleableVar = (T)bf.Deserialize(LoadFile);
            LoadFile.Close();
        }
        return MalleableVar;
    }
}
