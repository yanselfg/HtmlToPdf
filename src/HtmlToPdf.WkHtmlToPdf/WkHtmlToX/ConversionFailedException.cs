using System;

namespace HtmlToPdf.WkHtmlToPdf.WkHtmlToX
{
    sealed class ConversionFailedException : Exception
    {
        public ConversionFailedException(string errorText)
            : base(errorText)
        {
        }
    }
}