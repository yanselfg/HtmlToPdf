using HtmlToPdf.WkHtmlToPdf.Assets;
using HtmlToPdf.WkHtmlToPdf.Interop;
using System.IO.Compression;
using System.Reflection;

namespace HtmlToPdf.WkHtmlToPdf.WkHtmlToX
{
    sealed class WkHtmlToPdfLibrary
    {
        private const string LibraryFilename = "wkhtmltox.dll";
        private const string Compressed32BitLibraryFilename = "wkhtmltox_32.dll";
        private const string Compressed64BitLibraryFilename = "wkhtmltox_64.dll";

        public static NativeLibrary Load()
        {
            return NativeLibrary.Load(LibraryFilename, LoadLibraryContent);
        }

        private static byte[] LoadLibraryContent()
        {
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            {
                throw new PlatformNotSupportedException(string.Format("Platform {0} is not supported", Platform()));
            }

            using var wkhtmltoxZipArchive = WkHtmlToXZipArchive();

            return wkhtmltoxZipArchive == null 
                ? throw new Exception() 
                : wkhtmltoxZipArchive.ReadFile(CompressedLibraryFilename());
        }

        private static ZipArchive WkHtmlToXZipArchive()
        {
            var stream = GetManifestResourceStream();
            return stream == null ? throw new Exception() : new ZipArchive(stream);
        }

        private static Stream? GetManifestResourceStream()
        {
            return Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("HtmlToPdf.WkHtmlToPdf.Assets.wkhtmltox.zip");
        }

        private static string CompressedLibraryFilename()
        {
            return Environment.Is64BitProcess
                ? Compressed64BitLibraryFilename
                : Compressed32BitLibraryFilename;
        }

        private static string? Platform()
        {
            return Enum.GetName(typeof(PlatformID), Environment.OSVersion.Platform);
        }
    }
}