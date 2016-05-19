using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Design.Execution;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Silverlight.UI;
using Telerik.TestingFramework.Controls.KendoUI;
using Telerik.WebAii.Controls.Html;
using Telerik.WebAii.Controls.Xaml;
using System.Data;
using P2P.Testing.Shared.Class;
using P2P.Testing.Shared.Class.InvoiceAdministration.Create;
using P2P.Testing.Shared;
using P2P.Testing.Shared.Class.InvoiceAdministration.Search;
using P2P.Testing.Shared.Class.PaymentPlans.Search;
using P2P.Testing.Shared.Class.InvoiceAdministration.InvoiceDetails;
using P2P.Testing.Shared.Class.InvoiceAdministration;
using P2P.Testing.Shared.Class.InvoiceAdministration.ToolbarActions;

namespace APT.Scripts
{

    public class Cancel_PaymentPlanMatching_RemoveInvoice : BaseWebAiiTest
    {
        #region [ Dynamic Pages Reference ]

        private Pages _pages;

        /// <summary>
        /// Gets the Pages object that has references
        /// to all the elements, frames or regions
        /// in this project.
        /// </summary>
		public Pages Pages
		{
			get
			{
				if (_pages == null)
				{
					_pages = new Pages(Manager.Current);
				}
				return _pages;
			}
        }

        #endregion
        
		//@Test Case ID: 88891
        //@Developed By: nitinahu    

		 //Declare Global Variable
        private readonly DataRow drAPT;      

        //Create an object of EdXmlReader class   
        EdXmlReader objReadXml = new EdXmlReader();   

        //Create an object for 'P2PNavigation' class
        P2PNavigation p2pNavigationObj = new P2PNavigation();

        //Create an object for 'P2PCreateInvoice' class
        P2PCreateInvoice p2pCreateInvoiceObj = new P2PCreateInvoice();

        //Create an object for P2PInvoiceAdministrationSearch class
        P2PInvoiceAdministrationSearch searchObj = new P2PInvoiceAdministrationSearch();

        //Create an object for P2PPaymentPlansSearch class
        P2PPaymentPlansSearch p2pPaymentPlansSearchObj = new P2PPaymentPlansSearch();     
      
        //Create an object for P2PInvoiceAdministrationInvoiceDetails class
        P2PInvoiceAdministrationInvoiceDetails p2pInvoiceAdministrationInvoiceDetailsObj = new P2PInvoiceAdministrationInvoiceDetails();

        //Create an object for P2PInvoiceAdministrationVerification class
        P2PInvoiceAdministrationVerification p2pInvoiceAdministrationVerificationObj = new P2PInvoiceAdministrationVerification();           

        //Create an object for P2PExceptionHandler class
        P2PExceptionHandler exceptionHandler = new P2PExceptionHandler();

        //Constructor of Class
        public Cancel_PaymentPlanMatching_RemoveInvoice()
        {           
            //Read the Data Row from XML 
            drAPT = objReadXml.Read_xml_file("APT_TestData.xml", "CancelPaymentPlanMatchingAndRemoveInvoice", "TestCase", "88891");         

         }      

        [CodedStep("Login to P2P Client", RequiresSilverlight = true)]
        public void LogintoP2P()
        {
            // Create an object for "P2PLogin" class
            var objlogin = new P2PLogin(ActiveBrowser);

            try
            {
                //Call  method "LoginToSilverlightClient" from the "P2PLogin" class                
                objlogin.LoginToSilverlightClient(drAPT["LoginUser"].ToString(),"");
            }

            catch (Exception loginintoP2P)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(loginintoP2P, Globals.testScriptName);
            }
        }

