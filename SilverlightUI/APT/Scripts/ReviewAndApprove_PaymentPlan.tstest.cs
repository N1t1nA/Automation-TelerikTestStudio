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
using P2P.Testing.Shared;
using P2P.Testing.Shared.Class;
using P2P.Testing.Shared.Class.InvoiceAdministration.Search;
using P2P.Testing.Shared.Class.PaymentPlans.MyTasks;
using P2P.Testing.Shared.Class.InvoiceAdministration.ToolbarActions;

namespace APT.Scripts
{

    public class ReviewAndApprove_PaymentPlan : BaseWebAiiTest
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
        /*
            
        //@Test Case ID: 88886
           Pre-requisite Test Cases
         * *********************
         * Edit_Validate_SendToProcess_DraftPaymentPlan.tstest -TCID:88884
         * ChangeRecepient_HeaderReviewAndApproval_PaymentPlans.tstest  -TCID:88885      
         * *********************
        //@Developed By: nitinahu   */

        //Declare Global Variable
        private readonly DataRow drAPT1, drAPT2, drAPTLocators;

        //Create an object of EdXmlReader class   
        EdXmlReader objReadXml = new EdXmlReader();

        //Create an object for 'P2PNavigation' class
        P2PNavigation p2pNavigationObj = new P2PNavigation();

        //Create an object for P2PInvoiceAdministrationSearch class
        P2PInvoiceAdministrationSearch searchObj = new P2PInvoiceAdministrationSearch();

        //Create an object for P2PPaymentPlansMyTasks class
        P2PPaymentPlansMyTasks p2pPaymentPlansMyTasksObj = new P2PPaymentPlansMyTasks();     

        ////Create an object for P2PInvoiceAdministrationInvoiceDetails class
        //P2PInvoiceAdministrationInvoiceDetails p2pInvoiceAdministrationInvoiceDetailsObj = new P2PInvoiceAdministrationInvoiceDetails();

        ////Create an object for P2PPaymentPlansSearch class
        //P2PPaymentPlansSearch p2pPaymentPlansSearchObj = new P2PPaymentPlansSearch();

        ////Create an object for P2PPaymentPlansDetails class
        //P2PPaymentPlansDetails objppDetails = new P2PPaymentPlansDetails();       

        //Create an object for P2PExceptionHandler class
        P2PExceptionHandler exceptionHandler = new P2PExceptionHandler();

        //Constructor of Class
        public ReviewAndApprove_PaymentPlan()
        {
            
            //Read the Data Row from XML 
            drAPT1 = objReadXml.Read_xml_file("APT_TestData.xml", "EditAndValidateSendToProcessPaymentPlan", "TestCase", "88884");
            //Read the Data Row from XML 
            drAPT2 = objReadXml.Read_xml_file("APT_TestData.xml", "ChangeRecepientHeaderReviewAndApprovalPaymentPlan", "TestCase", "88885");
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
                objlogin.LoginToSilverlightClient(drAPT1["LoginUser"].ToString(), "");
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

        [CodedStep("Navigate to MyTask Page", RequiresSilverlight = true)]
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

        [CodedStep("Navigate to Payment Plan in My Tasks: Personal Mode", RequiresSilverlight = true)]
        public void NavigateToPaymentPlan()
        {
            try
            {
                //Call the Method to Navigate to Payment Plan
                p2pNavigationObj.NavigateToMyTaskPaymentPlanPage();
            }
            catch (Exception navigateToPaymentPlan)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToPaymentPlan, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Search the Payment Plan '50_PaymentPlan_1002' under My Tasks>Payment Plan", RequiresSilverlight = true)]
        public void SearchApprovalPlanonMyTaskPaymentPlan()
        {
            try
            {
                //Call the method to search the plan
                searchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(drAPT2["ApprovalPaymentPlanNumber"].ToString());

            }
            catch (Exception searchApprovalPlanonMyTaskPaymentPlan)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchApprovalPlanonMyTaskPaymentPlan, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Approve The Plan", RequiresSilverlight = true)]
        public void ApproveThePlan()
        {
            //Create a object for P2PInvoiceAdministrationToolbarActions class
            var toolbarAction = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //call the common function to click on Approved button
                toolbarAction.P2P_Invoice_Administration_MyTasks_ToolBarActions(drAPTLocators["MyTasks_PaymentPlan_Approved_Button_AutomationID"].ToString());
            }
            catch (Exception approveThePlan)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(approveThePlan, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Search the Payment Plan '130_PaymentPlan_1001' under My Tasks>Payment Plan", RequiresSilverlight = true)]
        public void SearchReviewPlanonMyTaskPaymentPlan()
        {
            try
            {
                //Call the method to search the plan
                searchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(drAPT2["HeaderReviewPaymentPlanNumber"].ToString());

            }
            catch (Exception searchReviewPlanonMyTaskPaymentPlan)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchReviewPlanonMyTaskPaymentPlan, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Review the Payment Plan", RequiresSilverlight = true)]
        public void ReviewThePaymentPlan()
        {            
            //Create a object for P2PInvoiceAdministrationToolbarActions class
            var toolbarAction = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //call the common function to click on Review button
                toolbarAction.P2P_Invoice_Administration_MyTasks_ToolBarActions(drAPTLocators["MyTasks_PaymentPlan_Reveiw_Button_AutomationID"].ToString());
            }
            catch (Exception searchAndReviewThePaymentPlan)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchAndReviewThePaymentPlan, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Search Payment Plan '10_PaymentPlan_1000' under My Tasks>Payment Plan and Review", RequiresSilverlight = true)]
        public void SearchAnotherReviewPlanonMyTaskPaymentPlan()
        {
            try
            {
                //Call the method to search the plan
                searchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(drAPT1["DraftPaymentPlanNumber"].ToString());

            }
            catch (Exception searchAnotherReviewPlanonMyTaskPaymentPlan)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchAnotherReviewPlanonMyTaskPaymentPlan, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
            //Review Payment Plan
            ReviewThePaymentPlan();
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
