using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GTradRestAPI
{
    /// <summary>
    /// Google Trad REST API (free) client
    /// </summary>
    public class GTradRestAPIClient : IGTradRestAPIClient
    {
        /// <summary>
        /// net utilities
        /// </summary>
        NetUtil Net;

        /// <summary>
        /// prepare a new instance
        /// </summary>
        public GTradRestAPIClient()
        {
            Net = new NetUtil();
        }       

        /// <summary>
        /// translate text from source and target languages codes
        /// </summary>
        /// <param name="sourceLanguage">source language code</param>
        /// <param name="targetLanguage">target language code</param>
        /// <param name="text">text to be translated</param>
        /// <returns>Translation object</returns>
        public Translation Translate(
            Languages sourceLanguage,
            Languages targetLanguage,
            string text
            )
        {
            CheckIsNotNull(text, "text");
            var q = Properties.Settings.Default.GTradRestAPIURL
                .Replace("{srcl}", LanguagesUtil.GetId(sourceLanguage))
                .Replace("{tgtl}", LanguagesUtil.GetId(targetLanguage))
                .Replace("{txt}", Net.Escape(text));
            var r = Net.GetQueryResponse(q);
            if (!string.IsNullOrWhiteSpace(r.Item1))
            {
                var o = JsonConvert.DeserializeObject(r.Item1) as JArray;
                if (o!=null)
                {
                    try
                    {
                        var translatedText = o[0][0][0].Value<string>();
                        var originalText = o[0][0][1].Value<string>();
                        return new Translation()
                        {
                            TranslatedText = translatedText,
                            OriginalText = originalText
                        };
                    } catch (Exception Ex)
                    {
                        throw new Exception($"Translate error: invalid result {o}");
                    }
                }
            }
            throw new Exception($"Translate error: {r.Item2}");
        }

        /// <summary>
        /// translate text from source and target language names if valids
        /// </summary>
        /// <param name="sourceLanguageName">source language name</param>
        /// <param name="targetLanguageName">target language name</param>
        /// <param name="text">text to be translated</param>
        /// <returns>Translation object</returns>
        public Translation TranslateFromNames(
            string sourceLanguageName,
            string targetLanguageName,
            string text
            )
        {
            var srcLng = LanguagesUtil.GetCode(sourceLanguageName);
            var tgtLng = LanguagesUtil.GetCode(targetLanguageName);
            CheckCodeIsValid(srcLng);
            CheckCodeIsValid(tgtLng);
            return Translate(
                (Languages)srcLng,
                (Languages)tgtLng,
                text);
        }

        /// <summary>
        /// translate text from source and target languages ids given as string if valids
        /// </summary>
        /// <param name="sourceLanguageId">source language id</param>
        /// <param name="targetLanguageId">target language id</param>
        /// <param name="text">text to be translated</param>
        /// <returns>Translation object</returns>
        public Translation Translate(
            string sourceLanguageId,
            string targetLanguageId,
            string text
            )
        {
            var srcLng = LanguagesUtil.GetCodeFromId(sourceLanguageId);
            var tgtLng = LanguagesUtil.GetCodeFromId(targetLanguageId);
            CheckCodeIsValid(srcLng);
            CheckCodeIsValid(tgtLng);
            return Translate(
                (Languages)srcLng,
                (Languages)tgtLng,
                text);
        }

        /// <summary>
        /// check if an object is null. abort if true
        /// </summary>
        /// <param name="o"></param>
        /// <param name="name"></param>
        void CheckIsNotNull(object o,string name)
        {
            if (o == null)
                throw new ArgumentNullException(name);
        }

        /// <summary>
        /// check if a language code is valid. abort if false
        /// </summary>
        /// <param name="languageCode"></param>
        void CheckCodeIsValid(Languages? languageCode)
        {
            if (languageCode == null)
                throw new ArgumentNullException($"language is not defined: {languageCode}");
        }
    }
}
