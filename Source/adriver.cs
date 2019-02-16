//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Design;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NSXmlfmt {

    public class driver {
        public const string PREFIX = "DTS";
        public const string NS = "www.microsoft.com/SqlServer/Dts";

        public const string PREFIX2 = "SQLTask";
        public const string NS2 = "www.microsoft.com/sqlserver/dts/tasks/sqltask";

        [STAThread()]
        public static void Main(string[] args) {
            int exitCode = 0;
            string filename;
            XmlSerializer xs;

            Logger.logDebug = false;
            if (args.Length < 1) {
                Console.Error.WriteLine("missing xml-filename.");
                exitCode = 1;
            } else {
                if (!File.Exists(filename = args[0])) {
                    Console.Error.WriteLine("file '" + filename + "' not found.");
                    exitCode = 2;
                } else {
                    XmlWriterSettings xws;
                    object anObj;
                    XmlReaderSettings xrs;

                    try {
                        xrs = new XmlReaderSettings();
                        xrs.ConformanceLevel = ConformanceLevel.Fragment;
                        xrs.IgnoreComments = true;
                        xrs.IgnoreWhitespace = true;
                        NameTable nt = new NameTable();

                        XmlNamespaceManager xnsmgr = new XmlNamespaceManager(nt);
                        xnsmgr.AddNamespace(PREFIX, NS);
                        xnsmgr.AddNamespace(PREFIX2, NS2);
                        XmlParserContext ctx = new XmlParserContext(null, xnsmgr, null, XmlSpace.None);

                        using (XmlReader xr = XmlReader.Create(filename, xrs, ctx)) {

                            /*
                            XmlAttributes atts = new XmlAttributes();
                            // Set to true to preserve namespaces, or false to ignore them.
                            atts.Xmlns = true;

                            XmlAttributeOverrides xover = new XmlAttributeOverrides();
                            // Add the XmlAttributes and specify the name of the 
                            // element containing namespaces.
                            xover.Add(typeof(Exec), "myNamespaces", atts);
                            //                            xover.Add(typeof(Execs), "myNamespaces", atts);
                            //                          xover.Add(typeof(Exec), "myNamespaces", atts);
                            xs = new XmlSerializer(typeof(Exec), xover);
                            */
                            xs = new XmlSerializer(typeof(Exec));

                            //      xsn.Add("DTS", "www.microsoft.com/SqlServer/Dts");
                            xs.UnknownAttribute += foundUnknownAttr;
                            xs.UnknownElement += foundUnknownElement;
                            xs.UnknownNode += foundUnknownNode;
                            xs.UnreferencedObject += foundRefObj;
                            //             anObj = xs.Deserialize(xr, xde);
                            anObj = xs.Deserialize(xr);
                            xs.UnknownAttribute -= foundUnknownAttr;
                            xs.UnknownElement -= foundUnknownElement;
                            xs.UnknownNode -= foundUnknownNode;
                            xs.UnreferencedObject -= foundRefObj;
                            xs = null;
                        }
                    } catch (Exception ex) {
                        Trace.WriteLine(decomposeMessage(ex));
                        throw;
                    }
                }
            }
            Environment.Exit(exitCode);
        }

        static string decomposeMessage(Exception ex) {
            StringBuilder sb = new StringBuilder();
            Exception ex0 = ex;

            sb.AppendLine(ex.Message);
            ex0 = ex.InnerException;
            while (ex0 != null) {
                sb.AppendLine("[" + ex0.GetType().Name + "] " + ex0.Message);
                ex0 = ex0.InnerException;
            }
            return sb.ToString();
        }

        static void foundRefObj(object sender, UnreferencedObjectEventArgs e) {
            Logger.log(MethodBase.GetCurrentMethod());
        }

        static void foundUnknownNode(object sender, XmlNodeEventArgs e) {
            //         Logger.log(MethodBase.GetCurrentMethod());
            //         Logger.log(MethodBase.GetCurrentMethod(),"unhandled "+e.NodeType+":"+e.LocalName );
        }

        static void foundUnknownElement(object sender, XmlElementEventArgs e) {
            Logger.log(MethodBase.GetCurrentMethod(), e.ObjectBeingDeserialized.GetType().FullName + " has an unhandled element: " + e.Element.Name + " (" + e.LineNumber + "," + e.LinePosition + ")");
        }

        static void foundUnknownAttr(object sender, XmlAttributeEventArgs e) {
            Logger.log(MethodBase.GetCurrentMethod(), e.ObjectBeingDeserialized.GetType().Name + " has an unknown attribute " + e.Attr.Name + ", Value=" + e.Attr.Value + " (" + e.LineNumber + "," + e.LinePosition + ")");
        }
    }

    /// <summary>logging class.</summary>
    public static class Logger {

        #region fields
        /// <summary>controls logging-style.</summary>
        public static bool logDebug = true;

        /// <summary>controls logging-style.</summary>
        public static bool logUnique = false;

        /// <summary>messages written.</summary>
        static readonly List<string> msgs = new List<string>();
        #endregion fields

        #region methods
        #region logging-methods
        /// <summary>log a message</summary>
        /// <param name="msg"/>
        /// <seealso cref="Debug"/>
        /// <seealso cref="Trace"/>
        /// <seealso cref="logDebug"/>
        /// <seealso cref="logUnique"/>
        /// <seealso cref="msgs"/>
        public static void log(string msg) {
            if (logUnique) {
                if (msgs.Contains(msg))
                    return;
                msgs.Add(msg);
            }
            if (logDebug)
#if DEBUG
                Debug.Print("[DEBUG] " + msg);
#endif

#if TRACE
            Trace.WriteLine("[TRACE] " + msg);
#endif
        }

        /// <summary>log a message</summary>
        /// <param name="mb"/>
        /// <seealso cref="makeSig"/>
        /// <seealso cref="log(MethodBase,string)"/>
        public static void log(MethodBase mb) {
            log(mb, string.Empty);
        }

        /// <summary>log a message</summary>
        /// <param name="mb"/>
        /// <param name="msg"/>
        /// <seealso cref="makeSig"/>
        /// <seealso cref="log(MethodBase,string)"/>
        public static void log(MethodBase mb, string msg) {
            log(makeSig(mb) + ":" + msg);
        }

        public static void log(MethodBase mb, Exception ex) {
            log(makeSig(mb) + ":" + ex.Message);
        }
        #endregion logging-methods

        #region misc. methods
        /// <summary>create a method-signature.</summary>
        /// <returns></returns>
        public static string makeSig(MethodBase mb) {
            return mb.ReflectedType.Name + "." + mb.Name;
        }
        #endregion misc. methods
        #endregion methods
    }

    [XmlRoot("Executable", Namespace = driver.NS)]
    public class Exec {

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


        public Exec() {
        }
    }

    public class Vars { }

    public class AProperty {
        [XmlAttribute("PackageFormatVersion", Form = XmlSchemaForm.Qualified)]
        public string mkgFmtVersion { get; set; }
        [XmlAttribute("Name", Form = XmlSchemaForm.Qualified)]
        public string name { get; set; }
    }

    public class ConnMgr {
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


    public class Comp {
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

    public class PInput {
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

    public class POutput {
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

    public class Prop {
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

    public class ObjData {
        [XmlElement("ConnectionManager")]
        public ConnMgr cmgr;

        [XmlElement("pipeline", Namespace = "")]
        public Pipeline pl;

        [XmlElement("SqlTaskData", Namespace = driver.NS2)]
        public SqlTaskData sqlTaskData;
    }

    public class Pipeline {

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

    public class PPath {
        [XmlAttribute]
        public string refId;
        [XmlAttribute]
        public string endId;
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public string startId;
    }

    public class SqlTaskData {
        [XmlAttribute("Connection", Form = XmlSchemaForm.Qualified)]
        public Guid connection;
        [XmlAttribute("SqlStatementSource", Form = XmlSchemaForm.Qualified)]
        public string sql;
    }

    public class PConstr {
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
}