using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using Telerik.WebAii.Controls.Xaml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArtOfTest.Common.Exceptions;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Controls.Xaml.Wpf;


namespace P2P.Testing.Shared.Class.Purchase
{
    public class P2PPurchaseDataManagementVerification : BaseWebAiiTest
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


        //Method to verify Welcome Page Deactivate Button
        public void P2PPDMWelcomePageVerifyDeactivateButton()
        {
            try
            {
                //Wait for Search Textbox to Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_DeactivateButton.Wait.ForExists(Globals.timeOut);

                //Verify P2P_Purchase_Data_Management_WelcomePage_DeactivateButton's is Visible
                Assert.AreEqual(ArtOfTest.WebAii.Silverlight.UI.Visibility.Visible, SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_DeactivateButton.ComputedVisibility, "Element visibility does not match expected value");

                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, " Welcome Page Deactivate button Found. Verification Passed.");

            }
            catch (AssertException ex)
            {
                //Write the log if Verification Fail
                throw new Exception(" Welcome Page Deactivate button not Found. Verification Failed!!! Message: " + ex.Message);
            }
        }

        //Method to verify Welcome Page is Inherited 
        public void P2PPDMWelcomePageVerifyInherited()
        {
            //Wait for Inherit checkbox to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_InheritCheckBox.Wait.ForExists(Globals.timeOut);

            if (SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_InheritCheckBox.IsChecked.Equals(true))
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, " Welcome Page is inherited. Verificaton Passed !");
            }
            else
            {
                //Write the log if Verification Fails
                Manager.Current.Log.WriteLine(LogType.Error, " Welcome Page is Not inherited. Verificaton Failed !");
            }

        }

        //Method to verify Welcome Page is not deleted
        public void P2PPDMVerifyWelcomePageIsNotDeleted()
        {
            //Wait for Additional actions dropdown to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Click on Additional actions dropdown
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            //wait for delete button to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_MoreActions_DeleteButton.Wait.ForExists(Globals.timeOut);

            //Verify delete button
            if (SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_MoreActions_DeleteButton.IsEnabled.Equals(false))
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, " Welcome Page cannot be deleted. Verificaton Passed !");

                //Click on Additional actions dropdown to close the dropdown
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();
            }
            else
            {
                //Write the log if Verification Fails
                Manager.Current.Log.WriteLine(LogType.Error, " Welcome Page can be deleted. Verificaton Failed ! Delete Button Found active.");
            }

        }

        //Method to verify Welcome Page is Deleted from grid 
        public void P2PPDMWelcomePageVerifyIsDeleted()
        {
            //Wait for Grid to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_GridViewControl.Wait.ForExists(Globals.timeOut);

            //Create a new RadGridView Control
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_GridViewControl;

            //If condition is true then execute  if block
            if (grid.Rows.Count.Equals(0))
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, " Grid is empty, Welcome Page Deleted. Verificaton Passed !!");
            }
            else
            {
                //Write the log if Verification Fails
                Manager.Current.Log.WriteLine(LogType.Error, " Welcome Page not deleted. Verificaton Failed !");
            }


        }

        //Method to verify Welcome Page Filter's(Active and inactive) status
        public void P2PPDMVerifyWelcomePageFilterStatus(string filterCount)
        {
            //Wait for checkbox (Active and inactive) to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_PersonalMode_SearchFilters_PendingCheckBox.Wait.ForExists(Globals.timeOut);
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_InActiveCheckBox.Wait.ForExists(Globals.timeOut);

            //Get state of each checkbox
            bool active = SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_PersonalMode_SearchFilters_PendingCheckBox.IsChecked.Value;
            bool inactive = SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_InActiveCheckBox.IsChecked.Value;

            if (filterCount.Equals("ALL"))
            {
                //If both checkbox are selected
                if (active.Equals(true) && inactive.Equals(true))
                {
                    //Write the log if Verification Pass
                    Manager.Current.Log.WriteLine(LogType.Information, " Welcome Page Filters : (Active and inactive), are checked. Verificaton Passed !");
                }
                else
                {
                    //Write the log if Verification Fails
                    Manager.Current.Log.WriteLine(LogType.Error, " Welcome Page Filters : (Active and inactive), are NOT checked. Verificaton Failed !");
                }
            }

            if (filterCount.Equals("SINGLE"))
            {
                //If Active CheckBox is Checked
                if (active.Equals(true) && inactive.Equals(false))
                {
                    //Write the Logs if Active Checkbox is Checked and Others are UnChecked (Verification is Passed)
                    Manager.Current.Log.WriteLine(" Welcome Page Filters :Active Checkbox is Checked. All Other Checkbox are UnChecked.");
                }

               //If inactive CheckBox is Checked
                else if (active.Equals(false) && inactive.Equals(true))
                {
                    //Write the Logs if inactive Checkbox is Checked and Others are UnChecked (Verification is Passed)
                    Manager.Current.Log.WriteLine(" Welcome Page Filters :Inactive Checkbox is Checked. All Other Checkbox are UnChecked.");
                }

                //If No Check Box is Checked  
                else if (active.Equals(false) && inactive.Equals(false))
                {
                    //Write the Logs if No Checkbox is Checked (Verification is Passed)
                    Manager.Current.Log.WriteLine(" Welcome Page Filters :All Checkbox are UnChecked.");
                }

               //Throw the Exception in case of Verification Failed.
                else
                {
                    //throws the Exception if wrong checkbox is checked
                    throw new Exception(" Welcome Page Filters :Wrong Checkbox is Checked, Verification Failed!");
                }
            }

        }

        //Method to Verify SubStatus/Status field in Grid
        //Method Obselete: status column not visible on grid. Will wait for specification to change, not deleting function.
        public void P2PPDMVerifyWelcomePageGridStatus(string secondryStatus, string headerName)
        {
            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_GridViewControl.Wait.ForExists(Globals.timeOut);

            //Create a new RadGridView Control
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_GridViewControl;

            try
            {

                //Check value in Exact Column using Column Header Name Value
                GridViewHeaderCell header = grid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == headerName);
                //Check Header value is not Null
                Assert.IsNotNull(header);

                if (grid.Rows.Count.Equals(0))
                {
                    Manager.Current.Log.WriteLine(LogType.Information, " There is No WelcomePage Found by Status Filter  :" + secondryStatus + ", Total Number of Records is 0");
                }
                else
                {
                    //Count all rows and save it in index varibable 
                    int index = header.Index;
                    //Verify Results in each row in grid
                    foreach (GridViewRow row in grid.Rows)
                    {
                        //Compare Each Row Value
                        if (row.Cells[index].TextBlockContent != secondryStatus)
                        {
                            //Write the log if Verification Fail
                            throw new Exception("WelcomePage Found by Other Secondary Status Verification Failed!!!!");
                        }
                    }

                    // Write the log if verification is Pass 
                    Manager.Current.Log.WriteLine(LogType.Information, " WelcomePage Found by Status Filter : " + secondryStatus);
                }
            }
            catch (AssertException ex)
            {
                // Write the log if verification is Fail
                throw new Exception("Unable to Find the Column Header:" + " " + headerName + "Unable to Verify!" + ex.Message);
            }
        }

        //Method to verify "Active/Inactive" status in  BasicData Panel PopUp
        public void P2PPDMVerifyWelcomePageFilterStatusInPopUp(string compareValue)
        {
            //Wait for BasicData Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HeaderDataButton.Wait.ForExists(Globals.timeOut);

            //Click on BasicData Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HeaderDataButton.User.Click();

            var screenValue = SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_BasicDataPopUp_StatusTextBox.Text.Trim();

            //Verify value on pop up
            if (compareValue.ToUpper() == screenValue.ToUpper())
            {
                //Log the Results if correct  Data Exists
                Manager.Current.Log.WriteLine(LogType.Information, " BasicData Pop Up Window:Status Column shows correct value : " + compareValue);
            }
            else
            {
                //Log the Results if incorrect Data Exists
                Manager.Current.Log.WriteLine(LogType.Error, " BasicData Pop Up Window:Status Column shows incorrect value : " + compareValue);
            }

            //Close the  BasicData Pop-up Window
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.User.Click();
        }

        //Method to verify AttachmentsPanelPopUp
        public void P2PPDMVerifyAttachmentDetailsPopUp(string[] fileName, string[] itemName, int arraySize)
        {
            //declare index variables for loops, statusflag : checks where verifications are failing in a loop for different iterations.
            int index = 0;
            bool statusflag = true;

            //Declare Framework element for attachment listbox
            FrameworkElement e;

            //Declare an array of textblocks
            TextBlock[] purchaseItem = new TextBlock[arraySize];

            //get all purchase items as text blocks
            foreach (string pNum in itemName)
            {
                //store item text block
                purchaseItem[index] = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(pNum).As<TextBlock>();

                //increment index
                index++;
            }

            //reset index 
            index = 0;

            //Wait for attachment Link Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_AttachmentButton.Wait.ForExists(Globals.timeOut);

            //Click on attachment Link Under Details Panel 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_AttachmentButton.User.Click();

            //Wait for attachment Pop-up to appear
            System.Threading.Thread.Sleep(Globals.pause);

            //Verify the attachments in Pop-Up
            foreach (string fName in fileName)
            {
                //Click On next purchase item
                purchaseItem[index].User.Click();

                //Wait for purchaseItem to get selected
                System.Threading.Thread.Sleep(Globals.pause);

                //Wait for attachments ListBox Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_InvoiceDetails_Attachment_AttachmentListbox.Wait.ForExists(Globals.timeOut);

                //Take Frameowrk Element for attachments Pop-Up
                e = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_InvoiceDetails_Attachment_AttachmentListbox;

                //initialize boolean 
                bool strFound = false;

                //Check the TextBlock contains the attachments.
                if (e != null)
                {
                    //read textblock of  Attachment Pop-Up
                    strFound = (e.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(fName.ToString()))) && (fName.ToUpper() != "No attachments".ToUpper());

                    //Check using If Condition that Attachment Exists
                    if (strFound == true)
                    {
                        //Case 1: Log the Results if correct Attachment File Exists
                        Manager.Current.Log.WriteLine(LogType.Information, "Correct Attachment File:( " + " " + fName + " " + " ) Exists in Attachments Pop-up, against Purchase Item=  " + " " + itemName[index]);
                    }
                    else
                    {
                        //Case 2: :Log the Results when plan has no Attachment and Attachment Listbox has just 1 textblock
                        if (e.Find.AllByType<TextBlock>().Count == 1)
                        {
                            //Verify Attachment in test data file
                            if (fName.ToUpper() == "No attachments".ToUpper())
                                //Log the Results when plan has no Attachment
                                Manager.Current.Log.WriteLine(LogType.Information, "No Attachment Exist in Attachment Pop-up, against Purchase Item=  " + " " + itemName[index]);
                            else
                            {
                                //Case 3 :set the bool flag if Attachment are showing incorrect 
                                //Log the Results when Purchase Item has incorrect Attachments
                                Manager.Current.Log.WriteLine(LogType.Information, "************* Fail : Incorrect Attachment shows in Attachment Pop-up, against Purchase Item=  " + " " + itemName[index] + ". Searching for Attachment  : " + fName);
                                statusflag = false;
                            }
                        }
                        else
                        {
                            //Case 3 :set the bool flag if Attachment are showing incorrect 
                            //Log the Results when Purchase Item has incorrect Attachment
                            Manager.Current.Log.WriteLine(LogType.Information, "************* Fail : Incorrect Attachment shows in Attachment Pop-up, against Purchase Item=  " + " " + itemName[index] + ". Searching for Attachment : " + fName);
                            statusflag = false;
                        }
                    }
                }
                else
                {
                    //Log the Results if attachments Listbox doesnt exist 
                    Manager.Current.Log.WriteLine(LogType.Error, "Attachments Pop-up not visible, Verification Failed!, against Purchase Item=  " + " " + itemName[index]);
                }

                //increment index for array's purchaseItem[], fileName[]
                index++;

                //Set focus on Plan grid
                SharedElement.P2P_Application.SilverlightApp.P2P_PurchaseDataManagement_PurchaseItems_GridViewControl.SetFocus();
            }

            //Close the Attachments Pop-Up
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.User.Click();

            if (statusflag == false)
            {
                //Log the Error if Attachment name was showing incorrect for any plan
                Manager.Current.Log.WriteLine(LogType.Error, "Attachments Pop Up Details incorrect. Verify for Purchase Item Name from log's above.");
            }
        }

        //Method to verify edit in Purchase Item (Name field)
        public void P2PPDMVerifyPurchaseItemDetails(string purchaseItemName)
        {
            //Wait for Name Textbox to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_NameTextBox.Wait.ForExists(Globals.timeOut);

            //Get value of Text Box
            var txtName = SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_NameTextBox.Text.Trim();

            if (txtName.ToUpper() == purchaseItemName.ToUpper())
            {
                //Write the log if Verification Passes
                Manager.Current.Log.WriteLine(LogType.Information, " Purchase Item name shows Correct value (" + txtName + ") Verificaton Passed!!");

            }
            else
            {
                //Write the log if Verification Fails
                Manager.Current.Log.WriteLine(LogType.Error, " Purchase Item name shows Incorrectvalue (" + txtName + ")  Verificaton Failed !!");

            }
        }

        //Method to verify MandatoryData Validation Hyperlink shows
        //Only used in one script. Try to use P2PInvoiceAdmiminstartionMandatoryDataVerification() under class P2PInvoiceAdministrationVerification
        public void P2PPDMVerifyPurchaseItemMandatoryDataCheck()
        {
            try
            {
                // Verify P2P_Purchase_Data_Management_PurchaseItems_ValidationHyperlinkButton's visibility is Visible
                Assert.AreEqual(ArtOfTest.WebAii.Silverlight.UI.Visibility.Visible, SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseItems_ValidationHyperlinkButton.ComputedVisibility, "Element visibility does not match expected value");

                //Write the log if Verification Passes
                Manager.Current.Log.WriteLine(LogType.Information, " Mandatory check shows, Verification Passed!!");
            }
            catch (AssertException ex)
            {
                // Write the log if verification is Fail
                throw new Exception(" Mandatory check doesnt show, Verification Failed!! " + ex.Message);
            }
        }

        //Method to verify Always Valid checkbox state
        public void P2PPDMVerifyAlwaysValidCheckbox(string validFlag, string dateFields = null)
        {
            //Wait for checkbox to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_AlwaysValidCheckBox.Wait.ForExists(Globals.timeOut);

            //Case: to verify if Always Valid Checkbox is Checked
            if (validFlag.ToUpper() == "TRUE")
            {
                //Check if valid checkbox is checked
                if (SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_AlwaysValidCheckBox.IsChecked.Equals(true))
                {
                    //Write the log if Verification Passes
                    Manager.Current.Log.WriteLine(LogType.Information, " Always Valid Checkbox is checked. Verification Passes!!");

                    //Check if Date From and Date To fields are disabled
                    if (dateFields != null)
                    {
                        //initaliase varaibles
                        bool dateTo = false;
                        bool dateFrom = false;

                        //Wait for date textbox Exists in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseCategory_DateValidFrom_Textbox.Wait.ForExists(Globals.timeOut);
                        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseCategory_DateValidTo_Textbox.Wait.ForExists(Globals.timeOut);

                        //Get Value of Date Fields- Enabled or Disabled
                        dateFrom = SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseCategory_DateValidFrom_Textbox.IsEnabled;
                        dateTo = SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseCategory_DateValidTo_Textbox.IsEnabled;

                        if (!(dateFrom && dateTo))
                            //Write the log if Verification Passes
                            Manager.Current.Log.WriteLine(LogType.Information, " Both Date Fields: Date From and Date To are disabled. Verification Passes!!");
                        else
                            //Write the log if Verification Fails
                            Manager.Current.Log.WriteLine(LogType.Error, " One of the Date Fields: Date From and Date To is enabled. Verificaton Failed !!");
                    }
                }
                else
                    //Write the log if Verification Fails
                    Manager.Current.Log.WriteLine(LogType.Error, " Always Valid Checkbox is NOT checked. Verificaton Failed !!");
            }
            //Case: to verify if Always Valid Checkbox is NOT Checked
            else
            {
                //Check if valid checkbox is not checked
                if (SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_AlwaysValidCheckBox.IsChecked.Equals(false))
                {
                    //Write the log if Verification Passes
                    Manager.Current.Log.WriteLine(LogType.Information, " Always Valid Checkbox is Not checked. Verification Passes!!");

                    //Check if Date From and Date To fields are enabled
                    if (dateFields != null)
                    {
                        //initaliase varaibles
                        bool dateTo = false;
                        bool dateFrom = false;

                        //Wait for date textbox Exists in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseCategory_DateValidFrom_Textbox.Wait.ForExists(Globals.timeOut);
                        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseCategory_DateValidTo_Textbox.Wait.ForExists(Globals.timeOut);

                        //Get Value of Date Fields- Enabled or Disabled
                        dateFrom = SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseCategory_DateValidFrom_Textbox.IsEnabled;
                        dateTo = SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseCategory_DateValidTo_Textbox.IsEnabled;

                        if ((dateFrom && dateTo))
                        {
                            //Write the log if Verification Passes
                            Manager.Current.Log.WriteLine(LogType.Information, " Both Date Fields: Date From and Date To are enabled. Verification Passes!!");

                            //Get Value of Textbox Date Valid From
                            string dateValue = SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseCategory_DateValidFrom_Textbox.Text.ToString();
                            string expectedDateValue = System.DateTime.Today.ToShortDateString();

                            //Check if Date Valid textbox has current system date
                            if (dateValue == expectedDateValue)
                                //Write the log if Verification Passes
                                Manager.Current.Log.WriteLine(LogType.Information, " Date From field shows Current System date (" + dateValue + ") . Verification Passes!!");
                            else
                                //Write the log if Verification Fails
                                Manager.Current.Log.WriteLine(LogType.Error, " Date From field doesnt show Current System date. Date showing is (" + dateValue + ") ,whereas Current Date is (" + expectedDateValue + ") . Verificaton Failed!!");
                        }
                        else
                            //Write the log if Verification Fails
                            Manager.Current.Log.WriteLine(LogType.Error, " One of the Date Fields: Date From or Date To is disabled. Verificaton Failed !!");
                    }
                }
                else
                    //Write the log if Verification Fails
                    Manager.Current.Log.WriteLine(LogType.Error, " Always Valid Checkbox is checked. Verificaton Failed !!");
            }
        }

        //Method to verify value in date fields: dateFrom and dateTo
        public void P2PPDMVerifyDateFromDateTo(string expectedDateFrom, string expectedDateTo)
        {
             //Wait for date textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseCategory_DateValidFrom_Textbox.Wait.ForExists(Globals.timeOut);
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseCategory_DateValidTo_Textbox.Wait.ForExists(Globals.timeOut);

            //Get Value of Date Fields
            string actualDateFrom = SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseCategory_DateValidFrom_Textbox.Text.ToString();
            string actualDateTo = SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseCategory_DateValidTo_Textbox.Text.ToString();

            if ((actualDateFrom == expectedDateFrom) && (actualDateTo == expectedDateTo))
                //Write the log if Verification Passes
                Manager.Current.Log.WriteLine(LogType.Information, " Date From (" + actualDateFrom + ") and Date To (" + actualDateTo + ") fields shows correct dates. Verification Passes!!");
            else
            {
                //Write the log if Verification Fails
                Manager.Current.Log.WriteLine(LogType.Information, " Actual Date From (" + actualDateFrom + "). Expected Date From (" + expectedDateFrom + ")");
                Manager.Current.Log.WriteLine(LogType.Information, " Actual Date To (" + actualDateTo + "). Expected Date To (" + expectedDateTo + ")");
                Manager.Current.Log.WriteLine(LogType.Error, " Date From and Date To fields shows incorrect dates. Verificaton Failed!!");
            }
        }

        //Method to verify Item is deactivated: i.e activate button will show.
        public void P2PPDMVerifyDeactivated()
        {
            try
            {
                //Wait for element to Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_ActivateButton.Wait.ForExists(Globals.timeOut);

                // Verify P2P_Purchase_Data_Management_ToolBar_DetailsPage_ActivateButton's visibility is Visible
                Assert.AreEqual(ArtOfTest.WebAii.Silverlight.UI.Visibility.Visible, SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_ActivateButton.ComputedVisibility, "Element visibility does not match expected value");
               
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, " Item Deactivated (i.e activate button shows). Verification Passed.");
            }
            catch (AssertException ex)
            {
                //Write the log if Verification Fail
                Manager.Current.Log.WriteLine(LogType.Error, " Deactivation Failed, Verification Failed!!! Message: " + ex.Message);
            }
        }

        //Method to verify Item is activated: i.e deactivate button will show.
        public void P2PPDMVerifyActivated()
        {
            try
            {
                //Wait for element to Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_DeactivateButton.Wait.ForExists(Globals.timeOut);

                // Verify P2P_Purchase_Data_Management_ToolBar_DetailsPage_DeactivateButton's visibility is Visible
                Assert.AreEqual(ArtOfTest.WebAii.Silverlight.UI.Visibility.Visible, SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_DeactivateButton.ComputedVisibility, "Element visibility does not match expected value");

                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, " Item Activated (i.e deactivate button shows). Verification Passed.");
            }
            catch (AssertException ex)
            {
                //Write the log if Verification Fail
                Manager.Current.Log.WriteLine(LogType.Error, " Activation Failed, Verification Failed!!! Message: " + ex.Message);
            }
        }

        public void P2PPDMVerifyPurchaseItemMandatoryDataFields(string companyName, string categoryName, string description, string internalDesc, string keywords)
        {
	        //Wait for Organisation Textbox to Exists in DOM
	        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.Wait.ForExists(Globals.timeOut);

            //Set Focus in TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.SetFocus();

	        //Get Value and Save in String Variable
            string organisationTextBoxText = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.Text.ToString();

	        //Wait for Purchasing Category Textbox to Exists in DOM
	        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseItems_PurchasingCategoryTextbox.Wait.ForExists(Globals.timeOut);

	        //Get Value and Save in String Variable
            string purchasingCategoryTextBoxText = SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseItems_PurchasingCategoryTextbox.Text.ToString();

	        //Wait for Description Textbox to Exists in DOM
	        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);

	        //Get Value and Save in String Variable
            string descriptionTextBoxText = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Text.ToString();

	        //Wait for Internal Description Textbox to Exists in DOM
	        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseItems_InternalDescriptionTextBox.Wait.ForExists(Globals.timeOut);

	        //Get Value and Save in String Variable
            string internalDescriptionTextBoxText = SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseItems_InternalDescriptionTextBox.Text.ToString();

	        //Wait for Keyword Textbox to Exists in DOM
	        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseItems_KeywordsTextBox.Wait.ForExists(Globals.timeOut);

	        //Get Value and Save in String Variable
            string keywordTextBoxText = SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseItems_KeywordsTextBox.Text.ToString();

	        //Data Condition Check
	        if(organisationTextBoxText == companyName && purchasingCategoryTextBoxText == categoryName && descriptionTextBoxText == description && internalDescriptionTextBoxText == internalDesc &&
	           keywordTextBoxText == keywords)
	        {
		        //Write the log if Verification Passes
		        Manager.Current.Log.WriteLine(LogType.Information, " Purchase Items Data shows Correct value ('" + organisationTextBoxText + "' & '" + purchasingCategoryTextBoxText + "' & '" + descriptionTextBoxText + "' & '" + internalDescriptionTextBoxText + "' & '" + keywordTextBoxText + "') Verificaton Passed!!");
	        }
	
	        else
	        {
		        //Write the log if Verification Fails
		        Manager.Current.Log.WriteLine(LogType.Error, " Purchase Item name shows Incorrect value ('" + organisationTextBoxText + "' & '" + purchasingCategoryTextBoxText + "' & '" + descriptionTextBoxText + "' & '" + internalDescriptionTextBoxText + "' & '" + keywordTextBoxText + "')  Verificaton Failed !!");
	        }
        }

        //Method to verify PurchasingCategory Tab data:  Supplier, Account, Task Management
        public void P2PPDMVerifyPurchasingCategoryTabs(string tabName, string compareValue)
        {
            switch (tabName.ToUpper())
            {
                case "SUPPLIER":
                    {
                        //Wait for element to Exists in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem2.Wait.ForExists(Globals.timeOut);
                        //Click on element
                        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem2.User.Click();

                        //Break out of loop
                        break;
                    }
                case "ACCOUNT":
                    {
                        //Wait for element to Exists in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem3.Wait.ForExists(Globals.timeOut);
                        //Click on element
                        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem3.User.Click();

                        //Break out of loop
                        break;
                    }
                case "TASK MANAGEMENT":
                    {
                        //Wait for element to Exists in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem4.Wait.ForExists(Globals.timeOut);
                        //Click on element
                        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem4.User.Click();

                        //Break out of loop
                        break;
                    }
                case "BASIC DATA":
                    {
                        //Wait for element to Exists in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem1.Wait.ForExists(Globals.timeOut);
                        //Click on element
                        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem1.User.Click();

                        //Goto Label to end the function     
                        goto Over;
                    }
                default:
                    {
                        //Write the log if Verification Fails
                        Manager.Current.Log.WriteLine(LogType.Error, " Tab Name is incorrect. Please verify!!");

                        //Break out of loop
                        break;
                    }                    
            }

            //Put the ListBox into a variable.
            FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=~grid")).CastAs<RadGridView>();

            //Check whether any TextBlock in the ListBox contains the specified string.
            bool found = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(compareValue));            

            //Log the results.
            if (found == true)
            {
                //Write the log if Verification Passes
                Manager.Current.Log.WriteLine(LogType.Information, " Grid contains value (" + compareValue + "). Verification Passes.");
            }
            else
            {
                //Write the log if Verification Fails
                Manager.Current.Log.WriteLine(LogType.Error, " Grid does not contain value (" + compareValue + "). Verificaton Failed !!");
            }
        Over: ;
        }


    }
}
