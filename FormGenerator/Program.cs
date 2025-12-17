using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Xsl;

namespace FormGenerator
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var schemaPath = args.ElementAtOrDefault(0)
                              ?? Path.Combine(AppContext.BaseDirectory, "SampleData", "sample.xsd");
            var outputDirectory = args.ElementAtOrDefault(1)
                                 ?? Path.Combine(AppContext.BaseDirectory, "GeneratedAngular");

            var html = TransformSchemaToHtml(schemaPath);
            var component = CreateAngularComponentFiles(html, outputDirectory);

            Console.WriteLine($"Angular component created at {component.TemplatePath}");
        }

        private static string TransformSchemaToHtml(string schemaPath)
        {
            if (!File.Exists(schemaPath))
            {
                throw new FileNotFoundException($"Schema not found at '{schemaPath}'.");
            }

            var settings = new XsltSettings(true, true);
            var transform = new XslCompiledTransform();
            var xslPath = Path.Combine(AppContext.BaseDirectory, "xsd2html2xml.xsl");
            transform.Load(xslPath, settings, new XmlUrlResolver());

            var stringBuilder = new StringBuilder();
            using (var writer = XmlWriter.Create(stringBuilder, transform.OutputSettings))
            using (var reader = XmlReader.Create(schemaPath))
            {
                transform.Transform(reader, writer);
            }

            return stringBuilder.ToString();
        }

        private static ComponentPaths CreateAngularComponentFiles(string htmlDocument, string outputDirectory)
        {
            Directory.CreateDirectory(outputDirectory);

            var bodyMatch = Regex.Match(htmlDocument, "<body[^>]*>([\\s\\S]*?)</body>", RegexOptions.IgnoreCase);
            var bodyContent = bodyMatch.Success ? bodyMatch.Groups[1].Value : htmlDocument;

            var styleMatches = Regex.Matches(htmlDocument, "<style[^>]*>([\\s\\S]*?)</style>", RegexOptions.IgnoreCase);
            var extractedStyles = string.Join(Environment.NewLine + Environment.NewLine,
                styleMatches.Cast<Match>().Select(m => m.Groups[1].Value.Trim()));

            var scriptMatches = Regex.Matches(bodyContent, "<script[^>]*>([\\s\\S]*?)</script>", RegexOptions.IgnoreCase);
            var scriptContent = string.Join(Environment.NewLine + Environment.NewLine,
                scriptMatches.Cast<Match>().Select(m => m.Groups[1].Value.Trim()));

            var templateContent = Regex.Replace(bodyContent, "<script[\\s\\S]*?</script>", string.Empty,
                RegexOptions.IgnoreCase).Trim();

            var templatePath = Path.Combine(outputDirectory, "generated-form.component.html");
            var stylesPath = Path.Combine(outputDirectory, "generated-form.component.css");
            var componentPath = Path.Combine(outputDirectory, "generated-form.component.ts");

            File.WriteAllText(templatePath, templateContent);
            File.WriteAllText(stylesPath, string.IsNullOrWhiteSpace(extractedStyles)
                ? "/* Styling generated from schema will appear here if provided. */"
                : extractedStyles);
            File.WriteAllText(componentPath, BuildComponentSource(scriptContent));

            return new ComponentPaths(templatePath, stylesPath, componentPath);
        }

        private static string BuildComponentSource(string scriptContent)
        {
            return @$"import {{ AfterViewInit, Component, ElementRef }} from '@angular/core';

@Component({{
  selector: 'app-generated-form',
  templateUrl: './generated-form.component.html',
  styleUrls: ['./generated-form.component.css']
}})
export class GeneratedFormComponent implements AfterViewInit {{
  constructor(private readonly host: ElementRef<HTMLElement>) {{}}

  ngAfterViewInit(): void {{
    this.injectGeneratedBehaviour();
  }}

  private injectGeneratedBehaviour(): void {{
    const scriptContent = `{EscapeForTemplateLiteral(scriptContent ?? string.Empty)}`;
    if (!scriptContent.trim()) {{
      return;
    }}

    const script = document.createElement('script');
    script.type = 'text/javascript';
    script.text = scriptContent;
    this.host.nativeElement.appendChild(script);
  }}
}}
";
        }

        private static string EscapeForTemplateLiteral(string value)
        {
            return value
                .Replace("\\", "\\\\")
                .Replace("`", "\\`")
                .Replace("${", "${'${'}")
                .Replace("\r", "\\r")
                .Replace("\n", "\\n");
        }

        private class ComponentPaths
        {
            public ComponentPaths(string templatePath, string stylesPath, string componentPath)
            {
                TemplatePath = templatePath;
                StylesPath = stylesPath;
                ComponentPath = componentPath;
            }

            public string TemplatePath { get; }

            public string StylesPath { get; }

            public string ComponentPath { get; }
        }
    }
}