        [CodedStep("Handle the Busy Indicator", RequiresSilverlight = true)]
        public void HandleBusyIndicator()
        {
            try
            {
                //Handle Busy Indicator
                P2PNavigation.CallBusyIndicator();
            }
            catch (Exception handleBusyIndicator)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(handleBusyIndicator, Globals.testScriptName);
            }
        }

        [CodedStep("Navigate to Overview Page", RequiresSilverlight = true)]
        public void OpenOverviewPage()
        {
            try
            {
                //Call the method to Click Overview Page
                p2pNavigationObj.NavigateInvoiceAdministrationToOverview();
            }
            catch (Exception openOverviewPage)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(openOverviewPage, Globals.testScriptName);
            }

            //Call the Coded Step Handle Busy Indicator
            HandleBusyIndicator();
        }

        [CodedStep("Filter Invoices From Organization BW80", RequiresSilverlight = true)]
        public void FilterInvoice()
        {
            try
            {
                //Call the method to Filter Invoices from Overview Page
                searchObj.P2PInvoiceAdministrationOverviewFilterOrganization(drAPT["OrganizationCode"].ToString());
            }
            catch (Exception filterInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(filterInvoice, Globals.testScriptName);
            }

            //Call the Coded Step Handle Busy Indicator
            HandleBusyIndicator();
        }

        [CodedStep("Select In Manual Matching PaymentPlan Invoices From Overview", RequiresSilverlight = true)]
        public void SelectInManualMatchingPaymentPlan()
        {

            try
            {
                //Method to Open Draft Invoice from Overview
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationOverviewInvoiceStatusesReceived(drAPT["InvoiceStatus"].ToString(), drAPT["ManualMatchingPaymentPlanCountifDisabled"].ToString());
            }
            catch (Exception selectInManualMatchingPaymentPlan)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(selectInManualMatchingPaymentPlan, Globals.testScriptName);
            }

            //Call the Coded Step Handle Busy Indicator
            HandleBusyIndicator();

        }

        [CodedStep("Search In Manual Matching PaymentPlan 'MatchPlan_1_BW80_2'", RequiresSilverlight = true)]
        public void SearchInvoiceOnMatchingSilo()
        {
            try
            {
                //Use the Object to Access the Method for Search the Invoice
                searchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(drAPT["ManualMatchingPaymentPlanInvoiceNumber"].ToString());
            }
            catch (Exception searchInvoiceOnMatchingSilo)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchInvoiceOnMatchingSilo, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Open Manual Matching PaymentPlan 'MatchPlan_1_BW80_2'", RequiresSilverlight = true)]
        public void OpenSelectedPaymentPlan()
        {
            //Create an object for shared class
            P2PInvoiceAdministrationToolbarActions openPaymentPlan = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //Call the Method to Open the Selected Plan                 
                openPaymentPlan.P2PInvoiceAdministrationOpenSelectedInvoice();
            }
            catch (Exception openSelectedPaymentPlan)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(openSelectedPaymentPlan, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }       

        [CodedStep("Cancel the Process", RequiresSilverlight = true)]
        public void CancelProcess()
        {

            try
            {
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationCancelProcess(drAPT["Comments"].ToString());
            }
            catch (Exception cancelProcess)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(cancelProcess, Globals.testScriptName);
            }
            //Handle Busy Indicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Received ", RequiresSilverlight = true)]
        public void NavigateToReceived()
        {
            try
            {
                //Call the Method to Navigate To Received Page 
                p2pNavigationObj.NavigateInvoiceAdministrationToReceived();
            }
            catch (Exception navigatetoReceived)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigatetoReceived, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator 
            HandleBusyIndicator();
        }

        [CodedStep("Clear All Search on Received Page ", RequiresSilverlight = true)]
        public void ClearAllSearch()
        {
            try
            {
                //Call the Method to Clear All Search 
                searchObj.P2PInvoiceAdministrationClearAllSearch();
            }
            catch (Exception clearAllSearch)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(clearAllSearch, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Refresh the Grid ", RequiresSilverlight = true)]
        public void RefreshTheGrid()
        {
            //Create an object for P2PInvoiceAdministrationToolbarActions
            var refreshGrid = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //Refresh the Received Page
                refreshGrid.P2PInvoiceAdministrationRefreshInvoicePage();
            }
            catch (Exception refreshTheGrid)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(refreshTheGrid, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Search the Invoice 'MatchPlan_1_BW80_2' on Received Silo and Open Selected", RequiresSilverlight = true)]
        public void SearchInvoiceOnReceivedSilo()
        {
            try
            {
                //Use the Object to Access the Method for Search the Invoice
                searchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(drAPT["ManualMatchingPaymentPlanInvoiceNumber"].ToString());
            }
            catch (Exception searchInvoiceOnReceivedSilo)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchInvoiceOnReceivedSilo, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();

            //Open Selected
            OpenSelectedPaymentPlan();
        }

        [CodedStep("Remove Invoice From Received Silo", RequiresSilverlight = true)]
        public void RemoveInvoice()
        {         
            try
            {
                //Call the Remove Invoice Method
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationRemoveInvoice(drAPT["Comments"].ToString());
            }

            catch (Exception removeInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(removeInvoice, Globals.testScriptName);
            }

            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();        
        }

        [CodedStep("Logout From P2P", RequiresSilverlight = true)]
        public void LogoutFromP2P()
        {
            // Create an object for "P2PLogin" class
            P2PLogin objlogout = new P2PLogin(Manager.Current.ActiveBrowser);
            try
            {
                //Call method for logout into the P2P Alusta Application
                objlogout.LogoutFromSilverlightClient();
            }
            catch (Exception logoutFromP2P)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(logoutFromP2P, Globals.testScriptName);
            }
        }

    }
}
