using System.Xml.Serialization;

[XmlRoot("ApplicationProperties")]
public class OldAppProps {
    
    [XmlArray("ColumnHeaders"),XmlArrayItem("ColumnHeader")]
    public OldColHeaders[] headers;

    [XmlArray("AcceptableStates"), XmlArrayItem("string")]
    public string[] stateAbbreviations;

    [XmlElement("ConnectionStringEpicor")]
    public string epicorConnStr;

    [XmlArray("Distributors"), XmlArrayItem("Distributor")]
    public OldDistrib[] distributors;
}

public class OldColHeaders {
    [XmlElement("Name")]
    public string headerName;

    [XmlArray("Synonyms"), XmlArrayItem("string")]
    public string[] synonyms;
}


public class OldDistrib {
    [XmlElement("Name")]
    public string distributorName;
    [XmlElement("EpicorId")]
    public int epicorId;
    [XmlElement("Group")]
    public int distributorGroup;
    [XmlArray("Synonyms"), XmlArrayItem("string")]
    public string[] blah;
}