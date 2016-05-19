using System;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Design.Execution;
using P2P.Testing.Shared.Class;
using P2P.Testing.Shared.Class.InvoiceAdministration.Create;
using P2P.Testing.Shared.Class.InvoiceAdministration;
using P2P.Testing.Shared.Class.InvoiceAdministration.Search;
using P2P.Testing.Shared.Class.InvoiceAdministration.InvoiceDetails;
using E2E.Class;
using System.Data;
using P2P.Testing.Shared;
using P2P.Testing.Shared.Class.PaymentPlans.Create;
using P2P.Testing.Shared.Class.PaymentPlans.ToolbarActions;
using P2P.Testing.Shared.Class.PaymentPlans.Search;
using P2P.Testing.Shared.Class.InvoiceAdministration.ToolbarActions;
using P2P.Testing.Shared.Class.PaymentPlans.MyTasks;
using P2P.Testing.Shared.Class.PaymentPlans.InvoiceDetails;
using System.Globalization;

namespace E2E.Scripts
{
    public class E2E_Invoice_ManualMatch_PaymentPlan : BaseWebAiiTest
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
        private readonly P2PExceptionHandler p2pExceptionHandlerObj = new P2PExceptionHandler();

        //Create an object for 'P2PNavigation' class
        private readonly P2PNavigation p2pNavigationObj = new P2PNavigation();

        //Create an object for 'P2PCreateInvoice' class
        private readonly P2PCreateInvoice p2pCreateInvoiceObj = new P2PCreateInvoice();

        //Create an object for 'P2PCreatePaymentPlan' class
        private readonly P2PCreatePaymentPlan p2pCreatePaymentPlanObj = new P2PCreatePaymentPlan();       

        //Create an object for P2PInvoiceAdministrationVerification class
        private readonly P2PInvoiceAdministrationVerification p2pInvoiceAdministrationVerificationObj = new P2PInvoiceAdministrationVerification();

        //Create an object for P2PInvoiceAdministrationSearch class
        private readonly P2PInvoiceAdministrationSearch p2pInvoiceAdministrationSearchObj = new P2PInvoiceAdministrationSearch();

        //Create an object for P2PPaymentPlansSearch class
        private readonly P2PPaymentPlansSearch p2pPaymentPlansSearchObj = new P2PPaymentPlansSearch();

        //Create an object for P2PInvoiceAdministrationInvoiceDetails class
        private readonly P2PInvoiceAdministrationInvoiceDetails p2pInvoiceAdministrationInvoiceDetailsObj = new P2PInvoiceAdministrationInvoiceDetails();

        //Create an object for P2PPaymentPlansMyTasks class
        private readonly P2PPaymentPlansMyTasks p2pPaymentPlansMyTasksObj = new P2PPaymentPlansMyTasks();        

        //Create an object for P2PPaymentPlansDetails class
        private readonly P2PPaymentPlansDetails p2pPaymentPlansDetailsObj = new P2PPaymentPlansDetails();       

        //Create an object for P2PInvoiceAdministrationImageActions class
        private readonly P2PInvoiceAdministrationImageActions p2pAddImageObj = new P2PInvoiceAdministrationImageActions();

        //Constructor for E2E_Invoice_ManualMatch_PaymentPlan
        public E2E_Invoice_ManualMatch_PaymentPlan()
        {
            //Initializing value to declared vaiable 'uploadPath'
            uploadPath = objReadXml.Upload_file("Sample.tiff");

            //Read the Data Row from XML 
            drE2E = objReadXml.Read_xml_file("E2E_TestData.xml", "E2E_Invoice_ManualMatch_PaymentPlan", "Functionality", "Invoice_ManualMatch_PaymentPlan");

            //Calling Generate Function
            P2P_Utility.GenerateNumber(drE2E["Functionality"].ToString());
        }

        //[CodedStep("Initial Login into P2P for Application loaded Successfully with Esat User")]
        //public void InitialLoginIntoP2P()
        //{
        //    //Perform Initial steps to check the Application loaded Successfully
        //    LoginIntoP2PWithEsaTihila();           
        //    // NavigateToPaymentPlansCreatePage();
        //    p2pNavigationObj.NavigateToPaymentPlans();
        //    //Call handled Busy Indicator
        //    HandleBusyIndicator();
        //    //Logout from the Application
        //    LogoutFromP2P();
        //}

