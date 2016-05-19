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
using P2P.Testing.Shared.Class.WebShop;
using P2P.Testing.Shared.Class.InvoiceAdministration.Create;
using P2P.Testing.Shared.Class.Purchase;
using P2P.Testing.Shared;
using P2P.Testing.Shared.Class.InvoiceAdministration.Search;
using P2P.Testing.Shared.Class.InvoiceAdministration.InvoiceDetails;
using P2P.Testing.Shared.Class.PaymentPlans.Search;
using P2P.Testing.Shared.Class.InvoiceAdministration.ToolbarActions;
using P2P.Testing.Shared.Class.InvoiceAdministration;

namespace APT.Scripts
{

    public class Edit_Validate_DraftInvoice : BaseWebAiiTest
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

       //@Test Case ID: 88873
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
        public Edit_Validate_DraftInvoice()
        {           
            //Read the Data Row from XML 
            drAPT = objReadXml.Read_xml_file("APT_TestData.xml", "EditAndValidateDraftInvoice", "TestCase", "88873");        
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

        [CodedStep("Select Draft Invoices From Overview", RequiresSilverlight = true)]
        public void SelectDraftInvoice()
        { 

            try
            {
                //Method to Open Draft Invoice from Overview
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationOverviewInvoiceStatusesReceived(drAPT["InvoiceStatus"].ToString(), drAPT["DraftInvoiceCountifDisabled"].ToString());
            }
             catch (Exception openDraftInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(openDraftInvoice, Globals.testScriptName);
            }

            //Call the Coded Step Handle Busy Indicator
            HandleBusyIndicator();
        
        }

        [CodedStep("Search The Draft Invoice 'Draft_BW80_0'", RequiresSilverlight = true)]
        public void SearchDraftInvoiceOnReceivedSilo()
        {

            try
            {
                //Call the Method to Search for Invoice
                p2pPaymentPlansSearchObj.P2PPaymentPlansFreeTextSearch(drAPT["DraftInvoiceNumber"].ToString(), drAPT["DraftInvoiceNumber"].ToString());
            }
            catch (Exception searchDraftInvoiceOnReceivedSilo)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchDraftInvoiceOnReceivedSilo, Globals.testScriptName);
            }
            //Handle Busy Indicator
            HandleBusyIndicator();
        }

        [CodedStep("Open The Draft Invoice 'Draft_BW80_0'", RequiresSilverlight = true)]
        public void OpenDraftInvoiceOnReceivedSilo()
        {            
            //Create an object for shared class
            P2PInvoiceAdministrationToolbarActions openInvoice = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //Call the Method to Open the Selected Invoice         
                openInvoice.P2PInvoiceAdministrationOpenSelectedInvoice();
            }                     
            catch (Exception openDraftInvoiceOnReceivedSilo)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(openDraftInvoiceOnReceivedSilo, Globals.testScriptName);
            }
            //Handle Busy Indicator
            HandleBusyIndicator();
        }

        [CodedStep("Change the Invoice Number to 'AUTO_Draft_BW80_0", RequiresSilverlight = true)]
        public void ChangeInvoiceNumber()
        {
            try
            {
               //call the Method to Edit the Invoice
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationEditInvoiceNumber(drAPT["ValidateDraftInvoiceNumber"].ToString());        
            }        
            catch (Exception changeInvoiceNumber)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(changeInvoiceNumber, Globals.testScriptName);
            }        
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

        [CodedStep("Clear All Search Filters on Search Silo ", RequiresSilverlight = true)]
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
        }

        [CodedStep("Refresh the Grid ", RequiresSilverlight = true)]
        public void RefreshTheGrid()
        {
            //Create an object for P2PInvoiceAdministrationToolbarActions
            var refreshGrid = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //Refresh the Grid
                refreshGrid.P2PInvoiceAdministrationRefreshInvoicePage(drAPT["ValidateDraftInvoiceNumber"].ToString());
            }
            catch (Exception refreshTheGrid)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(refreshTheGrid, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Search The Updated Invoice 'AUTO_Draft_BW80_0'", RequiresSilverlight = true)]
        public void SearchUpdatedInvoice()
        {
            try
            {
                //Use the Object to Access the Method "P2PInvoiceAdministrationFreeTextSearchToSearchInvoice"
                searchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(drAPT["ValidateDraftInvoiceNumber"].ToString());
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
                p2pInvoiceAdministrationVerificationObj.P2InvoiceAdministration_Search_VerifyInvoiceStatus(drAPT["ValidateDraftInvoiceNumber"].ToString(),drAPT["VerifyFinalStatus"].ToString(), drAPT["Header_Name_Status"].ToString(), drAPT["Button_Text"].ToString());
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
