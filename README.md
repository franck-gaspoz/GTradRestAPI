# GTradRestAPI - Google Translate Rest API (Free) 
C# .Net Framework 4.6.2
Visual Studio
<hr>

a C# library that deals with Google Translate REST API. Offers google translate service operations from a C# software

```CSharp
GCl = new GTradRestAPIClient();
// source language, target language, text to be translated
var r = GCl.TranslateAsync(Languages.en, Languages.fr, "text to be translated");
// translated text
var res = r.Result; 
```

<hr>

> ### :information_source: Developments are stopped in this repository
> To go further, please read informations at : [http://franckgaspoz.fr/en/use-google-translate-for-free-on-the-command-line/](http://franckgaspoz.fr/en/use-google-translate-for-free-on-the-command-line/)
