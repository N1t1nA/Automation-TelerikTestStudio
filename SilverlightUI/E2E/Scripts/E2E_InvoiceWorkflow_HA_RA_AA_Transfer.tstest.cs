using System;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Design.Execution;
using P2P.Testing.Shared.Class;
using System.Data;
using E2E.Class;
using P2P.Testing.Shared;
using System.Globalization;
using P2P.Testing.Shared.Class.InvoiceAdministration.Create;
using P2P.Testing.Shared.Class.InvoiceAdministration.InvoiceDetails;
using P2P.Testing.Shared.Class.InvoiceAdministration;
using P2P.Testing.Shared.Class.InvoiceAdministration.Search;
using P2P.Testing.Shared.Class.InvoiceAdministration.ToolbarActions;
using P2P.Testing.Shared.Class.PaymentPlans.MyTasks;


namespace E2E.Scripts
{

	//
	// You can add custom execution steps by simply
	// adding a void function and decorating it with the [CodedStep] 
	// attribute to the test method. 
	// Those steps will automatically show up in the test steps on save.
	//
	// The BaseWebAiiTest exposes all key objects that you can use
	// to access the current testcase context. [i.e. ActiveBrowser, Find ..etc]
	//
	// Data driven tests can use the Data[columnIndex] or Data["columnName"] 
	// to access data for a specific data iteration.
	//
	// Example:
	//
	// [CodedStep("MyCustom Step Description")]
	// public void MyCustomStep()
	// {
	//		// Custom code goes here
	//      ActiveBrowser.NavigateTo("http://www.google.com");
	//
	//		// Or
	//		ActiveBrowser.NavigateTo(Data["url"]);
	// }
	//
		

    public class E2E_InvoiceWorkflow_HA_RA_AA_Transfer : BaseWebAiiTest
    {
        #region [ Dynamic Pages Reference ]

        private Pages _pages;

        /// <summary>
        /// Gets the Pages object that has references
        /// to all the elements, frames or regions
        /// in this project.
        /// </summary>
		public Pages Pages
		{
			get
			{
				if (_pages == null)
				{
					_pages = new Pages(Manager.Current);
				}
				return _pages;
			}
        }

        #endregion
        
		
        //Declare Global Variable
        private readonly DataRow drE2E;
        string uploadPath;

        //Create an object of EdXmlReader class   
        private readonly EdXmlReader objReadXml = new EdXmlReader();

        //Create an object for P2PExceptionHandler class
        private readonly  P2PExceptionHandler p2pExceptionHandlerObj = new P2PExceptionHandler();

        //Create an object for 'P2PNavigation' class
        private readonly P2PNavigation p2pNavigationObj = new P2PNavigation();

        //Create an object for 'P2PCreateInvoice' class
        private readonly P2PCreateInvoice p2pCreateInvoiceObj = new P2PCreateInvoice();

        //Create an object for P2PInvoiceAdministrationImageActions class
        private readonly P2PInvoiceAdministrationImageActions p2pAddImageObj = new P2PInvoiceAdministrationImageActions();

        //Create an object for P2PInvoiceAdministrationVerification class
        private readonly P2PInvoiceAdministrationVerification p2pInvoiceAdministrationVerificationObj = new P2PInvoiceAdministrationVerification();

        //Create an object for P2PInvoiceAdministrationSearch class
        private readonly P2PInvoiceAdministrationSearch p2pInvoiceAdministrationSearchObj = new P2PInvoiceAdministrationSearch();        
        
        //private readonly P2PReceivedInvoiceDetails p2pReceivedInvoiceDetailsObj = new P2PReceivedInvoiceDetails();
        private readonly P2PInvoiceAdministrationInvoiceDetails p2pInvoiceAdministrationInvoiceDetailsObj = new P2PInvoiceAdministrationInvoiceDetails();        

        //Create an object for P2PMyTask class
        private readonly P2PPaymentPlansMyTasks p2pPaymentPlansMyTasksObj = new P2PPaymentPlansMyTasks();

