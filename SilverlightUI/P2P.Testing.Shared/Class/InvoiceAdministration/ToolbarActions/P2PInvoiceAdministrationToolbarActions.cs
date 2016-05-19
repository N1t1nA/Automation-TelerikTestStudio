using System;
using System.Linq;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Win32;
using ArtOfTest.WebAii.Win32.Dialogs;
using System.Windows.Forms;
using ArtOfTest.WebAii.Silverlight.UI;
using System.Threading;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.Common.Exceptions;
using Telerik.WebAii.Controls.Xaml;

namespace P2P.Testing.Shared.Class.InvoiceAdministration.ToolbarActions
{
    public class P2PInvoiceAdministrationToolbarActions : BaseWebAiiTest
    {
        private Browser _browser;
        #region [ Dynamic Pages Reference for P2P.Testing.Shared]

        private P2P.Testing.Shared.Pages _sharedElement;

        /// <summary>
        /// Gets the Pages object that has references
        /// to all the elements, frames or regions
        /// in this project.
        /// </summary>
        public P2P.Testing.Shared.Pages SharedElement
        {
            get
            {
                if (_sharedElement == null)
                {
                    _sharedElement = new P2P.Testing.Shared.Pages(Manager.Current);
                }
                return _sharedElement;
            }
        }

        #endregion
      
        public P2PInvoiceAdministrationToolbarActions(Browser browser)
        {
            _browser = browser;
        }

        //Method to Export Invoice to Excel 
        public void P2PInvoiceAdministrationExportToExcel(string activeDirectory, string filePath, string personalModeSearchExportExcel=null)
        {
            //Create a Directory Under C: drive with Name TestAutomation
            System.IO.Directory.CreateDirectory(activeDirectory);

            //Change the Current Settings 
            Manager.Current.Settings.UnexpectedDialogAction = UnexpectedDialogAction.DoNotHandle;
          
            //Handle SaveAs Dialog 
            SaveAsDialog saveAsDialog = SaveAsDialog.CreateSaveAsDialog(Manager.Current.ActiveBrowser, DialogButton.SAVE, filePath, Manager.Current.Desktop);

            //Add the SaveAs Dialog
            Manager.Current.DialogMonitor.AddDialog(saveAsDialog);

            //Initiated the DialogMonitor
            Manager.Current.DialogMonitor.Start();

            if(personalModeSearchExportExcel !=null)
            {
                //Wait for Export To Excel Toolbar Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PaymentPlans_ExportToExcel.Wait.ForExists(Globals.timeOut);

                //Click on Export To Excel Toolbar Button 
                SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PaymentPlans_ExportToExcel.User.Click();
            }
            else
            {
                //Wait for ExportToExcel Toolbarbutton Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ExportToExcelButton.Wait.ForExists(Globals.timeOut);

                //Click ExportToExcelToolbarbutton
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ExportToExcelButton.User.Click();
            }  

            //Handle the saveAsDialog
            saveAsDialog.WaitUntilHandled(Globals.navigationTimeOut);                   
               
            //Stops the DialogMonitor
            Manager.Current.DialogMonitor.Stop();                
            
            //calling handle busy indicator
            P2PNavigation.CallBusyIndicator();
        }

        //Method to Add Comment in Invoice
        public void P2PInvoiceAdministrationAdditionalDataAddComments(string comment)
        {                           
         
            //Wait for Additional Actions Dropdown button Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Click the Additional Actions drop down
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();           

            //Click on Add Comment Button From Additional Actions Dropdown
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_AddComment_DropdownButton.User.Click();

            //Wait for Add Comment Popup to appears 
            P2PNavigation.CallBusyIndicator();

            //Sets the Focus to Add Comment Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.SetFocus();

            //Enter the Comment in AddComment TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(comment, 20, true);
            
            //Wait for OK Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            // Click on OKButton after added a comment
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
        }

