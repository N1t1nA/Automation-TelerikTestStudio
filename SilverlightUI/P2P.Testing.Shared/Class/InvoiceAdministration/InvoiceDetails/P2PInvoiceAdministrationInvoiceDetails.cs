using System;
using System.Linq;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Win32.Dialogs;
using ArtOfTest.WebAii.Silverlight.UI;
using System.Windows.Forms;
using System.Diagnostics;
using Telerik.WebAii.Controls.Xaml;
using ArtOfTest.WebAii.Win32;
using System.Globalization;

namespace P2P.Testing.Shared.Class.InvoiceAdministration.InvoiceDetails
{
    public class P2PInvoiceAdministrationInvoiceDetails
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

        //Method for Find and Kill the Process (Acrobat Reader)
        public bool FindAndKillProcess(string processName)
        {
            //Get a list of all Running Processes on the Machine
            foreach (Process clsProcess in Process.GetProcesses())
            {
                //Check if any of the Running Processes
                if (clsProcess.ProcessName.StartsWith(processName))
                {
                    //Since We found the proccess we now need to use the
                    //Kill Method to kill the process. 
                    Manager.Current.Log.WriteLine(LogType.Information, "Acrobat Reader Opened Successfully, Invoice Printing Succeeded!");
                    clsProcess.Kill();
                    //Process Killed, return true
                    return true;
                }
            }
            //Process Not Found, return false
            return false;
        }

        //Method to Remove the Invoice
        public void P2PInvoiceAdministrationRemoveInvoice(string comment)
        {
            //Wait for Addtional Actions Dropdown Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Click on Addtional Actions Dropdown 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            //Wait for CancelInvoice Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_CancelInvoice_DropdownButton.Wait.ForExists(Globals.timeOut);

            //Click on CancelInvoice Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_CancelInvoice_DropdownButton.User.Click();

            //Wait for Text Box Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);

            //Click to Add Comment while Canceling the Invoice
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(comment.ToString(), 50, true);

            //Wait for OK Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK after adding Comments
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
        }

        //Method to Collaborate 
        public void P2PInvoiceAdministrationCollaborate(string selectUser, string collaboratemessage)
        {

            //Wait for Collaborate Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CollaborateButton.Wait.ForExists(Globals.timeOut);

            //Click on  Collaborate Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CollaborateButton.User.Click();

            //Wait for 'OK' button to enable
            System.Threading.Thread.Sleep(Globals.handleTime);

            //Wait for Grid to load
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_Collaborate_SelectUsersGrid.Wait.ForExists(Globals.timeOut);

            //Wait for Search User Text Box Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_SearchUserTextbox.Wait.ForExists(Globals.timeOut);

            //Enter User name in Search User Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_SearchUserTextbox.User.TypeText(selectUser, 50, true);

            //Click on Search User Button to Search User
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SelectRecipient_SearchButton.User.Click();

            //Wait for Grid to load
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_Collaborate_SelectUsersGrid.Wait.ForExists(Globals.timeOut);

            //Refresh Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_Collaborate_SelectUsersGrid.Refresh();

            //Declare 'userCell' as TextBlock variable
            TextBlock userCell = null;

            //Find the User Name and Select From the Grid 
            userCell = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(selectUser).As<TextBlock>();

            //Press Enter the Key
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);

            //Wait for ok button to be enabled
            P2PNavigation.WaitForPopUpOkButtonEnabled();

            //Click on OK Button to Close the Select Users Pop-Up
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            //Handle Busy Indicator                
            P2PNavigation.CallBusyIndicator();

            //@7 Aug 2013:Harmeet: Busy indicator doesnt show after this step. Need to add static wait here.
            //@13 Sept 2013: Harmeet: Globals.handleTime didnt wait for long enough. So will have to increase wait time.
            System.Threading.Thread.Sleep(Globals.navigationTimeOut);

            //Wait for Toggle Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_Collaborate_StartToggleButton.Wait.ForExists(Globals.navigationTimeOut);

            //Check the Toggle State
            bool checkToggleState = SharedElement.P2P_Application.SilverlightApp.P2P_Main_Collaborate_StartToggleButton.IsChecked.Value;

            //If Condition is False
            if (!checkToggleState)
            {
                //Click on Collaborate Toggle 
                SharedElement.P2P_Application.SilverlightApp.P2P_Main_Collaborate_StartToggleButton.User.Click();
            }

            //Wait for Chat Textbox for Visible
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_CollaboratePro_ChatMessageTextbox.Wait.ForVisible(Globals.navigationTimeOut);

            //Type a Message to Chat with Group
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_CollaboratePro_ChatMessageTextbox.User.TypeText(collaboratemessage.ToString(), 20, true);

            //Press Enter Button 
            Manager.Current.Desktop.KeyBoard.KeyDown(Keys.Enter);

            //Click on Hyperlink To Navigate Back To Collaborate Page
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_CollaboratePro_InvoiceHyperlink.Wait.ForExists(Globals.timeOut);

