using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using Telerik.WebAii.Controls.Xaml;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Controls.Xaml.Wpf;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.Common.Exceptions;
using System.Collections;
using P2P.Testing.Shared.Class;
using System.Globalization;
using P2P.Testing.Shared.Class.InvoiceAdministration;
using E2E.Class;

namespace P2P.Testing.Shared.Class.Purchase.GoodsReceipts
{
    public class P2PGoodsReceiptsVerification : BaseWebAiiTest
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

        //Method to verify GR(My Task) Free Text Search
        public void P2PGoodsReceiptsVerifyFreeTextSearch(string headerName, int iteration, string searchText)
        {
            //This bool value used for "Exit from For Loop "
            bool pageFound = false;

            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_ListViewGridcontrol.Wait.ForExists(Globals.timeOut);

            //Create a new RadGridView Control
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_ListViewGridcontrol;

            //Check value in Exact Column using Column Header Name Value
            GridViewHeaderCell header = grid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == headerName);

            if (headerName != header.TextBlockContent)
            {
                //Capture Screenshot If Verification Fails
                Manager.Current.Log.CaptureDesktop(Globals.capturedImageLocation + " Expected Header Column Name Not Found");

                //Write the log if verification is Failed                               
                Manager.Current.Log.WriteLine(LogType.Error, " Expected Header Column Name: " + headerName + ", NOT Found in the Grid. Verification Failed");
            }
            else
            {
                //If condition is true then execute  if block
                if (grid.Rows.Count.Equals(0))
                {
                    //Execute for loop for search the GR                
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

                        //Enter the Search Term to search for GR
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Text = searchText;

                        //Click on Search button 
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();

                        //Wait for Search Grid View to Exists in DOM Tree
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_ListViewGridcontrol.Wait.ForExists(Globals.timeOut);

                        //Create a new RadGridView Control
                        RadGridView newgrid = SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_ListViewGridcontrol;

                        //Condition For Exis from the Loop
                        if (newgrid.Rows.Count.Equals(0) == false)
                        {
                            pageFound = false;
                            break;

                        }

                    }

                }

                else
                {
                    pageFound = true;
                }

