namespace GTradRestAPI
{
    public interface IGTradRestAPIClient
    {
        /// <summary>
        /// translate text from source and target languages codes
        /// </summary>
        /// <param name="sourceLanguage">source language code</param>
        /// <param name="targetLanguage">target language code</param>
        /// <param name="text">text to be translated</param>
        /// <returns>Translation object</returns>
        Translation Translate(string sourceLanguageId, string targetLanguageId, string text);

        /// <summary>
        /// translate text from source and target language names if valids
        /// </summary>
        /// <param name="sourceLanguageName">source language name</param>
        /// <param name="targetLanguageName">target language name</param>
        /// <param name="text">text to be translated</param>
        /// <returns>Translation object</returns>
        Translation Translate(Languages sourceLanguage, Languages targetLanguage, string text);

        /// <summary>
        /// translate text from source and target languages ids given as string if valids
        /// </summary>
        /// <param name="sourceLanguageId">source language id</param>
        /// <param name="targetLanguageId">target language id</param>
        /// <param name="text">text to be translated</param>
        /// <returns>Translation object</returns>
        Translation TranslateFromNames(string sourceLanguageName, string targetLanguageName, string text);
    }
}