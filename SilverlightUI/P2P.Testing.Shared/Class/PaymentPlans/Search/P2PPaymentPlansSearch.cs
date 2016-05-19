using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Controls.Xaml.Wpf;
using P2P.Testing.Shared;
using System.Windows.Forms;
using ArtOfTest.WebAii.Silverlight;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArtOfTest.WebAii.Win32;


namespace P2P.Testing.Shared.Class.PaymentPlans.Search
{
    public class P2PPaymentPlansSearch : BaseWebAiiTest
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

        //Method to Search the Plan and Use Free Text Search for Searching Plans
        public void P2PPaymentPlansFreeTextSearch(string planName, string paymentPlanSearch=null)
        {
            if (paymentPlanSearch != null)
            {
                //Wait for Search Textbox to Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Wait.ForExists(Globals.timeOut);

                //Set Focus to Search Textbox to Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.SetFocus();

                //Select the String in Text Box
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

                //Generate a Keyboard Event to clear search
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

                //Enter the Search Term to search for plan
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.User.TypeText(planName, 100);
            }

            else
            {
                //Wait for Search Textbox to Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.Wait.ForExists(Globals.timeOut);

                //Set Focus to Search Textbox to Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.SetFocus();

                //Select the String in Text Box
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

                //Generate a Keyboard Event to clear search
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

                //Enter the Search Term to search for plan
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.User.TypeText(planName, 100);
            }

            //Wait for Search button to Exists in DOM Tree 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.Wait.ForExists(Globals.timeOut);

            //Click on Search button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();
        }

        // Method to Select the Check Boxes
        public void P2PPaymentPlanSearchStatusFiltersToSearchPaymentPlan(Boolean received, Boolean approved, Boolean removed, string inWorkflow = null)
        {
            // Using If Condition for the "Received" CheckBoxes
            if (received == true && approved == false && removed == false)
            {
                // Wait for  Received Check Box Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Received_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click the Received Check Box
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Received_CheckBox.User.Click();
            }

            // Using If Condition for the "Approved" CheckBoxes
            if (received == false && approved == true && removed == false)
            {

                // Wait for Received Check Box Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Received_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click the Received Check Box
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Received_CheckBox.User.Click();

                // Wait for Approved Check Box Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Approved_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click the Approved Check Box
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Approved_CheckBox.User.Click();

            }

            // Using If Condition for the "Removed" CheckBoxes
            if (received == false && approved == false && removed == true)
            {
                // Wait for Approved Check Box Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Approved_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click the Approved Check Box
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Approved_CheckBox.User.Click();

                // Wait for Removed Check Box Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Removed_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click the Removed Check Box
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Removed_CheckBox.User.Click();
            }

            // Using If Condition for the "Uncheck" All CheckBoxes
            if (received == false && approved == false && removed == false)
            {
                // Wait for Removed Check Box Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Removed_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click the Removed Check Box
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Removed_CheckBox.User.Click();
            }

            //Use condition if inworkflow checkbox needs to be checked
            if (inWorkflow != null)
            {
                // Wait for inworkflow Check Box Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_InWorkflow_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click the inworkflow Check Box
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_InWorkflow_CheckBox.User.Click();
            }
            //Handle Busy Indicator
            P2PNavigation.CallBusyIndicator();
        }

