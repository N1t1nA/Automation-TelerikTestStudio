using System;
using System.Linq;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Silverlight.UI;
using Telerik.WebAii.Controls.Xaml;
using System.Windows.Forms;
using ArtOfTest.WebAii.Silverlight;
using System.Globalization;
using ArtOfTest.WebAii.Win32;


namespace P2P.Testing.Shared.Class.PaymentPlans.Create
{
    public class P2PCreatePaymentPlan : BaseWebAiiTest
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

        //Method to Create New Payment Plans SelectInvoiceType
        public void P2PPaymentPlansCreateNewPaymentPlanSelectInvoiceType(string organizationUnit, string invoiceType, string paymentPlanType = null)
        {
            //Wait for PaymentsPlans SelectInvoiceType Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_SelectInvoiceType_Button.Wait.ForExists(Globals.navigationTimeOut);

            //Click on PaymentsPlans SelectInvoiceType Button
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_SelectInvoiceType_Button.User.Click();

            //Wait for SelectedOrganizations Textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.Wait.ForExists(Globals.navigationTimeOut);

            //Enter Value in SelectedOrganizations Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.User.TypeText(organizationUnit, 50);

            //Press tab to Activate InvoiceType ComboBox
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Global Function Call
            P2PNavigation.WaitForPopUpOkButtonEnabled();

            //Wait for InvoiceType ComboBox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceType_ComboBox.Wait.ForExists(Globals.navigationTimeOut);

            //Open the Combo Box item drop down
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceType_ComboBox.OpenDropDown(true);

            //Select Invoice Type from the Combo box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceType_ComboBox.SelectItemByText(true, invoiceType, true);

            //If paymentPlanType !null then execute if block
            if (paymentPlanType != null)
            {
                //Wait for P2P_PaymentPlans_PlanType ComboBox Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PlanTypeComboBox.Wait.ForExists(Globals.timeOut);
                //Open the Combo Box item drop down
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PlanTypeComboBox.OpenDropDown(true);
                //Select PaymentPlans_PlanType from the Combo box
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PlanTypeComboBox.SelectItemByText(true, paymentPlanType, true);
            }

            //Click on OK Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
        }