        [CodedStep("Login into P2P with Esa Tihila", RequiresSilverlight = true)]
        public void LoginIntoP2PWithEsaTihila()
        {
            //Create an object for "P2PLogin" class
            var objlogin = new P2PLogin(ActiveBrowser);

            try
            {
                //Call function for Login into P2P Application                
                objlogin.LoginToSilverlightClient("basware\\esat", "");
            }
            catch (Exception LoginIntoP2PWithEsaTihila)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(LoginIntoP2PWithEsaTihila, Globals.testScriptName);
            }
            //Call handled Busy Indicator
            HandleBusyIndicator();
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

        [CodedStep("Navigate to PaymentPlans", RequiresSilverlight = true)]
        public void NavigateToPaymentPlans()
        {
            try
            {
                //Call the Method to Navigate To PaymentPlans Page 
                p2pNavigationObj.NavigateToPaymentPlans();
            }
            catch (Exception navigateToPaymentPlans)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(navigateToPaymentPlans, Globals.testScriptName);
            }

            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate To PaymentPlans Create Page", RequiresSilverlight = true)]
        public void NavigateToPaymentPlansCreatePage()
        {
            try
            {
                //Call the Method to Navigate To PaymetPlans Create Page 
                p2pNavigationObj.NavigateToPaymentPlansCreatePage();
            }
            catch (Exception navigateToPaymentPlansCreatePage)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(navigateToPaymentPlansCreatePage, Globals.testScriptName);
            }

            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Create A Budget Based PaymentPlans From Payment Plan Create Page", RequiresSilverlight = true)]
        public void CreateNewBudgetBasedPaymentPlan()
        {
            try
            {
                //Call the Method to Create New Budget Based Payment Plan  
                p2pCreatePaymentPlanObj.P2PPaymentPlansCreateNewPaymentPlanSelectInvoiceType(drE2E["Organization_Unit"].ToString(), drE2E["Invoice_Type"].ToString(), drE2E["PaymentPlan_Type"].ToString());
            }
            catch (Exception createNewBudgetBasedPaymentPlan)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(createNewBudgetBasedPaymentPlan, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Create A Budget Based PaymentPlans From Payment Plan and added Header Data Fields", RequiresSilverlight = true)]
        public void CreateNewPaymentPlanAddHeaderData()
        {
            try
            {
                //Call the Method to Added a header datat feilds                
                p2pCreatePaymentPlanObj.P2PPaymentPlansCreateNewPaymentPlan(P2P_Utility.invoiceNumber, drE2E["Supplier_Code"].ToString());
            }
            catch (Exception createnewpaymentplanaddheaderdata)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(createnewpaymentplanaddheaderdata, Globals.testScriptName);
            }
        }

        [CodedStep("Create a Budget Row", RequiresSilverlight = true)]
        public void CreateBudgetRow()
        {
            try
            {
                //Call the Method to Create Budget Row
                p2pCreatePaymentPlanObj.P2PPaymentPlansCreateBudgetRow(drE2E["BudgetRow_TabItem"].ToString(), Convert.ToDouble(drE2E["Budget"], System.Globalization.CultureInfo.InvariantCulture.NumberFormat), drE2E["Invoice_lines"].ToString(), Convert.ToDouble(drE2E["Invoice_Total"], System.Globalization.CultureInfo.InvariantCulture.NumberFormat));
            }
            catch (Exception createBudgetRow)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(createBudgetRow, Globals.testScriptName);
            }
        }

