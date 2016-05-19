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
using P2P.Testing.Shared.Class;
using P2P.Testing.Shared.Class.InvoiceAdministration.Create;
using P2P.Testing.Shared.Class.InvoiceAdministration.Search;
using P2P.Testing.Shared.Class.PaymentPlans.Search;
using P2P.Testing.Shared.Class.InvoiceAdministration.InvoiceDetails;
using P2P.Testing.Shared.Class.InvoiceAdministration;
using P2P.Testing.Shared;
using P2P.Testing.Shared.Class.InvoiceAdministration.ToolbarActions;
using System.Data;
using P2P.Testing.Shared.Class.PaymentPlans.InvoiceDetails;
using System.Globalization;

namespace APT.Scripts
{

    public class RequestManualApprovalPaymentPlan_ProcessInvoiceManually : BaseWebAiiTest
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
        
		//@Test Case ID: 88892
        //@Developed By: nitinahu    

		 //Declare Global Variable
        private readonly DataRow drAPT, drAPTLocators;      

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
        public RequestManualApprovalPaymentPlan_ProcessInvoiceManually()
        {           
            //Read the Data Row from XML 
            drAPT = objReadXml.Read_xml_file("APT_TestData.xml", "RequestManualApprovalPaymentPlanAndProcessInvoiceManually", "TestCase", "88892");

            //Read the Data Row from XML 
            drAPTLocators = objReadXml.Read_xml_file("APT_TestData.xml", "FindLogic", "Locators", "AutomationID");

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

        [CodedStep("Search In Manual Matching PaymentPlan 'MatchPlan_0_BW80_0'", RequiresSilverlight = true)]
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

        [CodedStep("Open Manual Matching PaymentPlan 'MatchPlan_0_BW80_0'", RequiresSilverlight = true)]
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
        
        [CodedStep("Request Manual Approval", RequiresSilverlight = true)]
        public void RequestManualApproval()
        {
            //Create an object for shared class
            P2PInvoiceAdministrationToolbarActions reqManApproval = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //Call the Method to Request Manual Approval          
                reqManApproval.P2PRequestManualApproval(drAPT["UserForRequestManualApproval"].ToString(),drAPT["Comments"].ToString());
            }
            catch (Exception requestManualApproval)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(requestManualApproval, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();

            //Call Overview Page
            OpenOverviewPage();
        }

        [CodedStep("Navigate to Personal Mode", RequiresSilverlight = true)]
        public void NavigateToPersonalMode()
        {
            try
            {
                //Call the Coded Step Navigate To Personel Mode
                p2pNavigationObj.NavigateToPersonnelMode();
            }

            catch (Exception navigateToPersonalMode)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToPersonalMode, Globals.testScriptName);
            }

            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to MyTasks Page", RequiresSilverlight = true)]
        public void NavigateToMyTasks()
        {
            try
            {
                //Call the Method to Navigate To MyTasks Page
                p2pNavigationObj.NavigateToMyTasks();
            }
            catch (Exception navigateToMyTasks)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToMyTasks, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Personal Mode My Tasks: Invoices", RequiresSilverlight = true)]
        public void NavigateToMyTasksInvoice()
        {
            try
            {
                //Call the Method to Navigate To MyTasks Page
                p2pNavigationObj.NavigateMyTasksToInvoice();
            }
            catch (Exception navigateToMyTasksInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToMyTasksInvoice, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Search the Invoice Under My Tasks: Invoices", RequiresSilverlight = true)]
        public void SearchMyTasksInvoices()
        {
            try
            {
                //Use the Object to Access the Method for Search the Invoice
                searchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(drAPT["ManualMatchingPaymentPlanInvoiceNumber"].ToString(), drAPT["ManualMatchingPaymentPlanInvoiceNumber"].ToString());
            }
            catch (Exception searchMyTasksInvoices)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchMyTasksInvoices, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Create a Coding Rows in My Task Page", RequiresSilverlight = true)]
        public void CreateCodingRows()
        {
            try
            {   //Use the Object to Access the Method for Create Coding Row
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationMyTasksAddCodingRows(Convert.ToInt32(drAPT["Number_Of_Rows"].ToString()), drAPT["AccountCode"].ToString(), drAPT["CostCenterCode"].ToString(), Convert.ToDouble(drAPT["Net_Sum"], CultureInfo.InvariantCulture.NumberFormat), drAPT["Tax_Code"].ToString());
            }
            catch (Exception createCodingRows)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(createCodingRows, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Approve The Invoice", RequiresSilverlight = true)]
        public void ApproveTheInvoice()
        {
            //Create a object for P2PInvoiceAdministrationToolbarActions class
            var toolbarAction = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //call the common function to click on Approved button
                toolbarAction.P2P_Invoice_Administration_MyTasks_ToolBarActions(drAPTLocators["MyTasks_Invoice_Approve_Button_AutomationID"].ToString());
            }
            catch (Exception approveThePlan)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(approveThePlan, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Professional Mode", RequiresSilverlight = true)]
        public void NavigateToProfessionalMode()
        {
            //Call the Coded Step Navigate To Personel Mode Again
            p2pNavigationObj.NavigateToProfessionalMode();

            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Workflow Silo", RequiresSilverlight = true)]
        public void NavigateToWorkflow()
        {
            //Call the Coded Step Navigate To Personel Mode Again
            p2pNavigationObj.NavigateInvoiceAdministrationToWorkflow();

            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Clear All Search on Workflow Page ", RequiresSilverlight = true)]
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
        
        [CodedStep("Search Invoice in Workflow Silo 'MatchPlan_0_BW80_0' and Open Selected", RequiresSilverlight = true)]
        public void SearchInvoiceInWorkflow()
        {           

            try
            {
                //Call the Method to Navigate To Search Invoice 
                searchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(drAPT["ManualMatchingPaymentPlanInvoiceNumber"].ToString());
            }
            catch (Exception searchInvoiceInWorkflow)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchInvoiceInWorkflow, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();

            //Open the Invoice in Workflow
            OpenSelectedPaymentPlan();
        }

        [CodedStep("Click on Task Management Tab", RequiresSilverlight = true)]
        public void ClickOnTaskManagementTab()
        {
            try
            {
                //call the method to click on Task Management Tab
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationWorkflowTaskManagementTab();
            }
            catch (Exception clickOnTaskManagementTab)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(clickOnTaskManagementTab, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Cancel Approval Task", RequiresSilverlight = true)]
        public void CancelApprovalTask()
        {
            try
            {
                //call the method to click on Task Management Tab
                objppDetails.P2PPaymentPlanWorkflowTaskManagementCancelTask();
            }
            catch (Exception cancelApprovalTask)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(cancelApprovalTask, Globals.testScriptName);
            }
        }

        [CodedStep("Go Back to Main Page> Handle Unsaved Data Warning", RequiresSilverlight = true)]
        public void GoBackToInvoiceAdministration()
        {

            try
            {
                //call the method to click on Task Management Tab
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationBackToMainPage();
            }
            catch (Exception goBackToPaymentPlan)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(goBackToPaymentPlan, Globals.testScriptName);
            }         

        }

        [CodedStep("Save Unsaved Data Warning", RequiresSilverlight = true)]
        public void SaveUnsavedDataWarning()
        {
            try
            {
                //call the method to click on Task Management Tab
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationConfirmationDialogBox(true);
            }
            catch (Exception saveUnsavedDataWarning)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(saveUnsavedDataWarning, Globals.testScriptName);
            }

            //Call Coded Step for HandleBusyIndicator
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
