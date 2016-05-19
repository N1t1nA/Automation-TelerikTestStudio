using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using Telerik.WebAii.Controls.Xaml;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.Common.Exceptions;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Controls.Xaml.Wpf;

namespace P2P.Testing.Shared.Class.Purchase
{
    public class P2PPurchaseVerification : BaseWebAiiTest
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

        //Method to verify My Tasks Purchase Requisitions Free Text Search 
        public void P2PMyTasksPurchaseRequisitionsVerifyFreeTextSearch(string headerName, int iteration, string searchText)
        {
            //This bool value used for "Exit from For Loop "
            bool pageFound = false;

            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_RadGridViewcontrol.Wait.ForExists(Globals.timeOut);

            //Create a new RadGridView Control
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_RadGridViewcontrol;

            //Check value in Exact Column using Column Header Name Value
            GridViewHeaderCell header = grid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == headerName);

            //If condition is true then execute  if block
            if (grid.Rows.Count.Equals(0))
            {
                //Execute for loop for search the invoice                
                for (int i = 0; i < iteration; i++)
                {
                    //Wait for Search Textbox to Exists in DOM Tree
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Wait.ForExists(Globals.timeOut);

                    //Clear Search Textbox
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Text = string.Empty;

                    //Wait for Search Button to Exists in DOM Tree
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.Wait.ForExists(Globals.timeOut);

                    //Click on search button
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();

                    //Wait for Search Textbox to Exists in DOM Tree
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Wait.ForExists(Globals.timeOut);

                    //Enter the Search Term to search for invoice
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Text = searchText;

                    //Click on Search button 
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();

                    //Wait for Search Grid View to Exists in DOM Tree
                    SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_RadGridViewcontrol.Wait.ForExists(Globals.timeOut);

                    //Create a new RadGridView Control
                    RadGridView newgrid = SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_RadGridViewcontrol;

                    //Condition For Exis from the Loop
                    if (newgrid.Rows.Count.Equals(0) == false)
                    {
                        pageFound = true;
                        break;
                    }
                }
            }

            else
            {
                pageFound = true;
            }

