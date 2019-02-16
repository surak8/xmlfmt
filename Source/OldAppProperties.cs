using System.Xml.Serialization;

/// <summary>Seb's current XML layout.</summary>
/// <remarks>
/// <para>All properties are exposed as <strong>XmlElement</strong>, 
/// rather than <strong>XmlAttribute</strong>.</para>
/// <para>please note: I have chosen to name the classes differently, and have 
/// properly cased the variable names, but the <see cref="XmlArrayAttribute"/> and
/// <see cref="XmlArrayItemAttribute"/> attributes allow me to reproduce the (ugly) xml-names
/// that Seb desires.</para>
/// </remarks>
[XmlRoot("ApplicationProperties")]
public class OldAppProps {
    #region fields
    [XmlArray("ColumnHeaders"), XmlArrayItem("ColumnHeader")]
    public OldColHeaders[] headers;

    [XmlArray("AcceptableStates"), XmlArrayItem("string")]
    public string[] stateAbbreviations;

    [XmlElement("ConnectionStringEpicor")]
    public string epicorConnStr;

    [XmlArray("Distributors"), XmlArrayItem("Distributor")]
    public OldDistrib[] distributors;
    #endregion
}

public class OldColHeaders {
    #region fields
    [XmlElement("Name")]
    public string headerName;

    [XmlArray("Synonyms"), XmlArrayItem("string")]
    public string[] synonyms; 
    #endregion
}

public class OldDistrib {
    #region fields
    [XmlElement("Name")]
    public string distributorName;
    [XmlElement("EpicorId")]
    public int epicorId;
    [XmlElement("Group")]
    public int distributorGroup;
    [XmlArray("Synonyms"), XmlArrayItem("string")]
    public string[] blah; 
    #endregion
}