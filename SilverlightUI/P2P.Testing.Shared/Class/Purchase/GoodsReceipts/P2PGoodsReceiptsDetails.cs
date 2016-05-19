using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Win32.Dialogs;
using ArtOfTest.WebAii.Core;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Silverlight.UI;
using System.Threading;
using System.Windows.Forms;
using P2P.Testing.Shared.Class;
using Telerik.WebAii.Controls.Xaml;
using System.Globalization;
using ArtOfTest.WebAii.Win32;
using E2E.Class;

namespace P2P.Testing.Shared.Class.Purchase.GoodsReceipts
{
    public class P2PGoodsReceiptsDetails : BaseWebAiiTest
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

        //Method to Select a GR and Navigate To Related Document Tab
        public void NavigateToRelatedDocument(string prTitle)
        {
            //Wait for MyTasks Goods Receipts Pending Filter Button to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PendingFilterButton.Wait.ForExists(Globals.timeOut);

            //Click on MyTasks Goods Receipts Pending Filter Button
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PendingFilterButton.User.Click();

            //Wait for MyTasks Goods Receipts List View Grid Control to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_ListViewGridcontrol.Wait.ForExists(Globals.timeOut);

            //Setting Focus on List View Grid Control
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_ListViewGridcontrol.SetFocus();

            //Use FrameworkElement and read all the TextBlock in a fe
            FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_ListViewGridcontrol;

            //Saving Purchase Requisition in Text Block
            TextBlock selectPurchaseRequisition = fe.Find.ByTextContent(prTitle).As<TextBlock>();

            //Click on PR Title Header Cell
            selectPurchaseRequisition.User.Click(MouseClickType.LeftClick);

            //Calling Busy Indicator
            P2PNavigation.CallBusyIndicator();

            //Wait for My Tasks Goods Receipts Related Documents Tab to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_RelatedDocumentsTab.Wait.ForExists(Globals.timeOut);

            //Setting Focus on Related Document Tab
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_RelatedDocumentsTab.SetFocus();

            //Click on My Tasks Goods Receipts Related Documents Tab
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_RelatedDocumentsTab.User.Click();
        }

        //Method to Select Pending Filter And Select a GR
        public void P2pSearchAndSelectAGoodReceipt(string prTitle, string pendingFilter = null)
        {
            //If there is need to Click on Pending Filter before selecting the GR then keep 'pendingFilter' variable NULL
            if (pendingFilter == null)
            {
                //Wait for MyTasks Goods Receipts Pending Filter Button to Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PendingFilterButton.Wait.ForExists(Globals.timeOut);

                //Click on MyTasks Goods Receipts Pending Filter Button
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PendingFilterButton.User.Click();
            }

            //Wait for MyTasks Goods Receipts List View Grid Control to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_ListViewGridcontrol.Wait.ForExists(Globals.timeOut);

            //Setting Focus on List View Grid Control
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_ListViewGridcontrol.SetFocus();

            //Use FrameworkElement and read all the TextBlock in a fe
            FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_ListViewGridcontrol;

            //Saving Purchase Requisition in Text Block
            TextBlock selectPurchaseRequisition = fe.Find.ByTextContent(prTitle).As<TextBlock>();

            //Click on PR Title Header Cell
            selectPurchaseRequisition.User.Click();

            //Calling Busy Indicator
            P2PNavigation.CallBusyIndicator();
        }

        //Method to Upload Attachment to a Good Receipt
        public void P2PGoodsReceiptsAddAttachment(string filePath, string description,string defaultSize=null)
        {
            //Wait for My Tasks Goods Receipts Attachments Tab to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_AttachmentListTabitem.Wait.ForExists(Globals.timeOut);

            //Click on My Tasks Goods Receipts Attachments Tab
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_AttachmentListTabitem.User.Click();

            //Wait for Add Attachment Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddButton.Wait.ForExists(Globals.timeOut);
            //Click on Attachment Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddButton.User.Click();
            
            // Using If Condition for Execution in different Browser (Chrome, Firefox etc)
            if (Manager.Current.ActiveBrowser.BrowserType != BrowserType.InternetExplorer)
            {
                // Wait for Browse Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddAttachment_BrowseButton.Wait.ForExists(Globals.timeOut);

                //Click on Browse Button to Browse the Path 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddAttachment_BrowseButton.User.Click();

                //Type the Path for Saving the File
                Manager.Current.Desktop.KeyBoard.TypeText(filePath, 50);

                //Click on Enter Button
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);

                //Wait for Attachment Dialogs to appear 
                Thread.Sleep(Globals.handleTime);
            }
            else
            {
                try
                {
                    //Wait for Globals.timeOut milisec
                    System.Threading.Thread.Sleep(Globals.handleTime);
                    //Initialize the FileUpload Dialog
                    FileUploadDialog fileDialog = new FileUploadDialog(Manager.Current.ActiveBrowser, filePath, DialogButton.OPEN, "Open");
                    //Wait for Globals.timeOut milisec
                    Thread.Sleep(Globals.handleTime);

                    //Add the File Dialog
                    Manager.Current.DialogMonitor.AddDialog(fileDialog);
                    //Start the DialogMonitor
                    Manager.Current.DialogMonitor.Start();

                    //Wait for Browse Button Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddAttachment_BrowseButton.Wait.ForExists(Globals.timeOut);
                    //Click on Browse Button to Browse the Path 
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddAttachment_BrowseButton.User.Click();
                    //Wait for Globals.timeOut milisec
                    Thread.Sleep(Globals.handleTime);
                }
                catch (System.IO.IOException e)
                {
                    //Give the error message in case File Upload Dialog not handled
                    throw new Exception("File Upload Dialog is Not Handled:" + " : " + e);
                }
            }


