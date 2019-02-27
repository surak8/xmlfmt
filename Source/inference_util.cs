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
        CompilerParameters cp;
        int asmNo = 0;
        string tmp;

        cdp = new Microsoft.CSharp.CSharpCodeProvider();
        opts = new CodeGeneratorOptions();
        opts.BlankLinesBetweenMembers = false;
        opts.ElseOnClosing = true;
        using (XmlReader xr = XmlReader.Create(inFile)) {
            xss = xsi.InferSchema(xr);
            cp = new CompilerParameters();

            cp.IncludeDebugInformation = true;
            cp.WarningLevel = 4;
            cp.GenerateExecutable = false;
            cp.GenerateInMemory = true;
            cp.ReferencedAssemblies.Add("System.Xml.dll");
            //cp.CoreAssemblyFileName = "riktest" + (asmNo++).ToString() + ".dll";
            //cp.


            foreach (XmlSchema xs in xss.Schemas()) {
                ccu = new CodeCompileUnit();
                ccu.Namespaces.Add(ns = new CodeNamespace());
                ns.Imports.AddRange(
                    new CodeNamespaceImport[] {
                        new CodeNamespaceImport("System"),
                        new CodeNamespaceImport("System.Xml"),
                        new CodeNamespaceImport("System.Xml.Serialization"),
                });
                iterateThroughSchema(xs, ns);
                using (StringWriter sw = new StringWriter(sb = new StringBuilder())) {
                    cdp.GenerateCodeFromCompileUnit(ccu, sw, opts);
                }
                Trace.WriteLine(sb.ToString());
                sb.Length = 0;
                try {
                    var avar = cdp.CompileAssemblyFromDom(cp, ccu);
                    if (avar.NativeCompilerReturnValue != 0) {
                        foreach (var avar2 in avar.Errors)
                            sb.AppendLine(avar2.ToString());
                        sb.AppendLine();
                        Trace.WriteLine(sb.ToString());
                        Console.Error.WriteLine(sb.ToString());
                        sb.Length = 0;
                    }else {
                        var avar3=System.Reflection.Assembly.Load(avar.CompiledAssembly.FullName);
                        Trace.WriteLine("here");
                    }
                } catch (Exception ex) {
                    Trace.WriteLine(tmp=decomposeException(ex));
                    Console.Error.WriteLine(tmp);
                }
            }
        }
        Trace.WriteLine("here");
        return null;
    }

      static string decomposeException(Exception ex) {
        StringBuilder sb = new StringBuilder();
        Exception ex0 = ex;

        while (ex0 != null) {
            sb.AppendLine("[" + ex.GetType().Name + "] " + ex0.Message);
            ex0 = ex0.InnerException;
        }
        return sb.ToString();
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
        string className,fldName;

        Trace.WriteLine("found: " + xse.Name + ", Type=" + xse.SchemaType + ", Min=" + xse.MinOccurs + ", Max=" + xse.MaxOccurs);
        if (xse.SchemaType != null) {
            if (xse.SchemaType is XmlSchemaComplexType) {
                ctd = findClassNamed(className = "Class" + xse.Name,ns);
                if (ctdParent != null) {
                    if (!haveFieldNamed(fldName = "_" + xse.Name.ToLower(), ctdParent.Members)) {
                        Trace.WriteLine("*** Adding field " + fldName + " to class " + className+".");
                        ctdParent.Members.Add(f = new CodeMemberField(ctd.Name, fldName));
                        f.Comments.Add(new CodeCommentStatement("check this", true));
                        f.Attributes = MemberAttributes.Public;
                        f.CustomAttributes.Add(
                            new CodeAttributeDeclaration("XmlElement",
                            new CodeAttributeArgument(new CodePrimitiveExpression(xse.Name))));
                    }
                }
                Trace.IndentLevel++;
                handleComplexType(xse.SchemaType as XmlSchemaComplexType, ns, ctd);
                Trace.IndentLevel--;
            } else
                Trace.WriteLine("unhandled: " + xse.SchemaType.GetType().FullName);
        } else if (string.Compare(xse.SchemaTypeName.Name, "string") == 0) {
            Trace.WriteLine("schema-type-name:" + xse.SchemaTypeName);
            if (!haveFieldNamed(fldName= "_" + xse.Name.ToLower(), ctdParent.Members)) {
                ctrString = new CodeTypeReference(typeof(string));
                ctrXmlElement = new CodeTypeReference(typeof(XmlElementAttribute));
                ctdParent.Members.Add(f = new CodeMemberField(ctrString, fldName));

                f.CustomAttributes.Add(
                    new CodeAttributeDeclaration(
                        new CodeTypeReference("XmlElement"),
                        new CodeAttributeArgument(new CodePrimitiveExpression(xse.Name))));
                f.Attributes = MemberAttributes.Public;

            }
        } else {
            Trace.WriteLine("schema-type-name:" + xse.SchemaTypeName);
        }
    }

    static bool haveFieldNamed(string v, CodeTypeMemberCollection members) {
        foreach (CodeTypeMember ctm in members)
            if (ctm is CodeMemberField && string.Compare(ctm.Name, v) == 0)
                return true;
        return false;
    }

    static CodeTypeDeclaration findClassNamed(string v, CodeNamespace ns) {
        CodeTypeDeclaration ret = null;

        foreach (CodeTypeDeclaration ctd in ns.Types)
            if (string.Compare(ctd.Name, v) == 0) {
                ret = ctd;
                break;
            }
        if (ret == null)
            ns.Types.Add(ret = new CodeTypeDeclaration(v));
        return ret;
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