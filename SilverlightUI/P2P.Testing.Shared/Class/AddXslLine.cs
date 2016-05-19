using System;
using System.Linq;
using System.Xml.Linq;

namespace P2P.Testing.Shared.Class
{
    public class AddXslLine
    {
        //Open mstest test result file and adds xml-stylesheet line
        public void AddXslLinetoXml(string testResultFileLocation, string testResultFileSaveLocation)
        {

            //open mstest test result file
            var xmlDoc = XDocument.Load(testResultFileLocation);
            
            //add correct xsl file to variable
            var xslStyle = new XProcessingInstruction("xml-stylesheet", "type='text/xsl' href='Test.Results.Filter.xsl'");
            
            //add stylesheet line to the test result file
            xmlDoc.AddFirst(xslStyle);
            
            //save edited test result file 
            xmlDoc.Save(testResultFileSaveLocation);

        }
    
    }
}