        //Method to Create New Payment Plan Add Header Data 
        public void P2PPaymentPlansCreateNewPaymentPlanAddHeaderData(string supplier, string planName, string planReference, string currency, string paymentTermCode = null, string referencePerson = null)
        {
            //Wait for Payment Plan Name Textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_HeaderDataField_PlanName_TextBox.Wait.ForExists(Globals.navigationTimeOut);

            //Set focus on Payment Plan Name Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_HeaderDataField_PlanName_TextBox.SetFocus();

            //Click on Payment Plan Name Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_HeaderDataField_PlanName_TextBox.User.Click();

            //Enter the Data in Payment Plan Name Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_HeaderDataField_PlanName_TextBox.User.TypeText(planName, 50);

            //Wait for Payment Plan Name Textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PlanReference_HeaderData_TextBox.Wait.ForExists(Globals.navigationTimeOut);

            //Set focus on Plan Reference Name Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PlanReference_HeaderData_TextBox.SetFocus();

            //Click on Plan Reference Name Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PlanReference_HeaderData_TextBox.User.Click();

            //Enter the Data in Plan Reference Name Textbox Plan Reference
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PlanReference_HeaderData_TextBox.User.TypeText(planReference, 50);

            if (paymentTermCode != null)
            {
                //Wait for Supplier Selection Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierCode_SelectionButton.Wait.ForExists(Globals.navigationTimeOut);

                //Click on Supplier Selection Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierCode_SelectionButton.User.Click();

                //Global Function Call
                P2PNavigation.WaitForPopUpOkButtonEnabled();

                //Wait for Supplier Search Textbox Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SupplierSearch_TextBox.Wait.ForExists(Globals.navigationTimeOut);

                //Click to set a focus on Search Text Box
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SupplierSearch_TextBox.User.Click();

                //Enter the Data in Search TextBox
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SupplierSearch_TextBox.TypeText(supplier, 50);

                //Wait for Search Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administartion_SuppliersSearchButton.Wait.ForExists(Globals.navigationTimeOut);
                //Click on Search Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administartion_SuppliersSearchButton.User.Click();
                //Grid Refresh
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SuppliersListGridViewControl.Refresh();

                //Global Function Call
                P2PNavigation.WaitForPopUpOkButtonEnabled();

                //Wait For Text Box
                SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(supplier).As<TextBlock>().Wait.ForExists(Globals.navigationTimeOut);

                //Find the Supplier and Select From the Grid 
                TextBlock selectsupplier = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(supplier).As<TextBlock>();

                //Select the Supplier by the User
                selectsupplier.User.Click();

                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);
                //Click on OK Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

                //Wait for CurrencyCode Selection Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_CurrencyCode_SelectionButton.Wait.ForExists(Globals.navigationTimeOut);

                //Click on CurrencyCode Selection Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_CurrencyCode_SelectionButton.User.Click();

                //Enter the Currency in Search Text Box
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.User.TypeText(currency, 50);

                //Wait for  Search Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.Wait.ForExists(Globals.timeOut);
                //Click on Search Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();
                // Refresh the grid
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AccountCodeGridViewControl.Refresh();

                //Global Function Call
                P2PNavigation.WaitForPopUpOkButtonEnabled();

                //Find the Currency and Select From the Grid 
                TextBlock selectCurrency = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AccountCodeGridViewControl.Find.ByTextContent(currency).As<TextBlock>();

                //Select the Currency by the User
                selectCurrency.User.Click();

                //Wait for Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);
                //Click on OK Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

                //Wait for PaymentTermCode Selection Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_PaymentTermCode_SelectionButton.Wait.ForExists(Globals.navigationTimeOut);

                //Set Focus on PaymentTermCode Selection Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_PaymentTermCode_SelectionButton.SetFocus();

                //Click on PaymentTermCode Selection Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_PaymentTermCode_SelectionButton.User.Click(MouseClickType.LeftDoubleClick);

                //Wait for Search TextBox Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.Wait.ForExists(Globals.navigationTimeOut);

                //Enter the PaymentTermCode in Search Text Box
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.User.TypeText(paymentTermCode, 50);

                //Click on Search Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();

                //Refresh the grid
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AccountCodeGridViewControl.Refresh();

                //Global Function Call
                P2PNavigation.WaitForPopUpOkButtonEnabled();

                //Wait for Search TextBox Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AccountCodeGridViewControl.Find.ByTextContent(paymentTermCode).As<TextBlock>().Wait.ForExists(Globals.navigationTimeOut);

                //Find the Currency and Select From the Grid 
                TextBlock selectPaymentTermCode = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AccountCodeGridViewControl.Find.ByTextContent(paymentTermCode).As<TextBlock>();

                //Select the Payment Term Code by the User
                selectPaymentTermCode.User.Click();

                //Wait for Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.navigationTimeOut);
                //Click on OK Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
            }

            //Wait for Payment Plan ValidUntil Textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=ValidationStartDate|ValidationEndDate_HeaderDataField_EndDate")).Wait.ForExists(Globals.navigationTimeOut);

            //Enter the Date in Payment Plan ValidUntil Textbox
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=ValidationStartDate|ValidationEndDate_HeaderDataField_EndDate")).User.TypeText(DateTime.Now.AddMonths(1).ToShortDateString(), 50);

            //Press tab to Activate InvoiceType ComboBox
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            if (paymentTermCode != null)
            {
                //Wait for Payment Plan ValidFrom Textbox Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=ValidationStartDate|ValidationEndDate_HeaderDataField_StartDate")).Wait.ForExists(Globals.navigationTimeOut);

                //Enter the Date in Payment Plan ValidFrom Textbox
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=ValidationStartDate|ValidationEndDate_HeaderDataField_StartDate")).User.TypeText(DateTime.Now.ToShortDateString(), 50);

                //Press tab to Activate InvoiceType ComboBox
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
            }