        // Method to Verify the Data according to the selected Filters
        public void P2PPaymentPlanApprovedStatusFiltersToSearchPaymentPlan(Boolean active, Boolean deactivated, Boolean expired, Boolean waitingForActivation)
        {
            //Using if condition to select checkbox : active
            if (active == true && deactivated == false && expired == false && waitingForActivation == false)
            {
                // Wait for Deactivated Check Box Exist in DOM 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Deactivated_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click on Deactivated Check Box 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Deactivated_CheckBox.User.Click();

                // Wait for Expired Check Box Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Expired_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click on Expired Check Box 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Expired_CheckBox.User.Click();

                // Wait for waitingForActivation Check Box Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Waiting_For_Activation_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click on waitingForActivation Check Box 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Waiting_For_Activation_CheckBox.User.Click();
            }

            //Using if condition to select checkbox : deactivated
            if (active == false && deactivated == true && expired == false && waitingForActivation == false)
            {
                // Wait for Active Check Box Exist in DOM 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Active_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click on Active Check Box 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Active_CheckBox.User.Click();

                // Wait for Deactivated Check Box Exist in DOM 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Deactivated_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click on Deactivated Check Box 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Deactivated_CheckBox.User.Click();
            }

            //Using if condition to select checkbox :expired
            if (active == false && deactivated == false && expired == true &&waitingForActivation == false)
            {
                // Wait for  Deactivated Check Box Exist in DOM 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Deactivated_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click on Deactivated Check Box 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Deactivated_CheckBox.User.Click();

                // Wait for  Expired Check Box Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Expired_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click on Expired Check Box 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Expired_CheckBox.User.Click();
            }


            //Using if condition to select checkbox :expired
            if (active == false && deactivated == false && expired == false && waitingForActivation == true)
            {
              
                // Wait for  Expired Check Box Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Expired_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click on Expired Check Box 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Expired_CheckBox.User.Click();

                // Wait for waitingForActivation Check Box Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Waiting_For_Activation_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click on waitingForActivation Check Box 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Waiting_For_Activation_CheckBox.User.Click();
            }
            // Using If Condition for the "Uncheck" All CheckBoxes
            if (active == false && deactivated == false && expired == false && waitingForActivation == false)
            {
                // Wait for waitingForActivation Check Box Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Waiting_For_Activation_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click on waitingForActivation Check Box 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Waiting_For_Activation_CheckBox.User.Click();
            }
        }