            //Purchase Requisitions found true then if block code execute
            if (pageFound == true)
            {
                //Count all rows and save it in index variable
                int index = header.Index;

                //Verify Results in each row in grid
                foreach (GridViewRow row in grid.Rows)
                {
                    //Compare Each Row Value
                    if (row.Cells[index].TextBlockContent != searchText)
                    {
                        //Write the log if Verification Fail
                        Manager.Current.Log.WriteLine(LogType.Error, " Purchase Requisitions Found  by Other Name Verification Failed!!!!");
                    }
                }

                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, " Search Succeeded, Purchase Requisitions Found by Text Search: " + searchText);
            }
            else
            {
                //Write the log if Verification Fail
                Manager.Current.Log.WriteLine(LogType.Error, " Grid is empty, text : " + " " + searchText + " " + " is Not found");
            }
        }

        //Method to verify Purchase Requisitions Free Text Search (Personal Mode search)
        public void P2PSearchPurchaseRequisitionsVerifyFreeTextSearch(string headerName, int iteration, string searchText)
        {
            //This bool value used for "Exit from For Loop "
            bool pageFound = false;

            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PurchaseRequisitions_SearchGridView.Wait.ForExists(Globals.timeOut);

            //Create a new RadGridView Control
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PurchaseRequisitions_SearchGridView;

            //Check value in Exact Column using Column Header Name Value
            GridViewHeaderCell header = grid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == headerName);

            //If condition is true then execute  if block
            if (grid.Rows.Count.Equals(0))
            {
                //Execute for loop for search the invoice                
                for (int i = 0; i < iteration; i++)
                {
                    //Wait for Search Textbox to Exists in DOM Tree
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Wait.ForExists(Globals.timeOut);

                    //Clear Search Textbox
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Text = string.Empty;

                    //Wait for Search Button to Exists in DOM Tree
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.Wait.ForExists(Globals.timeOut);

                    //Click on search button
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();

                    //Wait for Search Textbox to Exists in DOM Tree
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Wait.ForExists(Globals.timeOut);

                    //Enter the Search Term to search for invoice
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Text = searchText;

                    //Click on Search button 
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();

                    //Wait for Search Grid View to Exists in DOM Tree
                    SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PurchaseRequisitions_SearchGridView.Wait.ForExists(Globals.timeOut);

                    //Create a new RadGridView Control
                    RadGridView newgrid = SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PurchaseRequisitions_SearchGridView;

                    //Condition For Exis from the Loop
                    if (newgrid.Rows.Count.Equals(0) == false)
                    {
                        pageFound = true;
                        break;
                    }
                }
            }

            else
            {
                pageFound = true;
            }

            //Purchase Requisitions found true then if block code execute
            if (pageFound == true)
            {
                //Count all rows and save it in index variable
                int index = header.Index;

                //Verify Results in each row in grid
                foreach (GridViewRow row in grid.Rows)
                {
                    //Compare Each Row Value
                    if (row.Cells[index].TextBlockContent != searchText)
                    {
                        //Write the log if Verification Fail
                        throw new Exception(" Purchase Requisitions Found  by Other Name Verification Failed!!!!");
                    }
                }

                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, " Search Succeeded, Purchase Requisitions Found by Text Search: " + searchText);
            }
            else
            {
                //Write the log if Verification Fail
                throw new Exception(" Grid is empty, text : " + " " + searchText + " " + " is Not found");
            }
        }

        //Method to verify Search:Purchase Requisitions Page Filter's status
        public void P2PVerifyPurchaseRequisitionsFilterStatus(string filterStatus, string filterName = null)
        {
            //Wait for checkbox to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_DraftCheckBox.Wait.ForExists(Globals.timeOut);
            SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_WaitingForApprovalCheckBox.Wait.ForExists(Globals.timeOut);
            SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_InOrderingCheckBox.Wait.ForExists(Globals.timeOut);
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_TransferCheckBox.Wait.ForExists(Globals.timeOut);
            SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_RemovedCheckBox.Wait.ForExists(Globals.timeOut);


            //Get state of each checkbox
            bool draft = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_DraftCheckBox.IsChecked.Value;
            bool waitingApproval = SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_WaitingForApprovalCheckBox.IsChecked.Value;
            bool inOrdering = SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_InOrderingCheckBox.IsChecked.Value;
            bool processed = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_TransferCheckBox.IsChecked.Value;
            bool removed = SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_RemovedCheckBox.IsChecked.Value;

            //Declare and initialise the bool flag
            bool result = false;

            switch (filterStatus.ToUpper())
            {
                case "DEFAULT":
                    if (draft.Equals(false) && waitingApproval.Equals(false) && inOrdering.Equals(false) && processed.Equals(false) && removed.Equals(false))
                    {
                        //Write the Logs if No Checkbox is Checked (Verification is Passed)
                        Manager.Current.Log.WriteLine(" Purchase Requisitions Filters :All Checkbox are UnChecked (by default).Verification Passes !!");
                    }
                    else
                    {
                        //Write the Logs if some Checkbox is Checked (Verification is failed)
                        Manager.Current.Log.WriteLine(" Purchase Requisitions Filters :All Checkbox are NOT UnChecked (by default).Verification Fails !!");
                    }
                    break;
                case "SINGLE":
                    result = P2PVerifySingleFiltersPurchaseRequisitions(filterName, draft, waitingApproval, inOrdering, processed, removed);
                    if (result.Equals(true))
                    {
                        //Write the Logs if No Checkbox is Checked (Verification is Passed)
                        Manager.Current.Log.WriteLine(" Purchase Requisitions Filter: (" + filterName + ") is Checked. All Other Checkbox are UnChecked.Verification Passes !!");
                    }
                    else
                    {
                        //Write the Logs if some Checkbox is Checked (Verification is failed)
                        Manager.Current.Log.WriteLine(" Purchase Requisitions Filter: (" + filterName + ")is NOT Checked.Verification Fails !!");
                    }
                    break;
                case "NOFILTER":
                    if (draft.Equals(false) && waitingApproval.Equals(false) && inOrdering.Equals(false) && processed.Equals(false) && removed.Equals(false))
                    {
                        //Write the Logs if all Checkbox are unChecked (Verification is Passed)
                        Manager.Current.Log.WriteLine(" Purchase Requisitions Filters :All Checkbox are UnChecked .Verification Passes !!");
                    }
                    else
                    {
                        //Write the Logs if some Checkbox is Checked (Verification is failed)
                        Manager.Current.Log.WriteLine(" Purchase Requisitions Filters :All Checkbox are NOT UnChecked .Verification Fails !!");
                    }
                    break;
                default:
                    throw new Exception(" Purchase Requisitions Filters :Wrong agrument passed for verifying filters, Verification Failed!");
            }


        }

        //Method to verify PurchaseRequisitions (Personal Mode: search tab) Filters
        public bool P2PVerifySingleFiltersPurchaseRequisitions(string filterName, bool draft, bool waitingApproval, bool inOrdering, bool processed, bool removed)
        {
            switch (filterName.ToUpper())
            {
                case "DRAFT":
                    //If Draft CheckBox is Checked
                    if (draft.Equals(true) && waitingApproval.Equals(false) && inOrdering.Equals(false) && processed.Equals(false) && removed.Equals(false))
                        return true;
                    else
                        return false;
                case "WAITINGFORAPPROVAL":
                    //If waitingForApproval CheckBox is Checked
                    if (draft.Equals(false) && waitingApproval.Equals(true) && inOrdering.Equals(false) && processed.Equals(false) && removed.Equals(false))
                        return true;
                    else
                        return false;
                case "INORDERING":
                    //If In inOrdering CheckBox is Checked
                    if (draft.Equals(false) && waitingApproval.Equals(false) && inOrdering.Equals(true) && processed.Equals(false) && removed.Equals(false))
                        return true;
                    else
                        return false;
                case "PROCESSED":
                    //If processed CheckBox is Checked
                    if (draft.Equals(false) && waitingApproval.Equals(false) && inOrdering.Equals(false) && processed.Equals(true) && removed.Equals(false))
                        return true;
                    else
                        return false;
                case "REMOVED":
                    //If removed CheckBox is Checked
                    if (draft.Equals(false) && waitingApproval.Equals(false) && inOrdering.Equals(false) && processed.Equals(false) && removed.Equals(true))
                        return true;
                    else
                        return false;
                default:
                    return false;
            }
        }


        //Method to verify Grid data after Search done with Purchase Requisitions Filter's 
        public void P2PVerifyPurchaseRequisitionsGridFilterSearch(string status, string headerName)
        {
            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PurchaseRequisitions_SearchGridView.Wait.ForExists(Globals.timeOut);

            //Create a new RadGridView Control
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PurchaseRequisitions_SearchGridView;

            try
            {
                //Check value in Exact Column using Column Header Name Value
                GridViewHeaderCell header = grid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == headerName);
                //Check Header value is not Null
                Assert.IsNotNull(header);

                if (grid.Rows.Count.Equals(0))
                {
                    Manager.Current.Log.WriteLine(LogType.Information, " " + " There is No Purchase Requisitions Found by Status Filter  :" + status + ", Total Number of Records is 0");
                }
                else
                {
                    //Count all rows and save it in index varibable 
                    int index = header.Index;
                    //Verify Results in each row in grid
                    foreach (GridViewRow row in grid.Rows)
                    {
                        //Compare Each Row Value
                        if (row.Cells[index].TextBlockContent != status)
                        {
                            //Write the log if Verification Fail
                            Manager.Current.Log.WriteLine(LogType.Error, " Purchase Requisitions Found by Other Status, Verification Failed!!!!");
                        }
                    }

                    // Write the log if verification is Pass 
                    Manager.Current.Log.WriteLine(LogType.Information, " " + "Purchase Requisitions Found by Status Filter : " + status);
                }
            }
            catch (AssertException ex)
            {
                // Write the log if verification is Fail
                Manager.Current.Log.WriteLine(LogType.Error, " Unable to Find the Column Header:" + " " + headerName + "Unable to Verify!" + ex.Message);
            }
        }

        //Method to verify Grid is empty: Search PurchaseRequisition
        public void P2PVerifyPurchaseRequisitionsSearchGridEmpty()
        {
            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PurchaseRequisitions_SearchGridView.Wait.ForExists(Globals.timeOut);

            if (SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PurchaseRequisitions_SearchGridView.Rows.Count.Equals(0))
            {
                //Print the Logs 
                Manager.Current.Log.WriteLine(LogType.Information, " Search Succeeded, 0 Purchase Requisitions Found with - All Status Filters as Unchecked");
            }
            else
            {
                //Print the Logs in case of failure
                Manager.Current.Log.WriteLine(LogType.Error, "Purchase Requisitions found with - All Status Filters as Unchecked, Verification Failed!");
            }
        }

        //Method to verify Grid is empty: MyTasks PurchaseRequisition and Webshop MY purchases
        public void P2PVerifyPurchaseRequisitionMyTasksGridEmpty()
        {
            //Wait for grid exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_RadGridViewcontrol.Wait.ForExists(Globals.timeOut);

            //Check the condition Row Count is 0
            if (SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_RadGridViewcontrol.Rows.Count.Equals(0))
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, " Grid is empty. Verificaton Passed !!");
            }
            else
            {
                //Write the log if Verification Fails
                Manager.Current.Log.WriteLine(LogType.Error, " Grid is Not empty. Verificaton Failed !");
            }
        }

        //Method to Verify Searched Purchase Orders
        public void SearchPurchaseOrderVerification(string purchaseOrderNumber)
        {
            bool found;

            if (SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PurchaseRequisitions_VerticalMenuItem.IsChecked.Equals(true))
            {
                // Wait for P2P_PersonalModeSearch_PurchaseRequisitions_SearchGridView Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PurchaseRequisitions_SearchGridView.Wait.ForExists(Globals.timeOut);

                //Put the Grid View Control into a FrameworkElement fe.
                FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PurchaseRequisitions_SearchGridView;

                //Check whether any TextBlock in the Grid View Control Contains Purchase Order Number
                found = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(purchaseOrderNumber));
            }

            else
            {
                // Wait for P2P_MyTasks_PurchaseOrders_GridViewControl Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrders_GridViewControl.Wait.ForExists(Globals.timeOut);

                //Put the Grid View Control into a FrameworkElement fe.
                FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrders_GridViewControl;

                //Check whether any TextBlock in the Grid View Control Contains Purchase Order Number
                found = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(purchaseOrderNumber));
            }

            //Log the results.
            if (found == true)
            {
                //Log the result if Purchase Order Number Exists
                Manager.Current.Log.WriteLine("Pass: " + purchaseOrderNumber + " found in Grid. Verification Passed!!");
            }
            else
            {
                //Throw Exception if Purchase Order Number Not Exists 
                throw new Exception("Fail: " + purchaseOrderNumber + " Not Found in grid. Verification Failed!!");
            }
        }

        //Method to Verify Open Searched Purchase Orders
        public void P2POpenSearchedPurchaseOrderVerification(string purchaseOrderNumber)
        {
            if (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_MyTaskButton.IsChecked.Equals(true))
            {
                if (SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseOrder_VerticalMenuItem.IsChecked.Equals(true))
                {
                    //Log the result if Purchase Order Number Exists
                    Manager.Current.Log.WriteLine(LogType.Information, purchaseOrderNumber + " found under My Task - Purchase Order Vertical Menu. Verification Passed!!");
                }

                else
                {

                    if (SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchasRequisition_VerticalMenuItem.IsChecked.Equals(true))
                    {
                        //Log the result if Purchase Order Number Exists
                        Manager.Current.Log.WriteLine(LogType.Information, purchaseOrderNumber + " found under My Task - Purchase Requisition Vertical Menu. Verification Passed!!");
                    }

                    else
                    {
                        //Log the result if Purchase Order Number Not Exists
                        Manager.Current.Log.WriteLine(LogType.Error + purchaseOrderNumber + " not found under My Task. Verification Failed!!");

                        //Throw Exception if Purchase Order Number Not Exists 
                        throw new Exception(LogType.Error + " PO not found under My Task. Verification Failed!!");
                    }
                }
            }

            else
            {
                if (SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PurchaseOrders_VerticalMenuItem.IsChecked.Equals(true))
                {
                    //Log the result if Purchase Order Number Exists
                    Manager.Current.Log.WriteLine(LogType.Information, purchaseOrderNumber + " found under Search - Purchase Order Vertical Menu. Verification Passed!!");
                }

                else
                {

                    if (SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PurchaseRequisitions_VerticalMenuItem.IsChecked.Equals(true))
                    {
                        //Log the result if Purchase Order Number Exists
                        Manager.Current.Log.WriteLine(LogType.Information, purchaseOrderNumber + " found under Search - Purchase Requisition Vertical Menu. Verification Passed!!");
                    }

                    else
                    {
                        //Log the result if Purchase Order Number Not Exists
                        Manager.Current.Log.WriteLine(LogType.Error + purchaseOrderNumber + " not found under Search. Verification Failed!!");

                        //Throw Exception if Purchase Order Number Not Exists 
                        throw new Exception(LogType.Error + " PO not found under My Task. Verification Failed!!");
                    }
                }
            }
        }

        //Method to Verify Buttons are disabled for Rejected Line
        public void P2PRejectedLineButtonVerification()
        {
            if (SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_ApproveToolbarButton.IsEnabled.Equals(false) && SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_RejectToolbarButton.IsEnabled.Equals(false))
            {
                //Log the result if Buttons are Disabled
                Manager.Current.Log.WriteLine("Pass: Approve & Reject buttons are disabled. Verification Passed!!");
            }

            else
            {
                //Log the result if Buttons are enabled
                Manager.Current.Log.WriteLine("Fail: Approve & Reject buttons are enabled. Verification Failed!!");

                //If any exception occurrs or verification fails
                throw new Exception(LogType.Error + "Failed: Approve & Reject buttons are enabled.");
            }
        }

        //Method to Verify PR for Rejected Line
        public void P2PMyTasks_VerifyPurchaseRequisitionLineStatus(string prLine, string prLineRejectedStatus)
        {
            // Wait for List View Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_LinesListViewGridControl.Wait.ForExists(Globals.timeOut);

            if (SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_LinesListViewGridControl.Rows.Count > 1)
            {
                //Find the Header cell Value as PR Line
                TextBlock lineStatus = SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_LinesListViewGridControl.Find.ByTextContent(prLine).As<TextBlock>();

                //Click on Textblock
                lineStatus.User.Click();
            }

            //Put the Grid View Control into a FrameworkElement fe.
            FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_LinesListViewGridControl;                      

            //Check whether any TextBlock in the Grid View Control Contains Purchase Order Number
            bool found = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(prLineRejectedStatus));

            if (found)
            {
                //Log the result if Verification Passes
                Manager.Current.Log.WriteLine(LogType.Information, "PR Line found '" + prLineRejectedStatus + "'. Verification Passed!!");
            }

            else
            {
                //Log the result if Verification Fails
                Manager.Current.Log.WriteLine(LogType.Error, "PR Line Status '" + prLineRejectedStatus + "'. Not Correct, Verification Failed!!");             
            }
        }        
    }
}
