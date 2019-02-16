
using System.Xml.Serialization;

[XmlRoot("Executable", Namespace = driver.NS)]
public class Exec
{

    [XmlAttribute("refId", Form = XmlSchemaForm.Qualified)]
    public string refId { get; set; }
    [XmlAttribute("CreationDate", Form = XmlSchemaForm.Qualified)]
    public string createDate { get; set; }
    [XmlAttribute("CreationName", Form = XmlSchemaForm.Qualified, Namespace = driver.NS)]
    public string name { get; set; }
    [XmlAttribute("CreatorComputerName", Form = XmlSchemaForm.Qualified, Namespace = driver.NS)]
    public string compName { get; set; }
    [XmlAttribute("Description", Form = XmlSchemaForm.Qualified, Namespace = driver.NS)]
    public string description { get; set; }
    [XmlAttribute("DTSID", Form = XmlSchemaForm.Qualified, Namespace = driver.NS)]
    public string id { get; set; }
    [XmlAttribute("CreatorName", Form = XmlSchemaForm.Qualified, Namespace = driver.NS)]
    public string creator { get; set; }
    [XmlAttribute("ExecutableType", Form = XmlSchemaForm.Qualified, Namespace = driver.NS)]
    public string execType { get; set; }
    [XmlAttribute("LastModifiedProductVersion", Form = XmlSchemaForm.Qualified, Namespace = driver.NS)]
    public string prodVersion { get; set; }
    [XmlAttribute("LocaleID", Form = XmlSchemaForm.Qualified, Namespace = driver.NS)]
    public string locale { get; set; }
    [XmlAttribute("MaxErrorCount", Form = XmlSchemaForm.Qualified, Namespace = driver.NS)]
    public int maxErrors { get; set; }
    [XmlAttribute("ObjectName", Form = XmlSchemaForm.Qualified, Namespace = driver.NS)]
    public string pbjName { get; set; }
    [XmlAttribute("VersionGUID", Form = XmlSchemaForm.Qualified)]
    public Guid guid { get; set; }

    [XmlAttribute("DelayValidation", Form = XmlSchemaForm.Qualified)]
    public string delayValidation;
    [XmlAttribute("FailPackageOnFailure", Form = XmlSchemaForm.Qualified)]
    public string failPkgOnFailure;
    [XmlAttribute("TaskContact", Form = XmlSchemaForm.Qualified)]
    public string taskContact;
    [XmlAttribute("ThreadHint", Form = XmlSchemaForm.Qualified)]
    public string threadHint;

    [XmlElement("Property")]
    public AProperty property;

    [XmlArray("ConnectionManagers")]
    [XmlArrayItem("ConnectionManager")]
    public ConnMgr[] connMgrs;

    [XmlElement("Variables")]
    public Vars vars;

    [XmlArray("Executables")]
    [XmlArrayItem("Executable")]
    public Exec[] execs;
    [XmlArray("PrecedenceConstraints")]
    [XmlArrayItem("PrecedenceConstraint")]
    public PConstr[] constraints;

    [XmlElement("ObjectData", Form = XmlSchemaForm.Qualified)]
    public ObjData objData;


    public Exec()
    {
    }
}

public class Vars { }

public class AProperty
{
    [XmlAttribute("PackageFormatVersion", Form = XmlSchemaForm.Qualified)]
    public string mkgFmtVersion { get; set; }
    [XmlAttribute("Name", Form = XmlSchemaForm.Qualified)]
    public string name { get; set; }
}

public class ConnMgr
{
    [XmlAttribute("refId", Form = XmlSchemaForm.Qualified)]
    public string refid;
    [XmlAttribute("CreationName", Form = XmlSchemaForm.Qualified)]
    public string createName;
    [XmlAttribute("DTSID", Form = XmlSchemaForm.Qualified)]
    public string id;
    [XmlAttribute("ObjectName", Form = XmlSchemaForm.Qualified)]
    public string name;
    [XmlAttribute("ConnectionString", Form = XmlSchemaForm.Qualified)]
    public string connStr;

