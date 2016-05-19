using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using Telerik.WebAii.Controls.Xaml;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Silverlight.UI;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.Common.Exceptions;
using System.Globalization;
using P2P.Testing.Shared.Class.InvoiceAdministration;
using System.Xml;
using System.Windows.Forms;

namespace P2P.Testing.Shared.Class.PaymentPlans
{
    public class P2PPaymentPlansVerification : BaseWebAiiTest
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

        //Method to verify Free Text Search: PP Search Silo. Common grid function didnt work for this screen.        
        public void P2PPaymentPlanVerifyFreeTextSearch(string headerName, int iteration, string searchText)
        {
            //This bool value used for "Exit from For Loop "
            bool searchResultsFound = false;

            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control.Wait.ForExists(Globals.timeOut);
            //Create a new RadGridView Control
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control;

            //Declare Final Grid Variable
            RadGridView finalgrid = grid;     

            //If condition is true then execute  if block
            if (grid.Rows.Count.Equals(0))
            {
                //Execute for loop for search the invoice                
                for (int i = 0; i < iteration; i++)
                {
                    //Wait for Search Textbox to Exists in DOM Tree
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.Wait.ForExists(Globals.timeOut);

                    //Clear Search Textbox
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.Text = string.Empty;

                    //Click on Search button 
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();

                    //Wait for button
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_RefreshButton.Wait.ForExists(Globals.timeOut);

                    //Click on button
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_RefreshButton.User.Click();

                    //Wait for Search Textbox to Exists in DOM Tree
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.Wait.ForExists(Globals.timeOut);

                    //Enter the Search Term to search for invoice
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.Text = searchText;

                    //Click on Search button 
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();

                    //Create a new RadGridView Control
                    RadGridView newgrid = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control;

                    //Condition For Exis from the Loop
                    if (newgrid.Rows.Count.Equals(0) == false)
                    {
                        searchResultsFound = true;
                        Manager.Current.Log.WriteLine(LogType.Information, " New grid reached");
                        //Set Current Grid
                        finalgrid = newgrid;
                        break;
                    }
                }
            }
            else
            {
                searchResultsFound = true;
            }

            //If grid has some rows then execute this block
            if (searchResultsFound == true)
            {
                //Check value in Exact Column using Column Header Name Value
                GridViewHeaderCell header = finalgrid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == headerName);
                //Count all rows and save it in index variable
                int index = header.Index;

                //Verify Results in each row in grid
                foreach (GridViewRow row in finalgrid.Rows)
                {
                    //Compare Each Row Value
                    if (row.Cells[index].TextBlockContent != searchText)
                    {
                        //Write the log if Verification Fail
                        Manager.Current.Log.WriteLine(LogType.Error, " Error: Grid shows row/rows having value other than: (" + searchText + "), under column: (" + headerName + "). Actual value showing: (" + row.Cells[index].TextBlockContent + "), under column: (" + header.Text + ") .Verification Failed!!");
                    }
                }
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, " Grid shows row/rows searched by text: (" + searchText + "), under column: (" + headerName + "). Verification Passed!");
            }
            else
            {
                //Write the log if Verification Fail
                Manager.Current.Log.WriteLine(LogType.Error, " Grid is empty, text : (" + searchText + "), under column: (" + headerName + ") is Not found.");
            }
        }

        //Method To Verify Payment Plan Search By Company: Search Silo. Common grid function didnt work for this screen.
        public void P2PPaymentPlanVerifySearchByCompanyPicker(string company, string headerName)
        {
            try
            {
                //RadGridView grid = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol")).CastAs<RadGridView>();

                //Wait for Grid Control Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control.Wait.ForExists(Globals.timeOut);
                //Create a new RadGridView Control
                RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control;

                if (grid.Rows.Count.Equals(0))
                {
                    // Write the log if verification is Fail
                    Manager.Current.Log.WriteLine(LogType.Error, " No Search Results Found by Company :- " + " " + company + ". Verification Failed!!!!");

                }
                else
                {
                    //Check value in Exact Column using Column Header Name Value
                    GridViewHeaderCell header = grid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == headerName);
                    //Check Header value is not Null
                    Assert.IsNotNull(header);
                    //Count all rows and save it in index varibable 
                    int index = header.Index;

                    //Verify Results in each row in grid
                    foreach (GridViewRow row in grid.Rows)
                    {
                        //Compare Each Row Value
                        if (row.Cells[index].TextBlockContent != company)
                        {
                            //Write the log if Verification Fail
                            Manager.Current.Log.WriteLine(LogType.Error, " Grid shows row/rows having company other than: (" + company + "). Verification Failed!!!!");
                        }
                    }

                    //Write the log if Verification Pass
                    Manager.Current.Log.WriteLine(LogType.Information, " Grid shows row/rows searched by company: (" + company + "). Verification Passed!");
                }
            }
            catch (AssertException ex)
            {
                // Write the log if verification is Fail
                Manager.Current.Log.WriteLine(LogType.Error, "Unable to Find the Column Header:" + " " + headerName + " Unable to Verify!" + ex.Message);
            }
        }

        //Method to verify the Secondary Status is exist in Grid for respective checkbox
        public void P2PPaymentPlanVerifySecondaryStatus(string secondryStatus = null, string headerName = null)
        {
            //If Received CheckBox is Checked
            if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Received_CheckBox.IsChecked == true && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Approved_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Removed_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_InWorkflow_CheckBox.IsChecked == false)
            {
                //Write the Logs if Received Checkbox is Checked and Others are UnChecked (Verification is Passed)
                Manager.Current.Log.WriteLine("Received Checkbox is Checked");
            }

            //If Approved CheckBox is Checked
            else if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Received_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Approved_CheckBox.IsChecked == true && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Removed_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_InWorkflow_CheckBox.IsChecked == false)
            {
                //Write the Logs if Approved Checkbox is Checked and Others are UnChecked (Verification is Passed)
                Manager.Current.Log.WriteLine("Approved Checkbox is Checked");
            }

            //If Removed CheckBox is Checked
            else if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Received_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Approved_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Removed_CheckBox.IsChecked == true && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_InWorkflow_CheckBox.IsChecked == false)
            {
                //Write the Logs if Approved Checkbox is Checked and Others are UnChecked (Verification is Passed)
                Manager.Current.Log.WriteLine("Removed Checkbox is Checked");
            }

            //If Workflow CheckBox is Checked
            else if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Received_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Approved_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Removed_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_InWorkflow_CheckBox.IsChecked == true)
            {
                //Write the Logs if Approved Checkbox is Checked and Others are UnChecked (Verification is Passed)
                Manager.Current.Log.WriteLine("Workflow Checkbox is Checked");
            }

             //If No Check Box is Checked  
            else if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Received_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Approved_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Removed_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_InWorkflow_CheckBox.IsChecked == false)
            {
                //throws the Exception if Verification Failed.
                throw new Exception("No Checkbox is Not Checked, Verification Failed!");
            }

            //Throw the Exception in case of Verification Failed.
            else
            {
                //throws the Exception if Verification Failed.
                throw new Exception("Wrong Checkbox is  Checked, Verification Failed!");
            }
        }

        //Method to Verify SubStatus/Status field in PaymentPlanGrid
        public void verifyPaymentPlanGridSubStatus(string secondryStatus, string headerName)
        {

            //Verify the Button is visible in the Application
            if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Button.IsChecked.Equals(true))
            {

                //Wait for Grid Control Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control.Wait.ForExists(Globals.timeOut);

                //Create a new RadGridView Control
                RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control;

                try
                {
                    //Check value in Exact Column using Column Header Name Value
                    GridViewHeaderCell header = grid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == headerName);

                    if (headerName != header.TextBlockContent)
                    {
                        //Capture Screenshot If Verification Fails
                        Manager.Current.Log.CaptureDesktop(Globals.capturedImageLocation + "HeaderColumnName");

                        //Write the log if verification is Failed                               
                        throw new Exception("Expected Header Column Name Does NOT Found in the Grid :" + " " + headerName + "Verification Failed");
                    }

                    //Check Header value is not Null
                    Assert.IsNotNull(header);
                    //Count all rows and save it in index varibable 
                    int index = header.Index;

                    if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control.Rows.Count.Equals(0))
                    {
                        Manager.Current.Log.WriteLine(LogType.Information + " " + "There is No PaymentPlans Found by Status Filter  :" + secondryStatus + ", Total Number of Records is 0");
                    }
                    else
                    {
                        //Verify Results in each row in grid
                        foreach (GridViewRow row in grid.Rows)
                        {
                            //Compare Each Row Value
                            if (row.Cells[index].TextBlockContent != secondryStatus)
                            {
                                //Capture Screenshot If Verification Fails
                                Manager.Current.Log.CaptureDesktop(Globals.capturedImageLocation + "PaymentPlanVerification");

                                //Write the log if Verification Fail
                                throw new Exception("PaymentPlans Found by Other Secondary Status Verification Failed!!!!");
                            }
                        }

                        // Write the log if verification is Pass 
                        Manager.Current.Log.WriteLine(LogType.Information + " " + "PaymentPlans Found by Status Filter : " + secondryStatus);
                    }
                }
                catch (AssertException ex)
                {
                    // Write the log if verification is Fail
                    throw new Exception("Unable to Find the Column Header:" + " " + headerName + "Unable to Verify!" + ex.Message);
                }
            }
        else
            {
                // Write the log if verification is Pass
                Manager.Current.Log.WriteLine(LogType.Information, "Payment Plan Count is Zero, So User cannot Verfiy the Count in a Grid : Received Silo");

            }
        }

        //Method to verify checkbox Status on received silo        
        public void P2PPaymentPlanVerifyCheckboxStatusInReceived()
        {

            //Verify the Button is visible in the Application
            if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Button.IsChecked.Equals(false))
            {
                goto outer;
            }
            //If Draft CheckBox is Checked
            if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Draft_CheckBox.IsChecked == true && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Invalid_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Valid_CheckBox.IsChecked == false)
            {
                //Write the Logs if Draft Checkbox is Checked and Others are UnChecked (Verification is Passed)
                Manager.Current.Log.WriteLine("Draft Checkbox is Checked. All Other Checkbox are UnChecked.");
            }

            //If Invalid CheckBox is Checked
            else if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Draft_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Invalid_CheckBox.IsChecked == true && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Valid_CheckBox.IsChecked == false)
            {
                //Write the Logs if Draft Checkbox is Checked and Others are UnChecked (Verification is Passed)
                Manager.Current.Log.WriteLine("Invalid Checkbox is Checked. All Other Checkbox are UnChecked.");
            }

                //If valid CheckBox is Checked
            else if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Draft_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Invalid_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Valid_CheckBox.IsChecked == true)
            {
                //Write the Logs if Draft Checkbox is Checked and Others are UnChecked (Verification is Passed)
                Manager.Current.Log.WriteLine("Valid Checkbox is Checked. All Other Checkbox are UnChecked.");
            }

            //If No Check Box is Checked  
            else if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Draft_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Invalid_CheckBox.IsChecked == false)
            {
                //throws the Exception if Verification Failed.
                throw new Exception("No Checkbox is Checked, Verification Failed!");
            }

            //Throw the Exception in case of Verification Failed.
            else
            {
                //throws the Exception if wrong checkbox is checked
                throw new Exception("Wrong Checkbox is  Checked, Verification Failed!");
            }
        outer:
            {
                // Write the log if verification is Pass
                Manager.Current.Log.WriteLine(LogType.Information, "Payment Plan Count is Zero, So User cannot Navigate to Received Silo");
            }
        }

        //Method To Verify Bank Account Number On SupplierPicker
        public void P2PPaymentPlansVerifyBankAccountNumberOnSupplierPicker(string supplier, string bankAccountNumber)
        {
            //Wait for Supplier Selection Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierCode_SelectionButton.Wait.ForExists(Globals.timeOut);

            //Set Focus to Supplier Selection Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierCode_SelectionButton.SetFocus();

            //Click on Supplier Selection Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierCode_SelectionButton.User.Click();

            //Wait for PopUpOkButtonEnabled
            P2PNavigation.WaitForPopUpOkButtonEnabled();

            //Click to set a focus on Search Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SupplierSearch_TextBox.User.Click();

            //Enter the Data in Search TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SupplierSearch_TextBox.TypeText(supplier, 50);

            //Click on Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administartion_SuppliersSearchButton.User.Click();

            P2PNavigation.WaitForPopUpOkButtonEnabled();

            //Find the Supplier and Select From the Grid 
            TextBlock selectsupplier = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(supplier).As<TextBlock>();

            //Select the Supplier by the User
            selectsupplier.User.Click();

            try
            {
                //Put the ListBox into a variable.
                FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Create_SupplierPicker_AccountGridView;

                //Check whether any TextBlock in the ListBox contains the specified string.
                bool found = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(bankAccountNumber));

                //Log the results.
                if (found == true)
                {
                    // Write the Log If Uploaded file found
                    Manager.Current.Log.WriteLine("Bank Account number " + ":" + " " + bankAccountNumber + " " + "Exists in Suplier picker Pop Window against Supplier: " + supplier);
                }
                else
                {
                    // Write the Log If Uploaded file Not found
                    throw new Exception("Bank Account number does Not Exist in Suplier picker Pop Window against" + supplier + " Verification Failed !!!");
                }
            }
            finally
            {
                //Click on OK Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            }
        }

        //Method to verify HistoryPanelPopUp
        public void P2PPaymentPlansHistoryPanelPopUpVerification(string[] comment, string[] planNumber)
        {

            //declare index variables for loops, statusflag : checks where verifications are failing in a loop for different iterations.
            int i = 0;
            int indexPlan = 0;
            bool statusflag = true;

            //Declare Framework element for history listbox
            FrameworkElement e;

            //Declare an array of textblocks
            var plan = new TextBlock[2];

            //get all Plan numbers as text blocks
            foreach (string pNum in planNumber)
            {
                //store plannumber text block
                plan[i] = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(pNum).As<TextBlock>();

                //increment index
                i++;
            }

            //Wait for History Link Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryButton.Wait.ForExists(Globals.timeOut);

            //Click on History Link Under Payment Plan Details Panel 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryButton.User.Click();

            //Wait for History Pop-up to appear
            //System.Threading.Thread.Sleep(Globals.timeOut);
            P2PNavigation.CallBusyIndicator();

            //Verify the Comments in History Pop-Up
            foreach (string strComment in comment)
            {
                //Click On next plan  number
                plan[indexPlan].User.Click();

                //Wait 
                System.Threading.Thread.Sleep(Globals.timeOut);
                
                //Wait for History and Comments ListBox Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsListBox.Wait.ForExists(Globals.timeOut);

                bool foundRichTextBlock = false;
                //Get RichText Box
                ArtOfTest.WebAii.Silverlight.UI.RichTextBox local = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsListBox.Find.ByName<ArtOfTest.WebAii.Silverlight.UI.RichTextBox>("historyContent");
                //Create The Xml Document
                XmlDocument doc = new System.Xml.XmlDocument();
                //Save in string
                string rtbContents = (string)local.GetProperty(new AutomationProperty("Xaml", typeof(string)));
                //Load string in the Document
                doc.LoadXml(rtbContents);
                //Check whether any TextBlock contains the specified string.
                foundRichTextBlock = doc.InnerText.Contains(strComment);
                                
                //Take Frameowrk Element for  History and Comments Pop-Up
                e = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsListBox;

                //initialize boolean 
                bool strFound = false;

                //Check the TextBlock contains the Comments.
                if (e != null)
                {
                    //read textblock of   History and Comments Pop-Up
                    strFound = (e.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(strComment.ToString()))) && (strComment.ToUpper() != "There are no entries".ToUpper());

                    //Variable listTextBlocks gives all the textblocks in the pop up
                    //var listTextBlocks = e.Find.AllByType<TextBlock>().Select(y => y.Text).ToList();

                    //Check using If Condition that Comments Exists?
                    if (strFound == true || foundRichTextBlock == true)
                    {
                        //Case 1: Log the Results if correct Comment Exists
                        Manager.Current.Log.WriteLine(LogType.Information, "Correct Comments: ( " + " " + strComment + " " + " )  Exists in History Pop-up, against Plan Name=  " + " " + planNumber[indexPlan]);
                    }
                    else
                    {
                        //Case 2: :Log the Results when plan has no Comments and History Listbox has just 1 textblock
                        if (e.Find.AllByType<TextBlock>().Count == 1)
                        {
                            //Verify comment in test data file
                            if (strComment.ToUpper() == "There are no entries".ToUpper())
                                //Log the Results when plan has no Comments
                                Manager.Current.Log.WriteLine(LogType.Information, "No Comments Exist in History Pop-up, against Plan Name=  " + " " + planNumber[indexPlan]);
                            else
                            {
                                //Case 3 :set the bool flag if Comments are showing incorrect 
                                //Log the Results when plan has incorrect Comments
                                Manager.Current.Log.WriteLine(LogType.Information, "************* Fail : Incorrect Comment shows in History Pop-up, against Plan Name=  " + " " + planNumber[indexPlan] + ". Searching for comment text : " + strComment);
                                statusflag = false;
                            }
                        }
                        else
                        {
                            //Case 3 :set the bool flag if Comments are showing incorrect 
                            //Log the Results when plan has incorrect Comments
                            Manager.Current.Log.WriteLine(LogType.Information, "************* Fail : Incorrect Comment shows in History Pop-up, against Plan Name=  " + " " + planNumber[indexPlan] + ". Searching for comment text : " + strComment);
                            statusflag = false;
                        }
                    }
                }
                else
                {
                    //Log the Error if History Listbox doesnt exist 
                    Manager.Current.Log.WriteLine(LogType.Error, "History Pop-up not visible, Verification Failed!, against Plan Name=  " + " " + planNumber[indexPlan]);
                }

                //increment index for array's planNumber[], plan[]
                indexPlan++;

                //Set focus on Plan grid
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control.SetFocus();
            }

            //Close the History Pop-Up
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.User.Click();

            if (statusflag == false)
            {
                //Log the Error if  History was showing incorrect for any plan
                Manager.Current.Log.WriteLine(LogType.Error, " History Data Pop Up Details incorrect. Verify for Plan Number from log's above.");
            }
        }

        //Method to verify HeaderDetailsPanelPopUp
        public void P2PPaymentPlansHeaderDetailsPanelPopUpVerification(string[] planNumber)
        {
            //declare index variables for loops, statusflag : checks where verifications are failing in a loop for different iterations.
            int indexPlan = 0;
            bool statusflag = true;

            //Declare an array of textblocks
            var plan = new TextBlock[2];

            //Wait for Header Data Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HeaderDataButton.Wait.ForExists(Globals.timeOut);

            //Click on Header Data Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HeaderDataButton.User.Click();

            //get all Plan numbers as text blocks and verify Header Data
            foreach (string pNum in planNumber)
            {
                //store plannumber text block
                plan[indexPlan] = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(pNum).As<TextBlock>();

                //Set focus on Plan grid
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control.SetFocus();

                //Click On plan 1 
                plan[indexPlan].User.Click();

                //Wait for plan number to get selected
                System.Threading.Thread.Sleep(Globals.timeOut);

                //Wait for PlanName_TextBox to load in dom (old: P2P_PaymentPlans_HeaderDataField_PlanName_TextBox)
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PlanReference_HeaderData_TextBox.Wait.ForExists(Globals.timeOut);

                //verify Reference TextBox contains correct plan reference 
                if (pNum == SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PlanReference_HeaderData_TextBox.Text.ToString())
                {
                    //Log the Results if correct Header Data Exists
                    Manager.Current.Log.WriteLine(LogType.Information, "Header Data Pop Up Details correct for Plan Name : " + planNumber[indexPlan]);
                }
                else
                {
                    //Log the Results if incorrect Header Data Exists
                    Manager.Current.Log.WriteLine(LogType.Information, "************* Fail : Header Data Pop Up Details incorrect for Plan Name : " + planNumber[indexPlan]);

                    //Set flag if Header data mis matches
                    statusflag = false;
                }

                //increment index
                indexPlan++;
            }

            //Close the  Header Data Pop-up Window
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.User.Click();

            if (statusflag == false)
            {
                //Log the Error if  Header Data was showing incorrect for any plan
                Manager.Current.Log.WriteLine(LogType.Error, " Header Data Pop Up Details incorrect. Verify for Plan Number from log's above.");
            }
        }

        //Method to verify AttachmentsPanelPopUp
        public void P2PPaymentPlansAttachmentsPanelPopUpVerification(string[] fileName, string[] planNumber)
        {
            //declare index variables for loops, statusflag : checks where verifications are failing in a loop for different iterations.
            int indexPlan = 0;
            bool statusflag = true;

            //Declare Framework element for attachment listbox
            FrameworkElement e;

            //Declare an array of textblocks
            TextBlock[] plan = new TextBlock[2];

            //get all Plan numbers as text blocks
            foreach (string pNum in planNumber)
            {
                //store plannumber text block
                plan[indexPlan] = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(pNum).As<TextBlock>();

                //increment index
                indexPlan++;
            }

            //reset index 
            indexPlan = 0;

            //Wait for attachment Link Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_AttachmentButton.Wait.ForExists(Globals.timeOut);

            //Click on attachment Link Under Payment Plan Details Panel 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_AttachmentButton.User.Click();

            //Wait for attachment Pop-up to appear
            //System.Threading.Thread.Sleep(Globals.timeOut);
            P2PNavigation.CallBusyIndicator();

            //Verify the attachments in Pop-Up
            foreach (string fName in fileName)
            {
                //Click On next plan  number
                plan[indexPlan].User.Click();

                //Wait for plan number to get selected
                System.Threading.Thread.Sleep(Globals.timeOut);

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
                        Manager.Current.Log.WriteLine(LogType.Information, "Correct Attachment File:( " + " " + fName + " " + " ) Exists in Attachments Pop-up, against Plan Name=  " + " " + planNumber[indexPlan]);
                    }
                    else
                    {
                        //Case 2: :Log the Results when plan has no Attachment and Attachment Listbox has just 1 textblock
                        if (e.Find.AllByType<TextBlock>().Count == 1)
                        {
                            //Verify Attachment in test data file
                            if (fName.ToUpper() == "No attachments".ToUpper())
                                //Log the Results when plan has no Attachment
                                Manager.Current.Log.WriteLine(LogType.Information, "No Attachment Exist in Attachment Pop-up, against Plan Name=  " + " " + planNumber[indexPlan]);
                            else
                            {
                                //Case 3 :set the bool flag if Attachment are showing incorrect 
                                //Log the Results when plan has incorrect Attachments
                                Manager.Current.Log.WriteLine(LogType.Information, "************* Fail : Incorrect Attachment shows in Attachment Pop-up, against Plan Name=  " + " " + planNumber[indexPlan] + ". Searching for Attachment  : " + fName);
                                statusflag = false;
                            }
                        }
                        else
                        {
                            //Case 3 :set the bool flag if Attachment are showing incorrect 
                            //Log the Results when plan has incorrect Attachment
                            Manager.Current.Log.WriteLine(LogType.Information, "************* Fail : Incorrect Attachment shows in Attachment Pop-up, against Plan Name=  " + " " + planNumber[indexPlan] + ". Searching for Attachment : " + fName);
                            statusflag = false;
                        }
                    }
                }
                else
                {
                    //Log the Results if attachments Listbox doesnt exist 
                    Manager.Current.Log.WriteLine(LogType.Error, "Attachments Pop-up not visible, Verification Failed!, against Plan Name=  " + " " + planNumber[indexPlan]);
                }

                //increment index for array's planNumber[], fileName[]
                indexPlan++;

                //Set focus on Plan grid
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control.SetFocus();
            }

            //Close the Attachments Pop-Up
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.User.Click();

            if (statusflag == false)
            {
                //Log the Error if Attachment name was showing incorrect for any plan
                Manager.Current.Log.WriteLine(LogType.Error, "Attachments Pop Up Details incorrect. Verify for Plan Number from log's above.");
            }
        }

        //Method to verify PaymentSchedulePanelPopUp
        public void P2PPaymentPlansPaymentSchedulePanelPopUpVerification(string[] scheduleData, string[] planNumber, string columnName)
        {
            //declare index variables for loops, statusflag : checks where verifications are failing in a loop for different iterations.
            int i = 0;
            int indexPlan = 0;
            bool statusflag = true;

            //Declare an array of textblocks
            var plan = new TextBlock[4];

            //get all Plan numbers as text blocks
            foreach (string pNum in planNumber)
            {
                //store plannumber text block
                plan[i] = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(pNum).As<TextBlock>();

                //increment index
                i++;
            }

            //Wait for PaymentSchedule Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PaymentSchedule_Button.Wait.ForExists(Globals.timeOut);

            //Click on  PaymentSchedule Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PaymentSchedule_Button.User.Click();

            //verify PaymentSchedule Data
            foreach (string sData in scheduleData)
            {
                //Click On next plan  number
                plan[indexPlan].User.Click();

                //Wait for plan number to get selected
                System.Threading.Thread.Sleep(Globals.timeOut);

                //Wait for Grid Control Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PaymentSchedule_Grid_PopUp.Wait.ForExists(Globals.timeOut);

                //Set focus on Pop up
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PaymentSchedule_Grid_PopUp.SetFocus();

                //Create a new RadGridView Control
                RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PaymentSchedule_Grid_PopUp;

                try
                {
                    ////Check value in Exact Column using Column Header Name Value
                    //GridViewHeaderCell header = grid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == columnName);

                    //Check Header value is not Null
                    //Assert.IsNotNull(header);

                    //Count all rows and save it in index varibable    
                    //int index = header.Index;

                    //Check if grid has no rows.
                    if (grid.Rows.Count.Equals(0))
                    {
                        //when no row found
                        Manager.Current.Log.WriteLine(LogType.Error, " ************* Fail : No rows found in Payment Schedule Pop Up. Total Number of Records is 0. Verification Failed !!!");
                    }

                    //Verify Results in each row in grid
                    foreach (GridViewRow row in grid.Rows)
                    {
                        //Manager.Current.Log.WriteLine(LogType.Information, row.Cells[0].TextBlockContent);

                        //Manager.Current.Log.WriteLine(LogType.Information, row.Cells[1].TextBlockContent);

                        //Manager.Current.Log.WriteLine(LogType.Information, row.Cells[2].TextBlockContent);

                        //Compare Each Row Value
                        if (row.Cells[1].TextBlockContent != sData)
                        {
                            //Write the log if Verification Fail
                            Manager.Current.Log.WriteLine(LogType.Information, "************* Fail : Payment Schedule Pop Up: Data found incorrect. Verification Failed for Plan Name :  " + planNumber[indexPlan]);

                            //set flag if data mis matches
                            statusflag = false;
                        }
                        else
                            // Write the log if verification is Pass 
                            Manager.Current.Log.WriteLine(LogType.Information, "Payment Schedule Pop Up: Data(" + sData + ") found in grid for field :  " + columnName + "  against Plan Name :  " + planNumber[indexPlan]);
                    }
                }
                catch (Exception ex)
                {
                    // Write the log if verification is Fail
                    Manager.Current.Log.WriteLine(LogType.Error, " Unable to Find the Column Header Unable to Verify!" + ex.Message);
                }
                //increment index
                indexPlan++;

                //Set focus on Plan grid
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control.SetFocus();
            }

            //Close the  Payment Schedule Pop Up Details Window
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.User.Click();

            if (statusflag == false)
            {
                //Log the Error if Payment Schedule Pop Up Data was showing incorrect for any plan
                Manager.Current.Log.WriteLine(LogType.Error, " Payment Schedule Pop Up Details incorrect. Verify for Plan Number from log's above.");
            }
        }

        //Method To Verify Check Box and Sub- status on Approved
        public void P2PPaymentPlanVerifyCheckboxStatusFilterInApproved()
        {
            //If Active CheckBox is Checked
            if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Active_CheckBox.IsChecked == true && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Deactivated_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Expired_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Waiting_For_Activation_CheckBox.IsChecked == false)
            {
                //Write the Logs if Active Checkbox is Checked and Others are UnChecked (Verification is Passed)
                Manager.Current.Log.WriteLine("Active Checkbox is Checked. All Other Checkbox are UnChecked.");
            }

            //If Deactivated CheckBox is Checked
            else if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Active_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Deactivated_CheckBox.IsChecked == true && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Expired_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Waiting_For_Activation_CheckBox.IsChecked == false)
            {
                //Write the Logs if Deactivated Checkbox is Checked and Others are UnChecked (Verification is Passed)
                Manager.Current.Log.WriteLine("Deactivated Checkbox is Checked. All Other Checkbox are UnChecked.");
            }

            else if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Active_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Deactivated_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Expired_CheckBox.IsChecked == true && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Waiting_For_Activation_CheckBox.IsChecked == false)
            {
                //Write the Logs if Expired Checkbox is Checked and Others are UnChecked (Verification is Passed)
                Manager.Current.Log.WriteLine("Expired Checkbox is Checked. All Other Checkbox are UnChecked.");
            }

            else if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Active_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Deactivated_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Expired_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Waiting_For_Activation_CheckBox.IsChecked == true)
            {
                //Write the Logs if Waiting_For_Activation Checkbox is Checked and Others are UnChecked (Verification is Passed)
                Manager.Current.Log.WriteLine("Expired Checkbox is Checked. All Other Checkbox are UnChecked.");
            }

             //If No Check Box is Checked  
            else if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Active_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Deactivated_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Expired_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Approved_Waiting_For_Activation_CheckBox.IsChecked == true)
            {
                //throws the Exception if Verification Failed.
                throw new Exception("No Checkbox is Checked, Verification Failed!");
            }

            //Throw the Exception in case of Verification Failed.
            else
            {
                //throws the Exception if wrong checkbox is checked
                throw new Exception("Wrong Checkbox is  Checked, Verification Failed!");
            }
        }

        //Method to Verify the Selected Payment Plans in Details Panel Pop Up(Header Data, Payment Schedule, History,Attachment)
        public void P2PPaymentPlanInvoiceDetailsPanelPopUpVerifications(string attachment, string paymentPlan, string paymentSchedule, string history)
        {
            P2PInvoiceAdministrationVerification verifyObj = new P2PInvoiceAdministrationVerification();

            //Attachment Pop up Verification
            verifyObj.P2PAttachmentsMainPageVerification(attachment, paymentPlan, "Add", "Attachment Pop-up");
            
            
            //Wait for Attachment Button visible in UI
            //System.Threading.Thread.Sleep(Globals.timeOut);

            ////Wait for Attachment Button Exists in DOM
            //SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_AttachmentButton.Wait.ForExists(Globals.timeOut);

            ////Click on Attachment Button to Attachment
            //SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_AttachmentButton.User.Click();

            //try
            //{
            //    //Wait for Attachment Pop-up to appear
            //    //System.Threading.Thread.Sleep(Globals.timeOut);
            //    P2PNavigation.CallBusyIndicator();

            //    //Create an  object for TextBlock
            //    TextBlock textBlockCell = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(attachment).As<TextBlock>();

            //    //Check using If Condition that Attachment text Exists?
            //    if (textBlockCell.TextLiteralContent == attachment)
            //    {
            //        //Log the Results if Attachment text Exists in Attachment Pop-up
            //        Manager.Current.Log.WriteLine("Attachment Comment:" + " " + attachment + " " + "exist in Attachment Pop-up, against:" + " " + paymentPlan);
            //    }
            //    else
            //    {
            //        //Throw the Exception if Verification Fails
            //        throw new Exception("Attachment Comment:" + " " + attachment + " " + "does NOT exist in Attachment Pop-up, Verification Failed!, against:" + " " + paymentPlan);
            //    }
            //}
            //finally
            //{
            //    //Close the Pop-Up Window
            //    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.User.Click();
            //}

            //Wait for HeaderData Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HeaderDataButton.Wait.ForExists(Globals.timeOut);

            //Click on HeaderData Button to Verify Payment Plan
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HeaderDataButton.User.Click();

            try
            {
                //Wait for HeaderData Pop-up to appear
                //System.Threading.Thread.Sleep(Globals.timeOut);
                P2PNavigation.CallBusyIndicator();

                //Create an  object for TextBlock
                TextBlock textBlockCell = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(paymentPlan).As<TextBlock>();

                //Check using If Condition that paymentPlan Exists?
                if (textBlockCell.TextLiteralContent == paymentPlan)
                {
                    //Log the Results if paymentPlan Code Exists in HeaderData Pop-up
                    Manager.Current.Log.WriteLine("Payment Plan:" + " " + paymentPlan + " " + "exist in HeaderData Pop-up, against:" + " " + paymentPlan);
                }
                else
                {
                    //Throw the Exception if Verification Fails
                    throw new Exception("Payment Plan:" + " " + paymentPlan + " " + "does NOT exist in HeaderData Pop-up, Verification Failed!, against:" + " " + paymentPlan);
                }
            }
            finally
            {
                //Close the Pop-Up Window
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.User.Click();
            }

            //Wait for PaymentSchedule Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PaymentSchedule_Button.Wait.ForExists(Globals.timeOut);

            //Click on PaymentSchedule  Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PaymentSchedule_Button.User.Click();
            {
                try
                {
                    //Wait for PaymentSchedule Pop-up to appear
                    //System.Threading.Thread.Sleep(Globals.timeOut);
                    P2PNavigation.CallBusyIndicator();

                    //Create an  object for TextBlock
                    TextBlock textBlockCell = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(paymentSchedule).As<TextBlock>();

                    //Check using If Condition that PaymentSchedule  Exists?
                    if (textBlockCell.TextLiteralContent == paymentSchedule)
                    {
                        //Log the Results if Payment Data Exists in PaymentSchedule Pop-up
                        Manager.Current.Log.WriteLine("Payment Schedule:" + " " + paymentSchedule + " " + "exist in PaymentSchedule Pop-up, against:" + " " + paymentPlan);
                    }
                    else
                    {
                        //Throw the Exception if Verification Fails
                        throw new Exception("Payment Schedule:" + " " + paymentSchedule + " " + "Does NOT exist in PaymentSchedule Pop-up, Verification Failed!, against:" + " " + paymentPlan);
                    }
                }
                finally
                {
                    //Close the Pop-Up Window
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.User.Click();
                }
            }


            //History Pop-up Verification
            verifyObj.P2PMainPageHistoryPopUpVerification(history, paymentPlan, "History Pop-up");

            //try
            //{
            //    //Wait for History Pop-up to appear
            //    //System.Threading.Thread.Sleep(Globals.timeOut);
            //    P2PNavigation.CallBusyIndicator();

            //    //Wait for History Button Exists in DOM
            //    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryButton.Wait.ForExists(Globals.timeOut);
            //    //Click  on History button
            //    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryButton.User.Click();

            //    //Wait for History Pop-up to appear
            //    System.Threading.Thread.Sleep(Globals.timeOut);
            //    //Create a object for Framework Element 
            //    FrameworkElement e = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsListBox;

            //    //Check the TextBlock contains the Comments.
            //    bool found = e.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(history));

            //    //Check using If Condition that Comments Exists?
            //    if (found == true)
            //    {
            //        //Log the Results if Comments Exists
            //        Manager.Current.Log.WriteLine("Comments:" + " " + history + " " + "Exists in History Pop-up, against:" + " " + paymentPlan);
            //    }
            //    else
            //    {
            //        //Throw the Exception if Verification Fails
            //        throw new Exception("Comments:" + " " + history + " " + "does NOT Exists in History Pop-up, Verification Failed!, against:" + " " + paymentPlan);
            //    }
            //}
            //finally
            //{
            //    //Close the History Pop-Up
            //    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.User.Click();
            //}
        }

        //Method to verify Navigation Text
        public void P2PPaymentPlanVerifyNavigationText(string navigationTextMessage)
        {
            //Refresh Dom Tree
            Manager.Current.ActiveBrowser.RefreshDomTree();

            //Wait for Page Load
            //System.Threading.Thread.Sleep(Globals.pause);
            P2PNavigation.CallBusyIndicator();

            //Verify the Navigation Text(Step 2, Verify Message: Invoice 2/5:Ready For Transfer)            
            if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Details_NavigationControl.TextBlockContent == navigationTextMessage)
            {
                // Write the log if verification is Pass
                Manager.Current.Log.WriteLine(LogType.Information, "Navigation Successfully:" + " " + navigationTextMessage);
            }
            else
            {
                // Write the log if verification is Fail
                Manager.Current.Log.WriteLine(LogType.Error, "Navigation Failed:" + " " + navigationTextMessage);
            }
        }

        //Method to Verify Attachment button disabled for Active Payment Plan
        public void P2PPaymentVerifyAttachmentInActivePlan()
        {
            // Wait for Attachment Tab Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_AttachmentsTabItem.Wait.ForExists(Globals.timeOut);

            //Click on Attachment Tab Items in Details Page
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_AttachmentsTabItem.User.Click();

            //Wait for Page Load
            //System.Threading.Thread.Sleep(Globals.pause);
            P2PNavigation.CallBusyIndicator();

            //Wait for Add Attachment Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddButton.Wait.ForExists(Globals.timeOut);

            //Verify  Attachment Button is disabled
            if (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddButton.IsEnabled.Equals(false))
            {
                // Write the log if verification is Pass
                Manager.Current.Log.WriteLine(LogType.Information, "Verification Passed : Add Attachment button shows disabled for Active Payment Plans.");
            }
            else
            {
                // Write the log if verification is Fail
                Manager.Current.Log.WriteLine(LogType.Error, "Verification Failed : Add Attachment button shows enabled for Active Payment Plans.");
            }
        }

        //Method to verify PaymentSchedulePanel After Open Selected Payment Plan
        public void P2PPaymentPalnsVerifyPaymentScheduleRow(double expectedSum)
        {
            //Wait for Action to be Complete
            //System.Threading.Thread.Sleep(Globals.pause);

            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Create_PaymentSchedule_Grid.Wait.ForExists(Globals.timeOut);

            //Find the Payment Schedule GridView                 
            RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Create_PaymentSchedule_Grid;

            //Find the Coding Row GridView                 
            FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Create_PaymentSchedule_Grid;

            try
            {

                //If condition is True execute if block
                if (fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(expectedSum.ToString(CultureInfo.CurrentCulture.NumberFormat))) == true)
                {
                    //Write the Result in log file when Verification is Pass
                    Manager.Current.Log.WriteLine(LogType.Information, "Expected & Actual Value For Expected Sum Are Same i.e. :" + expectedSum + " " + "Verification Passed");

                }

                else
                {
                    //Write the Result in log file when Verification is Fail
                    Manager.Current.Log.WriteLine(LogType.Error, "Expected & Actual Value For Expected Sum is Not Same  i.e. :" + expectedSum + " " + "Verification Failed !!!");                                   

                }
            }

             //To Verify the Coding Row to that Particular Payment Schedule Row
            finally
            {
                //Create an object for GridViewRow class
                GridViewRow gvr = rgv.Rows[rgv.Rows.Count - 1];

                //Entering Data in Payment Schedule Row 
                gvr.User.Click();
            }

        }

        //Method to verify Coding Row After Open Selected Payment Plan
        public void P2PPaymentPalnsVerifyCodingRow(string accountCode, string costCenterCode, double percentage, string optionalCostCenter = null, string codingRowTab = null)
        {
            if (codingRowTab != null)
            {
                //Wait for TabItem Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_InvocieDetails_CodingRowTab.Wait.ForExists(Globals.timeOut);

                //Click on TabItem
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_InvocieDetails_CodingRowTab.User.Click();
            }
            //wait for Coding Rows Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Wait.ForExists(Globals.navigationTimeOut);

            //Find the Coding Row GridView                 
            FrameworkElement rgv = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl;

            //optionalCostCenter added for scripts which dont have cost center in coding rows.So Verification done without cost center in else block. For scripts which have all 3 data(acc code, cost center code, percentage) execute IF block.
            if (optionalCostCenter == null)
            {
                //If condition is True execute if block          
                if (rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(accountCode)) == true && rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(costCenterCode)) == true && rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(percentage.ToString(CultureInfo.CurrentCulture.NumberFormat))) == true)
                {
                    //Write the Result in log file when Verification is Pass
                    Manager.Current.Log.WriteLine(LogType.Information, " Expected and Actual Values For AccountCode,Cost Center Code and Percentage Are Same i.e. : " + accountCode + ", " + costCenterCode + " & " + percentage.ToString(CultureInfo.CurrentCulture.NumberFormat) + ". " + "Verification Passed!");
                }
                else
                {
                    //Write the Result in log file when Verification is Fail
                    Manager.Current.Log.WriteLine(LogType.Error, " Expected Values & Actual Values For Account Code, Cost Center Code and Percentage Are Not Same i.e. : " + accountCode + " " + costCenterCode + " & " + percentage.ToString(CultureInfo.CurrentCulture.NumberFormat) + ". " + "Verification Failed!");
                    throw new Exception(" Expected Values & Actual Values For Account Code, Cost Center Code and Percentage Are Not Same i.e. : " + accountCode + " " + costCenterCode + " & " + percentage.ToString(CultureInfo.CurrentCulture.NumberFormat) + ". " + "Verification Failed !!!");
                }
            }
            else
            {
                //If condition is True execute if block          
                if (rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(accountCode)) == true && rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(percentage.ToString(CultureInfo.CurrentCulture.NumberFormat))) == true)
                {
                    //Write the Result in log file when Verification is Pass
                    Manager.Current.Log.WriteLine(LogType.Information, " Expected and Actual Values For AccountCode,Cost Center Code and Percentage Are Same i.e. :" + accountCode + ", " + costCenterCode + " & " + percentage.ToString(CultureInfo.CurrentCulture.NumberFormat) + ". " + "Verification Passed!");
                }
                else
                {
                    //Write the Result in log file when Verification is Fail
                    Manager.Current.Log.WriteLine(LogType.Information, " Expected Values & Actual Values For Account Code, Cost Center Code and Percentage Are Not Same i.e. : " + accountCode + " " + costCenterCode + " & " + percentage.ToString(CultureInfo.CurrentCulture.NumberFormat) + ". " + "Verification Failed!");
                    throw new Exception(" Expected Values & Actual Values For Account Code, Cost Center Code and Percentage Are Not Same i.e. : " + accountCode + " " + costCenterCode + " & " + percentage.ToString(CultureInfo.CurrentCulture.NumberFormat) + ". " + "Verification Failed !!!");
                }
            }
        }        

        //Method to verify the Overview/ Message Bar Count 
        public void P2PPaymentPlansVerifyOverviewGraphCounts(string paymentPlanCounts)
        {
            //Get the Message Bar Counts
            string messageBarCounts = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new XamlFindExpression("AutomationId=EraseUserMessageButton")).TextLiteralContent;
            //Get the Footer Count
            string footerCount = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new XamlFindExpression("AutomationId=TotalCountTextBlock")).TextLiteralContent;

            try
            {
                //Check the Condition if Both Matches
                if (messageBarCounts.Contains(paymentPlanCounts) == footerCount.Contains(paymentPlanCounts))
                {

                    //Logs if Verification Pass
                    Manager.Current.Log.WriteLine("Payment Plan Message Bar & Footer Actual Count : " + messageBarCounts + "  And Expected Count :" + footerCount + " ,Verification Pass");
                }
                else
                {
                    //Logs if Verification Failed
                    throw new Exception("Payment Plan Message Bar & Footer Actual Count :" + " " + messageBarCounts + " " + "does NOT Match with the Expected Result: " + footerCount + ", Verification Failed");

                }
            }
            finally
            {
                //Calling handle busy indicator
                P2PNavigation.CallBusyIndicator();
            }
        }

        //Method to Verify Status Filters for Graph
        public void P2PPaymentPlansVerifyStatusFiltersForGraph(Boolean paymentPlansSummaries)
        {
            //If condition is true the execute if block
            if (paymentPlansSummaries == false)
            {
                //Checked Received, In Workflow, Approved  CheckBox is Checked
                if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Received_CheckBox.IsChecked == true && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Approved_CheckBox.IsChecked == true && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Removed_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_InWorkflow_CheckBox.IsChecked == true)
                {
                    //Write the Logs if Received Checkbox is Checked and Others are UnChecked (Verification is Passed)
                    Manager.Current.Log.WriteLine("All Required Checkboxes are Checked");
                }
                else
                {
                    //throws the Exception if Verification Failed.
                    throw new Exception("All Required Checkboxes are  Un-Checked, Verification Failed!");
                }

            }
            else
            {
                if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Received_CheckBox.IsChecked == true && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Approved_CheckBox.IsChecked == true && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Removed_CheckBox.IsChecked == true && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_InWorkflow_CheckBox.IsChecked == true)
                {
                    //Write the Logs if Received Checkbox is Checked and Others are UnChecked (Verification is Passed)
                    Manager.Current.Log.WriteLine("All Checkboxes are Checked");
                }

                else
                {
                    //throws the Exception if Verification Failed.
                    throw new Exception("Wrong Checkbox are Checked, Verification Failed!");
                }
            }
        }

        // Method to Verify Edited Payment Plan
        public void P2PPaymentPlansEditPaymentPlanVerification(string supplierName, string currencyCode, Boolean button)
        {
            //Wait for SupplierNameHeaderData_TextBox Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_SupplierNameHeaderData_TextBox.Wait.ForExists(Globals.timeOut);

            //Get the SupplierNameHeaderData_TextBox value and Store into a String "strSupplierName"
            string strSupplierName = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_SupplierNameHeaderData_TextBox.Text.ToString();

            //If condition is True execute if block
            if (strSupplierName == supplierName)
            {
                //Write the Result in log file when Verification is Pass
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Supplier Name:" + " " + supplierName + " " + "&" + " " + "Actual Supplier Name" + " " + strSupplierName + " " + "Verification Pass");
            }
            else
            {
                //Write the Result in log file when Verification is Fail
                Manager.Current.Log.WriteLine(LogType.Error,"Expected Supplier Name:" + " " + supplierName + " " + "&" + " " + "Actual Supplier Name" + " " + strSupplierName + " " + "Does not Edited ,Verification Failed:");
            }


            if (button == true)
            {
                //Wait for Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Recieved_HeaderData_CurrencyCode_TextBox.Wait.ForExists(Globals.timeOut);
                //Get the Run time Value from the Currency Text Box
                string strCurrencyCode = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Recieved_HeaderData_CurrencyCode_TextBox.Text.ToString();

                //If condition is True execute if block
                if (strCurrencyCode == currencyCode)
                {
                    //Write the Result in log file when Verification is Pass
                    Manager.Current.Log.WriteLine(LogType.Information, "Expected Currency Code:" + " " + currencyCode + " " + "&" + " " + "Actual Currency Code" + " " + strCurrencyCode + " " + "Verification Pass");
                }
                else
                {
                    //Write the Result in log file when Verification is Fail
                    Manager.Current.Log.WriteLine(LogType.Error, "Expected Currency Code:" + " " + currencyCode + " " + "&" + " " + "Actual Currency Code" + " " + strCurrencyCode + " " + "Does not Edited ,Verification Failed:");
                }

            }

            //else
            //{
            //    //Wait for Exists in DOM
            //    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Recieved_HeaderData_CurrencyCode_TextBox.Wait.ForExists(Globals.timeOut);
            //    //Get the Run time Value from the Currency Text Box
            //    string strCurrencyCode = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Recieved_HeaderData_CurrencyCode_TextBox.Text.ToString();

            //    //If condition is True execute if block
            //    if (strCurrencyCode == currencyCode)
            //    {
            //        //Write the Result in log file when Verification is Pass
            //        Manager.Current.Log.WriteLine(LogType.Information, "Expected Currency Code:" + " " + currencyCode + " " + "&" + " " + "Actual Currency Code" + " " + strCurrencyCode + " " + "Verification Pass");
            //    }
            //    else
            //    {
            //        //Write the Result in log file when Verification is Fail
            //        Manager.Current.Log.WriteLine(LogType.Error, "Expected Currency Code:" + " " + currencyCode + " " + "&" + " " + "Actual Currency Code" + " " + strCurrencyCode + " " + "Does not Edited ,Verification Failed:");
            //    }
            //}

        }

        //Method to verify checkbox Status on received silo        
        public void P2PPaymentPlanVerifyCheckboxStatusInWorkflow()
        {

            //Verify the Button is visible in the Application
            if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Button.IsChecked.Equals(true))
            {

                //If Draft CheckBox is Checked
                if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Draft_CheckBox.IsChecked == true && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Invalid_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Valid_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Requested_CheckBox.IsChecked == false)
                {
                    //Write the Logs if Draft Checkbox is Checked and Others are UnChecked (Verification is Passed)
                    Manager.Current.Log.WriteLine("Draft Checkbox is Checked. All Other Checkbox are UnChecked.");
                }

                //If Invalid CheckBox is Checked
                else if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Draft_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Invalid_CheckBox.IsChecked == true && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Valid_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Requested_CheckBox.IsChecked == false)
                {
                    //Write the Logs if Draft Checkbox is Checked and Others are UnChecked (Verification is Passed)
                    Manager.Current.Log.WriteLine("Invalid Checkbox is Checked. All Other Checkbox are UnChecked.");
                }

                    //If valid CheckBox is Checked
                else if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Draft_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Invalid_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Valid_CheckBox.IsChecked == true && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Requested_CheckBox.IsChecked == false)
                {
                    //Write the Logs if Draft Checkbox is Checked and Others are UnChecked (Verification is Passed)
                    Manager.Current.Log.WriteLine("Valid Checkbox is Checked. All Other Checkbox are UnChecked.");
                }

                else if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Draft_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Invalid_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Valid_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Requested_CheckBox.IsChecked == true)
                {
                    //Write the Logs if Draft Checkbox is Checked and Others are UnChecked (Verification is Passed)
                    Manager.Current.Log.WriteLine("Valid Checkbox is Checked. All Other Checkbox are UnChecked.");
                }
                //If No Check Box is Checked  
                else if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Draft_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_Invalid_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Requested_CheckBox.IsChecked == false)
                {
                    //throws the Exception if Verification Failed.
                    throw new Exception("No Checkbox is Checked, Verification Failed!");
                }

                //Throw the Exception in case of Verification Failed.
                else
                {
                    //throws the Exception if wrong checkbox is checked
                    throw new Exception("Wrong Checkbox is  Checked, Verification Failed!");
                }
            }
        else
            {
                // Write the log if verification is Pass
                Manager.Current.Log.WriteLine(LogType.Information, "Payment Plan Count is Zero, So User cannot Navigate to Received Silo");
            }
        }

        // Method to verify records if Exceptions Check box is checked 
        public void P2PPaymentPlanWorkFlow_VerifySearchbyStatusforExceptions()
        {

            try
            {
                //Wait for Exceptions check box is Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_Exceptions_CheckBox.Wait.ForExists(Globals.timeOut);

                //Verify the Exceptions check box is checked  and Pending check box is Unchecked             
                if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_Exceptions_CheckBox.IsChecked == true && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_Pending_CheckBox.IsChecked == false)
                {
                    //Write the log if verification is Pass                
                    Manager.Current.Log.WriteLine(LogType.Information + " " + "Exceptions check box is Checked");
                }
                else
                {
                    //Write the log if verification is Fail
                    throw new Exception("Exceptions check box is Not Checked,Verification Failed!");
                }

                //Wait for Text Block Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_ExceptionsStatusFilterCount.Wait.ForExists(Globals.timeOut);

                //Store the runtime value in string exceptionCount
                string exceptionStatusFilterCount = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_ExceptionsStatusFilterCount.Text.ToString();
                //Get the Expected value after substring the existing string
                string exceptionsCount = exceptionStatusFilterCount.Substring(2, 1);

                //Store the runtime value in string totalCount
                string totalCountFooter = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_TotalCount_TextBlock.Text.ToString();

                //Get the Expected value after substring the existing string
                string tcnt = totalCountFooter.Substring(7, 1);

                //Compare the count and verify if condoition is true then execute if block 
                if (exceptionsCount == tcnt)
                {
                    //Write the log if verification is Pass                
                    Manager.Current.Log.WriteLine(LogType.Information + " " + "Actual Result" + " " + exceptionsCount + "Expected Result" + " " + tcnt + " ,Verification Pass");
                }

                else
                {
                    //Write the log if verification is Failed                
                    Manager.Current.Log.WriteLine(LogType.Information + " " + "Actual Result " + " " + exceptionsCount + "Expected Result" + " " + tcnt + " ,Verification Failed!!!");
                }
            }
            catch (AssertException ex)
            {
                //Write the log if verification is Fail
                throw new Exception("Payment Plans Not Found by Status Filter Exceptions" + ex.Message);
            }
        }

        // Method to verify records if Pending Check box is checked 
        public void P2PPaymentPlanWorkFlow_VerifySearchbyStatusforPending()
        {
            try
            {
                //Wait for Exceptions check box is Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_Pending_CheckBox.Wait.ForExists(Globals.timeOut);

                //Verify the Exceptions Unchecked box is checked  and Pending check box is checked             
                if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_Exceptions_CheckBox.IsChecked == false && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_Pending_CheckBox.IsChecked == true)
                {
                    //Write the log if verification is Pass                
                    Manager.Current.Log.WriteLine(LogType.Information + " " + "Pending check box is Checked");
                }
                else
                {
                    //Write the log if verification is Fail
                    throw new Exception("Pending check box is Not Checked,Verification Failed!");
                }

                //Wait for Text Block Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_PendingStatusFilterCount.Wait.ForExists(Globals.timeOut);

                //Store the runtime value in string pendingStatusFilterCount
                string pendingStatusFilterCount = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Workflow_PendingStatusFilterCount.Text.ToString();

                //Get the Expected value after substring the existing string
                string pendingCount = pendingStatusFilterCount.Substring(2, 2);

                //Store the runtime value in string totalCount
                string totalCountFooter = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_TotalCount_TextBlock.Text.ToString();

                //Get the Expected value after substring the existing string
                string tcnt = totalCountFooter.Substring(7, 2);

                //Compare the count and verify if condoition is true then execute if block 
                if (pendingCount == tcnt)
                {
                    //Write the log if verification is Pass                
                    Manager.Current.Log.WriteLine(LogType.Information + " " + "Actual Result" + " " + pendingCount + "Expected Result" + " " + tcnt + " ,Verification Pass");
                }

                else
                {
                    //Write the log if verification is Failed                
                    Manager.Current.Log.WriteLine(LogType.Information + " " + "Actual Result " + " " + pendingCount + "Expected Result" + " " + tcnt + " ,Verification Failed!!!");
                }
            }
            catch (AssertException ex)
            {
                //Write the log if verification is Fail
                throw new Exception("Payment Plans Not Found by Status Filter Pending" + ex.Message);
            }
        }

        //Method to Verify MyTasks PaymentPlan Grid is not empty
        public void P2PVerifyMyTasksPaymentPlanGridNotEmpty(string verifyEmptyGrid = null)
        {
            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PaymentPlans_GridViewControl.Wait.ForExists(Globals.timeOut);
            
            //Declare & assign 'RadGridView' variable
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PaymentPlans_GridViewControl;

            if (verifyEmptyGrid != null)
            {
                bool found = grid.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(verifyEmptyGrid));
                if (found == true)
                {
                    //Print the Logs in case of failure
                    Manager.Current.Log.WriteLine(LogType.Error, " Payment Plan found '" + verifyEmptyGrid + "' in the Grid. Verification Failed!");
                    throw new Exception("Verification Failed");
                }
                else
                {
                    //Print the Logs 
                    Manager.Current.Log.WriteLine(LogType.Information, " Payment Plan '" + verifyEmptyGrid + "' not found in the Grid. Verification Passed!");
                }
            }

            else
            {
                if (grid.Rows.Count.Equals(0))
                {
                    //Print the Logs in case of failure
                    Manager.Current.Log.WriteLine(LogType.Error, "No Payment Plans found, Grid is empty. Verification Failed!");
                }
                else
                {
                    //Print the Logs 
                    Manager.Current.Log.WriteLine(LogType.Information, " Search Succeeded, Payment Plans Found. Grid is Not empty. Verification Passes !!");
                }
            }
        }

        //Method To Verify Removed Payment Plan is in ReadOnly Mode
        public void P2PVerfiyRemovePaymentPlanInReadOnlyMode(string paymentPlan)
        {

            //Wait for Payment Paln Schedule Grid Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Create_PaymentSchedule_Grid.Wait.ForExists(Globals.timeOut);

            if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_HeaderDataField_PlanName_TextBox.IsReadOnly == true
                && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_PlanReference_HeaderData_TextBox.IsReadOnly == true
                && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Create_PaymentSchedule_Grid.IsReadOnly == true
                && SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.IsReadOnly == true)
            {
                // Wait for Attachment Tab Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_AttachmentsTabItem.Wait.ForExists(Globals.timeOut);
                //Click on Attachment Tab Items in Invoice Details Page
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_AttachmentsTabItem.User.Click();
                if (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddButton.IsEnabled == false)
                    //Print the Logs in case of failure
                    Manager.Current.Log.WriteLine(LogType.Information, "Payment Plan Hearder Data Feilds,Payment Plan Schedule & Coding Rows are in ReadOnly Mode Against " + paymentPlan + " Verification Passed!!");
            }

            else
            {
                //Print the Logs 
                Manager.Current.Log.WriteLine(LogType.Error, "Payment Plan Hearder Data Feilds, Payment Plan Schedule & Coding Rows are not in ReadOnly Mode Against " + paymentPlan + " Verification Failed!");
            }
        }

        //Method to verify Coding Rows grid and Payment Schedule Grid
        public void P2PVerifyCodingRowsandPaymentScheduleGrid(string codingRowsGrid = null)
        {

            if (codingRowsGrid != null)
            {
                //Wait for Coding Rows Grid
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Wait.ForExists(Globals.navigationTimeOut);

                //Find the Coding Row GridView                 
                RadGridView gridCodingRows = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl;


                // Check the Grid is Empty?
                if (gridCodingRows.Rows.Count.Equals(0))
                {
                    //Write the log if Verification Pass
                    Manager.Current.Log.WriteLine(LogType.Information, " Coding Rows Grid is Empty, Verification Passed!");
                }
                else
                {
                    //Write the log if Verification Fails
                    throw new Exception("Coding Rows Grid is Not empty, Verificaton Failed !");
                }
            }

            else
            {

                //Wait for Payment Schedule Grid Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Create_PaymentSchedule_Grid.Wait.ForExists(Globals.timeOut);

                //Find the Payment Schedule GridView                 
                RadGridView gridPaymentSchedule = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Create_PaymentSchedule_Grid;

                // Check the Grid is Empty?
                if (gridPaymentSchedule.Rows.Count.Equals(0))
                {
                    //Write the log if Verification Pass
                    Manager.Current.Log.WriteLine(LogType.Information, " Payment Schedule Grid is Empty, Verification Passed!");
                }
                else
                {
                    //Write the log if Verification Fails
                    throw new Exception("Payment Schedule Grid is Not empty, Verificaton Failed !");
                }

            }
        }

        //Method to verify Free Text Search: IA, PP, Purchase 
        public void P2PFreeTextSearch(string headerName, int iteration, string searchText, string searchOption = null)
        {
            //This bool value used for "Exit from For Loop "
            bool searchResultsFound = false;            

            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol")).CastAs<RadGridView>();

            //If condition is true then execute  if block
            if (grid.Rows.Count.Equals(0))
            {
                //Execute for loop for search the invoice                
                for (int i = 0; i < iteration; i++)
                {
                    //Call method to refresh search
                    P2PRefreshSearch(searchText, searchOption);
                    
                    //Create a new RadGridView Control
                    RadGridView newgrid = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol")).CastAs<RadGridView>();

                    //Condition For Exis from the Loop
                    if (newgrid.Rows.Count.Equals(0) == false)
                    {
                        searchResultsFound = true;
                        break;
                    }
                }
            }
            else
            {
                searchResultsFound = true;
            }

            //If grid has some rows then execute this block
            if (searchResultsFound == true)
            {
                //Check value in Exact Column using Column Header Name Value
                GridViewHeaderCell header = grid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == headerName);
                //Count all rows and save it in index variable
                int index = header.Index;

                //Verify Results in each row in grid
                foreach (GridViewRow row in grid.Rows)
                {
                    //Compare Each Row Value
                    if (row.Cells[index].TextBlockContent != searchText)
                    {
                        //Write the log if Verification Fail
                        Manager.Current.Log.WriteLine(LogType.Error, " Grid shows row/rows having value other than: (" + searchText + "), under column: (" + headerName + "). Verification Failed!!!!");
                    }
                }
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, " Grid shows row/rows searched by text: (" + searchText + "), under column: (" + headerName + "). Verification Passed!");
            }
            else
            {
                //Write the log if Verification Fail
                Manager.Current.Log.WriteLine(LogType.Error, " Grid is empty, text : (" + searchText + "), under column: (" + headerName + ") is Not found.");
            }       
        }

        //Method to refresh Search 
        public void P2PRefreshSearch(string searchText, string searchOption = null)
        {
            //Case1 : using P2P_Invoice_Administration_HeaderData_SearchTextBox
            if (searchOption != null)
            {
                //Wait for Search Textbox to Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.Wait.ForExists(Globals.timeOut);

                //Clear Search Textbox
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.Text = string.Empty;

                //Click on Search button 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();

                //Wait for Refresh Button to Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_RefreshButton.Wait.ForExists(Globals.timeOut);

                //Set Focus on Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_RefreshButton.SetFocus();

                //Click on Refresh button 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_RefreshButton.User.Click();
                
                //Wait for Search Textbox to Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.Wait.ForExists(Globals.timeOut);

                //Enter the Search Term to search for invoice
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.Text = searchText;

                //Click on Search button 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();
            }
            //Case1 : using P2P_Invoice_Administration_SearchTextbox
            else
            {
                //Wait for Search Textbox to Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Wait.ForExists(Globals.timeOut);

                //Clear Search Textbox
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Text = string.Empty;

                //Click on Search button 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();

                //Wait for Search Textbox to Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Wait.ForExists(Globals.timeOut);

                //Enter the Search Term to search for invoice
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Text = searchText;

                //Click on Search button 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();              
            }
        }        

        //Verify the Task state change after deleted the Task from the Task Management Grid
        public void P2PPayment_VerifytheTaskStateChange(string headerName, string recipientColumnText, string planName)
        {
            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control.Wait.ForExists(Globals.timeOut);

            do
            {

                // Wait for Refresh Button Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_RefreshButton.Wait.ForExists(Globals.timeOut);

                //Click on Refresh Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_RefreshButton.User.Click();

                //Wait for Grid Control Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control.Wait.ForExists(Globals.timeOut);

                //Refresh Grid
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control.Refresh();
                
                //Wait for Grid Control Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control.Wait.ForExists(Globals.timeOut);

            } while (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control.Rows.Count.Equals(0) == true);

            try
            {
                //Create a new RadGridView Control
                RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control;

                //Check value in Exact Column using Column Header Name Value
                GridViewHeaderCell header = grid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == headerName);

                //Check Header value is not Null
                Assert.IsNotNull(header);

                //Count all rows and save it in index varibable 
                int index = header.Index;

                //Verify Results in each row in grid
                foreach (GridViewRow row in grid.Rows)
                {
                    //Compare Each Row Value
                    if (row.Cells[index].TextBlockContent != recipientColumnText)
                    {
                        //Write the log if Verification Fail
                        throw new Exception("Invoice Number/Payment Plan  " + " " + planName + " " + "Found by Different data, Verification Failed!");
                    }
                }

                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information + " " + "Invoices/Payment Plan Found by " + ":" + recipientColumnText + " against Invoice Number/Payment Plan  " + " " + planName + " " + "Verification Passed");
            }
            catch (AssertException ex)
            {
                // Write the log if verification is Fail
                throw new Exception("Unable to Find the Column Header:" + " " + headerName + "Unable to Verify!" + ex.Message);
            }
        }

        //Verify the Validation Period Date  of a Payment Plan
        public void P2PPayment_VerifyValidationPeriodDate(string headerName, string paymentPlanDate)
        {
            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control.Wait.ForExists(Globals.timeOut);

            //Set Focus on Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control.SetFocus();

            //Click on Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control.User.Click();

            //Performing keyboard action
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Home);
         
            //Create a new RadGridView Control
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control;

            //Check value in Exact Column using Column Header Name Value
            GridViewHeaderCell header = grid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == headerName);
            
            //Declarte boolean variable
            bool found = false;
            
            do
            {
                //Declared & Initialize framework element fe.
                FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control;

                //Check whether any TextBlock in the ListBox contains the specified string.
                found = header.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(headerName));

                //Performing keyboard action
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Right);

            } while (found == false);                   
                
            //Check Header value is not Null
            Assert.IsNotNull(header);
                
            //Count all rows and save it in index varibable 
            int index = header.Index;
                
            //Verify Results in each row in grid
            foreach (GridViewRow row in grid.Rows)
            {
                //Compare Each Row Value
                if (row.Cells[index].TextBlockContent != paymentPlanDate)
                {
                    //Write the log if Verification Fail
                    Manager.Current.Log.WriteLine(LogType.Error, "Payment Plan not found with " + paymentPlanDate + ". Verification Failed");

                    //Write the log if Verification Fail
                    throw new Exception("Payment Plan found with " + paymentPlanDate + ". Verification Failed!");
                }
             }
            //Write the log if Verification Pass
            Manager.Current.Log.WriteLine(LogType.Information, "Payment Plan found with " + paymentPlanDate + ". Verification Passed");
        }

        //Verify Advance Search Clear Button
        public void P2PPayment_VerifyAdvanceSearchClearCriteria()
        {
            //Wait for TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Search_AdvanceSearch_ValidDatePickerTextBox.Wait.ForExists(Globals.timeOut);

            if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Search_AdvanceSearch_ValidHeaderDataCheckbox.IsChecked == false
                && SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Search_AdvanceSearch_ValidDatePickerTextBox.Text == string.Empty)
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, "Advance Search Criteria is cleared. Verification Passed");
            }
            else
            {
                //Write the log if Verification Fails
                Manager.Current.Log.WriteLine(LogType.Error, "Advance Search Criteria is not cleared. Verification Failed");
            }

            //Wait for close button of Advance Search
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.Wait.ForExists(Globals.timeOut);

            //Click on close button of Advance Search
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.User.Click();
        }

        //Verify Advance Search Clear Button
        public void P2PPaymentPlan_VerifyOpenedPaymentPlans(string toolbarTextBlockValue)
        {
            //Wait for Tool Bar TextBlock to load
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_ToolBar_PageTextTextblock.Wait.ForExists(Globals.timeOut);

            //Get text from TextBlock
            string compareTextValue = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_ToolBar_PageTextTextblock.Text;

            if (compareTextValue == toolbarTextBlockValue)
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, "'" + toolbarTextBlockValue + "' matched with '" + compareTextValue + "'. Verification Passed");
            }

            else
            {
                //Write the log if Verification Fails
                Manager.Current.Log.WriteLine(LogType.Error, "'" + toolbarTextBlockValue + " does not matched with '" + compareTextValue + "'. Verification Failed");
            }
        }

        //Verify Payment Plan Tab Count
        public void P2PVerifyTabCount(string tabName, string scenario, int expectedOriginalCount, int expectedActualCount)
        {
            switch (scenario.ToUpper())
            {
                case "INCREASE":
                    if ((expectedOriginalCount + 1) == expectedActualCount)
                    {
                        //Write the log if Verification Pass
                        Manager.Current.Log.WriteLine(LogType.Information, tabName + " Tab Count is increased by 1. Original Count was: " + expectedOriginalCount + ". Actual count is: " + expectedActualCount + ". Verification Passes !!");
                    }
                    else
                    {
                        //Write the log if Verification Fails
                        Manager.Current.Log.WriteLine(LogType.Error, tabName + " Tab Count is Not increased by 1. Original Count was: " + expectedOriginalCount + ". Actual count is: " + expectedActualCount + ". Verification Fails !!");
                    }
                    break;
                case "DECREASE":
                    if ((expectedOriginalCount - 1) == expectedActualCount)
                    {
                        //Write the log if Verification Pass
                        Manager.Current.Log.WriteLine(LogType.Information, tabName + " Tab Count is decreased by 1. Original Count was: " + expectedOriginalCount + ". Actual count is: " + expectedActualCount + ". Verification Passes !!");
                    }
                    else
                    {
                        //Write the log if Verification Fails
                        Manager.Current.Log.WriteLine(LogType.Error, tabName + " Tab Count is Not decreased by 1. Original Count was: " + expectedOriginalCount + ". Actual count is: " + expectedActualCount + ". Verification Fails !!");
                    }
                    break;
                case "SAME":
                    if (expectedOriginalCount  == expectedActualCount)
                    {
                        //Write the log if Verification Pass
                        Manager.Current.Log.WriteLine(LogType.Information, tabName + " Tab Count is same. Original Count was: " + expectedOriginalCount + ". Actual count is: " + expectedActualCount + ". Verification Passes !!");
                    }
                    else
                    {
                        //Write the log if Verification Fails
                        Manager.Current.Log.WriteLine(LogType.Error, tabName + " Tab Count is Not same. Original Count was: " + expectedOriginalCount + ". Actual count is: " + expectedActualCount + ". Verification Fails !!");
                    }
                    break;
                case "PENDING":
                    expectedActualCount = Convert.ToInt32(SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MyTasks_Pending_RadioButton.Text.Trim());
                    if (expectedOriginalCount + 1 == expectedActualCount)
                    {
                        //Write the log if Verification Pass
                        Manager.Current.Log.WriteLine(LogType.Information, tabName + " Tab Count is same. Original Count was: " + expectedOriginalCount + ". Actual count is: " + expectedActualCount + ". Verification Passes !!");
                    }
                    else
                    {
                        //Write the log if Verification Fails
                        Manager.Current.Log.WriteLine(LogType.Error, tabName + " Tab Count is Not same. Original Count was: " + expectedOriginalCount + ". Actual count is: " + expectedActualCount + ". Verification Fails !!");
                    }
                    break;
                case "ONHOLD":
                    expectedActualCount = Convert.ToInt32(SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_OnHoldRadioButton.Text.Trim());
                    if (expectedOriginalCount - 1 == expectedActualCount)
                    {
                        //Write the log if Verification Pass
                        Manager.Current.Log.WriteLine(LogType.Information, tabName + " Tab Count is same. Original Count was: " + expectedOriginalCount + ". Actual count is: " + expectedActualCount + ". Verification Passes !!");
                    }
                    else
                    {
                        //Write the log if Verification Fails
                        Manager.Current.Log.WriteLine(LogType.Error, tabName + " Tab Count is Not same. Original Count was: " + expectedOriginalCount + ". Actual count is: " + expectedActualCount + ". Verification Fails !!");
                    }
                    break;
                default:
                    //Write the log if Verification Fails
                    Manager.Current.Log.WriteLine(LogType.Error, tabName + " Tab Count could not be Verified!!");
                    break;
            }            
        }

        //Verify Payment Plan Open Page
        public void P2PPaymentPlan_OpenedPaymentPlansPageVerification(Boolean received, Boolean search)
        {
            if (received == true)
            {
                if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Button.IsChecked == true)
                {
                    //Write the log if Verification Pass
                    Manager.Current.Log.WriteLine(LogType.Information, "Payment plan is opened in Received Page. Verification Passed");
                }

                else
                {
                    //Write the log if Verification Fails
                    Manager.Current.Log.WriteLine(LogType.Error, "Payment plan is not opened in Received Page. Verification Failed");
                }
            }

            if (search == true)
            {
                if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Button.IsChecked == true)
                {
                    //Write the log if Verification Pass
                    Manager.Current.Log.WriteLine(LogType.Information, "Payment plan is opened in Search Page. Verification Passed");
                }

                else
                {
                    //Write the log if Verification Fails
                    Manager.Current.Log.WriteLine(LogType.Error, "Payment plan is not opened in Search Page. Verification Failed");
                }
            }
        }

        //Verify Description TextBox Data
        public void P2PPaymentPlan_VerifyDescriptionData(string descriptionTextValue)
        {
            //Wait for TextBlock to load
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_DescriptionHeaderDataTextbox.Wait.ForExists(Globals.timeOut);

            //Get text from TextBlock
            string compareTextValue = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Received_DescriptionHeaderDataTextbox.Text;

            if (compareTextValue == descriptionTextValue)
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, "'" + descriptionTextValue + "' matched with Description TextBox data '" + compareTextValue + "'. Verification Passed");
            }

            else
            {
                //Write the log if Verification Fails
                Manager.Current.Log.WriteLine(LogType.Error, "'" + descriptionTextValue + "' not matched with Description TextBox data '" + compareTextValue + "'. Verification Failed");
            }
        }

        public void P2PPaymentPlan_VerifyEditButton(Boolean editButton)
        {
            //Wait for button exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_EditToolBar_Button.Wait.ForExists(Globals.timeOut);

            if (editButton==true)
            {
            //Verify the Button is Enabled
            if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_EditToolBar_Button.IsEnabled == false)
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, "Edit button is Disabled in Detailed Page, Verfication Passed");
            }
            else
            {
                //Write the log if Verification Fails
                Manager.Current.Log.WriteLine(LogType.Error, "Edit button is Enabled in Detailed Page, Verfication Failed");
            }
            }
            else
            {
                 //Verify the Button is Enabled
            if (SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_EditToolBar_Button.IsEnabled == true)
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, "Edit button is Enabled in Detailed Page, Verfication Passed");
            }
            else
            {
                //Write the log if Verification Fails
                Manager.Current.Log.WriteLine(LogType.Error, "Edit button is Disabled in Detailed Page, Verfication Failed");
            }
            }

        }

        //Verify the Header Data 
        public void P2PPaymentPlanVerifyHeaderData(string invoiceType, string organizationName, string paymentPlanType, string paymentPlanNumber, string statusName, string subStatusName, string sumType, string referencePerson)
        {

            //var invoice, organization, payplantype, payplannumber, status, substatus,sum,refperson; 

            //wait for Invoice Type Textbox exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Details_InvoiceTypeHeaderDataTextBox.Wait.ForExists(Globals.timeOut);
            //-- Invoice with order
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Details_OrganizationNameHeaderDataTextbox.Wait.ForExists(Globals.timeOut);
            //-- SP Core Products 6210        
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Details_PaymentPlanTypeHeaderDataTextbox.Wait.ForExists(Globals.timeOut);
            //-- Schedule-based        
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Details_PaymentPlanNumberHeaderDataTextbox.Wait.ForExists(Globals.timeOut);
            //-- 5276        
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Details_StatusNameHeaderDataTextbox.Wait.ForExists(Globals.timeOut);
            //-- Received        
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Details_SubStatusHeaderDataTextbox.Wait.ForExists(Globals.timeOut);
            //--Requested        
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Details_SumTypeHeaderDataCombobox.Wait.ForExists(Globals.timeOut);
            //-- Gross
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ReferencePersonHeaderDataTextBox.Wait.ForExists(Globals.timeOut);
            //-- Tihilä Esa

            var invoice = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Details_InvoiceTypeHeaderDataTextBox.Text.ToString();
            var organization = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Details_OrganizationNameHeaderDataTextbox.Text.ToString();
            var payplantype = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Details_PaymentPlanTypeHeaderDataTextbox.Text.ToString();
            var payplannumber = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Details_PaymentPlanNumberHeaderDataTextbox.Text.ToString();
            var status = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Details_StatusNameHeaderDataTextbox.Text.ToString();
            var substatus = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Details_SubStatusHeaderDataTextbox.Text.ToString();
            var sum = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Details_SumTypeHeaderDataCombobox.Text.ToString();
            var refperson = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ReferencePersonHeaderDataTextBox.Text.ToString();

            if ((invoice == invoiceType) && (organization == organizationName) && (payplantype == paymentPlanType) && (payplannumber == paymentPlanNumber) && (status == statusName) && (substatus == subStatusName) && (sum == sumType) && (refperson == referencePerson))
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, "Verification Passed");
            }
            else
            {

                //Write the log if Verification Fails
                Manager.Current.Log.WriteLine(LogType.Error, "Verification Failed");
            }

        }

    }
}