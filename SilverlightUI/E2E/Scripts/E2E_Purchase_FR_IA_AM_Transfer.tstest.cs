using System;
using System.Data;
using System.Globalization;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Design.Execution;
using E2E.Class;
using P2P.Testing.Shared;
using P2P.Testing.Shared.Class;
using P2P.Testing.Shared.Class.InvoiceAdministration;
using P2P.Testing.Shared.Class.InvoiceAdministration.Create;
using P2P.Testing.Shared.Class.InvoiceAdministration.InvoiceDetails;
using P2P.Testing.Shared.Class.InvoiceAdministration.Search;
using P2P.Testing.Shared.Class.InvoiceAdministration.ToolbarActions;
using P2P.Testing.Shared.Class.Purchase;
using P2P.Testing.Shared.Class.Purchase.GoodsReceipts;
using P2P.Testing.Shared.Class.Purchase.ToolbarActions;
using P2P.Testing.Shared.Class.WebShop;
using P2P.Testing.WebShop;

namespace E2E
{
    public class E2E_Purchase_FR_IA_AM_Transfer : BaseWebAiiTest
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

        //Create object for P2PPaymentPlansSearch class
       // P2PPaymentPlansSearch p2pPaymentPlansSearchObj = new P2PPaymentPlansSearch();

        //Create object for P2PGoodsReceiptsSearch class
        P2PGoodsReceiptsSearch p2pGoodsReceiptsSearchObj = new P2PGoodsReceiptsSearch();
        
        //Create an object for P2PWebShopProductSearch class
       // P2PWebShopProductsSearch searchWebshopProdutsObj = new P2PWebShopProductsSearch();

        //Create an object for P2PInvoiceAdministrationSearch class
        P2PInvoiceAdministrationSearch searchPRObj = new P2PInvoiceAdministrationSearch();

        //Create an object for P2PWebShopVerifications class
        P2PWebShopVerifications p2pWebShopVerificaitonObj = new P2PWebShopVerifications();

        //Create an object for P2PWebShopShoppingBasket class
       //P2PWebShopShoppingBasket webshopShoppingBasketObj = new P2PWebShopShoppingBasket();                        

        //Create an object for P2PInvoiceAdministrationInvoiceDetails class
        P2PInvoiceAdministrationInvoiceDetails p2pInvoiceAdministrationInvoiceDetailsObj = new P2PInvoiceAdministrationInvoiceDetails();

        //Create an object for P2PInvoiceAdministrationImageActions class
        P2PInvoiceAdministrationImageActions addImageObj = new P2PInvoiceAdministrationImageActions();

        //Create an object for P2PInvoiceAdministrationVerification class
        P2PInvoiceAdministrationVerification p2pInvoiceAdministrationVerificationObj = new P2PInvoiceAdministrationVerification();

        //Create object for P2PGoodsReceiptsVerification class
        P2PGoodsReceiptsVerification p2pGoodsReceiptsVerificationObj = new P2PGoodsReceiptsVerification();

        //Create an object for P2PGoodsReceiptsToolbarActions class
       // P2PGoodsReceiptsToolbarActions p2pGoodsReceiptsToolbarActionsObj = new P2PGoodsReceiptsToolbarActions();

        //Create an object for P2PPurchaseLineRquisitionToolBarActions class
        P2PPurchaseLineRquisitionToolBarActions toolBarActionsObj = new P2PPurchaseLineRquisitionToolBarActions();

        //Create an object for P2PExceptionHandler class
        P2PExceptionHandler exceptionHandler = new P2PExceptionHandler();

        //Constructor
        public E2E_Purchase_FR_IA_AM_Transfer()
        {
            //Initializing value to declared vaiable 'uploadPath'
            uploadPath = objReadXml.Upload_file("Sample.tiff"); 

            //Read the Data Row from XML 
            drE2E = objReadXml.Read_xml_file("E2E_TestData.xml", "E2E_Purchase_FR_IA_AM_Transfer", "Functionality", "E2EFullyReceived");
            
            //Saving Data in Array
            prPendingStatus = new string[] { drE2E["Purchase_Requisition_Ordered_Status"].ToString() };
            prInfoStatus = new string[] { drE2E["Purchase_Requisition_Received_Status"].ToString() };
            verifyHistory = new string[] { drE2E["Verify_History_1"].ToString(), drE2E["Verify_History_2"].ToString(), drE2E["Verify_History_3"].ToString() };
            
            //Calling Generate Function
            P2P_Utility.GenerateNumber();
        }