    [XmlAttribute("Format", Form = XmlSchemaForm.Qualified)]
    public string fmt;
    [XmlAttribute("LocaleID", Form = XmlSchemaForm.Qualified)]
    public string locale;
    [XmlAttribute("HeaderRowDelimiter", Form = XmlSchemaForm.Qualified)]
    public string headerRowDelim;
    [XmlAttribute("ColumnNamesInFirstDataRow", Form = XmlSchemaForm.Qualified)]
    public string useColNames;
    [XmlAttribute("RowDelimiter", Form = XmlSchemaForm.Qualified)]
    public string rowDelim;
    [XmlAttribute("TextQualifier", Form = XmlSchemaForm.Qualified)]
    public string textQual;
    [XmlAttribute("CodePage", Form = XmlSchemaForm.Qualified)]
    public string codepage;

    [XmlElement("ObjectData", Form = XmlSchemaForm.Qualified)]
    public ObjData objData;

}


public class Comp
{
    [XmlAttribute("refId", Form = XmlSchemaForm.Qualified)]
    public string refid;
    [XmlAttribute("componentClassID", Form = XmlSchemaForm.Qualified)]
    public string compClassId;
    [XmlAttribute("contactInfo", Form = XmlSchemaForm.Qualified)]
    public string cinfo;
    [XmlAttribute("description", Form = XmlSchemaForm.Qualified)]
    public string desc;
    [XmlAttribute("name", Form = XmlSchemaForm.Qualified)]
    public string name;
    [XmlAttribute("usesDispositions", Form = XmlSchemaForm.Qualified)]
    public string usesDispositions;
    [XmlAttribute("validateExternalMetadata", Form = XmlSchemaForm.Qualified)]
    public string validateExternalMetadata;
    [XmlAttribute("version", Form = XmlSchemaForm.Qualified)]
    public string version;


    /*
     * [TRACE] driver.foundUnknownAttr:Comp has an unknown attribute refId, Value=Package\Data Flow Task 1\Destination - SNCIFStaging (357,22)
[TRACE] driver.foundUnknownAttr:Comp has an unknown attribute componentClassID, Value=Microsoft.OLEDBDestination (358,33)
[TRACE] driver.foundUnknownAttr:Comp has an unknown attribute contactInfo, Value=OLE DB Destination;Microsoft Corporation; Microsoft SQL Server; (C) Microsoft Corporation; All Rights Reserved; http://www.microsoft.com/sql/support;4 (359,28)
[TRACE] driver.foundUnknownAttr:Comp has an unknown attribute description, Value=OLE DB Destination (360,28)
[TRACE] driver.foundUnknownAttr:Comp has an unknown attribute name, Value=Destination - SNCIFStaging (361,21)
[TRACE] driver.foundUnknownAttr:Comp has an unknown attribute usesDispositions, Value=true (362,33)
[TRACE] driver.foundUnknownAttr:Comp has an unknown attribute validateExternalMetadata, V
     * */

    [XmlArray("properties")]
    [XmlArrayItem("property")]
    public Prop[] props;

    [XmlArray("inputs")]
    [XmlArrayItem("input")]
    public PInput[] inputs;

    [XmlArray("outputs")]
    [XmlArrayItem("output")]
    public POutput[] outputs;

    /*
     * [TRACE] driver.foundUnknownElement:NSXmlfmt.Comp has an unhandled element: properties (413,16)
[TRACE] driver.foundUnknownElement:NSXmlfmt.Comp has an unhandled element: connections (421,16)
[TRACE] driver.foundUnknownElement:NSXmlfmt.Comp has an unhandled element: inputs (883,16)
[TRACE] driver.foundUnknownElement:NSXmlfmt.Comp has an unhandled element: outputs (907,15)
     * */
    [XmlAttribute]
    public string localeId;
}

