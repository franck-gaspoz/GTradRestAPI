using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GTradRestAPI
{
    internal class NetUtil
    {
        /// <summary>
        /// user agent
        /// </summary>
        internal string UserAgent { get; set; } = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36";

        internal int NetworkQueryTimeout { get; set; } = 1000;

        internal NetUtil()
        {

        }

        /// <summary>
        /// http escape string
        /// </summary>
        /// <param name="s">string to be escaped</param>
        /// <returns>escaped string</returns>
        public string Escape(string s)
        {
            return Uri.EscapeDataString(s);
        }

        /// <summary>
        /// preform query at url and return result (or null) , status description
        /// </summary>
        /// <param name="url">uri</param>
        /// <returns>resut|null,status description</returns>
        public Tuple<string, string> GetQueryResponse(
            string url
            )
        {
            string r = null;
            try
            {
                var q = GetQuery(url);
                using (var rep = GetResponse(q))
                {
                    if (rep.StatusCode == HttpStatusCode.OK)
                    {
                        using (var sr = rep.GetResponseStream())
                        {
                            using (var str = new StreamReader(sr))
                            {
                                r = str.ReadToEnd();
                                return Tuple.Create(r, rep.StatusDescription);
                            }
                        }
                    }
                    else
                        return Tuple.Create<string, string>(null, rep.StatusDescription);
                }
            }
            catch (Exception Ex)
            {
                return Tuple.Create<string, string>(null, Ex.Message);
            }
        }

        /// <summary>
        /// http get at uri
        /// </summary>
        /// <param name="uri">uri</param>
        /// <param name="rethrowException">retain exceptions if true</param>
        /// <returns></returns>
        public string HTTPGet(
                    string uri,
                    bool rethrowException = true)
        {
            try
            {
                var q = GetQuery(uri);
                using (var r = GetResponse(q))
                {
                    if (r.StatusCode == HttpStatusCode.OK)
                    {
                        using (var sr = new StreamReader(r.GetResponseStream(), true))
                        {
                            var rs = sr.ReadToEnd();
                            return rs;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                if (rethrowException)
                    throw Ex;
            }
            return null;
        }

        /// <summary>
        /// obtain response from query
        /// </summary>
        /// <param name="query">query object</param>
        /// <returns>web repsonse</returns>
        public HttpWebResponse GetResponse(HttpWebRequest query)
        {
            return (HttpWebResponse)query.GetResponse();
        }

        /// <summary>
        /// get http get query object
        /// </summary>
        /// <param name="uri">target uri</param>
        /// <returns>web request</returns>
        public HttpWebRequest GetQuery(string uri)
        {
            var u = new Uri(uri);
            var query = (HttpWebRequest)WebRequest.Create(u);
            query.UserAgent = UserAgent;
            query.KeepAlive = false;
            query.Timeout = NetworkQueryTimeout;
            return query;
        }

    }
}