            if (defaultSize==null)
            {
            //Wait for AddComment_TextBox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);
            // Set Focus on Description Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.SetFocus();

            //Enter the Text in Description TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(description, 20, true);

            //Wait for OK button to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK button on Add Attachment Dialog Box 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_OKButton.User.Click();

            //Wait for Attachment to be Uploaded 
            Thread.Sleep(Globals.handleTime);
            }
        }

        //Method to Delete Attachment to a Good Receipt
        public void P2PGoodsReceiptsDeleteAttachment(string fileName, string comments, string prTitle)
        {
            //Wait for Attachment List Tab Item to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_AttachmentListTabitem.Wait.ForExists(Globals.timeOut);

            //Click on Attachment List Tab Item
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_AttachmentListTabitem.User.Click();

            try
            {
                //Wait for Attachment List box to Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_InvoiceDetails_Attachment_AttachmentListbox.Wait.ForExists(Globals.timeOut);

                //Put the ListBox into a variable.
                FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_InvoiceDetails_Attachment_AttachmentListbox;

                //Check whether any TextBlock in the ListBox contains the specified string.
                bool found = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(fileName));

                // if Condition is true then code execute under the if Block
                if (found == true)
                {
                    //Wait for Delete Attachment Button Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_DeleteAttachmentButton.Wait.ForExists(Globals.timeOut);

                    //Click on Delete Attachment Button
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_DeleteAttachmentButton.User.Click();

                    //Wait for Add Comment TextBox Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);

                    //Enter the Comment in Delete Attachment Box 
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(comments, 20, true);

                    //Wait for OK Button Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

                    //Click on OK Button after added a comment
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

                    //Log the result if Attachment Deleted
                    Manager.Current.Log.WriteLine("Attachment " + ":  (" + fileName + ")  Deleted from the Attachments Tab, for:" + " " + prTitle);
                }
                else
                {
                    //Throw the Exception, if Attachment is Not Deleted
                    throw new Exception("Attachment:  (" + fileName + ")  not deleted for :" + prTitle + ".  Verification Failed !! ");
                }
            }
            catch (Exception e)
            {
                //Give the error message in case Delete Dialog not handled
                throw new Exception("Delete Dialog is Not Handled:" + " : " + e);
            }
        }

        //Method to Edit Attachment to a Good Receipt
        public void P2PGoodsReceiptsEditAttachment(string fileName, string description, string prTitle)
        {
            //Wait for My Tasks Goods Receipts Attachments Tab to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_AttachmentListTabitem.Wait.ForExists(Globals.timeOut);

            //Click on My Tasks Goods Receipts Attachments Tab
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_AttachmentListTabitem.User.Click();
            
            try
            {
                //Put the ListBox into a variable fe.
                FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_InvoiceDetails_Attachment_AttachmentListbox;

                //Check whether any TextBlock in the ListBox contains the specified string.
                bool found = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(fileName));

                // if Condition is true then code execute under the if Block
                if (found == true)
                {
                    //Wait for Attachments Edit Button Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_AttachmentsEdit_Button.Wait.ForExists(Globals.timeOut);

                    //Click on Attachments Edit Button
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_AttachmentsEdit_Button.User.Click();

                    //Wait for AddComment_TextBox Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);

                    // Set Focus on Description Text Box
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.SetFocus();

                    //Hold the Ctrl Key
                    Manager.Current.Desktop.KeyBoard.KeyDown(Keys.ControlKey);

                    //Press the A Key
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.A);

                    //Press the Delete Key
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Back);

                    //Release the Ctrl Key
                    Manager.Current.Desktop.KeyBoard.KeyUp(Keys.ControlKey);

                    //Enter the Text in Description TextBox
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(description, 20, true);

                    //Wait for OK button to Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_OKButton.Wait.ForExists(Globals.timeOut);

                    //Click on OK button on Add Attachment Dialog Box 
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_OKButton.User.Click();
                }
                else
                {
                    //Throw the Exception, if Attachment not exist in the section
                    throw new Exception("Attachment:  (" + fileName + ")  Not Found!" + "against Number: " + prTitle);
                }
            }

            catch (Exception e)
            {
                //Give the error message in case Edit Dialog not handled
                throw new Exception("Edit Dialog is Not Handled:" + " : " + e);
            }
        }
        
        //Method to Receive all goods receipts
        public void P2PGoodsReceiptsFullyReceived(string receiveButton = null, string receiveDialog = null)
        {
            if (receiveDialog == null)
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
            }

            if (receiveButton == null)
            {
                //Wait for grid 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ManualMatching_GoodsReceiptGrid.Wait.ForExists(Globals.timeOut);

                //Log the no of lines showing in receive dialog
                Manager.Current.Log.WriteLine(LogType.Information, "Lines to be Received:" + SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ManualMatching_GoodsReceiptGrid.Rows.Count());

                //Wait for P2P_Invoice_Administration_AddComment_OKButton button to Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

                //Click on P2P_Invoice_Administration_AddComment_OKButton button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

                //Need to add static wait as Busyindicator doesnt work here
                System.Threading.Thread.Sleep(Globals.timeOut);
            }
        }

        //Method to Select One line in PR Panel( to partially receive GR: MY Task)
        public void P2PGoodsReceiptsSelectLine(int prRowNumber)
        {          
            //Wait for Tab item to exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PRLines_TabItem.Wait.ForExists(Globals.timeOut);

            //Click on Tab item 
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PRLines_TabItem.User.Click();

            //Wait for PR Panel List View Grid Control to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PRLines_ListViewGrid.Wait.ForExists(Globals.timeOut);

            //Click on the grid      
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PRLines_ListViewGrid.User.Click();

            //Select any one row on grid 
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PRLines_ListViewGrid.Rows[prRowNumber].MouseClick(MouseClickType.LeftDoubleClick, 1, 1);
        }

        //Method to Select One line in PR Panel( to partially receive GR: MY Task)
        public void P2PGoodsReceiptsReceived(string prRowNumber = null, string product = null, string partialQuantityReceived = null)
        {
            bool found = false;

            if (prRowNumber != null)
            {
                //Wait for Tab item to exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PRLines_TabItem.Wait.ForExists(Globals.timeOut);

                //Click on Tab item 
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PRLines_TabItem.User.Click();

                //Wait for PR Panel List View Grid Control to Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PRLines_ListViewGrid.Wait.ForExists(Globals.timeOut);

                //Click on the grid      
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PRLines_ListViewGrid.User.Click();

                //Select any one row on grid 
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PRLines_ListViewGrid.Rows[int.Parse(prRowNumber)].MouseClick(MouseClickType.LeftDoubleClick, 1, 1);

            }
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
            
            //Get grid in RadGridView variable
            RadGridView grid;

            do
            {
                //Wait for grid to load
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ManualMatching_GoodsReceiptGrid.Wait.ForExists(Globals.timeOut);

                //Get grid in RadGridView variable
                grid = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ManualMatching_GoodsReceiptGrid;

                //Get grid in RadGridView variable
                found = grid.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(product));

            } while (found == false);

            //To Receive Less quantity than Ordered one for Single PR Line
            if (partialQuantityReceived != null)
            {
                //Get grid in RadGridView variable
                grid = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ManualMatching_GoodsReceiptGrid;

                //Create an object for GridViewRow class
                GridViewRow gvr = grid.Rows[grid.Rows.Count - 1];

                //Left Click on Received Now Cell
                gvr.Cells[3].User.Click(MouseClickType.LeftDoubleClick);

                //Get and save Data
                double getData = Convert.ToDouble(gvr.Cells[3].Value);

                //Receive half of the full amount
                double typeData = (getData / 2);

                //Select the String in Text Box
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

                //Generate a Keyboard Event to clear search
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

                //Input data in Received Now Cell
                gvr.Cells[3].User.TypeText(typeData.ToString(CultureInfo.CurrentCulture.NumberFormat), 50);

                //Getting and Saving Data in Global Variable
                P2P_Utility.FetchingDataFromUI(typeData, "Received Quantity");
            }

            //Log the no of lines showing in receive dialog
            Manager.Current.Log.WriteLine(LogType.Information, "Lines to be Received:" + SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ManualMatching_GoodsReceiptGrid.Rows.Count());

            //Wait for P2P_Invoice_Administration_AddComment_OKButton button to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on P2P_Invoice_Administration_AddComment_OKButton button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            //Need to add static wait as Busyindicator doesnt work here
            System.Threading.Thread.Sleep(Globals.timeOut);
        }
    }
}