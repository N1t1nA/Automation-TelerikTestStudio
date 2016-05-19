using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Silverlight.UI;
using ArtOfTest.WebAii.Win32;
using Telerik.WebAii.Controls.Xaml;

namespace P2P.Testing.Shared.Class.PaymentPlans.InvoiceDetails
{
    public class P2PPaymentPlansDetails : BaseWebAiiTest
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

        //Global time out
        //readonly int timeOut = 5000;

        //Method to Modify and Save Payment Plan
        public void P2PPaymentPlanModifyHeaderDataAndSavePlan(Boolean save, string supplierCode, string currencyCode=null, string planReference = null)
        {

            //Wait for SupplierCode_SelectionButton Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierCode_SelectionButton.Wait.ForExists(Globals.timeOut);

            //Click on HeaderData_SupplierCode_SelectionButton" Button to Search supplierCode
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierCode_SelectionButton.User.Click();

            //Wait for Search Text Box Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SupplierSearch_TextBox.Wait.ForExists(Globals.timeOut);
            //Set the focus on Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SupplierSearch_TextBox.SetFocus();
            //Enter the Data in Search Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SupplierSearch_TextBox.TypeText(supplierCode, 50);

            //Click on Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administartion_SuppliersSearchButton.User.Click();

            //Handle Busy Indicator                
            P2PNavigation.CallBusyIndicator();

            //Find the User Name and Select From the Grid 
            TextBlock selectSupplierCode = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(supplierCode).As<TextBlock>();
            //Select the User
            selectSupplierCode.User.Click();

            //Use if Condition to Check Whether OK button is Enabled?    
            if (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SelectSupplier_OKButton.IsEnabled == true)

                //Click on OK Button to Select  the supplierCode
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SelectSupplier_OKButton.User.Click();


            if (currencyCode != null)
            {

                //Wait for CurrencyCode SelectionButton Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_CurrencyCode_SelectionButton.Wait.ForExists(Globals.timeOut);
                //Click on CurrencyCode Selection Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_CurrencyCode_SelectionButton.User.Click();

                //Wait for SearchTextBox Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.Wait.ForExists(Globals.timeOut);
                //Enter the Data in SearchTextBox
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.User.TypeText(currencyCode, 50);

                //Wait for SearchButton Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.Wait.ForExists(Globals.timeOut);
                //Click on SearchButton
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();

                //Wait for OK Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);
                //Click on OK Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            }

            if (planReference != null)
            {
                //Wait for Plan reference Textbox exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PlanReference_HeaderData_TextBox.Wait.ForExists(Globals.timeOut);

                //Set focus on Plan Reference Name Textbox
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PlanReference_HeaderData_TextBox.SetFocus();

                //Click on Plan Reference Name Textbox
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PlanReference_HeaderData_TextBox.User.Click();

                //Press Space Bar to Clear the Existing Value
                Manager.Current.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

                //Press Space Bar to Clear the Existing Value
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Delete);

                //Enter the Data in Plan Reference Name Textbox Plan Reference
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PlanReference_HeaderData_TextBox.User.TypeText(planReference, 50);
            }

            if (save == true)
            {
                //Click on Save_Button to Save the Modify Invoice
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.User.Click();
            }
        }

        //Method to cancel a Task under Workflow Task management Tab
        public void P2PPaymentPlanWorkflowTaskManagementCancelTask()        {

            //Wait for TaskManagement Cancel Task Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagementTabTabitem_CancelTaskButton.Wait.ForExists(Globals.timeOut);

            //SetFocus on Cancel Task
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagementTabTabitem_CancelTaskButton.SetFocus();
            
            //Click on TaskManagement Cancel Task Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagementTabTabitem_CancelTaskButton.User.Click();                    
         }

