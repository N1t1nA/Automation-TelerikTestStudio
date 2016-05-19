using System;
using System.Linq;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Silverlight.UI;
using Telerik.WebAii.Controls.Xaml;
using System.Windows.Forms;
using ArtOfTest.WebAii.Silverlight;
using System.Globalization;
using P2P.Testing.Shared.Class;
using ArtOfTest.WebAii.Win32;

namespace P2P.Testing.Shared.Class.InvoiceAdministration.Create
{
    public class P2PCreateInvoice : BaseWebAiiTest
    {
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

        //Pause after performed one step 
        readonly int pause = 2000;

        //Method to Create New Invoice       
        public void P2PInvoiceAdministrationCreateNewInvoice(string organizationUnit, string invoiceType, string supplier, string invoiceNumber, double grossSum, double taxSum, string referencePerson = null, string currencyCode = null, string paymentTermCode = null, string planreference =null,string textFeild=null)
        {
            //Wait to HeaderData_TabTabItem Exist in DOM
           // P2PNavigation.CallBusyIndicator();

            //Wait for HeaderData TabTabItem Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.Wait.ForExists(Globals.timeOut);

            //Set focus before perform next step
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.SetFocus();

            //Click on Header Data Tab Item
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.User.Click();

            //Wait for Select Invoice Type button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SelectInvoiceTypeButton.Wait.ForExists(Globals.timeOut);

            //Click on Select Invoice Type button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SelectInvoiceTypeButton.User.Click();           

            //Wait for OK Button Exist in DOM
            P2PNavigation.WaitForPopUpOkButtonEnabled();

            //Enter Organisation in Select Organization Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.User.TypeText(organizationUnit, 100);

            //Performing keyboard action
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            do
            {
                //Wait for InvoiceType ComboBox Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceType_ComboBox.Wait.ForExists(Globals.timeOut);

            } while (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceType_ComboBox.IsEnabled.Equals(false));

            //Wait for InvoiceType ComboBox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceType_ComboBox.Wait.ForExists(Globals.timeOut);

            //Open the Combo Box item drop down
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceType_ComboBox.OpenDropDown(true);

            //Select Invoice Type from the Combo box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceType_ComboBox.SelectItemByText(true,invoiceType, false);

            //Click on OK Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            //wait to header data items get loaded successfully
            P2PNavigation.CallBusyIndicator();
            //System.Threading.Thread.Sleep(Globals.timeOut);           

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierTextBox.SetFocus();
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierTextBox.User.Click();
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierTextBox.User.TypeText(supplier, 50);

            //P2P_Invoice_Administration_HeaderData_SupplierTextBox

            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab); 

            //Enter the Data in Invoice Number Header Data Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceNumberHeaderDataTextBox.SetFocus();
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceNumberHeaderDataTextBox.User.Click();
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceNumberHeaderDataTextBox.User.TypeText(invoiceNumber,50);

            //Press Tab Key Twice to Get Control towards Invoice Date
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab,20,2);          

            //Wait for TextBox Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_InvoiceDate_TextBox.Wait.ForExists(Globals.timeOut);

            //Set Focus on Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_InvoiceDate_TextBox.SetFocus();

            //Keyboard event to press 'Ctrl+A' if Date is already there.
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