                //GR found true then if block code execute
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
                            throw new Exception(" Goods Receipts Found by Other Name, Verification Failed!! ");
                        }
                    }
                    //Write the log if Verification Pass
                    Manager.Current.Log.WriteLine(LogType.Information, " Search Succeeded, Goods Receipts Found by Text Search: " + searchText);
                }
                else
                {
                    //Write the log if Verification Fail
                    throw new Exception(" Grid is empty, text : " + " " + searchText + " " + " is Not found");
                }
            }
        }

        //Method to verify Comment added in History and Comments tab of GR 
        public void P2PVerifyGoodsReceiptsComment(string comment)
        {
            //Wait for Element Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PRLines_HistoryComments_Tabitem.Wait.ForExists(Globals.timeOut);

            //Click on Tab Item
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PRLines_HistoryComments_Tabitem.User.Click();

            //Wait for Element Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsListBox.Wait.ForExists(Globals.timeOut);

            //Wait for Element Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_FilteringComments_ComboBox.Wait.ForExists(Globals.timeOut);

            //Get Value of combobox
            string comboItem = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_FilteringComments_ComboBox.Text;
            comboItem = comboItem.Trim();

            //Execute if condition when combo box item is "ALL"
            if (comboItem.ToUpper() != "ALL")
            {
                //Set the Combo Box to "All"
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_FilteringComments_ComboBox.SelectItemByText(true, "All", true);

                //Log Information
                Manager.Current.Log.WriteLine("By Default History and Comments Combobox doesnt show (All). Current value selected in ComboBox was: " + comboItem);

            }

            // Verify 'History and Comments TabItem' Contains Comments 
            P2PInvoiceAdministrationVerification verifyObj = new P2PInvoiceAdministrationVerification();
            verifyObj.P2PHistoryListBoxVerification(comment, "GR", "History and Comments Tab");

            //try
            //{
            //    //Verify the Comments in History Pop-Up
            //    FrameworkElement e = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsListBox;

            //    //Check whether any TextBlock contains the specified string.
            //    bool found = e.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(comment));

            //    //Check using If Condition that Comments Exists?
            //    if (found == true)
            //    {
            //        //Write the Result in log file when Verification is Pass
            //        Manager.Current.Log.WriteLine("Comment:  (" + " " + comment + " " + ")   Exists in History and Comments TabItem against the GR.");
            //    }
            //    else
            //    {
            //        //Write the Result in log file when Verification is Fail
            //        throw new Exception("Comment:  (" + " " + comment + ")   does NOT exist in History and Comments TabItem, Verification Failed against the GR.");
            //    }
            //}
            //finally
            //{}
        }

        //Method to Verify PO in Related Documents Tab      
        public void P2PWebShopMyTasks_VerifyRelatedDocument(string noRelatedDocumentText)
        {
            //Wait for ListBox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=listbox")).Wait.ForExists(Globals.timeOut);

            //Getting GridView control in Framework element variable grid
            FrameworkElement grid = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=listbox"));

            //Check the search result
            bool found = grid.Find.AllByType<ListBoxItem>().Count.Equals(null);

            //Check using If Condition that Comments Exists?
            if (found == false)
            {
                //Log the results if string Exists in Listbox
                Manager.Current.Log.WriteLine(LogType.Information, noRelatedDocumentText + " does not exists. Grid is not empty. Verification Pass!!");
            }
            else
            {
                //Log the results if string does Not Exists
                Manager.Current.Log.WriteLine(LogType.Error + noRelatedDocumentText + " exists. Grid is empty. Verification Failed!!");

                //Throw the Exception if string does Not Exists
                throw new Exception(LogType.Error + " There is No Purchase Order exists in Related Document Grid. Verification Failed!!");
            }
        }

        //Method to Verify Data of PO in Related Documents Tab
        public void P2PWebShopMyTasks_VerifyPODocument(string purchaseOrderType, string purchaseOrderNumber, string purchaseOrderStatus = null)
        {
            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_RelatedDocumentListBox.Wait.ForExists(Globals.timeOut);

            //Use FrameworkElement and read all the TextBlock in a fe
            FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_RelatedDocumentListBox;

            //Check the purchaseOrder in the Related Document
            bool checkPOType = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(purchaseOrderType));

            //generate part of PO/PR number
            string generatePurchaseNumber = purchaseOrderNumber + DateTime.Now.Year.ToString(CultureInfo.CurrentCulture.NumberFormat);

            //Check the purchaseOrderNumber in the Related Document
            bool checkPONumber = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(generatePurchaseNumber));

            //Saving Date Time into a string
            string purchaseOrderCreateDate = DateTime.Now.ToShortDateString().ToString(CultureInfo.CurrentCulture.NumberFormat);

            //Check the purchaseOrderCreateDate in the Related Document
            bool checkPOCreateDate = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(purchaseOrderCreateDate));

            if (purchaseOrderStatus == null)
            {
                //Check the values for the Purchase Order in Related Document
                if (checkPOType != true && checkPONumber != true && checkPOCreateDate != true)
                {
                    //Write the log if Verification Fail
                    throw new Exception(LogType.Error + " Related Document: '" + purchaseOrderType + "' - '" + generatePurchaseNumber + "': Created on '" + purchaseOrderCreateDate + "' not found. Verification Failed!");
                }

                else
                {
                    //Write the log if Verification Pass
                    Manager.Current.Log.WriteLine(LogType.Information, " Related Document: '" + purchaseOrderType + "' - '" + generatePurchaseNumber + "': Created on '" + purchaseOrderCreateDate + "' found. Verification Passed!");
                }
            }

            else
            {
                //Check the purchaseOrderStatus in the Related Document
                bool checkPOStatus = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(purchaseOrderStatus));

                //Check the values for the Purchase Order in Related Document
                if (checkPOType != true && checkPONumber != true && checkPOCreateDate != true && checkPOStatus != true)
                {
                    //Write the log if Verification Fail
                    throw new Exception(LogType.Error + " Related Document: '" + purchaseOrderType + "' - '" + generatePurchaseNumber + "': Created on '" + purchaseOrderCreateDate + "' and Status is '" + purchaseOrderStatus + "' not found. Verification Failed!");
                }

                else
                {
                    //Write the log if Verification Pass
                    Manager.Current.Log.WriteLine(LogType.Information, " Related Document: '" + purchaseOrderType + "' - '" + generatePurchaseNumber + "': Created on '" + purchaseOrderCreateDate + "' and Status is '" + purchaseOrderStatus + "' found. Verification Passed!");
                }
            }
        }

        //Method to verify History and Comments combobox items
        public void P2PVerifyGoodsReceiptsHistoryComboBox(string[] arrayItems)
        {
            //Wait for Element Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PRLines_HistoryComments_Tabitem.Wait.ForExists(Globals.timeOut);

            //Click on Tab Item
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PRLines_HistoryComments_Tabitem.User.Click();

            //Wait for Element Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HistoryandCommentsListBox.Wait.ForExists(Globals.timeOut);

            //Wait for Element Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_FilteringComments_ComboBox.Wait.ForExists(Globals.timeOut);

            //Click on combobox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_FilteringComments_ComboBox.User.Click();

            //Test data to compare
            //string[] compareItems = { "ALL", "COMMENTS", "USER ACTIONS", "SYSTEM ACTIONS" };

            //Compare Combox items
            foreach (string strCompare in arrayItems)
            {
                //Use FrameworkElement and read all the TextBlock in a fe
                FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_FilteringComments_ComboBox;

                //Check the Combox items
                bool checkItem = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(strCompare));

                //Check the values for items
                if (checkItem != true)
                {
                    //Write the log if Verification Fail
                    throw new Exception(LogType.Error + " Combobox Item not found: " + strCompare + ". Verification Failed!");
                }
                else
                {
                    //Write the log if Verification Pass
                    Manager.Current.Log.WriteLine(LogType.Information, " Combobox Item found: " + strCompare + ". Verification Passed!");
                }
            }

            //Click on combobox to close it
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_FilteringComments_ComboBox.User.Click();
        }

        //Method to Verify Add Attachment
        public void P2PGoodsReceiptsAddAttachmentVerification(string fileName, string prTitle)
        {
            //Use Exception Handling       
            //try
            //{
            // Wait for Attachment Tab Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_AttachmentListTabitem.Wait.ForExists(Globals.timeOut);

            //Click on Attachment Tab Items
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_AttachmentListTabitem.User.Click();

            P2PInvoiceAdministrationVerification verifyObj = new P2PInvoiceAdministrationVerification();
            verifyObj.P2PAttachmentListBoxVerification(fileName, prTitle, "Add", "Attachments Tab");

            ////Wait for Attachment ListBox Exists in DOM
            //SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_InvoiceDetails_Attachment_AttachmentListbox.Wait.ForExists(Globals.timeOut);

            ////Put the ListBox into a variable.
            //FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_InvoiceDetails_Attachment_AttachmentListbox;

            ////Check whether any TextBlock in the ListBox Contains FileName
            //bool found = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(fileName));

            ////Log the results.
            //if (found == true)
            //{
            //    //Log the result if Attachment Exists
            //    Manager.Current.Log.WriteLine(fileName + ":  Exists in Attachments Tab, against :" + " " + prTitle);
            //}
            //else
            //{
            //    //Throw Exception if Attachment Not Exists 
            //    throw new Exception(LogType.Error + fileName + ": does NOT Exist Attachments Tab, against :" + " " + prTitle + ", Verification Failed!");
            //}
            //}
            //catch (Exception e)
            // {
            //   //Gives the error message
            //    throw new Exception("Error occurs in Upload Dialog" + " : " + e);
            // }
        }

        //Method to Verify Edit Attachment
        public void P2PGoodsReceiptsEditAttachmentVerification(string fileName, string comment, string prTitle)
        {
            // Wait for Attachment Tab Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_AttachmentListTabitem.Wait.ForExists(Globals.timeOut);

            //Click on Attachment Tab Items in Details Page
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_AttachmentListTabitem.User.Click();

            P2PInvoiceAdministrationVerification verifyObj = new P2PInvoiceAdministrationVerification();
            verifyObj.P2PAttachmentListBoxVerification(comment, prTitle, "Edit", "Attachments Tab");

            ////Wait for Attachments List Box Exists in DOM 
            //SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_InvoiceDetails_Attachment_AttachmentListbox.Wait.ForExists(Globals.timeOut);

            ////Verify that TextBlock Contains FileName
            //try
            //{
            //    //Put the ListBox into a variable.
            //    FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_InvoiceDetails_Attachment_AttachmentListbox;

            //    //Check whether any TextBlock in the ListBox contains the specified string.
            //    bool found = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(comment));

            //    //Log the results.
            //    if (found == true)
            //    {
            //        // Write the Log If Uploaded file found
            //        Manager.Current.Log.WriteLine(fileName + " New Comment" + ":  (" + comment + ") Exists in Attachments Panel against:" + " " + prTitle + ": " + found);
            //    }
            //    else
            //    {
            //        // Write the Log If Uploaded file Not found
            //        throw new Exception(LogType.Error + fileName + " New Comment" + ":  (" + comment + ") does not exist Attachments Panel. Verification Failed against: " + " " + prTitle + ": " + found);
            //    }
            //}
            //catch (Exception e)
            //{
            //    //Gives the error message
            //    throw new Exception("Error occurs in Edit Dialog" + " : " + e);
            //}
        }

        //Method to Verify Delete Attachment
        public void P2PGoodsReceiptsDeleteAttachmentVerification(string text, string prTitle, string fileName)
        {
            //Wait for Attachments Tab Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_AttachmentListTabitem.Wait.ForExists(Globals.timeOut);

            //Click on Attachments Tab
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_AttachmentListTabitem.User.Click();

            P2PInvoiceAdministrationVerification verifyObj = new P2PInvoiceAdministrationVerification();
            verifyObj.P2PAttachmentListBoxVerification(text, prTitle, "Delete", "Attachments Tab");

            ////Wait for Attachments List Box Exists in DOM 
            //SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_InvoiceDetails_Attachment_AttachmentListbox.Wait.ForExists(Globals.timeOut);


            ////Put the ListBox into a variable.
            //FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_InvoiceDetails_Attachment_AttachmentListbox;

            ////Check whether any TextBlock in the ListBox contains the specified string.
            //bool found = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(text));

            ////Log the results.
            //if (found == true)
            //{
            //    // Write the Log If Uploaded file found
            //    Manager.Current.Log.WriteLine(fileName + " Comment:  (" + text + ")   Exists in Attachments Panel. Against : " + " " + prTitle);
            //}
            //else
            //{
            //    // Write the Log If Uploaded file Not found
            //    throw new Exception(LogType.Error + fileName + " Comment:  (" + text + ")   does NOT exist in Attachments Panel, Verification Failed Against : " + " " + prTitle);
            //}
        }

        //Method to Verify Fully Received Purchase requisition
        public void FullyReceivedPurchaserequisitionVerification(string prTitle)
        {
            //Wait for List View Grid Control Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_ListViewGridcontrol.Wait.ForExists(Globals.timeOut);

            //Verify that TextBlock Contains FileName
            try
            {
                //Put the ListBox into a variable.
                FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_ListViewGridcontrol;

                //Check whether any TextBlock in the ListBox contains the specified string.
                bool found = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(prTitle));

                //Log the results.
                if (found == true)
                {
                    // Write the Log If Uploaded file found
                    throw new Exception(LogType.Error + prTitle + " exists on Goods Receipt Page. Verification Failed!");
                }
                else
                {
                    // Write the Log If Uploaded file Not found
                    Manager.Current.Log.WriteLine(LogType.Information, prTitle + " does not exists on Goods Receipt Page. Verification Passed!");
                }
            }
            catch (Exception e)
            {
                //Gives the error message
                throw new Exception("Error occurs in verifing" + prTitle + " : " + e);
            }
        }

        //Method to Verify All Lines show in Receive dialog
        public void P2PVerifyGoodsReceiptsReceiveDailogAllLines(int expectedLineCount)
        {
            //Wait for P2P_MyTasks_GoodsReceipts_ReceiveButton button to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_ReceiveButton.Wait.ForExists(Globals.timeOut);

            //Click on P2P_MyTasks_GoodsReceipts_ReceiveButton button
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_ReceiveButton.User.Click();

            //Wait for P2P_MyTasks_GoodsReceipts_GoodsReceiptDialog to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_GoodsReceiptDialog.Wait.ForExists(Globals.timeOut);

            //Wait for OK button to Exists in DOM
            while (SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_GoodsReceiptDialog.IsOpen.Equals(false))
            {
                //Wait for P2P_MyTasks_GoodsReceipts_GoodsReceiptDialog to Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_GoodsReceiptDialog.Wait.ForExists(Globals.timeOut);
            }

            //Wait for grid 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ManualMatching_GoodsReceiptGrid.Wait.ForExists(Globals.timeOut);

            //Declare and get runtime value
            int actualLineCount = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ManualMatching_GoodsReceiptGrid.Rows.Count();

            //Verify receive dailog shows all Lines 
            if (actualLineCount == expectedLineCount)
            {
                //Logs Pass results when all lines show in receive dailog
                Manager.Current.Log.WriteLine(LogType.Information, " Goods Receipts Receive Dialog shows all(" + expectedLineCount + ") GR lines. Verification Passed!");
            }
            else
            {
                //Logs Fail results when all lines do not show in receive dailog
                Manager.Current.Log.WriteLine(LogType.Error, " Goods Receipts Receive Dialog doesn't show all(" + expectedLineCount + ") GR lines. Lines showing on screen(" + actualLineCount + "). Verification Failed!");
            }

            //Close Receive Dialog
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_CancelButton.Wait.ForExists(Globals.timeOut);

            //Click on cancel button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_CancelButton.SetFocus();

            //Click on cancel button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_CancelButton.User.Click();
        }

        //Method to Verify columns on grids : GR Panel.Received Quantity= PR Panel.Received
        public void P2PVerifyPRPanelGRPanelColumns(int prRowNumber, int grRowNumber, string prColumnName, string grColumnName, string selectAllLines = null)
        {
            //Added static wait here as busy indicator doesnt work.
            //The Coded step before this function call selects one PR Line
            //We need to wait after 1 PR line is selected so that corresponding GR panel to populate.
            System.Threading.Thread.Sleep(Globals.timeOut);

            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PRLines_ListViewGrid.Wait.ForExists(Globals.timeOut);
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_GRLines_ListViewGrid.Wait.ForExists(Globals.timeOut);

            //Create a new RadGridView Control
            RadGridView prGrid = SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PRLines_ListViewGrid;
            RadGridView grGrid = SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_GRLines_ListViewGrid;

            //Check value in Exact Column using Column Header Name Value                                        
            GridViewHeaderCell prHeader = prGrid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == prColumnName);
            GridViewHeaderCell grHeader = grGrid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == grColumnName);

            //Check Header value is not Null            
            if (prHeader.Equals(null) || grHeader.Equals(null))
            {
                throw new Exception(" Column not found on PR Panel / GR Panel");
            }

            //declare variables
            string prGridValue, grGridValue;
            int prGridIndex, grGridIndex;

            //Save column index  
            prGridIndex = prHeader.Index;
            grGridIndex = grHeader.Index;

            //Read out values from grid
            prGridValue = prGrid.Rows[prRowNumber].Cells[prGridIndex].Text;
            grGridValue = grGrid.Rows[grRowNumber].Cells[grGridIndex].Text;

            //If condition to check grid columns
            if (grGridValue.Contains(prGridValue))
            {
                //Logs Pass results when received quantity is same
                Manager.Current.Log.WriteLine(LogType.Information, " PR Line received quantity(" + prGridValue + ") is same as GR Line received  quantity(" + grGridValue + "). Verification Passed!");
            }
            else
            {
                //Logs Fail results when received quantity is not same
                Manager.Current.Log.WriteLine(LogType.Error, " PR Line received quantity(" + prGridValue + ") is Not same as GR Line received  quantity(" + grGridValue + "). Verification Failed!");
            }

            if (selectAllLines != null)
            {
                //Press Ctrl A to select all PR lines
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(ArtOfTest.WebAii.Win32.KeyBoard.KeysFromString("Ctrl+A"));
            }
        }

        //Method to Verify Error Message of Default Size Of Uploading Attachment
        public void VerifyErrorMsgOfDefaultSizeUploadingAttachment(string errorMsg)
        {
            bool found = false;

            //Check whether any TextBlock in the Grid View Control Contains Purchase Order Number
            found = SharedElement.P2P_Application.SilverlightApp.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(errorMsg));
            try
            {
                if (found == true)
                {

                    //Log the result if Purchase Order Number Exists
                    Manager.Current.Log.WriteLine(errorMsg + " Error Message found While Uploading File More Than Default Size Verification Passed!!");
                }

                else
                {
                    //Throw Exception if Purchase Order Number Not Exists 
                    throw new Exception(errorMsg + " Error Message not found While Uploading File More Than Default Size Verification Failed!!");
                }

            }
            finally
            {

                //Wait for OK button to Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_AddAttachment_CancelButton.Wait.ForExists(Globals.timeOut);

                //Click on OK button on Add Attachment Dialog Box 
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_AddAttachment_CancelButton.User.Click();
            }


        }

        //Method to Verify Shop Search Product Attachment
        public void P2PVerifySearchProductAttachment(string attachmentText)
        {
            //Wait for ListBox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=listbox")).Wait.ForExists(Globals.timeOut);

            //Set Focus on Grid
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=listbox")).SetFocus();

            //Getting & Finding Text of the Attachment Tab
            bool attachmentTextCompare = SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_ProductAttachmentsTabItem.Text.Contains(attachmentText);

            if (attachmentTextCompare == false)
            {
                //Write Logs If Pass
                Manager.Current.Log.WriteLine(LogType.Information, " Attachment found in Grid. Verification Passed!");
            }
            else
            {
                //Write Logs If fails
                Manager.Current.Log.WriteLine(LogType.Error, " Attachment not found in Grid. Grid is empty. Verification Failed!");
                throw new Exception("Verification Failed");
            }
        }

        /*E2E Verification Method*/
        //Method to Verify Remaining Receiving Goods Receipts
        public void P2PVerifyReceivedQuantity()
        {
            //Wait for grid to load
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ManualMatching_GoodsReceiptGrid.Wait.ForExists(Globals.timeOut);

            //Declaring RadGridView variable
            RadGridView grid;
            
            //Get grid in RadGridView variable
            grid = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ManualMatching_GoodsReceiptGrid;

            //Create an object for GridViewRow class
            GridViewRow gvr = grid.Rows[grid.Rows.Count - 1];

            //Get and save Data
            double getReceivedData = Convert.ToDouble(gvr.Cells[2].Value);

            if( getReceivedData == P2P_Utility.receivedQuantity)
            {
                //Write Logs If Pass
                Manager.Current.Log.WriteLine(LogType.Information, "Received Quantity: '" + getReceivedData + "' is equal to Received Now Quantity inputted: '" + P2P_Utility.receivedQuantity + "'. Verification Passed!");
            }
            else
            {
                //Write Logs If fails
                Manager.Current.Log.WriteLine(LogType.Error, " Received Quantity: '" + getReceivedData + "' is not equal to Received Now Quantity inputted: '" + P2P_Utility.receivedQuantity + "'. Verification Failed!");
                throw new Exception("Verification Failed");
            }
        }
    }
}