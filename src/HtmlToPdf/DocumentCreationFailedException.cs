namespace HtmlToPdf
{
    public sealed class DocumentCreationFailedException : Exception
    {
        internal DocumentCreationFailedException(string message)
            : base(message)
        {
        }
    }
}
