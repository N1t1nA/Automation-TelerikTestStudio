using System;
using System.Linq;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Win32.Dialogs;
using System.Windows.Forms;
using ArtOfTest.WebAii.Silverlight;
using System.IO;
using ArtOfTest.Common.Win32;
using E2E.Class;

namespace P2P.Testing.Shared.Class.InvoiceAdministration.InvoiceDetails
{
    public class P2PInvoiceAdministrationImageActions : BaseWebAiiTest
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
        
        //Method to Rotate the Image
        public void P2PInvoiceAdministrationRotateImage()
        {
            //Wait for Rotate Image Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Image_RotateButton.Wait.ForExists(Globals.timeOut);
            
            //Click on Rotate Image Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Image_RotateButton.User.Click();

            //Call handle busy indicator
            P2PNavigation.CallBusyIndicator();

            //Click on Rotate Image Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Image_RotateButton.User.Click();

            //Call handle busy indicator
            P2PNavigation.CallBusyIndicator();
        }

        //Method to Show the Image
        public void P2PInvoiceAdministrationShowImage()
        {
            //Wait for Show Image Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Image_ShowButton.Wait.ForExists(Globals.timeOut);           

            //Click on Show Image Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Image_ShowButton.User.Click();           
           
            //Wait for Pop-Up Window to Show Image
            Manager.Current.ActiveBrowser.WaitUntilReady();

            //Call handle busy indicator
            P2PNavigation.CallBusyIndicator();

            //Close the Pop-Up Window Using KeyCombination(Alt+F4)
            //Press and Hold Alt Key from the Keyboard
            Manager.Current.Desktop.KeyBoard.KeyDown(Keys.Alt);

            //Press F4 Key from the Keyboard
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.F4);