        //Method to Select Process of the Invoice
        public void P2PInvoiceAdministrationMoreActionsSelectProcess(string processID, string comment)
        {

            //Wait for Additional Actions Dropdown button Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Click the Additional Actions drop down
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            //Wait for Select Process Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MoreActions_SelectProcessButton.Wait.ForExists(Globals.timeOut);

            //Click on SelectProcess Button From Additional Actions Dropdown
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MoreActions_SelectProcessButton.User.Click();          

            //Wait for Selct Process Combobox exits in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SelectProcess_ProcessesCombobox.Wait.ForExists(Globals.timeOut);

            //Find the Process 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SelectProcess_ProcessesCombobox.OpenDropDown(true);
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SelectProcess_ProcessesCombobox.SelectItemByText(true, processID);
                        
            //Sets the Focus to Add Comment Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.SetFocus();

            //Enter the Comment in AddComment TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(comment, 20, true);

            //Wait for OK Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            // Click on OKButton after added a comment
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
        }

        //Method to Clear the Transfer Search Textbox
        public void P2PInvoiceAdministrationClearSearch(string paymentPlanPersonalSearch = null)
        {
            //Wait for Search Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Wait.ForExists(Globals.timeOut);

            //Set Focus on set box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.SetFocus();

            //Perform Click event under text box for set the focus 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.User.Click();

            //Press Ctrl A
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A")); 
            
            //Generate a Keyboard Event to clear search
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

            if (paymentPlanPersonalSearch != null)
            {
                //Generate a Keyboard Event to clear search
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Enter);
            }
        }  

