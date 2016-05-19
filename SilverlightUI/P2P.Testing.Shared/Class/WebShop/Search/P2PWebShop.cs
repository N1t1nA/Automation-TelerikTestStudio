using System;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Design;
using System.Collections.Generic;
using ArtOfTest.WebAii.Silverlight;
using Telerik.WebAii.Controls.Xaml;
using ArtOfTest.WebAii.Win32.Dialogs;
using E2E.Class;
using System.Windows.Forms;
using System.Globalization;
using ArtOfTest.WebAii.Win32;
using ArtOfTest.WebAii.Silverlight.UI;

namespace P2P.Testing.Shared.Class.WebShop
{
    public class P2PWebShop : BaseWebAiiTest
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
        
        //Method to Select Find-Form Option
        public void WebShopSearchFreeTextItemSelection(string freeTextItem)
        {
            //Set Focus on Webshop Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.Wait.ForExists(Globals.timeOut);

            //Set Focus on Webshop Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.SetFocus();

            //Click on Webshop Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();
            
            //Wait for Products to load
            P2PNavigation.CallBusyIndicator();

            //Wait for  Webshop Search Category Dropdown to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(freeTextItem)).Wait.ForExists(Globals.timeOut);

            //click on Find forms tab
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(freeTextItem)).User.Click();
        }

        //Method to Select Find-Form Option
        public void WebShopSearchClickOnAttachmentsTabItem()
        {
            //Set Focus on Webshop Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_ProductAttachmentsTabItem.Wait.ForExists(Globals.timeOut);

            //Set Focus on Webshop Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_ProductAttachmentsTabItem.SetFocus();

            //Click on Webshop Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_ProductAttachmentsTabItem.User.Click();
        }

        //Method to Export Invoice to Excel 
        public void WebshopOpenAttachment(string activeDirectory, string filePath)
        {
            //Create a Directory Under C: drive with Name TestAutomation
            System.IO.Directory.CreateDirectory(activeDirectory);

            //Change the Current Settings 
            Manager.Current.Settings.UnexpectedDialogAction = UnexpectedDialogAction.DoNotHandle;

            //Handle the Download Dialogs     
            DownloadDialogsHandler downloadHandler = new DownloadDialogsHandler(Manager.Current.ActiveBrowser, DialogButton.SAVE, filePath, Manager.Current.Desktop);

            //Start the Dilaog Moniter
            Manager.Current.DialogMonitor.Start();

            try
            {
                //Wait for ExportToExcel Toolbarbutton Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_ProductAttachments_OpenButton.Wait.ForExists(Globals.timeOut);

                //Click ExportToExcelToolbarbutton
                SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_ProductAttachments_OpenButton.User.Click();

                //Handle the Download Dialogs
                downloadHandler.WaitUntilHandled(Globals.navigationTimeOut);

            }
            catch (Exception Ex)
            {
                //Write the log if file not saved
                Manager.Current.Log.WriteLine(LogType.Error, "Unable to Save the File" + Ex.Message);
            }
        }

        //Method to Click On Product Category Button
        public void WebShopSearchProductCategory()
        {
            //Wait for Webshop Search Category Button to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_CategoryButton.Wait.ForExists(Globals.timeOut);

            //Click on Webshop Search Category Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_CategoryButton.User.Click();
        }

        //Method to Perform a More Actions 
        public void P2PWebShopMoreActions(string moreActionDropDownButton,string comment)
        {
            //Wait for AdditionalActionsDropdown Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Click on AdditionalActionsDropdown 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            //Wait for Remove button visible on the Application
            P2PNavigation.CallBusyIndicator();
            
            //Click on Remove button
            SharedElement.P2P_Application.SilverlightApp.Find.ByAutomationId(moreActionDropDownButton).User.Click();

            //Wait for Comment box exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);            

            //Sets the Focus to Add Comment Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.SetFocus();

            //Enter the Comment in AddComment TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(comment, 20);            
        }

        //Method to Select purchaseRequisition
        public void P2PWebShopSelectPurchaseRequisition(string purchaseRequisition)
        {
            //Wait for the Grid View Control
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_RadGridViewcontrol.Wait.ForExists(Globals.timeOut);
            //Click on purchaseRequisition
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_RadGridViewcontrol.Find.ByTextContent(purchaseRequisition).User.Click();
        }


        //************** Below functions are uses in E2E Test Scripts, Please do not modify**********
        //Method to Fetch Purchase Order
        public void P2PFetchPurchaseOrderNumber(string data, string findForms = null)
        {
            switch (data)
            {
                case "Purchase Order":
                    {
                        //Wait for Grid Control Exists in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_RelatedDocumentListBox.Wait.ForExists(Globals.timeOut);

                        //Use FrameworkElement and read all the TextBlock in a fe
                        FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_RelatedDocumentListBox;

                        //Calling Fetching Data Method
                        P2P_Utility.FetchingDataFromUI(fe, data);

                        break;
                    }

                case "Gross Total":
                    {
                        //Wait for Grid Control Exists in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_GrossSum_HeaderDataTextBox.Wait.ForExists(Globals.timeOut);

                        //Use FrameworkElement and read all the TextBlock in a fe
                        ArtOfTest.WebAii.Silverlight.UI.TextBox grossTotalData = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_GrossSum_HeaderDataTextBox;

                        //Getting and Saving Data in Global Variable
                        P2P_Utility.FetchingDataFromUI(grossTotalData, data);

                        break;
                    }

                case "Supplier Name":
                    {
                        //Find and save element in FrameWorkElement fe
                        SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_MyPurchases_TabControlRows.Find.ByTextContent("Line Data").User.Click();

                        if (findForms != null)
                        {
                            //Wait for Grid Control Exists in DOM
                            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierTextBox.Wait.ForExists(Globals.timeOut);
                            ArtOfTest.WebAii.Silverlight.UI.TextBox supplierNameData = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=txtSuppliers")).CastAs<ArtOfTest.WebAii.Silverlight.UI.TextBox>();
                            //Getting and Saving Data in Global Variable
                            P2P_Utility.FetchingDataFromUI(supplierNameData, data);
                        }
                        else
                        {
                            //Wait for Grid Control Exists in DOM
                            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_SupplierNameHeaderData_TextBox.Wait.ForExists(Globals.timeOut);
                            //Use FrameworkElement and read all the TextBlock in a fe
                            ArtOfTest.WebAii.Silverlight.UI.TextBox supplierNameData = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=Supplier_HeaderDataField")).CastAs<ArtOfTest.WebAii.Silverlight.UI.TextBox>();

                            //Getting and Saving Data in Global Variable
                            P2P_Utility.FetchingDataFromUI(supplierNameData, data);
                        }

                        break;
                    }

                default:
                    {
                      
                        //Write the log if no case found.
                        throw new Exception(LogType.Error + ": No Case Found. Please Check!!");
                    }
            }
        }

        //Method to Open Selected Purchase Requisition
        public void P2POpenSelectedPurchaseRequisition()
        {
            //Wait for WebShop Purchase Requisition Open Selected Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_OpenSelectedButton.Wait.ForExists(Globals.timeOut);

            //Click on WebShop Purchase Requisition Open Selected Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_OpenSelectedButton.User.Click();
        }

        //Method to create Find Forms
        public void P2PWebShopFindForms(string tabItem, string findFormsDetails, string name, string purchaseCategory, double unitPrice, string quantityUnit, string currencyCode, string taxCode, string supplierCode)
        {
            //Wait for Webshop Product Search Button to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.Wait.ForExists(Globals.timeOut);

            //Set focus on Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.SetFocus();

            //Click on Webshop Product Search button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click(MouseClickType.LeftDoubleClick);

            //Wait for Page Load Successfully
            P2PNavigation.CallBusyIndicator();
            //System.Threading.Thread.Sleep(Globals.navigationTimeOut);

            //Wait for  Webshop Find Forms Tab Item to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.Find.ByAutomationId(tabItem).Wait.ForExists(Globals.timeOut);
            //Click on Find Forms  Tab Item
            SharedElement.P2P_Application.SilverlightApp.Find.ByAutomationId(tabItem).User.Click();

            //Wait for Page Load Successfully
            P2PNavigation.CallBusyIndicator();
            //Wait for  Webshop findFormsDetails Tab Item to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.Find.ByAutomationId(findFormsDetails).Wait.ForExists(Globals.timeOut);
            //Click on findFormsDetails Tab Item
            SharedElement.P2P_Application.SilverlightApp.Find.ByAutomationId(findFormsDetails).User.Click();

            //Wait for Name Text feilds to  Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_FreeTextItem_HeaderData_NameTextBox.Wait.ForExists(Globals.timeOut);
            //Set focus on the text box
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_FreeTextItem_HeaderData_NameTextBox.SetFocus();
            //Enter the Find forms Name
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_FreeTextItem_HeaderData_NameTextBox.User.TypeText(name, 100);

            //Press to move the Focus on the next text box
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            // Wait for P2P_WebShop_Search_FreeTextItem_HeaderData_PurchasingCategoryTextBox to  Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_FreeTextItem_HeaderData_PurchasingCategoryTextBox.Wait.ForExists(Globals.timeOut);
            //Set focus on the text box
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_FreeTextItem_HeaderData_PurchasingCategoryTextBox.SetFocus();
            //Enter the HeaderData_PurchasingCategory
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_FreeTextItem_HeaderData_PurchasingCategoryTextBox.User.TypeText(purchaseCategory, 100);

            //Press Tab button to move the Focus on the next text box
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Enter the  Unit price
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_FreeTextItem_HeaderData_UnitPriceTextBox.User.TypeText(unitPrice.ToString(CultureInfo.CurrentCulture.NumberFormat), 100);
            //Press to move the Focus on the next text box
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            // Wait for P2P_WebShop_Search_FreeTextItem_HeaderData_QuantityUnit_TextBox to  Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_FreeTextItem_HeaderData_QuantityUnit_TextBox.Wait.ForExists(Globals.timeOut);
            //Enter the value into Text box
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_FreeTextItem_HeaderData_QuantityUnit_TextBox.User.TypeText(quantityUnit.ToString(CultureInfo.CurrentCulture.NumberFormat), 100);

            //Press Tab button to move the Focus on the next text box
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab, 5, 2);

            // Wait for P2P_WebShop_Find_Form_Details_TaxCode_TextBox to  Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Find_Form_Details_TaxCode_TextBox.Wait.ForExists(Globals.timeOut);
            //Enter the value into Text box
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Find_Form_Details_TaxCode_TextBox.User.TypeText(taxCode, 100);

            //Press Tab button to move the Focus on the next text box
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab, 5, 4);

            //Wait for SupplierTextBox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierTextBox.Wait.ForExists(Globals.timeOut);
            //Enter the Supplier in SupplierTextBox Text box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierTextBox.User.TypeText(supplierCode, 100);
         
            //Wait for AddToBasketButton Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_AddToBasketButton.Wait.ForExists(Globals.timeOut);
            //Click on OK Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_AddToBasketButton.User.Click();
            //Press tab to move the focus on the next text box
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
        }

        //Method to Edit Purchase Requisition Details
        public void WebShopProductsEditPurchaseRequsitionDetails()
        {
            //Wait for Addtional Actions Dropdown Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Click on Addtional Actions Dropdown 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            //Wait for Edit Purchase requisition Details Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_AdditionalAction_CreateRequisitionButton.Wait.ForExists(Globals.timeOut);

            //Click on the Edit Purchase requisition Details
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_AdditionalAction_CreateRequisitionButton.User.Click();
        }

        //Method to Input Date And Purpose Data
        public void WebShopPurchaseRequisitionInputDateAndPurposeData(string purposeData)
        {
            //Wait for TextBox Exists in DOM
           // SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_GrossSumHeaderDataTextBox.Wait.ForExists(Globals.timeOut);

            //Wait for TextBox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_MyPurchases_PurchaseRequisition_PurposeHeaderDataTextbox.Wait.ForExists(Globals.timeOut);

            //Set Focus on Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_MyPurchases_PurchaseRequisition_PurposeHeaderDataTextbox.SetFocus();

            //Click on TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_MyPurchases_PurchaseRequisition_PurposeHeaderDataTextbox.User.TypeText(purposeData, 50);

            //Write Log for Purpose Number Generated
            Manager.Current.Log.WriteLine(LogType.Information, "Purpose Number Generated is '" + purposeData + "'");

            //Wait for TextBox Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_InvoiceDate_TextBox.Wait.ForExists(Globals.timeOut);

            //Set Focus on Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_InvoiceDate_TextBox.SetFocus();

            //Enter Date in Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_InvoiceDate_TextBox.User.TypeText(DateTime.Now.ToShortDateString(), 100);

            //Get the value of the Text Box
            var getDateTextBoxValue = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_InvoiceDate_TextBox.Text;

            //If Value is not entered it entered again
            if (getDateTextBoxValue.Equals(""))
            {
                //Click on  Date Text Box
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_InvoiceDate_TextBox.User.Click();

                //Enter Date in Text Box
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_InvoiceDate_TextBox.User.TypeText(DateTime.Now.ToShortDateString(), 100);
            }
        }

        public void P2PEnterSupplierCode(string supplierCode)
        {           

            //Find and save element in FrameWorkElement fe
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_MyPurchases_TabControlRows.Find.ByTextContent("Line Data").User.Click();

            //Wait for Textbox Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierTextBox.Wait.ForExists(Globals.timeOut);

            //Set Focus on Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierTextBox.SetFocus();

            //Click on Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierTextBox.User.Click();

            //Select the String in Text Box
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

            //Generate a Keyboard Event to clear search
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

            //Click on Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierTextBox.User.TypeText(supplierCode, 50);

            //Performing keyboard action
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
        }

        //Method to Input Account And Cost Center Code Data
        public void WebShopPurchaseRequisitionInputAccountAndCostCenterCodeData(string accountCode, string costCenterCode, string selectTab = null, string supplierCode = null)
        {
            //If condition is true then execute if block
            if (supplierCode != null)
            {
                //Wait for Textbox Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierTextBox.Wait.ForExists(Globals.timeOut);

                //Set Focus on Textbox
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierTextBox.SetFocus();

                //Click on Textbox
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierTextBox.User.Click();

                //Select the String in Text Box
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

                //Generate a Keyboard Event to clear search
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

                //Click on Textbox
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_HeaderData_SupplierTextBox.User.TypeText(supplierCode, 50);

                //Performing keyboard action
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
            }

            //Wait for Tab Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_MyPurchases_TabControlRows.Wait.ForExists(Globals.timeOut);

            //Find and save element in FrameWorkElement fe
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_MyPurchases_TabControlRows.Find.ByTextContent(selectTab).User.Click();

            //Wait for Textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_MyPurchases_Invoices_AccountNumberTextBox.Wait.ForExists(Globals.timeOut);

            //Set Focus on Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_MyPurchases_Invoices_AccountNumberTextBox.SetFocus();

            //Click on Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_MyPurchases_Invoices_AccountNumberTextBox.User.Click();

            //Select the String in Text Box
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

            //Generate a Keyboard Event to clear search
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

            //Click on Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_MyPurchases_Invoices_AccountNumberTextBox.User.TypeText(accountCode, 50);

            //Performing keyboard action
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Wait for Textbox Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_MyPurchases_Invoices_CostCenterTextBox.Wait.ForExists(Globals.timeOut);

            //Set Focus on Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_MyPurchases_Invoices_CostCenterTextBox.SetFocus();

            //Click on Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_MyPurchases_Invoices_CostCenterTextBox.User.Click();

            //Select the String in Text Box
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

            //Generate a Keyboard Event to clear search
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

            //Click on Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_MyPurchases_Invoices_CostCenterTextBox.User.TypeText(costCenterCode, 50);

            //Performing keyboard action
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
        }

        //Method to Send PR for Process
        public void WebShopPurchaseRequisitionSendToProcess()
        {
            //Wait for Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_SendToProcessButton.Wait.ForExists(Globals.timeOut);

            //Click on Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_SendToProcessButton.User.Click();
        }

        //Method to Add Products to Basket
        public void WebShopProductsAddToShoppingBasket(string productName)
        {
            //Wait for Tab Item Exists in DOM                     
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_ProductDetailsTabItem.Wait.ForExists(Globals.timeOut);

            //Wait for Tab Item Exists in DOM                     
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_ProductDetailsTabItem.User.Click();

            if (SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Product_GridViewControl.Rows.Count > 1)
            {
                //Select the TextBlock Product Name Exist in the Grid
                SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Product_GridViewControl.Find.ByTextContent(productName).As<TextBlock>().User.Click();
            }

            //Wait for  Webshop ProductDetailsTabItem  Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_ProductDetailsTabItem.Wait.ForExists(Globals.timeOut);

            //Click on the ProductDetailsTabItem
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_ProductDetailsTabItem.User.Click();

            //Call Static CallBusyIndicator method
            P2PNavigation.CallBusyIndicator();

            //Wait for  Webshop Product Add TO Basket Button Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=imagetextbutton", "AutomationId=AddToBasketButton")).Wait.ForExists(Globals.timeOut);

            //Click on the Product Add TO Basket Button
            //SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_AddToBasketButton.User.Click();
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=imagetextbutton", "AutomationId=AddToBasketButton")).User.Click();
            //Wait for system so that product get load successfully in DOM
            System.Threading.Thread.Sleep(Globals.handleTime);
        }


        //Method to Search the Webshop Products
        public void WebShopProductsSearch(string productName = null, string tabItem = null)
        {
            //Wait for Webshop Product Search Button to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.Wait.ForExists(Globals.timeOut);

            //Set focus on Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.SetFocus();

            //Click on Webshop Product Search button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click(MouseClickType.LeftDoubleClick);

            //Wait for Page Load Successfully
            P2PNavigation.CallBusyIndicator();

            if (productName != null)
            {
                //Wait for  Webshop Product Tab Item to Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.Find.ByAutomationId(tabItem).Wait.ForExists(Globals.timeOut);
                //Click on Product Tab Item
                SharedElement.P2P_Application.SilverlightApp.Find.ByAutomationId(tabItem).User.Click();

                //Wait for  Webshop Product Search Textbox to Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_Textbox.Wait.ForExists(Globals.timeOut);

                //Enter the Product Name in Product Search Text Box
                SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_Textbox.User.TypeText(productName, 50);
            }

            //Wait for Webshop Product Search Button to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.Wait.ForExists(Globals.timeOut);

            //Set focus on Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.SetFocus();

            //Click on Webshop Product Search button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click(MouseClickType.LeftDoubleClick);

            //Wait for Grid get populated the data
            P2PNavigation.CallBusyIndicator();
        }
        


        //**************************************
    }

}
