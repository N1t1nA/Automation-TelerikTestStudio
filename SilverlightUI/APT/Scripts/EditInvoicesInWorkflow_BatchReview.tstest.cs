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
using P2P.Testing.Shared.Class.InvoiceAdministration.ToolbarActions;
using P2P.Testing.Shared;
using System.Data;

namespace APT.Scripts
{

    public class EditInvoicesInWorkflow_BatchReview : BaseWebAiiTest
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
        
		// Add your test methods here...

         //@Test Case ID: 88877
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
      

        //Create an object for P2PInvoiceAdministrationInvoiceDetails class
        P2PInvoiceAdministrationInvoiceDetails p2pInvoiceAdministrationInvoiceDetailsObj = new P2PInvoiceAdministrationInvoiceDetails();

        //Create an object for P2PInvoiceAdministrationVerification class
        P2PInvoiceAdministrationVerification p2pInvoiceAdministrationVerificationObj = new P2PInvoiceAdministrationVerification();

        //Create an object for P2PExceptionHandler class
        P2PExceptionHandler exceptionHandler = new P2PExceptionHandler();

        //Constructor of Class
        public EditInvoicesInWorkflow_BatchReview()
        {           
            //Read the Data Row from XML 
            drAPT = objReadXml.Read_xml_file("APT_TestData.xml", "EditInvoicesInWorkflowBatchReview", "TestCase", "88877");

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

        [CodedStep("Select Exceptions Invoices From Overview", RequiresSilverlight = true)]
        public void SelectExceptionsInvoice()
        {

            try
            {
                //Method to Open Draft Invoice from Overview
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationOverviewInvoiceStatusesReceived(drAPT["InvoiceStatus"].ToString(), drAPT["ExceptionsInvoiceCountifDisabled"].ToString());
            }
            catch (Exception selectExceptionsInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(selectExceptionsInvoice, Globals.testScriptName);
            }

            //Call the Coded Step Handle Busy Indicator
            HandleBusyIndicator();

        }

        [CodedStep("Search Exceptions Invoice 'ReviewMissingRecipient_BW80_0'", RequiresSilverlight = true)]
        public void SearchInvoiceOnWorkflow()
        {
            try
            {
                //Use the Object to Access the Method for Search the Invoice
                searchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(drAPT["ExceptionsInvoiceNumber"].ToString());
            }
            catch (Exception searchInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchInvoice, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Open The Invoice 'ReviewMissingRecipient_BW80_0'", RequiresSilverlight = true)]
        public void OpenInvoiceOnWorkflowSilo()
        {
            //Create an object for shared class
            P2PInvoiceAdministrationToolbarActions openInvoice = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //Call the Method to Open the Selected Invoice         
                openInvoice.P2PInvoiceAdministrationOpenSelectedInvoice();
            }
            catch (Exception openInvoiceOnWorkflowSilo)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(openInvoiceOnWorkflowSilo, Globals.testScriptName);
            }
            //Handle Busy Indicator
            HandleBusyIndicator();
        }       

        [CodedStep("Click on Task Management Tab Change the Recepient'Juha Tillqvist' and Save", RequiresSilverlight = true)]
        public void ChangeRecepientAndSave()
        {
            try
            {
                //call the method to Assigned Plan to Recipient JariT
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationWorkflowTaskManagementAddNewTask(null, drAPT["ReferencePerson"].ToString(), drAPT["ReferencePerson"].ToString(),null);
            }
            catch (Exception AssignedPlanToSylvanderMatti)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(AssignedPlanToSylvanderMatti, Globals.testScriptName);
            }

            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Go Back to Main Page", RequiresSilverlight = true)]
        public void GoBackToInvoiceAdministration()
        {

            try
            {
                //call the method to click on Task Management Tab
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationBackToMainPage();
            }
            catch (Exception goBackToInvoiceAdministration)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(goBackToInvoiceAdministration, Globals.testScriptName);
            }

            //Call Coded Step for HandleBusyIndicator
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

        [CodedStep("Search Exceptions Invoice 'ReviewReferencePerson_BW80_0'", RequiresSilverlight = true)]
        public void SearchInvoiceOnWorkflowToForward()
        {
            try
            {
                //Use the Object to Access the Method for Search the Invoice
                searchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(drAPT["ExceptionsInvoiceNumberForward"].ToString());
            }
            catch (Exception searchInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchInvoice, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Forward Invoice 'ReviewReferencePerson_BW80_0' to 'Juha Tillqvist'", RequiresSilverlight = true)]
        public void ForwardInvoice()
        {
            //Create a object for P2PInvoiceAdministrationToolbarActions class           
            var invoiceForwarded = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //Use the Object to access the Method forward Invoice
                invoiceForwarded.P2PWorkflowForwardProcess(drAPT["ReferencePerson"].ToString(), drAPT["Comments"].ToString());
            }
            catch (Exception forwardApprovalPaymentPlan)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(forwardApprovalPaymentPlan, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
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
        public void SearchMyTasksInvoicesAssignedViaTaskManagement()
        {
            try
            {
                //Use the Object to Access the Method for Search the Invoice
                searchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(drAPT["ExceptionsInvoiceNumber"].ToString(), drAPT["ExceptionsInvoiceNumber"].ToString());
            }
            catch (Exception searchMyTasksInvoices)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchMyTasksInvoices, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }
        
        [CodedStep("Review the Invoice", RequiresSilverlight = true)]
        public void ReviewTheInvoice()
        {
            //Create a object for P2PInvoiceAdministrationToolbarActions class
            var toolbarAction = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //call the common function to click on Review button
                toolbarAction.P2P_Invoice_Administration_MyTasks_ToolBarActions(drAPTLocators["MyTasks_Invoice_Review_Button_AutomationID"].ToString());
            }
            catch (Exception reviewerReviewedTheInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(reviewerReviewedTheInvoice, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Select Recepient While Review", RequiresSilverlight = true)]
        public void SelectRecepient()
        {
            try
            {
                //Use the object to access the Method for Invoice Send To Reviewer               
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationSelectRecipients(drAPT["Recepient"].ToString());
            }
            catch (Exception invoiceSendToRecipients)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(invoiceSendToRecipients, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();           
        }

        [CodedStep("Search the Invoice Under My Tasks: Invoices, and Review ", RequiresSilverlight = true)]
        public void SearchMyTasksInvoicesForwardedAndReview()
        {
            try
            {
                //Use the Object to Access the Method for Search the Invoice
                searchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(drAPT["ExceptionsInvoiceNumberForward"].ToString(), drAPT["ExceptionsInvoiceNumberForward"].ToString());
            }
            catch (Exception searchMyTasksInvoices)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchMyTasksInvoices, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
            //Review the Invoice
            ReviewTheInvoice();
            //Select Recepient
            SelectRecepient();
        }

        [CodedStep("Navigate to Professional Mode", RequiresSilverlight = true)]
        public void NavigateToProfessionalMode()
        {
            try
            {
                //Call the Coded Step Navigate To Personel Mode Again
                p2pNavigationObj.NavigateToProfessionalMode();
            }
            catch (Exception navigateToProfessionalMode)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToProfessionalMode, Globals.testScriptName);
            }

            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Workflow Silo", RequiresSilverlight = true)]
        public void NavigateToWorkflow()
        {
            try
            {
                //Call the Coded Step Navigate To Personel Mode Again
                p2pNavigationObj.NavigateInvoiceAdministrationToWorkflow();
            }
            catch (Exception navigateToWorkflow)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToWorkflow, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Search and Open Invoice 'ReviewMissingRecipient_BW80_0' in Workflow Silo", RequiresSilverlight = true)]
        public void SearchInvoiceInWorkflowandOpen()
        {
            //Call the Coded Step ClearAllSearch & RefreshTheGrid
            ClearAllSearch();
            RefreshTheGrid();            
            SearchInvoiceOnWorkflow();
            OpenInvoiceOnWorkflowSilo();
        }

        [CodedStep("Change the Invoice Number to 'AUTO_ReviewMissingRecipient_BW80_0' and Save" , RequiresSilverlight = true)]
        public void ChangeInvoiceNumberAndSave()
        {
            //Create an object for shared class
            P2PInvoiceAdministrationToolbarActions saveInvoice = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //call the Method to Edit the Invoice
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationEditInvoiceNumber(drAPT["ExceptionsCorrectedInvoiceNumber"].ToString());
                saveInvoice.P2P_Invoice_Administration_InvoiceSave();
            }
            catch (Exception changeInvoiceNumberAndSave)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(changeInvoiceNumberAndSave, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Select Process - GeneratedProcess_BW80_ID ", RequiresSilverlight=true)]
        public void SelectProcess()
        {

            //Create an object for shared class
            P2PInvoiceAdministrationToolbarActions selectProc = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //call the Method to Select the Process                
                selectProc.P2PInvoiceAdministrationMoreActionsSelectProcess(drAPT["ProcessID"].ToString(), drAPT["Comments"].ToString());
            }
            catch (Exception selectProcess)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(selectProcess, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();        
        }

        [CodedStep("Repeat> Search and Open Forwarded Invoice 'ReviewReferencePerson_BW80_0' in Workflow Silo ", RequiresSilverlight = true)]
        public void SearchForwardedInvoiceInWorkflowandOpen()
        {
            //Call the Coded Step ClearAllSearch & RefreshTheGrid
            ClearAllSearch();
            RefreshTheGrid();
            SearchInvoiceOnWorkflowToForward();
            OpenInvoiceOnWorkflowSilo();
        }

        [CodedStep("Repeat>Change the Forwarded Invoice Number to 'AUTO_ReviewReferencePerson_BW80_0' and Save and Select Process", RequiresSilverlight = true)]
        public void ChangeForwardedInvoiceNumberSaveAndProcess()
        {
            //Create an object for shared class
            P2PInvoiceAdministrationToolbarActions saveInvoice = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //call the Method to Edit the Invoice
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationEditInvoiceNumber(drAPT["ExceptionsCorrectedInvoiceNumberForward"].ToString());
                saveInvoice.P2P_Invoice_Administration_InvoiceSave();
            }
            catch (Exception changeInvoiceNumberAndSave)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(changeInvoiceNumberAndSave, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();

            //Select the Process
            SelectProcess();
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

        [CodedStep("Search The Updated Invoice 'AUTO_ReviewMissingRecipient_BW80_0'", RequiresSilverlight = true)]
        public void SearchUpdatedInvoice()
        {
                             
            try
            {
                //Use the Object to Access the Method "P2PInvoiceAdministrationFreeTextSearchToSearchInvoice"
                searchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(drAPT["ExceptionsCorrectedInvoiceNumber"].ToString());
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
                p2pInvoiceAdministrationVerificationObj.P2InvoiceAdministration_Search_VerifyInvoiceStatus(drAPT["ExceptionsCorrectedInvoiceNumber"].ToString(), drAPT["VerifyFinalStatus"].ToString(), drAPT["Header_Name_Status"].ToString(), drAPT["Button_Text"].ToString());
            }
            catch (Exception verifyFreeTextSearch)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(verifyFreeTextSearch, Globals.testScriptName);
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
