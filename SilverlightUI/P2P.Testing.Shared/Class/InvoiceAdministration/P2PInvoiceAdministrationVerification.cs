using System;
using System.Linq;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Silverlight.UI;
using System.IO;
using ArtOfTest.Common.UnitTesting;
using Telerik.WebAii.Controls.Xaml;
using ArtOfTest.Common.Exceptions;
using System.Xml;
using Telerik.WebAii.Controls.Html;
using System.Windows.Forms;
using E2E.Class;
using ArtOfTest.WebAii.Win32.Dialogs;
using ArtOfTest.WebAii.Win32;

namespace P2P.Testing.Shared.Class.InvoiceAdministration
{
    public class P2PInvoiceAdministrationVerification : BaseWebAiiTest
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

        //************************ Common Grid Functions************************************//
        //Method to verify the Grid is empty
        public void P2PVerifyGridIsEmpty()
        {
            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol")).Wait.ForExists(Globals.timeOut);

            //Get Grid reference
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol")).CastAs<RadGridView>();

            if (grid.Rows.Count.Equals(0))
            {
                //Print the Logs if verification passes
                Manager.Current.Log.WriteLine(LogType.Information, " Grid is Empty. Verification Passes!!");
            }
            else
            {
                //Print the Logs in case of failure
                Manager.Current.Log.WriteLine(LogType.Error, " Error: Grid is not empty. Verification Failed!!");
            }
        }