        //Method to Clear the Transfer Search Textbox
        public void P2PInvoiceAdministrationClearSearch_Textbox()
        {
            //Wait for Search Textbox to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.Wait.ForExists(Globals.timeOut);

            //Clear Search Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.SetFocus();

            //Press Ctrl A
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A")); 

            //Generate a Keyboard Event to clear search
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);
        } 
        
        //Method to Clear the Company Search Textbox
        public void P2PInvoiceAdministrationClearCompany()
        {
            //Wait for Company Textbox to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.Wait.ForExists(Globals.timeOut);

            //Set focus on P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.SetFocus();

            //Generate mouse event to click on P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.User.Click();

            //Press Ctrl A
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

            //Generate a Keyboard Event to clear search
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);
        }

        //Method for  Refresh the Invoice Grid 
        public void P2PInvoiceAdministrationRefreshInvoicePage(string noRefreshButton = null)
        {
            //Case: when no refresh button is available on screen. Then click on search magnifying button.
            if (noRefreshButton != null)
            {
                //Wait for Search Button to Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.Wait.ForExists(Globals.timeOut);

                //Click on Search button 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();
            }
            else
            {
                // Wait for Refresh Button Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_RefreshButton.Wait.ForExists(Globals.timeOut);

                //Click on Refresh Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_RefreshButton.User.Click();
            }
        }

        //Method for  Refresh the Invoice Grid 
        public void P2PInvoiceAdministrationSearchRefreshGrid()
        {
            // Wait for Refresh Button Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_RefreshButton.Wait.ForExists(Globals.timeOut);

            //Click on Refresh Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_RefreshButton.User.Click();
        }


        //Method to Open Selected Invoice from the Main Page
        public void P2PInvoiceAdministrationOpenSelectedInvoice(string paymentPlanOpenselectedbutton= null)
        {            
            //Wait for Open Selected Toolbar Button Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_OpenSelectedButton.Wait.ForExists(Globals.timeOut);

            //Click on Open Selected Toolbarbutton to Open the Selected Invoice
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_OpenSelectedButton.User.Click();           
        }

        //Method to Open Selected Invoice from the Main Page
        public void P2PInvoiceAdministrationSearchOpenSelectedInvoice()
        {
            //Wait for Open Selected Toolbar Button Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_OpenSelectedbutton.Wait.ForExists(Globals.timeOut);

            //Click on Open Selected Button to Open the Selected Invoice
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_OpenSelectedbutton.User.Click();
        }

        //Method to Sent Invoice for Prebooking
        public void P2PInvoiceAdministrationSendToPreebook(Boolean prebookButton, Boolean matchingPrebookButton)
        {

            //If condition is true then execute if block
            if (prebookButton == true)
            {
                //Wait for AdditionalActionsDropdown Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

                //Set focus on AdditionalActionsDropdown
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.SetFocus();
                
                //Click on AdditionalActionsDropdown 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

                //Wait for Prebooked_DropdownButton Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Prebooked_DropdownButton.Wait.ForExists(Globals.timeOut);
                //Click on Prebooked_DropdownButton 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Prebooked_DropdownButton.User.Click();
            }

            if (matchingPrebookButton == true)
            {
                //Wait for AdditionalActionsDropdown Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);
                //Click on AdditionalActionsDropdown 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

                //Wait for Prebooked_DropdownButton Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskPutOnHoldButton.Wait.ForExists(Globals.timeOut);
                //Click on Prebooked_DropdownButton 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskPutOnHoldButton.User.Click();
            }
 
        }

        //Method to Reset Unclear Status
        public void P2PInvoiceAdministrationResetUnclear()
        {
            //Wait for Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_Status_UnclearCheckBox.Wait.ForExists(Globals.timeOut);
            //Click on Unclear Checkbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_Status_UnclearCheckBox.User.Click();        
        }

        //Method to Press Arrow Keys to make invoice column visible
        public void P2PPRessRightArrowKey()
        {
            //1. Put focus on Invoice List GridView
            //2. Press Arrow Keys 10 times

            //Wait for WorkFlow Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_InvoicesGridViewControl.Wait.ForExists(Globals.timeOut);
            //SetFocus on Received Grid Control by clicking inside the grid
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_InvoicesGridViewControl.User.Click();

            //Press Arrow Keys 10 times
            for(int i = 0; i < 10 ; i++)
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Right);
        }

        //Method to Select Multiple Invoices
        public void P2PInvoiceAdministrationSelectMultipleInvoices(string headerCell, string invoiceNum1, string invoiceNum2, string invoiceNum3, Boolean received, Boolean matching, Boolean workflow, Boolean transfer, Boolean search)
        {
            //Condition for Received
            if (received == true)
            {
                //Wait for Received Grid Control Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_InvoicesGridViewControl.Wait.ForExists(Globals.timeOut);

                //Find the Header cell Value as Invoice Number
                TextBlock invoiceNumberHeaderCell = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_InvoicesGridViewControl.Find.ByTextContent(headerCell).As<TextBlock>();
                
                //Click on Invoice Number Header Cell for Sorting the Invoice
                invoiceNumberHeaderCell.User.Click();
            }

            //Condition for Matching
            if (matching == true)
            {
                //Wait for Matching Grid Control Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_InvoicesGridViewControl.Wait.ForExists(Globals.timeOut);

                //Find the Header cell Value as Invoice Number
                TextBlock invoiceNumberHeaderCell = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_InvoicesGridViewControl.Find.ByTextContent(headerCell).As<TextBlock>();
                
                //Click on Invoice Number Header Cell for Sorting the Invoice
                invoiceNumberHeaderCell.User.Click();
            }

            //Condition for Workflow
            if (workflow == true)
            {
                //Wait for WorkFlow Grid Control Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol")).Wait.ForExists(Globals.timeOut);

                //Find the Header cell Value as Invoice Number
                TextBlock invoiceNumberHeaderCell = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol")).Find.ByTextContent(headerCell).As<TextBlock>();
                
                //Click on Invoice Number Header Cell for Sorting the Invoice
                invoiceNumberHeaderCell.User.Click();
            }

            //Condition for Transfer
            if(transfer==true)
            {
                //declare RadGirdView
                GridViewVirtualizingPanel rgv = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=PART_GridViewVirtualizingPanel")).As<GridViewVirtualizingPanel>();

                //Wait for Invoice Types Grid Control for Exists in DOM 
                rgv.Wait.ForExists(Globals.timeOut);

                GridViewHeaderRow gridHeaderRow = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=Row_-1")).CastAs<GridViewHeaderRow>();

                //Changed the FindLogic 
                TextBlock invoiceNumberHeaderCell = gridHeaderRow.Find.ByTextContent(headerCell).As<TextBlock>();

                //Click the TextBlock to Sort
                invoiceNumberHeaderCell.User.Click();

                ////Wait for Transfer Grid Control Exists in DOM
                //SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_InvoicesGridViewControl.Wait.ForExists(Globals.timeOut);

                ////Find the Header cell Value as Invoice Number
                //TextBlock invoiceNumberHeaderCell = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_InvoicesGridViewControl.Find.ByTextContent(headerCell).As<TextBlock>();
                
                ////Click on Invoice Number Header Cell for Sorting the Invoice
                //invoiceNumberHeaderCell.User.Click();
            }

            //Condition for Search
            if (search == true)
            {

                //Wait for Search Grid Control Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_InvoicesGridViewControl.Wait.ForExists(Globals.timeOut);

                //Find the Header cell Value as Invoice Number
                TextBlock invoiceNumberHeaderCell = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_InvoicesGridViewControl.Find.ByTextContent(headerCell).As<TextBlock>();
                
                //Click on Invoice Number Header Cell for Sorting the Invoice
                invoiceNumberHeaderCell.User.Click();
            }

            //Find the Invoice Numbers in Transfer Grid Control 
            TextBlock invoice1 = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(invoiceNum1).As<TextBlock>();
            TextBlock invoice2 = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(invoiceNum2).As<TextBlock>();
            TextBlock invoice3 = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(invoiceNum3).As<TextBlock>();


            //Click One Invoice to Clear the Default Selection
            invoice1.User.Click();

            //Press the Control Key down
            Manager.Current.Desktop.KeyBoard.KeyDown(Keys.Control);

            //Click on the Multiple rows you want to select 
            invoice2.User.Click();

            //Click on the Multiple rows you want to select 
            invoice3.User.Click();

            //Release the Control Key Up
            Manager.Current.Desktop.KeyBoard.KeyUp(Keys.Control);
        }

        //Method is used in ***APT*** Scripts
        public void P2PInvoiceAdministrationMultipleInvoices_Select(string headerCell, string invoiceNum1, string invoiceNum2, string invoiceNum3 = null, string invoiceNum4 = null, string invoiceNumberColumn = null,string editFunctionality=null)
        {
            try
            {
                //Declare testblock to store invoice number column 
                var invoiceNumberHeaderCell = new TextBlock();

                //Get the invoice grid on page
                FrameworkElement e = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol"));

                //assert that grid is not null
                Assert.IsNotNull(e);

                //Find the Header cell Value as Invoice Number
                invoiceNumberHeaderCell = e.Find.ByTextContent(headerCell).As<TextBlock>();

                if (invoiceNumberColumn == null)
                {
                    /*Workaround Starts to get visibilty of the Invoice Number column*/
                    //Set focus on grid
                    e.SetFocus();

                    //Press End Key
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.End);
                    /*Workaround Ends to get visibilty of the Invoice Number column*/
                }

                //Click on Invoice Number Header Cell for Sorting the Invoice Column
                invoiceNumberHeaderCell.User.Click();

                //Click on header cell for sorting the records
                if (editFunctionality!=null)
                {
                    invoiceNumberHeaderCell.Wait.ForExists(Globals.timeOut);
                    invoiceNumberHeaderCell.User.Click();
                }
                //get all invoice numbers
                TextBlock invoice1 = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(invoiceNum1).As<TextBlock>();
                TextBlock invoice2 = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(invoiceNum2).As<TextBlock>();

                //Click One Invoice to Clear the Default Selection
                invoice1.User.Click();

                //Press the Control Key down
                Manager.Current.Desktop.KeyBoard.KeyDown(Keys.Control);

                //Click on the Multiple rows you want to select 
                invoice2.User.Click();

                //if there is a need to select 4th invoice then pass value for invoiceNum4
                if (invoiceNum3 != null)
                {
                    TextBlock invoice3 = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(invoiceNum3).As<TextBlock>();

                    //Click on the Multiple rows you want to select 
                    invoice3.User.Click();
                }

                //if there is a need to select 4th invoice then pass value for invoiceNum4
                if (invoiceNum4 != null)
                {
                    //Get invoice number
                    TextBlock invoice4 = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(invoiceNum4).As<TextBlock>();
                    
                    //Click on Invoice
                    invoice4.User.Click();
                }

                //Release the Control Key Up
                Manager.Current.Desktop.KeyBoard.KeyUp(Keys.Control);
            }
            catch (Exception)
                {
                    throw new Exception("Invoice Grid not found on page.");    
                }
        }
               
        //Method to put Invoice on Reserve
        public void P2PReserveInvoice(string reserveComment)
        {
            //Wait for P2P_Invoice_Administration_AdditionalActionsDropdown Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            // Click on P2P_Invoice_Administration_AdditionalActionsDropdown
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            //Wait for Reserve Button Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_ReserveToolbarActionButton.Wait.ForExists(Globals.timeOut);

            //Click on Reserve Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_ReserveToolbarActionButton.User.Click();

            //Wait for Add Comment Textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);

            //Set focus on Add Comment Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.SetFocus();

            //Click on Add Comment Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.Click();

            //Input comment in Add Comment Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(reserveComment, 50);

            //Wait for OK Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
        }

        //Method to Release Reserved Invoice
        public void P2PReleaseReservedInvoice(string reserveComment = null)
        {
            //Wait for P2P_Invoice_Administration_AdditionalActionsDropdown Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            // Click on P2P_Invoice_Administration_AdditionalActionsDropdown
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            //Wait for Reserve Button Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_ReleaseToolbarActionButton.Wait.ForExists(Globals.timeOut);

            //Click on Reserve Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_ReleaseToolbarActionButton.User.Click();

            if (reserveComment != null)
            {
                //Wait for Add Comment Textbox Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);

                //Set focus on Add Comment Textbox
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.SetFocus();

                //Click on Add Comment Textbox
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.Click();

                //Input comment in Add Comment Textbox
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(reserveComment, 50);

                //Wait for OK Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

                //Click on OK Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
            }
        }

        //Method to Request manual Approval
        public void P2PRequestManualApproval(string selectUser, string comment)
        {
            //Wait for Addtional Actions Dropdown Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Set focus on Addtional Actions Dropdown 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.SetFocus();

            //Click on Addtional Actions Dropdown 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            //Wait for P2P_Invoice_Administration_Matching_RequestManualApprovalToolbarButton Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_RequestManualApprovalToolbarButton.Wait.ForExists(Globals.timeOut);

            //Setfocus on P2P_Invoice_Administration_Matching_RequestManualApprovalToolbarButton
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_RequestManualApprovalToolbarButton.SetFocus();

            //Click P2P_Invoice_Administration_Matching_RequestManualApprovalToolbarButton
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_RequestManualApprovalToolbarButton.User.Click();

            //Wait for P2P_Invoice_Administration_Workflow_RecepientName_TextBox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_RecepientName_TextBox.Wait.ForExists(Globals.timeOut);

            //Setfocus on P2P_Invoice_Administration_Workflow_RecepientName_TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_RecepientName_TextBox.SetFocus();

            //Click P2P_Invoice_Administration_Workflow_RecepientName_TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_RecepientName_TextBox.User.Click();

            //Press Ctrl A
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));           

            //Generate a Keyboard Event to clear search
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

            //Input select user
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_RecepientName_TextBox.User.TypeText(selectUser, 50);

            //Press Tab
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Wait for P2P_Invoice_Administration_AddComment_TextBox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);

            //Setfocus on P2P_Invoice_Administration_AddComment_TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.SetFocus();

            //Click P2P_Invoice_Administration_AddComment_TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.Click();

            //Input comment
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(comment, 50);

            //Wait for P2P_Invoice_Administration_AddComment_OKButton Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);

            //Setfocus on P2P_Invoice_Administration_AddComment_OKButton
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.SetFocus();

            //Click P2P_Invoice_Administration_AddComment_OKButton
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
        }

        //Method to Forward a Task
        public void P2PWorkflowForwardProcess(string selectUser, string comments, string otherModule = null)
        {            
            //Wait for Forward Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_ForwardButton.Wait.ForExists(Globals.timeOut);

            //Click on Forward Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_ForwardButton.User.Click();            

            //Wait for Browse Button to Browse the User Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_AddRecipientBrowseButton.Wait.ForExists(Globals.timeOut);

            //Click on Browse Button to Browse the User
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_AddRecipientBrowseButton.User.Click();

            //Wait for Search Textbox Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_SearchUserTextbox.Wait.ForExists(Globals.timeOut);

            //Type the Text to Search for Users
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_SearchUserTextbox.User.TypeText(selectUser, 20);

            //In case of Payment Plan module search button is separate
            if (otherModule != null)
            {
                //Wait for Search Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SelectRecipient_SearchButton.Wait.ForExists(Globals.timeOut);
                //Click on Search Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SelectRecipient_SearchButton.User.Click();
            }
            else
            {
                //Wait for Search Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Settings_SearchUser_Button.Wait.ForExists(Globals.timeOut);
                //Click on Search Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Settings_SearchUser_Button.User.Click();
            }

            //Wait for OK Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_SelectUsers_OKButton.Wait.ForExists(Globals.timeOut);
            //Click on OK Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_SelectUsers_OKButton.User.Click();

            //Wait for Comments Textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);
            //Enter the Comments 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(comments, 50);

            //Wait for OK Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);
            //Click on OK Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();            
        }

        //Method to Save Invoice
        public void P2P_Invoice_Administration_InvoiceSave()
        {
            //Wait for Save button exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.Wait.ForExists(Globals.timeOut);
            //Click on Save Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.User.Click();

        }

        //Method to Perform on Tool Bar Action functions
        public void P2P_Invoice_Administration_MyTasks_ToolBarActions(string toolbarAction = null, string approveButton=null)
        {
            //If condition is true then execute the if block
            if (toolbarAction != null)
            {
                //Store the framework element in variable
                FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("Name=actionToolbar", "XamlTag=actiontoolbar"));
                //Wait for fe element exists in DOM
                fe.Wait.ForExists(Globals.timeOut);
                //Click on fe Button
                fe.Find.ByAutomationId(toolbarAction).User.Click();
            }
            else
            {
                //Store the framework element in variable
                FrameworkElement tabControl = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("Name=toolbarItemsControl", "XamlTag=itemscontrol"));
                //Wait for tabControl element exists in DOM
                tabControl.Wait.ForExists(Globals.timeOut);
                //Click on button Button
                tabControl.Find.ByTextContent(approveButton).User.Click();               
            }
            
        }


        //Method to Matching Cancel And ResendButton
        public void P2PInvoiceAdministrationMatchingCancelAndResend()
        {                 

            //Wait for Addtional Actions Dropdown Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Set focus on Addtional Actions Dropdown 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.SetFocus();

            //Click on Addtional Actions Dropdown 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            //Wait for P2P_Invoice_Administration_Matching_CancelAndResendButton Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_CancelAndResendButton.Wait.ForExists(Globals.timeOut);

            //Setfocus on P2P_Invoice_Administration_Matching_CancelAndResendButton
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_CancelAndResendButton.SetFocus();

            //Click P2P_Invoice_Administration_Matching_CancelAndResendButton
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_CancelAndResendButton.User.Click();            
        }
    }
}

