using System;

namespace HtmlToPdf
{
    public sealed class PdfDocumentCreationFailedException : Exception
    {
        public PdfDocumentCreationFailedException(string error)
            : base(error)
        {
        }
    }
}