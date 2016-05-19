using System;
using System.Linq;
using System.Data;
using P2P.Testing.Shared.Class;

using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Design.Execution;

namespace P2P.Testing.Shared.Script
{

	//
	// You can add custom execution steps by simply
	// adding a void function and decorating it with the [CodedStep] 
	// attribute to the test method. 
	// Those steps will automatically show up in the test steps on save.
	//
	// The BaseWebAiiTest exposes all key objects that you can use
	// to access the current testcase context. [i.e. ActiveBrowser, Find ..etc]
	//
	// Data driven tests can use the Data[columnIndex] or Data["columnName"] 
	// to access data for a specific data iteration.
	//
	// Example:
	//
	// [CodedStep("MyCustom Step Description")]
	// public void MyCustomStep()
	// {
	//		// Custom code goes here
	//      ActiveBrowser.NavigateTo("http://www.google.com");
	//
	//		// Or
	//		ActiveBrowser.NavigateTo(Data["url"]);
	// }
	//


    public class P2PEditTestResultMatchingMainSQL : BaseWebAiiTest
    {
        [CodedStep("Edit SQL Test Result File for Matching Main")]
        public void EditP2PTestResult()
        {
            //create instance to XmlReader class
            EdXmlReader objEdXmlReader = new EdXmlReader();

            // Read correct data row from xml file
            DataRow drTestAdminSqlResults = objEdXmlReader.Read_xml_file("P2PTestResults.xml", "Test_Results", "TR_Application", "MatchingMainAdminSQL");

            // Read correct data row from xml file
            DataRow drTestSilverlightSqlResults = objEdXmlReader.Read_xml_file("P2PTestResults.xml", "Test_Results", "TR_Application", "MatchingMainSilverlightSQL");
                                                   
            //create instance to AddXslLine class
            AddXslLine objAddXslLine = new AddXslLine();

            //call class method to edit admin test result xlm file
            objAddXslLine.AddXslLinetoXml(drTestAdminSqlResults["TR_File_Location"].ToString(), drTestAdminSqlResults["TR_File_Save_Location"].ToString());

            //call class method to edit admin test result xlm file
            objAddXslLine.AddXslLinetoXml(drTestSilverlightSqlResults["TR_File_Location"].ToString(), drTestSilverlightSqlResults["TR_File_Save_Location"].ToString());

        }
    }
}
