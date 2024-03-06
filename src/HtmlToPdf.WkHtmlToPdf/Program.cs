using HtmlToPdf.WkHtmlToPdf.WkHtmlToX;
using System.Runtime.Serialization.Json;
using System.Text;

namespace HtmlToPdf.WkHtmlToPdf
{
    static class Program
    {
        static int Main()
        {
            try
            {
                ConvertStandardInputToPdf();
                return 0;
            }
            catch (Exception ex)
            {
                WriteExceptionMessageToStandardError(ex);
                return -1;
            }
        }

        private static void WriteExceptionMessageToStandardError(Exception ex)
        {
            using var standardErrort = Console.OpenStandardError();
            using var writer = new StreamWriter(standardErrort);
            writer.WriteAsBase64EncodedString(ex.Message);
        }

        private static void ConvertStandardInputToPdf()
        {
            var source = ConversionSource();
            
            if (source == null)
            {
                throw new Exception();
            }

            ConvertToPdf(source);
        }

        private static ConversionSource? ConversionSource()
        {
            using var standardInput = Console.OpenStandardInput();
            using var streamReader = new StreamReader(standardInput);
            return DeserializeBase64EncodedSource<ConversionSource>(streamReader.ReadToEnd());
        }

        private static T? DeserializeBase64EncodedSource<T>(string base64EncodedObject)
        {
            using var stream = new MemoryStream(Convert.FromBase64String(base64EncodedObject));
            var serializer = new DataContractJsonSerializer(typeof(T));
            return (T?)(serializer.ReadObject(stream));
        }

        private static void ConvertToPdf(ConversionSource conversionSource)
        {
            using var wkhtmlToPdfContext = WkHtmlToPdfContext.Create();
            foreach (var globalSetting in conversionSource.GlobalSettings)
            {
                WkHtmlToX.WkHtmlToPdf.wkhtmltopdf_set_global_setting(wkhtmlToPdfContext.GlobalSettingsPointer,
                    globalSetting.Key,
                    globalSetting.Value);
            }

            foreach (var objectSetting in conversionSource.ObjectSettings)
            {
                WkHtmlToX.WkHtmlToPdf.wkhtmltopdf_set_object_setting(wkhtmlToPdfContext.ObjectSettingsPointer,
                    objectSetting.Key,
                    objectSetting.Value);
            }

            wkhtmlToPdfContext.Convert(conversionSource.Html);
        }

        private static void WriteAsBase64EncodedString(this TextWriter writer, string str)
        {
            writer.Write(Convert.ToBase64String(Encoding.UTF8.GetBytes(str)));
        }
    }
}

