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
using P2P.Testing.Shared.Class.InvoiceAdministration.Search;
using P2P.Testing.Shared.Class.PaymentPlans.Search;
using P2P.Testing.Shared.Class.PaymentPlans.InvoiceDetails;
using P2P.Testing.Shared.Class.InvoiceAdministration.InvoiceDetails;
using P2P.Testing.Shared.Class.InvoiceAdministration;
using P2P.Testing.Shared.Class.InvoiceAdministration.ToolbarActions;
using P2P.Testing.Shared;

namespace APT.Scripts
{

    public class EditAndMatch_NoPOFoundInvoices_Header_ROW_GR : BaseWebAiiTest
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
        
		 //@Test Case ID: 88876
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

        //Create an object for P2PPaymentPlansDetails class
        P2PPaymentPlansDetails objppDetails = new P2PPaymentPlansDetails();
      
        //Create an object for P2PInvoiceAdministrationInvoiceDetails class
        P2PInvoiceAdministrationInvoiceDetails p2pInvoiceAdministrationInvoiceDetailsObj = new P2PInvoiceAdministrationInvoiceDetails();

        //Create an object for P2PInvoiceAdministrationVerification class
        P2PInvoiceAdministrationVerification p2pInvoiceAdministrationVerificationObj = new P2PInvoiceAdministrationVerification();           

        //Create an object for P2PExceptionHandler class
        P2PExceptionHandler exceptionHandler = new P2PExceptionHandler();

        //Constructor of Class
        public EditAndMatch_NoPOFoundInvoices_Header_ROW_GR()
        {           
            //Read the Data Row from XML 
            drAPT = objReadXml.Read_xml_file("APT_TestData.xml", "EditAndMatchNoPOFoundInvoicesHeaderROWGR", "TestCase", "88876");        
        }      

        [CodedStep("Login To P2P Client", RequiresSilverlight = true)]
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

        [CodedStep("Select In Manual Order Matching Invoices From Overview", RequiresSilverlight = true)]
        public void SelectInManualOrderMatching()
        {

            try
            {
                //Method to Open Draft Invoice from Overview
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationOverviewInvoiceStatusesReceived(drAPT["InvoiceStatus"].ToString(), drAPT["ManualOrderMatchingCountifDisabled"].ToString());
            }
            catch (Exception selectInManualOrderMatching)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(selectInManualOrderMatching, Globals.testScriptName);
            }