            //Release Alt Key
            Manager.Current.Desktop.KeyBoard.KeyUp(Keys.Alt);           
        }

        //Method to Save the Image
        public void P2PInvoiceAdministrationSaveImage(string activeDirectory, string filePath)
        {
           //Create a Directory Under C: drive with Name TestAutomation
            System.IO.Directory.CreateDirectory(activeDirectory);

            //Change the Current Settings 
            Manager.Current.Settings.UnexpectedDialogAction = UnexpectedDialogAction.DoNotHandle;

            //Using if Condition to execute Script using Different Browsers(Chrome) 
            if ((Manager.Current.ActiveBrowser.BrowserType.Equals(7)))
            {
                //Wait for Save Image Button button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Image_SaveButton.Wait.ForExists(Globals.timeOut);

                //Click on Save Image Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Image_SaveButton.User.Click();

                //Call handle busy indicator
                P2PNavigation.CallBusyIndicator();

                //Type the Path for Saving the File
                Manager.Current.Desktop.KeyBoard.TypeText(filePath, 50);

                //Click on Enter Button
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);

                //Call handle busy indicator
                P2PNavigation.CallBusyIndicator();

                //Close the Pop-Up Window Using KeyCombination(Alt+F4)
                //Press and Hold the Alt Key from Keyboard
                Manager.Current.Desktop.KeyBoard.KeyDown(Keys.Alt);
                //Press the F4 Key from Keyboard
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.F4);
                //Release the Alt Key from Keyboard
                Manager.Current.Desktop.KeyBoard.KeyUp(Keys.Alt);

            }
            //Using else to execute Script using Different Browsers(IE9 and Firefox)
            else
            {

                //Handle the Download Dialogs     
                DownloadDialogsHandler downloadHandler = new DownloadDialogsHandler(Manager.Current.ActiveBrowser, DialogButton.SAVE, filePath, Manager.Current.Desktop);

                //Start the Dilaog Moniter
                Manager.Current.DialogMonitor.Start();

                //Wait for Save Image Button button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Image_SaveButton.Wait.ForExists(Globals.timeOut);

                try
                {
                    //Click on Save Image Button
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Image_SaveButton.User.Click();                   

                    //Handle the Download Dialogs
                    downloadHandler.WaitUntilHandled(Globals.handleTime);

                }
                catch (Exception Ex)
                {
                    //Write the log if file not saved
                    Manager.Current.Log.WriteLine(LogType.Error, "Unable to Save the File" + Ex.Message);
                }

                //Stops the Dilaog Moniter
                Manager.Current.DialogMonitor.Stop();
            }
        }

        //Method to Print the Image
        public void P2PInvoiceAdministrationPrintImage()
        {
            //Change the Current Settings 
            Manager.Current.Settings.UnexpectedDialogAction = UnexpectedDialogAction.DoNotHandle;

            //Handle the Download Dialogs to Open the PDF File, In this Case Location is %Temp%, as in DialogButton.OPEN Location is Ignored
            DownloadDialogsHandler iedownloadHandler = new DownloadDialogsHandler(Manager.Current.ActiveBrowser, DialogButton.OPEN, "%Temp%", Manager.Current.Desktop);

            //Start the Dilaog Moniter
            Manager.Current.DialogMonitor.Start();

            //Wait for Print Image Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Image_PrintButton.Wait.ForExists(Globals.timeOut);

            try
            {
                //Click on Print Image Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Image_PrintButton.User.Click();
                
                //Call handle busy indicator
                P2PNavigation.CallBusyIndicator();

                //Sets the Focus Print Image Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Image_PrintButton.SetFocus();

                //Handle the IE Dialog
                iedownloadHandler.WaitUntilHandled(Globals.handleTime);
            }
            catch(Exception Ex)
            {
                //Write the log if Image Not Printed
                Manager.Current.Log.WriteLine(LogType.Error, "Unable to Print the Invoice Image" + Ex.Message);
            }

            //Stops the Dilaog Moniter
            Manager.Current.DialogMonitor.Stop();
        }

        //Method to Add new Image
        public void P2PInvoiceAdministrationAddNewImage(string filePath,Boolean create)
        {
            if (create == true)
            {
                //Wait for Add Image Button Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddImageButton.Wait.ForExists(Globals.timeOut);
                //Click on Add Image Button 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddImageButton.User.Click();
            }

            else
            {
                //Wait for Add New Image Button Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_AddNewImageButton.Wait.ForExists(Globals.timeOut);
                //Click on Add New Image Button 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_AddNewImageButton.User.Click();
            }
           
            //Using If Condition for Using different Browser(Firefox & Chrome)
            if (Manager.Current.ActiveBrowser.BrowserType != BrowserType.InternetExplorer)
            {
                // Wait for Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddAttachment_BrowseButton.Wait.ForExists(Globals.timeOut);

                //Click on Browse Button to Browse the Path 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddAttachment_BrowseButton.User.Click();

                //Call handle busy indicator
                P2PNavigation.CallBusyIndicator();

                //Type the Path for Saving the File
                Manager.Current.Desktop.KeyBoard.TypeText(filePath, 50);

                //Click on Enter Button
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);
            }

            //If Browser Type is Internet Explorer
            else
            {
                try
                {                    
                    //Check file available for upload & Write Log
                    Manager.Current.Log.WriteLine(File.Exists(filePath) ? "File exists for upload." : "File does not exist.");

                    //Calling Fetching Data Method
                    P2P_Utility.FetchingDataFromUI(filePath, "File Upload Dialog");

                    //Click on Browse Button to Browse the Path 
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_AddAttachment_BrowseButton.User.Click();

                    //Add the File Upload Dialog Box
                    Manager.Current.DialogMonitor.AddDialog(P2P_Utility.fileUploadDialog);
                    
                    //Wait to handle Load
                    System.Threading.Thread.Sleep(Globals.timeOut);

                    //Invoke Dialog Moniter and Handle the Upload Dialog Box
                    Manager.Current.DialogMonitor.Start();
                }
                catch (System.IO.IOException ex)
                {
                    //Give the error message in case File Upload Dialog not handled
                    throw new Exception("Error Message" + " : " + ex);
                }
            }
        }

        //Method to Delete the Image
        public void P2PInvoiceAdministrationDeleteImage()
        {
            //Wait for Delete Image Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Image_DeleteButton.Wait.ForExists(Globals.timeOut);

            //Click on Delete Image Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Image_DeleteButton.User.Click();

            //Wait for OK Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
        }

        //Method to Cancel Delete Image
        public void P2PInvoiceAdministrationCancelDeleteImage()
        {

            //Wait for Delete Image Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Image_DeleteButton.Wait.ForExists(Globals.timeOut);

            //Click on Delete Image Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Image_DeleteButton.User.Click();

            //Wait for OK Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_CancelButton.Wait.ForExists(Globals.timeOut);

            //Click on OK Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_CancelButton.User.Click();

            //Call handle busy indicator
            P2PNavigation.CallBusyIndicator();
        }
    }
}
    