            if (referencePerson != null)
            {   ///Wait for Payment Plan ReferencePerson Textbox Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ReferencePersonHeaderDataTextBox.Wait.ForExists();

                //Enter the User in ReferencePersonTextBox
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ReferencePersonHeaderDataTextBox.User.TypeText(referencePerson, 50);
            }

        }

        //Method to Create New Payment Plans  Schedule
        public void P2PPaymentPlansCreateNewPaymentSchedule(double expectedSum, string modifyPaymentSchedule = null, string dontEditPaymentScheduleRow = null)
        {
            if (modifyPaymentSchedule == null)
            {
                //Wait for Add Row Payment Schedule Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PaymentSchedule_AddRow_Button.Wait.ForExists(Globals.navigationTimeOut);

                //Click on Add Row Payment Schedule Button
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PaymentSchedule_AddRow_Button.User.Click();

            }

            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Create_PaymentSchedule_Grid.Wait.ForExists(Globals.timeOut);

            //Find the Payment Schedule GridView                 
            RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Create_PaymentSchedule_Grid;

            //Create an object for GridViewRow class
            GridViewRow gvr = rgv.Rows[rgv.Rows.Count - 1];

            //Select cell
            gvr.Cells[3].User.Click(MouseClickType.LeftDoubleClick);

            //Entering Data in Payment Schedule Row 
            if (dontEditPaymentScheduleRow == null)
            {
                //Wait
                System.Threading.Thread.Sleep(Globals.pause);
                //Set Focus
                //gvr.Cells[3].SetFocus();
                //Clear old value from cell no : 3.
                //Keyboard event to press 'Ctrl+A' key for selecting all text
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

                //Press Space Bar to Clear the Existing Value
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Back"));

                //Entering Data in Payment Schedule Row
                //Removed adding value because previous steps did not work (clearing cell value)
                gvr.Cells[3].User.TypeText(expectedSum.ToString(CultureInfo.CurrentCulture.NumberFormat), 50);
            }
        }

        //Method to Create New Payment Plans  Schedule
        public void P2PCreateNewCodingRow(string accountCode, string costCenterCode, double percentage, string modifyCodingRow = null, string modifyOnlyAccount = null, string sendToProcess = null)
        {
            if (modifyCodingRow == null)
            {

                //Wait for Add New Coding Row Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.Wait.ForExists(Globals.navigationTimeOut);

                //Click on Add New Coding Row Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.User.Click();
            }

            //Wait for Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Wait.ForExists(Globals.navigationTimeOut);

            //Set Focus on Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.SetFocus();

            //Find the Coding Row GridView                 
            RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl;

            //Create an object for GridViewRow class
            GridViewRow gvr = rgv.Rows[rgv.Rows.Count - 1];

            //Click on Cell for focus in a grid
            gvr.Cells[1].SetFocus();
            gvr.Cells[1].User.Click(MouseClickType.LeftDoubleClick);

            //Press tab to Activate InvoiceType ComboBox
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Wait for image to  load Properly
            System.Threading.Thread.Sleep(Globals.timeOut);

            //Keyboard ACtion
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);

            ////Wait for Account Code Seletion Button Exists in DOM
            //SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=SelectionButton")).Wait.ForExists(Globals.navigationTimeOut);

            ////Click on Account Code Seletion Button
            //SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=SelectionButton")).User.Click();

            //Global Function Call
            P2PNavigation.WaitForPopUpOkButtonEnabled();

            //Wait for GridViewControl Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AccountCodeGridViewControl.Wait.ForExists(Globals.handleTime);

            //Enter the Data in Search Text box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.User.TypeText(accountCode, 50);

            //Click on Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();

            //Global Function Call
            P2PNavigation.WaitForPopUpOkButtonEnabled();

            //Click on OK Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            if (modifyOnlyAccount == null)
            {

                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Wait.ForExists(Globals.handleTime);

                //Click on Cell for focus in a grid
                gvr.Cells[3].User.Click(MouseClickType.LeftDoubleClick);

                //Press tab to Activate InvoiceType ComboBox
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

                //Wait for image to  load Properly
                System.Threading.Thread.Sleep(Globals.timeOut);

                //Keyboard ACtion
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);

                ////Wait for Cost Center Code Seletion Button Exists in DOM
                //SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_CostCenterCode_SelectionButton.Wait.ForExists(Globals.navigationTimeOut);

                ////Click on Cost Center Code Seletion Button
                //SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_CostCenterCode_SelectionButton.User.Click();

                //Global Function Call
                P2PNavigation.WaitForPopUpOkButtonEnabled();

                //Wait for GridViewControl Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AccountCodeGridViewControl.Wait.ForExists(Globals.handleTime);

                //Enter the Data in Search Text box
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.User.TypeText(costCenterCode, 50);

                //Click on Search Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();

                //Global Function Call
                P2PNavigation.WaitForPopUpOkButtonEnabled();

                //Click on OK Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

                //Global Function Call
                P2PNavigation.CallBusyIndicator();

                if (sendToProcess == null)
                {
                    //Wait for Coding Link Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.User.Click();

                    //Keyboard Action
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Home);

                    //Check the condition
                    bool foundTextBlock = false;

                    do
                    {
                        //Get TextBlock and Verify the Attachments ListBox
                        FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl;

                        //Check whether any TextBlock contains the specified string.
                        foundTextBlock = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(percentage.ToString()));

                        //Press tab to move the location in grid                       
                        Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Right);

                        //Wait for Coding Link Exists in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Wait.ForExists(Globals.timeOut);

                        //refresh the framework element
                        fe.Refresh();

                    } while (foundTextBlock == false);
                }

                //Click in Cost Center Cell 
                gvr.Cells[4].User.Click();

                //Press tab
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab, 1, 2);

                //Click in percentage cell/Coding Gross Total Cell
                gvr.Cells[6].SetFocus();
                gvr.Cells[6].User.Click(MouseClickType.LeftDoubleClick);

                //Press Ctrl A
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

                //Generate a Keyboard Event to clear search
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

                //Entering Value in Percentage Column/Coding Gross Total Cell
                Manager.Current.Desktop.KeyBoard.TypeText(percentage.ToString(CultureInfo.InvariantCulture.NumberFormat), 50);
            }
        }

        //Method to Create New Payment Plans  Schedule
        public void P2PPaymentPlansCreateNewCodingRow(string accountCode, string costCenterCode, double percentage, string modifyCodingRow = null, string modifyOnlyAccount = null, string sendToProcess = null)
        {
            if (modifyCodingRow == null)
            {

                //Wait for Add New Coding Row Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.Wait.ForExists(Globals.navigationTimeOut);

                //Click on Add New Coding Row Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.User.Click();
            }

            //Wait for Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Wait.ForExists(Globals.navigationTimeOut);

            //Set Focus on Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.SetFocus();

            //Find the Coding Row GridView                 
            RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl;

            //Create an object for GridViewRow class
            GridViewRow gvr = rgv.Rows[rgv.Rows.Count - 1];


            //Click on Cell for focus in a grid
            gvr.Cells[1].SetFocus();
            gvr.Cells[1].User.Click(MouseClickType.LeftDoubleClick);

            //Press tab to Activate InvoiceType ComboBox
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Wait for image to  load Properly
            System.Threading.Thread.Sleep(Globals.timeOut);

            //Keyboard ACtion
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);

            ////Wait for Account Code Seletion Button Exists in DOM
            //SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=SelectionButton")).Wait.ForExists(Globals.navigationTimeOut);

            ////Click on Account Code Seletion Button
            //SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=SelectionButton")).User.Click();

            //Global Function Call
            P2PNavigation.WaitForPopUpOkButtonEnabled();

            //Wait for GridViewControl Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AccountCodeGridViewControl.Wait.ForExists(Globals.handleTime);

            //Enter the Data in Search Text box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.User.TypeText(accountCode, 50);

            //Click on Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();

            //Global Function Call
            P2PNavigation.WaitForPopUpOkButtonEnabled();

            //Click on OK Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            if (modifyOnlyAccount == null)
            {

                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Wait.ForExists(Globals.handleTime);

                //Click on Cell for focus in a grid
                gvr.Cells[3].User.Click(MouseClickType.LeftDoubleClick);

                //Press tab to Activate InvoiceType ComboBox
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

                //Wait for image to  load Properly
                System.Threading.Thread.Sleep(Globals.timeOut);

                //Keyboard ACtion
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);

                ////Wait for Cost Center Code Seletion Button Exists in DOM
                //SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_CostCenterCode_SelectionButton.Wait.ForExists(Globals.navigationTimeOut);

                ////Click on Cost Center Code Seletion Button
                //SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_CostCenterCode_SelectionButton.User.Click();

                //Global Function Call
                P2PNavigation.WaitForPopUpOkButtonEnabled();

                //Wait for GridViewControl Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AccountCodeGridViewControl.Wait.ForExists(Globals.handleTime);

                //Enter the Data in Search Text box
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.User.TypeText(costCenterCode, 50);

                //Click on Search Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();

                //Global Function Call
                P2PNavigation.WaitForPopUpOkButtonEnabled();

                //Click on OK Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

                //Global Function Call
                P2PNavigation.CallBusyIndicator();

                //Press tab
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab, 1, 2);

                if (sendToProcess == null)
                {
                    //Wait for Coding Link Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.User.Click();

                    //Keyboard Action
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Home);

                    //Check the condition
                    bool foundTextBlock = false;

                    do
                    {
                        //Get TextBlock and Verify the Attachments ListBox
                        FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl;

                        //Check whether any TextBlock contains the specified string.
                        foundTextBlock = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(percentage.ToString()));

                        //Press tab to move the location in grid                       
                        Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Right);

                        //Wait for Coding Link Exists in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Wait.ForExists(Globals.timeOut);

                        //refresh the framework element
                        fe.Refresh();

                    } while (foundTextBlock == false);
                }

            }
            //Click on percentage cell/Coding Gross Total Cell
            gvr.Cells[6].User.Click();

            //Press Ctrl A
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

            //Generate a Keyboard Event to clear search
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

            //Entering Value in Percentage Column/Coding Gross Total Cell
            gvr.Cells[6].User.TypeText(percentage.ToString(CultureInfo.InvariantCulture.NumberFormat), 90);



        }

        //Method to Create New Payment Plans  Schedule
        public void P2PPaymentPlansCodingRowSetTaxCode(string taxCode)
        {
            //Wait for Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Wait.ForExists(Globals.navigationTimeOut);

            //Click on Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.User.Click();

            //Find the Coding Row GridView                 
            RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl;

            //Create an object for GridViewRow class
            GridViewRow gvr = rgv.Rows[rgv.Rows.Count - 1];

            //Press tab to Activate TaxCode ComboBox
            for (int i = 0; i < 11; i++)
            {
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
            }

            gvr.Cells[8].User.Click(MouseClickType.LeftDoubleClick);

            //Move to Tax Code Column
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Keyboard event to press 'Ctrl+A' key for selecting all text
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(ArtOfTest.WebAii.Win32.KeyBoard.KeysFromString("Ctrl+A"));

            //Press Space Bar to Clear the Existing Value
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Back);

            //Enter Tax Code
            Manager.Current.Desktop.KeyBoard.TypeText(taxCode);
        }

        //Method to Save the Payment Plan SaveasDraft
        public void P2PPaymentPlansSaveasDraft()
        {
            //Wait for SaveAsDraft Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_SaveAsDraftButton.Wait.ForExists(Globals.timeOut);
            //Click on SaveAsDrafft Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_SaveAsDraftButton.User.Click();
        }

        //Method to Save the Payment Plan SaveasDraft
        public void P2PPaymentPlansSaveasDraftForInWorkflow()
        {
            //Wait for SaveAsDraft Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.Wait.ForExists(Globals.timeOut);
            //Click on SaveAsDrafft Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.User.Click();
        }

        //Method to Approved the Payment Plan
        public void P2PPaymentPlansApproved()
        {
            //Wait for SaveAsDraft Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SendToValidationButton.Wait.ForExists(Globals.timeOut);
            //Click on SaveAsDrafft Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SendToValidationButton.User.Click();
        }

        //Method to Create New Payment Plans  Schedule
        public void P2PPaymentPlansModifyCodingRow(string costCenterCode)
        {

            //Wait for Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Wait.ForExists(Globals.navigationTimeOut);

            //Click on Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.User.Click();

            //Find the Coding Row GridView                 
            RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl;

            //Refresh the RadGrid
            rgv.Refresh();

            //Create an object for GridViewRow class
            GridViewRow gvr = rgv.Rows[rgv.Rows.Count - 1];

            //Click on the Gridview Rows
            gvr.User.Click();


            //Wait for Cost Center Code Seletion Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_CostCenterCode_SelectionButton.Wait.ForExists(Globals.navigationTimeOut);

            //Click on Cost Center Code Seletion Button
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_CostCenterCode_SelectionButton.User.Click();

            //Global Function Call
            P2PNavigation.WaitForPopUpOkButtonEnabled();

            //Wait for GridViewControl Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AccountCodeGridViewControl.Wait.ForExists(Globals.handleTime);

            //Enter the Data in Search Text box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.User.TypeText(costCenterCode, 50);

            //Click on Search Button (Button is Different) which is used in Common Method
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_CostCenterCodeSearchButton.User.Click();

            //Global Function Call
            P2PNavigation.WaitForPopUpOkButtonEnabled();

            //Click on OK Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            //Global Function Call
            P2PNavigation.CallBusyIndicator();

        }

        //Method to Read Account Code from coding Row Grid
        public string P2PGetAccountCodeCodingRow()
        {
            //Wait for Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Wait.ForExists(Globals.navigationTimeOut);

            //Set Focus on Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.SetFocus();

            //Find the Coding Row GridView                 
            RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl;

            //Create an object for GridViewRow class and get last row
            GridViewRow gvr = rgv.Rows[rgv.Rows.Count - 1];

            //Click on Cell for focus in a grid
            gvr.Cells[2].SetFocus();
            //gvr.Cells[1].User.Click(MouseClickType.LeftDoubleClick);

            string accountCode = gvr.Cells[2].Text;

            return accountCode;
        }

        //Method to Create a  Budget Row 
        public void P2PPaymentPlansCreateBudgetRow(string tabItem, double budget, string invoicelines, double invoiceTotal)
        {

            //Wait for Add Row Payment Schedule Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_BudgetRowTabControl.Wait.ForExists(Globals.navigationTimeOut);

            //Click on Add Row Payment Schedule Button
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_BudgetRowTabControl.Find.ByTextContent(tabItem).User.Click();

            //Wait for Add Row Payment Schedule Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Budget_AddRow_Button.Wait.ForExists(Globals.navigationTimeOut);

            //Click on Add Row Payment Schedule Button
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Budget_AddRow_Button.User.Click();

            //Create object for RadGridView
            RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=BudgetRowGridView", "XamlTag=editablegridview")).CastAs<RadGridView>();

            rgv.Wait.ForExists(Globals.timeOut);
            //Create an object for GridViewRow class
            GridViewRow gvr = rgv.Rows[rgv.Rows.Count - 1];

            //Press tab to move the focus
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyDown(Keys.Tab);
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyDown(Keys.Tab);
            //Enter the Date into the date field
            gvr.Cells[2].User.TypeText(DateTime.Now.AddMonths(1).ToShortDateString(), 50);
            //Press tab to move the focus
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyDown(Keys.Tab);
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyDown(Keys.Tab);

            //Select cell
            gvr.Cells[3].User.Click(MouseClickType.LeftDoubleClick);
            //Enter a budget into the budget Text box
            gvr.Cells[3].User.TypeText(budget.ToString(CultureInfo.InvariantCulture.NumberFormat), 75);

            //Press tab to move the focus
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyDown(Keys.Tab);
            //Enter a invoicelines into the invoicelines Text box
            gvr.Cells[4].User.TypeText(invoicelines.ToString(CultureInfo.InvariantCulture.NumberFormat), 75);

            //Press tab to move the focus
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyDown(Keys.Tab);
            //Keyboard event to press 'Ctrl+A' key for selecting all text
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

            //Press Space Bar to Clear the Existing Value
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Back"));

            //Enter a invoiceTotal into the invoiceTotal Text box
            gvr.Cells[5].User.TypeText(invoiceTotal.ToString(CultureInfo.InvariantCulture.NumberFormat), 75);
        }


        //Method to Create P2PPaymentPlansCreateNewBudgetCodingRow
        public void P2PPaymentPlansCreateNewBudgetCodingRow(string tabItem, double percentage)
        {
            //Store the object  into tab control
            ArtOfTest.WebAii.Silverlight.UI.TabControl tb = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Budget_InvoicesTabControl;
            //Click on Tab item 
            tb.Find.ByTextContent(tabItem).User.Click();

            //Wait for Add Row button exists
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.Wait.ForExists(Globals.timeOut);
            //Click on Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.User.Click();

            //Store RadGridView into a rgv variable
            RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Budget_CodingGridView;
            GridViewRow gvr = rgv.Rows[rgv.Rows.Count - 1];

            //Click on cell
            gvr.Cells[2].User.Click();
            //Type percentage into the specific cell
            gvr.Cells[2].User.TypeText(percentage.ToString(CultureInfo.InvariantCulture.NumberFormat), 75);
        }

        //Method to create common method for creating a Payment Plan for E2E TestSCript"E2E_Invoice_ManualMatch_PaymentPlan.tstest"
        public void P2PPaymentPlansCreateNewPaymentPlan(string planName, string supplierCode)
        {
            //Wait for Payment Plan Name Textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_HeaderDataField_PlanName_TextBox.Wait.ForExists(Globals.timeOut);

            //Set focus on Payment Plan Name Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_HeaderDataField_PlanName_TextBox.SetFocus();

            //Click on Payment Plan Name Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_HeaderDataField_PlanName_TextBox.User.Click();

            //Enter the Data in Payment Plan Name Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_HeaderDataField_PlanName_TextBox.User.TypeText(planName, 50);

            //Wait for Payment Plan Name Textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PlanReference_HeaderData_TextBox.Wait.ForExists(Globals.timeOut);

            //Set focus on Plan Reference Name Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PlanReference_HeaderData_TextBox.SetFocus();

            //Click on Plan Reference Name Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PlanReference_HeaderData_TextBox.User.Click();

            //Enter the Data in Plan Reference Name Textbox Plan Reference
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PlanReference_HeaderData_TextBox.User.TypeText(planName, 50);

            //Set focus on Plan Reference Name Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierTextBox.SetFocus();

            //Enter the Data in supplierCode Textbox 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierTextBox.User.TypeText(supplierCode, 50);

            //Press tab to Activate supplierCode Text box
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Wait for Payment Plan ValidFrom Textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=ValidationStartDate|ValidationEndDate_HeaderDataField_StartDate")).Wait.ForExists(Globals.navigationTimeOut);

            //Enter the Date in Payment Plan ValidFrom Textbox
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=ValidationStartDate|ValidationEndDate_HeaderDataField_StartDate")).User.TypeText(DateTime.Now.ToShortDateString(), 50);

            //Press tab to move the focus on the End Date Text Box
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);            
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Wait for Payment Plan ValidUntil Textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=ValidationStartDate|ValidationEndDate_HeaderDataField_EndDate")).Wait.ForExists(Globals.navigationTimeOut);

            //Enter the Date in Payment Plan ValidUntil Textbox
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=ValidationStartDate|ValidationEndDate_HeaderDataField_EndDate")).User.TypeText(DateTime.Now.AddMonths(1).ToShortDateString(), 50);

            //Press tab to Activate InvoiceType ComboBox
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
        }
    }
}


