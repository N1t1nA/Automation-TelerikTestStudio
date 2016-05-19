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
using P2P.Testing.Shared;
using P2P.Testing.Shared.Class.PaymentPlans.InvoiceDetails;
using P2P.Testing.Shared.Class.InvoiceAdministration.Search;
using P2P.Testing.Shared.Class.PaymentPlans.Search;
using P2P.Testing.Shared.Class.InvoiceAdministration.ToolbarActions;
using P2P.Testing.Shared.Class.InvoiceAdministration.InvoiceDetails;

namespace APT.Scripts
{

    public class ChangeRecepient_HeaderReviewAndApproval_PaymentPlans : BaseWebAiiTest
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
        //@Test Case ID: 88885
        //@Developed By: nitinahu    

        //Declare Global Variable
        private readonly DataRow drAPT;

        //Create an object of EdXmlReader class   
        EdXmlReader objReadXml = new EdXmlReader();

        //Create an object for 'P2PNavigation' class
        P2PNavigation p2pNavigationObj = new P2PNavigation();

        //Create an object for P2PInvoiceAdministrationSearch class
        P2PInvoiceAdministrationSearch searchObj = new P2PInvoiceAdministrationSearch();

        //Create an object for P2PInvoiceAdministrationInvoiceDetails class
        P2PInvoiceAdministrationInvoiceDetails p2pInvoiceAdministrationInvoiceDetailsObj = new P2PInvoiceAdministrationInvoiceDetails();

        //Create an object for P2PPaymentPlansSearch class
        P2PPaymentPlansSearch p2pPaymentPlansSearchObj = new P2PPaymentPlansSearch();

        //Create an object for P2PPaymentPlansDetails class
        P2PPaymentPlansDetails objppDetails = new P2PPaymentPlansDetails();       

        //Create an object for P2PExceptionHandler class
        P2PExceptionHandler exceptionHandler = new P2PExceptionHandler();

        //Constructor of Class
        public ChangeRecepient_HeaderReviewAndApproval_PaymentPlans()
        {
            //Read the Data Row from XML 
            drAPT = objReadXml.Read_xml_file("APT_TestData.xml", "ChangeRecepientHeaderReviewAndApprovalPaymentPlan", "TestCase", "88885");

        }

        [CodedStep("Login to P2P Client", RequiresSilverlight = true)]
        public void LogintoP2P()
        {
            // Create an object for "P2PLogin" class
            var objlogin = new P2PLogin(ActiveBrowser);

            try
            {
                //Call  method "LoginToSilverlightClient" from the "P2PLogin" class                
                objlogin.LoginToSilverlightClient(drAPT["LoginUser"].ToString(), "");
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

        [CodedStep("Navigate to PaymentPlans", RequiresSilverlight = true)]
        public void NavigateToPaymentPlans()
        {
            try
            {
                //Call the Method to Navigate To PaymentPlans Page 
                p2pNavigationObj.NavigateToPaymentPlans();
            }
            catch (Exception navigateToPaymentPlans)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToPaymentPlans, Globals.testScriptName);
            }

            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Payment Plan Overview Page", RequiresSilverlight = true)]
        public void OpenOverviewPage()
        {
            try
            {
                //Call the method to Click Overview Page
                p2pNavigationObj.NavigatePaymentPlansToOverview();
            }
            catch (Exception openOverviewPage)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(openOverviewPage, Globals.testScriptName);
            }

            //Call the Coded Step Handle Busy Indicator
            HandleBusyIndicator();
        }

        [CodedStep("Filter Invoices From Organization 'BW80'", RequiresSilverlight = true)]
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

        [CodedStep("Select Pending Payment Plan From Overview", RequiresSilverlight = true)]
        public void SelectPendingPaymentPlans()
        {

            try
            {
                //Method to Open Draft Invoice from Overview
                objppDetails.P2PPaymentPlanOverviewInvoiceStatusesReceived(drAPT["PaymentPlanStatus"].ToString(), drAPT["PendingPaymentPlanCountifDisabled"].ToString());
            }
            catch (Exception selectPendingPaymentPlans)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(selectPendingPaymentPlans, Globals.testScriptName);
            }

            //Call the Coded Step Handle Busy Indicator
            HandleBusyIndicator();

        }

        [CodedStep("Search The HeaderReview Payment Plan '130_PaymentPlan_1001'", RequiresSilverlight = true)]
        public void SearchHeaderReviewPaymentPlanOnWorkflow()
        {
            try
            {
                //Call the Method to Search for Payment Plan
                p2pPaymentPlansSearchObj.P2PPaymentPlansFreeTextSearch(drAPT["HeaderReviewPaymentPlanNumber"].ToString());
            }
            catch (Exception searchHeaderReviewPaymentPlanOnWorkflow)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchHeaderReviewPaymentPlanOnWorkflow, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Open The HeaderReview Payment Plan '130_PaymentPlan_1001'", RequiresSilverlight = true)]
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

        [CodedStep("Click on Task Management Tab", RequiresSilverlight = true)]
        public void ClickOnTaskManagementTab()
        {
            try
            {
                //call the method to click on Task Management Tab
                objppDetails.P2PPaymentPlanClickOnTaskManagementTab();
            }
            catch (Exception clickOnTaskManagementTab)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(clickOnTaskManagementTab, Globals.testScriptName);
            }
        }

        [CodedStep("Change the Recepient 'Juha Tillqvist' under Task Management and Save", RequiresSilverlight = true)]      
        public void ChangeRecepientAndSave()
        {
            try
            {
                //call the method to Assigned Plan to Recipient JariT
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationWorkflowTaskManagementAddNewTask(drAPT["ReferencePerson"].ToString(), drAPT["ReferencePerson"].ToString(), drAPT["ReferencePerson"].ToString(), drAPT["ReferencePerson"].ToString());
            }
            catch (Exception AssignedPlanToSylvanderMatti)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(AssignedPlanToSylvanderMatti, Globals.testScriptName);
            }

            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Payment Plan Workflow Silo", RequiresSilverlight = true)]
        public void NavigateToPaymentPlanWorkflow()
        {
            try
            {
                //Navigate to Approved Silo 
                p2pNavigationObj.NavigatePaymentPlansToInWorkflow();
            }
            catch (Exception navigateToPaymentPlanWorkflow)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToPaymentPlanWorkflow, Globals.testScriptName);
            }

            //Call handled busy indicator
            HandleBusyIndicator();
        }

        [CodedStep("Search the Approval Payment Plan '50_PaymentPlan_1002'", RequiresSilverlight = true)]
        public void SearchApprovalPaymentPlan()
        {
            try
            {
                //Clear Search
                p2pPaymentPlansSearchObj.P2PPaymentPlanClearSearch();
                //Call the Method to Search for Payment Plan
                p2pPaymentPlansSearchObj.P2PPaymentPlansFreeTextSearch(drAPT["ApprovalPaymentPlanNumber"].ToString());
            }
            catch (Exception searchApprovalPaymentPlan)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchApprovalPaymentPlan, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();            
        }

        [CodedStep("Forward Approval Payment Plan '50_PaymentPlan_1002' to 'Juha Tillqvist'", RequiresSilverlight = true)]
        public void ForwardApprovalPaymentPlan()
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