        [CodedStep("Create a New Coding Row", RequiresSilverlight = true)]
        public void CreateNewCodingRow()
        {
            try
            {
                //Call the Method to Create New Coding Row
                p2pCreatePaymentPlanObj.P2PPaymentPlansCreateNewBudgetCodingRow(drE2E["Coding_Row_TabItem"].ToString(), Convert.ToDouble(drE2E["Percentage"], System.Globalization.CultureInfo.InvariantCulture.NumberFormat));
            }
            catch (Exception createNewCodingRow)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(createNewCodingRow, Globals.testScriptName);
            }
        }

        [CodedStep("Click on Send To Validate Button", RequiresSilverlight = true)]
        public void SendToValidatePaymentPlan()
        {
            //Create an object for P2PPaymentPlansToolbarActions class
            P2PPaymentPlansToolbarActions p2pPaymentPlansToolbarActionsObj = new P2PPaymentPlansToolbarActions(ActiveBrowser);

            try
            {
                //Call the Method to Create New Coding Row
                p2pPaymentPlansToolbarActionsObj.P2PPaymentPlanClickOnValidateButton(drE2E["Validate_Button"].ToString());
            }
            catch (Exception sendToValidatePaymentPlan)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(sendToValidatePaymentPlan, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Payment Plan Received Page", RequiresSilverlight = true)]
        public void NavigateToPaymentPlanReceived()
        {
            try
            {
                //Call the Method to Navigate To Payment Plan Received 
                p2pNavigationObj.NavigatePaymentPlansToReceived();
            }
            catch (Exception navigatePaymentPlansToReceived)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(navigatePaymentPlansToReceived, Globals.testScriptName);
            }
            //Handle Busy Indicator
            HandleBusyIndicator();
        }

        [CodedStep("Search Payment Plan", RequiresSilverlight = true)]
        public void SearchPaymentPlan()
        {
            try
            {
                //Call the Method to Search for Payment Plan
                p2pPaymentPlansSearchObj.P2PPaymentPlansFreeTextSearch(P2P_Utility.invoiceNumber);
            }
            catch (Exception searchPaymentPlan)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(searchPaymentPlan, Globals.testScriptName);
            }
            //Handle Busy Indicator
            HandleBusyIndicator();
        }

        [CodedStep("Verify Payment Plan: In Received Page shows", RequiresSilverlight = true)]
        public void VerifyPaymentPlanReceivedPage()
        {           
            //Added this Verification code as the Application response time is slow and it doesnt show PP in time.
            try
            {
                //Call the Method to Verify PR starts showing under correct tab                
                p2pInvoiceAdministrationVerificationObj.P2PVerifyFreeTextSearch("Plan Name", 10, P2P_Utility.invoiceNumber, "IA_SEARCH");
            }
            catch (Exception verifyPaymentPlanReceivedPage)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(verifyPaymentPlanReceivedPage, Globals.testScriptName);
            }
        }        

        [CodedStep("Open Selected Payment Plan", RequiresSilverlight = true)]
        public void OpenSelectedPaymentPlan()
        {
            //Create an object for shared class
            P2PInvoiceAdministrationToolbarActions openPaymentPlan = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //Call the Method to Open the Selected Plan                 
                openPaymentPlan.P2PInvoiceAdministrationOpenSelectedInvoice();
            }
            catch (Exception openSelectedPaymentPlan)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(openSelectedPaymentPlan, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Click on SendToProcess Button", RequiresSilverlight = true)]
        public void ClickOnSendToProcessButton()
        {
            try
            {
                //Call the Method to SendToProcess Selected Plan 
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PSendInvoiceToProcess(drE2E["Send_To_Process_Button"].ToString());
            }
            catch (Exception ClickOnSendToProcessButton)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(ClickOnSendToProcessButton, Globals.testScriptName);
            }
        }

        [CodedStep("Payment Plan Send To Recipients", RequiresSilverlight = true)]
        public void PaymentPlanSendToRecipients()
        {
            try
            {
                //Call the Method to Selected Plan  for Send To Reviewer               
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationSelectRecipients(drE2E["Recipients"].ToString());
            }
            catch (Exception paymentPlanSendToRecipients)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(paymentPlanSendToRecipients, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();

            //Navigate to Approved Page
            NavigatePaymentPlansToApproved();

        }

        [CodedStep("Logout From P2P", RequiresSilverlight = true)]
        public void LogoutFromP2P()
        {
            //Create an object for "P2PLogin" class
            P2PLogin objlogout = new P2PLogin(ActiveBrowser);

            try
            {
                //Call method "LogoutToSilverlightClient" from the "P2PLogin" class
                objlogout.LogoutFromSilverlightClient();
            }
            catch (Exception logoutFromP2P)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(logoutFromP2P, Globals.testScriptName);
            }
        }

        [CodedStep("Login into P2P with Ojala Jari", RequiresSilverlight = true)]
        public void LoginIntoP2PWithOjalaJari()
        {
            // Create an object for "P2PLogin" class
            var objlogin = new P2PLogin(ActiveBrowser);

            try
            {
                //Call the Method to login into the application               
                objlogin.LoginToSilverlightClient("basware\\Jario", "");
            }
            catch (Exception loginIntoP2PWithOjalaJari)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(loginIntoP2PWithOjalaJari, Globals.testScriptName);
            }
            //Call handled Busy Indicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Personal Mode", RequiresSilverlight = true)]
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
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Payment Plan in My Tasks: Personal Mode", RequiresSilverlight = true)]
        public void NavigateToPaymentPlan()
        {
            try
            {
                //Call the Method to Navigate to Payment Plan
                p2pNavigationObj.NavigateToMyTaskPaymentPlanPage();
            }
            catch (Exception navigateToPaymentPlan)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(navigateToPaymentPlan, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Click on Pending Tab", RequiresSilverlight = true)]
        public void ClickOnPendingTab()
        {
            try
            {
                //Call method to click on Pending Tab
                p2pPaymentPlansMyTasksObj.P2PMyTasksPaymentPlanClickOnTab(drE2E["Pending_Tab"].ToString());
            }
            catch (Exception clickOnPendingTab)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(clickOnPendingTab, Globals.testScriptName);
            }
        }

        [CodedStep("Search the Plan in Payment Plan Page ", RequiresSilverlight = true)]
        public void SearchPlanInPaymentPlanPage()
        {
            try
            {
                //Call the method to search the plan
                p2pInvoiceAdministrationSearchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(P2P_Utility.invoiceNumber);

            }
            catch (Exception searchPlanInPaymentPlanPage)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(searchPlanInPaymentPlanPage, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Put Payment Plan OnHold", RequiresSilverlight = true)]
        public void PutPaymentPlanOnHold()
        {
            try
            {
                //Use the method to Payment Plan put On Hold
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PMyTaskPutOnHold(drE2E["Payment_PutOnHold_Comment"].ToString(), drE2E["Put_On_Hold"].ToString());
            }
            catch (Exception putPaymentPlanOnHold)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(putPaymentPlanOnHold, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Logout From P2P User : Ojala Jari", RequiresSilverlight = true)]
        public void LogoutFromP2PUserJariO()
        {
            //Call the coded step to logout from the application
            LogoutFromP2P();
        }

        [CodedStep("Navigate to Professional Mode", RequiresSilverlight = true)]
        public void NavigateToProfessionalMode()
        {
            //Call the Coded Step NavigateToProfessionalMode Again
            p2pNavigationObj.NavigateToProfessionalMode();

            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate To PaymentPlan Workflow Page", RequiresSilverlight = true)]
        public void NavigateToPaymentPlansWorkflowPage()
        {
            try
            {
                //Call the Method to Navigate To PaymetPlans Workflow Page 
                p2pNavigationObj.NavigatePaymentPlansToInWorkflow();
            }
            catch (Exception navigateToPaymentPlansWorkflowPage)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(navigateToPaymentPlansWorkflowPage, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Search And Open Selected Payment Plan ", RequiresSilverlight = true)]
        public void SearchAndOpenSelectedPaymentPlan()
        {
            //Call the coded step here for search and open selected payment plan
            SearchPaymentPlan();
            OpenSelectedPaymentPlan();
        }

        [CodedStep("Click on Task Management Tab", RequiresSilverlight = true)]
        public void ClickOnTaskManagementTab()
        {
            try
            {
                //call the method to click on Task Management Tab
                p2pPaymentPlansDetailsObj.P2PPaymentPlanClickOnTaskManagementTab();
            }
            catch (Exception clickOnTaskManagementTab)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(clickOnTaskManagementTab, Globals.testScriptName);
            }
        }

        [CodedStep("Release Payment Plan", RequiresSilverlight = true)]
        public void ReleasePaymentPlan()
        {
            try
            {
                //call the method to Release the payment plan
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationWorkflowTaskManagementUnHoldATask(drE2E["Recipient2"].ToString());
            }
            catch (Exception releasePaymentPlan)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(releasePaymentPlan, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();

            
        }

        [CodedStep("Assigned Plan to Recipient Sylvander Matti", RequiresSilverlight = true)]
        public void AssignedPlanToSylvanderMatti()
        {
            try
            {
                //call the method to Assigned Plan to Recipient JariT
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationWorkflowTaskManagementAddNewTask(drE2E["Recipient2"].ToString(), drE2E["Recipient2"].ToString(), drE2E["Recipient2"].ToString(), drE2E["Recipient2"].ToString());
            }
            catch (Exception AssignedPlanToSylvanderMatti)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(AssignedPlanToSylvanderMatti, Globals.testScriptName);
            }

            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Verify Release from Hold in History & Comments in Payment Plan Workflow Silo", RequiresSilverlight = true)]
        public void VerifyReleaseFromHoldInHistoryAndCommentsTab()
        {
            try
            {
                //Call method to verify History & Comments                
                p2pInvoiceAdministrationVerificationObj.P2PDetailPageCommentsVerification(drE2E["ComboBox"].ToString(), drE2E["Release_Comment"].ToString(), P2P_Utility.invoiceNumber, drE2E["History_And_Comments_TabItems"].ToString());
            }
            catch (Exception verifyReleaseFromHoldInHistoryAndCommentsTab)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(verifyReleaseFromHoldInHistoryAndCommentsTab, Globals.testScriptName);                
            }

            //Navigate to Transfer page
            NavigateToTransfer();
        }

        [CodedStep("Login into P2P with Matti S", RequiresSilverlight = true)]
        public void LoginIntoP2PWithMattiS()
        {
            // Create an object for "P2PLogin" class
            var objlogin = new P2PLogin(ActiveBrowser);

            try
            {
                //Call function for Login into P2P Application
                objlogin.LoginToSilverlightClient("basware\\Mattis", "");
            }
            catch (Exception loginintoP2P)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(loginintoP2P, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate To My Tasks -> Payment Plan Page", RequiresSilverlight = true)]
        public void NavigateToMyTasksPaymentPlan()
        {
            //Call the Coded Step Navigate To Personel Mode Again
            p2pNavigationObj.NavigateToPersonnelMode();
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
            //Call the coded step to navigate to my task-> payment plan 
            NavigateToMyTasks();
            NavigateToPaymentPlan();
        }

        [CodedStep("Search and Review the Payment Plan under My Tasks-> Payment Plan Page ", RequiresSilverlight = true)]
        public void SearchAndReviewThePaymentPlan()
        {
            //Call the coded strp for click on Pending Tab and Search the Plan
            ClickOnPendingTab();
            SearchPlanInPaymentPlanPage();

            //Create a object for P2PInvoiceAdministrationToolbarActions class
            var toolbarAction = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //call the common function to click on Review button
                toolbarAction.P2P_Invoice_Administration_MyTasks_ToolBarActions(drE2E["MyTasks_PaymentPlan_Reveiw_Button_AutomationID"].ToString());
            }
            catch (Exception searchAndReviewThePaymentPlan)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(searchAndReviewThePaymentPlan, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Logout Matti S From P2P", RequiresSilverlight = true)]
        public void LogoutMattiSFromP2P()
        {
            // Create an object for "P2PLogin" class
            P2PLogin objlogout = new P2PLogin(Manager.Current.ActiveBrowser);

            try
            {
                //Call method to logout into the P2P Alusta Application
                objlogout.LogoutFromSilverlightClient();
            }
            catch (Exception logoutJariTFromP2P)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(logoutJariTFromP2P, Globals.testScriptName);
            }
        }

        [CodedStep("Login into P2P with Mika Kor", RequiresSilverlight = true)]
        public void LoginIntoP2PWithMikakor()
        {
            // Create an object for "P2PLogin" class
            var objlogin = new P2PLogin(ActiveBrowser);
            try
            {
                //Call function for Login into P2P Application                
                objlogin.LoginToSilverlightClient("BASWARE\\Mikakor", "");
            }
            catch (Exception LoginIntoP2PWithMikakor)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(LoginIntoP2PWithMikakor, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate To My Tasks-> Payment Plan Page With User: Mikakor", RequiresSilverlight = true)]
        public void NavigateToMyTasksPaymentPlanWithMikakor()
        {
            //Call the coded step to Navigate To My Tasks-> Payment Plan Page
            NavigateToMyTasksPaymentPlan();
        }

        [CodedStep("Search Payment Plan in Payment Plan Silo", RequiresSilverlight = true)]
        public void SearchPlan()
        {
            //Call the coded step for click on Pending Tab and Search the Plan
            ClickOnPendingTab();
            SearchPlanInPaymentPlanPage();
        }

        [CodedStep("Forwarded to Header Approver: Lehtinen Jari", RequiresSilverlight = true)]
        public void ForwardedPlanToHeaderApprover()
        {
            try
            {
                //Use the object to access the Method for Payment Plan to Approver               
                p2pPaymentPlansMyTasksObj.P2PMoreActionsDropdown(drE2E["Forward"].ToString(), drE2E["Recipient_Pöllänen_Jari"].ToString());
            }
            catch (Exception forwardedPlanToHeaderApprover)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(forwardedPlanToHeaderApprover, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Logout Basware Mika kor From P2P", RequiresSilverlight = true)]
        public void LogoutMikakorFromP2P()
        {
            //Call the coded step for logout
            LogoutFromP2P();
        }

        [CodedStep("Login into P2P with Lehtinen Jari", RequiresSilverlight = true)]
        public void LoginIntoP2PWithJarileh()
        {
            // Create an object for "P2PLogin" class
            var objlogin = new P2PLogin(ActiveBrowser);

            try
            {
                //Call function for Login into P2P Application                
                objlogin.LoginToSilverlightClient("basware\\Jarileh", "");
            }
            catch (Exception loginIntoP2PWithJariP)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(loginIntoP2PWithJariP, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate To My Tasks-> Payment Plan Page With User: Lehtinen, Jari", RequiresSilverlight = true)]
        public void NavigateToMyTasksPaymentPlanWithJarileh()
        {
            //Call the coded step for navigation
            NavigateToMyTasksPaymentPlan();
        }

        [CodedStep("Reject the Plan", RequiresSilverlight = true)]
        public void RejectThePlan()
        {
            //Call the coded step here for search the existing plan
            SearchPlan();

            //Create a object for P2PInvoiceAdministrationToolbarActions class
            var toolbarAction = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //call the common function to click on Reject button
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PMyTaskReject(P2P_Utility.invoiceNumber, drE2E["Reject_Comment"].ToString(), drE2E["Reject_Reason_Combo_Box_Item"].ToString(), P2P_Utility.invoiceNumber);
            }
            catch (Exception rejectThePlan)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(rejectThePlan, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Logout Basware Lehtinen, Jari From P2P", RequiresSilverlight = true)]
        public void LogoutLehtinenJariFromP2P()
        {
            //Call the coded step for logout
            LogoutFromP2P();
        }

        [CodedStep("Login into P2P with Esa Tihila", RequiresSilverlight = true)]
        public void LoginIntoP2PEsaTihila()
        {
            //Call the coded step for login
            LoginIntoP2PWithEsaTihila();
        }

        [CodedStep("Navigate To Professional Mode-> Payment Plans-> Workflow Page With User: Esa Tihila", RequiresSilverlight = true)]
        public void NavigateToMyTasksPaymentPlanWithEsa()
        {
            //Call the coded step Login and Navigation
            LoginIntoP2PEsaTihila();
            NavigateToPaymentPlans();
            NavigateToPaymentPlansWorkflowPage();
        }

        [CodedStep("Open and Search the Rejected Plan ", RequiresSilverlight = true)]
        public void OpenTheRejectedPlan()
        {
            //Call the coded step for Search and Open the plan
            SearchPaymentPlan();
            OpenSelectedPaymentPlan();
        }

        [CodedStep("Assigned Plan to Recipient Lampinen, Jari", RequiresSilverlight = true)]
        public void AssignedPlanToJaril()
        {
            try
            {
                //Call the method to Assigned Plan to Recipient Lampinen, Jari
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationWorkflowTaskManagementAddNewTask(drE2E["Recipient_Lampinen_Jari"].ToString(), drE2E["Recipient_Lampinen_Jari"].ToString(), drE2E["Recipient_Lampinen_Jari"].ToString(), drE2E["Recipient_Lampinen_Jari"].ToString());
            }
            catch (Exception assignedPlanToJaril)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(assignedPlanToJaril, Globals.testScriptName);
            }
            //Call the coded step HandleBusyIndicator
            HandleBusyIndicator();
            //Navigate to Transfer
            NavigateToTransfer();
            LogoutEsaTihilaFromP2P();
        }

        [CodedStep("Logout From Esa Tihila", RequiresSilverlight = true)]
        public void LogoutEsaTihilaFromP2P()
        {
            //Call the coded step for logout
            LogoutFromP2P();
        }

        [CodedStep("Login into P2P with Lampinen,Jari", RequiresSilverlight = true)]
        public void LoginWithLampinenJari()
        {
            // Create an object for "P2PLogin" class
            var objlogin = new P2PLogin(ActiveBrowser);

            try
            {
                //Call function for Login into P2P Application
                objlogin.LoginToSilverlightClient("basware\\Jaril", "");
            }
            catch (Exception loginWithLampinenJari)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(loginWithLampinenJari, Globals.testScriptName);
            }
        }

        [CodedStep("Navigate To My Tasks-> Payment Plan Page With User: Lampinen,Jari", RequiresSilverlight = true)]
        public void NavigateToMyTasksPaymentPlanWithJaril()
        {
            //Call the coded step for Navigation
            NavigateToMyTasks();
            NavigateToPaymentPlan();
        }

        [CodedStep("Approve The Plan", RequiresSilverlight = true)]
        public void ApproveThePlan()
        {
            //Create a object for P2PInvoiceAdministrationToolbarActions class
            var toolbarAction = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);
            try
            {
                //call the common function to click on Approved button
                toolbarAction.P2P_Invoice_Administration_MyTasks_ToolBarActions(drE2E["MyTasks_PaymentPlan_Approved_Button_AutomationID"].ToString());
            }
            catch (Exception approveThePlan)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(approveThePlan, Globals.testScriptName);
            }
            //Call Coded Step HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Logout Basware Lampinen Jari From P2P", RequiresSilverlight = true)]
        public void LogoutLampinenJariFromP2P()
        {
            //Call the coded step for logout
            LogoutFromP2P();
        }

        [CodedStep("Login into P2P with Owner Esa Tihila and Navigate to Payment Plan", RequiresSilverlight = true)]
        public void LoginwithPlanOwnerEsaTihila()
        {
            //Call the coded step and navigate to payment plan
            LoginIntoP2PWithEsaTihila();
            NavigateToPaymentPlans();
        }

        [CodedStep("Navigate PaymentPlans To Approved", RequiresSilverlight = true)]
        public void NavigatePaymentPlansToApproved()
        {
            try
            {
                //Navigate to Approved Silo and Check the state is active
                p2pNavigationObj.NavigatePaymentPlansToApproved();
            }
            catch (Exception navigatePaymentPlansToApproved)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(navigatePaymentPlansToApproved, Globals.testScriptName);
            }

            //Call handled busy indicator
            HandleBusyIndicator();
        }

        [CodedStep("Search and Verify the Plan State: Active", RequiresSilverlight = true)]
        public void SearchAndVerifyThePlanState()
        {
            //Call the coded step to SearchPaymentPlan
            ClearAllSearch();
            RefreshTheGrid();
            SearchPaymentPlan();

            try
            {
                // Call the Method "P2InvoiceAdministration_Search_VerifyInvoiceStatus" for Verify the Message in a Grid
                p2pInvoiceAdministrationVerificationObj.P2InvoiceAdministration_Search_VerifyInvoiceStatus(P2P_Utility.invoiceNumber, drE2E["Status_Active"].ToString(), drE2E["Header_Name_Status_PaymentPlan"].ToString(), drE2E["Button_Text"].ToString());                
            }
            //If any Exception occurs then catch block will execute
            catch (Exception searchAndVerifyThePlanState)
            {
                //Captures Screenshot of Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(searchAndVerifyThePlanState, Globals.testScriptName);
            }
        }

        [CodedStep("Navigate to Invoice Administration->Create", RequiresSilverlight = true)]
        public void NavigateToIACreate()
        {
            try
            {
                //Call the Method to Navigate To Create Page 
                p2pNavigationObj.NavigateInvoiceAdministrationToCreate();
            }
            catch (Exception navigateToIACreate)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(navigateToIACreate, Globals.testScriptName);
            }
            //Call existing coded step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Create New Invoice with User EsaTihila", RequiresSilverlight = true)]
        public void CreateNewInvoice()
        {
            try
            {
                //Use the method to create a New Invoice                            
                p2pCreateInvoiceObj.P2PInvoiceAdministrationCreateNewInvoice(drE2E["Organization_Unit"].ToString(), drE2E["Invoice_Type"].ToString(), drE2E["Supplier_Code"].ToString(), P2P_Utility.invoiceNumber, Convert.ToDouble(drE2E["Gross_Sum"], CultureInfo.InvariantCulture.NumberFormat), Convert.ToDouble(drE2E["Tax_Sum"], CultureInfo.InvariantCulture.NumberFormat), P2P_Utility.invoiceNumber, null, null, P2P_Utility.invoiceNumber);
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
            //Call Coded Step for HandleBusyIndicator
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
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Click Send To Validation", RequiresSilverlight = true)]
        public void InvoiceSendToValidation()
        {
            try
            {
                //Use the method for Send the Invoice for Validation
                p2pCreateInvoiceObj.P2PInvoiceAdministrationSendToValidation();
            }
            catch (Exception invoiceSendToValidation)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(invoiceSendToValidation, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to IA Matching", RequiresSilverlight = true)]
        public void NavigateToIAMatching()
        {
            try
            {
                //Call the Method to NavigateInvoiceAdministrationToMatching Page 
                p2pNavigationObj.NavigateInvoiceAdministrationToMatching();
            }
            catch (Exception navigateToIAMatching)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(navigateToIAMatching, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Search And Open the Invoice under Matching Silo", RequiresSilverlight = true)]
        public void SearchAndOpenTheInvoice()
        {
            //Call the coded step Clear and Refresh the Page
            ClearAllSearch();
            RefreshTheGrid();

            try
            {
                //Call the Method to Search the invoice
                p2pInvoiceAdministrationSearchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(P2P_Utility.invoiceNumber);
            }
            catch (Exception searchAndOpenTheInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(searchAndOpenTheInvoice, Globals.testScriptName);
            }
            //Call the coded step open selected
            OpenSelectedPaymentPlan();
        }

        [CodedStep("UnMatched invoice and Update the Gross Sum under Matching Silo", RequiresSilverlight = true)]
        public void UnMatchedTheInvoiceAndUpdateTheGrossSum()
        {
            try
            {
                //Call the Method to UnMatched the invoice
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationMatchedAndUnMatchedThePlan(drE2E["PaymentPlan_UnMatch_Button"].ToString(), Convert.ToDouble(drE2E["Updated_Gross_Sum"], System.Globalization.CultureInfo.InvariantCulture.NumberFormat));
            }
            catch (Exception unMatchedTheInvoiceAndUpdateTheGrossSum)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(unMatchedTheInvoiceAndUpdateTheGrossSum, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Match it Again and Confirm the Invoice", RequiresSilverlight = true)]
        public void MatchItAgainAndConfirmTheInvoice()
        {
            try
            {
                //Call the Method to Match the invoice
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PInvoiceAdministrationMatchedAndUnMatchedThePlan(drE2E["PaymentPlan_UnMatch_Button"].ToString(), Convert.ToDouble(drE2E["Updated_Gross_Sum"], System.Globalization.CultureInfo.InvariantCulture.NumberFormat), drE2E["PaymentPlan_Match_Button"].ToString());
            }
            catch (Exception matchItAgainAndConfirmTheInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(matchItAgainAndConfirmTheInvoice, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Transfer", RequiresSilverlight = true)]
        public void NavigateToTransfer()
        {
            try
            {
                //Call the Method to NavigateInvoiceAdministrationToTransfer Page 
                p2pNavigationObj.NavigateInvoiceAdministrationToTransfer();
            }
            catch (Exception navigateToTransfer)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(navigateToTransfer, Globals.testScriptName);
            }
            //Call Coded Step for HandleBusyIndicator
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
            //Call Coded Step for HandleBusyIndicator
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
            //Call Coded Step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Use Free Text to Search Invoice", RequiresSilverlight = true)]
        public void SearchInvoiceUsingFreeTextSearch()
        {
            try
            {
                //Use the Object to Access the Method "P2PInvoiceAdministrationFreeTextSearchToSearchInvoice"
                p2pInvoiceAdministrationSearchObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(P2P_Utility.invoiceNumber);
            }
            catch (Exception searchInvoiceUsingFreeTextSearch)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                p2pExceptionHandlerObj.CapturedImage(searchInvoiceUsingFreeTextSearch, Globals.testScriptName);
            }

            //Handle Busy Indicator
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