using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using NSXmlfmt;

public class driver {
    [STAThread()]
    public static void Main(string[] args) {
        int exitCode = 0;
        string filename, inFile, outFile;
        //NewAppProps nap;

        Logger.logDebug = false;
        if (args.Length < 1) {
            Console.Error.WriteLine("missing xml-filename.");
            exitCode = 1;
        } else {
            if (!File.Exists(filename = args[0])) {
                Console.Error.WriteLine("file '" + filename + "' not found.");
                exitCode = 2;
            } else {
                inFile = Path.GetFullPath(filename);
                outFile = Path.Combine(Path.GetDirectoryName(inFile), "New" + Path.GetFileName(inFile));
                Console.Out.WriteLine("reading: " + inFile);
                var avar = inferSchema(inFile);
                //var anObj = readXmlFile<OldAppProps>(filename);
                //if (anObj != null) {
                //    nap = new NewAppProps(anObj);
                //    Console.Out.WriteLine("re-writing " + outFile + " with parameters.");
                //    string tet = writeNewXml(outFile, nap);
                //    Trace.WriteLine("RESULT:=" + Environment.NewLine + tet + Environment.NewLine);
                //}
            }
        }
        Environment.Exit(exitCode);
    }

    static XmlSchema inferSchema(string inFile) {
        XmlSchemaSet xss;
        XmlSchemaInference xsi = new XmlSchemaInference();
        CodeCompileUnit ccu;
        CodeNamespace ns;
        StringBuilder sb;
        CodeDomProvider cdp;
        CodeGeneratorOptions opts;

        cdp = new Microsoft.CSharp.CSharpCodeProvider();
        opts = new CodeGeneratorOptions();
        opts.BlankLinesBetweenMembers = false;
        opts.ElseOnClosing = true;
        using (XmlReader xr = XmlReader.Create(inFile)) {
            xss = xsi.InferSchema(xr);
            foreach (XmlSchema xs in xss.Schemas()) {
                ccu = new CodeCompileUnit();
                ccu.Namespaces.Add(ns = new CodeNamespace());
                iterateThroughSchema(xs, ns);
                using (StringWriter sw = new StringWriter(sb = new StringBuilder())) {
                    cdp.GenerateCodeFromCompileUnit(ccu, sw, opts);
                }
                Trace.WriteLine(sb.ToString());
                sb.Length = 0;
            }
        }
        Trace.WriteLine("here");
        return null;
    }

    static void iterateThroughSchema(XmlSchema xs, CodeNamespace ns) {
        foreach (XmlSchemaObject anobj in xs.Items) {
            if (anobj is XmlSchemaElement) {
                Trace.IndentLevel++;
                processElement(anobj as XmlSchemaElement, ns, null);
                Trace.IndentLevel--;
            } else
                Trace.WriteLine("found: " + anobj.GetType().FullName);
        }
    }

    static void processElement(XmlSchemaElement xse, CodeNamespace ns, CodeTypeDeclaration ctdParent) {
        CodeTypeDeclaration ctd;
        CodeMemberField f;
        CodeTypeReference ctrString, ctrXmlElement;

        Trace.WriteLine("found: " + xse.Name + ", Type=" + xse.SchemaType + ", Min=" + xse.MinOccurs + ", Max=" + xse.MaxOccurs);
        if (xse.SchemaType != null) {
            if (xse.SchemaType is XmlSchemaComplexType) {
                ns.Types.Add(ctd = new CodeTypeDeclaration("Class" + xse.Name));
                Trace.IndentLevel++;
                handleComplexType(xse.SchemaType as XmlSchemaComplexType, ns, ctd);
                Trace.IndentLevel--;
            } else
                Trace.WriteLine("unhandled: " + xse.SchemaType.GetType().FullName);
        } else if (string.Compare(xse.SchemaTypeName.Name, "string") == 0) {
            Trace.WriteLine("schema-type-name:" + xse.SchemaTypeName);
            ctrString = new CodeTypeReference(typeof(string));
            ctrXmlElement = new CodeTypeReference(typeof(XmlElementAttribute));
            ctdParent.Members.Add(f = new CodeMemberField(ctrString, "_" + xse.Name.ToLower()));

            f.CustomAttributes.Add(
                new CodeAttributeDeclaration(
                    new CodeTypeReference("XmlElement"),
                    new CodeAttributeArgument(new CodePrimitiveExpression(xse.Name))));
            f.Attributes = MemberAttributes.Public;
        } else {
            Trace.WriteLine("schema-type-name:" + xse.SchemaTypeName);
        }
    }

    static void handleComplexType(XmlSchemaComplexType xsct, CodeNamespace ns, CodeTypeDeclaration ctdParent) {
        if (xsct.Particle is XmlSchemaSequence) {
            Trace.IndentLevel++;
            handleSequence(xsct.Particle as XmlSchemaSequence, ns, ctdParent);
            Trace.IndentLevel--;
        } else
            Trace.WriteLine("unhandled: " + xsct.Particle.GetType().FullName);
    }

    static void handleSequence(XmlSchemaSequence xss, CodeNamespace ns, CodeTypeDeclaration ctdParent) {
        Trace.WriteLine("have " + xss.GetType().FullName + " Min=" + xss.MinOccurs + ", Max=" + xss.MaxOccurs);
        foreach (XmlSchemaObject xso in xss.Items) {
            if (xso is XmlSchemaElement) {
                Trace.IndentLevel++;
#warning "fix this"
                processElement(xso as XmlSchemaElement, ns, ctdParent);
                Trace.IndentLevel--;
            } else
                Trace.WriteLine("found: " + xso.GetType().FullName);
        }
    }
}