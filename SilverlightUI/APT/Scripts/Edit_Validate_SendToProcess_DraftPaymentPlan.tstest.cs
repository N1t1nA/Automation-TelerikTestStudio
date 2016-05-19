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
using System.Data;
using P2P.Testing.Shared;
using P2P.Testing.Shared.Class.PaymentPlans.InvoiceDetails;
using P2P.Testing.Shared.Class.InvoiceAdministration.ToolbarActions;
using P2P.Testing.Shared.Class.PaymentPlans.ToolbarActions;

namespace APT.Scripts
{

    public class Edit_Validate_SendToProcess_DraftPaymentPlan : BaseWebAiiTest
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

        //@Test Case ID: 88884
        //@Developed By: nitinahu    

        //Declare Global Variable
        private readonly DataRow drAPT;

        //Create an object of EdXmlReader class   
        EdXmlReader objReadXml = new EdXmlReader();

        //Create an object for 'P2PNavigation' class
        P2PNavigation p2pNavigationObj = new P2PNavigation();

        //Create an object for P2PInvoiceAdministrationSearch class
        P2PInvoiceAdministrationSearch searchObj = new P2PInvoiceAdministrationSearch();

        //Create an object for P2PPaymentPlansSearch class
        P2PPaymentPlansSearch p2pPaymentPlansSearchObj = new P2PPaymentPlansSearch();

        //Create an object for P2PPaymentPlansDetails class
        P2PPaymentPlansDetails objppDetails = new P2PPaymentPlansDetails();

        //Create an object for P2PInvoiceAdministrationInvoiceDetails class
        P2PInvoiceAdministrationInvoiceDetails p2pInvoiceAdministrationInvoiceDetailsObj = new P2PInvoiceAdministrationInvoiceDetails();        

        //Create an object for P2PExceptionHandler class
        P2PExceptionHandler exceptionHandler = new P2PExceptionHandler();

        //Constructor of Class
        public Edit_Validate_SendToProcess_DraftPaymentPlan()
        {
            //Read the Data Row from XML 
            drAPT = objReadXml.Read_xml_file("APT_TestData.xml", "EditAndValidateSendToProcessPaymentPlan", "TestCase", "88884");

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

        [CodedStep("Select Draft Payment Plan From Overview", RequiresSilverlight = true)]
        public void SelectDraftPaymentPlan()
        {

            try
            {
                //Method to Open Draft Invoice from Overview
                objppDetails.P2PPaymentPlanOverviewInvoiceStatusesReceived(drAPT["PaymentPlanStatus"].ToString(), drAPT["DraftPaymentPlanCountifDisabled"].ToString());
            }
            catch (Exception openDraftInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(openDraftInvoice, Globals.testScriptName);
            }

            //Call the Coded Step Handle Busy Indicator
            HandleBusyIndicator();

        }

        [CodedStep("Search The Payment Plan '10_PaymentPlan_1000'", RequiresSilverlight = true)]
        public void SearchPaymentPlanOnReceivedSilo()
        {
            try
            {
                //Call the Method to Search for Payment Plan
                p2pPaymentPlansSearchObj.P2PPaymentPlansFreeTextSearch(drAPT["DraftPaymentPlanNumber"].ToString());
            }
            catch (Exception searchPaymentPlanOnReceivedSilo)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchPaymentPlanOnReceivedSilo, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Open The Payment Plan '10_PaymentPlan_1000'", RequiresSilverlight = true)]
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

        [CodedStep("Add 'Juha Tillqvist' as Reference Persion", RequiresSilverlight = true)]
        public void AddReferencePerson()
        {
            try
            {
                //call the Method to Edit the Invoice
                objppDetails.P2PPaymentPlanAddReferencePerson(drAPT["ReferencePerson"].ToString());

            }
            catch (Exception addReferencePerson)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(addReferencePerson, Globals.testScriptName);
            }
        }

        [CodedStep("Click on Validate Button", RequiresSilverlight = true)]
        public void ValidatePaymentPlan()
        {
            //Create an object for P2PPaymentPlansToolbarActions class
            P2PPaymentPlansToolbarActions p2pPaymentPlansToolbarActionsObj = new P2PPaymentPlansToolbarActions(ActiveBrowser);

            try
            {
                //Call the Method to Create New Coding Row
                p2pPaymentPlansToolbarActionsObj.P2PPaymentPlanClickOnValidateButton(drAPT["Validate_Button"].ToString());
            }
            catch (Exception sendToValidatePaymentPlan)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(sendToValidatePaymentPlan, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Payment Plan Received Silo", RequiresSilverlight = true)]
        public void NavigateToPaymentPlanReceived()
        {
            try
            {
                //Call the Method to Navigate To Payment Plan Received 
                p2pNavigationObj.NavigatePaymentPlansToReceived();
            }
            catch (Exception navigatePaymentPlansToReceived)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigatePaymentPlansToReceived, Globals.testScriptName);
            }
            //Handle Busy Indicator
            HandleBusyIndicator();
        }

        [CodedStep("Search the Validated Payment Plan '10_PaymentPlan_1000' and Open Selected", RequiresSilverlight = true)]
        public void SearchValidatedPaymentPlanAndOpen()
        {
            try
            {
                //Call the Method to Search for Payment Plan
                p2pPaymentPlansSearchObj.P2PPaymentPlanClearSearch();
                p2pPaymentPlansSearchObj.P2PPaymentPlansFreeTextSearch(drAPT["DraftPaymentPlanNumber"].ToString());
            }
            catch (Exception searchValidatedPaymentPlan)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchValidatedPaymentPlan, Globals.testScriptName);
            }
            //Handle Busy Indicator
            HandleBusyIndicator();

            //Open the Payment Plan
            OpenSelectedPaymentPlan();
        }

        [CodedStep("Click on SendToProcess Button", RequiresSilverlight = true)]
        public void SendToProcessPaymentPlan()
        {
            try
            {
                //Call the Method to SendToProcess Selected Plan 
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PSendInvoiceToProcess(drAPT["Send_To_Process_Button"].ToString());
            }
            catch (Exception ClickOnSendToProcessButton)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(ClickOnSendToProcessButton, Globals.testScriptName);
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