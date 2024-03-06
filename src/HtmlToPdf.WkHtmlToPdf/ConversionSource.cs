namespace HtmlToPdf.WkHtmlToPdf
{
    public class ConversionSource
    {
        public string Html { get; set; }
        public IDictionary<string, string> GlobalSettings { get; set; }
        public IDictionary<string, string> ObjectSettings { get; set; }
    }
}