        //Click On Task Management Tab Item
        public void P2PPaymentPlanClickOnTaskManagementTab()
        {
            //Wait for TaskManagementTabTabitem Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_InWorkflow_TaskManagement_TabItem.Wait.ForExists(Globals.timeOut);

            //Click on TaskManagementTabTabitem
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_InWorkflow_TaskManagement_TabItem.User.Click();
        }

        //Method to Add a new Task under Workflow Task management Tab
        public void P2PPaymentPlanWorkflowTaskManagementAddNewTask(string selectUser, string paymentPlanTaskManagement = null)
        {
            //Wait for TaskManagement Add Task Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagementTabTabitem_AddTaskButton.Wait.ForExists(Globals.timeOut);

            //Click on TaskManagement Add Task Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagementTabTabitem_AddTaskButton.User.Click();

            //
            if (paymentPlanTaskManagement != null)
            {

                //Create  a RadGridView
                RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_WorkFlow_PlansDetails_TaskManagement_TaskGridView;

                //Create an object for GridViewRow class
                GridViewRow gvr = rgv.Rows[0];

                //Store row count in integer variable
                int countRows = rgv.Rows.Count;

                //Try to select the newly row added in grid
                gvr = rgv.Rows[rgv.Rows.Count - countRows];

                //Set focus on Cell for focus in a grid
                gvr.Cells[3].SetFocus();

                //Click on Cell for focus in a grid
                gvr.Cells[3].User.Click(MouseClickType.LeftClick);
               
                //Wait for AddRecipient Browse Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_AddRecipientBrowseButton.Wait.ForExists(Globals.timeOut);

                //Set Focus on AddRecipient Browse Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_AddRecipientBrowseButton.SetFocus();

                //Click on AddRecipient Browse Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_AddRecipientBrowseButton.User.Click();
            }
            else
            {
                //FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskGrid;            

                //Wait for TaskManagement Grid Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskGrid.Wait.ForExists(Globals.timeOut);

                //Wait for AddRecipient Browse Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_AddUserRecipientBrowseButton.Wait.ForExists(Globals.timeOut);

                //Set Focus on AddRecipient Browse Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_AddUserRecipientBrowseButton.SetFocus();

                //Click on AddRecipient Browse Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_AddUserRecipientBrowseButton.User.Click();


            }
            //Wait for Search User Text Box Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_SelectUser_SearchTextBox.Wait.ForExists(Globals.timeOut);

            //Enter Value in Add Recipient Search User Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_SelectUser_SearchTextBox.User.TypeText(selectUser, 50);

            //Click on Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_SearchUserButton.User.Click();

            //Wait for Ok Button To Enabled
            P2PNavigation.WaitForPopUpOkButtonEnabled();

            //Select the User from Result Grid
            TextBlock userCell = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_Collaborate_SelectUsersGrid.Find.ByTextContent(selectUser).As<TextBlock>();

            //Select the User
            userCell.User.Click();

            //Click on Ok Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            //Wait for  Save_Button to Save
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.Wait.ForExists(Globals.timeOut);

            //Click on Save_Button to Save the Modify Invoice
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.User.Click();
        }

