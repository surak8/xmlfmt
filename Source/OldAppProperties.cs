using System.Text;
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


    public override string ToString() {
        StringBuilder sb = new StringBuilder();


        sb.AppendLine("ConnectionStringEpicor:" + epicorConnStr + " " +
            "ColumnHeaders:" + (headers == null ? "none" : this.headers.Length.ToString()) + " " +
             "AcceptableStates:" + (stateAbbreviations == null ? "none" : this.stateAbbreviations.Length.ToString()) + " " +
                       "Distributors:" + (headers == null ? "none" : this.distributors.Length.ToString()) + ".");
        return base.ToString() + " " + sb.ToString();
    }
}

public class OldColHeaders {
    #region fields
    [XmlElement("Name")]
    public string headerName;

    [XmlArray("Synonyms"), XmlArrayItem("string")]
    public string[] synonyms;
    #endregion
    public override string ToString() {
        StringBuilder sb = new StringBuilder();


        sb.AppendLine("Name:" + headerName + " " +
            "Synonyms:" + (synonyms == null ? "none" : this.synonyms.Length.ToString()) + " " +
         ".");
        return base.ToString() + " " + sb.ToString();
    }
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

    public override string ToString() {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Distributor:" + distributorName+" Group:"+distributorGroup+" Synonyms:"+(blah==null?"none":this.blah.Length.ToString())+".");
        return base.ToString()+" "+sb.ToString();
    }
}