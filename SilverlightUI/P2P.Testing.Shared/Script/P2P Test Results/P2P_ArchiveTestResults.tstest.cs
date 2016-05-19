using System;
using System.Linq;

using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Design.Execution;
using P2P.Testing.Shared.Class;

namespace P2P.Testing.Shared.Script.P2P_Test_Results
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


    public class P2PArchiveTestResults : BaseWebAiiTest
    {

        [CodedStep("Archive test results")]
		public void ArchiveTestResults()
        {

            //Create xml files for archive paths


            ArchiveTestResults objArchive = new ArchiveTestResults();

            //archive SQL Admin test results
            objArchive.ArchiveP2PTestResults("C:\\P2P\\Test.Automation.Test.Results.Archive", "P2P.Admin.Test.Result.SQL.trx", "C:\\P2P\\Test.Automation.Test.Results");

            //archive SQL Silverlight test results
            objArchive.ArchiveP2PTestResults("C:\\P2P\\Test.Automation.Test.Results.Archive", "P2P.Silverlight.Test.Result.SQL.trx", "C:\\P2P\\Test.Automation.Test.Results");



        }
    }
}