        //[CodedStep("E2E Fix Work Around: Login into P2P with veijol User")]
        //public void WorkAroundLoginintoP2P()
        //{            
        //    LoginintoP2P();
        //    HandleBusyIndicator();
        //    NavigateToPersonalMode();
        //    NavigateToWebshop();
        //    LogoutFromP2P();
        //}

        [CodedStep("Login into P2P", RequiresSilverlight = true)]
        public void LoginintoP2P()
        {
            // Create an object for "P2PLogin" class
            var objlogin = new P2PLogin(ActiveBrowser);

            try
            {
                //Call  method "LoginToSilverlightClient" from the "P2PLogin" class                
                objlogin.LoginToSilverlightClient("basware\\tonikoi", "");
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

        [CodedStep("Navigate to Personal Mode Search", RequiresSilverlight = true)]
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
                p2pWebShopObj.WebShopProductsSearch(drE2E["Product_Name"].ToString(), drE2E["Product_TabItem"].ToString());
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
            //Navigate to ProductSearch page
            NavigateToProductSearchPage();
            //Navigate to shopping basket page
            NavigateToProductBasketPage();

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

            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        //Harmeet 18 Mar 2015: Commented out un necessary code.
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

            //Call the Coded Step here for Handle Busy Indicator 
            HandleBusyIndicator();
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

            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }

        [CodedStep("Navigate To Pricing Tab", RequiresSilverlight = true)]
        public void NavigateToPricingTab()
        {
            try
            {
                //Call the Method to p2pNavigationObj
                p2pNavigationObj.NavigateToMyPurchasesPricingTab(drE2E["Select_Pricing_Tab"].ToString());
            }

            catch (Exception navigateToPricingTab)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToPricingTab, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
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
				
        
        [CodedStep("Navigate to MyPurchases Info Tab", RequiresSilverlight = true)]
        public void NavigateToMyPurchaseInfo()
        {
            try
            {
                //Call the Method to Navigate to MyPurchases 
                p2pNavigationObj.NavigateToWebShopPurchaseRequisitionPage();
            }
            catch (Exception navigateToMyPurchases)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToMyPurchases, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();

            try
            {
                //Call the Method to Click On Info tab  
                p2pNavigationObj.NavigateToWebShopMyPRInfoFilter();
            }
            catch (Exception ClickInfoTab)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(ClickInfoTab, Globals.testScriptName);
            }
            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
        }
       
        [CodedStep("Verify Purchase Requisition status: In Workflow", RequiresSilverlight = true)]
        public void VerifyPurchaseRequisitionStatusInWorkflow()
        {
            SearchPurchaseRequisition();


            //Added this Verification code as the Application response time is slow and it doesnt update PR status from "Draft"  to "Workflow"
            try
            {
                //Call the Method to Verify PR starts showing under correct tab                
                p2pInvoiceAdministrationVerificationObj.P2PVerifyFreeTextSearch("Purpose", 10, P2P_Utility.purposeNumber, "SHOP_SEARCH");
            }
            catch (Exception verifyPurchaseRequisitionShows)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(verifyPurchaseRequisitionShows, Globals.testScriptName);
            }


            try
            {
                //Call the Method to Verify Grid returned with PR in status "In Workflow"  
                p2pInvoiceAdministrationVerificationObj.P2PVerifyFreeTextSearch(drE2E["PR_Header_Name_Status"].ToString(), 2, drE2E["PR_InWorkflow"].ToString(), "SHOP_SEARCH");
                p2pInvoiceAdministrationVerificationObj.P2PVerifyFreeTextSearch(drE2E["Header_Name_Current_Recipient"].ToString(), 1, drE2E["Current_Recipient_Name"].ToString(), "SHOP_SEARCH");
            }
            catch (Exception VerifyPurchaseRequisitionsStatusOrdered)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(VerifyPurchaseRequisitionsStatusOrdered, Globals.testScriptName);
            }
        }        

		[CodedStep("Logout Basware Toni From P2P", RequiresSilverlight = true)]
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

        [CodedStep("Login into P2P with First Approver", RequiresSilverlight = true)]
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

            ////Call the Coded Step Handle Busy Indicator Again
            //HandleBusyIndicator();
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
            //Workaround to Refresh the Grid for getting the Requisition for Approval.
           // NavigateToGoodsReceipts();

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
       
        /**************************************************Owner Logs into the Application**********************************************************/

        [CodedStep("Login into P2P with PR Owner", RequiresSilverlight = true)]
        public void LoginintoP2PWithOwner()
        {
            //Coded step call
            LoginintoP2P();
        }

        [CodedStep("Onwer: Navigate to Personnel Mode", RequiresSilverlight = true)]
        public void AgainNavigateToPersonalMode()
        {
            //Call the Coded Step Navigate To Personel Mode Again
            NavigateToPersonalMode();

            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();
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
            //Workaround to Refresh the Grid for getting the Requisition.
           // NavigateToProductSearchPage();

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
            //Workaround to refresh the Status of the Purchase Requisition
            p2pNavigationObj.NavigateToWebShopMyPRDraftFilter();

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
            //try
            //{
            //    //Call the Method to Verify Status
            //    //p2pWebShopVerificaitonObj.P2PWebShop_VerifyPurchaseRequisitionStatus(prPendingStatus, Convert.ToInt32(drE2E["Array_Index_1"]));
            //    p2pInvoiceAdministrationVerificationObj.P2PVerifyFreeTextSearch(drE2E["PR_Header_Name_Status"].ToString(), 10, drE2E["Purchase_Requisition_Ordered_Status"].ToString(), "SHOP_SEARCH");
            //}

            //catch (Exception verifyPurchaseRequisitionOrderedStatus)
            //{
            //   Globals.testScriptName = ExecutionContext.Test.Name + "_";
            //    exceptionHandler.CapturedImage(verifyPurchaseRequisitionOrderedStatus, Globals.testScriptName);
            //}

            //Added this Verification code as the Application response time is slow and it doesnt update PR status from "Approved" to "Ordered"
            try
            {
                //Call the Method to Verify PR starts showing under correct tab                
                p2pInvoiceAdministrationVerificationObj.P2PVerifyFreeTextSearch("Purpose", 10, P2P_Utility.purposeNumber, "SHOP_SEARCH");
            }
            catch (Exception verifyPurchaseRequisitionShows)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(verifyPurchaseRequisitionShows, Globals.testScriptName);
            }

            try
            {
                //Call the Method to Verify Status
                //p2pWebShopVerificaitonObj.P2PWebShop_VerifyPurchaseRequisitionStatus(prPendingStatus, Convert.ToInt32(drE2E["Array_Index_1"]));                
                p2pInvoiceAdministrationVerificationObj.P2PVerifyFreeTextSearch(drE2E["PR_Header_Name_Status"].ToString(), 0, drE2E["Purchase_Requisition_Ordered_Status"].ToString(), "SHOP_SEARCH");
            }
            catch (Exception verifyPurchaseRequisitionOrderedStatus)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(verifyPurchaseRequisitionOrderedStatus, Globals.testScriptName);
            }            
        }

        [CodedStep("Navigate To My Tasks - Goods Receipts Page to Receive all Goods", RequiresSilverlight = true)]
        public void NavigateToMyTaskGoodsReceiptsReceiveAllGoods()
        {
            //Calling Coded Step Again
            NavigateToMyTasks();

            //Workaround to refresh the Status of the Purchase Requisition
           // NavigateToMyTasksPurchaseRequisitionPage();
        }

        [CodedStep("Navigate To Goods Receipts", RequiresSilverlight = true)]
        public void NavigateToGoodsReceipts()
        {
            try
            {
                //Call the Method to Navigate To Goods Receipts
                p2pNavigationObj.NavigateToMyTasksGoodReceiptPage();
            }
            catch (Exception navigateToGoodsReceipts)
            {
               Globals.testScriptName = ExecutionContext.Test.Name + "_";
               exceptionHandler.CapturedImage(navigateToGoodsReceipts, Globals.testScriptName);
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
            catch (Exception searchGoodsReceipts)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchGoodsReceipts, Globals.testScriptName);
            }

            //Call the Coded Step Handle Busy Indicator Again
            HandleBusyIndicator();

            //search for GR again on Jnekins server execution is fast and GR doesnt show under this tab soon enough
            try
            {
                //Call the Method to Verify GR starts showing under GR Tab                
                p2pInvoiceAdministrationVerificationObj.P2PVerifyFreeTextSearch("Requisition Title", 10, P2P_Utility.purposeNumber, "SHOP_SEARCH");
            }
            catch (Exception verifyGRShows)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(verifyGRShows, Globals.testScriptName);
            }
        }

                
        [CodedStep("Fully Receive GR", RequiresSilverlight = true)]
        public void FullyReceiveGoodsReceipts()
        {
            try
            {
                //Call the Method to Fully Receive GR
                goodsReceiptsObj.P2PGoodsReceiptsReceived(null, drE2E["Product_Name"].ToString());
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

            //Workaround to Refresh Grid
            NavigateToProductSearchPage();

            //Call existing Coded Step here: NavigateToWebShopPurchaseRequisition
            NavigateToWebshopMyPurchasesPage();

            //Workaround to Refresh Grid
            ClickPendingTab();

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

        [CodedStep("Open Selected Purchase Requisition", RequiresSilverlight = true)]
        public void OpenSelectedPurchaseRequisition()
        {
            try
            {
                //Call the Method to Open Selected Purchase Requisition
                p2pWebShopObj.P2POpenSelectedPurchaseRequisition();
            }

            catch (Exception openSelectedPurchaseRequisition)
            {
               Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(openSelectedPurchaseRequisition, Globals.testScriptName);
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

            catch (Exception navigateToRelatedDocumentTab)
            {
              Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(navigateToRelatedDocumentTab, Globals.testScriptName);
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

        [CodedStep("Login into P2P", RequiresSilverlight = true)]
        public void LogInP2PApplication()
        {
            //Create object for P2PLogin class
            var objlogin = new P2PLogin(ActiveBrowser);

            try
            {
                //Call the Method to Login into Application                
                objlogin.LoginToSilverlightClient("basware\\esat", "");
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
                Convert.ToDouble(drE2E["Tax_Sum_FR"], CultureInfo.InvariantCulture.NumberFormat),null,null,null,null,drE2E["Text_20"].ToString());                
            }
            catch (Exception createNewInvoice)
            {
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(createNewInvoice, Globals.testScriptName);
            }
        }

        [CodedStep("Add Purchase Order Number", RequiresSilverlight = true)]
        public void AddPurchaseOrderNumber()
        {            
            try
            {
                //Use the object to Access the Method to Add PO Number
                p2pInvoiceAdministrationInvoiceDetailsObj.P2PMatchingUpdatePONumber(P2P_Utility.poNumber);
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
            catch (Exception searchInvoiceUsingFreeTextSearch)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(searchInvoiceUsingFreeTextSearch, Globals.testScriptName);
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
                p2pInvoiceAdministrationVerificationObj.P2InvoiceAdministration_Search_VerifyInvoiceStatus(P2P_Utility.invoiceNumber, drE2E["Invoice_Final_Status"].ToString(), drE2E["Header_Name_Status"].ToString(), drE2E["Button_Text"].ToString(), drE2E["Invoice_Final_Status1"].ToString());
            }
            catch (Exception verifyFreeTextSearch)
            {
                //Capture Image of Error and Throw Exception
                Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(verifyFreeTextSearch, Globals.testScriptName);
            }
        }

        [CodedStep("Open Search Invoice On Invoice Administration Search Page", RequiresSilverlight = true)]
        public void OpenSearchInvoiceAdministrationSearchPage()
        {
            try
            {
                //Create an Object for shared invoice class
                P2PInvoiceAdministrationToolbarActions openInvoice = new P2PInvoiceAdministrationToolbarActions(ActiveBrowser);

                //Use the Object to Access the Method "P2PInvoiceAdministrationSearchOpenSelectedInvoice" to Open the Selected Invoice 
                openInvoice.P2PInvoiceAdministrationSearchOpenSelectedInvoice();
            }
            catch (Exception openSearchInvoiceAdministrationSearchPage)
            {
               Globals.testScriptName = ExecutionContext.Test.Name + "_";
                exceptionHandler.CapturedImage(openSearchInvoiceAdministrationSearchPage, Globals.testScriptName);
            }
            //Call again Coded Handle Busy Indicater
            HandleBusyIndicator();
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