            //Clear the Date Field
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Back);

            //Enter Date in Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_InvoiceDate_TextBox.User.TypeText(DateTime.Now.ToShortDateString(), 100);

            //Execute if block if referencePerson  is not null
            if (referencePerson != null)
            {
                //Enter the User in ReferencePersonTextBox
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ReferencePersonHeaderDataTextBox.User.TypeText(referencePerson, 50);

                if (planreference != null)
                {
                    SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PlanReference_HeaderData_TextBox.Wait.ForExists(Globals.timeOut);
                    //Enter the User in ReferencePersonTextBox
                    SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PlanReference_HeaderData_TextBox.User.TypeText(planreference, 50);

                }
            }

            //click inside the Gross Sum Header Data Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_GrossSumHeaderDataTextBox.User.Click();
            //Empty Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_GrossSumHeaderDataTextBox.Text = string.Empty;
            //Enter the Data in Gross Sum Header Data Text Box            
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_GrossSumHeaderDataTextBox.User.TypeText(grossSum.ToString(CultureInfo.CurrentCulture.NumberFormat), 50);

            //Move out of textbox
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Enter the Data in Tax Sum Header Data Text Box  
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_TaxSumHeaderDataTextBox.User.Click();
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_TaxSumHeaderDataTextBox.Text = string.Empty;
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_TaxSumHeaderDataTextBox.User.TypeText(taxSum.ToString(CultureInfo.CurrentCulture.NumberFormat), 50);            

            //Enter the Text in Text Feild 
            if (textFeild!=null)
            {
                //Enter the Data in Tax Sum Header Data Text Box  
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=headerdatatextbox", "automationid=Text20_HeaderDataField")).SetFocus();
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=headerdatatextbox", "automationid=Text20_HeaderDataField")).User.Click();
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=headerdatatextbox", "automationid=Text20_HeaderDataField")).User.TypeText(textFeild,50);
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
            }
            
            
            //Execuet if block if currencyCode  is not null
            if (currencyCode != null)
            {
                //Wait for Invoice Date TextBox Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_InvoiceDate_TextBox.Wait.ForExists(Globals.timeOut);

                //Click on Invoice Date TextBox
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_InvoiceDate_TextBox.SetFocus();

                //Keyboard event to press 'Ctrl+A' key for selecting all text
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

                //Performing keyboard action
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Back);
                
                //Enter the Date in Invoice Date Text Box
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_InvoiceDate_TextBox.User.TypeText(DateTime.Now.ToShortDateString().ToString(CultureInfo.CurrentCulture.NumberFormat), 50);

                //Wait for CurrencyCode SelectionButton Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_HeaderData_CurrencyCode_TextBox.Wait.ForExists(Globals.timeOut);

                //Click on CurrencyCode Selection Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_HeaderData_CurrencyCode_TextBox.SetFocus();

                //Keyboard event to press 'Ctrl+A' key for selecting all text
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

                //Performing keyboard action
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Back);

                //Wait for SearchTextBox Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_HeaderData_CurrencyCode_TextBox.User.TypeText(currencyCode, 50);

                //Performing keyboard action
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
            }

            //Execuet if block if paymentTermCode  is not null
            if (paymentTermCode != null)
            {
                //Wait for PaymentTermCode Selection Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_HeaderData_PaymentTermCode_TextBox.Wait.ForExists(Globals.timeOut);

                //Set Focus on PaymentTermCode Selection Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_HeaderData_PaymentTermCode_TextBox.SetFocus();

                //Keyboard event to press 'Ctrl+A' key for selecting all text
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

                //Performing keyboard action
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Back);

                //Click on PaymentTermCode Selection Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_HeaderData_PaymentTermCode_TextBox.User.TypeText(paymentTermCode, 50);

                //Performing keyboard action
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

                //Performing keyboard action
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.PageUp);
            }
        }
        
        //Method to Create Coding Row
        public void P2PInvoiceAdministrationCreateCodingRow(int codingRow, int accountCodeCount,int costCenterCount ,string accountCode, string costCenterCode, double sum, Boolean addNetSum, string dontEnterCostCenter = null)        
        {
            //Wait for Add Row Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.Wait.ForExists(Globals.timeOut);
            
            //Click on Add Row Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.User.Click();

            //Workaround for application freeze
            System.Threading.Thread.Sleep(pause);

            //Find the Coding Row using AutomationID                   
            RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl;

            //Create an object for GridViewRow class
            GridViewRow gvr;            

            //Use for loop for creating a Coding Rows
            for (int i = 0; i <codingRow; i++)
            {                
                //Read always newly added row
                gvr= rgv.Rows[rgv.Rows.Count - 1];

                //Click on Cell for focus in a grid
                gvr.Cells[10].User.Click();

                //Pause after click on a cell
                System.Threading.Thread.Sleep(pause);

                //Press enter for add new rows
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);

                //Pause after added a new row in grid
                System.Threading.Thread.Sleep(pause);

                //Refresh the Grid
                rgv.Refresh();

                // Get an instance of our Silverlight app.
                SilverlightApp app = Manager.Current.ActiveBrowser.SilverlightApps()[0];

                //Refresh the Visual Tree
                app.RefreshVisualTrees();
            }

            foreach (GridViewRow row in rgv.Rows)            
            {                   
                if (dontEnterCostCenter == null)
                {

                    //Click on Cell  Cost Centre Code
                    row.Cells[2].User.Click(MouseClickType.LeftClick);

                    //Pause after added a new row in grid
                    System.Threading.Thread.Sleep(pause);

                    //Press tab to move selection on Select Cost Centre button
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

                    // Press enter to open picker and select the required input data
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);

                    //Wait for GridViewControl Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AccountCodeGridViewControl.Wait.ForExists(Globals.handleTime);

                    //Enter the Data in Search Text Box
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.User.TypeText(costCenterCode, 50);

                    //Click on Search Button
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();

                    //Call busy indicator for handled the Busy indicator
                    P2PNavigation.CallBusyIndicator();

                    //Find the Cost Center Code from the Grid
                    TextBlock selectCostCenterCode = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AccountCodeGridViewControl.Find.ByTextContent(costCenterCode).As<TextBlock>();

                    //Click on Cost Center Code
                    selectCostCenterCode.User.Click();

                    //Click on OK Button
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

                    //Pause for refresh the Coding Row Grid
                    System.Threading.Thread.Sleep(pause);

                    //Press tab to move the location in grid
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
                }

                //Click on Account Code
                row.Cells[4].User.Click(MouseClickType.LeftClick);

                //Pause for refresh the Coding Row Grid
                System.Threading.Thread.Sleep(pause);

                //Press enter to open picker and select the required input data
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);
                
                //Wait for GridViewControl Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AccountCodeGridViewControl.Wait.ForExists(Globals.handleTime);
                
                //Enter the Data in Search Text box
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.User.TypeText(accountCode, 50);
                
                //Click on Search Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();
                
                //Wait for company code search in the grid
                System.Threading.Thread.Sleep(pause);

                //Find the Account Code from the Grid
                TextBlock selectAccountCode = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AccountCodeGridViewControl.Find.ByTextContent(accountCode).As<TextBlock>();

                //Click on Account Code
                selectAccountCode.User.Click();

                //Click on OK Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

                //Pause for refresh the Coding Row Grid
                System.Threading.Thread.Sleep(pause);
               
                if (addNetSum == true)
                {
                    //Left Double Click in net Sum Cell                 
                    row.Cells[6].User.Click();

                    //Pause after left Double click on gross sum cell
                    System.Threading.Thread.Sleep(pause);

                    //Refresh the grid
                    row.Refresh();

                    //Enter the Data in Net Sum Cell
                    row.Cells[6].User.TypeText(sum.ToString(CultureInfo.CurrentCulture.NumberFormat), 50);

                    //Refresh the Grid
                    rgv.Refresh();

                    // Get an instance of our Silverlight app.
                    SilverlightApp app = Manager.Current.ActiveBrowser.SilverlightApps()[0];

                    //Refresh the DOM Tree
                    app.RefreshVisualTrees();
                }
                else
                {
                    //Click on cell[24]
                    row.Cells[24].User.Click();

                    //Pause after left Double click on gross sum cell
                    System.Threading.Thread.Sleep(pause);

                    //Refresh the grid
                    row.Refresh();

                    //Enter the Data in Net Sum Cell                
                    row.Cells[24].User.TypeText(sum.ToString(CultureInfo.CurrentCulture.NumberFormat), 50);

                    //Refresh the Grid
                    rgv.Refresh();

                    // Get an instance of our Silverlight app.
                    SilverlightApp app = Manager.Current.ActiveBrowser.SilverlightApps()[0];

                    //Refresh the DOM Tree
                    app.RefreshVisualTrees();
                }
            }
            //Press Tab button
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);           
        }

        //Method to Save the Invoice SaveasDraft
        public void P2PInvoiceAdministrationSaveasDraft()
        {
            //Wait for SaveAsDraft Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_SaveAsDraftButton.Wait.ForExists(Globals.timeOut);

            //Click on SaveAsDrafft Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_SaveAsDraftButton.User.Click();
        }

        //Method to Send the Invoice for Validation
        public void P2PInvoiceAdministrationSendToValidation()
        {
            //Wait for SendToValidation Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SendToValidationButton.Wait.ForExists(Globals.timeOut);
           
            //Click on SendToValidation Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SendToValidationButton.User.Click(MouseClickType.LeftDoubleClick);                     
        }

        //Method to Send the Invoice for Process
        public void P2PInvoiceAdministrationSendToProcess(string addComment=null,string user=null)
        {
            //Wait for SendToValidation Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_SendtoProcess.Wait.ForExists(Globals.timeOut);

            //Click on SendToValidation Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_SendtoProcess.User.Click();

            //Pause after left Double click on gross sum cell
            System.Threading.Thread.Sleep(Globals.timeOut);

            //If Condition is true then execute if block
            if (user != null)
            {
                //Wait for Text box Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_RecepientName_TextBox.Wait.ForExists(Globals.timeOut);

                //Segt focus on Text Box
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_RecepientName_TextBox.SetFocus();

                //Type User Name in Search text box
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_RecepientName_TextBox.User.TypeText(user, 50);

                //Press tab to select the user
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);                
            }

            //If Condition is true then execute if block
            if(addComment!=null)
            {
                //Set focus on Comment Text Box
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.SetFocus();

                //Enter the Comment in Text Box
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(addComment, 50);

                //Wait for Ok Button Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

                //Click on OK Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();                           
            }            
        }

        //Method to change Invoice Type
        public void P2PInvoiceAdministrationChangeInvoiceType(string invoiceType)
        {
            //Wait for HeaderData TabTabItem Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.Wait.ForExists(Globals.timeOut);

            //Set focus before perform next step
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.SetFocus();

            //Click on Header Data Tab Item
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.User.Click();

            //Wait for Select Invoice Type button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SelectInvoiceTypeButton.Wait.ForExists(Globals.timeOut);

            //Click on Select Invoice Type button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SelectInvoiceTypeButton.User.Click();

            //Wait for BusyIndicator disappear from the UI
            P2PNavigation.CallBusyIndicator();

            //Wait for OK Button Exist in DOM
            P2PNavigation.WaitForPopUpOkButtonEnabled();

            do
            {
                //Wait for InvoiceType ComboBox Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceType_ComboBox.Wait.ForExists(Globals.timeOut);

            } while (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceType_ComboBox.IsEnabled.Equals(false));

            //Open the Combo Box item drop down
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceType_ComboBox.OpenDropDown(true);

            //Select Invoice Type from the Combo box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceType_ComboBox.SelectItemByText(true, invoiceType, false);

            //Click on OK Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();            
        }

        //Method to Enter Tax Code
        public void P2PInvoiceAdministrationInputTaxCode(string taxCode)
        {
            //Wait for HeaderData TabTabItem Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Create_TaxCodeTextBox.Wait.ForExists(Globals.timeOut);

            //Set focus before perform next step
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Create_TaxCodeTextBox.SetFocus();

            //Click on Header Data Tab Item
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Create_TaxCodeTextBox.User.TypeText(taxCode, 50);

            //Press tab
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
        }
    }
}
