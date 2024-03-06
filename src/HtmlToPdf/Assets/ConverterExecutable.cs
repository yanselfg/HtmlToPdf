using System.Reflection;

namespace HtmlToPdf.Assets
{
    sealed class ConverterExecutable
    {
        private const string ConverterExecutableFilename = "HtmlToPdf.WkHtmlToPdf.exe";

        private ConverterExecutable()
        {
        }

        public static ConverterExecutable Get()
        {
            var bundledFile = new ConverterExecutable();
            bundledFile.CreateIfConverterExecutableDoesNotExist();
            return bundledFile;
        }

        public string FullConverterExecutableFilename
        {
            get { return ResolveFullPathToConverterExecutableFile(); }
        }

        private void CreateIfConverterExecutableDoesNotExist()
        {
            if (!File.Exists(FullConverterExecutableFilename))
            {
                Create(GetConverterExecutableContent());
            }
        }

        private static byte[] GetConverterExecutableContent()
        {
            using var resourceStream = GetConverterExecutable() ?? throw new Exception();
            var resource = new byte[resourceStream.Length];
            resourceStream.Read(resource, 0, resource.Length);
            return resource;
        }

        private static Stream? GetConverterExecutable()
        {
            return Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("HtmlToPdf.Assets.OpenHtmlToPdf.WkHtmlToPdf.exe");
        }

        private void Create(byte[] fileContent)
        {
            try
            {
                if (!Directory.Exists(BundledFilesDirectory()))
                {
                    Directory.CreateDirectory(BundledFilesDirectory());
                }

                using var file = File.Open(FullConverterExecutableFilename, FileMode.Create);

                file.Write(fileContent, 0, fileContent.Length);
            }
            catch (IOException)
            {
                throw;
            }
        }

        private static string ResolveFullPathToConverterExecutableFile()
        {
            return Path.Combine(BundledFilesDirectory(), ConverterExecutableFilename);
        }

        private static string BundledFilesDirectory()
        {
            return Path.Combine(Path.GetTempPath(), "HtmlToPdf", Version());
        }

        private static string Version()
        {
            return string.Format("{0}_{1}",
                Assembly.GetExecutingAssembly().GetName().Version,
                Environment.Is64BitProcess ? 64 : 32);
        }
    }
}