        //Method to Forward a Task under Workflow Task management Tab
        public void P2PPaymentnWorkflowTaskManagementForwardATask(string selectUser, string selectNewUser)
        {
            //Wait for TaskManagementTabTabitem Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_InWorkflow_TaskManagement_TabItem.Wait.ForExists(Globals.timeOut);

            //Click on TaskManagementTabTabitem
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_InWorkflow_TaskManagement_TabItem.User.Click();

            //Wait for Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_RecepientName_TextBox.Wait.ForExists(Globals.timeOut);

            //Get data from textbox
            string rgv = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_RecepientName_TextBox.Text;

            if (rgv == selectUser)
            {
                //Select the User
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_RecepientName_TextBox.User.Click();
            }

            //Wait for AddRecipient Browse Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_AddRecipientBrowseButton.Wait.ForExists(Globals.timeOut);

            //Click on AddRecipient Browse Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_AddRecipientBrowseButton.User.Click();

            //Wait for grid to load
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_Collaborate_SelectUsersGrid.Wait.ForExists(Globals.timeOut);

            //Wait for Search User Text Box Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_SelectUser_SearchTextBox.Wait.ForExists(Globals.timeOut);

            //Enter Value in Add Recipient Search User Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_SelectUser_SearchTextBox.User.TypeText(selectNewUser, 50);

            //Click on Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_SearchUserButton.User.Click();

            //Wait for grid to load
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_Collaborate_SelectUsersGrid.Wait.ForExists(Globals.timeOut);

            P2PNavigation.WaitForPopUpOkButtonEnabled();

            //Select the User from Result Grid
            TextBlock userCell = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_Collaborate_SelectUsersGrid.Find.ByTextContent(selectNewUser).As<TextBlock>();

            //Select the User
            userCell.User.Click();

            //Click on Ok Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            //Wait for busyindicator
            P2PNavigation.CallBusyIndicator();

            //Click on Save Invoice button Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.User.Click();
        }

        //Click On Activate Button
        public void P2PPaymentPlanApprovedActivate()
        {
            //Wait for ActivateButton Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_ActivateButton.Wait.ForExists(Globals.timeOut);
            //Click on ActivateButton
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_ActivateButton.User.Click();
        }

        //Click On Deactivate Button
        public void P2PPaymentPlanApprovedDeactivate()
        {
            //Wait for Deactivate Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_DeactivateButton.Wait.ForExists(Globals.timeOut);
            //Click on Deactivate Button
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_DeactivateButton.User.Click();
        }

