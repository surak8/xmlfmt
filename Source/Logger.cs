
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace NSXmlfmt {

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
        /// <remarks><para>log to the <see cref="Debug"/> and/or <see cref="Trace"/> loggers.</para></remarks>
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

        /// <summary>Log a location and <see cref="Exception"/></summary>
        /// <param name="mb"></param>
        /// <param name="ex"></param>
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

        /// <summary>Recurse through the <see cref="Exception"/> structure, and 
        /// show all inner errors.</summary>
        /// <param name="ex"></param>
        /// <returns>a <see cref="string"/> containing the <see cref="Exception"/> messages.</returns>
        /// <seealso cref="StringBuilder"/>
        /// <seealso cref="Exception"/>
        /// <seealso cref="Exception.InnerException"/>
        public static string decomposeException(Exception ex) {
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
    }
}