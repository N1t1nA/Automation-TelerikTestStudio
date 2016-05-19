using System;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Design.Execution;
using System.Data;
using P2P.Testing.Shared.Class;
using P2P.Testing.Shared.Class.WebShop;
using P2P.Testing.Shared.Class.Purchase;
using P2P.Testing.Shared.Class.Purchase.GoodsReceipts;
using P2P.Testing.Shared.Class.PaymentPlans.Search;
using P2P.Testing.Shared.Class.InvoiceAdministration.Search;
using P2P.Testing.Shared.Class.InvoiceAdministration;
using P2P.Testing.Shared.Class.InvoiceAdministration.InvoiceDetails;
using P2P.Testing.WebShop;
using P2P.Testing.Shared.Class.Purchase.ToolbarActions;
using E2E.Class;
using P2P.Testing.Shared;
using P2P.Testing.ConfigTool.Class;
using System.Globalization;
using P2P.Testing.Shared.Class.InvoiceAdministration.Create;


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

    public class E2E_Purchase_PR_IA_MM_Transfer : BaseWebAiiTest
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

        //Declaring Array Variables
        string[] prPendingStatus;
        string[] prInfoStatus;
        string[] verifyHistory;
        string uploadPath;

        //Create an object of EdXmlReader class   
        EdXmlReader objReadXml = new EdXmlReader();

        //Create an object for 'P2PWebShop' class
        P2PWebShop p2pWebShopObj = new P2PWebShop();

        //Create an object for 'P2PNavigation' class
        P2PNavigation p2pNavigationObj = new P2PNavigation();

        //Create an object for 'P2PCreateInvoice' class
        P2PCreateInvoice p2pCreateInvoiceObj = new P2PCreateInvoice();

        //Create an object for P2PPurchaseVerification class
        P2PPurchaseVerification verifyPRObj = new P2PPurchaseVerification();

        //Create an object for 'P2PGoodsReceiptsDetails' class
        P2PGoodsReceiptsDetails goodsReceiptsObj = new P2PGoodsReceiptsDetails();

        //Create object for P2PWebShopPurchases class
      //  P2PWebShopMyPurchases p2pWebShopPurchasesObj = new P2PWebShopMyPurchases();

        //Create object for P2PPaymentPlansSearch class
        P2PPaymentPlansSearch p2pPaymentPlansSearchObj = new P2PPaymentPlansSearch();

        //Create object for P2PGoodsReceiptsSearch class
        P2PGoodsReceiptsSearch p2pGoodsReceiptsSearchObj = new P2PGoodsReceiptsSearch();

       //Create an object for P2PWebShopProductSearch class
       // P2PWebShopProductsSearch searchWebshopProdutsObj = new P2PWebShopProductsSearch();

        //Create an object for P2PInvoiceAdministrationSearch class
        P2PInvoiceAdministrationSearch searchPRObj = new P2PInvoiceAdministrationSearch();

        //Create an object for P2PWebShopVerifications class
        P2PWebShopVerifications p2pWebShopVerificaitonObj = new P2PWebShopVerifications();

        //Create an object for P2PWebShopShoppingBasket class
       // P2PWebShopShoppingBasket webshopShoppingBasketObj = new P2PWebShopShoppingBasket();        

        //Create an object for 'P2PMatchingInvoiceDetails' class
       // P2PMatchingInvoiceDetails p2pMatchingInvoiceDetailsObj = new P2PMatchingInvoiceDetails();

        //Create an object for P2PInvoiceAdministrationImageActions class
        P2PInvoiceAdministrationImageActions addImageObj = new P2PInvoiceAdministrationImageActions();

        //Create an object for P2PInvoiceAdministrationVerification class
        P2PInvoiceAdministrationVerification p2pInvoiceAdministrationVerificationObj = new P2PInvoiceAdministrationVerification();

        //Create object for P2PGoodsReceiptsVerification class
        P2PGoodsReceiptsVerification p2pGoodsReceiptsVerificationObj = new P2PGoodsReceiptsVerification();

        //Create an object for P2PGoodsReceiptsToolbarActions class
        P2PGoodsReceiptsToolbarActions p2pGoodsReceiptsToolbarActionsObj = new P2PGoodsReceiptsToolbarActions();

        //Create an object for P2PPurchaseLineRquisitionToolBarActions class
        P2PPurchaseLineRquisitionToolBarActions toolBarActionsObj = new P2PPurchaseLineRquisitionToolBarActions();

        P2PInvoiceAdministrationInvoiceDetails p2pInvoiceAdministrationInvoiceDetailsObj = new P2PInvoiceAdministrationInvoiceDetails();

        //Create an object for P2PExceptionHandler class
        P2PExceptionHandler exceptionHandler = new P2PExceptionHandler();
        
        //Constructor
        public E2E_Purchase_PR_IA_MM_Transfer()
        {
            //Initializing value to declared vaiable 'uploadPath'
            uploadPath = objReadXml.Upload_file("Sample.tiff");

            //Read the Data Row from XML 
            drE2E = objReadXml.Read_xml_file("E2E_TestData.xml", "E2E_Purchase_PR_IA_MM_Transfer", "Functionality", "E2EPartiallyReceived");

            //Saving Data in Array
            prPendingStatus = new string[] { drE2E["Purchase_Requisition_Ordered_Status"].ToString() };
            prInfoStatus = new string[] { drE2E["Purchase_Requisition_Received_Status"].ToString() };
            verifyHistory = new string[] { drE2E["Verify_History_1"].ToString(), drE2E["Verify_History_2"].ToString(), drE2E["Verify_History_3"].ToString(), drE2E["Verify_History_4"].ToString(), drE2E["Invoice_Transfer_Status"].ToString() };

            //Calling Generate Function
            P2P_Utility.GenerateNumber();
        }

        [CodedStep("Login into P2P")]
        public void LoginintoP2P()
        {
            // Create an object for "P2PLogin" class
            var objlogin = new P2PLogin(ActiveBrowser);

            try
            {
                //Call  method "LoginToSilverlightClient" from the "P2PLogin" class
                objlogin.LoginToSilverlightClient("basware\\veijol", "");
            }

            catch (Exception loginintoP2P)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(loginintoP2P, Globals.testScriptName);
            }
        }

        [CodedStep("Handle the Busy Indicator", RequiresSilverlight = true)]
        public void HandleBusyIndicator()
        {
            try
            {
                //Handle Busy Indicator
                P2PNavigation.CallBusyIndicator();
            }
            catch (Exception handleBusyIndicator)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(handleBusyIndicator, Globals.testScriptName);
            }
        }

        [CodedStep("Navigate To Configuration Tool Page", RequiresSilverlight = true)]
        public void NavigateToConfigurationTool()
        {
            try
            {
                //Use the method to Navigate to ConfigTool
                p2pNavigationObj.NavigateToConfigurationMode("Configuration");
            }
            catch (Exception navigateToConfigurationTool)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToConfigurationTool, Globals.testScriptName);
            }
            //Call the existing coded step here for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Screen", RequiresSilverlight = true)]
        public void NavigateToScreen()
        {
            //Create an object for P2PConfigToolConfigurationArea
            var screen = new P2PConfigToolConfigurationArea(ActiveBrowser);

            try
            {
                //Use the method to select the Configuration Area
                screen.P2P_ConfigurationTool_ConfigurationArea(drE2E["Screen"].ToString(), drE2E["Screen_1"].ToString());
            }
            catch (Exception navigateToScreen)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToScreen, Globals.testScriptName);
            }

            //Call the existing coded step here for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Delete Association Mapping from OM Configuration", RequiresSilverlight = true)]
        public void DeleteAssociationMapping()
        {
            //Create an object for P2PConfigToolConfigurationArea
            P2PConfigToolConfigurationArea deleteRule = new P2PConfigToolConfigurationArea(ActiveBrowser);

            try
            {
                //Use the Method to Delete Association Mapping
                deleteRule.P2PConfigurationTool_DeleteAssociationMapping(drE2E["OM_Configuration"].ToString(), drE2E["Invoice_Line_Field"].ToString());
            }

            catch (Exception deleteAssociationMapping)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(deleteAssociationMapping, Globals.testScriptName);
            }

            //Call the existing coded step here for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Personal Mode", RequiresSilverlight = true)]
        public void NavigateToPersonalMode()
        {
            try
            {
                //Call the Method to Navigate To PersonelMode Page 
                p2pNavigationObj.NavigateToPersonnelMode("Personnel");
            }
            catch (Exception navigateToPersonalMode)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToPersonalMode, Globals.testScriptName);
            }

            //Call the Coded Step here for Handle Busy Indicator 
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Webshop", RequiresSilverlight = true)]
        public void NavigateToWebShop()
        {
            try
            {
                //Call the Method to Navigate To WebShop Page 
                p2pNavigationObj.NavigateToWebShop();
            }
            catch (Exception navigateToWebShop)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToWebShop, Globals.testScriptName);
            }

            //Call the Coded Step here for  Handle Busy Indicator 
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to WebShop Product Search Page", RequiresSilverlight = true)]
        public void NavigateToProductSearchPage()
        {
            try
            {
                //Call the Method to Navigate To Products Search Page 
                p2pNavigationObj.NavigateToWebShopProductSearchPage();
            }
            catch (Exception navigateToProductSearchPage)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToProductSearchPage, Globals.testScriptName);
            }

            //Call the Coded Step here for Handle Busy Indicator 
            HandleBusyIndicator();
        }

        [CodedStep("Search the Products on WebShop Products Search Page", RequiresSilverlight = true)]
        public void SearchTheProductOnProductsSearchPage()
        {
            try
            {
                //Call the Method P2PWebShopProductSearch 
                p2pWebShopObj.WebShopProductsSearch(drE2E["Product_Name"].ToString());
            }
            catch (Exception searchTheProductOnProductsSearchPage)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchTheProductOnProductsSearchPage, Globals.testScriptName);
            }

            //Call the Coded Step here for Handle Busy Indicator 
            HandleBusyIndicator();
        }

        [CodedStep("Verify the Searched Product On Page", RequiresSilverlight = true)]
        public void VerifySearchedProduct()
        {
            try
            {
                //Call the Method to verify the basket
                p2pWebShopVerificaitonObj.P2PWebShop_VerifyProductsSearch(drE2E["Product_Name"].ToString());
            }
            catch (Exception verifySearchedProduct)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(verifySearchedProduct, Globals.testScriptName);
            }
        }

        [CodedStep("Add Products To Basket", RequiresSilverlight = true)]
        public void AddProductsToBasket()
        {
            try
            {
                //Call the Method to Add Products in a Shopping Basket
                p2pWebShopObj.WebShopProductsAddToShoppingBasket(drE2E["Product_Name"].ToString());
            }
            catch (Exception addProductsToBasket)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(addProductsToBasket, Globals.testScriptName);
            }

            //Call the Coded Step here for Handle Busy Indicator 
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Product Basket Page", RequiresSilverlight = true)]
        public void NavigateToProductBasketPage()
        {
            try
            {
                //Call the Method to Navigate To Shopping Basket
                p2pNavigationObj.NavigateToWebShopShoppingBasket();
            }
            catch (Exception navigateToProductBasketPage)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToProductBasketPage, Globals.testScriptName);
            }

            //Call the Coded Step here for Handle Busy Indicator 
            HandleBusyIndicator();
        }

        [CodedStep("Verify the Products Exist On Product Basket Page", RequiresSilverlight = true)]
        public void VerifyAddProductsToProductsBasketPage()
        {
            try
            {
                //Call the Method to verify the basket
                p2pWebShopVerificaitonObj.P2PWebShop_VerifyProductsAddToBasket(drE2E["Product_Name"].ToString());
            }
            catch (Exception verifyAddProductsToProductsBasketPage)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(verifyAddProductsToProductsBasketPage, Globals.testScriptName);
            }
        }

        [CodedStep("Click On Edit Purchase requisition Details", RequiresSilverlight = true)]
        public void ClickOnEditPurchaserequisitionDetails()
        {
            try
            {
                //Call the Method to click on Edit Purchase requisition Details
                p2pWebShopObj.WebShopProductsEditPurchaseRequsitionDetails();
            }
            catch (Exception clickOnEditPurchaserequisitionDetails)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(clickOnEditPurchaserequisitionDetails, Globals.testScriptName);
            }

            //Call the Coded Step here for Handle Busy Indicator 
            HandleBusyIndicator();
        }

        [CodedStep("Navigate To Lines Tab", RequiresSilverlight = true)]
        public void NavigateToLinesTab()
        {
            try
            {
                //Call the Method to p2pNavigationObj
                p2pNavigationObj.NavigateToPRLinePage();
            }

            catch (Exception navigateToLinesTab)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToLinesTab, Globals.testScriptName);
            }

            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Verify Purchase Requisition", RequiresSilverlight = true)]
        public void VerifyPurchaseRequisition()
        {
            try
            {
                //Call the Method to verify the Status
                verifyPRObj.P2PMyTasks_VerifyPurchaseRequisitionLineStatus(drE2E["Array_Index_1"].ToString(), drE2E["Purchase_Requisition"].ToString());
            }
            catch (Exception verifyPurchaseRequisitionStatus)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(verifyPurchaseRequisitionStatus, Globals.testScriptName);
            }
        }

        [CodedStep("Enter Purpose and Date", RequiresSilverlight = true)]
        public void InputPurposeAndDate()
        {
            try
            {
                //Call the Method to Enter Values in fields
                p2pWebShopObj.WebShopPurchaseRequisitionInputDateAndPurposeData(P2P_Utility.purposeNumber);
            }
            catch (Exception inputPurposeAndDate)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(inputPurposeAndDate, Globals.testScriptName);
            }
        }

        [CodedStep("Fetching the Gross Total Number and Saving in Global Variable", RequiresSilverlight = true)]
        public void FetchingTheGrossTotalNumberAndSavingInGlobalVariable()
        {
            try
            {
                //Call the Method to p2pNavigationObj
                p2pWebShopObj.P2PFetchPurchaseOrderNumber(drE2E["Gross_Total"].ToString());
            }

            catch (Exception fetchingTheGrossTotalNumberAndSavingInGlobalVariable)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(fetchingTheGrossTotalNumberAndSavingInGlobalVariable, Globals.testScriptName);
            }
        }

        [CodedStep("Enter Account & Cost Center Code", RequiresSilverlight = true)]
        public void InputAccountAndCostCenterCode()
        {
            try
            {
                //Call the Method to Enter Values in fields
                p2pWebShopObj.WebShopPurchaseRequisitionInputAccountAndCostCenterCodeData(drE2E["Account_Code"].ToString(), drE2E["Cost_Center_Code"].ToString(), drE2E["Select_Tab"].ToString());
            }
            catch (Exception inputAccountAndCostCenterCode)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(inputAccountAndCostCenterCode, Globals.testScriptName);
            }
        }

        [CodedStep("Fetching the Supplier Name and Saving in Global Variable", RequiresSilverlight = true)]
        public void FetchingTheSupplierAndSavingInGlobalVariable()
        {
            try
            {
                //Call the Method to p2pNavigationObj
                p2pWebShopObj.P2PFetchPurchaseOrderNumber(drE2E["Supplier_Code"].ToString());
            }

            catch (Exception fetchingTheSupplierAndSavingInGlobalVariable)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(fetchingTheSupplierAndSavingInGlobalVariable, Globals.testScriptName);
            }
        }

        [CodedStep("Send PR to Process", RequiresSilverlight = true)]
        public void SendPRToProcess()
        {
            try
            {
                //Call the Method to click on Send To Process Button
                p2pWebShopObj.WebShopPurchaseRequisitionSendToProcess();
            }
            catch (Exception sendPRToProcess)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(sendPRToProcess, Globals.testScriptName);
            }

            //Call the Coded Step here for Handle Busy Indicator 
            HandleBusyIndicator();
        }

        [CodedStep("Logout Basware Veijol From P2P", RequiresSilverlight = true)]
        public void LogoutFromP2P()
        {
            // Create an object for "P2PLogin" class
            P2PLogin objlogout = new P2PLogin(Manager.Current.ActiveBrowser);

            try
            {
                //Call method "LogoutToSilverlightClient" from the "P2PLogin" class
                objlogout.LogoutFromSilverlightClient();
            }
            catch (Exception LogoutFromP2P)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(LogoutFromP2P, Globals.testScriptName);
            }
        }

        /**************************************************Purchase Requisition Send for Approval**************************************************/


        /**************************************************First Approver Logs into the Application*************************************************/

        [CodedStep("Login into P2P with First Approver")]
        public void FirstApproverLoginintoP2P()
        {
            //Create an object for P2PLogin class  
            P2PLogin objlogin = new P2PLogin(ActiveBrowser);

            try
            {
                //Call the Method to Login into Application
                objlogin.LoginToSilverlightClient("basware\\jaripul", "");
            }
            catch (Exception firstApproverLoginintoP2P)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(firstApproverLoginintoP2P, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Personnel Mode", RequiresSilverlight = true)]
        public void FirstApproverNavigateToPersonalMode()
        {
            //Call the Coded Step Navigate To Personel Mode Again
            NavigateToPersonalMode();
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
                exceptionHandler.CapturedImage(navigateToMyTasks, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to My Tasks Purchase Requisition Page", RequiresSilverlight = true)]
        public void NavigateToMyTasksPurchaseRequisitionPage()
        {
            try
            {
                //Call the Method to Navigate To MyTasks Page
                p2pNavigationObj.NavigateToMyTaskPurchaseRequisitionPage();
            }
            catch (Exception navigateToMyTasksPurchaseRequisitionPage)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToMyTasksPurchaseRequisitionPage, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Search Purchase Requisition", RequiresSilverlight = true)]
        public void SearchPurchaseRequisition()
        {
            try
            {
                //Call the Method to Search Purchase Requisition
                searchPRObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(P2P_Utility.purposeNumber);
            }
            catch (Exception searchPurchaseRequisition)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchPurchaseRequisition, Globals.testScriptName);
            }
            //Call the Coded Step here for  Handle Busy Indicator 
            HandleBusyIndicator();
        }

        [CodedStep("Select Purchase Requisition", RequiresSilverlight = true)]
        public void SelectPurchaseRequisition()
        {
            try
            {
                //Call the Method to Select Purchase Requisition
                p2pPaymentPlansSearchObj.P2PPaymentPlanGridClickInTheCell(P2P_Utility.purposeNumber);
            }
            catch (Exception selectPurchaseRequisition)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(selectPurchaseRequisition, Globals.testScriptName);
            }
            //Call the Coded Step here for  Handle Busy Indicator 
            HandleBusyIndicator();
        }

        [CodedStep("Approve Purchase Requisition", RequiresSilverlight = true)]
        public void ApprovePurchaseRequisition()
        {
            try
            {
                //Call the Method to Approve Purchase Requisition
                toolBarActionsObj.P2PMyTasksPurchaseRequisitionApproveAction();
            }
            catch (Exception approvePurchaseRequisition)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(approvePurchaseRequisition, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Logout from P2P Application: First Approver", RequiresSilverlight = true)]
        public void LogoutFirstApprover()
        {
            //Call the Coded Step Logout Again
            LogoutFromP2P();
        }

        /**************************************************First Approver Logs Out the Application**************************************************/


        /**************************************************Second Approver Logs into the Application************************************************/

        [CodedStep("Login into P2P with Final Approver")]
        public void FinalApproverLoginintoP2P()
        {
            //Create an object for P2PLogin class  
            P2PLogin objlogin = new P2PLogin(ActiveBrowser);

            try
            {
                //Call the Method to Login into Application
                objlogin.LoginToSilverlightClient("basware\\jaria", "");
            }
            catch (Exception finalApproverLoginintoP2P)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(finalApproverLoginintoP2P, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Personnel Mode", RequiresSilverlight = true)]
        public void FinalApproverNavigateToPersonalMode()
        {
            //Call the Coded Step Navigate To Personel Mode Again
            NavigateToPersonalMode();
        }

        [CodedStep("Navigate to MyTask Page", RequiresSilverlight = true)]
        public void FinalApproverNavigateToMyTasks()
        {
            //coded step call
            NavigateToMyTasks();
        }

        [CodedStep("Navigate to My Tasks Purchase Requisition Page", RequiresSilverlight = true)]
        public void FinalApproverNavigateToMyTasksPurchaseRequisitionPage()
        {
            //Call the Coded Step 
            NavigateToMyTasksPurchaseRequisitionPage();
        }

        [CodedStep("Search & Select Purchase Requisition", RequiresSilverlight = true)]
        public void FinalApproverSearchAndSelectPurchaseRequisition()
        {
            //Call the Coded Step 
            SearchPurchaseRequisition();

            //Call the Coded Step
            SelectPurchaseRequisition();
        }

        [CodedStep("Final Approve Purchase Requisition", RequiresSilverlight = true)]
        public void FinalApprovePurchaseRequisition()
        {
            //Call the Coded Step to Approve requisition
            ApprovePurchaseRequisition();
        }

        [CodedStep("Logout from P2P Application: Final Approver", RequiresSilverlight = true)]
        public void LogoutFinalApprover()
        {
            //Call the Coded Step Logout Again
            LogoutFromP2P();
        }

        /**************************************************Second Approver Logs Out the Application**************************************************/


        /**************************************************Owner Logs into the Application**********************************************************/

        [CodedStep("Login into P2P with PR Owner")]
        public void LoginintoP2PWithOwner()
        {
            //Coded step call
            LoginintoP2P();
            HandleBusyIndicator();
        }

        [CodedStep("Onwer: Navigate to Personnel Mode", RequiresSilverlight = true)]
        public void AgainNavigateToPersonalMode()
        {
            //Call the Coded Step Navigate To Personel Mode Again
            NavigateToPersonalMode();
        }

        [CodedStep("Onwer: Navigate to Webshop Page", RequiresSilverlight = true)]
        public void NavigateToWebshop()
        {
            try
            {
                //Call the Method to Navigate To Webshop Page
                p2pNavigationObj.NavigateToWebShop();
            }
            catch (Exception navigateToWebshop)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToWebshop, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Owner: Navigate to Webshop My Purchases Page", RequiresSilverlight = true)]
        public void NavigateToWebshopMyPurchasesPage()
        {
            try
            {
                //Call the Method to Navigate To Webshop My Purchases Page
                p2pNavigationObj.NavigateToWebShopPurchaseRequisitionPage();
            }
            catch (Exception navigateToWebshopMyPurchasesPage)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToWebshopMyPurchasesPage, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Owner: Click on Pending Tab", RequiresSilverlight = true)]
        public void ClickPendingTab()
        {
            try
            {
                //Call method to click on Pending Tab 
                p2pNavigationObj.NavigateToWebShopMyPRPendingFilter();
            }
            catch (Exception clickPendingTab)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(clickPendingTab, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Onwer: Search Purchase Requisition", RequiresSilverlight = true)]
        public void AgainSearchPurchaseRequisition()
        {
            //Call the Coded Step To Search Purchase Requisition
            SearchPurchaseRequisition();
        }

        [CodedStep("Verify Purchase Requisition Status: Ordered", RequiresSilverlight = true)]
        public void VerifyPurchaseRequisitionOrderedStatus()
        {
            try
            {
                //Call the Method to Verify Status
                p2pWebShopVerificaitonObj.P2PWebShop_VerifyPurchaseRequisitionStatus(prPendingStatus, Convert.ToInt32(drE2E["Array_Index_1"]));
            }

            catch (Exception verifyPurchaseRequisitionOrderedStatus)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(verifyPurchaseRequisitionOrderedStatus, Globals.testScriptName);
            }
        }

        [CodedStep("Navigate To Goods Receipts", RequiresSilverlight = true)]
        public void NavigateToGoodsReceipts()
        {
            //Calling Coded Step Again
            NavigateToMyTasks();

            try
            {
                //Call the Method to Navigate To Goods Receipts
                p2pNavigationObj.NavigateToMyTasksGoodReceiptPage();
            }
            catch (Exception NavigateToGoodsReceipts)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(NavigateToGoodsReceipts, Globals.testScriptName);
            }

            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Click On Pending Tab", RequiresSilverlight = true)]
        public void ClickOnPendingTab()
        {
            try
            {
                //Call method to Click on Pending
                p2pGoodsReceiptsSearchObj.P2PMyTasksGRClickOnTab(drE2E["Pending_Tab"].ToString());
            }
            catch (Exception clickOnPendingTab)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(clickOnPendingTab, Globals.testScriptName);
            }

            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Search for a Goods Receipt", RequiresSilverlight = true)]
        public void SearchGoodsReceipts()
        {
            try
            {
                //Call method to search for a GR 
                searchPRObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(P2P_Utility.purposeNumber);
            }
            catch (Exception SearchGoodsReceipts)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(SearchGoodsReceipts, Globals.testScriptName);
            }

            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Select a Goods Receipts", RequiresSilverlight = true)]
        public void SelectGoodsReceipts()
        {
            try
            {
                //Call method to open selected GR
                goodsReceiptsObj.P2pSearchAndSelectAGoodReceipt(P2P_Utility.purposeNumber, drE2E["Product_Name"].ToString());
            }
            catch (Exception SelectGoodsReceipts)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(SelectGoodsReceipts, Globals.testScriptName);
            }
        }

        [CodedStep("Partialy Receive GR", RequiresSilverlight = true)]
        public void PartialyReceiveGoodsReceipts()
        {
            try
            {
                //Call the Method to Fully Receive GR
                goodsReceiptsObj.P2PGoodsReceiptsReceived(null, drE2E["Product_Name"].ToString(), drE2E["Product_Name"].ToString());
            }
            catch (Exception partialyReceiveGoodsReceipts)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(partialyReceiveGoodsReceipts, Globals.testScriptName);
            }
            //Call the Coded Step here for Handle Busy Indicator 
            HandleBusyIndicator();

            //Click on Pending Tab
            ClickOnPendingTab();
        }

        [CodedStep("Verify Good Receipt on My Task Goods Receipt Page: Pending", RequiresSilverlight = true)]
        public void VerifyGoodsReceiptsInPendingStatus()
        {
            try
            {
                //Call the Method to Verify Status
                p2pGoodsReceiptsVerificationObj.FullyReceivedPurchaserequisitionVerification(drE2E["Product_Name"].ToString());
            }

            catch (Exception verifyGoodsReceiptsInPendingStatus)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(verifyGoodsReceiptsInPendingStatus, Globals.testScriptName);
            }
        }

        [CodedStep("Navigate to Goods Receipts Page and Click On Partially Received Tab", RequiresSilverlight = true)]
        public void NavigateToGoodsReceiptsPageAndClickOnPartiallyReceivedTab()
        {
            try
            {
                //Call method to Click on Pending
                p2pGoodsReceiptsSearchObj.P2PMyTasksGRClickOnTab(drE2E["Partly_Received"].ToString());
            }
            catch (Exception navigateToGoodsReceiptsPageAndClickOnPartiallyReceivedTab)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToGoodsReceiptsPageAndClickOnPartiallyReceivedTab, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Search Goods Receipts on Partially Received Tab", RequiresSilverlight = true)]
        public void SearchGoodsReceiptsOnPartiallyReceivedTab()
        {
            //Search Goods Receipt
            SearchGoodsReceipts();
        }

        [CodedStep("Verify Good Receipt on Partialy Received Page", RequiresSilverlight = true)]
        public void VerifyGoodsReceiptsOnPartialyReceivedPage()
        {
            //Verification of Goods Receipts
            VerifyGoodsReceiptsInPendingStatus();

            try
            {
                //Call the Method to Verify Status
                p2pGoodsReceiptsVerificationObj.P2PGoodsReceiptsVerifyFreeTextSearch(drE2E["Header_Name_GR"].ToString(), Convert.ToInt32(drE2E["Iteration_GR"]), drE2E["GR_Status"].ToString());
            }

            catch (Exception verifyGoodsReceiptsOnPartialyReceivedPage)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(verifyGoodsReceiptsOnPartialyReceivedPage, Globals.testScriptName);
            }
        }

        [CodedStep("Select and Click Receive Button", RequiresSilverlight = true)]
        public void SelectAndClickReceiveButton()
        {
            //Select Goods Receipt
            SelectGoodsReceipts();

            try
            {
                //Call the Method to Fully Receive GR
                goodsReceiptsObj.P2PGoodsReceiptsFullyReceived(drE2E["Product_Name"].ToString());
            }
            catch (Exception selectAndClickReceiveButton)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(selectAndClickReceiveButton, Globals.testScriptName);
            }
        }

        [CodedStep("Verify Received Quantity of Goods Receipts in Receive Dialog", RequiresSilverlight = true)]
        public void VerifyReceivedQuanityOfGoodsReceipts()
        {
            try
            {
                //Call the Method to Verify Received Quantity
                p2pGoodsReceiptsVerificationObj.P2PVerifyReceivedQuantity();
            }

            catch (Exception verifyReceivedQuanityOfGoodsReceipts)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(verifyReceivedQuanityOfGoodsReceipts, Globals.testScriptName);
            }
        }
                
        [CodedStep("Fully Receive GR", RequiresSilverlight = true)]
        public void FullyReceiveGoodsReceipts()
        {           
            try
            {
                //Call the Method to Fully Receive GR
                goodsReceiptsObj.P2PGoodsReceiptsFullyReceived(null, drE2E["Product_Name"].ToString());
            }
            catch (Exception fullyReceiveGoodsReceipts)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(fullyReceiveGoodsReceipts, Globals.testScriptName);
            }
            //Call the Coded Step here for Handle Busy Indicator 
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Info filter Page", RequiresSilverlight = true)]
        public void NavigateToInfoFilterPage()
        {
            //Call existing Coded Step here: NavigateToWebShop
            NavigateToWebShop();

            //Call existing Coded Step here: NavigateToWebShopPurchaseRequisition
            NavigateToWebshopMyPurchasesPage();

            try
            {
                //Call the Method to Navigate to Info Filter Page
                p2pNavigationObj.NavigateToWebShopMyPRInfoFilter();
            }

            catch (Exception navigateToInfoFilterPage)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToInfoFilterPage, Globals.testScriptName);
            }

            //Call the Coded Step here for Handle Busy Indicator 
            HandleBusyIndicator();
        }

        [CodedStep("Verify Purchase Requisition Status: Received", RequiresSilverlight = true)]
        public void VerifyPurchaseRequisitionStatusReceived()
        {
            //Call existing Coded Step here: SearchPurchaseRequisition
            SearchPurchaseRequisition();

            try
            {
                //Call the Method to Verify Status
                p2pWebShopVerificaitonObj.P2PWebShop_VerifyPurchaseRequisitionStatus(prInfoStatus, Convert.ToInt32(drE2E["Array_Index_1"]));
            }

            catch (Exception verifyPurchaseRequisitionInfoStatus)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(verifyPurchaseRequisitionInfoStatus, Globals.testScriptName);
            }
        }

        [CodedStep("Select Purchase Requisition from Info Page", RequiresSilverlight = true)]
        public void SelectInfoPurchaseRequisition()
        {
            try
            {
                //Call the method to select PR
                p2pWebShopObj.P2PWebShopSelectPurchaseRequisition(P2P_Utility.purposeNumber);
            }
            catch (Exception selectInfoPurchaseRequisition)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(selectInfoPurchaseRequisition, Globals.testScriptName);
            }
        }

        [CodedStep("Open Selected Purchase Requisition", RequiresSilverlight = true)]
        public void OpenSelectedPurchaseRequisition()
        {
            try
            {
                //Call the Method to Open Selected Purchase Requisition
                p2pWebShopObj.P2POpenSelectedPurchaseRequisition();
            }

            catch (Exception OpenSelectedPurchaseRequisition)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(OpenSelectedPurchaseRequisition, Globals.testScriptName);
            }
            //Call the Coded Step here for  Handle Busy Indicator 
            HandleBusyIndicator();
        }

        [CodedStep("Navigate To Related Documents Tab", RequiresSilverlight = true)]
        public void NavigateToRelatedDocumentTab()
        {
            try
            {
                //Call the Method to p2pNavigationObj
                p2pNavigationObj.NavigateToPRRelatedDocumentPage();
            }

            catch (Exception NavigateToRelatedDocumentTab)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(NavigateToRelatedDocumentTab, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Fetching the Purchase Order Number and Saving in Global Variable", RequiresSilverlight = true)]
        public void FetchingThePurchaseOrderNumberAndSavingInGlobalVariable()
        {
            try
            {
                //Call the Method to p2pNavigationObj
                p2pWebShopObj.P2PFetchPurchaseOrderNumber(drE2E["Purchase_Order"].ToString());
            }

            catch (Exception fetchingThePurchaseOrderNumberAndSavingInGlobalVariable)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(fetchingThePurchaseOrderNumberAndSavingInGlobalVariable, Globals.testScriptName);
            }
        }

        [CodedStep("Verify Goods Receipt in Related Documents", RequiresSilverlight = true)]
        public void VerifyGoodsReceiptInRelatedDocument()
        {
            try
            {
                //Call the Method to Verify GoodsReceipt
                p2pGoodsReceiptsVerificationObj.P2PWebShopMyTasks_VerifyPODocument(drE2E["Document_Type"].ToString(), drE2E["Document_Type_Number"].ToString());
            }

            catch (Exception verifyGoodsReceiptInRelatedDocument)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(verifyGoodsReceiptInRelatedDocument, Globals.testScriptName);
            }
        }

        [CodedStep("Logout from P2P Application", RequiresSilverlight = true)]
        public void LogoutFromP2PApplication()
        {
            //Call the Coded Step Logout Again
            LogoutFromP2P();
        }

        /**************************************************Owner Logs Logs Out the Application**************************************************/


        /**************************************************Basware\ilkka logs into the Application**********************************************/

        [CodedStep("Login into P2P")]
        public void LogInP2PApplication()
        {
            //Create object for P2PLogin class
            var objlogin = new P2PLogin(ActiveBrowser);

            try
            {
                //Call the Method to Login into Application
                objlogin.LoginToSilverlightClient("basware\\ilkka", "");
            }
            catch (Exception logInP2PApplication)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(logInP2PApplication, Globals.testScriptName);
            }

            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
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
                exceptionHandler.CapturedImage(navigateToCreate, Globals.testScriptName);
            }

            //Call existing coded step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Create New Invoice", RequiresSilverlight = true)]
        public void CreateNewInvoice()
        {
            try
            {
                //Use the method to create a New Invoice            
                p2pCreateInvoiceObj.P2PInvoiceAdministrationCreateNewInvoice(drE2E["Organization_Unit"].ToString(), drE2E["Invoice_Type"].ToString(),
                P2P_Utility.supplierName, P2P_Utility.invoiceNumber, Convert.ToDouble(P2P_Utility.grossTotal, CultureInfo.InvariantCulture.NumberFormat),
                Convert.ToDouble(drE2E["Tax_Sum"], CultureInfo.InvariantCulture.NumberFormat));

            }
            catch (Exception createNewInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(createNewInvoice, Globals.testScriptName);
            }
        }

        [CodedStep("Enter Tax Code", RequiresSilverlight = true)]
        public void EnterTaxCode()
        {
            try
            {
                //Use the object to Access the Method to Add PO Number
                p2pCreateInvoiceObj.P2PInvoiceAdministrationInputTaxCode(drE2E["Tax_Code"].ToString());
            }
            catch (Exception enterTaxCode)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(enterTaxCode, Globals.testScriptName);
            }
        }

        [CodedStep("Add Purchase Order Number", RequiresSilverlight = true)]
        public void AddPurchaseOrderNumber()
        {
            try
            {
                //Use the object to Access the Method to Add PO Number
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PMatchingUpdatePONumber(P2P_Utility.invoiceNumber);
            }
            catch (Exception addPurchaseOrderNumber)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(addPurchaseOrderNumber, Globals.testScriptName);
            }
        }

        [CodedStep("Add Image with Invoice", RequiresSilverlight = true)]
        public void AddImage()
        {
            try
            {
                //Use the object to Access the Method to Save the Invoice Image
                addImageObj.P2PInvoiceAdministrationAddNewImage(uploadPath, true);
            }
            catch (Exception addedImage)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(addedImage, Globals.testScriptName);
            }
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
                exceptionHandler.CapturedImage(verifyAddImageDialog, Globals.testScriptName);
            }
            //Call existing coded step for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Verify Image Added To Invoice", RequiresSilverlight = true)]
        public void VerifyImageAdded()
        {
            try
            {
                //Use the object to Verify Image Added To Invoice
                // Use the method for verify the Image added successfully
                p2pInvoiceAdministrationVerificationObj.P2PDetailPageCommentsVerification(drE2E["ComboBox"].ToString(), drE2E["Image_Comment"].ToString(),
                P2P_Utility.invoiceNumber, drE2E["Module_Name"].ToString());

            }
            catch (Exception verifyImageAdded)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(verifyImageAdded, Globals.testScriptName);
            }
        }

        [CodedStep("Click Send To Validation")]
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
                exceptionHandler.CapturedImage(sendToValidation, Globals.testScriptName);
            }

            //Call the existing coded step here for HandleBusyIndicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate To Search Page", RequiresSilverlight = true)]
        public void NavigateToInvoiceAdministrationSearch()
        {
            try
            {
                //Call the Method to Navigate To Search Page 
                p2pNavigationObj.NavigateInvoiceAdministrationToSearch();
            }
            catch (Exception NavigateToSearch)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(NavigateToSearch, Globals.testScriptName);
            }

            //Handle Busy Indicator
            HandleBusyIndicator();
        }

        [CodedStep("Use Free Text to Search Invoice", RequiresSilverlight = true)]
        public void SearchInvoiceUsingFreeTextSearch()
        {
            try
            {
                //Use the Object to Access the Method "P2PInvoiceAdministrationFreeTextSearchToSearchInvoice"
                searchPRObj.P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(P2P_Utility.invoiceNumber);
            }
            catch (Exception SearchInvoiceUsingFreeTextSearch)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(SearchInvoiceUsingFreeTextSearch, Globals.testScriptName);
            }

            //Handle Busy Indicator
            HandleBusyIndicator();
        }

        [CodedStep("Verify Free Text Search", RequiresSilverlight = true)]
        public void VerifyFreeTextSearch()
        {
            try
            {
                // Call the Method "P2InvoiceAdministration_Search_VerifyInvoiceStatus" for Verify the Message in a Grid
                p2pInvoiceAdministrationVerificationObj.P2InvoiceAdministration_Search_VerifyInvoiceStatus(P2P_Utility.invoiceNumber, drE2E["Invoice_Final_Status"].ToString(), drE2E["Header_Name_Status"].ToString(), drE2E["Button_Text"].ToString());
            }
            catch (Exception VerifyFreeTextSearch)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(VerifyFreeTextSearch, Globals.testScriptName);
            }

            //Handle Busy Indicator
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Matching and Search Invoice", RequiresSilverlight = true)]
        public void NavigateToMatchingAndSearchInvoice()
        {
            try
            {
                //Call the Method to Navigate To Matching Page 
                p2pNavigationObj.NavigateInvoiceAdministrationToMatching();
            }
            catch (Exception navigateToMatchingAndSearchInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToMatchingAndSearchInvoice, Globals.testScriptName);
            }

            //Call Again Coded Step Handle Busy Indicator
            HandleBusyIndicator();
            
            //Search Invoice in Matching Area
            SearchInvoiceUsingFreeTextSearch();
        }

        [CodedStep("Open Invoice On Matching Silo", RequiresSilverlight = true)]
        public void OpenInvoiceOnMatchingPage()
        {
            try
            {
                //Use the Object to Access the Method to Open the Selected Invoice 
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PMatchingOpenSelectedInvoice();
            }
            catch (Exception openInvoiceOnMatchingPage)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(openInvoiceOnMatchingPage, Globals.testScriptName);
            }
            //Call again Coded Handle Busy Indicater
            HandleBusyIndicator();
        }

        [CodedStep("Search Purchase Order Number to Associate", RequiresSilverlight = true)]
        public void SearchPurchaseOrderNumberToAssociate()
        {
            try
            {
                //Call the Method to Search for Purchase Order
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PMatchingSearchPurchaseOrder(P2P_Utility.poNumber);
            }
            catch (Exception searchPurchaseOrderNumberToAssociate)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchPurchaseOrderNumberToAssociate, Globals.testScriptName);
            }
            //Call again Coded Handle Busy Indicater
            HandleBusyIndicator();
        }
                
        [CodedStep("Associate Searched Purchase Order with invoice", RequiresSilverlight = true)]
        public void AssociateSearchedPurchaseOrder()
        {
            try
            {
                //Call the Method to Search for Purchase Order
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PMatchingSelectSearchedPurchaseOrder(P2P_Utility.poNumber);
            }
            catch (Exception AssociateSearchedPurchaseOrder)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(AssociateSearchedPurchaseOrder, Globals.testScriptName);
            }
            //Call again Coded Handle Busy Indicater
            HandleBusyIndicator();
        }

        [CodedStep("Confirm Manual Match with Purchase Order", RequiresSilverlight = true)]
        public void ConfirmManualMatch()
        {
            try
            {
                //Call the Method to Search for Purchase Order
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PMatchingConfirmInvoice();
            }
            catch (Exception confirmManualMatch)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(confirmManualMatch, Globals.testScriptName);
            }
            //Call again Coded Handle Busy Indicater
            HandleBusyIndicator();
        }

        [CodedStep("Navigate to Transfer Silo", RequiresSilverlight = true)]
        public void NavigateToTransfer()
        {
            try
            {
                //Call the Method to Navigate To Transfer Silo 
                p2pNavigationObj.NavigateInvoiceAdministrationToTransfer();

            }
            catch (Exception Refresh)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(Refresh, Globals.testScriptName);
            }

            //Call Again Coded Step Handle Busy Indicator
            HandleBusyIndicator();
        }

        [CodedStep("Search Invoice On Transfer Silo", RequiresSilverlight = true)]
        public void SearchInvoiceOnTransferPage()
        {
            //Call Coded Step to Search
            SearchInvoiceUsingFreeTextSearch();
        }

        [CodedStep("Verify Free Text Search On Transfer Silo", RequiresSilverlight = true)]
        public void VerifyFreeTextSearchOnTransferSilo()
        {
            try
            {
                // Call the Method for Verify the Message in a Grid
                p2pInvoiceAdministrationVerificationObj.P2PVerifyFreeTextSearch(drE2E["Header_Name"].ToString(), Convert.ToInt32(drE2E["Iteration"]), P2P_Utility.invoiceNumber, drE2E["Search_Option"].ToString());
            }
            catch (Exception verifyFreeTextSearchOnTransferSilo)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(verifyFreeTextSearchOnTransferSilo, Globals.testScriptName);
            }
        }

        [CodedStep("Open Searched Invoice On Transfer Page", RequiresSilverlight = true)]
        public void OpenSelectedInvoice()
        {
            //Open Searched Invoice
            OpenInvoiceOnMatchingPage();
        }
        
        [CodedStep("Verify History and Comments", RequiresSilverlight = true)]
        public void VerifyHistoryAndComments()
        {
            try
            {
                // Call the Method "P2PInvoiceAdministration_HistoryAndCommentsVerification" for Verify the Message in a Grid
                p2pInvoiceAdministrationVerificationObj.P2PInvoiceAdministration_HistoryAndCommentsVerification(verifyHistory, Convert.ToInt32(drE2E["Array_Index_2"]), drE2E["Search_History_Comment_ListBox"].ToString(), drE2E["History_Comment_ListBox"].ToString(), drE2E["ComboBox"].ToString());
            }
            catch (Exception verifyHistoryAndComments)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(verifyHistoryAndComments, Globals.testScriptName);
            }
        }

        [CodedStep("Logout From Application", RequiresSilverlight = true)]
        public void LogoutFromApplication()
        {
            //Calling Coded Step Again
            LogoutFromP2P();
        }
    }
}