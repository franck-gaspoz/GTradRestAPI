﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static GTradRestAPI.Properties.Settings;

namespace GTradRestAPI
{
    public class GTradRestAPISettings
    {
        /// <summary>
        /// if true, the texts are split and translated using multiple queries
        /// </summary>
        public bool SplitStringBeforeTranslate { get; set; } = true;

        /// <summary>
        /// if true and SplitStringBeforeTranslate, the translation of segments is parrallelized
        /// </summary>
        public bool ParallelizeTranslationOfSegments { get; set; } = true;

        /// <summary>
        /// Google trad api uri (path and query)
        /// </summary>
        public string GTradRestAPIURL { get; set; } = "https://translate.googleapis.com/translate_a/single?client=gtx&sl={srcl}&tl={tgtl}&dt=t&q={txt}";

        /// <summary>
        /// query time out
        /// </summary>
        public int NetworkQueryTimeout { get; set; } = 2000;

        /// <summary>
        /// user agent
        /// </summary>
        public string UserAgent { get; set; } = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36";

        /// <summary>
        /// builds a new instance of the API settings
        /// </summary>
        public GTradRestAPISettings()
        {
            this.GTradRestAPIURL = Default.GTradRestAPIURL;
            this.NetworkQueryTimeout = Default.NetworkQueryTimeout;
            this.UserAgent = Default.UserAgent;
            this.ParallelizeTranslationOfSegments = Default.ParallelizeTranslationOfSegments;
            this.SplitStringBeforeTranslate = Default.SplitStringBeforeTranslate;
        }
    }
}