        //Method to Select Coding Rows and Payment Schedule and Delete from Keyboard
        public void P2PPaymentPlanDeleteAllCodingRowsAndPaymentSchedule(string codingRow = null)
        {
            if (codingRow != null)
            {
                //Wait for Coding Rows Grid
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Wait.ForExists(Globals.navigationTimeOut);

                //Click on Grid
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.User.Click();

                //Find the Coding Row GridView                 
                RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl;

                //Create an object for GridViewRow class
                GridViewRow gvr = rgv.Rows[rgv.Rows.Count - 1];

                //Click on Cell for focus in a grid
                gvr.Cells[3].User.Click(MouseClickType.LeftDoubleClick);

                //Press Space Bar to Clear the Existing Value
                Manager.Current.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

                //Press Space Bar to Clear the Existing Value
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Delete);
            }
            else
            {
                //Wait for Payment Schedule Grid Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Create_PaymentSchedule_Grid.Wait.ForExists(Globals.timeOut);

                //Find the Payment Schedule GridView                 
                RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Create_PaymentSchedule_Grid;

                //Create an object for GridViewRow class
                GridViewRow gvr = rgv.Rows[rgv.Rows.Count - 1];

                //Entering Data in Payment Schedule Row 
                gvr.Cells[6].User.Click();

                //Press Space Bar to Clear the Existing Value
                Manager.Current.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

                //Press Space Bar to Clear the Existing Value
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Delete);
            }

            //Wait for Confirmation Yes Button Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);
            //Click on Yes Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
        }

        //Enter Contact Person Email ID
        public void P2PPaymentPlanContactPersonEmailID(string emailID)
        {
            //Wait for textbox to load
            SharedElement.P2P_Application.SilverlightApp.P2P_Payment_Plan_Workflow_ContactEmailID_TextBox.Wait.ForExists(Globals.timeOut);

            //Setfocus on textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Payment_Plan_Workflow_ContactEmailID_TextBox.SetFocus();

            //Press Space Bar to Clear the Existing Value
            Manager.Current.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

            //Press Space Bar to Clear the Existing Value
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Back);

            //Enter Email Id
            SharedElement.P2P_Application.SilverlightApp.P2P_Payment_Plan_Workflow_ContactEmailID_TextBox.User.TypeText(emailID, 50);
        }

        public void P2PPaymentPlanEditFunctionality()
        {
            //Wait for Button exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_EditToolBar_Button.Wait.ForExists(Globals.timeOut);
            //Click on button
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_EditToolBar_Button.User.Click();
            //Wait for grid get populated
            P2PNavigation.CallBusyIndicator();            
        }

        //Method to Click Payment Plan Statuses link from Overview Page
        public void P2PPaymentPlanOverviewInvoiceStatusesReceived(string paymentplanStatusLink, string paymentplanCount = null)
        {

            switch (paymentplanStatusLink)
            {
                case "Draft":
                    //Wait for Exist in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Overview_Received_DraftLink.Wait.ForExists(Globals.timeOut);
                    //Set Focus on the Link
                    SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Overview_Received_DraftLink.SetFocus();

                    //declare variable and get actual count from the UI
                    string actualDraftCount = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Overview_Received_DraftLink.Text.ToString();

                    //If block execute if Condition is true
                    if (actualDraftCount != paymentplanCount)
                    {
                        //Click on Draft link in Overview Page
                        SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Overview_Received_DraftLink.User.Click();
                    }
                    else
                    {
                        Manager.Current.Log.WriteLine("Draft PaymentPlan Count on Overview Page is :-" + actualDraftCount);
                    }
                    break;
                case "Expired":
                    //Wait for Exist in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Overview_Approved_ExpiredLink.Wait.ForExists(Globals.timeOut);
                    //Set Focus on the Link
                    SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Overview_Approved_ExpiredLink.SetFocus();

                    //declare variable and get actual count from the UI
                    string actualExpiredCount = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Overview_Approved_ExpiredLink.Text.ToString();

                    //If block execute if Condition is true
                    if (actualExpiredCount != paymentplanCount)
                    {
                        //Click on Draft link in Overview Page
                        SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Overview_Approved_ExpiredLink.User.Click();
                    }
                    else
                    {
                        Manager.Current.Log.WriteLine("Expired PaymentPlan Count on Overview Page is :-" + actualExpiredCount);
                    }
                    break;
                case "Pending":
                    //Wait for Exist in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_PendingLink.Wait.ForExists(Globals.timeOut);
                    //Set Focus on the Link
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_PendingLink.SetFocus();

                    //declare variable and get actual count from the UI
                    string actualPendingCount = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_PendingLink.Text.ToString();

                    //If block execute if Condition is true
                    if (actualPendingCount != paymentplanCount)
                    {
                        //Click on Draft link in Overview Page
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_PendingLink.User.Click();
                    }
                    else
                    {
                        Manager.Current.Log.WriteLine("Pending PaymentPlan Count on Overview Page is :-" + actualPendingCount);
                    }
                    break;

                default:
                    //Log Failure for Tab item not found
                    Manager.Current.Log.WriteLine(LogType.Error, "PaymentPlan Navigation Links are Disabled");
                    break;
            }
        }

        //Method to Add Reference Person to Payment Plan Details
        public void P2PPaymentPlanAddReferencePerson(string referencePerson)
        {

            //Wait for Payment Plan ReferencePerson Textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ReferencePersonHeaderDataTextBox.Wait.ForExists(Globals.timeOut);

            //Set Focus on Reference Person Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ReferencePersonHeaderDataTextBox.SetFocus();

            //Empty if some Data already present
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ReferencePersonHeaderDataTextBox.Text = string.Empty;
            

            //Enter the User in ReferencePersonTextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ReferencePersonHeaderDataTextBox.User.TypeText(referencePerson, 50);        
        }

        //Method to Navigation between Payment Plans
        public void P2PPaymentPlanNavigation()
        {

            //Wait for Navigation Next button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_NextButton.Wait.ForExists(Globals.timeOut);
            //Set Focus on Navigation Next button
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_NextButton.SetFocus();
            //Click on Navigation Next button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_NextButton.User.Click();
            
        }


    }
}
