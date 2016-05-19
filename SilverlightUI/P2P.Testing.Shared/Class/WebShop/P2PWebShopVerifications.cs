using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using ArtOfTest.Common.UnitTesting;
using Telerik.WebAii.Controls.Xaml;
using ArtOfTest.WebAii.Silverlight;
using System.Drawing;
using ArtOfTest.WebAii.Controls.Xaml;
using ArtOfTest.WebAii.Silverlight.UI;
using P2P.Testing.Shared;
using P2P.Testing.Shared.Class;
using System.Globalization;
using System.Windows.Forms;
using ArtOfTest.WebAii.Win32;



namespace P2P.Testing.WebShop
{
    public class P2PWebShopVerifications : BaseWebAiiTest
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

        //Method to Verify WebShop Products Search       
        public void P2PWebShop_VerifyProductsSearch(string productName)
        {
            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol")).Wait.ForExists(Globals.timeOut);

            //Use FrameworkElement and read all the TextBlock in a fe
            FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=baslistviewgridcontrol"));

            //Check the search  result is "No Search Result" or other than Search Results
            bool search = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(productName));

            //Compare Each Row Value
            if (search == true)
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, "  Search Succeed, Product Found by The Product Name:- " + productName + " Verification Passed!");
            }
            else
            {
                //Write the log if Verification Fail
                //Manager.Current.Log.WriteLine(LogType.Error + "  No Product Found by Product Name:- " + productName + " Verification Failed!!!!");
                throw new Exception("  No Product Found by Product Name:- " + productName + " Verification Failed!!!!");
            }
        }

        //Method to Verify Add Products To Basket 
        public void P2PWebShop_VerifyProductsAddToBasket(string productName)
        {
            //Wait for Page to load Properly CallBusyIndicator
            P2PNavigation.CallBusyIndicator();

            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_ShopingBasket_GridViewControl.Wait.ForExists(Globals.timeOut);

            //Use FrameworkElement and read all the TextBlock in a RadGrid
            FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_ShopingBasket_GridViewControl;

            //Check the search  result is "No Search Result" or other than Search Results
            bool search = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(productName));

            //Compare Each Row Value
            if (search == true)
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, " Product Added Successfully in the Shopping Basket by Name:- " + productName + ", Verification Passed");
            }
            else
            {
                //Write the log if Verification Fail
                throw new Exception("  No Product Added in the Shopping Basket by Name:- " + productName + ", Verification Failed!");
                //Manager.Current.Log.WriteLine(LogType.Error + "  No Product Added in the Shopping Basket by Name:- " + productName + ", Verification Failed!");
            }
        }

        //Method to Verify Saved Shopping Basket 
        public void P2PWebShop_VerifySaveShoppingBasket(string shoppingListName)
        {
            //Wait for Addtional Actions Dropdown Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Click on Addtional Actions Dropdown 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            // Wait For WebShop SaveBasketButton Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_AdditionalAction_SelectShoppingListButton.Wait.ForExists(Globals.timeOut);

            //Click on WebShop SaveBasketButton
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_AdditionalAction_SelectShoppingListButton.User.Click();

            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_SelectShoppingLists_GridViewControl.Wait.ForExists(Globals.timeOut);

            //Use FrameworkElement and read all the TextBlock in a fe
            FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_SelectShoppingLists_GridViewControl;

            //Check the search  result is "No Search Result" or other than Search Results
            bool search = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(shoppingListName));

            try
            {
                //Compare Each Row Value
                if (search == true)
                {
                    //Write the log if Verification Pass
                    Manager.Current.Log.WriteLine(LogType.Information + "  Shopping List Found Successfully in the Grid by Name:- " + shoppingListName + " Verification Passed!");
                }
                else
                {
                    //Write the log if Verification Fail
                    throw new Exception("No Shopping List Found in the Grid by Name:- " + shoppingListName + " Verification Failed!!!!");
                    //Manager.Current.Log.WriteLine(LogType.Error + "  No Shopping List Found in the Grid by Name:- " + shoppingListName + " Verification Failed!!!!");
                }
            }

            //Use Finally to forcefully Close the Select Shopping List
            finally
            {
                //Wait for OK Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

                //Click on OK
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
            }

            //Wait for Page to load Properly CallBusyIndicator
            P2PNavigation.CallBusyIndicator();

        }

        //Method to Verify Renamed Shopping List 
        public void P2PWebShop_VerifyRenameShoppingList(string shoppingListName)
        {
            //Wait for Addtional Actions Dropdown Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Click on Addtional Actions Dropdown 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            // Wait For WebShop SelectBasketButton Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_AdditionalAction_SelectShoppingListButton.Wait.ForExists(Globals.timeOut);

            //Click on WebShop SelectBasketButton
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_AdditionalAction_SelectShoppingListButton.User.Click();

            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_SelectShoppingLists_GridViewControl.Wait.ForExists(Globals.timeOut);

            //Use FrameworkElement and read all the TextBlock in a fe
            FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_SelectShoppingLists_GridViewControl;

            //Check the search  result is "No Search Result" or other than Search Results
            bool search = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(shoppingListName));


            try
            {
                //Compare Each Row Value
                if (search == true)
                {
                    //Write the log if Verification Pass
                    Manager.Current.Log.WriteLine(LogType.Information + "  Updated Shopping List Found Successfully in the Grid by Name:- " + shoppingListName + " Verification Passed!");
                }
                else
                {
                    //Write the log if Verification Fail
                    Manager.Current.Log.WriteLine(LogType.Error + "  No Updated Shopping List Found in the Grid by Name:- " + shoppingListName + " Verification Failed!!!!");
                }
            }

            //Use Finally to forcefully Close the Select Shopping List
            finally
            {

                //Wait for OK Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

                //Click on OK Button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
            }

            //Calling Busy Indicator
            P2PNavigation.CallBusyIndicator();

        }

        //Method to Verify the ShopingBasketEmpty after Check Out Products
        public void P2PWebShop_VerifyShopingBasketEmpty(string productCheckOutResult)
        {
            //Wait for grid exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_ShopingBasket_GridViewControl.Wait.ForExists(Globals.timeOut);

            //Check the condition Row Count is 0
            if (SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_ShopingBasket_GridViewControl.Rows.Count.Equals(0))
            {



                //Write the Log If Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, "Products Check Out from the ShoppingBasket. Grid Contains Text" + ": " + productCheckOutResult + ". Verification Passed");






            }

            else
            {




                //Write the Log If Verification Failed
                throw new Exception(LogType.Error + "Products Check Out Failed, Grid Does NOT contains text: " + " " + productCheckOutResult + ". Verification Failed");
            }
        }

        //Verify that Compared Products Count
        public void P2PWebShop_CompareItems(string verifyItems)
        {
            //Get the String Compare
            string compareText = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new XamlFindExpression("TextContent=Compare")).TextLiteralContent;
            //Get the No of Items
            string noOfItems = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new XamlFindExpression("TextContent=~Items")).TextLiteralContent;

            //Concatenate String and No of Items
            string verifyProducts = compareText + noOfItems;

            try
            {
                //Check the Condition if Both Matches
                if (verifyProducts == verifyItems)
                {

                    //Logs if Verification Pass
                    Manager.Current.Log.WriteLine("Items Comparison at Execution Time:" + " " + verifyProducts + " " + "Matches with the Expected Result: " + verifyItems + ", Verification Pass");

                }
                else
                {
                    //Logs if Verification Failed
                    throw new Exception("Items at Execution Time: " + " " + verifyProducts + " " + "does NOT Match with the Expected Result: " + verifyItems + ", Verification Failed");

                }
            }
            finally
            {
                //Calling Busy Indicator
                P2PNavigation.CallBusyIndicator();
            }
        }

        //Method to Verify the Remove Shopping List
        public void P2PWebShop_VerifyRemoveShopingList()
        {
            //Wait for Addtional Actions Dropdown Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Click on Addtional Actions Dropdown 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            //Calling Busy Indicator
            P2PNavigation.CallBusyIndicator();

            //Code will be used for future Reference 

            /* if (SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_AdditionalAction_SelectShoppingListButton.IsEnabled.Equals(true))
             {

                 // Wait For WebShop SelectBasketButton Exists in DOM
                 SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_AdditionalAction_SelectShoppingListButton.Wait.ForExists(timeout);

                 //Click on WebShop SelectBasketButton
                 SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_AdditionalAction_SelectShoppingListButton.User.Click();

                 //Wait for Grid Control Exists in DOM
                 SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_SelectShoppingLists_GridViewControl.Wait.ForExists(timeout);

                 //Use FrameworkElement and read all the TextBlock in a fe
                 FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_SelectShoppingLists_GridViewControl;

                 //Check the search  result is "No Search Result" or other than Search Results
                 bool search = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(ShoppingListname));
             }
           */

            //Check the search  result For Shopping List Removed Successfully or Not
            bool search = SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_AdditionalAction_SelectShoppingListButton.IsEnabled.Equals(true);

            try
            {
                //Compare Each Row Value
                if (search == true)
                {
                    //Write the log if Verification Fail
                    throw new Exception(LogType.Error + " Shopping List Exits, as Additional Actions Context Menu Item is Enabled, Verification Failed!!!!");
                }
                else
                {
                    //Write the log if Verification Pass
                    Manager.Current.Log.WriteLine(LogType.Information + "  No Shopping List Exits, as Additional Actions Context Menu Item is Disabled, Verification Passed!");
                }

            }
            finally
            {
                //Calling Busy Indicator
                P2PNavigation.CallBusyIndicator();

            }



            //Code will be used for future Reference 

            ////Use Finally to forcefully Close the Select Shopping List
            //finally
            //{

            //    //Wait for OK Button Exists in DOM
            //    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(timeout);

            //    //Click on OK Button
            //    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
            //}

            ////Wait for Page to load Properly
            //System.Threading.Thread.Sleep(pause);


        }

        //Method to Verify CheckOut Of Free Text Item
        public void P2PWebShop_VerifyCheckoutFreeTextItem(string freeTextItemDescription)
        {
            //Wait for WebShop Purchase Requisition Info Filter Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_InfoFilterButton.Wait.ForExists(Globals.timeOut);

            //Click on WebShop Purchase Requisition Info Filter Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_InfoFilterButton.User.Click();

            //Calling Busy Indicator
            P2PNavigation.CallBusyIndicator();

            //Use FrameworkElement and read all the TextBlock in a fe
            FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_RadGridViewcontrol;

            //Check the free Text Item Description
            bool search = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(freeTextItemDescription));

            //Compare Each Row Value
            if (search == true)
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information + " Free Text Item Checked Out. " + freeTextItemDescription + " Verification Passed!");
            }

            else
            {
                //Write the log if Verification Fail
                Manager.Current.Log.WriteLine(LogType.Error + " No Free Text Item Checked Out. " + freeTextItemDescription + " Verification Failed!");
            }
        }

        //Verify that if User is On Comparison Page
        public void P2PWebShop_VerifyRemoveProductOnComparisonPage(string verifyItemAfterDeletion)
        {
            //Get the String Compare
            string compareText = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new XamlFindExpression("TextContent=Compare")).TextLiteralContent;
            //Get the No of Items
            string noOfItems = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new XamlFindExpression("TextContent=~Items")).TextLiteralContent;

            //Concatenate String and No of Items
            string verifyProducts = compareText + noOfItems;

            //bool search = SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_ProductSearch_VerticalMenuItem.IsEnabled;

            try
            {
                //Check the Condition if Both Matches
                if ((verifyProducts == verifyItemAfterDeletion) && (SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_ProductSearch_VerticalMenuItem.IsEnabled == true))
                {

                    //Logs if Verification Pass
                    Manager.Current.Log.WriteLine(LogType.Information + ": Expected Result: " + verifyProducts + " - Product is removed from Comparison Page & User is not redirect to Search Page. Verification Pass!!");

                }
                else
                {
                    //Logs if Verification Failed
                    throw new Exception(LogType.Information + ": " + verifyProducts + ": Some error Occurred. Either non of the products are removed from Comparison Page OR User is redirected to Search Page. Verfication Failed!");
                }
            }
            finally
            {
                //Calling Busy Indicator
                P2PNavigation.CallBusyIndicator();
            }
        }

        //Verify that if User is On Search Page
        public void P2PWebShop_VerifyNavigateToSearchPage(string automationID)
        {
            //Check the Condition if User is On Search Page
            if ((SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_ProductSearch_VerticalMenuItem.IsChecked == true) &&
                (SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(automationID)).IsVisible == true))
            {
                //Logs if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, ": Expected - Product is removed from Comparison Page & User is redirected to Search Page. Verification Pass!");
            }
            else
            {
                //Logs if Verification Failed
                Manager.Current.Log.WriteLine(LogType.Error, "User is not redirected back to Search Page. Verificaiton Failed!");
            }

            //Click on Close Button
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(automationID)).User.Click();
        }

        //Verify Products are Displaying in the Grid
        public void P2PWebShop_VerifyProductList()
        {
            //Calling Busy Indicator
            P2PNavigation.CallBusyIndicator();

            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Product_GridViewControl.Wait.ForExists(Globals.timeOut);

            //Copy the RadGridView into a local variable
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Product_GridViewControl;

            //Check count of RadGridView
            if (grid.Rows.Count == 0)
            {
                //Write the log if Verification Fail
                Manager.Current.Log.WriteLine(LogType.Error, ": There are No Products in the Grid. Verification Failed!");
            }
            else
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, ": Products are listed in the Grid. Verification Passed!");
            }
        }

        //Verify Products are Displaying in the Grid
        public void P2PWebShop_VerifyProductCategory(string productCategory)
        {
            //Wait for Grid Control Exists in DOMl
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Product_GridViewControl.Wait.ForExists(Globals.timeOut);

            //Wait for Tab Control
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=tabControl")).Wait.ForExists(Globals.timeOut);

            //Click on P2P_WebShop_Search_Category Button
            //SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_CategoryButton.User.Click();
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=tabControl")).Find.ByTextContent(productCategory).User.Click();

            //Copy the RadGridView into a local variable
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Product_GridViewControl;

            //Grab the VirtualizingPanel contained in the RadGridView. This is used to control the viewable portion of the grid.
            FrameworkElement VirtualizingPanel = grid.Find.ByType("GridViewVirtualizingPanel");

            //Getting the Count of the list of products in grid to a locally declared variable
            int gridProductList = VirtualizingPanel.Children.Count;

            //Wait for Rad Tree View Exists in DOM
            //SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_ProductCategoriesRadTreeView.Wait.ForExists(Globals.timeOut);

            //Grab the RadTreeView in local variable rtv
            //RadTreeView rtv = SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_ProductCategoriesRadTreeView;
            ArtOfTest.WebAii.Silverlight.UI.TabControl tbc = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=tabControl")).CastAs<ArtOfTest.WebAii.Silverlight.UI.TabControl>();

            //Finding productCategory in rtv and casting & saving result in Textblock.
            //TextBlock productCategoryTextBlock = rtv.Find.ByTextContent(productCategory).CastAs<TextBlock>();
            TextBlock productCategoryTextBlock = tbc.Find.ByTextContent(productCategory).CastAs<TextBlock>();

            //Fetching text from productCategoryTextBlock into locally decalred variable
            string productCategoryString = productCategoryTextBlock.Text.ToString();

            //Store String Start index in a variable "strproductCategoryStartIndex"
            int strproductCategoryStartIndex = productCategoryString.IndexOf("(");

            //Store String Last index in a variable "strproductCategoryEndIndex"
            int strproductCategoryEndIndex = productCategoryString.IndexOf(")");

            //Store Sub String into a variable "productCategoryExpectedString"
            string productCategoryExpectedString = productCategoryString.Substring(strproductCategoryStartIndex + 1, strproductCategoryEndIndex - strproductCategoryStartIndex - 1);

            //Comparing both values productCategoryResultString & gridProductList
            if (gridProductList.ToString() == productCategoryExpectedString)
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, ": Selected Product Category items are '" + productCategoryExpectedString + "'. And Expected number of Products in Grid are '" + gridProductList + "'. Both values are same. Verification Passed!");
            }
            else
            {
                //Write the log if Verification fails
                Manager.Current.Log.WriteLine(LogType.Error, ": Selected Product Category items are '" + productCategoryExpectedString + "'. And Actual number of products in Grid are '" + gridProductList + "'. Values are not same. Verification Failed!");
            }
        }

        //Verify Toolbar Action Buttons Availability
        public void P2PWebShop_VerifyToolBarActionButtons(string verificationCase)
        {
            switch (verificationCase)
            {
                case "DRAFT":
                    {
                        //Wait for button to load in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_SendToProcessButton.Wait.ForExists(Globals.timeOut);

                        //Wait for button to load in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_SaveButton.Wait.ForExists(Globals.timeOut);

                        //Wait for Dropdown to load in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

                        //Set Focus on Dropdown
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.SetFocus();

                        //Click on Dropdown
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

                        //Wait for button to load in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskPutOnHoldButton.Wait.ForExists(Globals.timeOut);

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_SendToProcessButton.IsVisible.Equals(true) &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_SaveButton.IsVisible.Equals(true) &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_SaveButton.IsEnabled.Equals(false) &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.IsVisible.Equals(true) &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskPutOnHoldButton.IsVisible.Equals(true))
                        {
                            //Write the log if Verification Pass
                            Manager.Current.Log.WriteLine(LogType.Information + ": Send To Process Button, Savecas Draft Button, Additional Action Dropdown & Remove button are Visible and Save as draft Button is Disabled. Verification Passed!");
                        }

                        else
                        {
                            //Write the log if Verification Failed
                            Manager.Current.Log.WriteLine(LogType.Information + ": Send To Process Button, Save Button, Additional Action Dropdown & Remove button are not Visible or Save Button is not Disabled. Verification Failed!");

                            //Write the log if Verification fails
                            throw new Exception(LogType.Error + ": Some error occurred. Verification Failed!");
                        }
                        break;
                    }
                case "APPROVED":
                    {
                        //Wait for Dropdown to load in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

                        //Set Focus on Dropdown
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.SetFocus();

                        //Click on Dropdown
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

                        //Wait for button to load in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskPutOnHoldButton.Wait.ForExists(Globals.timeOut);

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.IsVisible.Equals(true) &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskPutOnHoldButton.IsVisible.Equals(true) &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskPutOnHoldButton.IsEnabled.Equals(true))
                        {
                            //Write the log if Verification Pass
                            Manager.Current.Log.WriteLine(LogType.Information + ": Additional Action Dropdown & Remove button are Visible and Remove Button is Enabled. Verification Passed!");
                        }

                        else
                        {
                            //Write the log if Verification Failed
                            Manager.Current.Log.WriteLine(LogType.Information + ": Additional Action Dropdown & Remove button are not Visible or Remove Button is Disabled. Verification Failed!");

                            //Write the log if Verification fails
                            throw new Exception(LogType.Error + ": Some error occurred. Verification Failed!");
                        }
                        break;
                    }
                case "CANCELED":
                    {
                        //Wait for Dropdown to load in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

                        //Set Focus on Dropdown
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.SetFocus();

                        //Click on Dropdown
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

                        //Wait for button to load in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskPutOnHoldButton.Wait.ForExists(Globals.timeOut);

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.IsVisible.Equals(true) &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskPutOnHoldButton.IsVisible.Equals(true) &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_TaskPutOnHoldButton.IsEnabled.Equals(false))
                        {
                            //Write the log if Verification Pass
                            Manager.Current.Log.WriteLine(LogType.Information + ": Additional Action Dropdown & Remove button are Visible and Remove Button is Disabled. Verification Passed!");
                        }

                        else
                        {
                            //Write the log if Verification Failed
                            Manager.Current.Log.WriteLine(LogType.Information + ": Additional Action Dropdown & Remove button are not Visible or Remove Button is not Disabled. Verification Failed!");

                            //Write the log if Verification fails
                            throw new Exception(LogType.Error + ": Some error occurred. Verification Failed!");
                        }
                        break;
                    }
                case "SUPPLIER CONFIRMED":
                    {
                        //Wait for Dropdown to load in Dom
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_MoreActionsRaddropdownbutton.Wait.ForExists(Globals.timeOut);

                        //Set Focus on Dropdown
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_MoreActionsRaddropdownbutton.SetFocus();

                        //click on DropDown
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_MoreActionsRaddropdownbutton.User.Click();

                        //Wait for Check Button to load in Dom
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_CheckButton.Wait.ForExists(Globals.timeOut);

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_CheckButton.IsEnabled.Equals(true) &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_MoreActionsRaddropdownbutton.IsEnabled.Equals(true))
                        {
                            //Write the log if Verification Pass
                            Manager.Current.Log.WriteLine(LogType.Information + ": Check and More Action Dropdown buttons are Enabled. Verification Passed!");
                        }

                        else
                        {
                            //Write the log if Verification Failed
                            Manager.Current.Log.WriteLine(LogType.Information + ": Check and More Action Dropdown buttons are Disabled. Verification Failed!");

                            //Write the log if Verification fails
                            throw new Exception(LogType.Error + ": Some error occurred. Verification Failed!");
                        }
                        break;
                    }
                case "SUPPLIER REJECTED":
                    {
                        //Wait for Dropdown to load in Dom
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_SupplierRejectRadDropdownButton.Wait.ForExists(Globals.timeOut);

                        //Set Focus on Dropdown
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_SupplierRejectRadDropdownButton.SetFocus();

                        //click on DropDown
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_SupplierRejectRadDropdownButton.User.Click();

                        //Wait for Check Button to load in Dom
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_RemoveButton.Wait.ForExists(Globals.timeOut);

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_RemoveButton.IsEnabled.Equals(true) &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_SupplierRejectRadDropdownButton.IsEnabled.Equals(true))
                        {
                            //Write the log if Verification Pass
                            Manager.Current.Log.WriteLine(LogType.Information + ": Remove and More Action Dropdown buttons are Enabled. Verification Passed!");
                        }

                        else
                        {
                            //Write the log if Verification Failed
                            Manager.Current.Log.WriteLine(LogType.Information + ": Remove and More Action Dropdown buttons are Disabled. Verification Failed!");

                            //Write the log if Verification fails
                            throw new Exception(LogType.Error + ": Some error occurred. Verification Failed!");
                        }
                        break;
                    }
                case "MY TASKS REVIEW":
                    {
                        //Wait for Button to load in Dom
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_ReviewUpButton.Wait.ForExists(Globals.timeOut);

                        //Wait for Button to load in Dom
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_ReviewDownButton.Wait.ForExists(Globals.timeOut);

                        //Wait for Dropdown to load in Dom
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

                        //Wait for Button to load in Dom
                        //SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_SaveButton.Wait.ForExists(Globals.timeOut);

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_ReviewUpButton.IsVisible.Equals(true) &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_ReviewDownButton.IsVisible.Equals(true) &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.IsVisible.Equals(true)/* &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_SaveButton.IsVisible.Equals(true) &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_SaveButton.IsEnabled.Equals(false)*/)
                        {
                            //Write the log if Verification Pass
                            Manager.Current.Log.WriteLine(LogType.Information + ": Review Up, Review Down, Additional Action, Save buttons are visible. Save button is Disabled. Verification Passed!");
                        }

                        else
                        {
                            //Write the log if Verification Failed
                            Manager.Current.Log.WriteLine(LogType.Information + ": Review Up, Review Down, Additional Action, Save buttons are not visible. Save button is not Disabled. Verification Failed!");

                            //Write the log if Verification fails
                            throw new Exception(LogType.Error + ": Some error occurred. Verification Failed!");
                        }
                        break;
                    }
                case "MY TASKS APPROVE":
                    {
                        //Wait for Button to load in Dom
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_ApproveToolbarButton.Wait.ForExists(Globals.timeOut);

                        //Wait for Button to load in Dom
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_RejectToolbarButton.Wait.ForExists(Globals.timeOut);

                        //Wait for Dropdown to load in Dom
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

                        //Set focus on Dropdown
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.SetFocus();

                        //Click on Dropdown
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

                        //Wait for Button to load in Dom
                        //SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_SaveButton.Wait.ForExists(Globals.timeOut);

                        //Wait for RadContextMenu in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Requisition_MoreAction_RadContextMenu.Wait.ForExists(Globals.timeOut);



                        if (SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_ApproveToolbarButton.IsVisible.Equals(true) &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_RejectToolbarButton.IsVisible.Equals(true) &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.IsVisible.Equals(true) &&
                            /*SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_SaveButton.IsVisible.Equals(true) &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_SaveButton.IsEnabled.Equals(false) && */
                            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisitions_MoreActions_RemoveButton.IsVisible.Equals(true) &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisitions_MoreActions_RemoveButton.IsEnabled.Equals(false))
                        {
                            //Write the log if Verification Pass
                            Manager.Current.Log.WriteLine(LogType.Information + ": Approve, Reject, Additional Action, Save, Remove buttons are visible. Save & Remove buttons are Disabled. Verification Passed!");
                        }

                        else
                        {
                            //Write the log if Verification Failed
                            Manager.Current.Log.WriteLine(LogType.Information + ": Approve, Reject, Additional Action, Save, Remove buttons are not visible. Save & Remove buttons are not Disabled. Verification Failed!");

                            //Write the log if Verification fails
                            throw new Exception(LogType.Error + ": Some error occurred. Verification Failed!");
                        }
                        break;
                    }

                case "SUPPLIER CONFIRMED ACTION":
                    {
                        //Wait for Dropdown to load in Dom
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_MoreActionsRaddropdownbutton.Wait.ForExists(Globals.timeOut);

                        //Wait for Check Button to load in Dom
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_CheckButton.Wait.ForExists(Globals.timeOut);

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_CheckButton.IsEnabled.Equals(false) &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_MoreActionsRaddropdownbutton.IsEnabled.Equals(false))
                        {
                            //Write the log if Verification Pass
                            Manager.Current.Log.WriteLine(LogType.Information + ": Check and More Action Dropdown buttons are Disabled. Verification Passed!");
                        }

                        else
                        {
                            //Write the log if Verification Failed
                            Manager.Current.Log.WriteLine(LogType.Information + ": Check and More Action Dropdown buttons are Enabled. Verification Failed!");

                            //Write the log if Verification fails
                            throw new Exception(LogType.Error + ": Some error occurred. Verification Failed!");
                        }
                        break;
                    }
                case "SUPPLIER REJECTED ACTION":
                    {
                        //Wait for Dropdown to load in Dom
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_SupplierRejectRadDropdownButton.Wait.ForExists(Globals.timeOut);

                        //Wait for Check Button to load in Dom
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_RemoveButton.Wait.ForExists(Globals.timeOut);

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_RemoveButton.IsEnabled.Equals(false) &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_SupplierRejectRadDropdownButton.IsEnabled.Equals(false))
                        {
                            //Write the log if Verification Pass
                            Manager.Current.Log.WriteLine(LogType.Information + ": Remove and More Action Dropdown buttons are Disabled. Verification Passed!");
                        }

                        else
                        {
                            //Write the log if Verification Failed
                            Manager.Current.Log.WriteLine(LogType.Information + ": Remove and More Action Dropdown buttons are Enabled. Verification Failed!");

                            //Write the log if Verification fails
                            throw new Exception(LogType.Error + ": Some error occurred. Verification Failed!");
                        }
                        break;
                    }
                default:
                    {
                        //Log error if no case found.
                        Manager.Current.Log.WriteLine(LogType.Error, "  No Case Found. Please Check!!");

                        //Write the log if no case found.
                        throw new Exception(LogType.Error + ": No Case Found. Please Check!!");
                    }
            }
        }

        //Verify Add/Delete Buttons on Lines Tab
        public void P2PWebShop_VerifyAddDeleteLineButtons(string buttonVerification, string lineStatus = null, string prLineStatus = null)
        {
            switch (buttonVerification)
            {
                case "Verify_Buttons":
                    {
                        //Wait for Add Button to Exists in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_AddLineButton.Wait.ForExists(Globals.timeOut);

                        //Wait for Delete Button to Exists in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Image_DeleteButton.Wait.ForExists(Globals.timeOut);

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Image_DeleteButton.IsVisible.Equals(true) &&
                            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_AddLineButton.IsVisible.Equals(true))
                        {
                            //Write the log if Verification Pass
                            Manager.Current.Log.WriteLine(LogType.Information + ": Add and Delete buttons are Available. Verification Passed!");
                        }
                        else
                        {
                            //Write the log if Verification Fail
                            Manager.Current.Log.WriteLine(LogType.Information + ": Add and Delete buttons are not Available. Verification Failed!");

                            //Write the log if Verification Fail
                            throw new Exception(LogType.Error + ": Add and Delete buttons are not Available. Verification Failed!");
                        }
                        break;
                    }

                case "Verify_Delete_Button":
                    {
                        //Wait for Grid View to load in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_LinesListViewGridControl.Wait.ForExists(Globals.timeOut);

                        //Wait for Delete Button to Exists in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Image_DeleteButton.Wait.ForExists(Globals.timeOut);

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_LinesListViewGridControl.Rows.Count.Equals(1))
                        {
                            if (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Image_DeleteButton.IsEnabled.Equals(false))
                            {
                                //Write the log if Verification Pass
                                Manager.Current.Log.WriteLine(LogType.Information + ": Delete buttons is Disabled. Verification Passed!");
                            }
                            else
                            {
                                //Write the log if Verification Fail
                                Manager.Current.Log.WriteLine(LogType.Information + ": Delete buttons is not Disabled. Verification Failed!");

                                //Write the log if Verification Fail
                                throw new Exception(LogType.Error + ": Delete buttons is not Disabled. Verification Failed!");
                            }
                        }

                        else
                        {
                            //Write the log if Verification Fail
                            Manager.Current.Log.WriteLine(LogType.Information + ": There are more than 1 Rows. Verification Failed!");

                            //Write the log if Verification Fail
                            throw new Exception(LogType.Error + ": There are more than 1 Rows. Verification Failed!");
                        }
                        break;
                    }

                case "Verify_Add_Delete_Buttons_InConfirmation_Status":
                    {
                        //Use FrameworkElement and read all the TextBlock in a fe
                        FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_LinesListViewGridControl;

                        //Saving Purchase Requisition Status in boolean Variable
                        bool selectedPRLineStatusApproved = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(lineStatus));

                        //Refresh Grid
                        fe.Refresh();

                        //Saving Purchase Requisition Status in boolean Variable
                        bool selectedPRLineStatusRejected = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(prLineStatus));

                        if (selectedPRLineStatusApproved.Equals(true) && selectedPRLineStatusRejected.Equals(true))
                        {
                            //Write the log if Verification Pass
                            Manager.Current.Log.WriteLine(LogType.Information + ": Add and Delete buttons are not Visible. Verification Passed!");
                        }
                        else
                        {
                            //Write the log if Verification Fail
                            Manager.Current.Log.WriteLine(LogType.Information + ": Add and Delete buttons are Visible. Verification Failed!");

                            //Write the log if Verification Fail
                            throw new Exception(LogType.Error + ": Verification Failed!");
                        }
                        break;
                    }

                case "Verify_Add_Delete_Buttons_Final_Approval_Status":
                    {
                        //Use FrameworkElement and read all the TextBlock in a fe
                        FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_LinesListViewGridControl;

                        //Saving Purchase Requisition Status in boolean Variable
                        bool selectedPRLineStatusApproved = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(lineStatus));

                        if (selectedPRLineStatusApproved.Equals(true))
                        {
                            //Write the log if Verification Pass
                            Manager.Current.Log.WriteLine(LogType.Information + ": Add and Delete buttons are not Visible. Verification Passed!");
                        }
                        else
                        {
                            //Write the log if Verification Fail
                            Manager.Current.Log.WriteLine(LogType.Information + ": Add and Delete buttons are Visible. Verification Failed!");

                            //Write the log if Verification Fail
                            throw new Exception(LogType.Error + ": Verification Failed!");
                        }
                        break;
                    }

                default:
                    {
                        //Log error if filter not found on page.
                        Manager.Current.Log.WriteLine(LogType.Error, "  Buttons not available on Page. Please Check!!");

                        break;
                    }
            }
        }

        //Verify Detail View of Selected Product
        public void P2PWebShop_VerifyProductDetailView(double defaultQuantity, string verificationProductDetailViewCase, string productName = null, string productDescription = null, string productCategory = null)
        {
            //Declaring String
            double actualQuantity;
            string actualProductName;
            string actualProductDescription;
            string actualProductCategory;

            switch (verificationProductDetailViewCase)
            {
                case "Detail_Veiw_Verification":
                    {
                        //Waiting for Textbox to load in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_Product_QuantityTextbox.Wait.ForExists(Globals.timeOut);

                        //Waiting for Textbox to load in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Purchase_Name_HeaderDataTextArea.Wait.ForExists(Globals.timeOut);

                        //Waiting for Textbox to load in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Purchase_ShortDescription_HeaderDataTextArea.Wait.ForExists(Globals.timeOut);

                        //Waiting for Textbox to load in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Purchase_ProductCategory_HeaderDataTextBox.Wait.ForExists(Globals.timeOut);

                        //SetFocus on TextBox
                        SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_Product_QuantityTextbox.SetFocus();

                        //Saving the value of the Text box in a String
                        actualQuantity = Convert.ToDouble(SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_Product_QuantityTextbox.Text);

                        //Saving the value of the Text box in a String
                        actualProductName = SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Purchase_Name_HeaderDataTextArea.Text.ToString();

                        //Saving the value of the Text box in a String
                        actualProductDescription = SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Purchase_ShortDescription_HeaderDataTextArea.Text.ToString();

                        //Saving the value of the Text box in a String
                        actualProductCategory = SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Purchase_ProductCategory_HeaderDataTextBox.Text.ToString();

                        //If actualQuantity and defaultQuantity is same then below Block will execute
                        if (actualQuantity.ToString(CultureInfo.CurrentCulture.NumberFormat) == defaultQuantity.ToString(CultureInfo.CurrentCulture.NumberFormat))
                        {
                            //Write the log if Verification Passed
                            Manager.Current.Log.WriteLine(LogType.Information + ": Default quantity(" + defaultQuantity + ") is equal to Actual Quantity(" + actualQuantity + "). Verification Passed!");
                        }
                        else
                        {
                            //Write the log if Verification Fails
                            Manager.Current.Log.WriteLine(LogType.Error + ": Default quantity(" + defaultQuantity + ") is not equal to Actual Quantity(" + actualQuantity + "). Verification Failed!");
                            throw new Exception(LogType.Error + ": Verification Failed!");
                        }

                        //If actualProductName and productName is same then below Block will execute
                        if (actualProductName.Contains(productName))
                        {
                            //Write the log if Verification Passed
                            Manager.Current.Log.WriteLine(LogType.Information + ": Product Name(" + productName + ") is matching with Actual Product Name(" + actualProductName + "). Verification Passed!");
                        }
                        else
                        {
                            //Write the log if Verification Fails
                            Manager.Current.Log.WriteLine(LogType.Error + ": Product Name(" + productName + ") is not matching with Actual Product Name(" + actualProductName + "). Verification Failed!");
                            throw new Exception(LogType.Error + ": Verification Failed!");
                        }

                        //If actualProductDescription and productDescription is same then below Block will execute
                        if (actualProductDescription.Contains(productDescription))
                        {
                            //Write the log if Verification Passed
                            Manager.Current.Log.WriteLine(LogType.Information + ": Product Description(" + productDescription + ") is matching with Actual Product Description(" + actualProductDescription + "). Verification Passed!");
                        }
                        else
                        {
                            //Write the log if Verification Fails
                            Manager.Current.Log.WriteLine(LogType.Error + ": Product Description(" + productDescription + ") is not matching with Actual Product Description(" + actualProductDescription + "). Verification Failed!");
                            throw new Exception(LogType.Error + ": Verification Failed!");
                        }

                        //If actualProductCategory and productCategory is same then below Block will execute
                        if (actualProductCategory.Contains(productCategory))
                        {
                            //Write the log if Verification Passed
                            Manager.Current.Log.WriteLine(LogType.Information + ": Product Category(" + productCategory + ") is matching with Actual Product Category(" + actualProductCategory + "). Verification Passed!");
                        }
                        else
                        {
                            //Write the log if Verification Fails
                            Manager.Current.Log.WriteLine(LogType.Error + ": Product Category(" + productCategory + ") is not matching with Actual Product Category(" + actualProductCategory + "). Verification Failed!");
                            throw new Exception(LogType.Error + ": Verification Failed!");
                        }
                        break;
                    }

                case "Quantity_Verification":
                    {
                        //Wait for Textbox to load in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_Product_QuantityTextbox.Wait.ForExists(Globals.timeOut);

                        //SetFocus on TextBox
                        SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_Product_QuantityTextbox.SetFocus();

                        //Saving the value of the Text box in a String
                        actualQuantity = Convert.ToDouble(SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_Product_QuantityTextbox.Text);

                        //If actualQuantity and defaultQuantity is same then below Block will execute
                        if (actualQuantity.ToString(CultureInfo.CurrentCulture.NumberFormat) == defaultQuantity.ToString(CultureInfo.CurrentCulture.NumberFormat))
                        {
                            //Write the log if Verification Passed
                            Manager.Current.Log.WriteLine(LogType.Information + ": Default quantity(" + defaultQuantity + ") is equal to Actual Quantity(" + actualQuantity + "). Verification Passed!");
                        }
                        else
                        {
                            //Write the log if Verification Fails
                            Manager.Current.Log.WriteLine(LogType.Error + ": Default quantity(" + defaultQuantity + ") is not equal to Actual Quantity(" + actualQuantity + "). Verification Failed!");
                            throw new Exception(LogType.Error + ": Verification Failed!");
                        }

                        if (SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_ProductAttachmentsTabItem.IsEnabled.Equals(false))
                        {
                            //Write the log if quantity is not between 1 to 10
                            Manager.Current.Log.WriteLine(LogType.Information + ": Quantity should be between 1 to 10. Inputed quantity is " + actualQuantity + ".");
                        }

                        else
                        {
                            //Write the log if quantity is between 1 to 10
                            Manager.Current.Log.WriteLine(LogType.Information + ": Quantity lies between 1 to 10. Inputed quantity is " + actualQuantity + ".");
                        }

                        break;
                    }

                default:
                    {
                        //Log error if no Case found.
                        Manager.Current.Log.WriteLine(LogType.Error, ": Something went wrong. Please Check!!");

                        break;
                    }
            }
        }

        //Method to Verify WebShop Purchase Requisition Status
        public void P2PWebShop_VerifyPurchaseRequisitionStatus(string[] verifyRequisitionStatus, int arrayIndex, string rejectPurchaseRequistion = null)
        {
            //Declaring integer
            int indexStatus = 0;

            //Declaring statusflag
            bool statusFlag = true;

            //Declare an array of textblocks
            TextBlock[] status = new TextBlock[arrayIndex];

            //Wait for the Grid View Control
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_RadGridViewcontrol.Wait.ForExists(Globals.timeOut);

            //Set Focus on Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_RadGridViewcontrol.SetFocus();

            //get all Status as text blocks and verify
            foreach (string prStatus in verifyRequisitionStatus)
            {
                if (rejectPurchaseRequistion == null)
                {
                    //Store Status text block
                    status[indexStatus] = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(prStatus).As<TextBlock>();

                    //Check whether any TextBlock contains the specified string.
                    bool found = SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_RadGridViewcontrol.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(prStatus));

                    if (found)
                    {
                        //Log the Results if correct Status exists
                        Manager.Current.Log.WriteLine(LogType.Information, " Result Found in Grid with Status '" + prStatus + "'. Verification Passed!!");
                    }
                    else
                    {
                        //Log the Results if Status does not exists
                        Manager.Current.Log.WriteLine(LogType.Information, " Result Found in Grid with Status '" + prStatus + "'. Verification Failed!!");

                        //Set flag if data mismatches
                        statusFlag = false;
                    }

                    //increment index
                    indexStatus++;

                    if (statusFlag == false)
                    {
                        //Log the Results if Status does not exists
                        Manager.Current.Log.WriteLine(LogType.Information, " Result Found in Grid with Status '" + prStatus + "'. Verification Failed!!");

                        //Write the log if Verification Fail
                        throw new Exception(LogType.Error + " - Grid Not Found. Verification Failed!!");
                    }
                }

                else
                {
                    //Use FrameworkElement and read all the TextBlock in a fe
                    FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_RadGridViewcontrol;

                    //Saving Purchase Requisition Status in boolean Variable
                    bool purchaseRequisitionStatus = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(prStatus));

                    if (purchaseRequisitionStatus)
                    {
                        //Log the Results if Status does not exists
                        Manager.Current.Log.WriteLine(LogType.Information, prStatus + " Rejected Requisition Found in Grid. Verification Failed!!");

                        //Write the log if Verification Fail
                        throw new Exception(LogType.Error + " - Verification Failed!!");
                    }
                    else
                    {
                        //Log the Results if Verification Passed
                        Manager.Current.Log.WriteLine(LogType.Information, prStatus + " Rejected Requisition Not Found in Grid[Return" + purchaseRequisitionStatus + "]. Verification Passed!!");
                    }
                }
            }
        }

        //Method to Verify My Purchase Requisition Page
        public void P2PWebShop_VerifyPurchaseRequisitionPage()
        {
            //Wait for Vertical Menu to load in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_VerticalMenuItem.Wait.ForExists(Globals.timeOut);

            if (SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_VerticalMenuItem.IsChecked == true)
            {
                //Write the log if Verification Passed
                Manager.Current.Log.WriteLine(LogType.Information + ": User is navigated to Shop My Purchase Requisition Page Successfully. Verification Passed!");
            }
            else
            {
                //Write the log if Verification Fails
                Manager.Current.Log.WriteLine(LogType.Error + ": User is not navigated to Shop My Purchase Requisition Page. Verification Failed!");
                throw new Exception(LogType.Error + ": Verification Failed!");
            }
        }

        //Method to Verify Purchase Requisition Status
        public void P2PWebShop_VerifyPurchaseRequisitionStatus(string draftStatus)
        {
            //Wait for TextBox to load in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_MyPurchaseRequisition_Status_TextBox.Wait.ForExists(Globals.timeOut);

            //Declare String variable
            string prStatus = SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_MyPurchaseRequisition_Status_TextBox.Text;

            //Wait for Header Data to load in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_HeaderDataTabItem.Wait.ForExists(Globals.timeOut);

            //Click on Header Data
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_HeaderDataTabItem.User.Click();

            if (prStatus == draftStatus)
            {
                //Write the log if Verification Passed
                Manager.Current.Log.WriteLine(LogType.Information + ": Purchase Requisition Status is " + prStatus + ". Verification Passed!");
            }
            else
            {
                //Write the log if Verification Fails
                Manager.Current.Log.WriteLine(LogType.Error + ": Purchase Requisition Status is " + prStatus + ". Verification Failed!");
                throw new Exception(LogType.Error + ": Verification Failed!");
            }
        }

        //Method to Verify PR for Rejected Line
        public void P2PMyTasks_VerifyPurchaseRequisitionLineStatus(string prLineStatus, string firstProduct, string secondProduct)
        {
            // Wait for List View Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_LinesListViewGridControl.Wait.ForExists(Globals.timeOut);

            //Getting count of Variable
            int rowCount = SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_LinesListViewGridControl.Rows.Count();

            //Put the Grid View Control into a FrameworkElement fe.
            FrameworkElement fe = SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_LinesListViewGridControl;

            //Check whether Status in the Grid View Control
            bool foundStatus = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(prLineStatus));

            //Refresh Grid
            fe.Refresh();

            //Check whether First Product in the Grid View Control
            bool foundFirstProduct = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(firstProduct));

            //Refresh Grid
            fe.Refresh();

            //Check whether First Product in the Grid View Control
            bool foundSecondProduct = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(secondProduct));

            if (foundStatus == true && foundFirstProduct == true && foundSecondProduct == true && rowCount == 2)
            {
                //Log the result if Verification Passes
                Manager.Current.Log.WriteLine("Lines Status is " + prLineStatus + ". Verification Passed!!");
                Manager.Current.Log.WriteLine("Number of PR Lines generated are: " + rowCount + " with Status is " + prLineStatus + ". Verification Passed!!");
                Manager.Current.Log.WriteLine("Lines generated for Products " + firstProduct + " & " + secondProduct + ". Verification Passed!!");
            }

            else
            {
                //Log the result if Verification Fails
                Manager.Current.Log.WriteLine("Actual count of PR Lines: " + rowCount + ". Actual status of PR Lines: " + prLineStatus + ". Expected count of PR Lines :2. Expected status of PR Lines: draft. Expected products :" + firstProduct + " and " + secondProduct + ". Verification Failed!!");

                //If any exception occurrs or verification fails
                throw new Exception(LogType.Error + "Verification Failed!!.");
            }
        }

        //Method to Verify Buttons State for Item Comparison 
        public void P2PWebShop_VerifyButtonState(string freeTextItem, string state)
        {
            //Wait for Button Exists in DOM           
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(freeTextItem)).Wait.ForExists(Globals.timeOut);

            //Check if Verification is Passed            
            if (SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(freeTextItem)).TextBlockContent == state)
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, "Button State is Changed. Verification Passed!");
            }
            else
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Error, "Current Button State is:" + SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(freeTextItem)).TextBlockContent);
            }
        }

        //Method to Verify Purchasing Category data
        public void P2PWebShop_VerifyPurchaseCategoryData(string purchasingCategory, string purchaseCategoryTextBoxID, string purchasingCategoryData)
        {
            ArtOfTest.WebAii.Silverlight.UI.TextBox purchaseCategoryTextbox = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(purchaseCategoryTextBoxID)).As<ArtOfTest.WebAii.Silverlight.UI.TextBox>();

            //Wait for TextBox Exists in DOM                     
            purchaseCategoryTextbox.Wait.ForExists(Globals.timeOut);

            //Focus on Purchasing Category TextBox
            purchaseCategoryTextbox.SetFocus();

            //Select the String in Text Box
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

            //Generate a Keyboard Event to clear search
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

            //Input data in Header Data Purchasing Category TextBox
            purchaseCategoryTextbox.User.TypeText(purchasingCategory, 50);

            //Performing keyboard action by pressing Tab key
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Check whether Data is correct
            bool purchaseCategoryTextboxVerification = purchaseCategoryTextbox.Text.ToString().Equals(purchasingCategoryData);

            if (purchaseCategoryTextboxVerification == true)
            {
                //Write the log if Verification Passed
                Manager.Current.Log.WriteLine(LogType.Information, " Purchase Category Data is correct '" + purchaseCategoryTextbox.Text.ToString() + "'. Verification Passed!");
            }
            else
            {
                //Write the log if Verification Fails
                Manager.Current.Log.WriteLine(LogType.Error, "Purchase Category Data is not correct '" + purchaseCategoryTextbox.Text.ToString() + "'. Verification Failed!");
                throw new Exception("Verification Failed!");
            }
        }
    }
}