        // Method to Select the Status Filter Check Boxes on Received Silo
        public void P2PPaymentPlanReceivedStatusFilters(string receivedFilter)
        {
            // Wait for invalid Check Box Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Invalid_CheckBox.Wait.ForExists(Globals.timeOut);
            // Wait for draft Check Box Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Draft_CheckBox.Wait.ForExists(Globals.timeOut);
            // Wait for Valid Check Box Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Valid_CheckBox.Wait.ForExists(Globals.timeOut);
            // Wait for Requested Check Box Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Requested_CheckBox.Wait.ForExists(Globals.timeOut);

            switch (receivedFilter.ToUpper())
            {
                case "DRAFT":
                    {
                        //Condition for the draft CheckBox
                        if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Invalid_CheckBox.IsChecked.Equals(true))
                        {
                            // Click the invalid Check Box to uncheck it
                            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Invalid_CheckBox.User.Click();
                        }

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Draft_CheckBox.IsChecked.Equals(false))
                        {
                            // Click the draft Check Box to select it
                            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Draft_CheckBox.User.Click();
                        }

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Valid_CheckBox.IsChecked.Equals(true))
                        {
                            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Valid_CheckBox.User.Click();
                        }

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Requested_CheckBox.IsChecked.Equals(true))
                        {
                            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Requested_CheckBox.User.Click();
                        }
                        break;
                    }

                case "INVALID":
                    {
                        //Condition for the invalid CheckBox
                        if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Draft_CheckBox.IsChecked.Equals(true))
                        {
                            // Click the invalid Check Box to uncheck it
                            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Draft_CheckBox.User.Click();
                        }

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Invalid_CheckBox.IsChecked.Equals(false))
                        {
                            // Click the draft Check Box to unselect it
                            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Invalid_CheckBox.User.Click();
                        }

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Valid_CheckBox.IsChecked.Equals(true))
                        {
                            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Valid_CheckBox.User.Click();
                        }

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Requested_CheckBox.IsChecked.Equals(true))
                        {
                            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Requested_CheckBox.User.Click();
                        }
                        break;
                    }

                case "VALID":
                    {
                        //Condition for the invalid CheckBox
                        if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Draft_CheckBox.IsChecked.Equals(true))
                        {
                            // Click the invalid Check Box to uncheck it
                            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Draft_CheckBox.User.Click();
                        }

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Invalid_CheckBox.IsChecked.Equals(true))
                        {
                            // Click the draft Check Box to unselect it
                            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Invalid_CheckBox.User.Click();
                        }

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Valid_CheckBox.IsChecked.Equals(false))
                        {
                            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Valid_CheckBox.User.Click();
                        }

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Requested_CheckBox.IsChecked.Equals(true))
                        {
                            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Requested_CheckBox.User.Click();
                        }
                        break;
                    }
                case "REQUESTED":
                    {
                        //Condition for the "Uncheck" All CheckBoxes
                        if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Draft_CheckBox.IsChecked.Equals(true))
                        {
                            // Click the invalid Check Box to uncheck it
                            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Draft_CheckBox.User.Click();
                        }

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Invalid_CheckBox.IsChecked.Equals(true))
                        {
                            // Click the draft Check Box to unselect it
                            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Invalid_CheckBox.User.Click();
                        }

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Valid_CheckBox.IsChecked.Equals(true))
                        {
                            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Valid_CheckBox.User.Click();
                        }

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Requested_CheckBox.IsChecked.Equals(false))
                        {
                            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Requested_CheckBox.User.Click();
                        }

                        break;
                    }

                case "NO FILTER":
                    {
                        //Condition for the "Uncheck" All CheckBoxes
                        if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Draft_CheckBox.IsChecked.Equals(true))
                        {
                            // Click the invalid Check Box to uncheck it
                            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Draft_CheckBox.User.Click();
                        }

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Invalid_CheckBox.IsChecked.Equals(true))
                        {
                            // Click the draft Check Box to unselect it
                            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Invalid_CheckBox.User.Click();
                        }

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Valid_CheckBox.IsChecked.Equals(true))
                        {
                            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Valid_CheckBox.User.Click();
                        }

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Requested_CheckBox.IsChecked.Equals(true))
                        {
                            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Requested_CheckBox.User.Click();
                        }

                        break;
                    }
                default:
                    {
                        //Log error if grid not found on page.
                        Manager.Current.Log.WriteLine(LogType.Error, "  Filter on Received Silo Not Found. Please Check!!");

                        break;
                    }
            }
        }

        // Method to Click on PaymentPlan Grid Cell 
        public void P2PPaymentPlanGridClickInTheCell(string gridCell)
        {
            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol")).Wait.ForExists(Globals.timeOut);

            //Find the cell Value in Grid
            TextBlock gridCellValue = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol")).Find.ByTextContent(gridCell).As<TextBlock>();

            //Click on Cell 
            gridCellValue.User.Click();
        }

        // Method to Verify the Data according to the selected Filters
        public void P2PPaymentPlanWorkflowStatusFilters(Boolean Pending, Boolean Exceptions)
        {
            //Using if condition to select checkbox : Pending & Exceptions
            if (Pending == true && Exceptions == true)
            {
                // Wait for Deactivated Check Box Exist in DOM 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_Pending_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click on Deactivated Check Box 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_Pending_CheckBox.User.Click();

                // Wait for Expired Check Box Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_Exceptions_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click on Expired Check Box 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_Exceptions_CheckBox.User.Click();
            }


            //Using if condition to select checkbox : Exceptions
            if (Pending == true && Exceptions == false)
            {
                // Wait for Deactivated Check Box Exist in DOM 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_Pending_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click on Deactivated Check Box 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_Pending_CheckBox.User.Click();
            }


            //Using if condition to select checkbox :expired
            if (Pending == false && Exceptions == true)
            {
                // Wait for Deactivated Check Box Exist in DOM 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_Pending_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click on Deactivated Check Box 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_Pending_CheckBox.User.Click();

                // Wait for Expired Check Box Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_Exceptions_CheckBox.Wait.ForExists(Globals.timeOut);

                // Click on Expired Check Box 
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_Exceptions_CheckBox.User.Click();
            }
        }
        
        //Method to Clear the Search Textbox
        public void P2PPaymentPlanClearSearch()
        {
            //Wait for Search text box exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.Wait.ForExists(Globals.timeOut);

            //Set Focus on set box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.SetFocus();

            //Press Ctrl A
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(ArtOfTest.WebAii.Win32.KeyBoard.KeysFromString("Ctrl+A"));

            //Generate a Keyboard Event to clear search
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

            //Wait for Clear Search Button Exits in DOM 
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=ClearButton", "XamlTag=button")).Wait.ForExists(Globals.timeOut);

            //Click on Clear Search 
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=ClearButton", "XamlTag=button")).User.Click();
            
        }

        //Method to Select Multiple Payment Plans
        public void P2PSelectMultiplePaymentPlans(string[] planNumber, string headerCell, string sorting = null)
        {
            //Declare testblock to store invoice number column 
            TextBlock invoiceNumberHeaderCell = new TextBlock();

            //Get the invoice grid on page
            FrameworkElement e = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol"));

            //assert that grid is not null
            Assert.IsNotNull(e);

            //Find the Header cell Value as Invoice Number
            invoiceNumberHeaderCell = e.Find.ByTextContent(headerCell).As<TextBlock>();

            if (sorting != null)
            {
                //Click to Sort the Grid
                invoiceNumberHeaderCell.User.Click();
            }

            //Declare an array of textblocks
            TextBlock[] plan = new TextBlock[planNumber.Length];

            //declare index variables for loops
            int indexPlan = 0;

            //get all Plan numbers as text blocks
            foreach (string pNum in planNumber)
            {
                //store plannumber text block
                plan[indexPlan] = e.Find.ByTextContent(pNum).As<TextBlock>();

                //increment index
                indexPlan++;
            }

            //reset array index
            indexPlan = 0;

            //Click One first plan
            plan[indexPlan].User.Click();

            //Press Ctrl A
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

            ////Press the Control Key down
            //Manager.Current.Desktop.KeyBoard.KeyDown(Keys.Control);

            ////Click on the Multiple rows you want to select 
            //plan[indexPlan++].User.Click();

            ////Click on the Multiple rows you want to select 
            //plan[indexPlan++].User.Click();

            ////Release the Control Key Up
            //Manager.Current.Desktop.KeyBoard.KeyUp(Keys.Control);
        }

        //Method to click on edit and input data in description textbox
        public void P2PPaymentPlanEditAndAddDescriptionData(string description)
        {
            //Wait for button exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Search_InvoiceDetailEditButton.Wait.ForExists(Globals.timeOut);

            //Click on Edit Button
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Search_InvoiceDetailEditButton.User.Click();

            //Calling Handle Busy Indicator
            P2PNavigation.CallBusyIndicator();

            //Wait for TextBox exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_DescriptionHeaderDataTextbox.Wait.ForExists(Globals.timeOut);

            //SetFocus on TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_DescriptionHeaderDataTextbox.SetFocus();

            //Press Ctrl A
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

            //Generate a Keyboard Event to clear search
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

            //Enter Data in TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_DescriptionHeaderDataTextbox.User.TypeText(description, 50);

            //Generate a Keyboard Event
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Tab);
        }        

        //Method to Select All Checkboxes in Payment Plans (Personal Mode Search)
        public void P2PPaymentPlansPersonalModeSearchFilters(string casetoTest, Boolean pending, Boolean compleated, Boolean canceled, Boolean collaborated)
        {         
            // Wait for Pending Check Box Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_Pending_CheckBox.Wait.ForExists(Globals.timeOut);
            // Wait for Cancelled Check Box Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PaymentPlans_Canceled_CheckBox.Wait.ForExists(Globals.timeOut);            
            // Wait for Compleated Check Box Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PaymentPlans_Compleated_CheckBox.Wait.ForExists(Globals.timeOut);
            // Wait for Colloborated Check Box Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PaymentPlans_Collaborated_CheckBox.Wait.ForExists(Globals.timeOut);

            switch (casetoTest)
            {
                case "All Filters Selected":
                    {

                        // Click on Pending Check Box 
                        SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_Pending_CheckBox.User.Click();
                        // Click on Cancelled Check Box 
                        SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PaymentPlans_Canceled_CheckBox.User.Click(); 
                        // Click on Compleated Check Box 
                        SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PaymentPlans_Compleated_CheckBox.User.Click();
                        // Click on Colloborated Check Box 
                        SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PaymentPlans_Collaborated_CheckBox.User.Click();
                        
                        break;
                    }
                default:
                    {
                        //Log error if grid not found on page.
                        Manager.Current.Log.WriteLine(LogType.Error, "  Filters on Personal Mode Search Payment Plans are Not Found.");

                        break;
                    }
            }
        }
    }
}


        

    