            //Click on Hyperlink To Navigate Back To Collaborate Page
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_CollaboratePro_InvoiceHyperlink.User.Click();
        }

        //Method to Cancel the Collaborate 
        public void P2PInvoiceAdministrationCancelCollaborate()
        {
            //Wait for Collaborate Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CollaborateButton.Wait.ForExists(Globals.timeOut);

            //Click on Collabirate Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CollaborateButton.User.Click();

            //Wait for Cancel Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_CancelButton.Wait.ForExists(Globals.timeOut);

            //Click on Cancel Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_CancelButton.User.Click();

            //Log the results.
            Manager.Current.Log.WriteLine(LogType.Information, "  Collaborate Process Cancelled!");
        }

        //Method to Print the Invoice 
        public void P2PInvoiceAdministrationPrintInvoice(Boolean basicData, Boolean invoiceHistory, Boolean codingRows, Boolean images, string processName, Boolean allImages = true)
        {
            //Wait for Addtional Actions Dropdown Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Click on Addtional Actions Dropdown 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            //Wait for Print Invoice Dropdown Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_PrintInvoice_Button.Wait.ForExists(Globals.timeOut);

            //Click on Print Invoice Dropdown Button to Print the Invoice
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_PrintInvoice_Button.User.Click();

            //Handle Busy Indicator                
            P2PNavigation.CallBusyIndicator();

            //Use if condition to Check State of basicData Checkbox
            if (basicData == false)

                //UnCheck Basic Data Checkbox  
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Print_BasicDataCheckbox.UnCheck(true);

            //Use if condition to Check State of invoiceHistory Checkbox
            if (invoiceHistory == false)

                //UnCheck Invoice Checkbox  
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Print_InvoiceHistoryCheckbox.UnCheck(true);

            //Use if condition to Check State of codingRows CheckBox
            if (codingRows == false)

                //UnCheck Coding Rows  
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Print_CodingRowsCheckbox.UnCheck(true);

            //Use if condition to Check State of images CheckBox 
            if (images == false)
            {
                //Handle Busy Indicator                
                P2PNavigation.CallBusyIndicator();
            }

            else
            {
                //Click on Current Images Radio Button 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Print_CurrentImage_RadioButton.User.Click();
            }

            //Handle the Download Dialogs to Open the PDF File, In this Case Location is %Temp%, as in DialogButton.OPEN Location is Ignored
            DownloadDialogsHandler handler = new DownloadDialogsHandler(Manager.Current.ActiveBrowser, DialogButton.OPEN, "%Temp%", Manager.Current.Desktop);

            try
            {
                //Click on Preview Button for the Print.
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Print_PreviewButton.User.Click();

                //Handle Busy Indicator                
                P2PNavigation.CallBusyIndicator();

                //Handle the Dialog
                handler.WaitUntilHandled(Globals.handleTime);
            }

            catch (Exception Ex)
            {
                //Write the log if Print Invoice Fails
                Manager.Current.Log.WriteLine(LogType.Error, "Unable to Print the Invoice" + Ex.Message);
            }
        }

        //Method to Click on Back Button 
        public void P2PInvoiceAdministrationBackToMainPage()
        {
            //Wait for Back Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_BackButton.Wait.ForExists(Globals.timeOut);

            //Settings Focus OnBeforeUnloadDialog Back Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_BackButton.SetFocus();

            //Click on Back Button to go Back to Transfer Page            
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_BackButton.User.Click();
        }

        //Method to Cancel the Process
        public void P2PInvoiceAdministrationCancelProcess(string comment)
        {

            //Wait for Addtional Actions Dropdown Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Click on Addtional Actions Dropdown 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            //Wait for Cancel Invoice Process Dropdown Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CancelProcess_DropdownButton.Wait.ForExists(Globals.timeOut);

            //Click on Cancel Invoice Process
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CancelProcess_DropdownButton.User.Click();

            //Click to Add Comment while Canceling the Invoice
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(comment.ToString(), 20, true);

            //Wait for OK Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            // Click on OK after adding Comments
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
        }

        //Method to Add Attachment 
        public void P2PInvoiceAdministrationAddAttachment(string filePath, string description, string paymentPlanScreen = null, string myTasks = null)
        {
            if (paymentPlanScreen != null)
            {
                // Wait for Attachment Tab Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_InvoiceDetails_AttachmentsTabItem.Wait.ForExists(Globals.timeOut);

                //Click on Attachment Tab Items in Invoice Details Page
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_InvoiceDetails_AttachmentsTabItem.User.Click();
            }
            else
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
                    SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=AttachmentTab")).Wait.ForExists(Globals.timeOut);

                    //Click on Attachment Tab Items in Invoice Details Page
                    SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=AttachmentTab")).User.Click();
                }
            }

            //Wait for Add Attachment Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddButton.Wait.ForExists(Globals.timeOut);

            //setFocus on Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddButton.SetFocus();

            //Click on Attachment Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddButton.User.Click();

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
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(description, 80);

            //Wait for OK button to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK button on Add Attachment Dialog Box 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_OKButton.User.Click();

            //System wait
            System.Threading.Thread.Sleep(Globals.timeOut);
        }

        //Method to Delete Attachment 
        public void P2PInvoiceAdministrationDeleteAttachment(string fileName, string comments, string invoiceID)
        {
            //Wait for Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_AttachmentsTabItem.Wait.ForExists(Globals.timeOut);

            //Navigate to Attachment Section of Invoice Details Page
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_AttachmentsTabItem.User.Click();

            //Handle Busy Indicator                
            P2PNavigation.CallBusyIndicator();

            try
            {
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

                    //Wait for AddComment_TextBox to load in Dom
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);

                    //Enter the Comment in Delete Attachment Box 
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(comments, 20, true);

                    //Wait for OK Button Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.pause);

                    //Click on OK Button after added a comment
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

                    //Log the result if Attachment Deleted
                    Manager.Current.Log.WriteLine("Attachment " + ":  (" + fileName + ")  Deleted from the Attachments Tab, for:" + " " + invoiceID);
                }
                else
                {
                    //Throw the Exception, if Attachment is Not Deleted
                    throw new Exception("Attachment:  (" + fileName + ")  not deleted for :" + invoiceID + ".  Verification Failed !! ");
                }
            }
            finally
            {
                //Handle Busy Indicator                
                P2PNavigation.CallBusyIndicator();
            }
        }

        //Method to Edit Attachment 
        public void P2PInvoiceAdministrationEditAttachment(string fileName, string description, string invoiceID, string paymentPlanScreen = null)
        {
            //Wait for Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_AttachmentsTabItem.Wait.ForExists(Globals.timeOut);

            //Navigate to Attachment Section of Invoice Details Page
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_AttachmentsTabItem.User.Click();

            try
            {
                //Put the ListBox into a variable.
                FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_InvoiceDetails_Attachment_AttachmentListbox;

                //Check whether any TextBlock in the ListBox contains the specified string.
                bool found = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(fileName));

                // if Condition is true then code execute under the if Block
                if (found == true)
                {

                    if (paymentPlanScreen != null)
                    {
                        //Wait for Edit Attachment button Exists in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_DetailsPage_EditAttachmentButton.Wait.ForExists(Globals.timeOut);

                        //Click on Edit Attachment button
                        SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_DetailsPage_EditAttachmentButton.User.Click();
                    }
                    else
                    {
                        //Wait for Exists in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_AttachmentsEdit_Button.Wait.ForExists(Globals.timeOut);

                        //Navigate to Attachment Section of Invoice Details Page
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_AttachmentsEdit_Button.User.Click();
                    }

                    //Wait for AddComment_TextBox Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);

                    // Set Focus on Description Text Box
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.SetFocus();

                    //Hold the Ctrl Key
                    Manager.Current.Desktop.KeyBoard.KeyDown(Keys.ControlKey);

                    //Press the A Key
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.A);

                    //Press the Delete Key
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Delete);

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
                    throw new Exception("Attachment:  (" + fileName + ")  Not Found!!!!!!" + "against Number:" + invoiceID);
                }
            }

            finally
            {
                //Call handle busy indicator
                P2PNavigation.CallBusyIndicator();
            }
        }

        //Method to close Confirmation Dialog Box
        public void P2PCloseConfirmationDialogBox()
        {
            //Wait for Cross Button Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.Wait.ForExists(Globals.timeOut);

            //Click on Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.User.Click();
        }

        //Method to Confirmation Dialog Box
        public void P2PInvoiceAdministrationConfirmationDialogBox(Boolean confirmation)
        {
            P2PNavigation.WaitForPopUpOkButtonEnabled();

            if (confirmation == true)
            {
                //Wait for Confirmation Yes Button Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

                //setfocus on the button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.SetFocus();

                //Click on Yes Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click(MouseClickType.LeftClick);
            }

            else
            {
                //Wait for Confirmation Cancel Button Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_CancelButton.Wait.ForExists(Globals.timeOut);

                //setfocus on the button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_CancelButton.SetFocus();
                //Click on Cancel Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_CancelButton.User.Click(MouseClickType.LeftClick);
            }
        }

        //Method to Modify and Save Invoice
        public void P2PInvoiceAdministrationModifyHeaderDataAndSaveInvoice(Boolean prebook, Boolean modifyHeaderData, Boolean save, string supplierCode = null, string currencyCode = null, string paymentPlan = null)
        {
            if (paymentPlan == null)
            {
                //Wait for Header Data Tab Item Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.Wait.ForExists(Globals.timeOut);

                // Click on Header Data Tab Item Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.User.Click();
            }

            if (prebook == true)
            {
                //Wait for InvoiceDate_TextBox Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_InvoiceDate_TextBox.Wait.ForExists(Globals.timeOut);

                //Get the Runtime Value of InvoiceDate_TextBox
                string date = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_InvoiceDate_TextBox.Text.ToString();

                //If the Condition is true then execute if block
                if (date != System.DateTime.Now.ToShortDateString())
                {
                    //Type the Current System Date on InvoiceDate_TextBox
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_InvoiceDate_TextBox.User.TypeText(System.DateTime.Now.ToShortDateString().ToString(CultureInfo.CurrentCulture.NumberFormat), 50);

                    //Keyboard Action
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
                }
            }

            if (modifyHeaderData == true)
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

                //Declare Interger variable
                int i = 500;

                //Wait for InvoiceType ComboBox Exists in DOM
                while (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SelectSupplier_OKButton.IsEnabled == false)
                {
                    //Wait for Page load Properly
                    System.Threading.Thread.Sleep(i);

                    i++;
                    if (i == 510)
                    {
                        break;
                    }
                }

                //Click on OK Button to Select  the supplierCode
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SelectSupplier_OKButton.User.Click();

                //Wait for Globals.timeOut milisec
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_InvoiceDate_TextBox.Wait.ForVisible(Globals.timeOut);

                //Enter the System Date in Invoice Date Text Box           
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_InvoiceDate_TextBox.User.TypeText(DateTime.Now.ToShortDateString().ToString(CultureInfo.CurrentCulture.NumberFormat), 50);

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

                if (save == true)
                {
                    //Click on Save_Button to Save the Modify Invoice
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.User.Click();
                }
            }
        }

        //Method to Navigate the Second Invoice
        public void P2PInvoiceAdministrationInvoiceNavigation(Boolean workflow, Boolean transfer, Boolean matching)
        {
            //If condition is true then execute if block
            if (workflow == true)
            {
                //Wait for InvoiceDetails_NAVNextButton Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_InvoiceDetails_NAVNextButton.Wait.ForExists(Globals.timeOut);

                //Click on InvoiceDetails_NAVNextButton
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_InvoiceDetails_NAVNextButton.User.Click();
            }

            if (transfer == true)
            {
                //Wait for InvoiceDetails_NAVNextButton Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_InvoiceDetails_NAVNextButton.Wait.ForExists(Globals.timeOut);

                //Click on InvoiceDetails_NAVNextButton
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_InvoiceDetails_NAVNextButton.User.Click();
            }

            if (matching == true)
            {
                //Wait for InvoiceDetails_NAVNextButton Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=NextButton")).Wait.ForExists(Globals.timeOut);

                //Click on InvoiceDetails_NAVNextButton
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=NextButton")).User.Click();
            }
        }

        //Method to Select Unclear Secondary Status
        public void P2PSelectUnclearSecondaryStatusInvoice(string secondaryStatus)
        {
            //Wait for Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_Status_UnclearCheckBox.Wait.ForExists(Globals.timeOut);

            //Focus on "In Transfer" CheckBoxes
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_Status_UnclearCheckBox.SetFocus();

            //Click on "In Transfer" CheckBoxes
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_Status_UnclearCheckBox.Check(true);

            //Find Invoice Using Secondary Status
            TextBlock invoice1 = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(secondaryStatus).As<TextBlock>();

            //Click One Invoice to Clear the Default Selection
            invoice1.User.Click();

            //Press the Control Key down
            Manager.Current.Desktop.KeyBoard.KeyDown(Keys.Control);

            //Press the Control Key down
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.A);

            //Release the Control Key
            Manager.Current.Desktop.KeyBoard.KeyUp(Keys.Control);
        }

        //Method to Export Invoice to Excel Coding Rows 
        public void P2PInvoiceAdministrationCodingRowsExportToExcel(string activeDirectory, string filePath)
        {
            //Wait for Coding Rows Tab Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_InvocieDetails_CodingRowTab.Wait.ForExists(Globals.timeOut);

            //Click on Coding Rows Tab
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_InvocieDetails_CodingRowTab.User.Click();


            //Create a Directory Under C: drive with Name TestAutomation
            System.IO.Directory.CreateDirectory(activeDirectory);

            //Change the Current Settings 
            Manager.Current.Settings.UnexpectedDialogAction = UnexpectedDialogAction.DoNotHandle;


            //Handle SaveAs Dialog 
            SaveAsDialog saveAsDialog = SaveAsDialog.CreateSaveAsDialog(Manager.Current.ActiveBrowser, DialogButton.SAVE, filePath, Manager.Current.Desktop);

            //Add the SaveAs Dialog
            Manager.Current.DialogMonitor.AddDialog(saveAsDialog);

            //Initiated the DialogMonitor
            Manager.Current.DialogMonitor.Start();

            //Wait for ExportToExcel Toolbarbutton Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_InvocieDetails_ExportToExcelButton.Wait.ForExists(Globals.timeOut);

            //Click ExportToExcelToolbarbutton
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_InvocieDetails_ExportToExcelButton.User.Click();

            //Handle the saveAsDialog
            saveAsDialog.WaitUntilHandled(Globals.navigationTimeOut);

            //Stops the DialogMonitor
            Manager.Current.DialogMonitor.Stop();
        }

        //Method to Delete Coding Rows
        public void DeleteCodingRows(string headerCell, Boolean keyBoardDelete, string deletecontextMenu = null)
        {
            //Wait for Coding Rows Tab Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_InvocieDetails_CodingRowTab.Wait.ForExists(Globals.timeOut);
            //Click on Coding Rows Tab
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_InvocieDetails_CodingRowTab.User.Click();

            //Wait for Grid Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Wait.ForExists(Globals.timeOut);

            //Put the Header Cell into a variable
            TextBlock invoiceNumberHeaderCell = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Find.ByTextContent(headerCell).As<TextBlock>();

            //Click on Invoice Number Header Cell for Sorting the Coding Rows
            invoiceNumberHeaderCell.User.Click();

            //Find the Coding Row using AutomationID                   
            RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl;

            //Create an object for GridViewRow class
            GridViewRow gvr;

            if (rgv.Rows.Count > 0)
            {
                //Read always newly added row
                gvr = rgv.Rows[rgv.Rows.Count - 1];

                //Click on Cell for focus in a grid
                gvr.Cells[10].User.Click();

                //If condition is true then execute if block
                if (keyBoardDelete == true)
                {
                    //Select on Net Sum Header Cell to Release Control from Textblock
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Find.ByTextContent(headerCell).As<TextBlock>().User.Click();

                    /* CODE FOR MULTIPLE SELECTION */
                    //Press control Key for selection
                    Manager.Current.Desktop.KeyBoard.KeyDown(Keys.Control);

                    //Press key A for select all Coding Rows
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.A);

                    //Release control Key for selection
                    Manager.Current.Desktop.KeyBoard.KeyUp(Keys.Control);

                    //Press to Delete Key
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Delete);

                    //Wait for No button Exist in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_CancelButton.Wait.ForExists(Globals.timeOut);

                    //Click on No Button
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_CancelButton.User.Click();

                    //Select on Net Sum Header Cell to Release Control from Textblock
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Find.ByTextContent(headerCell).As<TextBlock>().User.Click();

                    //Press to Delete Key
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Delete);

                    //Wait for Yes Button exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

                    //Click on Yes button
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

                    //Wait for Save Button exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.Wait.ForExists(Globals.timeOut);

                    //Click on Save Button
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.User.Click();

                    //Handle Busy Indicator 
                    P2PNavigation.CallBusyIndicator();

                    //Write a log in Result file
                    Manager.Current.Log.WriteLine(LogType.Information, "Multiple Coding Rows deleted Successfully through KeyBoard");
                }

                //If condition is true then execute if block
                if (deletecontextMenu != null)
                {
                    //Select on Net Sum Header Cell to Release Control from Textblock
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Find.ByTextContent(headerCell).As<TextBlock>().User.Click();

                    /* CODE FOR MULTIPLE SELECTION */
                    //Press control Key for selection
                    Manager.Current.Desktop.KeyBoard.KeyDown(Keys.Control);
                    //Wait for a seconds to before selection a KeyDown A
                    System.Threading.Thread.Sleep(Globals.pause);
                    //Press key A for select all Coding Rows
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.A);
                    //Release control Key for selection
                    Manager.Current.Desktop.KeyBoard.KeyUp(Keys.Control);

                    //Click on Cell for focus in a grid
                    gvr.Cells[10].User.Click(MouseClickType.RightClick);

                    //Wait for a seconds to before Context Menu Pop Up Appear on the screen
                    System.Threading.Thread.Sleep(Globals.timeOut);

                    //Wait for ContextMenuControl exist in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRow_ContextMenuControl.Wait.ForExists(Globals.timeOut);
                    //Click on Delete Row Menu items in the grid
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRow_ContextMenuControl.Find.ByTextContent(deletecontextMenu).As<TextBlock>().User.Click();

                    //Wait for Yes button appear in the page
                    System.Threading.Thread.Sleep(Globals.timeOut);

                    //Wait for Yes Button exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);
                    //Click on Yes button
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

                    //Wait for Save Button exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.Wait.ForExists(Globals.timeOut);
                    //Click on Save Button
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.User.Click();

                    //Handle Busy Indicator 
                    P2PNavigation.CallBusyIndicator();

                    //Write a log in Result file
                    Manager.Current.Log.WriteLine(LogType.Information, "Multiple Coding Rows deleted Successfully through Mouse Right Click Context Menu");
                }
            }
            else
            {
                //Write a log in Result file
                Manager.Current.Log.WriteLine(LogType.Information, "There are no Coding Rows. Number of coding Rows are '" + rgv.Rows.Count + "'");
            }
        }

        //Method to Save Invoice as Draft
        public void P2PInvoiceAdministrationClickOnSaveasDraftButton()
        {
            //Wait for SendToValidation Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_SaveAsDraftButton.Wait.ForExists(Globals.timeOut);

            // Click on Save As Draft Button to Save the Modificaton in Header Data
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_SaveAsDraftButton.User.Click();
        }

        //Method to Collaborate By Using Discussion Link
        public void P2PInvoiceAdministrationCollaborateAction()
        {
            //Call the Busy Indicator
            P2PNavigation.CallBusyIndicator();

            //AutomationId=~AddFirstNewImageButton
            FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=~ActiveDiscussion"));

            //Click On Discussion Link    
            fe.User.Click();

            //Handle Busy Indicator
            P2PNavigation.CallBusyIndicator();
        }

        //Method to Go to Silo From Collaborate Page
        public void P2PInvoiceAdministrationFromCollaborateBackToSilo()
        {
            //Click on Hyperlink To Navigate Back To Collaborate Page
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_CollaboratePro_InvoiceHyperlink.Wait.ForExists(Globals.timeOut);

            //Click on Hyperlink To Navigate Back To Collaborate Page
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_CollaboratePro_InvoiceHyperlink.User.Click();
        }

        //Method to Enter the Reference Number
        public void P2PInvoiceAdministrationReferenceNumber(string referenceNumber, string currencyCode = null)
        {
            //Wait for HeaderData_TabTabItem Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.Wait.ForExists(Globals.timeOut);

            //Click on HeaderData_TabTabItem
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.User.Click();

            //Wait for HeaderData_TabTabItem Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_ReferenceNumber_TextBox.Wait.ForExists(Globals.timeOut);

            //Set focus on ReferenceNumber_TextBox Text Box   
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_ReferenceNumber_TextBox.SetFocus();
            //Set focus on the text block
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_ReferenceNumber_TextBox.User.Click();

            //Select the String in Text Box
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

            //Generate a Keyboard Event to clear search
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

            //Wait for ReferenceNumber_TextBox Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_ReferenceNumber_TextBox.Wait.ForExists(Globals.timeOut);

            //Enter the Data in ReferenceNumber_TextBox Text Box            
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_ReferenceNumber_TextBox.User.TypeText(referenceNumber, 50);

            if (currencyCode != null)
            {
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

        }

        //Method to Send the Invoice for Validation
        public void P2PInvoiceAdministrationSendToValidation()
        {
            //Wait for SendToValidation Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SendToValidationButton.Wait.ForExists(Globals.timeOut);

            //Click on SendToValidation Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SendToValidationButton.User.Click(MouseClickType.LeftDoubleClick);
        }

        //Method to Close Unsaved data warning dialog
        public void P2PInvoiceAdministrationCloseDialog()
        {
            //Wait for Close Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.Wait.ForExists(Globals.timeOut);

            //Set focus on Close Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.SetFocus();

            //Click on Close Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetailsPanel_HistoryandAllCommentsPopup_CloseButton.User.Click();
        }

        //Method to Enter the Reference Number
        public void P2PInvoiceAdministrationESRReferenceNumber(string esrReferenceNumber)
        {
            //Wait for HeaderData_TabTabItem Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.Wait.ForExists(Globals.timeOut);

            //Click on HeaderData_TabTabItem
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.User.Click();

            //Wait for HeaderData_TabTabItem Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.MyTask_Create_Invoice_Text3_HeaderDataTextBox.Wait.ForExists(Globals.timeOut);

            //Set focus on ReferenceNumber_TextBox Text Box   
            SharedElement.P2P_Application.SilverlightApp.MyTask_Create_Invoice_Text3_HeaderDataTextBox.SetFocus();

            //Select the String in Text Box
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

            //Generate a Keyboard Event to clear search
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

            //Wait for ReferenceNumber_TextBox Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.MyTask_Create_Invoice_Text3_HeaderDataTextBox.Wait.ForExists(Globals.timeOut);

            //Enter the Data in ReferenceNumber_TextBox Text Box            
            SharedElement.P2P_Application.SilverlightApp.MyTask_Create_Invoice_Text3_HeaderDataTextBox.User.TypeText(esrReferenceNumber, 50);

            //Generate a Keyboard Event
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Tab);
        }

        //Method to Select Supplier from Supplier Picker window
        public void P2PSelectSupplierFromPickerWindow(string supplier, string dontClickOk = null)
        {
            //Wait for element to Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierCode_SelectionButton.Wait.ForExists(Globals.timeOut);

            //Click on element  
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierCode_SelectionButton.User.Click();

            //Wait for element to Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_SelectSuppliers_AllSuppliers_TabItem.Wait.ForExists(Globals.timeOut);

            //Click on element  
            SharedElement.P2P_Application.SilverlightApp.P2P_SelectSuppliers_AllSuppliers_TabItem.User.Click();

            //Wait for Supplier Search Textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SupplierSearch_TextBox.Wait.ForExists(Globals.navigationTimeOut);

            //Click to set a focus on Search Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SupplierSearch_TextBox.User.Click();

            //Empty textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SupplierSearch_TextBox.Text = string.Empty;

            //Enter the Data in Search TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SupplierSearch_TextBox.TypeText(supplier, 50);

            //Wait for Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administartion_SuppliersSearchButton.Wait.ForExists(Globals.navigationTimeOut);

            //Click on Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administartion_SuppliersSearchButton.User.Click();

            //Wait for element to Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SuppliersListGridViewControl.Wait.ForExists(Globals.timeOut);

            //Find the Supplier and Select From the Grid 
            TextBlock selectsupplier = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SuppliersListGridViewControl.Find.ByTextContent(supplier).As<TextBlock>();

            if (dontClickOk == null)
            {
                //Select the Supplier by the User
                selectsupplier.User.Click();
                //Wait for ok Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);
                //Click on OK Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
            }
            else
            {
                //Select the Supplier by the User
                selectsupplier.User.Click(MouseClickType.LeftDoubleClick);
            }
        }

        //Method to Add Supplier from Supplier Picker window to Favorites Tab
        public void P2PAddSupplierInFavoritesTab(string supplier, string dontClickOk = null)
        {
            //Call method to search a supplier in All suppliers tab.
            P2PSelectSupplierFromPickerWindow(supplier, supplier);

            //Add the selected supplier to Favorites Tab
            RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SuppliersListGridViewControl;

            //Create an object for GridViewRow class
            GridViewRow gvr;

            //Read always newly added row
            gvr = rgv.Rows[rgv.Rows.Count - 1];

            //Click on Cell which has AddFavButton
            gvr.Cells[4].User.Click();
        }

        //Method to Search Supplier from Favorites Tab in  Supplier Picker window
        public void P2PSearchSupplierInFavoritesTab(string supplier)
        {
            //Wait for element to Exist in DOM
            SharedElement.P2P_Application.SilverlightApp._P2P_SelectSuppliers_Favorite_TabItem.Wait.ForExists(Globals.timeOut);

            //Click on element  
            SharedElement.P2P_Application.SilverlightApp._P2P_SelectSuppliers_Favorite_TabItem.User.Click();

            //Wait for Supplier Search Textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_SelectSuppliers_Favorites_SupplierTextBox.Wait.ForExists(Globals.navigationTimeOut);

            //Click to set a focus on Search Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_SelectSuppliers_Favorites_SupplierTextBox.User.Click();

            //Empty textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_SelectSuppliers_Favorites_SupplierTextBox.Text = string.Empty;

            //Enter the Data in Search TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_SelectSuppliers_Favorites_SupplierTextBox.TypeText(supplier, 50);

            //Wait for Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_SelectSuppliers_Favorites_SearchButton.Wait.ForExists(Globals.navigationTimeOut);

            //Click on Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_SelectSuppliers_Favorites_SearchButton.User.Click();

            //Wait for element to Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_SelectedSuppliers_FavoritesGridViewControl.Wait.ForExists(Globals.timeOut);

            //Find the Supplier and Select From the Grid 
            TextBlock selectsupplier = SharedElement.P2P_Application.SilverlightApp.P2P_SelectedSuppliers_FavoritesGridViewControl.Find.ByTextContent(supplier).As<TextBlock>();

            //Select the Supplier by the User
            selectsupplier.User.Click(MouseClickType.LeftDoubleClick);
        }

        //Method to Remove Supplier from Supplier Picker window in Favorites Tab
        public void P2PRemoveSupplierInFavoritesTab(string supplier)
        {
            //Wait for element to Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_SelectedSuppliers_FavoritesGridViewControl.Wait.ForExists(Globals.timeOut);

            //Add the selected supplier to Favorites Tab
            RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_SelectedSuppliers_FavoritesGridViewControl;

            //Create an object for GridViewRow class
            GridViewRow gvr;

            //Read always newly added row
            gvr = rgv.Rows[rgv.Rows.Count - 1];

            //Click on Cell which has RemoveFavButton
            gvr.Cells[4].User.Click();
        }

        //Method to Select the Organisation and Invoice Type
        public void P2PInvoiceAdministrationCreateSelectOrganisation(string headerTab, string organizationUnit = null)
        {
            //Wait for HeaderData TabTabItem Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(headerTab)).Wait.ForExists(Globals.timeOut);

            //Click on Header Data Tab Item
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(headerTab)).User.Click();

            if (organizationUnit != null)
            {
                //Call handle busy indicator
                P2PNavigation.CallBusyIndicator();

                //Wait for OK Button Exist in DOM
                P2PNavigation.WaitForPopUpOkButtonEnabled();

                //Enter Organisation in Select Organization Textbox
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(headerTab)).User.TypeText(organizationUnit, 100);

                //Performing keyboard action
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
            }
        }

        //Method to delete PO Number of an Invoice
        public void P2PInvoiceDeletePONumber()
        {
            //Wait for  Tab Item Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.Wait.ForExists(Globals.timeOut);

            // Click on HeaderData Tab TabItem
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.User.Click();

            //Press Tab key
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Wait for Header Text Box Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_InvoiceDetails_PurchaseOrderNumber_HeaderDataTextbox.Wait.ForExists(Globals.timeOut);

            //Set Focus on Header Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_InvoiceDetails_PurchaseOrderNumber_HeaderDataTextbox.SetFocus();

            //Click on Header Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_InvoiceDetails_PurchaseOrderNumber_HeaderDataTextbox.User.Click();

            //Select the String in Text Box
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

            //Generate a Keyboard Event to clear search
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);
        }


        //Method to Select My Tasks Toolbar Button
        public void P2PSearchEditToolBarButtons(string button)
        {
            switch (button)
            {
                case "IA_Search":
                    //Wait for element to Exists in DOM Tree
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_EditToolBar_Button.Wait.ForExists(Globals.timeOut);

                    //Click on Collaborate Button
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_EditToolBar_Button.User.Click();
                    break;

                case "PaymentPaln_Search":
                    //Wait for Button exist in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_EditToolBar_Button.Wait.ForExists(Globals.timeOut);
                    //Click on button
                    SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_EditToolBar_Button.User.Click();
                    break;

                default:
                    //Log Failure for Tab item not found
                    Manager.Current.Log.WriteLine(LogType.Error, "ToolBar Buttons Not Found, Verification Failed!!!!");
                    break;
            }
        }

        //Method to select the Recipients
        public void P2PInvoiceAdministrationSelectRecipients(string selectUser, string forward = null)
        {
            if (forward != null)
            {
                //Wait for Additional Actions Dropdown button Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

                //Click the Additional Actions drop down
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

                //Click on Hold Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Find.ByTextContent(forward).User.Click();

                P2PNavigation.CallBusyIndicator();
            }

            SharedElement.P2P_Application.SilverlightApp.P2P_Settings_SearchUser_TextBox.Wait.ForExists(Globals.timeOut);
            //Click on Text box to set the focus
            SharedElement.P2P_Application.SilverlightApp.P2P_Settings_SearchUser_TextBox.User.Click();
            //Enter the Recipients Name n Text box
            SharedElement.P2P_Application.SilverlightApp.P2P_Settings_SearchUser_TextBox.User.TypeText(selectUser, 50);

            //Wait for Button exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SelectRecipient_SearchButton.Wait.ForExists(Globals.timeOut);
            //Click on Search button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SelectRecipient_SearchButton.User.Click();
            //Wait for OK button exists in DOM
            P2PNavigation.WaitForPopUpOkButtonEnabled();
            //Refresh the grid
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_Collaborate_SelectUsersGrid.Refresh();
            //Press enter to selected user moved to selection box
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Enter);
            //Wait for OK button exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);
            //Click on OK button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
        }

        //Method to create a Coding Rows in My Tasks 
        public void P2PInvoiceAdministrationMyTasksCreateCodingRows(int codingRow, string accountCode, string costCenterCode, double addNetSum, string taxCode)
        {
            //Workaround for application freeze
            System.Threading.Thread.Sleep(Globals.pause);
            //Wait for Add Row Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.Wait.ForExists(Globals.timeOut);
            //Set focus on the  button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.SetFocus();
            //Click on Add Row Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.User.Click(MouseClickType.LeftDoubleClick);

            //Workaround for application freeze
            System.Threading.Thread.Sleep(Globals.pause);

            //Find the Coding Row using AutomationID                   
            RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl;

            //Create an object for GridViewRow class
            GridViewRow gvr;

            //Use for loop for creating a Coding Rows
            for (int i = 0; i < codingRow; i++)
            {
                //Read always newly added row
                gvr = rgv.Rows[rgv.Rows.Count - 1];

                //Click on Cell for focus in a grid
                gvr.Cells[7].User.Click();

                //Pause after click on a cell
                System.Threading.Thread.Sleep(Globals.pause);
            }

            foreach (GridViewRow row in rgv.Rows)
            {
                //Click on Cell for Account Code
                row.Cells[3].User.Click(MouseClickType.LeftClick);

                //Enter the Account Code                
                row.Cells[3].User.TypeText(accountCode, 100);

                //Pause After Account Code
                System.Threading.Thread.Sleep(Globals.pause);

                //Press tab to move the location in grid
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab, 500, 2);

                //Pause for refresh the Coding Row Grid
                System.Threading.Thread.Sleep(Globals.pause);

                //Click on Cell for Cost Center Code
                row.Cells[5].User.Click(MouseClickType.LeftClick);

                //Enter the CostCenter Code
                row.Cells[5].User.TypeText(costCenterCode, 100);

                //Pause after left Double click on gross sum cell
                System.Threading.Thread.Sleep(Globals.pause);

                //Press tab to move the location in grid
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab, 500, 5);

                //Pause for refresh the Coding Row Grid
                System.Threading.Thread.Sleep(Globals.pause);

                //get the Net Sum from Test Data 
                string NetSum = addNetSum.ToString(CultureInfo.CurrentCulture.NumberFormat);

                //Enter the Net Sum
                Manager.Current.Desktop.KeyBoard.TypeText(NetSum, 10);

                //Pause for refresh the Coding Row Grid
                System.Threading.Thread.Sleep(Globals.pause);

                //Press Tab Key to Set Focus on Tax Code field
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

                //get the Tax Code from Test Data 
                string taxcode = taxCode.ToString(CultureInfo.CurrentCulture.NumberFormat);

                //Select the String in Text Box
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

                //Generate a Keyboard Event to clear search
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

                //Enter the Tax Code
                Manager.Current.Desktop.KeyBoard.TypeText(taxcode, 75);

                //Pause for refresh the Coding Row Grid
                System.Threading.Thread.Sleep(Globals.pause);
            }
        }

        //Method to Match and UnMatched the Payment Plan
        public void P2PInvoiceAdministrationMatchedAndUnMatchedThePlan(string unMatchPlan, double updateGrossSum, string matchPlan = null)
        {
            //Execute if block if condition is true
            if (matchPlan == null)
            {
                //Wait for button exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_PlanDetails_Button.Wait.ForExists(Globals.timeOut);
                //Click on Plan details button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_PlanDetails_Button.User.Click();

                //Wait for Manual matching container exists in DOM
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=stackpanel", "Name=manualMatchingButtonContainer")).Wait.ForExists(Globals.timeOut);
                //Create an object for container
                StackPanel manualMatchingToolBarButton = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=stackpanel", "Name=manualMatchingButtonContainer")).CastAs<StackPanel>();
                //Click on umMatch button
                manualMatchingToolBarButton.Find.ByTextContent(unMatchPlan).User.Click();

                P2PNavigation.CallBusyIndicator();
                //Wait for HeaderData TabTabItem Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.Wait.ForExists(Globals.timeOut);

                //Set focus before perform next step
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.SetFocus();

                //Click on Header Data Tab Item
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.User.Click();
                //Wait for text box exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_GrossSumHeaderDataTextBox.Wait.ForExists(Globals.timeOut);
                //Set focus on text box
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_GrossSumHeaderDataTextBox.SetFocus();

                //Hold the Ctrl Key
                Manager.Current.Desktop.KeyBoard.KeyDown(Keys.ControlKey);

                //Press the A Key
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.A);

                //Press the Delete Key
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Delete);

                //Release the Ctrl Key
                Manager.Current.Desktop.KeyBoard.KeyUp(Keys.ControlKey);

                //Enter the gross sum
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_GrossSumHeaderDataTextBox.User.TypeText(updateGrossSum.ToString(CultureInfo.CurrentCulture.NumberFormat), 50);
                //Wait for button exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.Wait.ForExists(Globals.timeOut);
                //Click on Save button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.User.Click();
            }

            else
            {
                //Create an object for Manual Matching Container
                StackPanel matchAgain = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=stackpanel", "Name=manualMatchingButtonContainer")).CastAs<StackPanel>();
                //Click on Match Button
                matchAgain.Find.ByTextContent(matchPlan).User.Click();
                //Wait for Buys indicator handled the scripts
                P2PNavigation.CallBusyIndicator();
                //Wait for exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_ConfirmButton.Wait.ForExists(Globals.timeOut);
                //Set focus on Confirm Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_ConfirmButton.SetFocus();
                //Clcik on Confirm button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_ConfirmButton.User.Click();
            }
        }

        //************Below are the common functions which is used in E2E Test Script, So please do not edit***********
        //Method to Update PO Number of an Invoice
        public void P2PMatchingUpdatePONumber(string purchaseOrderNumber, string fta = null, string supplier = null)
        {
            //Wait for  Tab Item Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.Wait.ForExists(Globals.timeOut);

            // Click on HeaderData Tab TabItem
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_TabTabItem.User.Click();

            //Press Tab key
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Wait for Header Text Box Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_InvoiceDetails_PurchaseOrderNumber_HeaderDataTextbox.Wait.ForExists(Globals.timeOut);

            //Set Focus on Header Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_InvoiceDetails_PurchaseOrderNumber_HeaderDataTextbox.SetFocus();

            //Click on Header Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_InvoiceDetails_PurchaseOrderNumber_HeaderDataTextbox.User.Click();

            //Select the String in Text Box
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

            //Generate a Keyboard Event to clear search
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

            //Enter the Data in Header Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_InvoiceDetails_PurchaseOrderNumber_HeaderDataTextbox.User.TypeText(purchaseOrderNumber, 50);

            if (fta != null)
            {
                //Wait for exists in DOM
                SharedElement.P2P_Application.SilverlightApp.MyTask_Create_Invoice_Text3_HeaderDataTextBox.Wait.ForExists(Globals.timeOut);
                //Enter the Data in Header Text Box
                SharedElement.P2P_Application.SilverlightApp.MyTask_Create_Invoice_Text3_HeaderDataTextBox.User.TypeText(fta, 50);
            }
            if (supplier != null)
            {
                //Wait for Supplier Selection Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierCode_SelectionButton.Wait.ForExists(Globals.timeOut);

                //Click on Supplier Selection Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierCode_SelectionButton.User.Click();

                //Wait for OK Button Exist in DOM
                P2PNavigation.WaitForPopUpOkButtonEnabled();

                //Wait for Supplier List Grid View Control Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SuppliersListGridViewControl.Wait.ForExists(Globals.handleTime);

                //Click to set a focus on Search Text Box
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SupplierSearch_TextBox.User.Click();

                //Set focus on the  Search text box
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SupplierSearch_TextBox.SetFocus();

                //Enter the Data in Search TextBox
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SupplierSearch_TextBox.TypeText(supplier, 50);

                //Click on Search Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administartion_SuppliersSearchButton.User.Click();

                //Wait for Supplier List Grid View Control Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SuppliersListGridViewControl.Wait.ForExists(Globals.handleTime);

                //Find the Supplier and Select From the Grid 
                TextBlock selectSupplier = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(supplier).As<TextBlock>();

                //Select the Supplier by the User
                selectSupplier.User.Click();

                //Click on OK Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
            }
        }

        //Method to Modify and Save Invoice in Received Page
        public void P2PSendInvoiceToProcess(string paymentPlanToolBarAction = null)
        {

            if (paymentPlanToolBarAction != null)
            {
                //Wait for Search Button exist in DOM
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=actiontoolbar", "Name=ActionToolbar")).SetFocus();
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=actiontoolbar", "Name=ActionToolbar")).Wait.ForExists();
                //Create an object for tool bar actions
                ArtOfTest.WebAii.Silverlight.UI.UserControl paymentPlanToolBarActions = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=actiontoolbar", "Name=ActionToolbar")).CastAs<ArtOfTest.WebAii.Silverlight.UI.UserControl>();
                //Click on toolbar button
                paymentPlanToolBarActions.Find.ByTextContent(paymentPlanToolBarAction).User.Click();
            }

            else
            {
                //Wait for Exists
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_SendtoProcess.Wait.ForExists(Globals.timeOut);

                //Set Focus
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_SendtoProcess.SetFocus();

                //click send to process
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_SendtoProcess.User.Click(MouseClickType.LeftDoubleClick);

                //Wait for Saving the Records Successfully
                System.Threading.Thread.Sleep(Globals.timeOut);
            }
        }

        //Method to Send Invoice To Process from Received Silo after Open Selected
        public void P2PSendInvoiceToProcessDetailsPage()
        {
            //Wait for Search Button exist in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=IA.UIElements.AdminPageToolbar.SendToProcessDetail_Button_Text", "XamlTag=toolbarbutton")).Wait.ForExists(Globals.timeOut);
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=IA.UIElements.AdminPageToolbar.SendToProcessDetail_Button_Text", "XamlTag=toolbarbutton")).SetFocus();
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=IA.UIElements.AdminPageToolbar.SendToProcessDetail_Button_Text", "XamlTag=toolbarbutton")).User.Click();
        }

        //Method to select Recipient
        public void P2PSelectRecipient(string selectUser)
        {
            //Wait for Search User Text Box Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_SearchUserTextbox.Wait.ForExists(Globals.timeOut);

            //Enter User name in Search User Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_SearchUserTextbox.User.TypeText(selectUser, 50, true);

            //Click on Search User Button to Search User
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SelectRecipient_SearchButton.User.Click();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_Collaborate_SelectUsersGrid.Refresh();

            TextBlock userCell = null;

            //Find the User Name and Select From the Grid 
            userCell = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(selectUser).As<TextBlock>();

            //Press Enter the Key
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);

            //Click on OK Button to Close the Select Users Pop-Up
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
        }

        //Method to Unhold Task under Workflow Task management Tab   
        public void P2PInvoiceAdministrationWorkflowTaskManagementUnHoldATask(string paymentPlan = null, string selectUser = null)
        {
            if (paymentPlan == null)
            {
                //Wait for TaskManagementTabTabitem Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagementTabTabitem.Wait.ForExists(Globals.timeOut);

                //Click on TaskManagementTabTabitem
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagementTabTabitem.User.Click();

                //Wait for some time
                P2PNavigation.CallBusyIndicator();
            }

            if (selectUser != null)
            {
                //Create  a RadGridView
                ArtOfTest.WebAii.Silverlight.UI.ListBox rgv = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskGrid;

                //Get user in TextBlock
                TextBlock selectesUser = rgv.Find.ByTextContent(selectUser).As<TextBlock>();

                //Click User
                selectesUser.User.Click();
            }

            //wait for Release button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_ReleaseTaskButton.Wait.ForExists(Globals.timeOut);

            //SetFocus on Unhold button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_ReleaseTaskButton.SetFocus();

            //Click on Release Button to Release the Task
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_ReleaseTaskButton.User.Click();

            //Wait for some time
            P2PNavigation.CallBusyIndicator();

            //Wait for Search Button exist in DOM                
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=actiontoolbar", "Name=ActionToolbar")).Wait.ForExists();

            ArtOfTest.WebAii.Silverlight.UI.UserControl paymentPlanToolBarActions = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=actiontoolbar", "Name=ActionToolbar")).CastAs<ArtOfTest.WebAii.Silverlight.UI.UserControl>();

            //paymentPlanToolBarActions.Find.ByTextContent("Save").User.Click();
            paymentPlanToolBarActions.Find.ByAutomationId("IA.UIElements.AdminPageToolbar.Save_Button_Text").User.Click();

        }

        //Method to Add a new Task under Workflow Task management Tab
        public void P2PInvoiceAdministrationWorkflowTaskManagementAddNewTask(string paymentPlan = null, string selectUser = null, string addNewRow = null, string paymentPlanTaskManagement = null)
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

            if (addNewRow == null)
            {
                //Wait for TaskManagement Add Task Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagementTabTabitem_AddTaskButton.Wait.ForExists(Globals.timeOut);

                //Click on TaskManagement Add Task Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagementTabTabitem_AddTaskButton.User.Click();
            }

            //If condition is true then execute if block    
            if (selectUser != null)
            {
                if (paymentPlanTaskManagement != null)
                {
                    //Wait for grid to load
                    SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_WorkFlow_PlansDetails_TaskManagement_TaskGridView.Wait.ForExists(Globals.timeOut);

                    //Create  a RadGridView
                    RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_WorkFlow_PlansDetails_TaskManagement_TaskGridView;

                    //Create an object for GridViewRow class
                    GridViewRow gvr;

                    //Store row count in integer variable
                    int countRows = rgv.Rows.Count;

                    //Try to select the newly row added in grid
                    gvr = rgv.Rows[rgv.Rows.Count - countRows];

                    //Click on Cell for focus in a grid
                    gvr.Cells[3].User.Click();

                    //Presss Tab to  Set focus on the Browse button
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
                    //Press enter button to open the Select Recipeint Dialog 
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);
                }

                else
                {

                    //Wait for Grid to Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskGrid.Wait.ForExists(Globals.timeOut);
                    //Click on Grid Control
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskGrid.User.Click();
                    //Set Focus on Task by Pressing Tab Key      
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab, 500, 2);
                    Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);
                }

                //Wait for Recipient Displayed Properly
                P2PNavigation.CallBusyIndicator();

                //Wait for Search User Text Box Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_SelectUser_SearchTextBox.Wait.ForExists(Globals.timeOut);

                //Enter Value in Add Recipient Search User Text Box
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_SelectUser_SearchTextBox.User.TypeText(selectUser, 50);

                //Click on Search Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_SearchUserButton.User.Click();

                //Wait for Search Result Load Properly
                P2PNavigation.CallBusyIndicator();

                //Select the User from Result Grid
                TextBlock userCell = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_Collaborate_SelectUsersGrid.Find.ByTextContent(selectUser).As<TextBlock>();

                //Select the User
                userCell.User.Click();

                //Click on Ok Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
            }

            P2PNavigation.CallBusyIndicator();
            //Wait for Search Button exist in DOM                
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=actiontoolbar", "Name=ActionToolbar")).Wait.ForExists(Globals.timeOut);

            ArtOfTest.WebAii.Silverlight.UI.UserControl paymentPlanToolBarActions = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=actiontoolbar", "Name=ActionToolbar")).CastAs<ArtOfTest.WebAii.Silverlight.UI.UserControl>();

            paymentPlanToolBarActions.Find.ByAutomationId("IA.UIElements.AdminPageToolbar.Save_Button_Text").User.Click();
        }

        //Method to Unhold Task under Workflow Task management Tab
        public void P2PMyTaskPutOnHold(string comment, string putOnHold)
        {
            //Wait for Additional Actions Dropdown button Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Click the Additional Actions drop down
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            //Click on Hold Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Find.ByTextContent(putOnHold).User.Click();

            //Verify That P2P_Invoice_Administration_AddComment_OKButton is enabled or not
            if (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.IsEnabled == false)
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, "P2P_Invoice_Administration_AddComment_OKButton Is  Not enabled verification Successful");
            }

            else
            {
                //Write the log if Verification Fail
                throw new Exception("P2P_Invoice_Administration_AddComment_OKButton Is enabled verification Failed!");
            }

            //If condition is true then execute if block
            // if(putOnHold==null)
            // {
            //Wait for Cancel Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_CancelButton.Wait.ForExists(Globals.timeOut);

            //Click on Cancel Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_CancelButton.User.Click();
            // }

            //Wait for Additional Actions Dropdown button Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Click the Additional Actions drop down
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            //Click on Hold Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Find.ByTextContent(putOnHold).User.Click();

            //Wait for Add Comment Textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);

            //Set focus on Add Comment Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.SetFocus();

            //Click to Add Comment while Task Put On Hold and Entering Text
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(comment.ToString(), 100, true);

            //Wait for OK Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK after adding Comments
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            //Calling busy indicator
            P2PNavigation.CallBusyIndicator();
        }

        //Method to Unhold Task under Workflow Task management Tab
        public void P2PMyTaskReject(string invoiceNumber, string comment, string rejectReason, string paymentPlan = null)
        {

            if (paymentPlan == null)
            {

                //Wait for Personnel Mode My Task button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_MyTaskButton.Wait.ForExists(Globals.timeOut);

                //Click on Personnel Mode My Task button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_MyTaskButton.User.Click();

                ////Wait for P2P_MyTask_Invoice_ListView Button Exists in DOM
                //SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_Invoice_ListView_Button.Wait.ForExists();

                ////Click the P2P_MyTask_Invoice_ListView Button
                //SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_Invoice_ListView_Button.User.Click();

                //Click on Personnel Mode My Task button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Wait.ForExists(Globals.timeOut);

                //Clear the P2P_Invoice_Administration_SearchTextbox
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Text = "";

                //Enter Value in Add Recipient Search User Text Box
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.User.TypeText(invoiceNumber, 50);

                //Click on Search Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_MyTask_InvoiceSearchButton.User.Click();

                try
                {
                    //Create  a RadGridView
                    RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_InvoicesGridViewControl;

                    //Find the Invoice Number from the Grid
                    TextBlock selectInvoiceNumber = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(invoiceNumber).As<TextBlock>();

                    //Click on Invoice Number
                    selectInvoiceNumber.User.Click();

                }
                finally
                {
                    //Calling busy indicator
                    P2PNavigation.CallBusyIndicator();
                }

            }

            //Wait for Personnel Mode My Task button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_MyTasks_ToolBar_Reject_Button.Wait.ForExists(Globals.timeOut);

            //Click on Personnel Mode My Task button
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_MyTasks_ToolBar_Reject_Button.User.Click();

            //Wait for Combobox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_RejectReasonComboBox.Wait.ForExists(Globals.timeOut);

            //Left Click on Reject Reason ComboBox to keep Focus on the ComboBox
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_RejectReasonComboBox.OpenDropDown(true);

            //Click on Reject Reason ComboBox
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_RejectReasonComboBox.SelectItemByText(true, rejectReason, true);

            //Wait for Add Comment Textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);

            //Set focus on Add Comment Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.SetFocus();

            //Click to Add Comment while Task Put On Hold
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(comment, 50, true);

            //Wait for OK Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK after adding Comments
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            //Calling busy indicator
            P2PNavigation.CallBusyIndicator();
        }

        //Method to Confirm invoice(in Matching Silo)   
        public void P2PMatchingConfirmInvoice()
        {
            do
            {
                //Wait for button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_ConfirmButton.Wait.ForExists(Globals.timeOut);
            } while (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_ConfirmButton.IsEnabled == false);

            //Click on button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_ConfirmButton.User.Click();
        }

        //Method to Search Purchase Order
        public void P2PMatchingSearchPurchaseOrder(string purchaseOrderNumber)
        {
            //Wait for TextBox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ManualMatching_OrderDataSearchTextBox.Wait.ForExists(Globals.timeOut);

            //Set focus on TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ManualMatching_OrderDataSearchTextBox.SetFocus();

            //Select the String in Text Box
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

            //Generate a Keyboard Event to clear search
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

            //Enter Data in Purchase Order Number
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_ManualMatching_OrderDataSearchTextBox.User.TypeText(purchaseOrderNumber, 50);

            //Wait for Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_Matching_OrderDataSearchButton.Wait.ForExists(Globals.timeOut);

            //Set focus on Button
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_Matching_OrderDataSearchButton.SetFocus();

            //Click on Button
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_Matching_OrderDataSearchButton.User.Click();
        }

        //Method to Select Searched Purchase Order
        public void P2PMatchingSelectSearchedPurchaseOrder(string purchaseOrder)
        {
            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_PurchaseOrderGridSingleOrder.Wait.ForExists(Globals.timeOut);

            // P2P_Invoice_Administration_Matching_PurchaseOrderGrid
            //Set focus on Grid Control
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_PurchaseOrderGridSingleOrder.SetFocus();

            //Find Purchase order
            TextBlock selectPurchaseOrder = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_PurchaseOrderGridSingleOrder.Find.ByTextContent(purchaseOrder).As<TextBlock>();

            //Click on Purchase Order
            selectPurchaseOrder.User.Click();

            //Wait for Match button to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_MatchButton.Wait.ForExists(Globals.timeOut);

            //Click Match button to associate Searched PO number
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_MatchButton.User.Click();
        }

        //Method to Open Selected Invoice
        public void P2PMatchingOpenSelectedInvoice()
        {
            //Wait for Button Exists in DOM.
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=IA.UIElements.AdminPageToolbar.OpenSelected_Button_Text")).Wait.ForExists(Globals.timeOut);

            //Click on Button.
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=IA.UIElements.AdminPageToolbar.OpenSelected_Button_Text")).User.Click();
        }

        //Click On Task Management Tab Item
        public void P2PInvoiceAdministrationWorkflowTaskManagementTab()
        {
            //Wait for TaskManagementTabTabitem Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagementTabTabitem.Wait.ForExists(Globals.timeOut);

            //Click on TaskManagement Tab
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagementTabTabitem.User.Click();

            //Wait for Grid to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskGrid.Wait.ForExists(Globals.timeOut);

            //Click on Grid Control
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskGrid.User.Click();

            //Set Focus on Task by Pressing Tab Key         
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

        }

        //Method to Click Draft link from Overview Page
        public void P2PInvoiceAdministrationOverviewInvoiceStatusesReceived(string invoiceStatusLink, string invoiceCount = null)
        {

            switch (invoiceStatusLink)
            {
                case "Draft":
                    //Wait for Exist in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_Received_DraftLink.Wait.ForExists(Globals.timeOut);
                    //Set Focus on the Link
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_Received_DraftLink.SetFocus();

                    //declare variable and get actual count from the UI
                    string actualDraftCount = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_Received_DraftLink.Text.ToString();

                    //If block execute if Condition is true
                    if (actualDraftCount != invoiceCount)
                    {
                        //Click on Draft link in Overview Page
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_Received_DraftLink.User.Click();
                    }
                    else
                    {
                        Manager.Current.Log.WriteLine("Draft Invoice Count on Overview Page is :-" + actualDraftCount);
                    }
                    break;

                case "Invalid":

                    //Wait for Exist in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_Received_InvalidLink.Wait.ForExists(Globals.timeOut);
                    //Set Focus on the Link
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_Received_InvalidLink.SetFocus();

                    //declare variable and get actual count from the UI
                    string actualInvalidCount = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_Received_InvalidLink.Text.ToString();

                    //If block execute if Condition is true
                    if (actualInvalidCount != invoiceCount)
                    {
                        //Click on Invalid link in Overview Page
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_Received_InvalidLink.User.Click();
                    }
                    else
                    {
                        Manager.Current.Log.WriteLine("Invalid Invoice Count on Overview Page is :-" + actualInvalidCount);
                    }
                    break;

                case "In manual payment plan matching":

                    //Wait for Exist in DOM                       
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_InManualMatchingPaymentPlan.Wait.ForExists(Globals.timeOut);
                    //Set Focus on the Link
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_InManualMatchingPaymentPlan.SetFocus();

                    //declare variable and get actual count from the UI
                    string actualManualMatchingPPCount = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_InManualMatchingPaymentPlan.Text.ToString();

                    //If block execute if Condition is true
                    if (actualManualMatchingPPCount != invoiceCount)
                    {
                        //Click on Invalid link in Overview Page
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_InManualMatchingPaymentPlan.User.Click();
                    }
                    else
                    {
                        Manager.Current.Log.WriteLine("In ManualMatching PaymentPlan Invoice Count on Overview Page is :-" + actualManualMatchingPPCount);
                    }
                    break;

                case "In manual order matching":

                    //Wait for Exist in DOM                       
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_InManualOrderMatchingLink.Wait.ForExists(Globals.timeOut);
                    //Set Focus on the Link
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_InManualOrderMatchingLink.SetFocus();

                    //declare variable and get actual count from the UI
                    string actualManualOrderMatchingCount = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_InManualOrderMatchingLink.Text.ToString();

                    //If block execute if Condition is true
                    if (actualManualOrderMatchingCount != invoiceCount)
                    {
                        //Click on Invalid link in Overview Page
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_InManualOrderMatchingLink.User.Click();
                    }
                    else
                    {
                        Manager.Current.Log.WriteLine("In Manual Order Matching Invoice Count on Overview Page is :-" + actualManualOrderMatchingCount);
                    }
                    break;

                case "Exceptions":

                    //Wait for Exist in DOM                       
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_OverviewWorkflow_ExceptionLink.Wait.ForExists(Globals.timeOut);
                    //Set Focus on the Link
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_OverviewWorkflow_ExceptionLink.SetFocus();

                    //declare variable and get actual count from the UI
                    string actualExceptionsCount = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_OverviewWorkflow_ExceptionLink.Text.ToString();

                    //If block execute if Condition is true
                    if (actualExceptionsCount != invoiceCount)
                    {
                        //Click on Invalid link in Overview Page
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_OverviewWorkflow_ExceptionLink.User.Click();
                    }
                    else
                    {
                        Manager.Current.Log.WriteLine("Exceptions Invoice Count on Overview Page is :-" + actualExceptionsCount);
                    }
                    break;

                case "Incomplete for transfer":

                    //Wait for Exist in DOM                       
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_Transfer_UnclearLink.Wait.ForExists(Globals.timeOut);
                    //Set Focus on the Link
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_Transfer_UnclearLink.SetFocus();

                    //declare variable and get actual count from the UI
                    string actualIncompleteCount = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_Transfer_UnclearLink.Text.ToString();

                    //If block execute if Condition is true
                    if (actualIncompleteCount != invoiceCount)
                    {
                        //Click on Invalid link in Overview Page
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_Transfer_UnclearLink.User.Click();
                    }
                    else
                    {
                        Manager.Current.Log.WriteLine("'Incomplete for transfer' Invoice Count on Overview Page is :-" + actualIncompleteCount);
                    }
                    break;
                case "Transfer failed":

                    //Wait for Exist in DOM                       
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_Transfer_TransferFailedLink.Wait.ForExists(Globals.timeOut);
                    //Set Focus on the Link
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_Transfer_TransferFailedLink.SetFocus();

                    //declare variable and get actual count from the UI
                    string actualTransferfailedCount = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_Transfer_TransferFailedLink.Text.ToString();

                    //If block execute if Condition is true
                    if (actualTransferfailedCount != invoiceCount)
                    {
                        //Click on Invalid link in Overview Page
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_Transfer_TransferFailedLink.User.Click();
                    }
                    else
                    {
                        Manager.Current.Log.WriteLine("'Transfer failed' Invoice Count on Overview Page is :-" + actualTransferfailedCount);
                    }
                    break;
                default:
                    //Log Failure for Tab item not found
                    Manager.Current.Log.WriteLine(LogType.Error, "Invoice Navigation Links are Disabled");
                    break;
            }
        }

        //Method to Edit the Invoice Number
        public void P2PInvoiceAdministrationEditInvoiceNumber(string invoiceNumber)
        {

            //Wait for Header Data to load in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_HeaderDataTabItem.Wait.ForExists(Globals.timeOut);
            //Click on Header Data
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_HeaderDataTabItem.User.Click();

            //Enter the Data in Invoice Number Header Data Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceNumberHeaderDataTextBox.SetFocus();
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceNumberHeaderDataTextBox.User.Click();

            //Select the String in Text Box
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

            //Generate a Keyboard Event to clear search
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceNumberHeaderDataTextBox.User.TypeText(invoiceNumber, 50);

            //Press Tab Key Get Control Forward
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
        }

        //Method to Create a Coding Rows in My Tasks ***APT*** As Fields are in Different Order
        public void P2PInvoiceAdministrationMyTasksAddCodingRows(int codingRow, string accountCode, string costCenterCode, double addNetSum, string taxCode)
        {
            //Workaround for application freeze
            System.Threading.Thread.Sleep(Globals.pause);
            //Wait for Add Row Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.Wait.ForExists(Globals.timeOut);
            //Set focus on the  button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.SetFocus();
            //Click on Add Row Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.User.Click(MouseClickType.LeftDoubleClick);

            //Workaround for application freeze
            System.Threading.Thread.Sleep(Globals.pause);

            //Find the Coding Row using AutomationID                   
            RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl;

            //Create an object for GridViewRow class
            GridViewRow gvr;

            //Use for loop for creating a Coding Rows
            for (int i = 0; i < codingRow; i++)
            {
                //Read always newly added row
                gvr = rgv.Rows[rgv.Rows.Count - 1];

                //Click on Cell for focus in a grid
                gvr.Cells[2].User.Click();

                //Pause after click on a cell
                System.Threading.Thread.Sleep(Globals.pause);
            }

            foreach (GridViewRow row in rgv.Rows)
            {
                //Click on Cell for Account Code
                row.Cells[2].User.Click(MouseClickType.LeftClick);

                //Enter the Account Code                
                row.Cells[2].User.TypeText(accountCode, 100);

                //Pause After Account Code
                System.Threading.Thread.Sleep(Globals.pause);

                //Press tab to move the location in grid
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab, 500, 2);

                //Pause for refresh the Coding Row Grid
                System.Threading.Thread.Sleep(Globals.pause);

                //Click on Cell for Cost Center Code
                row.Cells[4].User.Click(MouseClickType.LeftClick);

                //Enter the CostCenter Code
                row.Cells[4].User.TypeText(costCenterCode, 100);

                //Pause after left Double click on gross sum cell
                System.Threading.Thread.Sleep(Globals.pause);

                //Press tab to move the location in grid
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab, 500, 5);

                //Pause for refresh the Coding Row Grid
                System.Threading.Thread.Sleep(Globals.pause);

                //get the Net Sum from Test Data 
                string NetSum = addNetSum.ToString(CultureInfo.CurrentCulture.NumberFormat);

                //Enter the Net Sum
                Manager.Current.Desktop.KeyBoard.TypeText(NetSum, 10);

                //Pause for refresh the Coding Row Grid
                System.Threading.Thread.Sleep(Globals.pause);

                //Press Tab Key to Set Focus on Tax Code field
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

                //Use Static Wait as F is Missed while Typing F0
                System.Threading.Thread.Sleep(Globals.pause);

                //Enter the Tax Code
                Manager.Current.Desktop.KeyBoard.TypeText(taxCode, 100);

                //Pause for refresh the Coding Row Grid
                System.Threading.Thread.Sleep(Globals.pause);
            }
        }

        //Method to Search Plan Reference on Matching Details Page ***APT***
        public void P2PInvoiceAdministrationMatchingDetailsSearchPlanReference(string planReference)
        {
            //Wait for Plan Reference Textbox Exists in Dom
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MatchingDetails_PaymentPlanSearchTextbox.Wait.ForExists(Globals.timeOut);
            //Empty if Some Value already there
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MatchingDetails_PaymentPlanSearchTextbox.Text= string.Empty;            
            //Set Focus on Search Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MatchingDetails_PaymentPlanSearchTextbox.SetFocus();
            //Click on Search Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MatchingDetails_PaymentPlanSearchTextbox.User.Click();
            //Enter the Plan Reference 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MatchingDetails_PaymentPlanSearchTextbox.User.TypeText(planReference, 50);
            //Wait for Search Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MatchingDetails_PaymentPlanSearchButton.Wait.ForExists(Globals.timeOut);
            //Click on Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MatchingDetails_PaymentPlanSearchButton.User.Click();
        }

        //Method to Select PlanDeials on Matching Details Page and Match***APT***
        public void P2PInvoiceAdministrationMatchingSelectPlanDetailsRowAndMatch(int selectRowNum)
        {
            //Wait for Plan Details Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_PlanDetails_Button.Wait.ForExists(Globals.timeOut);
            //Click on Plan Details Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_PlanDetails_Button.User.Click();
            //Wait for Payment Schedule Row grid Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MatchingDetails_PaymentScheduleRowGrid.Wait.ForExists(Globals.timeOut);
            //Set Focus on Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MatchingDetails_PaymentScheduleRowGrid.SetFocus();
            //Select the Row to Match
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MatchingDetails_PaymentScheduleRowGrid.Rows[selectRowNum].IsSelected = true;
            //Wait for Match Button Exists in Dom
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MatchingDetails_PaymentSchedule_MatchButton.Wait.ForExists(Globals.timeOut);
            //Set Focus on Match Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MatchingDetails_PaymentSchedule_MatchButton.SetFocus();
            //Click on Match Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MatchingDetails_PaymentSchedule_MatchButton.User.Click();
        }

        //Method to Add Coding Row after Open Selected ***APT***
        public void P2PInvoiceAdministrationDetailsAddCodingRow(int selectRowNum, int cellNum, string contextMenu, string costCenterCode, double addNetSum)
        {

            //Wait for Grid Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Wait.ForExists(Globals.timeOut);
            //Set Focus on Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.SetFocus();
            //Select the Last Row
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Rows[selectRowNum].IsSelected = true;

            //Right Click to Activate the Control
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Rows[selectRowNum].Cells[cellNum].User.Click(MouseClickType.RightClick);

            //Wait for Context Menu Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRow_ContextMenuControl.Wait.ForExists(Globals.timeOut);
            //Click on Add Row Context Menu
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRow_ContextMenuControl.Find.ByTextContent(contextMenu).As<TextBlock>().User.Click();

            //Pause for refresh the Coding Row Grid
            System.Threading.Thread.Sleep(Globals.pause);

            //Press tab to move the location in grid
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab, 500, 2);

            //Pause for refresh the Coding Row Grid
            System.Threading.Thread.Sleep(Globals.pause);

            //Enter the CostCenter Code
            Manager.Current.Desktop.KeyBoard.TypeText(costCenterCode, 100);

            //Press tab to move the location in grid
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab, 500, 5);

            //Pause for refresh the Coding Row Grid
            System.Threading.Thread.Sleep(Globals.pause);

            //get the Net Sum from Test Data 
            string NetSum = addNetSum.ToString(CultureInfo.CurrentCulture.NumberFormat);

            //Enter the Net Sum
            Manager.Current.Desktop.KeyBoard.TypeText(NetSum, 10);

            //Pause for refresh the Coding Row Grid
            System.Threading.Thread.Sleep(Globals.pause);

        }

        //Method to Send Invoice for Transfer***APT***
        public void P2PInvoiceAdministrationSendInvoiceToTransfer()
        {
            //Wait for Transfer Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_TransferButton.Wait.ForExists(Globals.timeOut);

            //Set Focus on Transfer Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_TransferButton.SetFocus();

            //Click on Transfer Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_TransferButton.User.Click();
        }

        //Method to Go to LineLevel***APT***
        public void P2PMatchingDetailsPOLineLevelSwitch()
        { 
         //Wait for Line Level Control Exists in DOM
         SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_MatchingDetails_SwitchToRowLevelButton.Wait.ForExists(Globals.timeOut);
            //Set Focus on Line Level Button
         SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_MatchingDetails_SwitchToRowLevelButton.SetFocus();
            //Click on Switch to Line Level Button
         SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_MatchingDetails_SwitchToRowLevelButton.User.Click();

            //Call the Busy Indicator
         P2PNavigation.CallBusyIndicator();         
        }
        
        //Method to Select All Invoices and Purchase Orders ***APT***
        public void P2PMatchingSelectAllInvoicesPurchaseOrders(bool selectAll)
        {
            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_PurchaseOrderGrid.Wait.ForExists(Globals.timeOut);          
            //Set focus on Grid Control
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_PurchaseOrderGrid.SetFocus();
            if (selectAll == true)
            {
                //Select All the Lines 
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));
            }

            //Wait for Invoice Lines Tab Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_InvoiceLinesTab.Wait.ForExists(Globals.timeOut);        
            //Click on Invoice Lines Tab 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_InvoiceLinesTab.User.Click();
            //Wait For Invoice Lines Grid Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_InvoiceLinesGridListviewControl.Wait.ForExists(Globals.timeOut);
            //Set focus on Invoice Lines Grid Control 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_InvoiceLinesGridListviewControl.SetFocus();
            //Select All the Invoice Lines
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));            
            //Wait for Match button to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MatchingDetails_PurchaseOrderMatchButton.Wait.ForExists(Globals.timeOut);

            //Click Match button to associate Searched PO number
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MatchingDetails_PurchaseOrderMatchButton.User.Click();
        }

        //Method to Edit Coding Row (Net Total) after Open Selected ***APT***
        public void P2PInvoiceAdministrationDetailsEditCodingRow(int selectRowNum, int cellNum, double editNetSum)
        {

            //Wait for Grid Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Wait.ForExists(Globals.timeOut);
            //Set Focus on Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.SetFocus();
            //Select the Last Row
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Rows[selectRowNum].IsSelected = true;

            //Right Click to Activate the Control
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CodingRowGridControl.Rows[selectRowNum].Cells[cellNum].User.Click(MouseClickType.LeftClick);
                        
            //Pause for refresh the Coding Row Grid
            System.Threading.Thread.Sleep(Globals.pause);                     

            //Press tab to move the location in grid
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab, 500, 9);

            //Pause for refresh the Coding Row Grid
            System.Threading.Thread.Sleep(Globals.pause);

            //get the Net Sum from Test Data 
            string NetSum = editNetSum.ToString(CultureInfo.CurrentCulture.NumberFormat);

            //Enter the Net Sum
            Manager.Current.Desktop.KeyBoard.TypeText(NetSum, 10);

            //Pause for refresh the Coding Row Grid
            System.Threading.Thread.Sleep(Globals.pause);
        }

        //Method to Iterate the Grid ***APT***
        public void P2PMatchingIterateGridAndMatchGRRowWithInvoiceLine()
        {
            int gridRowCount = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_PurchaseOrderGrid.Rows.Count();
            for (int i = 0; i < gridRowCount; i++)
            {
                P2PMatchingMatchGRRowWithInvoiceLine(i);
            }
        }

        //Method to Select GR Row and Invoice Lines and Match ***APT***
        public void P2PMatchingMatchGRRowWithInvoiceLine(int rowNum)
        {                             
            
            //Wait for Purchase Order grid control Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_PurchaseOrderGrid.Wait.ForExists(Globals.timeOut);
            //Select the First Line from the Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_PurchaseOrderGrid.Rows[rowNum].User.Click();
            //Wait for GR Level Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_MatchingDetails_SwitchToGRLevelButton.Wait.ForExists(Globals.timeOut);            
            //Set Focus on Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_MatchingDetails_SwitchToGRLevelButton.SetFocus();
            //Click on GR Level Icon
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_MatchingDetails_SwitchToGRLevelButton.User.Click();
            //Set Focus on Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_MatchingDetails_GrGrid.Wait.ForExists(Globals.timeOut);
            //Set Focus on Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_MatchingDetails_GrGrid.SetFocus();
            //Select the First Row
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_MatchingDetails_GrGrid.Rows[0].User.Click();
            //Set Focus on Invoice Lines Tab
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_InvoiceLinesTab.SetFocus();
            //Click on Invoice Lines Tab
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching_InvoiceLinesTab.User.Click();
            //Set Focus
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_InvoiceLinesGridListviewControl.SetFocus();
            //Select the First Line from the Invoice Lines Tab
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_InvoiceLinesGridListviewControl.Rows[rowNum].User.Click();                                  
            //Wait for Match button to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_MatchingDetails_GRGrid_MatchButtonTextBlock.Wait.ForExists(Globals.timeOut);
            //Click Match button to associate Searched PO number Match It
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_MatchingDetails_GRGrid_MatchButtonTextBlock.User.Click();
            //Call Busy Indicator
            P2PNavigation.CallBusyIndicator();
            //Switch Back to GR ROW Level
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_MatchingDetails_SwitchBackFromGRToRowLevelButton.Wait.ForExists(Globals.timeOut);
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_MatchingDetails_SwitchBackFromGRToRowLevelButton.SetFocus();
            //Click to Switch Back 
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_MatchingDetails_SwitchBackFromGRToRowLevelButton.User.Click();       
        
        }


    }
}