using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Xsl;
using System.Xml;
using System.IO;
using System.Xml.XPath;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XsltSettings settings = new XsltSettings(true, true);
            XslCompiledTransform transform = new XslCompiledTransform();
            transform.Load(@"xsd2html2xml.xsl", settings, new XmlUrlResolver());
            transform.Transform(tbXsdPath.Text,
                tbHtmlPath.Text);
            webBrowser1.DocumentText = File.ReadAllText(tbHtmlPath.Text);
        }

    }
}