        //Constructor for E2E_InvoiceWorkflow_HA_RA_AA_Transfer
        public E2E_InvoiceWorkflow_HA_RA_AA_Transfer()
        {
            //Initializing value to declared vaiable 'uploadPath'
            uploadPath = objReadXml.Upload_file("Sample.tiff");
           // DataRow drPath = objReadXml.Read_xml_file("P2PTestSettings.xml", "Invoice_Attachment", "File_Name", "Sample.doc");

            //Read the Data Row from XML 
            drE2E = objReadXml.Read_xml_file("E2E_TestData.xml", "E2E_InvoiceWorkflow_HA_RA_AA_Transfer", "Functionality", "E2InvoiceWorkflow");                                    
            
            //Calling Generate Function
            P2P_Utility.GenerateNumber(drE2E["Functionality"].ToString());
        }

        //[CodedStep("Initial Login into P2P for Application loaded Successfully with Ilkka User")]
        //public void InitialLoginIntoP2P()
        //{
        //    //Perform Initial steps for Application loaded Successfully
        //    LoginIntoP2P();
        //    HandleBusyIndicator();
        //    NavigateToProfessionalMode();
        //    p2pNavigationObj.NavigateInvoiceAdministrationToCreate();
        //    LogoutFromP2P();
        //}

        [CodedStep("Login into P2P with Esa Tihila", RequiresSilverlight = true)]
        public void LoginIntoP2P()
        {
            // Create an object for "P2PLogin" class
            var objlogin = new P2PLogin(ActiveBrowser);

            try
            {
                //Call function for Login into P2P Application                
                objlogin.LoginToSilverlightClient("basware\\esat","");
            }
            catch (Exception loginintoP2P)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(loginintoP2P, Globals.testScriptName);
            }
        }