        //Method to verify Free Text Search( one value under column): IA, PP, Purchase, Matching
        public void P2PVerifyFreeTextSearch(string headerName, int iteration, string searchText, string searchOption)
        {
            //This bool value used for "Exit from For Loop "
            bool searchResultsFound = false;

            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol")).Wait.ForExists(Globals.timeOut);
            //Get Grid reference
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol")).CastAs<RadGridView>();

            //Refresh the grid.
            grid.Refresh();

            //If condition is true then execute  if block
            if (grid.Rows.Count.Equals(0))
            {
                //Execute for loop for search the invoice                
                for (int i = 0; i < iteration; i++)
                {
                    //Search the text again in grid
                    Manager.Current.Log.WriteLine(LogType.Information, " Grid returned 0 rows. Search again. Iteration: " + i);
                    //Call method to refresh search
                    P2PRefreshSearch(searchText, searchOption);

                    //Refresh the Grid to Get the Updated Result
                    grid.Refresh();

                    //Condition For Exis from the Loop
                    if (grid.Rows.Count.Equals(0) == false)
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
                grid.Refresh();
                grid.SetFocus();
                //Check value in Exact Column using Column Header Name Value
                GridViewHeaderCell header = grid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == headerName);
                //Count all rows and save it in index variable
                int index = header.Index;

                //Verify Results in each row in grid
                foreach (GridViewRow row in grid.Rows)
                {
                    //Compare Each Row Value for all grids
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
                Manager.Current.Log.WriteLine(LogType.Error, " Error: Grid is empty, text : (" + searchText + "), under column: (" + headerName + ") is Not found.Verification Failed!!");
            }
        }

        //Method to refresh Search 
        public void P2PRefreshSearch(string searchText, string searchOption)
        {
            switch (searchOption.ToUpper())
            {
                case "IA_SEARCH":
                    {
                        //Wait for Search Textbox to Exists in DOM Tree
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.Wait.ForExists(Globals.timeOut);

                        //Clear Search Textbox
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.Text = string.Empty;

                        //Click on Search button 
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();

                        //Wait for Search Textbox to Exists in DOM Tree
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.Wait.ForExists(Globals.timeOut);

                        //Enter the Search Term to search for invoice
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SearchTextBox.User.TypeText(searchText, 50);

                        //Click on Search button 
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();

                        break;
                    }
                case "SEARCH":
                    {
                        //Wait for Search Textbox to Exists in DOM Tree
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Wait.ForExists(Globals.timeOut);

                        //Clear Search Textbox
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Text = string.Empty;

                        //Click on Search button 
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();

                        //Wait for refresh button to load
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_RefreshButton.Wait.ForExists(Globals.timeOut);

                        //Click on refresh button
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_RefreshButton.User.Click();

                        //Call again Coded Handle Busy Indicater
                        P2PNavigation.CallBusyIndicator();

                        //Wait for Search Textbox to Exists in DOM Tree
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Wait.ForExists(Globals.timeOut);

                        //Enter the Search Term to search for invoice
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.User.TypeText(searchText, 50);

                        //Click on Search button 
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();
                        break;
                    }
                case "SHOP_SEARCH":
                    {
                        //Wait for Search Textbox to Exists in DOM Tree
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Wait.ForExists(Globals.timeOut);

                        //Clear Search Textbox
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Text = string.Empty;

                        //Click on Search button 
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();
                       
                        //Call again Coded Handle Busy Indicater
                        P2PNavigation.CallBusyIndicator();

                        //Wait for Search Textbox to Exists in DOM Tree
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Wait.ForExists(Globals.timeOut);

                        //Enter the Search Term to search for invoice
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.User.TypeText(searchText, 50);

                        //Click on Search button 
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();
                        break;
                    }
                case "MATCHING_SEARCH":
                    {
                        //Wait for Search Textbox to Exists in DOM Tree
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Wait.ForExists(Globals.timeOut);

                        //Clear Search Textbox
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Text = string.Empty;

                        //Click on Search button 
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();

                        //Wait for refresh button to load
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_RefreshButton.Wait.ForExists(Globals.timeOut);

                        //Click on refresh button
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_RefreshButton.User.Click();

                        //Wait for Search Textbox to Exists in DOM Tree
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Wait.ForExists(Globals.timeOut);

                        //Enter the Search Term to search for invoice
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.User.TypeText(searchText, 50);

                        //Click on Search button 
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();
                        break;
                    }
                default:
                    {
                        //Log Failure for Search TextBox not found
                        Manager.Current.Log.WriteLine(LogType.Error, " Search Option not found.  Name being searched: " + searchOption + ". Verification Failed !!");
                        break;
                    }
            }
        }

        //Method To Verify Search Done by Company Picker
        public void P2PVerifyCompanyPickerSearch(string company, string headerName, string organizationHeaderColumn = null)
        {
            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol")).Wait.ForExists(Globals.timeOut);
            //Get Grid reference
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol")).CastAs<RadGridView>();

            if (grid.Rows.Count.Equals(0))
            {
                // Write the log if grid is empty
                Manager.Current.Log.WriteLine(LogType.Error, " Error: No Search Results Found by Company :- " + company + ". Grid is empty. Verification Failed!!");
            }
            else
            {
                //Wait for Grid to load
                grid.Wait.ForExists(Globals.timeOut);
                grid.Refresh();
                grid.SetFocus();

                //If Organization Header Column is at the End of the Grid
                if (organizationHeaderColumn != null)
                {
                    //KeyBoard Action
                    Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.End);
                }

                grid.Refresh();
                grid.SetFocus();

                //Check value in Exact Column using Column Header Name Value
                GridViewHeaderCell header = grid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == headerName);

                //Count all rows and save it in index varibable 
                int index = header.Index;

                //Verify Results in each row in grid
                foreach (GridViewRow row in grid.Rows)
                {
                    //Compare Each Row Value
                    if (row.Cells[index].TextBlockContent != company)
                    {
                        //Write the log if Verification Fail
                        //Manager.Current.Log.WriteLine(LogType.Error, " Error: Grid shows row/rows having company other than: (" + company + "). Verification Failed!!");
                        string test = row.Cells[index].TextBlockContent;
                        string test1 = row.Cells[index].Text;
                    }
                }

                if (organizationHeaderColumn != null)
                {
                    //Set Focus
                    grid.SetFocus();

                    //KeyBoard Action
                    Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Home);
                }

                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, " Grid shows row/rows searched by company: (" + company + "). Verification Passed!");
            }
        }

        //Method to Verify Coding Rows Grid is Empty
        //Can not use common grid function in this case, as there is no unique gird showing on the screen with XamlTag=baslistviewgridcontrol.
        public void P2PVerifyCodingRowsGridEmpty()
        {
            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Wait.ForExists(Globals.timeOut);

            //Get Grid reference
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl;

            if (grid.Rows.Count.Equals(0))
            {
                //Print the Logs if verification passes
                Manager.Current.Log.WriteLine(LogType.Information, " Grid is Empty. Verification Passes!!");
            }
            else
            {
                //Print the Logs in case of failure
                Manager.Current.Log.WriteLine(LogType.Error, " Error: Grid is not empty. Verification Failed!!");
            }
        }

        //Method to Verify Free Text Search : My Tasks: Payment Plan tab 
        //Can not use common grid function in this case, as there is no unique gird showing on the screen with XamlTag=baslistviewgridcontrol.
        public void P2PMyTasksVerifyPaymentPlanFreeTextSearch(string searchText, string headerName)
        {
            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PaymentPlans_GridViewControl.Wait.ForExists(Globals.timeOut);
            //Use FrameworkElement and read all the TextBlocks 
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PaymentPlans_GridViewControl;

            //If condition is true then execute  if block
            if (grid.Rows.Count.Equals(0))
            {
                //Write the log if Verification Fail
                throw new Exception("Grid is Empty, Text Not Found (" + searchText + "). Verification Failed!!!");
            }
            else
            {
                //Use Exception Handling 
                try
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
                        if (row.Cells[index].TextBlockContent != searchText)
                        {
                            //Write the log if Verification Fail
                            throw new Exception("Text Not Found (" + searchText + "). Verification Failed!!!");
                        }
                    }
                    //Write the log if Verification Pass
                    Manager.Current.Log.WriteLine(LogType.Information, " Search Succeeded, Results Found by Free Text Search : " + searchText);
                }
                catch (AssertException ex)
                {
                    // Write the log if verification is Fail
                    throw new Exception("Unable to Find the Column Header:" + " " + headerName + "Unable to Verify!" + ex.Message);
                }
            }
        }

        //Method to verify Free Text Search( multiple values under same Column in a grid ): IA, PP, Purchase, Matching
        public void P2PVerifyGridSearchMultipleValues(string headerName, string[] expectedSearchValues)
        {
            //This bool value used for "Exit from For Loop "
            bool searchResultsFound = true;

            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol")).CastAs<RadGridView>();

            //If condition is true then execute  if block
            if (grid.Rows.Count.Equals(0))
            {
                //Write the log if Verification Fail
                Manager.Current.Log.WriteLine(LogType.Error, " Grid is Empty. Verification Failed!!!!");
            }
            else
            {
                //Check value in Exact Column using Column Header Name Value
                GridViewHeaderCell header = grid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == headerName);
                //Count all rows and save it in index variable
                int index = header.Index;

                //Verify Results in each row in grid
                foreach (GridViewRow row in grid.Rows)
                {
                    if (expectedSearchValues.Contains(row.Cells[index].TextBlockContent))
                    {
                        //Do nothing
                        Manager.Current.Log.WriteLine(LogType.Information, "Grid shows row having valid column value : (" + row.Cells[index].TextBlockContent + "), under column: (" + headerName + "). Verification Passed!");
                    }
                    else
                    {
                        //Value Grid Value not found in expected values array
                        searchResultsFound = false;
                        //Write the log if Verification Fail
                        Manager.Current.Log.WriteLine(LogType.Information, " Grid shows row having invalid column value : (" + row.Cells[index].TextBlockContent + "), under column: (" + headerName + "). Verification Failed!!!!");
                        break;
                    }
                }

                //Get all values from array into string
                string allValues = string.Empty;
                foreach (string searchValue in expectedSearchValues)
                {
                    allValues = allValues + ", " + searchValue;
                }
                allValues = allValues.Substring(2);

                if (searchResultsFound == true)
                {
                    //Write the log if Verification Pass                    
                    Manager.Current.Log.WriteLine(LogType.Information, " Grid shows row/rows searched with valid values (" + allValues + "), under column: (" + headerName + "). Verification Passed!");
                }
                else
                {
                    //Write the log if Verification Fail
                    Manager.Current.Log.WriteLine(LogType.Error, " Grid shows row/rows having invalid value under column: (" + headerName + "). Verify from logs above. Verification Failed!!!!");
                }
            }
        }


        //************************ End of Common Grid Functions************************************//




        //************************ History and Comments Verification Methods: Detail and Main Page************************************//

        //Method to Verify Comments on Main Page: All Comments Pop-up and History Pop-up
        public void P2PMainPageCommentsVerification(string comment, string invoiceId)
        {
            //Find the Invoice Numbers in Grid 
            TextBlock invoiceNumber = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(invoiceId).As<TextBlock>();

            //Click One Invoice to Clear the Default Selection
            invoiceNumber.User.Click();

            //*********************************************Verify from "All Comments" Hyper Link*********************************************                  
            P2PMainPageAllCommentsPopUpVerification(comment, invoiceId, "All Comments Pop-up");

            //*********************************************Verify from "History" Panel PopUp*********************************************
            P2PMainPageHistoryPopUpVerification(comment, invoiceId, "History Pop-up");
        }

        //Method to Verify Comments on Main Page: History Pop-up (right panel on main page)
        public void P2PMainPageHistoryPopUpVerification(string comment, string invoiceID, string windowName)
        {
            //Wait for History Link Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryButton.Wait.ForExists(Globals.timeOut);

            //Click on History Link Under Invoice Details Panel  
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryButton.User.Click();

            try
            {
                P2PHistoryListBoxVerification(comment, invoiceID, windowName);
            }
            finally
            {
                //Close the History Pop-Up
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.User.Click();
            }
        }

        //Method to Verify Comments on Main Page: All Comments Pop-up (Upper right panel on main page)
        public void P2PMainPageAllCommentsPopUpVerification(string comment, string invoiceID, string windowName)
        {
            //Wait for All Comments Link Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_AllCommentsButton.Wait.ForExists(Globals.timeOut);

            //Click on All Comments Link Pop-Up
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_AllCommentsButton.User.Click();

            try
            {
                P2PHistoryListBoxVerification(comment, invoiceID, windowName);
            }
            finally
            {
                //Close the All Comments Pop-Up
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.User.Click();
            }
        }

        //Method to Verify Comments on Detail Page: History and Comments Tab 
        public void P2PDetailPageCommentsVerification(string comboBoxItems, string comment, string invoiceNumber, string moduleName)
        {
            //moduleName valid values: PAYMENT_PLAN, IA, MY_TASKS_PAYMENT_PLAN, IA_SEARCH
            switch (moduleName.ToUpper())
            {
                case "PAYMENT_PLAN":
                    {
                        //SEt focus History and Comments TabItem
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkflow_HistoryandCommentsTab.SetFocus();
                        //Click on the History and Comments TabItem
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkflow_HistoryandCommentsTab.User.Click();
                        //break out of loop
                        break;
                    }
                case "IA":
                    {
                        //SEt focus History and Comments TabItem
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsTab.SetFocus();
                        //Click on the History and Comments TabItem
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsTab.User.Click();
                        //break out of loop
                        break;
                    }
                case "MY_TASKS_PAYMENT_PLAN":
                    {
                        //SEt focus History and Comments TabItem
                        SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_HistoryandCommentsTabItem.SetFocus();
                        //Click on the History and Comments TabItem
                        SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_HistoryandCommentsTabItem.User.Click();
                        //break out of loop
                        break;
                    }
                case "IA_SEARCH":
                    {
                        //SEt focus History and Comments TabItem
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_HistoryandCommentsTab.SetFocus();
                        //Click on the History and Comments TabItem
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_HistoryandCommentsTab.User.Click();
                        //break out of loop
                        break;
                    }
                case "MY_TASK":
                    {
                        //SEt focus History and Comments TabItem
                        SharedElement.P2P_Application.SilverlightApp.P2P_Mytask_HistoryAndCommentsTabItem.SetFocus();
                        //Click on the History and Comments TabItem
                        SharedElement.P2P_Application.SilverlightApp.P2P_Mytask_HistoryAndCommentsTabItem.User.Click();
                        //break out of loop
                        break;
                    }
                default:
                    {
                        //Log Failure for Tab item not found
                        Manager.Current.Log.WriteLine(LogType.Error, " History and Comments TabItem not found. Module Name being searched: " + moduleName + ". Verification Failed !!");
                        break;
                    }
            }

            //Handle Busy Indicator                
            P2PNavigation.CallBusyIndicator();
            
            //Open combobox dropdown 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_FilteringComments_ComboBox.OpenDropDown(true);
            //Select the correct item from the Combo Box dropdown
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_FilteringComments_ComboBox.SelectItemByText(true, comboBoxItems, true);
            
            // Verify 'History and Comments TabItem' Contains Comments             
            P2PHistoryListBoxVerification(comment, invoiceNumber, "History and Comments TabItem");
        }

        //Method to Verify HistoryListBoxVerification: this method can be used for main page and detail page.
        public void P2PHistoryListBoxVerification(string comment, string invoiceId, string windowName)
        {
            //Case1: Verify the string in RichText Block
            //Case2: Verify the string in TextBlock               

            //Intialiase Varaibles
            bool foundRichTextBlock = false;
            bool foundTextBlock = false;

            //Handle Busy Indicator                
            P2PNavigation.CallBusyIndicator();

            //Wait for History and Comments ListBox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsListBox.Wait.ForExists(Globals.timeOut);
            //Get Framework element
            FrameworkElement e = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsListBox;
            //Refresh the list box
            e.Refresh();

            //Case1 : starts
            //Get RichTextBox and Verify the Comments in History Pop-Up
            //Create The Xml Document
            XmlDocument doc = new System.Xml.XmlDocument();
            //intilaise string
            string rtbContents = string.Empty;
            //Check whether any TextBlock contains the specified string.
            foundRichTextBlock = e.Find.AllByType<ArtOfTest.WebAii.Silverlight.UI.RichTextBox>().Any((tb) =>
            {
                //Save in string
                rtbContents = (string)tb.GetProperty(new AutomationProperty("Xaml", typeof(string)));
                //Load string in the Document
                doc.LoadXml(rtbContents);
                return doc.InnerText.Contains(comment);
            });

            //Case2 : starts
            //Get TextBlock and Verify the Comments in History Pop-Up                     
            foundTextBlock = e.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(comment));

            //Check using If Condition that Comments Exists
            if ((foundRichTextBlock == true) || (foundTextBlock == true))
            {
                //Log the results if Comments Exists in All Comments Pop-up
                Manager.Current.Log.WriteLine(LogType.Information, " Comment: " + comment + " Exists in " + windowName + ", against :- " + invoiceId + ", Verification Passed!");
            }
            else
            {
                //Throw the Exception if Comments does Not Exists
                Manager.Current.Log.WriteLine(LogType.Error, " Comment: " + comment + " does NOT Exists in " + windowName + ", against :- " + invoiceId + ", Verification Failed!");
            }
        }

        //************************ End of History and Comments Verification Methods: Detail and Main Page************************************//





        public void P2PFileExistsVerification(string p2pFunctionality, string activeDirectory, string filePath, string fileName, string invoiceId)
        {
            //Verify that Filename Exists in System             
            try
            {
                //Check if the Directory Exists
                if ((System.IO.Directory.Exists(activeDirectory) == true))
                {
                    //Check if file exists
                    if ((File.Exists(filePath) == true))
                        //Write log
                        Manager.Current.Log.WriteLine(p2pFunctionality + " File Exists: " + " " + fileName + " against " + invoiceId);
                    else
                        //Throws the Exception if File does not Exists
                        throw new FileNotFoundException(p2pFunctionality + " File does not Exists: ", fileName + " against " + invoiceId);
                }
                else
                {
                    //Throws the Exception if File does not Exists
                    throw new FileNotFoundException(p2pFunctionality + " Directory does not Exist: ", activeDirectory + " against " + invoiceId);
                }
            }

            finally
            {
                //Delete the File from the System
                System.IO.File.Delete(filePath);
            }
        }

        //Method to Verify the Selected Invoice Details in Invoice Details Panel Pop Up(Header Data, Coding, History)
        public void P2PInvoiceDetailsPanelPopUpVerification(string accountCode, string invoiceID, string comment = null)
        {
            bool foundTextBlock = false;

            //Wait for Coding Link Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_CodingButton.Wait.ForExists(Globals.timeOut);

            //Click on Coding Link to Verify Account Code
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_CodingButton.User.Click();

            try
            {
                //Handle Busy Indicator                
                P2PNavigation.CallBusyIndicator();

                //Get the value in a grid
                RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl;
                //Click on on the grid for visibility          
                rgv.User.Click();

                //Workaround for Empty Grid Starts Here {bug 69588}
                bool headerCostCentreCode = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains("Cost Center Code"));

                if (headerCostCentreCode != true)
                {
                    throw new Exception(" Column Name: Cost Center Code, NOT found on Coding Row Grid Pannel");
                }
                //Workaround for Empty Grid Ends {bug 69588}

                //Keyboard Action
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Home);

                do
                {
                    //Get TextBlock and Verify the Attachments ListBox
                    FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl;

                    //Check whether any TextBlock contains the specified string.
                    foundTextBlock = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(accountCode));

                    //Press tab to move the location in grid                       
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

                    //Wait for Coding Link Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Wait.ForExists(Globals.timeOut);

                    //refresh the Framework Element
                    fe.Refresh();

                }
                while (foundTextBlock == false);

                //Store the text block value into the cells
                TextBlock textBlockCell = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(accountCode).As<TextBlock>();

                //Check using If Condition that Account Code Exists?
                if (textBlockCell.TextLiteralContent == accountCode)
                {
                    //Log the Results if Account Code Exists in Coding Pop-up
                    Manager.Current.Log.WriteLine("Account Code:" + " " + accountCode + " " + "exist in Coding Pop-up, against Invoice Number:" + " " + invoiceID);
                }
                else
                {
                    //Throw the Exception if Verification Fails
                    throw new Exception("Account Code:" + " " + accountCode + " " + "does NOT exist in Coding Pop-up, Verification Failed!, against Invoice Number:" + " " + invoiceID);
                }
            }

            finally
            {

                //Wait for Close Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.Wait.ForExists(Globals.timeOut);
                //Close the Coding Pop-Up Window
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.User.Click();
            }

            //Wait for Header Data Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HeaderDataButton.Wait.ForExists(Globals.timeOut);

            //Click on Header Data Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HeaderDataButton.User.Click();
            {
                try
                {
                    //Handle Busy Indicator                
                    P2PNavigation.CallBusyIndicator();

                    // Verify the Invoice Number exist in Header Data PopUp
                    Assert.IsFalse((ArtOfTest.Common.CompareUtils.StringCompare(SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceNumberHeaderDataTextBox.TextLiteralContent, invoiceID, ArtOfTest.Common.StringCompareType.Same) == true));

                    //Log the Results if Invoice ID Exists in Header Data Pop-up
                    Manager.Current.Log.WriteLine("Invoice Number:" + " " + invoiceID + " " + "Exists in Header Data Pop-up");
                }
                catch (Exception ex)
                {
                    //Throw the Exception if Verification Fails
                    throw new Exception("Error Message:" + " " + invoiceID + " " + "Invoice Number does NOT Exist in Header Data Pop-up, Verification Failed" + ex.Message);
                }

                finally
                {
                    //Close the Coding Pop-Up Window
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.User.Click();
                }
            }

            //Verify the Comments Exists in History Pop-up (InCase of Transfer,Workflow,Received and Matching)
            //This block will execute only if contains Comments
            if (comment != null)
            {
                P2PMainPageHistoryPopUpVerification(comment, invoiceID, "History Pop-up");
            }
        }



        //************************ Attachment Verification Methods: Detail and Main Page************************************//

        //Method to Verify Attachments Listbox: this method can be used for main page(attachment pop up link) and detail page(attachment tab).
        public void P2PAttachmentListBoxVerification(string fileName, string invoiceId, string addEditDelete, string windowName)
        {
            //Valid values for variable: windowname
            //Attachments Tab, Attachments Pop-up
            //Valid values for variable: addEditDelete
            //ADD, EDIT, DELETE

            //Handle Busy Indicator                
            P2PNavigation.CallBusyIndicator();

            //Intialiase Varaibles            
            bool foundTextBlock = false;

            //Wait for Attachments ListBox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_InvoiceDetails_Attachment_AttachmentListbox.Wait.ForExists(Globals.timeOut);

            //Get TextBlock and Verify the Attachments ListBox
            FrameworkElement e = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_InvoiceDetails_Attachment_AttachmentListbox;
            //Check whether any TextBlock contains the specified string.
            foundTextBlock = e.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(fileName));

            //Check using If Condition that Attachments Exists
            if ((foundTextBlock == true))
            {
                //Log the results for passed
                switch (addEditDelete.ToUpper())
                {
                    case "ADD":
                        {
                            Manager.Current.Log.WriteLine(LogType.Information, " Attachment: " + fileName + " Exists in " + windowName + ", against :- " + invoiceId + ". Verification Passed!");
                            break;
                        }
                    case "EDIT":
                        {
                            Manager.Current.Log.WriteLine(LogType.Information, " Edited Attachment Description: " + fileName + ", Exists in " + windowName + ", against :- " + invoiceId + ". Verification Passed!");
                            break;
                        }
                    case "DELETE":
                        {
                            Manager.Current.Log.WriteLine(LogType.Information, " Attachment Deleted against :- " + invoiceId + ". Verification Passed!");
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            else
            {
                //Log the results for failure
                switch (addEditDelete.ToUpper())
                {
                    case "ADD":
                        {
                            Manager.Current.Log.WriteLine(LogType.Error, " Attachment: " + fileName + " does not Exists in " + windowName + ", against :- " + invoiceId + ". Verification failed!");
                            break;
                        }
                    case "EDIT":
                        {
                            Manager.Current.Log.WriteLine(LogType.Error, " Edited Attachment Description: " + fileName + ", does not Exists in " + windowName + ", against :- " + invoiceId + ". Verification failed!");
                            break;
                        }
                    case "DELETE":
                        {
                            Manager.Current.Log.WriteLine(LogType.Error, " Attachment Not Deleted against :- " + invoiceId + ". Verification failed!");
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }

        //Method to Verify Attachments(add, edit, delete) at Invoice main page 
        public void P2PAttachmentsMainPageVerification(string fileName, string invoiceID, string addEditDelete, string windowName)
        {
            //Wait for Attachments Tab Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_AttachmentButton.Wait.ForExists(Globals.timeOut);

            //Click on Attachments Tab
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_AttachmentButton.User.Click();

            switch (addEditDelete.ToUpper())
            {
                case "ADD":
                    {
                        P2PAttachmentListBoxVerification(fileName, invoiceID, "ADD", windowName);
                        break;
                    }
                case "EDIT":
                    {
                        P2PAttachmentListBoxVerification(fileName, invoiceID, "EDIT", windowName);
                        break;
                    }
                case "DELETE":
                    {
                        P2PAttachmentListBoxVerification(fileName, invoiceID, "DELETE", windowName);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            //Close the Attachments Pop-Up Window
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.User.Click();
        }

        //Method to  Verify Attachments(add, edit, delete): detail page
        public void P2PAttachmentTabDetailPageVerification(string fileName, string invoiceID, string addEditDelete, string windowName, string myTasks = null)
        {
            if (myTasks != null)
            {
                // Wait for Attachment Tab Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=attachementTab")).Wait.ForExists(Globals.timeOut);

                //    //Click on Attachment Tab Items in Invoice Details Page
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=attachementTab")).User.Click();
            }
            else
            {
                // Wait for Attachment Tab Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_AttachmentsTabItem.Wait.ForExists(Globals.timeOut);

                //Click on Attachment Tab Items in Details Page
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_AttachmentsTabItem.User.Click();
            }

            switch (addEditDelete.ToUpper())
            {
                case "ADD":
                    {
                        P2PAttachmentListBoxVerification(fileName, invoiceID, "ADD", windowName);
                        break;
                    }
                case "EDIT":
                    {
                        P2PAttachmentListBoxVerification(fileName, invoiceID, "EDIT", windowName);
                        break;
                    }
                case "DELETE":
                    {
                        P2PAttachmentListBoxVerification(fileName, invoiceID, "DELETE", windowName);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        //************************ End of Attachment Verification Methods: Detail and Main Page************************************//




        //Method to Verify the Data in  Header Data 
        public void UnSavedDataWarningVerification(string supplierName)
        {
            //Wait for SupplierNameHeaderData_TextBox Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_SupplierNameHeaderData_TextBox.Wait.ForExists(Globals.timeOut);

            //Get the SupplierNameHeaderData_TextBox value and Store into a String "strSupplierName"
            string strSupplierName = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_SupplierNameHeaderData_TextBox.Text.ToString();

            //If Condition is true execute if block
            if (strSupplierName == supplierName)
            {
                //Write the Result in log file when Verification is Pass
                Manager.Current.Log.WriteLine(LogType.Information, "Unsaved Data Warning Message appears and Cancelled the Process,Expected Supplier Name" + " " + supplierName + "&" + " " + " Actual Supplier Name" + " " + strSupplierName + " " + "Verification Pass");

            }
            else
            {
                //Write the Result in log file when Verification is Fail
                throw new Exception("Unsaved Data Warning Message appears But Process does not Cancelled,Expected Supplier Name" + " " + supplierName + " " + "&" + " " + " Actual Supplier Name" + strSupplierName + " " + "Verification Failed");

            }
        }

        // Method to Verify Edited  Existing Invoice
        public void P2PInvoiceAdministration_EditInvoiceVerification(string supplierName, string currencyCode, Boolean button)
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
                Manager.Current.Log.WriteLine(LogType.Error, "Expected Supplier Name:" + " " + supplierName + " " + "&" + " " + "Actual Supplier Name" + " " + strSupplierName + " " + "Does not Edited ,Verification Failed:");
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

            else
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

            //Get the System from the RunTime Value 
            string invoiceDate = System.DateTime.Today.ToShortDateString();
            //Wait for Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_InvoiceDate_TextBox.Wait.ForExists(Globals.timeOut);
            //Get the Run time Value from the InvoiceDateText Box
            string strInvoiceDate = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_InvoiceDate_TextBox.Text.ToString();

            //If condition is True execute if block
            if (strInvoiceDate == invoiceDate)
            {
                //Write the Result in log file when Verification is Pass
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Invoice Date:" + " " + invoiceDate + " " + "&" + " " + "Actual Invoice Date" + " " + strInvoiceDate + " " + "Verification Pass");
            }
            else
            {
                //Write the Result in log file when Verification is Fail
                Manager.Current.Log.WriteLine(LogType.Error, "Expected Invoice Date:" + " " + invoiceDate + " " + "&" + " " + "Actual Invoice Date" + " " + strInvoiceDate + " " + "Does not Edited ,Verification Failed:");
            }
        }

        //Method to Verify the Prebook Status in History And Comments Tab Item
        public void P2PInvoiceAdministration_VerifyPrebookStatus(string comment, string invoiceNumber, string all = null, string comboBoxItems = null)
        {
            //Call busy indicator method  for handle the busy indicator in the Application
            P2PNavigation.CallBusyIndicator();

            //Wait for History and Comments TabItem Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_AttachmentsTabItem.Wait.ForExists(Globals.timeOut);

            //Click on the History and Comments TabItem
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_AttachmentsTabItem.User.Click();

            //Wait for History and Comments TabItem Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsTab.Wait.ForExists(Globals.timeOut);

            //Click on the History and Comments TabItem
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsTab.User.Click();

            //Call handle busy indicator method for handling the busy indicator
            P2PNavigation.CallBusyIndicator();

            if (comboBoxItems != null)
            {
                //Select the Comments from the Combo Box
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_FilteringComments_ComboBox.SelectItemByText(true, comboBoxItems, true);

                //Call handle busy indicator method for handling the busy indicator
                P2PNavigation.CallBusyIndicator();

                //Workaround if want to select the same value again and close the dropdown.
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsTab.User.Click();

                //Call handle busy indicator method for handling the busy indicator
                P2PNavigation.CallBusyIndicator();
            }

            try
            {
                //Wait for list box exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsListBox.Wait.ForExists(Globals.timeOut);

                //Verify the Comments in History Pop-Up
                FrameworkElement e = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsListBox;

                int i = 500;
                //Wait for List Box Exists in DOM 
                while (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsListBox.TextLiteralContent.Contains(comment))
                {
                    //Wait for Page load Properly 
                    System.Threading.Thread.Sleep(i);
                    i++;
                    if (i == 510)
                    {
                        break;
                    }
                }

                P2PHistoryListBoxVerification(comment, invoiceNumber, "History and Comments Tab");

                ////Check whether any TextBlock contains the specified string.
                //bool found = e.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(comment));

                ////Check using If Condition that Comments Exists?
                //if (found == true)
                //{
                //    //Write the Result in log file when Verification is Pass
                //    Manager.Current.Log.WriteLine("Comment :" + " " + comment + " " + "Exists in History and Comments TabItem against Invoice Number:-" + invoiceNumber);
                //}
                //else
                //{
                //    //Write the Result in log file when Verification is Fail
                //    throw new Exception("Comment:" + " " + comment + "does NOT exist in History and Comments TabItem, Verification Failed against Invoice Number:- " + invoiceNumber);
                //}
            }
            finally
            {
                //Combo box Selection change System Actions to ALL 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_FilteringComments_ComboBox.SelectItemByText(true, all, true);
                //Call handle busy indicator method for handling the busy indicator
                P2PNavigation.CallBusyIndicator();

                //Workaround if want to select the same value again and close the dropdown.
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsTab.User.Click();
                //Call handle busy indicator method for handling the busy indicator
                P2PNavigation.CallBusyIndicator();
            }
        }


        //Method to verify the Recipients Name in Task Grid View on Details Page
        public void P2PInvoiceAdministration_VerifyRecipientsName(string recipientName, Boolean missingRecipientName, string invoiceNumber, string recipient = null, string paymentPlan = null, string paymentPlanTaskManagement = null)
        {
            if (paymentPlan != null)
            {
                //Wait for TaskManagementTabTabitem Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_InWorkflow_TaskManagement_TabItem.Wait.ForExists(Globals.timeOut);
                //Click on TaskManagementTabTabitem
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_InWorkflow_TaskManagement_TabItem.User.Click();
            }
            else
            {
                //Wait for TaskManagementTabTabitem Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagementTabTabitem.Wait.ForExists(Globals.timeOut);
                //Click on TaskManagementTabTabitem
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagementTabTabitem.User.Click();
            }

            //If condition is true then execute if block
            if (missingRecipientName == false)
            {
                //Initialize Boolean Variable 
                bool found = false;

                if (paymentPlanTaskManagement != null)
                {
                    //Wait for P2P_Invoice_Aministration_TaskGridViewMainPage Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_WorkFlow_PlansDetails_TaskManagement_TaskGridView.Wait.ForExists(Globals.timeOut);
                    //Create  a RadGridView              
                    RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_WorkFlow_PlansDetails_TaskManagement_TaskGridView;

                    try
                    {
                        //Check whether any TextBlock in the ListBox contains the specified string.
                        found = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(recipientName));

                        //If condition is true then execute if block
                        if (found == true)
                        {
                            //Write the Log If verification is Pass
                            Manager.Current.Log.WriteLine(LogType.Information, "Recipient Name " + " " + recipientName + " " + "Exists in a Task Management grid against : " + " " + invoiceNumber + ", Verification Passed");
                        }
                    }

                    catch (AssertException ex)
                    {
                        //Write the Log If verification is Fail
                        throw new Exception("Recipient Name:" + " " + recipientName + " " + "Does NOT Exists in Task Management grid against :" + " " + invoiceNumber + ", Verification Failed!" + ex.Message);
                    }
                }
                else
                {
                    //Wait for P2P_Invoice_Aministration_TaskGridViewMainPage Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskGrid.Wait.ForExists(Globals.timeOut);
                    //Create  a RadGridView              
                    FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskGrid;

                    try
                    {
                        //Check whether any TextBlock in the ListBox contains the specified string.
                        found = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(recipientName));

                        //If condition is true then execute if block
                        if (found == true)
                        {
                            //Write the Log If verification is Pass
                            Manager.Current.Log.WriteLine(LogType.Information, "Recipient Name " + " " + recipientName + " " + "Exists in a Task Management grid against : " + " " + invoiceNumber + ", Verification Passed");
                        }
                    }

                    catch (AssertException ex)
                    {
                        //Write the Log If verification is Fail
                        throw new Exception("Recipient Name:" + " " + recipientName + " " + "Does NOT Exists in Task Management grid against :" + " " + invoiceNumber + ", Verification Failed!" + ex.Message);
                    }
                }
            }

            else
            {


                if (paymentPlanTaskManagement != null)
                {
                    //Wait for Grid Control Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_WorkFlow_PlansDetails_TaskManagement_TaskGridView.Wait.ForExists(Globals.timeOut);

                    try
                    {
                        //Create a new RadGridView Control                    
                        RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_WorkFlow_PlansDetails_TaskManagement_TaskGridView;

                        bool found = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(recipientName));

                        //Compare Each Row Value
                        if (found == true)
                        {
                            Manager.Current.Log.WriteLine(LogType.Error, "Recipient Name found against: " + invoiceNumber + ". Verification Failed!!");
                        }
                        else
                        {
                            //Write the log if Verification Pass
                            Manager.Current.Log.WriteLine(LogType.Information, " " + invoiceNumber + " found without Recipient Name. Verification Passed!!");
                        }
                    }
                    catch (AssertException ex)
                    {
                        // Write the log if verification is Fail
                        throw new Exception(invoiceNumber + " found with Recipient Name, Verification Failed!" + ex.Message);
                    }

                }

                else
                {
                    //Wait for Grid Control Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskGrid.Wait.ForExists(Globals.timeOut);

                    try
                    {
                        //Create a new RadGridView Control                    
                        FrameworkElement fe1 = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskGrid;

                        bool found = fe1.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(recipientName));

                        //Compare Each Row Value
                        if (found == true)
                        {
                            Manager.Current.Log.WriteLine(LogType.Error, "Recipient Name found against: " + invoiceNumber + ". Verification Failed!!");
                        }
                        else
                        {
                            //Write the log if Verification Pass
                            Manager.Current.Log.WriteLine(LogType.Information, " " + invoiceNumber + " found without Recipient Name. Verification Passed!!");
                        }
                    }
                    catch (AssertException ex)
                    {
                        // Write the log if verification is Fail
                        throw new Exception(invoiceNumber + " found with Recipient Name, Verification Failed!" + ex.Message);
                    }
                }
            }
        }

        //Method to Verify the Collaboration Text on details page
        public void P2PInvoiceAdministration_VerifyCollaborate(string verificationText, string invoiceID)
        {
            //Call the Busy Indicator
            P2PNavigation.CallBusyIndicator();

            //Verify that Collaborate Contains Verification Message                       
            try
            {
                //Verify that Collaboration Message after Collborate Process
                Assert.IsTrue(ArtOfTest.Common.CompareUtils.StringCompare(SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=~ActiveDiscussion")).Text, verificationText, ArtOfTest.Common.StringCompareType.Same) == true);

                //Print the Logs if Collaboration Suceeded
                Manager.Current.Log.WriteLine(LogType.Information + "   Collborate Success: (" + verificationText + ") , against : " + " " + invoiceID);
            }
            catch (AssertException ex)
            {
                //Print the Logs if Colaboration Failed
                throw new Exception(invoiceID + "  is NOT Collborated, Verification Failed!:  " + ex.Message);
            }
        }

        //Method to Verify any column in Task Grid View on Main Page : Common to IA and PP projects : Workflow Silo
        public void P2PWorkFlowVerifyTaskGridViewMainPage(string searchValue, string searchColumn, string invoiceId)
        {
            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Aministration_TaskGridViewMainPage.Wait.ForExists(Globals.timeOut);
            //Create a new RadGridView Control
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Aministration_TaskGridViewMainPage;

            try
            {
                //Check value in Exact Column using Column Header Name Value                                        
                GridViewHeaderCell header = grid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == searchColumn);

                //Check Header value is not Null
                Assert.IsNotNull(header);
                //Count all rows and save it in index varibable 
                int index = header.Index;

                foreach (GridViewRow row in grid.Rows)
                {
                    //Compare Each Row Value
                    if (row.Cells[index].TextBlockContent == searchValue)
                    {
                        //Write the log if Verification Pass
                        Manager.Current.Log.WriteLine(LogType.Information, "Value (" + searchValue + ") exists in Task Grid against: " + invoiceId + " , Verification Passed");
                    }
                    else
                    {
                        //Write the log if Verification Failed
                        throw new Exception("Value (" + searchValue + ") does not exist in Task Grid against: " + invoiceId + " ,Verification Failed!!!!");
                    }
                }
            }
            catch (AssertException ex)
            {
                // Write the log if verification is Fail
                throw new Exception("Header Column: (" + searchColumn + ") not found in Task Grid against: " + invoiceId + " , Verification Failed! " + ex.Message);
            }
        }

        //Method to verify the AdavnceSearchFilters
        public void P2PSearchVerifyAdavnceSearchFilters(string paymentPlanFilters, string headerName)
        {

            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_InvoicesGridViewControl.Wait.ForExists(Globals.timeOut);
            //Use FrameworkElement and read all the TextBlocks 
            var rgv = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_InvoicesGridViewControl;


            //If condition is true then execute  if block
            if (rgv.Rows.Count.Equals(0))
            {

                //Write the log if Verification Fail
                throw new Exception(LogType.Information + " " + "Grid is Empty, Payment Plan Not Found by :-" + " " + paymentPlanFilters + " " + " Verification Failed!!!");
            }

            else
            {
                //Use Exception Handling 
                try
                {
                    //Check value in Exact Column using Column Header Name Value
                    var header = rgv.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == headerName);
                    //Check Header value is not Null
                    Assert.IsNotNull(header);
                    //Count all rows and save it in index varibable 
                    int index = header.Index;
                    //Verify Results in each row in grid
                    foreach (GridViewRow row in rgv.Rows)
                    {
                        //Compare Each Row Value
                        if (row.Cells[index].TextBlockContent != paymentPlanFilters)
                        {
                            //Write the log if Verification Fail
                            throw new Exception("Payment Plan Filters Found by Other Payment Plan Filters Verification Failed!!!!");
                        }
                    }
                    //Write the log if Verification Pass
                    Manager.Current.Log.WriteLine(LogType.Information + " " + "Search Succeed, Payment Plan Found by Payment Plan Filters :" + paymentPlanFilters);
                }
                catch (AssertException ex)
                {
                    // Write the log if verification is Fail
                    throw new Exception("Unable to Find the Column Header:" + " " + headerName + "Unable to Verify!" + ex.Message);
                }
            }
        }

        //Method to verify MandatoryData Validation Hyperlink shows
        public void P2PInvoiceAdmiminstartionMandatoryDataVerification(string verifyValidationError)
        {
            //Wait for Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseItems_ValidationHyperlinkButton.Wait.ForExists(Globals.navigationTimeOut);

            //Getting Text block content in string variable
            string textBlockContent = SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseItems_ValidationHyperlinkButton.TextBlockContent.ToString();

            //Getting Text block content in string variable
            //string validationMessage = textBlockContent.Substring(0, 16);

            // Verify P2P_Purchase_Data_Management_PurchaseItems_ValidationHyperlinkButton's visibility is Visible               
            if (textBlockContent.Contains(verifyValidationError))
            {
                //Write the log if Verification Passes
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Result: " + verifyValidationError + " " + "Actual Result: " + textBlockContent + " Verification Passed!!");
            }
            else
            {
                //Write the log if Verification Fails
                Manager.Current.Log.WriteLine(LogType.Error, "Expected Result: " + verifyValidationError + " " + "Actual Result: " + textBlockContent + " Verification Failed!!");
            }
        }

        //Method to verify MandatoryData Validation Hyperlink shows
        public void P2PVerifyValidationMessageWithMultipleMessages(string verifyValidationError)
        {
            //Use FrameworkElement and read all the TextBlock in a fe
            FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_ValidationSummaryControl;

            //Check the search  result is "No Search Result"
            bool search = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(verifyValidationError));

            // Verify P2P_Purchase_Data_Management_PurchaseItems_ValidationHyperlinkButton's visibility is Visible               
            if (search.Equals(true))
            {
                //Write the log if Verification Passes
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Result: " + verifyValidationError + "exists. Verification Passed!!");
            }
            else
            {
                //Write the log if Verification Fails
                Manager.Current.Log.WriteLine(LogType.Error, "Expected Result: " + verifyValidationError + " doesnt exists. Verification Failed!!");
            }
        }

        //Method to Verify the Invoice Navigation
        public void P2PVerifyInvoiceNavigation(string navigationText)
        {
            //Verify that the Contents of TextBlock are Visible
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_InvoiceDetails_InvoiceNavigationControl.Wait.ForExists(Globals.timeOut);

            //Verify the Navigation Text(Step 2, Verify Message: Invoice 2/5:Ready For Transfer)            
            if (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_InvoiceDetails_InvoiceNavigationControl.TextBlockContent == navigationText)
            {
                // Write the log if verification is Pass
                Manager.Current.Log.WriteLine(LogType.Information, "Navigation Successfully:" + " " + navigationText);
            }
            else
            {
                // Write the log if verification is Fail
                Manager.Current.Log.WriteLine(LogType.Information, "Navigation Failed:" + " " + navigationText);
            }
        }

        //Method to Verify the button is enabled or not
        public void P2PVerifySendToProcessButtonEnabled(bool buttonEnabled)
        {
            bool state = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_SendtoProcess.IsEnabled;

            if (buttonEnabled)
            {
                if (state)
                    // Write the log if verification is Pass
                    Manager.Current.Log.WriteLine(LogType.Information, " Send To Process Button is enabled. Verification Passes.");
                else
                    // Write the log if verification is Fail
                    Manager.Current.Log.WriteLine(LogType.Error, " Send To Process Button is disabled. Verification Failed.");
            }
            else
            {
                if (!state)
                    // Write the log if verification is Pass
                    Manager.Current.Log.WriteLine(LogType.Information, " Send To Process Button is disabled. Verification Passes.");
                else
                    // Write the log if verification is Fail
                    Manager.Current.Log.WriteLine(LogType.Error, " Send To Process Button is enabled. Verification Failed.");
            }
        }

        //Method to Verify that Grid does not contain Invoices 
        public void P2PVerifyGridDoesNotContainMultipleInvoices(string[] arr, string headerCell)
        {
            try
            {
                //Declare testblock to store invoice number column 
                TextBlock invoiceNumberHeaderCell = new TextBlock();

                //Get the invoice grid on page
                FrameworkElement e = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol"));

                //assert that grid is not null
                Assert.IsNotNull(e);

                //Find the Header cell Value as Invoice Number
                invoiceNumberHeaderCell = e.Find.ByTextContent(headerCell).As<TextBlock>();

                foreach (string invoiceNumber in arr)
                {
                    //declare a boolean variable
                    bool result = false;

                    //get the Result
                    result = e.TextLiteralContent.Contains(invoiceNumber);

                    if (result == true)
                    {
                        throw new Exception("Invoices:-" + invoiceNumber + " Found on the Grid.");
                    }

                    else
                    {
                        Manager.Current.Log.WriteLine("Invoices:-" + invoiceNumber + " Does Not Exists in the Grid");
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Header Name Does Not Exists!");
            }
        }

        /*THIS METHOD IS USING IN E2E TEST CASE*/
        //Method to Verify Search Invoice with Status
        public void P2InvoiceAdministration_Search_VerifyInvoiceStatus(string invoiceID, string searchInvoiceStatus, string searchColumn, string buttonText, string searchInvoiceStatus2 = null)
        {
            //Intialize the bool variable to false and used for comparison
            bool search = false;

            //Intialize the bool variable to false and used for comparison
            bool searchStatus = false;

            //Intialize the bool variable to false and used for comparison
            bool searchStatus2 = false;

            //Intialize the bool variable to false and used for comparison
            //bool result = false;

            //Declare and initializing value to an integer variable
            int count = 0;

            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol")).Wait.ForExists(Globals.timeOut);

            //Use FrameworkElement and read all the TextBlock in a fe
            FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol"));

            //Check the search  result is "No Search Result"
            search = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(invoiceID));

            if (search == true)
            {
                //Write the log if Verification Passed
                Manager.Current.Log.WriteLine(LogType.Information, "Invoice Number: '" + invoiceID + "' is found in the Grid. Verification Passed!!");

                do
                {
                    //Wait for Grid Control Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol")).Wait.ForExists(Globals.timeOut);

                    //Declare RadGridView variable
                    RadGridView grid = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol")).CastAs<RadGridView>();

                    //Check value in Exact Column using Column Header Name Value                                        
                    GridViewHeaderCell header = grid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == searchColumn);

                    //Check Header value is not Null
                    Assert.IsNotNull(header);

                    //Count all rows and save it in index varibable 
                    int index = header.Index;

                    foreach (GridViewRow row in grid.Rows)
                    {
                        //Write Log of the Searched Invoice Status
                        Manager.Current.Log.WriteLine(LogType.Information, "Invoice Number: '" + invoiceID + "' is found with status '" + row.Cells[index].TextBlockContent.ToString() + "'.");

                        break;
                    }

                    //Use FrameworkElement and read all the TextBlock in a fe
                    FrameworkElement status = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol"));

                    //Refresh Grid
                    status.Refresh();

                    //Check the search  result is "No Search Result"
                    searchStatus = status.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(searchInvoiceStatus));

                    if (searchInvoiceStatus2 != null)
                    {
                        //Check the Search result for another status
                        searchStatus2 = status.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(searchInvoiceStatus2));
                    }

                    //if (searchInvoiceStatus2 != null)
                    //{
                    //    //Check the Search result for another status
                    //    result = searchStatus2 || searchStatus;
                    //}
                    //else
                    //{
                    //    //Check the Search result for another status
                    //    result = searchStatus;
                    //}

                    //To avoid infinite loop
                    if (count == 10)
                    {
                        //Write Log
                        Manager.Current.Log.WriteLine(LogType.Error + " Breaking Loop. Reason: Count exceeds limit(" + count + ").");
                        throw new Exception("Invoice not found in the Grid.");
                    }

                    //Wait for Refresh Button
                    SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(buttonText).As<TextBlock>().Wait.ForExists(Globals.handleTime);

                    //Click on Refresh Button
                    SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(buttonText).As<TextBlock>().User.Click();

                    //Wait for Refresh Button
                    SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(buttonText).As<TextBlock>().Wait.ForExists(Globals.handleTime);

                    //Calling handle busy indicator
                    P2PNavigation.CallBusyIndicator();

                    //Wait for Grid to Load
                    SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol")).Wait.ForExists(Globals.handleTime);

                    //Increment value of variable count
                    count++;

                } while (searchStatus == false && searchStatus2 == false);

                if (searchStatus == true || searchStatus2 == true)
                {
                    //Write Log
                    Manager.Current.Log.WriteLine(LogType.Information, "Invoice Number: '" + invoiceID + "' found in the Grid with status(" + searchInvoiceStatus + "). Verification Passed!!");
                }
            }

            else
            {
                if (search != true)
                {
                    //Write the log if Verification Fails
                    Manager.Current.Log.WriteLine(LogType.Error, "Invoice Number: '" + invoiceID + "' not found in the Grid. Verification Failed!!");
                    throw new Exception("Invoice not found in the Grid.");
                }
                else
                {
                    //Write the log if Verification Fails
                    Manager.Current.Log.WriteLine(LogType.Error, "Loop breaks at ('" + count + "'). Invoice Number: '" + invoiceID + "' found in the Grid with status(" + searchInvoiceStatus + "). Verification Failed!!");
                    throw new Exception("Invoice not found in the Grid.");
                }
            }
        }

        /*THIS METHOD IS USING IN E2E TEST CASE*/
        //Method to Verify HistoryListBoxVerification: this method can be used for main page and detail page.
        public void P2PInvoiceAdministration_HistoryAndCommentsVerification(string[] verifyHistory, int arrayIndex, string searchHistoryAndComment, string historyAndCommentListBox, string comboBoxItems)
        {
            //Declare bool variable
            bool foundRichTextBlock = false;

            if (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching.IsChecked == true)
            {
                //Write Log if invoice is opened in matching area
                Manager.Current.Log.WriteLine(LogType.Information, " Invoice is opened in 'Matching Area'. Something went wrong. Please Check!");

                //Capturing Screenshot
                Manager.Current.Log.CaptureDesktop(Globals.capturedImageLocation + "verifyHistoryAndCommentError");
            }

            if (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search.IsChecked == true)
            {
                //Wait for Refresh Button
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(searchHistoryAndComment)).Wait.ForExists(Globals.timeOut);

                //Set focus on History and Comments TabItem
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(searchHistoryAndComment)).SetFocus();

                //Click on the History and Comments TabItem
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(searchHistoryAndComment)).User.Click();
            }

            else
            {
                //Wait for Refresh Button
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(historyAndCommentListBox)).Wait.ForExists(Globals.timeOut);

                //Set focus on History and Comments TabItem
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(historyAndCommentListBox)).SetFocus();

                //Click on the History and Comments TabItem
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(historyAndCommentListBox)).User.Click();
            }

            //Calling Busy Indicator
            P2PNavigation.CallBusyIndicator();

            //Checking the value of Combo Box to be clicked
            if (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_FilteringComments_ComboBox.Equals(comboBoxItems))
            {
                //Wait for the History and Comments List Box Item
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsListBox.Wait.ForExists(Globals.handleTime);
            }
            else
            {
                //Select the Comments from the Combo Box
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_FilteringComments_ComboBox.SelectItemByText(true, comboBoxItems, true);

                //Wait for the History and Comments List Box Item
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsListBox.Wait.ForExists(Globals.handleTime);
            }

            //Verify the Comments in History Pop-Up
            FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsListBox;

            //get all history as text blocks and verify
            foreach (string historyAndComment in verifyHistory)
            {
                //Grid refresh
                fe.Refresh();

                //Declaring statusflag
                bool statusFlag = true;

                //Get RichTextBox and Verify the Comments in History Pop-Up
                //Create The Xml Document
                XmlDocument doc = new System.Xml.XmlDocument();

                //intilaise string
                string rtbContents = string.Empty;

                //Check whether any TextBlock contains the specified string.
                foundRichTextBlock = fe.Find.AllByType<ArtOfTest.WebAii.Silverlight.UI.RichTextBox>().Any((tb) =>
                {
                    //Save in string
                    rtbContents = (string)tb.GetProperty(new AutomationProperty("Xaml", typeof(string)));

                    //Load string in the Document
                    doc.LoadXml(rtbContents);
                    return doc.InnerText.Contains(historyAndComment);
                });

                //verify HeaderDataField_PlanName_TextBox contains correct plan name 
                if (foundRichTextBlock.Equals(true))
                {
                    //Log the Results if correct History and Comments exists
                    Manager.Current.Log.WriteLine(LogType.Information, "'" + historyAndComment + "' Found under History and Comments Tab. Verification Passed!!");
                }
                else
                {
                    //Log the Results if History and Comments does not exists
                    Manager.Current.Log.WriteLine(LogType.Error, "'" + historyAndComment + "' Not Found under History and Comments Tab. Verification Failed!!");

                    //Set flag if data mis matches
                    statusFlag = false;
                }

                //Grid refresh
                fe.Refresh();

                if (statusFlag == false)
                {
                    //Log the Error if History and Comment showing incorrect data
                    Manager.Current.Log.WriteLine(LogType.Error, " - History and Comments Details are incorrect");
                }
            }
        }

        //Method to Verify Control State throughout P2P
        public void P2PControlStateVerifications(string controlName, string verifyState = null)
        {
            switch (controlName)
            {

                case "ComboBox":
                    {

                        //Click on the History and Comments Tab.
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsTab.User.Click();
                        //Handle the BusyIndicator(If Any)
                        P2PNavigation.CallBusyIndicator();
                        //get the framework elemen of type Combobox
                        var cb = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=FilteringComboBox", "XamlTag=combobox"));
                        cb.CastAs<ArtOfTest.WebAii.Silverlight.UI.ComboBox>();
                        cb.Wait.ForExists(Globals.timeOut);
                        //Get an Actual State
                        string actualState = cb.ToolTipText;

                        if (actualState == verifyState)
                        {
                            Manager.Current.Log.WriteLine(LogType.Information, "Combobox State:- " + verifyState + ": Matches with the Selected State");
                        }
                        else
                        {
                            Manager.Current.Log.WriteLine(LogType.Error, "Combobox State Expected:- " + verifyState + "Does Not Matches, with Actual State:- " + actualState + " Verification Failed!");
                        }

                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        //Method to verify image is added or not
        public void P2PVerifyImageAdded()
        {
            //Wait for HeaderData TabTabItem Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ImageCount_Textblock.Wait.ForExists(Globals.timeOut);
            //Get Textblock Content
            string imageCount = SharedElement.P2P_Application.SilverlightApp.P2P_ImageCount_Textblock.TextLiteralContent;

            if (string.IsNullOrEmpty(imageCount))
            {
                //Wait for few seconds
                //Initialise counter
                int i = 2000;

                do
                {
                    //Wait for image to  load Properly
                    System.Threading.Thread.Sleep(i);

                    //Increment counter
                    i++;

                    //Get Textblock Content again
                    imageCount = SharedElement.P2P_Application.SilverlightApp.P2P_ImageCount_Textblock.TextLiteralContent;

                    //Check if image count is populated
                    if (!(string.IsNullOrEmpty(imageCount)))
                    {
                        //Log the results if image Exists 
                        Manager.Current.Log.WriteLine(LogType.Information, " Image uploaded completely. Verification Passed!!");
                        break;
                    }

                    //Check counter
                    if (i == 40000)
                    {
                        //Log the results if image does Not load/exisits
                        Manager.Current.Log.WriteLine(LogType.Error, " Image NOT uploaded completely within desired time. Verification Failed!!");
                        break;
                    }
                } while (i < 40000);
            }
            else
            {
                //Log the results if image Exists 
                Manager.Current.Log.WriteLine(LogType.Information, " Image uploaded completely. Verification Passed!!");
            }
        }

        //Method to Verify Selected Supplier Window: Selected Suppliers TextBox
        public void P2PVerifySelectedSuppliersDetailsSupplierTextBox(string expectedSelectedSuppliers)
        {
            //Wait for element to Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SuppliersListGridViewControl.Wait.ForExists(Globals.timeOut);

            //Put the ListBox into a variable.
            FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SuppliersListGridViewControl;
            fe.Refresh();
            //Declare bool value. Check whether any TextBlock in the ListBox contains the specified string.            
            bool suppliersFound = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(expectedSelectedSuppliers));


            ////Get all textblocks as A List
            //var supplierList = fe.Find.AllByType<TextBlock>().Select(y => y.Text).ToList();

            ////Declare variable to store all textblock values.
            //string allValues = " ";
            //foreach (string searchValue in supplierList)
            //{
            //    //Store all supplier textblock's in a string "allvalues"
            //    allValues = allValues + ", " + searchValue;

            //    //Get exact supplier name without the supplier code. ex: "Addwise Oy(BW00)" will return "Addwise Oy"
            //    string actualValue = searchValue.Remove(searchValue.IndexOf("("));

            //    //Compare actual value with expected supplier value
            //    if (actualValue.ToUpper() == expectedSelectedSuppliers.ToUpper())
            //        suppliersFound = true;
            //}
            ////Remove extra space
            //allValues = allValues.Substring(2);

            if (suppliersFound == true)
            {
                //Log the Results if correct string exists
                Manager.Current.Log.WriteLine(LogType.Information, " Selected Suppliers TextBox contains supplier " + expectedSelectedSuppliers + " Verification Passed!!");
            }
            else
            {
                //Log the Results if string does not exists
                Manager.Current.Log.WriteLine(LogType.Error, " Selected Suppliers TextBox does not contain supplier " + expectedSelectedSuppliers + " Verification Failed!!");
            }
        }

        //Method to Verify Selected Supplier Window: Bank Accounts, More Information
        public void P2PVerifySelectedSuppliersDetailsWindow(string supplier, string expectedBankAccounts, string expectedMoreInformation)
        {
            //Put the ListBox into a variable.
            FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_SelectSuppliers_BankAccountAreaScrollViewer;

            //Check whether any TextBlock in the ListBox contains the specified string.
            bool foundBankAccounts = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(expectedBankAccounts));

            //Log the results.
            if (foundBankAccounts == true)
            {
                //Log the Results if correct string exists
                Manager.Current.Log.WriteLine(LogType.Information, " Bank Account number (" + expectedBankAccounts + ") Exists in Suplier picker Pop Window against Supplier:" + supplier + ". Verification Passed!!");
            }
            else
            {
                //Log the Results if string does not exists
                Manager.Current.Log.WriteLine(LogType.Error, " Bank Account number (" + expectedBankAccounts + ") does Not Exist in Suplier picker Pop Window against Supplier:" + supplier + ". Verification Failed !!!");
            }

            //Put the ListBox into a variable.
            FrameworkElement moreInfor = SharedElement.P2P_Application.SilverlightApp.P2P_SelectSuppliers_MoreInfoAreaScrollViewer;

            //Check whether any TextBlock in the ListBox contains the specified string.
            bool foundMoreInfo = moreInfor.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(expectedMoreInformation));

            //Log the results.
            if (foundMoreInfo == true)
            {
                //Log the Results if correct string exists
                Manager.Current.Log.WriteLine(LogType.Information, " More Information (" + expectedMoreInformation + ") Exists in Suplier picker Pop Window against Supplier:" + supplier + ". Verification Passed!!");
            }
            else
            {
                //Log the Results if string does not exists
                Manager.Current.Log.WriteLine(LogType.Error, " More Information (" + expectedMoreInformation + ") does Not Exist in Suplier picker Pop Window against Supplier:" + supplier + ". Verification Failed !!!");
            }
        }

        //Method to Verify Selected Supplier TextBox Value
        public void P2PVerifySelectedSupplierTextBoxValue(string expectedSuppliers)
        {
            //Wait for element to Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierTextBox.Wait.ForExists(Globals.timeOut);

            //Get Element actual screen Value
            string actualSuppliers = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierTextBox.Text.Trim();

            if (actualSuppliers.ToUpper().Contains(expectedSuppliers.ToUpper()))
            {
                //Log the Results if correct string exists
                Manager.Current.Log.WriteLine(LogType.Information, " Supplier's TextBox contains supplier (" + expectedSuppliers + "). Verification Passed!!");

            }
            else
            {
                //Log the Results if string does not exists
                Manager.Current.Log.WriteLine(LogType.Error, " Supplier's TextBox does not contain supplier (" + expectedSuppliers + "). Actual Suppliers showing (" + actualSuppliers + "). Verification Failed!!");
                throw new Exception("Verification Failed.");
            }
        }

        //Method to Verify Favorites Icon
        public void P2PVerifySelectedSuppliersFavoritesTab(string expectedSuppliers)
        {
            //Put the ListBox into a variable.
            FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_SelectedSuppliers_FavoritesGridViewControl;

            //Check whether any TextBlock in the ListBox contains the specified string.
            bool found = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(expectedSuppliers));

            //Log the results.
            if (found == true)
            {
                //Log the Results if correct string exists
                Manager.Current.Log.WriteLine(LogType.Information, " Searched Supplier (" + expectedSuppliers + ") Exists in Suplier picker Pop Window Under Favorites Tab. Verification Passed!!");
            }
            else
            {
                //Log the Results if string does not exists
                Manager.Current.Log.WriteLine(LogType.Error, " Searched Supplier (" + expectedSuppliers + ") doesnt exists in Suplier picker Pop Window Under Favorites Tab. Verification Failed !!!");
            }
        }

        //Method to Verify Favorites Grid is empty in Selected Supplier Window
        public void P2PVerifySelectedSuppliersFavoritesTabGridEmpty()
        {
            //Wait for element to Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_SelectedSuppliers_FavoritesGridViewControl.Wait.ForExists(Globals.timeOut);

            //Get Grid reference
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_SelectedSuppliers_FavoritesGridViewControl;

            if (grid.Rows.Count.Equals(0))
            {
                //Print the Logs if verification passes
                Manager.Current.Log.WriteLine(LogType.Information, " Selected Supplier Window: Favorites Tab: Grid is Empty. Verification Passes!!");
            }
            else
            {
                //Print the Logs in case of failure
                Manager.Current.Log.WriteLine(LogType.Error, " Error: Selected Supplier Window: Favorites Tab: Grid is not empty. Verification Failed!!");
            }
        }

        //Method to Verify Save As Draft Button
        public void P2PVerifySaveAsDraftButton(string verificationCase, string button)
        {
            //Wait for button to load
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(button)).Wait.ForExists(Globals.timeOut);

            //Set focus on button.
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(button)).SetFocus();

            //Get element into Silverlight Button
            ArtOfTest.WebAii.Silverlight.UI.Button buttonVerify = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(button)).CastAs<ArtOfTest.WebAii.Silverlight.UI.Button>();

            switch (verificationCase)
            {
                case "Verify_Button":
                    {
                        //Log the results.
                        if (buttonVerify.IsEnabled == false)
                        {
                            //Log the Results if Button is disabled
                            Manager.Current.Log.WriteLine(LogType.Information, " Invoice is Saved as draft. Verification Passed!!");
                        }
                        else
                        {
                            //Log the Results if Button is enabled
                            Manager.Current.Log.WriteLine(LogType.Error, " Invoice is not Saved as draft. Verification Failed !!");
                            throw new Exception("Verification Failed.");
                        }
                        break;
                    }

                case "Verify_Textbox":
                    {
                        //Log the results.
                        if (buttonVerify.IsEnabled == true || buttonVerify.IsVisible == true)
                        {
                            //Log the Results if Button is disabled
                            Manager.Current.Log.WriteLine(LogType.Information, " Search Textbox is visible. Verification Passed!!");
                        }
                        else
                        {
                            //Log the Results if Button is enabled
                            Manager.Current.Log.WriteLine(LogType.Error, " Search Textbox is not visible. Verification Failed !!");
                            throw new Exception("Verification Failed.");
                        }
                        break;
                    }

                default:
                    {
                        //Log the Results if not case found
                        Manager.Current.Log.WriteLine(LogType.Error, " No Case Found");
                        throw new Exception("No Case Found.");
                    }
            }
        }

        //Method to Verify Add image Upload Dialog Verification
        public void P2PVerifyAddImageUploadDialog(string browseButton)
        {
            //IF() block will execute if dialog remains open
            if (P2P_Utility.fileUploadDialog.Window.ContainsControl(00000001) == false || P2P_Utility.fileUploadDialog.Window.GetWindowText().Contains("Open") == false || P2P_Utility.fileUploadDialog.Window.ContainsText("&Open", true) == false)
            {
                //Write Informative message
                Manager.Current.Log.WriteLine(LogType.Information, "Verification Passed: Image Upload Dialog is not Open.");
            }
            else
            {
                //Write Informative message
                Manager.Current.Log.WriteLine(LogType.Information, "Message: Image Upload Dialog is Open. Handling...!!");

                //Remove the File Upload Dialog Box
                Manager.Current.DialogMonitor.RemoveDialog(P2P_Utility.fileUploadDialog);

                //Set Focus on Unhandled Dialog
                P2P_Utility.fileUploadDialog.Window.SetFocus();

                //Close Unhandled Dialog
                P2P_Utility.fileUploadDialog.Window.Close();

                //Declared a New File Upload Dialog
                FileUploadDialog handledFileUploadDialog = new FileUploadDialog(Manager.Current.ActiveBrowser, P2P_Utility.fileUploadPath, DialogButton.OPEN, "Open");

                //Wait for browse button
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(browseButton)).Wait.ForExists(Globals.timeOut);

                //Click on Browse Button to Browse the Path
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(browseButton)).User.Click();

                //Add the File Upload Dialog Box
                Manager.Current.DialogMonitor.AddDialog(handledFileUploadDialog);

                //Wait to handle Load
                System.Threading.Thread.Sleep(Globals.timeOut);

                //Invoke Dialog Moniter and Handle the Upload Dialog Box
                Manager.Current.DialogMonitor.Start();

                if (handledFileUploadDialog.Window.ContainsControl(00000001) == false || handledFileUploadDialog.Window.GetWindowText().Contains("Open") == false || handledFileUploadDialog.Window.ContainsText("&Open", true) == false)
                {
                    //Write message in Logs
                    Manager.Current.Log.WriteLine(LogType.Information, "Image Upload Dialog is Handled Properly");
                }
                else
                {
                    //Write message in Logs
                    Manager.Current.Log.WriteLine(LogType.Information, "Image Upload Dialog is still open");

                    //Set focus on dialog
                    handledFileUploadDialog.Window.SetFocus();

                    //Clear file path using keyboard action
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Back);

                    //Set File path in Clipboard
                    Clipboard.SetText(P2P_Utility.fileUploadPath);

                    //Wait to handle Load
                    System.Threading.Thread.Sleep(Globals.timeOut);

                    //Enter file path which is stored in clipboard using keyboard action
                    Manager.Current.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+V"));

                    //Using keyboard action & Set Focus on 'Open' button
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab, 2, 2);

                    //Upload File
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);

                    if (handledFileUploadDialog.Window.ContainsControl(00000001) == false || handledFileUploadDialog.Window.GetWindowText().Contains("Open") == false || handledFileUploadDialog.Window.ContainsText("&Open", true) == false)
                    {
                        //Write message in Logs
                        Manager.Current.Log.WriteLine(LogType.Information, "Image Upload Dialog is Handled Properly");
                    }
                    else
                    {
                        //Check file upload dialog & Write Log
                        Manager.Current.Log.WriteLine(LogType.Error, "Image Upload Dialog is not Closed.");
                        throw new Exception("Image Upload Dialog is not Closed.");
                    }
                }
            }
        }

        //Method to Select My Tasks Toolbar Button
        public void P2PVerifyTheSiloExists(string Page)
        {
            switch (Page)
            {
                case "IA_Search":
                    //Wait for P2P_Invoice_Administration_Search Button Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search.Wait.ForExists(Globals.navigationTimeOut);

                    //Click on P2P_Invoice_Administration_Search Button 
                    if (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search.IsEnabled == true)
                    {
                        //Write message in Logs
                        Manager.Current.Log.WriteLine(LogType.Information, "Silo'Page is Exists:" + Page);
                    }
                    else
                    {
                        Manager.Current.Log.WriteLine(LogType.Error, "Silo'Page Does NOT Exists:" + Page);
                    }
                    break;

                default:
                    //Log Failure for Tab item not found
                    Manager.Current.Log.WriteLine(LogType.Error, "Silo Page does not exists, Verification Failed!!!!");
                    break;
            }
        }

        //Method to verify the P2PInvoiceAdministration FrameWorkElements Localization Text Verificationn
        public void P2PInvoiceAdministrationFrameWorkElementsVerification(string invoiceAdministration, string collaborate, string paymentPlan, string dataManagement)
        {

            //Declare the string variable
            string invoiceAdministrationObj, collaborateObj, paymentPlanObj, dataManagementObj;

            //Store the Runtime Values into the above declared variables
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_Invoice_Administration.Wait.ForExists(Globals.timeOut);
            invoiceAdministrationObj = SharedElement.P2P_Application.SilverlightApp.P2P_Main_Invoice_Administration.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=Collaborate Pro", "XamlTag=applicationradiobutton")).Wait.ForExists(Globals.timeOut);
            collaborateObj = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=Collaborate Pro", "XamlTag=applicationradiobutton")).Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Main_PaymentPlans.Wait.ForExists(Globals.timeOut);
            paymentPlanObj = SharedElement.P2P_Application.SilverlightApp.P2P_Main_PaymentPlans.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=applicationradiobutton", "AutomationId=Settings")).Wait.ForExists(Globals.timeOut);
            dataManagementObj = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=applicationradiobutton", "AutomationId=Settings")).Text.ToString();

            //Analytics Localization text required
            //SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer.Wait.ForExists(Globals.timeOut);
            //transferObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer.Text.ToString();

            //If the condition is true then execute if block
            if ((invoiceAdministration == invoiceAdministrationObj) && (collaborate == collaborateObj) & (paymentPlan == paymentPlanObj) && (dataManagement == dataManagementObj))
            {
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Value are :" + invoiceAdministration + " " + collaborate + " " + paymentPlan + " " + dataManagement + " " + " && " + "Actual Value  are :" + invoiceAdministrationObj + " " + collaborateObj + " " + paymentPlanObj + " " + dataManagementObj + "  Verification Passed");
            }

            else
            {
                throw new Exception("Expected Value are :" + invoiceAdministration + " " + collaborate + " " + paymentPlan + " " + dataManagement + " " + " && " + "Actual Value  are :" + invoiceAdministrationObj + " " + collaborateObj + " " + paymentPlanObj + " " + dataManagementObj + "  Verification Failed!!!");
            }

        }

        //Method to verify the ModuleName Localization Text Verification
        public void P2PInvoiceAdministrationModuleNameLocalizationTextVerification(string overview, string received, string matching, string wokrflow, string transfer, string search, string create)
        {
            //Declare the string variable
            string overviewObj, receivedObj, matchingObj, wokrflowObj, transferObj, searchObj, createObj;

            //Store the Runtime Values into the above declared variables
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview.Wait.ForExists(Globals.timeOut);
            overviewObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_Button.Wait.ForExists(Globals.timeOut);
            receivedObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_Button.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching.Wait.ForExists(Globals.timeOut);
            matchingObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_Button.Wait.ForExists(Globals.timeOut);
            wokrflowObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_Button.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer.Wait.ForExists(Globals.timeOut);
            transferObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search.Wait.ForExists(Globals.timeOut);
            searchObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Create.Wait.ForExists(Globals.timeOut);
            createObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Create.Text.ToString();

            //If the condition is true then execute if block
            if ((overview == overviewObj) && (received == receivedObj) & (matching == matchingObj) && (wokrflow == wokrflowObj) && (transfer == transferObj) && (search == searchObj) && (create == createObj))
            {
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Value are :" + overview + " " + received + " " + matching + " " + wokrflow + " " + transfer + " " + search + " " + create + " && " + "Actual Value  are :" + overviewObj + " " + receivedObj + " " + matchingObj + " " + wokrflowObj + " " + transferObj + " " + searchObj + " " + createObj + "  Verification Passed");
            }

            else
            {
                throw new Exception("Expected Value are :" + overview + " " + received + " " + matching + " " + wokrflow + " " + transfer + " " + search + " " + create + " && " + "Actual Value  are :" + overviewObj + " " + receivedObj + " " + matchingObj + " " + wokrflowObj + " " + transferObj + " " + searchObj + " " + createObj + "  Verification Failed!!!");
            }


        }

        //Method to verify the filters localization text verification
        public void P2PInvoiceAdministrationFiltersLocalizationTextVerification(string valid, string invalid, string draft, string returned)
        {
            string validObj, invalidObj, draftObj, returnedObj;

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_ValidCheckBox.Wait.ForExists(Globals.timeOut);
            validObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_ValidCheckBox.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_InvalidCheckBox.Wait.ForExists(Globals.timeOut);
            invalidObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_InvalidCheckBox.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_DraftCheckBox.Wait.ForExists(Globals.timeOut);
            draftObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_DraftCheckBox.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_ReturnedCheckBox.Wait.ForExists(Globals.timeOut);
            returnedObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_ReturnedCheckBox.Text.ToString();

            //If the condition is true then execute if block
            if ((validObj.Contains(valid).Equals(true)) && (invalidObj.Contains(invalid).Equals(true)) & (draftObj.Contains(draft).Equals(true)) && (returnedObj.Contains(returned).Equals(true)))
            {
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Value are :" + valid + " " + invalid + " " + draft + " " + returned + " " + "  && " + "Actual Value  are :" + validObj + " " + invalidObj + " " + draftObj + " " + returnedObj + "  Verification Passed");
            }

            else
            {
                throw new Exception("Expected Value are :" + valid + " " + invalid + " " + draft + " " + returned + " " + "  && " + "Actual Value  are :" + validObj + " " + invalidObj + " " + draftObj + " " + returnedObj + " Verification Failed!!!");
            }
        }

        //Method to verify the ToolBarActions Localization Text Verification
        public void P2PInvoiceAdministrationToolBarActionsLocalizationVerification(string openSelected, string refreshButton, string exportToExcelButton, string invMoreActionsDropdown, string sendToProcess, string prebook)
        {
            //Declare the string variable
            string openSel, refreshBtn, exportToExcel, invMoreActions, sendToProcessObj, prebookObj;

            //Store the Runtime Values into the above declared variables
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_OpenSelectedButton.Wait.ForExists(Globals.timeOut);
            openSel = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_OpenSelectedButton.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_RefreshButton.Wait.ForExists(Globals.timeOut);
            refreshBtn = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_RefreshButton.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ExportToExcelButton.Wait.ForExists(Globals.timeOut);
            exportToExcel = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ExportToExcelButton.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);
            invMoreActions = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_SendtoProcess.Wait.ForExists(Globals.timeOut);
            sendToProcessObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_SendtoProcess.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Aministration_PrebookedButton.Wait.ForExists(Globals.timeOut);
            prebookObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Aministration_PrebookedButton.Text.ToString();

            //If the condition is true then execute if block
            if ((invMoreActionsDropdown == invMoreActions) && (openSelected == openSel) & (refreshButton == refreshBtn) && (exportToExcelButton == exportToExcel) && (sendToProcess == sendToProcessObj) && (prebook == prebookObj))
            {
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Value are :" + openSelected + " " + refreshButton + " " + exportToExcelButton + " " + invMoreActionsDropdown + " " + sendToProcess + " " + prebook + " && " + "Actual Value  are :" + openSel + " " + refreshBtn + " " + exportToExcelButton + " " + invMoreActions + " " + sendToProcessObj + " " + prebookObj + "  Verification Passed");
            }

            else
            {
                throw new Exception("Expected Value are :" + openSelected + " " + refreshButton + " " + exportToExcelButton + " " + invMoreActionsDropdown + " " + sendToProcess + " " + prebook + " && " + "Actual Value  are :" + openSel + " " + refreshBtn + " " + exportToExcelButton + " " + invMoreActions + " " + sendToProcessObj + " " + prebookObj + "  Verification Failed!!!");
            }

        }

        //Method to verify the Details Hyperlink Button Localization Text Verification
        public void P2PInvoiceAdministrationDetailsHyperlinkButtonLocalizationTextVerification(string allComments, string headerData, string coding, string attachment, string history)
        {
            //Declare the string variable
            string allCommentsObj, headerDataObj, codingObj, attachmentObj, historyObj;

            //Store the Runtime Values into the above declared variables
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_AllCommentsButton.Wait.ForExists(Globals.timeOut);
            allCommentsObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_AllCommentsButton.Text.ToString();

            //Store the Runtime Values into the above declared variables
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HeaderDataButton.Wait.ForExists(Globals.timeOut);
            headerDataObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HeaderDataButton.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_CodingButton.Wait.ForExists(Globals.timeOut);
            codingObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_CodingButton.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_AttachmentButton.Wait.ForExists(Globals.timeOut);
            attachmentObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_AttachmentButton.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryButton.Wait.ForExists(Globals.timeOut);
            historyObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryButton.Text.ToString();


            //If the condition is true then execute if block
            if ((allComments == allCommentsObj) && (headerData == headerDataObj) & (coding == codingObj) && (attachment == attachmentObj) && (history == historyObj))
            {
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Value are :" + allComments + " " + headerData + " " + coding + " " + attachment + " " + history + "  && " + "Actual Value  are :" + allCommentsObj + " " + headerDataObj + " " + codingObj + " " + attachmentObj + " " + headerDataObj + "  Verification Passed");
            }

            else
            {
                throw new Exception("Expected Value are :" + allComments + " " + headerData + " " + coding + " " + attachment + " " + history + "  && " + "Actual Value  are :" + allCommentsObj + " " + headerDataObj + " " + codingObj + " " + attachmentObj + " " + headerDataObj + " Verification Failed!!!");
            }
        }

        //Method to verify the Details ToolBar Button Localization Text Verification
        public void P2PInvoiceAdministrationDetailsToolBarLocalizationTextVerification(string back, string sendToProcess, string collaborate, string invMoreAction)
        {
            //Declare the string variable
            string backObj, sendToProcessObj, collaborateObj, invMoreActionObj;

            //Store the Runtime Values into the above declared variables
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_BackButton.Wait.ForExists(Globals.timeOut);
            backObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_BackButton.Text.ToString();

            //Store the Runtime Values into the above declared variables
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_SendtoProcess.Wait.ForExists(Globals.timeOut);
            sendToProcessObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_SendtoProcess.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CollaborateButton.Wait.ForExists(Globals.timeOut);
            collaborateObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CollaborateButton.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);
            invMoreActionObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Text.ToString();



            //If the condition is true then execute if block
            if ((back == backObj) && (sendToProcess == sendToProcessObj) & (collaborate == collaborateObj) && (invMoreAction == invMoreActionObj))
            {
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Value are :" + back + " " + sendToProcess + " " + collaborate + " " + invMoreAction + " " + "  && " + "Actual Value  are :" + backObj + " " + sendToProcessObj + " " + collaborateObj + " " + invMoreActionObj + "  Verification Passed");
            }

            else
            {
                throw new Exception("Expected Value are :" + back + " " + sendToProcess + " " + collaborate + " " + invMoreAction + " " + "  && " + "Actual Value  are :" + backObj + " " + sendToProcessObj + " " + collaborateObj + " " + invMoreActionObj + " Verification Failed!!!");
            }
        }

        //Method to verify the TabItems Localization Text Verification
        public void P2PInvoiceAdministrationDetailsTabItemsLocalizationTextVerification(string headerData, string history, string attachment, string taskManagement)
        {
            //Declare the string variable
            string headerDataObj, historyObj, attachmentObj, taskManagementObj;

            //Store the Runtime Values into the above declared variables
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.Wait.ForExists(Globals.timeOut);
            headerDataObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.Text.ToString();

            //Store the Runtime Values into the above declared variables
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsTab.Wait.ForExists(Globals.timeOut);
            historyObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsTab.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_AttachmentsTabItem.Wait.ForExists(Globals.timeOut);
            attachmentObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_AttachmentsTabItem.Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagementTabTabitem.Wait.ForExists(Globals.timeOut);
            taskManagementObj = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagementTabTabitem.Text.ToString();

            //If the condition is true then execute if block
            if ((headerData == headerDataObj) && (historyObj.Contains(history).Equals(true)) & (attachmentObj.Contains(attachment).Equals(true)) && (taskManagement == taskManagementObj))
            {
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Value are :" + headerData + " " + history + " " + attachment + " " + taskManagement + " " + "  && " + "Actual Value  are :" + headerDataObj + " " + historyObj + " " + attachmentObj + " " + taskManagementObj + "  Verification Passed");
            }

            else
            {
                throw new Exception("Expected Value are :" + headerData + " " + history + " " + attachment + " " + taskManagement + " " + "  && " + "Actual Value  are :" + headerDataObj + " " + historyObj + " " + attachmentObj + " " + taskManagementObj + " Verification Failed!!!");
            }
        }

        //Method to verify the Labels Localization Text Verification
        public void P2PInvoiceAdministrationLabelsLocalizationTextVerification(string OrganizationFilter, string filters, string creationDate, string searchText, string lastComment, string invoiceDetails)
        {
            //Create an object for a Framework Element
            ArtOfTest.WebAii.Silverlight.UI.Panel frameworkElement = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=grid", "name=SearchPage", "|", "XamlPath=/border[0]/grid[0]/wrappanel[0]")).CastAs<ArtOfTest.WebAii.Silverlight.UI.Panel>();

            //Find the parameters value exist in the Framework Element
            bool OrgFilterObj = frameworkElement.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(OrganizationFilter));
            bool filterObj = frameworkElement.Find.AllByType<TextBlock>().Any((tb1) => tb1.Text.Contains(filters));
            bool creationDateObj = frameworkElement.Find.AllByType<TextBlock>().Any((tb2) => tb2.Text.Contains(creationDate));
            bool searchTextObj = frameworkElement.Find.AllByType<TextBlock>().Any((tb3) => tb3.Text.Contains(searchText));

            //If the condition is true then execute if block          
            if ((OrgFilterObj == true) && (filterObj == true) && (creationDateObj == true) && (searchTextObj == true))
            {
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Value are :" + OrganizationFilter + " " + filters + " " + creationDate + " " + searchText + " Found , Verification Passed");
            }
            else
            {
                throw new Exception("Expected Value are :" + OrganizationFilter + " " + filters + " " + creationDate + " " + searchText + " Does NOT Found , Verification Passed");
            }

            //Create an object for a commentUserControl and invoiceDetailsUserControl
            ArtOfTest.WebAii.Silverlight.UI.UserControl commentUserControl = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("Name=LastCommentContainer", "XamlTag=lastcommentcontrol")).CastAs<ArtOfTest.WebAii.Silverlight.UI.UserControl>();
            ArtOfTest.WebAii.Silverlight.UI.UserControl invoiceDetailsUserControl = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("Name=InvoiceDetailsPanel", "XamlTag=invoicedetailspanelcontrol")).CastAs<ArtOfTest.WebAii.Silverlight.UI.UserControl>();

            //Find the parameters value exist in the commentUserControl and invoiceDetailsUserControl
            bool lastCommentObj = commentUserControl.Find.AllByType<TextBlock>().Any((tb4) => tb4.Text.Contains(lastComment));
            bool invoiceDetailsObj = invoiceDetailsUserControl.Find.AllByType<TextBlock>().Any((tb5) => tb5.Text.Contains(invoiceDetails));

            //If the condition is true then execute if block          
            if ((lastCommentObj == true) && (invoiceDetailsObj == true))
            {
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Value are :" + lastComment + " " + invoiceDetails + " Found , Verification Passed");
            }
            else
            {
                throw new Exception("Expected Value are :" + lastComment + " " + invoiceDetails + " Does NOT Found , Verification Passed");
            }
        }

        //Method to verify the ColumnsName Localization Text Verification
        public void P2PInvoiceAdministrationColumnsNameLocalizationTextVerification(string subStatus, string companyCode, string companyName, string supplierName, string invoiceNumber, string invoiceTypeCode, string purchaseOrderNumber, string invoiceDate, string currencyCode, string grossTotal, string grosssToalCompany, string paymentTermCode, string panelText, string totalCount, string show_hide_Columns)
        {
            //Create an object for a Framework Element
            RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=ReceivedGridView", "XamlTag=baslistviewgridcontrol")).CastAs<RadGridView>();

            //Find the parameters value exist in the RadGridView
            bool subStatusObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(subStatus));
            bool companyCodeObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(companyCode));
            bool companyNameObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(companyName));
            bool supplierNameObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(supplierName));
            bool invoiceNumberObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(invoiceNumber));
            bool invoiceTypeCodeObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(invoiceTypeCode));
            bool purchaseOrderNumberObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(purchaseOrderNumber));
            bool invoiceDateObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(invoiceDate));
            bool currencyCodeObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(currencyCode));
            bool grossTotalObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(grossTotal));
            bool grosssToalCompanyObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(grosssToalCompany));
            bool paymentTermCodeObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(paymentTermCode));
            bool panelTextObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(panelText));
            bool totalCountObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(totalCount));
            bool show_hide_ColumnsObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(show_hide_Columns));

            //If the condition is true then execute if block
            if ((subStatusObj == true) && (companyCodeObj == true) && (companyNameObj == true) && (supplierNameObj == true) && (invoiceNumberObj == true) && (invoiceTypeCodeObj == true) && (purchaseOrderNumberObj == true) && (invoiceDateObj == true) && (currencyCodeObj == true) && (grossTotalObj == true) && (paymentTermCodeObj == true) && (panelTextObj == true) && (totalCountObj == true) && (show_hide_ColumnsObj == true))
            {
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Value are :" + subStatus + " " + companyCode + " " + companyName + " " + supplierName + " " + invoiceNumber + " " + invoiceTypeCode + " " + purchaseOrderNumber + " " + invoiceDate + " " + currencyCode + " " + grossTotal + " " + grosssToalCompany + " " + paymentTermCode + " " + panelText + " " + totalCount + " " + show_hide_Columns + " Found, Verification Passed");
            }

            else
            {
                throw new Exception("Expected Value are :" + subStatus + " " + companyCode + " " + companyName + " " + supplierName + " " + invoiceNumber + " " + invoiceTypeCode + " " + purchaseOrderNumber + " " + invoiceDate + " " + currencyCode + " " + grossTotal + " " + grosssToalCompany + " " + paymentTermCode + " " + panelText + " " + totalCount + " " + show_hide_Columns + " Does Not Found , Verification Failed!!!");
            }
        }

        //Method to verify the SelectOrganizationUnit Localization Text Verification
        public void P2PInvoiceAdministrationSelectOrganizationUnitTextVerification(string orgUnitTitle, string orgUnit, string okButton, string cancelButton)
        {
            //wait for SelectOrganizationUnit button exists in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=LaunchOrganizationDialog", "XamlTag=button")).Wait.ForExists(Globals.timeOut);
            //Set focus on the SelectOrganizationUnit 
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=LaunchOrganizationDialog", "XamlTag=button")).SetFocus();
            //Click on SelectOrganizationUnit button
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=LaunchOrganizationDialog", "XamlTag=button")).User.Click(MouseClickType.LeftDoubleClick);

            //Wait for Dailog visisble on UI 
            P2PNavigation.CallBusyIndicator();

            //wait for RadWindow button exists in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlPath=/canvas[0]/browserwindowpresenter[0]/multiorganizationselection[0]")).CastAs<Telerik.WebAii.Controls.Xaml.RadWindow>().Wait.ForExists(Globals.timeOut);
            //Create an object for RadWindow
            Telerik.WebAii.Controls.Xaml.RadWindow OrgUnit = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlPath=/canvas[0]/browserwindowpresenter[0]/multiorganizationselection[0]")).CastAs<Telerik.WebAii.Controls.Xaml.RadWindow>();

            //Find the parameters value exist in the RadWindow
            bool orgUnitTitleObj = OrgUnit.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(orgUnitTitle));
            bool orgUnitObj = OrgUnit.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(orgUnit));
            bool okButtonObj = OrgUnit.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(okButton));
            bool cancelButtonObj = OrgUnit.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(cancelButton));


            //If the condition is true then execute if block
            if ((orgUnitTitleObj == true) && (orgUnitObj == true) && (okButtonObj == true) && (cancelButtonObj == true))
            {
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Value are :" + orgUnitTitle + " " + orgUnit + " " + okButton + " " + cancelButton + " Found, Verification Passed");
            }

            else
            {
                throw new Exception("Expected Value are :" + orgUnitTitle + " " + orgUnit + " " + okButton + " " + cancelButton + " Does Not Found , Verification Failed!!!");
            }

            //wait for Cancel button exists in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=CancelButton", "XamlTag=button")).Wait.ForExists(Globals.timeOut);
            //Click on Cancel button
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=CancelButton", "XamlTag=button")).User.Click();
        }

        //Method to verfiy the HeaderDataFeilds localization Text Verification
        public void P2PInvoiceAdministrationHeaderDataFeildsLocalizationTextVerification(string selectInvoiceType, string organizatinUnit, string invoiceType, string supplierCode, string supplierName, string supplierBank, string supplierbankIban, string invoiceTypeCode, string invoiceNumber, string refrencePerson, string invoiceDate, string baseDate, string currencyCode, string refrenceNumber, string exchangeRateCompany, string grossTotal, string grossTotalCompany, string grossTotalOrg, string netTotal, string netTotalCompany, string taxSum, string dueDate, string planReference, string purchaseOrderNumber, string paymentTermCode, string paymentTermName, string cashPercent, string cashDate, string cashSum, string paymentMethod, string exchangeRatebasedate)
        {
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.Wait.ForExists(Globals.timeOut);
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.User.Click();

            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=tabControl", "XamlTag=tabcontrol")).Wait.ForExists(Globals.timeOut);
            ArtOfTest.WebAii.Silverlight.UI.TabControl tabControl = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=tabControl", "XamlTag=tabcontrol")).CastAs<ArtOfTest.WebAii.Silverlight.UI.TabControl>();

            bool selectInvoiceTypeObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(selectInvoiceType));
            bool organizatinUnitObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(organizatinUnit));
            bool invoiceTypeObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(invoiceType));
            bool supplierCodeObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(supplierCode));
            bool supplierNameObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(supplierName));
            bool supplierBankObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(supplierBank));
            bool supplierbankIbanObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(supplierbankIban));
            bool invoiceTypeCodeObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(invoiceTypeCode));
            bool invoiceNumberObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(invoiceNumber));
            bool refrencePersonObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(refrencePerson));
            bool baseDateObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(baseDate));

            bool currencyCodeObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(currencyCode));
            bool refrenceNumberObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(refrenceNumber));
            bool invoiceDateObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(invoiceDate));
            bool exchangeRateObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(exchangeRateCompany));
            bool grossTotalObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(grossTotal));
            bool netTotalObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(netTotal));
            bool grossTotalCompanyObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(grossTotalCompany));
            bool grossTotalOrgObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(grossTotalOrg));
            bool netTotalCompanyObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(netTotalCompany));
            bool taxSumObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(taxSum));
            bool dueDateObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(dueDate));

            bool planReferenceObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(planReference));
            bool purchaseOrderNumberObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(purchaseOrderNumber));
            bool paymentTermCodeObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(paymentTermCode));
            bool paymentTermNameObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(paymentTermName));
            bool cashPercentObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(cashPercent));
            bool cashDateObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(cashDate));
            bool cashSumObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(cashSum));
            bool paymentMethodObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(paymentMethod));
            bool exchangeRatebasedateObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(exchangeRatebasedate));

            //If the condition is true then execute if block
            if ((selectInvoiceTypeObj == true) && (organizatinUnitObj == true) && (invoiceTypeObj == true) && (supplierCodeObj == true) && (supplierNameObj == true) && (supplierBankObj == true) && (supplierbankIbanObj == true) && (invoiceTypeCodeObj == true) && (invoiceNumberObj == true) && (refrencePersonObj == true) && (invoiceDateObj == true) && (baseDateObj == true) && (currencyCodeObj == true) && (refrenceNumberObj == true) && (exchangeRateObj == true) && (grossTotalObj == true) && (grossTotalCompanyObj == true) && (grossTotalOrgObj == true) && (netTotalObj == true) && (netTotalCompanyObj == true) && (taxSumObj == true) && (dueDateObj == true) && (planReferenceObj == true) && (purchaseOrderNumberObj == true) && (paymentTermCodeObj == true) && (paymentTermNameObj == true) && (cashPercentObj == true) && (cashDateObj == true) && (cashSumObj == true) && (paymentMethodObj == true) && (exchangeRatebasedateObj == true))
            {
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Value are :" + selectInvoiceType + " " + organizatinUnit + " " + invoiceType + " " + supplierCode + " " + supplierName + " " + supplierBank + " " + supplierbankIban + " " + invoiceTypeCode + " " + invoiceNumber + " " + refrencePerson + " " + invoiceDateObj + " " + invoiceDate + " " + baseDate + " " + currencyCode + " " + refrenceNumber + " " + exchangeRateCompany + " " + grossTotal + " " + grossTotalCompany + " " + grossTotalOrg + " " + netTotal + " " + netTotalCompany + " " + taxSum + " " + dueDate + " " + planReference + " " + purchaseOrderNumber + " " + paymentTermCode + " " + paymentTermName + " " + cashPercent + " " + cashDate + " " + cashSum + " " + paymentMethod + " " + exchangeRatebasedate + " Found, Verification Passed");
            }

            else
            {
                throw new Exception("Expected Value are :" + selectInvoiceType + " " + organizatinUnit + " " + invoiceType + " " + supplierCode + " " + supplierName + " " + supplierBank + " " + supplierbankIban + " " + invoiceTypeCode + " " + invoiceNumber + " " + refrencePerson + " " + invoiceDate + " " + baseDate + " " + currencyCode + " " + refrenceNumber + " " + exchangeRateCompany + " " + grossTotal + " " + grossTotalCompany + " " + grossTotalOrg + " " + netTotal + " " + netTotalCompany + " " + taxSum + " " + dueDate + " " + planReference + " " + purchaseOrderNumber + " " + paymentTermCode + " " + paymentTermName + " " + cashPercent + " " + cashDate + " " + cashSum + " " + paymentMethod + " " + exchangeRatebasedate + " Does Not Found , Verification Failed!!!");
            }



        }

        //Method to verfiy the AttachmentFeilds localization Text Verification
        public void P2PInvoiceAdministrationAttachmentFeildsLocalizationTextVerification(string noAttachmentText, string filePath, string addAttachmentTitle, string file, string total, string description, string descriptionText, string ok, string cancel)
        {
            //Wait for Attachment tab items exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_AttachmentsTabItem.Wait.ForExists(Globals.timeOut);
            //Click on Attachment tab items
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_AttachmentsTabItem.User.Click();

            //Wait for tabControl exists in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=tabControl", "XamlTag=tabcontrol")).Wait.ForExists(Globals.timeOut);
            //Create an object for tabControl
            ArtOfTest.WebAii.Silverlight.UI.TabControl tabControl = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=tabControl", "XamlTag=tabcontrol")).CastAs<ArtOfTest.WebAii.Silverlight.UI.TabControl>();

            //Find the parameters value exist in the RadWindow
            bool noAttachmentTextObj = tabControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(noAttachmentText));
            //If the condition is true then execute if block
            if (noAttachmentTextObj == true)
            {
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Value are :" + noAttachmentText + " Found, Verification Passed");
            }

            else
            {
                throw new Exception("Expected Value are :" + noAttachmentText + " Does Not Found , Verification Failed!!!");
            }


            //Wait for Add Attachment Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddButton.Wait.ForExists(Globals.timeOut);

            //setFocus on Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddButton.SetFocus();

            //Click on Attachment Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddButton.User.Click();

            //Wait for OK button exists in DOM
            P2PNavigation.CallBusyIndicator();

            //Wait for dialog to open
            System.Threading.Thread.Sleep(Globals.pause);

            // Using If Condition for Execution in different Browser (Chrome, Firefox etc)
            if (Manager.Current.ActiveBrowser.BrowserType.Equals(1))
            {
                // Wait for Browse Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddAttachment_BrowseButton.Wait.ForExists(Globals.timeOut);

                //Click on Browse Button to Browse the Path 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddAttachment_BrowseButton.User.Click();

                //Type the Path for Saving the File
                Manager.Current.Desktop.KeyBoard.TypeText(filePath, 70);

                //Click on Enter Button
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);
            }
            else
            {
                try
                {
                    //Initialize the FileUpload Dialog
                    FileUploadDialog fileDialog = new FileUploadDialog(Manager.Current.ActiveBrowser, filePath, DialogButton.OPEN, "Open");

                    //Add the File Dialog
                    Manager.Current.DialogMonitor.AddDialog(fileDialog);

                    //Start the DialogMonitor
                    Manager.Current.DialogMonitor.Start();

                    //Wait for Browse Button Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddAttachment_BrowseButton.Wait.ForExists(Globals.timeOut);

                    //Click on Browse Button to Browse the Path 
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddAttachment_BrowseButton.User.Click();

                    //System wait
                    System.Threading.Thread.Sleep(Globals.pause);
                }
                catch (System.IO.IOException e)
                {
                    //Give the error message in case File Upload Dialog not handled
                    throw new Exception("File Upload Dialog is Not Handled:" + " : " + e);
                }
            }

            //Wait for AddComment_TextBox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);

            // Set Focus on Description Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.SetFocus();

            //Enter the Text in Description TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(descriptionText, 80);

            //Wait for RadWindow button exists in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlPath=/canvas[0]/browserwindowpresenter[0]/singlefileupload[0]")).Wait.ForExists(Globals.timeOut);
            //Create an object for RadWindow
            Telerik.WebAii.Controls.Xaml.RadWindow radWindow = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlPath=/canvas[0]/browserwindowpresenter[0]/singlefileupload[0]")).CastAs<Telerik.WebAii.Controls.Xaml.RadWindow>();

            //Find the parameters value exist in the RadWindow
            bool addAttachmentTitleObj = radWindow.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(addAttachmentTitle));
            bool fileObj = radWindow.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(file));
            bool totalObj = radWindow.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(total));
            bool descriptionObj = radWindow.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(description));
            bool okObj = radWindow.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(ok));
            bool cancelObj = radWindow.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(cancel));

            //If the condition is true then execute if block
            if ((addAttachmentTitleObj == true) && (fileObj == true) && (totalObj == true) && (descriptionObj == true) && (okObj == true) && (cancelObj == true))
            {
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Value are :" + addAttachmentTitle + " " + file + " " + addAttachmentTitle + " " + total + " " + description + " " + ok + " " + cancel + " Found, Verification Passed");
            }

            else
            {
                throw new Exception("Expected Value are :" + addAttachmentTitle + " " + file + " " + addAttachmentTitle + " " + total + " " + description + " " + ok + " " + cancel + "  Does Not Found , Verification Failed!!!");
            }


            //Wait for OK button to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK button on Add Attachment Dialog Box 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_OKButton.User.Click(MouseClickType.LeftDoubleClick);

            //System wait
            System.Threading.Thread.Sleep(Globals.timeOut);

        }

        //Method to verfiy the CodingRow localization Text Verification
        public void P2PInvoiceAdministrationCodingRowLocalizationTextVerification(string codingRowTabItem, string invoiceRowTabItem, string costCenterCode, string costCenterName, string accountCode, string accountName, string netTotal, string netTotalCompany, string netTotalOrganization, string grossTotal, string taxCode, string taxPercent, string taxSum, string taxOverride, string lastComment)
        {            
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=tabControlRows", "XamlTag=tabcontrol")).Wait.ForExists();
            string codingRowTabItemObj = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=tabControlRows", "XamlTag=tabcontrol")).Text.ToString();

            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=invoiceRowsTab", "XamlTag=tabitem")).Wait.ForExists();
            string invoiceRowTabItemObj = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=invoiceRowsTab", "XamlTag=tabitem")).Text.ToString();

            //If the condition is true then execute if block
            if ((codingRowTabItem == codingRowTabItemObj) && (invoiceRowTabItem == invoiceRowTabItemObj))
            {
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Value are :" + codingRowTabItem + " " + invoiceRowTabItem + " " + "&& Actual Values are :" + codingRowTabItem + " " + invoiceRowTabItem + "  Verification Passed");
            }

            else
            {
                throw new Exception("Expected Value are :" + codingRowTabItem + " " + invoiceRowTabItem + " && Actual Values are :" + codingRowTabItem + " " + invoiceRowTabItem + " " + "  Verification Failed!!!");
            }


            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=codingRowTab", "XamlTag=tabitem")).Wait.ForExists(Globals.timeOut);
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=codingRowTab", "XamlTag=tabitem")).User.Click();

            System.Threading.Thread.Sleep(Globals.timeOut);
            
            //Wait for tabControl exists in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=CodingGridControl", "XamlTag=codinggridcontrol")).Wait.ForExists(Globals.timeOut);
            //Create an object for tabControl
            Telerik.WebAii.Controls.Xaml.RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=CodingGridControl", "XamlTag=codinggridcontrol")).CastAs<Telerik.WebAii.Controls.Xaml.RadGridView>();

            bool costCenterCodeObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(costCenterCode));
            bool costCenterNameObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(costCenterName));
            bool accountCodeObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(accountCode));
            bool accountNameObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(accountName));
            bool netTotalObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(netTotal));
            bool netTotalCompanyObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(netTotalCompany));

            bool netTotalOrganizationObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(netTotalOrganization));
            bool grossTotalObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(grossTotal));

            //bool taxCodeObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(taxCode));
            //bool taxPercentObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(taxPercent));
            //bool taxSumObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(taxSum));
            //bool taxOverrideObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(taxOverride));
            //bool lastCommentObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(lastComment));

            //If the condition is true then execute if block
            //if ((costCenterCodeObj == true) && (costCenterNameObj == true) && (accountCodeObj == true) && (accountNameObj == true) && (netTotalObj == true) && (netTotalCompanyObj == true) && (netTotalOrganizationObj == true) && (grossTotalObj == true) && (taxCodeObj == true) && (taxPercentObj == true) && (taxSumObj == true) && (taxOverrideObj == true) && (lastCommentObj == true))
            if ((costCenterCodeObj == true) && (costCenterNameObj == true) && (accountCodeObj == true) && (accountNameObj == true) && (netTotalObj == true) && (netTotalCompanyObj == true) && (netTotalOrganizationObj == true) && (grossTotalObj == true))
            {
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Value are :" + costCenterCode+ " " + costCenterName + " " + accountCode + " " + accountName + " " + netTotal + " " + netTotalCompany + " " + netTotalOrganization + " " + grossTotal + " " + " Found, Verification Passed");
            }

            else
            {
                throw new Exception("Expected Value are :" + costCenterCode + " " + costCenterName + " " + accountCode + " " + accountName + " " + netTotal + " " + netTotalCompany + " " + netTotalOrganization + " " + grossTotal + " " + " Does Not Found , Verification Failed!!!");
            }            
        }

        public void P2PInvoiceAdministrationInvoiceLinesLocalizationTextVerification(string purchaseOrderNumber, string netPrice, string netTotal, string productCode, string quantity,string quantityUnit,string noInvoiceLine)
        {
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=invoiceRowsTab", "XamlTag=tabitem")).Wait.ForExists(Globals.timeOut);
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=invoiceRowsTab", "XamlTag=tabitem")).User.Click();

            P2PNavigation.CallBusyIndicator();

            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=InvoiceLinesGrid", "XamlTag=listviewgridcontrol")).Wait.ForExists(Globals.timeOut); ;
            Telerik.WebAii.Controls.Xaml.RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=InvoiceLinesGrid", "XamlTag=listviewgridcontrol")).CastAs<Telerik.WebAii.Controls.Xaml.RadGridView>();

            bool purchaseOrderNumberObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(purchaseOrderNumber));
            bool netPriceObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(netPrice));
            bool netTotalObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(netTotal));
            bool productCodeObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(productCode));
            bool quantityObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(quantity));
            bool quantityUnitObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(quantityUnit));
            bool noInvoiceLineObj = rgv.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(noInvoiceLine));


            if ((purchaseOrderNumberObj == true) && (netPriceObj == true) && (netTotalObj == true) && (productCodeObj == true) && (quantityObj == true) && (quantityUnitObj == true) && (noInvoiceLineObj == true))
            {
                Manager.Current.Log.WriteLine(LogType.Information, "Expected Value are :" + purchaseOrderNumber + " " + netPrice + " " + netTotal + " " + productCode + " " + netTotalObj + " " + quantity + " " + quantityUnit + " " + noInvoiceLine + " " + " Found, Verification Passed");
            }

            else
            {
                throw new Exception("Expected Value are :" + purchaseOrderNumber + " " + netPrice + " " + netTotal + " " + productCode + " " + netTotalObj + " " + quantity + " " + quantityUnit + " " + noInvoiceLine + " Does Not Found , Verification Failed!!!");
            }
        }
              
        //Method to Verify the upload dialog doesnt show for add attachment
        public void P2PVerifyDialog()
        {
            try
            {
                //Wait for 30 secs ( or 1/2 min) for the radwindow to close on screen
                SharedElement.P2P_Application.SilverlightApp.Get<Telerik.WebAii.Controls.Xaml.RadWindow>(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlPath=/canvas[0]/browserwindowpresenter[0]/singlefileupload[0]"), false, 0).Wait.ForExistsNot(30000);

                //Assert no radwindow shows on screen
                Assert.IsNull(SharedElement.P2P_Application.SilverlightApp.Find.ByBaseType<IRadWindow>());
            }
            catch (AssertException ex)
            {
                Manager.Current.Log.WriteLine(LogType.Information, "Upload Dialog doesnt close in time. " + ex.Message);
            }

            ////Initialise counter
            //int i = 2000;

            //int childCount= Manager.Current.ActiveBrowser.ContentWindow.AllChildren.Count;

            //Telerik.WebAii.Controls.Xaml.RadWindow RadWinCheck = new Telerik.WebAii.Controls.Xaml.RadWindow();

            ////FrameworkElement RadWinCheck = SharedElement.P2P_Application.SilverlightApp.Find.ByBaseType<Telerik.WebAii.Controls.Xaml.RadWindow>;
            ////Wait for browse button
            ////SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(browseButton)).Wait.ForExists(Globals.timeOut);

            //while(RadWinCheck.IsActiveWindow)
            //{
            //    //wait for few milliseconds
            //    System.Threading.Thread.Sleep(i);

            //    //Increment counter
            //    i++;

            //    //Check counter
            //    if (i == 40000)
            //    {
            //        //Log the results if image does Not load/exisits within desired time
            //        Manager.Current.Log.WriteLine(LogType.Information, " Image NOT uploaded completely within desired time. Verification Failed!!");
            //        break;
            //    }
            //}  
        }
    }
}
