using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Xsl;

namespace FormGenerator
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            XsltSettings settings = new XsltSettings(true, true);
            XslCompiledTransform transform = new XslCompiledTransform();
            transform.Load(@"xsd2html2xml.xsl", settings, new XmlUrlResolver());
            var x = Test();
            File.WriteAllText(@"D:\Work\form-generator\abc.xsd", x);
            transform.Transform(@"D:\_code\PaymentSafe2.5_GIT\BaseCommon\Networks\NetworkFiles\NetworkFiles\SWIFT\2023\XSD\Category 1\MT103\MT103.xsd",
                @"D:\Work\form-generator\camt.056.001.08.html");
        }

        public static string Test()
        {
            // List of schema file paths to merge
            var schemaFiles = new List<string>
        {
            @"D:\_code\PaymentSafe2.5_GIT\BaseCommon\Networks\NetworkFiles\NetworkFiles\SWIFT\2023\XSD\Base Schemas\SWIFTBaseTypes.xsd",
            @"D:\_code\PaymentSafe2.5_GIT\BaseCommon\Networks\NetworkFiles\NetworkFiles\SWIFT\2023\XSD\Category 1\MT103\MT103.xsd"  // Add other schema paths as needed
        };

            // Create a schema set to load and merge schemas
            var schemaSet = new XmlSchemaSet();

            // Load each schema from the list
            foreach (var schemaFile in schemaFiles)
            {
                using (var stream = File.OpenRead(schemaFile))
                {
                    var schema = XmlSchema.Read(stream, ValidationEventHandler);
                    schemaSet.Add(schema);
                }
            }

            // Merge the schemas into a single XML Schema
            var mergedSchema = new XmlSchema();
            foreach (XmlSchema schema in schemaSet.Schemas())
            {
                mergedSchema.Includes.Add(schema);
            }

            // Serialize the merged schema into an XML string
            var stringBuilder = new StringBuilder();
            var xmlWriterSettings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8,
            };

            using (var writer = XmlWriter.Create(stringBuilder, xmlWriterSettings))
            {
                mergedSchema.Write(writer);
            }

            return stringBuilder.ToString();
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            Console.WriteLine($"Schema validation error: {e.Message}");
        }
    }
}