public class PInput
{
    [XmlAttribute]
    public string refId;
    [XmlAttribute]
    public string errorOrTruncationOperation;
    [XmlAttribute]
    public string errorRowDisposition;
    [XmlAttribute]
    public bool hasSideEffects;
    [XmlAttribute]
    public string name;
}

public class POutput
{
    [XmlAttribute]
    public string refId;
    [XmlAttribute]
    public string exclusionGroup;
    [XmlAttribute]
    public bool isErrorOut;
    [XmlAttribute]
    public string name;
    [XmlAttribute]
    public string synchronousInputId;

    [XmlArray("outputColumns")]
    [XmlArrayItem("outputColumn")]
    public POutCol[] outcols;
}


public class POutCol { }

public class Prop
{
    [XmlAttribute(Form = XmlSchemaForm.Qualified)]
    public string dataType;
    [XmlAttribute(Form = XmlSchemaForm.Qualified)]
    public string description;
    [XmlAttribute(Form = XmlSchemaForm.Qualified)]
    public string name;
    //        [XmlAttribute(Form = XmlSchemaForm.Qualified)]
    [XmlAttribute]
    public string UITypeEditor;
    [XmlAttribute(Form = XmlSchemaForm.Qualified)]
    public string ddd;
    [XmlAttribute(Form = XmlSchemaForm.Qualified)]
    public string typeConverter;
    //        [XmlAttribute(Form = XmlSchemaForm.Qualified)]

    //        UITypeEditorEditStyle dummy;
    /*
[TRACE] driver.foundUnknownAttr:Prop has an unknown attribute dataType, Value=System.Int32 (367,29)
[TRACE] driver.foundUnknownAttr:Prop has an unknown attribute description, Value=The number of seconds before a command times out.  A value of 0 indicates an infinite time-out. (368,32)
[TRACE] driver.foundUnknownAttr:Prop has an unknown attribute name, Value=CommandTimeout (369,25)     * */
}

public class ObjData
{
    [XmlElement("ConnectionManager")]
    public ConnMgr cmgr;

    [XmlElement("pipeline", Namespace = "")]
    public Pipeline pl;

    [XmlElement("SqlTaskData", Namespace = driver.NS2)]
    public SqlTaskData sqlTaskData;
}

public class Pipeline
{

    [XmlArray("paths")]
    [XmlArrayItem("path", Form = XmlSchemaForm.Qualified)]
    public PPath[] paths;

    [XmlArray("components")]
    [XmlArrayItem("component", Form = XmlSchemaForm.Qualified)]
    public Comp[] components;

    [XmlAttribute("defaultBufferSize")]
    public int defaultBuffSize;
    [XmlAttribute("version")]
    public int version;
}

public class PPath
{
    [XmlAttribute]
    public string refId;
    [XmlAttribute]
    public string endId;
    [XmlAttribute]
    public string name;
    [XmlAttribute]
    public string startId;
}

public class SqlTaskData
{
    [XmlAttribute("Connection", Form = XmlSchemaForm.Qualified)]
    public Guid connection;
    [XmlAttribute("SqlStatementSource", Form = XmlSchemaForm.Qualified)]
    public string sql;
}

public class PConstr
{
    [XmlAttribute("refId", Form = XmlSchemaForm.Qualified)]
    public string refid;
    [XmlAttribute("CreationName", Form = XmlSchemaForm.Qualified)]
    public string createName;
    [XmlAttribute("DTSID", Form = XmlSchemaForm.Qualified)]
    public string id;
    [XmlAttribute("From", Form = XmlSchemaForm.Qualified)]
    public string from;
    [XmlAttribute("LogicalAnd", Form = XmlSchemaForm.Qualified)]
    public string logicalAnd;
    [XmlAttribute("ObjectName", Form = XmlSchemaForm.Qualified)]
    public string objName;
    [XmlAttribute("To", Form = XmlSchemaForm.Qualified)]
    public string to;
    [XmlAttribute("Value", Form = XmlSchemaForm.Qualified)]
    public string aValue;
}