            //Call the Coded Step Handle Busy Indicator
            HandleBusyIndicator();

        }       

        [CodedStep("Select Invocies Having PO's(HEA_0, ROW_0 and RGR_0)", RequiresSilverlight = true)]
        public void SelectInvoicesHavingPOs()
        {

            //Create an Object for P2PInvoiceAdministrationToolbarActions class
            P2PInvoiceAdministrationToolbarActions objToolbarActions = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);

            try
            {
                //Use the Object to Access the Method for Search the Invoice
                objToolbarActions.P2PInvoiceAdministrationMultipleInvoices_Select(drAPT["PurchaseOrderColumn"].ToString(), drAPT["InvoiceNumberPO_HEA_0"].ToString(), drAPT["InvoiceNumberPO_ROW_0"].ToString(), drAPT["InvoiceNumberPO_RGR_0"].ToString(), null, drAPT["PurchaseOrderColumn"].ToString());
            }
            catch (Exception cancelAndResendPaymentPlan)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(cancelAndResendPaymentPlan, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();         
        }        

        [CodedStep("Open Selected Invoices Having PO's(HEA_0, ROW_0 and RGR_0)", RequiresSilverlight = true)]
        public void OpenSelectedInvoices()
        {
            //Create an object for shared class
            P2PInvoiceAdministrationToolbarActions openPaymentPlan = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //Call the Method to Open the Selected Plan                 
                openPaymentPlan.P2PInvoiceAdministrationOpenSelectedInvoice();
            }
            catch (Exception openSelectedInvoices)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(openSelectedInvoices, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Next Button and Move to Invoice 'Row_0_BW80_0'", RequiresSilverlight = true)]
        public void MoveToNextInvoice()
        { 
        
            try
            {
                //call the method to click on Task Management Tab
                objppDetails.P2PPaymentPlanNavigation();
            }
            catch (Exception selectNextApprovalTask)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(selectNextApprovalTask, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();   
        
        }

        [CodedStep("Search the Purchase Order 'ROW_2' Against Invoice Number 'Row_0_BW80_0'", RequiresSilverlight = true)]
        public void SearchPurchaseOrderROW_2()
        {           
       
            try
            {
                //Call the Method to Search for Purchase Order
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PMatchingSearchPurchaseOrder(drAPT["PurchaseOrderROW_2"].ToString());
            }
            catch (Exception searchPurchaseOrderROW_2)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchPurchaseOrderROW_2, Globals.testScriptName);
            }
            //Call again Coded Handle Busy Indicater
            HandleBusyIndicator();
        }

        [CodedStep("Go To LineLevel and SelectAll Lines from PO and Invoice", RequiresSilverlight = true)]
        public void LineLevelSelectAllPOInvoices()
        {

            try
            {
                //Call the Method to Search for Purchase Order
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PMatchingDetailsPOLineLevelSwitch();
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PMatchingSelectAllInvoicesPurchaseOrders(true);
            }
            catch (Exception lineLevelSelectAllPOInvoices)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(lineLevelSelectAllPOInvoices, Globals.testScriptName);
            }
            //Call again Coded Handle Busy Indicater
            HandleBusyIndicator();
        
        }
                
        [CodedStep("Confirm Manual Match", RequiresSilverlight = true)]
        public void ConfirmManualMatch()
        {
            try
            {
                //Call the Method to Search for Purchase Order
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PMatchingConfirmInvoice();
            }
            catch (Exception confirmManualMatch)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(confirmManualMatch, Globals.testScriptName);
            }
            //Call again Coded Handle Busy Indicater
            HandleBusyIndicator();
        }

        [CodedStep("Search the Purchase Order 'RGR_2' Against Invoice Number 'RowGR_0_BW80_0'", RequiresSilverlight = true)]
        public void SearchPurchaseOrderRGR_2LineLevelMatch()
        {

            try
            {
                //Call the Method to Search for Purchase Order
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PMatchingSearchPurchaseOrder(drAPT["PurchaseOrderRGR_2"].ToString());
            }
            catch (Exception searchPurchaseOrderRGR_2)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchPurchaseOrderRGR_2, Globals.testScriptName);
            }
            //Call again Coded Handle Busy Indicater
            HandleBusyIndicator();         
        }

        [CodedStep("Open GR Row '1' and First Invoice Line and Repeat for All Lines", RequiresSilverlight = true)]
        public void GRRowAndInvoiceLineLevelMatch()
        {

            try
            {
                //Call the Method to GRRowAndInvoiceLineLevelMatch
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PMatchingDetailsPOLineLevelSwitch();
                //Iterate Grid and Match GR ROW with Invoice Line
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PMatchingIterateGridAndMatchGRRowWithInvoiceLine();
                
            }
            catch (Exception gRRowAndInvoiceLineLevelMatch)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(gRRowAndInvoiceLineLevelMatch, Globals.testScriptName);
            }
            //Call again Coded Handle Busy Indicater
            HandleBusyIndicator();
            //Confirm All and Match
            ConfirmManualMatch();
        }

        [CodedStep("Search the Purchase Order 'HEA_2' Against Invoice Number 'Header_0_BW80_0'", RequiresSilverlight = true)]
        public void SearchPurchaseOrderHEA_2()
        {

            try
            {
                //Call the Method to Search for Purchase Order
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PMatchingSearchPurchaseOrder(drAPT["PurchaseOrderHEA_2"].ToString());
            }
            catch (Exception searchPurchaseOrderRGR_2)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchPurchaseOrderRGR_2, Globals.testScriptName);
            }
            //Call again Coded Handle Busy Indicater
            HandleBusyIndicator();                  
        }

        [CodedStep("Match The Invoice", RequiresSilverlight = true)]
        public void MatchInvoice()
        {

            try
            {
                //Call the Method to Search for Purchase Order
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PMatchingSelectSearchedPurchaseOrder(drAPT["PurchaseOrderHEA_2"].ToString());
            }
            catch (Exception matchInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(matchInvoice, Globals.testScriptName);
            }
            //Call again Coded Handle Busy Indicater
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

        [CodedStep("Search the Invoice 'Header_0_BW80_0' on Received Silo and Open Selected", RequiresSilverlight = true)]
        public void SearchInvoiceOnReceivedSilo()
        {
            try
            {
                //Use the Object to Access the Method for Search the Invoice
                searchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(drAPT["InvoiceNumberHEA"].ToString());
            }
            catch (Exception searchInvoiceOnReceivedSilo)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchInvoiceOnReceivedSilo, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();

            //Open Selected
            OpenSelectedInvoices();
        }

        [CodedStep("Validate The Invoice", RequiresSilverlight = true)]
        public void ValidateInvoice()
        {
            try
            {
                //Use the method for Send the Invoice for Validation
                p2pCreateInvoiceObj.P2PInvoiceAdministrationSendToValidation();
            }
            catch (Exception validateInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(validateInvoice, Globals.testScriptName);
            }
            //Call the existing coded step here for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate To Invoice Administration Search", RequiresSilverlight = true)]
        public void NavigateToInvoiceAdministrationSearch()
        {
            try
            {
                //Call the Method to Navigate To Search Page 
                p2pNavigationObj.NavigateInvoiceAdministrationToSearch();
            }
            catch (Exception navigateToInvoiceAdministrationSearch)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToInvoiceAdministrationSearch, Globals.testScriptName);
            }

            //Handle Busy Indicator
            HandleBusyIndicator();
        }

        [CodedStep("Clear and Refresh the Grid ", RequiresSilverlight = true)]
        public void ClearRefreshTheGrid()
        {

            ClearAllSearch();
            //Create an object for P2PInvoiceAdministrationToolbarActions
            var refreshGrid = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //Refresh the Grid
                refreshGrid.P2PInvoiceAdministrationRefreshInvoicePage(drAPT["Button_Text"].ToString());
            }
            catch (Exception clearRefreshTheGrid)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(clearRefreshTheGrid, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Search The Updated Invoice 'Row_0_BW80_0'", RequiresSilverlight = true)]
        public void SearchUpdatedInvoice()
        {
            try
            {
                //Use the Object to Access the Method "P2PInvoiceAdministrationFreeTextSearchToSearchInvoice"
                searchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(drAPT["InvoiceNumberROW"].ToString());
            }
            catch (Exception searchUpdatedInvoice)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchUpdatedInvoice, Globals.testScriptName);
            }

            //Handle Busy Indicator
            HandleBusyIndicator();
        }

        [CodedStep("Verify Invoice Status 'Storaging in progress'", RequiresSilverlight = true)]
        public void VerifyInvoiceStatus()
        {
            try
            {
                // Call the Method "P2InvoiceAdministration_Search_VerifyInvoiceStatus" for Verify the Status
                p2pInvoiceAdministrationVerificationObj.P2InvoiceAdministration_Search_VerifyInvoiceStatus(drAPT["InvoiceNumberROW"].ToString(), drAPT["VerifyFinalStatus"].ToString(), drAPT["Header_Name_Status"].ToString(), drAPT["Button_Text"].ToString());
            }
            catch (Exception verifyInvoiceStatus)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(verifyInvoiceStatus, Globals.testScriptName);
            }
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
