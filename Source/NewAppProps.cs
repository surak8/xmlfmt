using System.Collections.Generic;
using System.Xml.Serialization;

namespace NSXmlfmt {

    /// <summary>new class</summary>
    /// <remarks>
    /// /// <para>Those fields that are single-entry are <see cref="XmlElementAttribute"/>,and
    /// things that are lists or collections are <see cref="XmlArrayAttribute"/>.</para>
    /// </remarks>
    [XmlRoot("ApplicationProperties")]
    public class NewAppProps {
        #region fields
        [XmlArray("ColumnHeaders"), XmlArrayItem("ColumnHeader")]
        public NewColHeader[] headers;

        [XmlArray("AcceptableStates"), XmlArrayItem("AcceptableState")]
        public string[] stateAbbrevs;

        [XmlAttribute("ConnectionStringEpicor")]
        public string epicorConnStr;

        [XmlArray("Distributors"), XmlArrayItem("Distributor")]
        public NewDistrib[] distributors;
        #endregion
        #region ctors

        /// <summary>For xml-serialization, a default public constructor is required, sigh.</summary>
        public NewAppProps() { }

        /// <summary>that<b>really</b> does the work.</summary>
        /// <param name="anObj"></param>
        /// <remarks>
        /// <para>Yes, it's a copy-constructor. This allows me to take the <strong>OLD</strong> object and map it to 
        /// the "proper" <see cref="XmlElementAttribute"/> or <see cref="XmlAttributeAttribute"/> attributes.
        /// </para>
        /// </remarks>
        public NewAppProps(OldAppProps anObj) {
            this.epicorConnStr = anObj.epicorConnStr;
            copyHeaders(anObj);
            copyStates(anObj);
            copyDistributors(anObj);
        }

        #endregion
        #region utility methods to duplicate the contents of the OLD hierarchy.

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
        #endregion
    }

    public class NewColHeader {

        #region fields
        [XmlAttribute("Name")]
        public string headerName;

        [XmlArray("Synonyms"), XmlArrayItem("Synonym")]
        public string[] synonyms;
        #endregion

        #region ctors
        public NewColHeader() { }

        public NewColHeader(OldColHeaders avar) {
            headerName = avar.headerName;
            createCinnamon(avar);
        }
        #endregion
        #region initialization methods

        /// <summary>allow aliases here.</summary>
        /// <remarks>
        /// <para>Joke method name. I am teasing Seb.
        /// <strong>Synonym</strong>and<strong>cinnamon</strong>
        /// are frequently misunderstood.
        /// </para></remarks>
        /// <param name="avar"></param>
        void createCinnamon(OldColHeaders avar) {
            List<string> tmp = new List<string>();

            foreach (string str in avar.synonyms)
                tmp.Add(str);
            this.synonyms = tmp.ToArray();
        } 
        #endregion
    }

    public class NewDistrib {
        #region fields
        [XmlArray("Synonyms"), XmlArrayItem("Synonym")]
        public string[] syns;

        [XmlAttribute("Name")]
        public string distributorName;

        [XmlAttribute("EpicorId")]
        public int epicorId;

        [XmlAttribute("Group")]
        public int distributorGroup;
        #endregion
        #region ctors

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

        #endregion
    }
}