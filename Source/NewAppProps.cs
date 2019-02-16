using System.Collections.Generic;
using System.Xml.Serialization;

namespace NSXmlfmt {

    [XmlRoot("ApplicationProperties")]
    public class NewAppProps {
        [XmlArray("ColumnHeaders"), XmlArrayItem("ColumnHeader")]
        public NewColHeader[] headers;

        [XmlArray("AcceptableStates"), XmlArrayItem("string")]
        public string[] stateAbbrevs;

        [XmlAttribute("ConnectionStringEpicor")]
        public string epicorConnStr;

        [XmlArray("Distributors"), XmlArrayItem("Distributor")]
        public NewDistrib[] distributors;


        public NewAppProps() { }

        public NewAppProps(OldAppProps anObj) {
            this.epicorConnStr = anObj.epicorConnStr;
            copyHeaders(anObj);
            copyStates(anObj);
            copyDistributors(anObj);
        }

        void copyDistributors(OldAppProps anObj) {
            List<NewDistrib> tmp = new List<NewDistrib>();

            foreach (var avar in anObj.distributors)
                tmp.Add(new NewDistrib(avar));
            this.distributors = tmp.ToArray();
        }

        void copyStates(OldAppProps anObj) {
            List<string> tmpStates = new List<string>();

            foreach (var avar in anObj.stateAbbreviations)
                tmpStates.Add(avar);
            this.stateAbbrevs = tmpStates.ToArray();
        }

        void copyHeaders(OldAppProps anObj) {
            List<NewColHeader> cols = new List<NewColHeader>();

            foreach (var avar in anObj.headers)
                cols.Add(new NewColHeader(avar));

            this.headers = cols.ToArray();
        }
    }

    public class NewColHeader {

        [XmlAttribute("Name")]
        public string headerName;

        [XmlArray("Synonyms"), XmlArrayItem("string")]
        public string[] synonyms;

        public NewColHeader() { }

        public NewColHeader(OldColHeaders avar) {
            headerName = avar.headerName;
            List<string> tmp = new List<string>();
            foreach (string str in avar.synonyms)
                tmp.Add(str);
            this.synonyms = tmp.ToArray();
        }

    }

    public class NewDistrib {
        [XmlArray("Synonyms"), XmlArrayItem("string")]
        public string[] syns;

        [XmlAttribute("Name")]
        public string distributorName;

        [XmlAttribute("EpicorId")]
        public int epicorId;

        [XmlAttribute("Group")]
        public int distributorGroup;

        public NewDistrib() { }

        public NewDistrib(OldDistrib avar) {
            List<string> tmp = new List<string>();

            foreach (string str in avar.blah)
                tmp.Add(str);
            this.syns = tmp.ToArray();

            this.distributorGroup = avar.distributorGroup;
            this.distributorName = avar.distributorName;
            this.epicorId = avar.epicorId;
        }
    }
}