        [CodedStep("Handle Busy Indicator", RequiresSilverlight = true)]
        public void HandleBusyIndicator()
        {
            try
            {
                //Call function Handle Busy Indicator
                P2PNavigation.CallBusyIndicator();
            }
            catch (Exception handleBusyIndicator)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(handleBusyIndicator, Globals.testScriptName);
            }
        }

        [CodedStep("Navigate to Create", RequiresSilverlight = true)]
        public void NavigateToCreate()
        {
            try
            {
                //Call the Method to Navigate To Create Page 
                p2pNavigationObj.NavigateInvoiceAdministrationToCreate();
            }
            catch (Exception navigateToCreate)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(navigateToCreate, Globals.testScriptName);
            }           
        }

        [CodedStep("Create New Invoice with User Ilkka", RequiresSilverlight = true)]
        public void CreateNewInvoice()
        {
            try
            {
                //Use the method to create a New Invoice                            
                p2pCreateInvoiceObj.P2PInvoiceAdministrationCreateNewInvoice(drE2E["Organization_Unit"].ToString(),drE2E["Invoice_Type"].ToString(),drE2E["Supplier_Code"].ToString(),P2P_Utility.invoiceNumber,Convert.ToDouble(drE2E["Gross_Sum"],CultureInfo.InvariantCulture.NumberFormat),Convert.ToDouble(drE2E["Tax_Sum"],CultureInfo.InvariantCulture.NumberFormat));
            }
            catch (Exception createNewInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(createNewInvoice, Globals.testScriptName);
            }
        }

        [CodedStep("Add Image with Invoice", RequiresSilverlight = true)]
        public void AddImage()
        {
            try
            {
                //Use the object to Access the Method to Save the Invoice Image
                p2pAddImageObj.P2PInvoiceAdministrationAddNewImage(uploadPath, true);
            }
            catch (Exception addedImage)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(addedImage, Globals.testScriptName);
            }

            //Call existing coded step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Verify Add image Dialog", RequiresSilverlight = true)]
        public void VerifyAddImageDialog()
        {
            try
            {
                // Use the method for verify the Image upload dialog
                p2pInvoiceAdministrationVerificationObj.P2PVerifyAddImageUploadDialog(drE2E["Image_Browse_Button"].ToString());
            }
            catch (Exception verifyAddImageDialog)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(verifyAddImageDialog, Globals.testScriptName);
            }
            //Call existing coded step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Verify Image Added To Invoice", RequiresSilverlight = true)]
        public void VerifyImageAdded()
        {
            try
            {
                // Use the method for verify the Image added successfully
                p2pInvoiceAdministrationVerificationObj.P2PDetailPageCommentsVerification(drE2E["ComboBox"].ToString(), drE2E["Image_Comment"].ToString(),P2P_Utility.invoiceNumber, drE2E["Module_Name"].ToString());                
            }
            catch (Exception verifyImageAdded)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(verifyImageAdded, Globals.testScriptName);
            }
        }

        [CodedStep("Click Send To Validation", RequiresSilverlight = true)]
        public void InvoiceSendToValidation()
        {
            try
            {
                //Use the method for Send the Invoice for Validation
                p2pCreateInvoiceObj.P2PInvoiceAdministrationSendToValidation();
            }
            catch (Exception sendToValidation)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(sendToValidation, Globals.testScriptName);
            }
            //Call the existing coded step here for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Received ", RequiresSilverlight = true)]
        public void NavigateToReceived()
        {
            try
            {
                //Call the Method to Navigate To Received Page 
                p2pNavigationObj.NavigateInvoiceAdministrationToReceived();
            }
            catch (Exception navigatetoReceived)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(navigatetoReceived, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator 
            HandleBusyIndicator();
        }

        [CodedStep("Clear All Search on Received Page ", RequiresSilverlight = true)]
        public void ClearAllSearch()
        {
            try
            {
                //Call the Method to Clear All Search 
                p2pInvoiceAdministrationSearchObj.P2PInvoiceAdministrationClearAllSearch();
            }
            catch (Exception clearAllSearch)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(clearAllSearch, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Refresh the Grid ", RequiresSilverlight = true)]
        public void RefreshTheGrid()
        {
            //Create an object for P2PInvoiceAdministrationToolbarActions
            var refreshGrid = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {                
                //Refresh the Received Page
                refreshGrid.P2PInvoiceAdministrationRefreshInvoicePage();
            }
            catch (Exception refreshTheGrid)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(refreshTheGrid, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Search the Invoice in Received Page ", RequiresSilverlight = true)]
        public void SearchInvoice()
        {            
            try
            {
                //Use the Object to Access the Method for Search the Invoice
                p2pInvoiceAdministrationSearchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(P2P_Utility.invoiceNumber);
            }
            catch (Exception searchInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(searchInvoice, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }


        [CodedStep("Verify the Invoice in Received Page ", RequiresSilverlight = true)]
        public void VerifyInvoiceInReceived()
        {
            try
            {
                //Use the method to verify the Invoice is Exists in Received Page                                       
                p2pInvoiceAdministrationVerificationObj.P2PVerifyFreeTextSearch(drE2E["Header_Name"].ToString(), 10, P2P_Utility.invoiceNumber, "SEARCH");
            }
            catch (Exception verifyInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(verifyInvoice, Globals.testScriptName);
            }
        }

        [CodedStep("Invoice Send To Process", RequiresSilverlight = true)]
        public void SendToProcess()
        {
            try
            {
                //Use the object to access the Method for Invoice Send To Process               
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PSendInvoiceToProcess();                 
            }
            catch (Exception sendToProcess)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(sendToProcess, Globals.testScriptName);
            }           
        }



        [CodedStep("Invoice Send To Recipients", RequiresSilverlight = true)]
        public void InvoiceSendToRecipients()
        {
            try
            {
                //Use the object to access the Method for Invoice Send To Reviewer               
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationSelectRecipients(drE2E["Reviewer"].ToString());                
            }
            catch (Exception invoiceSendToRecipients)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(invoiceSendToRecipients, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Logout Esa Tihilia From P2P", RequiresSilverlight = true)]
        public void LogoutFromP2P()
        {
            // Create an object for "P2PLogin" class
            P2PLogin objlogout = new P2PLogin(Manager.Current.ActiveBrowser);
            try
            {
                //Call method for logout into the P2P Alusta Application
                objlogout.LogoutFromSilverlightClient();
            }
            catch (Exception logoutFromP2P)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(logoutFromP2P, Globals.testScriptName);
            }
        }

        [CodedStep("Login into P2P with Juha Tillqvist", RequiresSilverlight = true)]
        public void FirstReviewerLoginIntoP2P()
        {
            //Create an object for P2PLogin class  
            P2PLogin objlogin = new P2PLogin(ActiveBrowser);

            try
            {
                //Call method for login into the P2P Alusta Application
                objlogin.LoginToSilverlightClient("basware\\Juhat", "");
                
            }
            catch (Exception firstApproverLoginintoP2P)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(firstApproverLoginintoP2P, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Personnel Mode", RequiresSilverlight = true)]
        public void NavigateToPersonalMode()
        {
            //Call the Coded Step Navigate To Personel Mode Again
            p2pNavigationObj.NavigateToPersonnelMode();

            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to MyTask Page", RequiresSilverlight = true)]
        public void NavigateToMyTasks()
        {
            try
            {
                //Call the Method to Navigate To MyTasks Page
                p2pNavigationObj.NavigateToMyTasks();
            }
            catch (Exception navigateToMyTasks)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(navigateToMyTasks, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Invoice Page in My Tasks: Personal Mode", RequiresSilverlight = true)]
        public void NavigateToInvoicePage()
        {
            try
            {
                //Call the Method to Navigate To MyTasks Page
                p2pNavigationObj.NavigateMyTasksToInvoice();
            }
            catch (Exception navigateToInvoicePage)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(navigateToInvoicePage, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Search the Invoice in Invoice Page ", RequiresSilverlight = true)]
        public void SearchInvoiceInInvoicePage()
        {           
            try
            {
                //Use the Object to Access the Method for Search the Invoice
                p2pInvoiceAdministrationSearchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(P2P_Utility.invoiceNumber, drE2E["Functionality"].ToString());
            }
            catch (Exception searchInvoiceInInvoicePage)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(searchInvoiceInInvoicePage, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Add Comment to Invoice ", RequiresSilverlight = true)]
        public void AddComment()
        {
            try
            {                
                //Use the method to Add a Comment
                p2pPaymentPlansMyTasksObj.P2PMyTaskAddNewComment(drE2E["Reviewer_Comment"].ToString(), drE2E["Add_Comment"].ToString());
            }
            catch (Exception addedComment)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(addedComment, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Verify Comment to Invoice ", RequiresSilverlight = true)]
        public void VerifyComment()
        {
            try
            {                
                //Verify Comments after Added Attachment under History and Attachment section 
                p2pInvoiceAdministrationVerificationObj.P2PDetailPageCommentsVerification(drE2E["ComboBox"].ToString(), drE2E["Reviewer_Comment"].ToString(), P2P_Utility.invoiceNumber, drE2E["History_And_Comment_TabItem"].ToString());
            }
            catch (Exception verifyComment)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(verifyComment, Globals.testScriptName);
            }

            //Call the Coded Step Handle Busy Indicator Again
            //HandleBusyIndicator();
        }

        [CodedStep("Add Attachment to Invoice ", RequiresSilverlight = true)]
        public void AddAttachment()
        {
            try
            {
                //Use the Object to Access the Method for Search the Invoice
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationAddAttachment(uploadPath, drE2E["AddAttachment_Description"].ToString(), null, drE2E["Put_On_Hold"].ToString());                
            }
            catch (Exception addAttachment)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(addAttachment, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();

            try
            {
                //Use the Object to Access the Method  to Verify the Attachment upload
                p2pInvoiceAdministrationVerificationObj.P2PVerifyDialog();
            }
            catch (Exception verifyUploaddialog)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(verifyUploaddialog, Globals.testScriptName);
            }
        }

        [CodedStep("Verify Add Attachment Under Attachment Tab", RequiresSilverlight = true)]
        public void VerifyAddAttachment()
        {           
            try
            {
                //Use the Object to Access the Method to verify the  Attachement files
                p2pInvoiceAdministrationVerificationObj.P2PAttachmentTabDetailPageVerification(drE2E["File_Name"].ToString(), P2P_Utility.invoiceNumber, "Add", "History Comments TabItem", drE2E["File_Name"].ToString());
            }
            catch (Exception verifyAddAttachment)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(verifyAddAttachment, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Verify Comments after Added Attachment Under History And Attachment Tab", RequiresSilverlight = true)]
        public void VerifyCommentsAfterAddAttachment()
        {           
            try
            {                
                //Verify Comments after Added Attachment under History and Attachment section 
                p2pInvoiceAdministrationVerificationObj.P2PDetailPageCommentsVerification(drE2E["ComboBox"].ToString(), drE2E["Attachment_Comment"].ToString(), P2P_Utility.invoiceNumber, drE2E["History_And_Comment_TabItem"].ToString());
            }
            catch (Exception verifyCommentsAfterAddAttachment)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(verifyCommentsAfterAddAttachment, Globals.testScriptName);
            }
        }

        [CodedStep("Invoice Put on Hold", RequiresSilverlight = true)]
        public void InvoicePutOnHold()
        {
            try
            {                
                //Use the method to Task put On Hold
                p2pPaymentPlansMyTasksObj.P2PMyTaskPutOnHold(drE2E["Invoice_PutOnHold_Comment"].ToString(), drE2E["Put_On_Hold"].ToString());
            }
            catch (Exception invoicePutOnHold)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(invoicePutOnHold, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Invoice Release from Hold", RequiresSilverlight = true)]
        public void InvoiceReleaseFromHold()
        {
            try
            {
                //Use the Object to Access the Method for Invoice ReleaseFromHold
                p2pPaymentPlansMyTasksObj.P2PMyTaskReleaseFromHold(drE2E["Release_Comment"].ToString(), drE2E["ReleaseButton_AutomationID"].ToString());
            }
            catch (Exception invoiceReleaseFromHold)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(invoiceReleaseFromHold, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }
       
        [CodedStep("Create a Coding Rows in My Task Page", RequiresSilverlight = true)]
        public void CreateCodingRows()
        {
            try
            {   //Use the Object to Access the Method for Create Coding Row
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationMyTasksCreateCodingRows(Convert.ToInt32(drE2E["Number_Of_Rows"].ToString()),drE2E["Account_Code_Row1"].ToString(),drE2E["costCenterCode_Row1"].ToString(),Convert.ToDouble(drE2E["Net_Sum"], CultureInfo.InvariantCulture.NumberFormat), drE2E["Tax_Code"].ToString());
            }
            catch (Exception createCodingRows)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(createCodingRows, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Reviewer Reviewed the Newly Created Invoice: Header Review", RequiresSilverlight = true)]
        public void ReviewerReviewedTheInvoice()
        {
            //Create a object for P2PInvoiceAdministrationToolbarActions class
            var toolbarAction = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //call the common function to click on Review button
                toolbarAction.P2P_Invoice_Administration_MyTasks_ToolBarActions(drE2E["Review_Button_AutomationID"].ToString());
            }
            catch (Exception reviewerReviewedTheInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(reviewerReviewedTheInvoice, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();    
        }

        [CodedStep("Logout Basware Juha Tillqvist From P2P", RequiresSilverlight = true)]
        public void LogoutReviewerFromP2P()
        {
            // Create an object for "P2PLogin" class
            P2PLogin objlogout = new P2PLogin(Manager.Current.ActiveBrowser);

            try
            {
                //Call method to logout into the P2P Alusta Application
                objlogout.LogoutFromSilverlightClient();
            }
            catch (Exception logoutReviewerFromP2P)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(logoutReviewerFromP2P, Globals.testScriptName);
            }
        }

        [CodedStep("Login into P2P with Aki Salminen", RequiresSilverlight = true)]
        public void FirstRowApproverLoginIntoP2P()
        {
            //Create an object for P2PLogin class  
            P2PLogin objlogin = new P2PLogin(ActiveBrowser);
            try
            {
                //Call method for login into the P2P Alusta Application
                objlogin.LoginToSilverlightClient("basware\\Akis", "");
            }
            catch (Exception firstApproverLoginintoP2P)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(firstApproverLoginintoP2P, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Personal Mode", RequiresSilverlight = true)]
        public void FirstRowApproverNavigateToPersonalMode()
        {
            //Call the Coded Step Navigate To Personal Mode 
            NavigateToPersonalMode();

            //Call Coded Step HandleBusyIndicator
            //HandleBusyIndicator();
        }

        [CodedStep("Navigate to MyTask Page: First Row Approver", RequiresSilverlight = true)]
        public void FirstRowApproverNavigateToMyTasks()
        {
            //Call the Coded Step NavigateToMyTasks
            NavigateToMyTasks();
            p2pNavigationObj.NavigateToWebShop();
            HandleBusyIndicator();
            NavigateToMyTasks();

        }

        [CodedStep("Navigate to My Tasks InvoicePage", RequiresSilverlight = true)]
        public void FirstRowApproverNavigateToMyTasksInvoicePage()
        {
            
            //Call the Coded Step NavigateToInvoicePage
           // p2pNavigationObj.NavigateToMyTaskPurchaseRequisitionPage();
           // HandleBusyIndicator();
          //  p2pNavigationObj.NavigateToMyTasksGoodReceiptPage();
            HandleBusyIndicator();           
            NavigateToInvoicePage();

        }

        [CodedStep("Search Invoice in Invoice Page", RequiresSilverlight = true)]
        public void FirstRowApproverSearchInvoice()
        {
            //Call the Coded Step Navigate To Personal Mode 
            SearchInvoiceInInvoicePage();

            //Call Coded Step HandleBusyIndicator
           // HandleBusyIndicator(); 
        }

        [CodedStep("First Row Approver Approved the Invoice: In Row Approval", RequiresSilverlight = true)]
        public void FirstRowApproverApprovedInvoice()
        {
            //Create a object for P2PInvoiceAdministrationToolbarActions class
            var toolbarAction = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);

            try
            {
                //call the common function to click on Confirm button
                toolbarAction.P2P_Invoice_Administration_MyTasks_ToolBarActions(drE2E["Confirm_Button_AutomationID"].ToString());
            }
            catch (Exception firstRowApproverApprovedInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(firstRowApproverApprovedInvoice, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();           
        }

        [CodedStep("Logout from P2P Application: First Row Approver", RequiresSilverlight = true)]
        public void LogoutFirstRowApprover()
        {
            //Call the coded step to logout from the P2p Alusta Application
            LogoutFromP2P();
        }

        [CodedStep("Login into P2P with Jari Antikanen", RequiresSilverlight = true)]
        public void SecondRowApproverLoginIntoP2P()
        {
            //Create an object for P2PLogin class  
            P2PLogin objlogin = new P2PLogin(ActiveBrowser);

            try
            {
                //Call the Method to Login into Application
                objlogin.LoginToSilverlightClient("basware\\Jaria", "");
            }
            catch (Exception secondRowApproverLoginIntoP2P)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(secondRowApproverLoginIntoP2P, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Personal Mode", RequiresSilverlight = true)]
        public void SecondRowApproverNavigateToPersonalMode()
        {
            //Call the Coded Step Navigate To Personel Mode 
            NavigateToPersonalMode();

            //Call the Coded Step Handle Busy Indicator 
            //HandleBusyIndicator();
        }

        [CodedStep("Second Row Approver Navigate to MyTask Page", RequiresSilverlight = true)]
        public void SecondRowApproverNavigateToMyTasks()
        {
            //Call the Coded Step NavigateToMyTasks
            NavigateToMyTasks();
        }

        [CodedStep("Second Row Approver Navigate to My Tasks Invoice Page", RequiresSilverlight = true)]
        public void SecondRowApproverNavigateToMyTasksInvoicePage()
        {
            //Call the Coded Step NavigateToInvoicePage
            NavigateToInvoicePage();
        }

        [CodedStep("Second Row Approver Search Inovice", RequiresSilverlight = true)]
        public void SecondRowApproverSearchInvoice()
        {
            //Call the Coded Step SearchInvoiceInInvoicePage
            SearchInvoiceInInvoicePage();            
        }

        [CodedStep("Second Row Approver Approved Invoice", RequiresSilverlight = true)]
        public void SecondRowApproverApprovedInvoice()
        {
            try
            {
                //Call the Coded Step to Approved Invoice            
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PSelectRecipient(drE2E["HeaderApprover"].ToString());
            }
            catch (Exception secondRowApproverApprovedInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(secondRowApproverApprovedInvoice, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Logout from P2P Application: Second Row Approver", RequiresSilverlight = true)]
        public void LogoutSecondRowApprover()
        {
            //Call the Coded Step Logout 
            LogoutFromP2P();
        }

        [CodedStep("Login into P2P with Jari Antikanen", RequiresSilverlight = true)]
        public void HeaderApproverLoginIntoP2P()
        {
            //Create an object for P2PLogin class  
            P2PLogin objlogin = new P2PLogin(ActiveBrowser);
            try
            {
                //Call the Method to Login into P2P Alusta Application                
                objlogin.LoginToSilverlightClient("basware\\jaria", "");
            }
            catch (Exception headerApproverLoginIntoP2P)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(headerApproverLoginIntoP2P, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Personal Mode", RequiresSilverlight = true)]
        public void HeaderApproverNavigateToPersonalMode()
        {
            //Call the Coded Step Navigate To Personel Mode Again
            NavigateToPersonalMode();

            //Call the Coded Step Handle Busy Indicator Again
            //HandleBusyIndicator();
        }

        [CodedStep("Header Approver Navigate to MyTask Page", RequiresSilverlight = true)]
        public void HeaderApproverNavigateToMyTasks()
        {
            //Call the Coded Step Navigate To Personel Mode Again
            NavigateToMyTasks();
        }

        [CodedStep("Header Approver Navigate to My Tasks Invoice Page", RequiresSilverlight = true)]
        public void HeaderApproverNavigateToMyTasksInvoicePage()
        {
            //Call the Coded Step NavigateToInvoicePage Again
            NavigateToInvoicePage();
        }

        [CodedStep("Header Approver Search Inovice", RequiresSilverlight = true)]
        public void HeaderApproverrSearchInvoice()
        {
            //Call the Coded Step Navigate SearchInvoiceInInvoicePage Again
            SearchInvoiceInInvoicePage();           
        }

        [CodedStep("Header Approver Approved Invoice: In Header Approval", RequiresSilverlight = true)]
        public void HeaderApproverApprovedInvoice()
        {
            //Create a object for P2PInvoiceAdministrationToolbarActions class           
            var toolbarAction = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //call the common function to click on Confirm button
                toolbarAction.P2P_Invoice_Administration_MyTasks_ToolBarActions(drE2E["Approved_Button_AutomationID"].ToString());
            }
            catch (Exception headerApproverApprovedInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(headerApproverApprovedInvoice, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();                     
        }

        [CodedStep("Logout from P2P Application: In Header Approval", RequiresSilverlight = true)]
        public void LogoutHeaderApprover()
        {
            //Call method for logout into the P2P Alusta Application
            LogoutFromP2P();
        }

        [CodedStep("Login into P2P with Esa Tihila", RequiresSilverlight = true)]
        public void AdditionalApproverLoginIntoP2P()
        {
            //Call method for login into the P2P Alusta Application
            LoginIntoP2P();

            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();   
        }

        [CodedStep("Navigate to Professional Mode", RequiresSilverlight = true)]
        public void NavigateToProfessionalMode()
        {
            //Call the Coded Step Navigate To Personel Mode Again
            p2pNavigationObj.NavigateToProfessionalMode();

            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }
        
        [CodedStep("Navigate to Workflow Silo", RequiresSilverlight = true)]
        public void NavigateToWorkflow()
        {
            //Call the Coded Step Navigate To Personel Mode Again
            p2pNavigationObj.NavigateInvoiceAdministrationToWorkflow();

            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Search Invoice in Workflow Silo", RequiresSilverlight = true)]
        public void SearchInvoiceInWorkflow()
        {
            //Call the Coded Step ClearAllSearch & RefreshTheGrid
            ClearAllSearch();
            RefreshTheGrid();

            try
            {
                //Call the Method to Navigate To Search Invoice 
                p2pInvoiceAdministrationSearchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(P2P_Utility.invoiceNumber);
            }
            catch (Exception searchInvoiceInWorkflow)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(searchInvoiceInWorkflow, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Invoice Forwarded to Illka", RequiresSilverlight = true)]
        public void ForwardTasks()
        {
            //Create a object for P2PInvoiceAdministrationToolbarActions class           
            var invoiceForwarded = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //Use the Object to access the Method forward Invoice
                invoiceForwarded.P2PWorkflowForwardProcess(drE2E["AdditionalApproval"].ToString(), drE2E["Forwarded_Comment"].ToString());
            }
            catch (Exception forwardTasks)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(forwardTasks, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Personal Mode : Esa Tihila", RequiresSilverlight = true)]
        public void AdditionalApproverNavigateToPersonalMode()
        {
            //Call the Coded Step Navigate To Personel Mode Again
            NavigateToPersonalMode();

            //Call Coded Step HandleBusyIndicator
            //HandleBusyIndicator();
        }

        [CodedStep("Additional Approver Navigate to MyTask Page: Esa Tihila", RequiresSilverlight = true)]
        public void AdditionalApproveNavigateToMyTasks()
        {
            //Call the Coded Step NavigateToMyTasks Again
            NavigateToMyTasks();
        }

        [CodedStep("Additional Approver Navigate to My Tasks Invoice Page: Esa Tihila", RequiresSilverlight = true)]
        public void AdditionalApproveNavigateToMyTasksInvoicePage()
        {
            //Call the Coded Step NavigateToInvoicePage Again
            NavigateToInvoicePage();
        }

        [CodedStep("Additional Approver Search the Inovice", RequiresSilverlight = true)]
        public void AdditionalApproverSearchInvoice()
        {
            //Call the Coded Step SearchInvoiceInInvoicePage Again
            SearchInvoiceInInvoicePage();

            //Call Coded Step HandleBusyIndicator
            //HandleBusyIndicator();      
        }

        [CodedStep("Additional Approver Approved the Invoice : Esa Tihila", RequiresSilverlight = true)]
        public void AdditionalApproverApprovedInvoice()
        {
            //Create a object for P2PInvoiceAdministrationToolbarActions class                       
            var toolbarAction = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);

            try
            {
                //call the common function to click on Confirm button
                toolbarAction.P2P_Invoice_Administration_MyTasks_ToolBarActions(null, drE2E["Approve_ToolBar_Button"].ToString());
            }
            catch (Exception additionalApproverApprovedInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(additionalApproverApprovedInvoice, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();              
        }

        [CodedStep("Navigate to Professional Mode after Approval: Esa Tihila", RequiresSilverlight = true)]
        public void NavigateToProfessionalModeAfterApproval()
        {
            //Call the Coded Step NavigateToProfessionalMode Again
            p2pNavigationObj.NavigateToProfessionalMode();

            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Transfer Silo", RequiresSilverlight = true)]
        public void NavigateToTransfer()
        {
            //Call the Coded Step NavigateInvoiceAdministrationToTransfer
            p2pNavigationObj.NavigateInvoiceAdministrationToTransfer();

            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Search Invoice in Transfer Silo", RequiresSilverlight = true)]
        public void AdditionalApproverSearchInvoiceInWorkflow()
        {
            //Call Coded Step ClearAllSearch & RefreshTheGrid
            ClearAllSearch();
            RefreshTheGrid();            

            try
            {
                //Call the Method to SearchInvoiceInWorkflow
                SearchInvoiceInWorkflow();
            }
            catch (Exception additionalApproverSearchInvoiceInWorkflow)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(additionalApproverSearchInvoiceInWorkflow, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Verify Invoice in Transfer Silo with Status: Ready for transfer", RequiresSilverlight = true)]
        public void VerifyTheInvoiceStatus()
        {
            try
            {
                // Call the Method "P2InvoiceAdministration_Search_VerifyInvoiceStatus" for Verify the Message in a Grid
                p2pInvoiceAdministrationVerificationObj.P2InvoiceAdministration_Search_VerifyInvoiceStatus(P2P_Utility.invoiceNumber, drE2E["Invoice_Final_Status"].ToString(), drE2E["Header_Name_Status"].ToString(), drE2E["Button_Text"].ToString(), drE2E["Invoice_Final_Status1"].ToString());
            }
            catch (Exception verifyTheInvoiceStatus)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(verifyTheInvoiceStatus, Globals.testScriptName);
            }
        }

        [CodedStep("Logout from P2P Application: Esa Tihila", RequiresSilverlight = true)]
        public void LogoutfromP2PApplication()
        {
            //Call the Coded Step Logout Again
            LogoutFromP2P();
        }     
                
    }
}
