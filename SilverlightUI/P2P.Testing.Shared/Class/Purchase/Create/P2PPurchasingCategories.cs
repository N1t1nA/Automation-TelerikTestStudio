using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using ArtOfTest.Common.UnitTesting;
using Telerik.WebAii.Controls.Xaml;
using ArtOfTest.WebAii.Controls.Xaml.Wpf;
using P2P.Testing.Shared.Class;
using System.Windows.Forms;
using ArtOfTest.WebAii.Win32;

namespace P2P.Testing.Shared.Class.Purchase.Create
{
    public class P2PPurchasingCategories: BaseWebAiiTest
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

        //Method to Select one time in Tree view
        public void P2PSelectPurchasingCategory(string purchaseCategory)
        {
            //Wait for  Webshop Product Category to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchasingCategory_CategoryRadTreeView.Wait.ForExists(Globals.timeOut);

            //Copy the RadTreeView into a local variable
            RadTreeView treeCategory = SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchasingCategory_CategoryRadTreeView;

            //Find Product Category in tree and store in Textblock
            TextBlock pruchasingCategoryTextBlock = treeCategory.Find.ByTextContent(purchaseCategory).CastAs<TextBlock>();

            //Click on productCategoryTextBlock
            pruchasingCategoryTextBlock.User.Click();
        }

        //Method to check/uncheck Always Valid checkbox
        public void P2PCheckUncheckAlwaysValidCheckbox(bool stateOfCheckbox)
        {
            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem1.Wait.ForExists(Globals.timeOut);

            //Click on element
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem1.User.Click();

            //Wait for checkbox to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_AlwaysValidCheckBox.Wait.ForExists(Globals.timeOut);

            //Check Always Valid checkbox
            if (stateOfCheckbox.Equals(true))
            {
                if (SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_AlwaysValidCheckBox.IsChecked.Equals(false))
                {
                    // Click the Check Box to select it
                    SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_AlwaysValidCheckBox.User.Click();
                }
            }
            //Uncheck Always Valid checkbox
            else
            {
                if (SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_AlwaysValidCheckBox.IsChecked.Equals(true))
                {
                    // Click the Check Box to uncheck it
                    SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_AlwaysValidCheckBox.User.Click();
                }
            }
        }   

        //Method to Enter value in date fields: dateFrom and dateTo
        public void P2PEnterDateFields(string dateFrom, string dateTo)
        {
            //Wait for date textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseCategory_DateValidFrom_Textbox.Wait.ForExists(Globals.timeOut);

            //Set Focus
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseCategory_DateValidFrom_Textbox.SetFocus();

            //Press Ctrl A
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(ArtOfTest.WebAii.Win32.KeyBoard.KeysFromString("Ctrl+A"));

            //Generate a Keyboard Event to clear search
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

            //Enter Value in Date textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseCategory_DateValidFrom_Textbox.Text = dateFrom;

            //Press Tab
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            if (dateTo != null)
            {
                //Wait for date textbox Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseCategory_DateValidTo_Textbox.Wait.ForExists(Globals.timeOut);

                //Set Focus
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseCategory_DateValidTo_Textbox.SetFocus();

                //Enter Value in Date textbox
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseCategory_DateValidTo_Textbox.Text = dateTo;

                //Press Tab
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
            }
        }

        //Method to Create Data in Tab
        public void P2PPurchasingCategoryCreateTabData(string supplierComboboxValue, string supplierCode, string accountCode, string recipientName)
        {
            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem2.Wait.ForExists(Globals.timeOut);

            //Click on element
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem2.User.Click();

            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchasingCategory_SupplierComboBox.Wait.ForExists(Globals.timeOut);

            //Click on element
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchasingCategory_SupplierComboBox.OpenDropDown(true);

            //Select value from combobox
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchasingCategory_SupplierComboBox.SelectItemByText(true, supplierComboboxValue, true);

            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.Wait.ForExists(Globals.timeOut);

            //Click on element 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.User.Click();

            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_Create_AddRow_ItemsTextbox.Wait.ForExists(Globals.timeOut);

            //Enter supplier code
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_Create_AddRow_ItemsTextbox.User.TypeText(supplierCode, 50);

            //Press Tab
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem3.Wait.ForExists(Globals.timeOut);

            //Click on element
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem3.User.Click();

            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.Wait.ForExists(Globals.timeOut);

            //Click on element 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.User.Click();

            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_FreeTextItem_HeaderData_CurrencyCodeTextBox.Wait.ForExists(Globals.timeOut);

            //Enter accountCode 
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_Search_FreeTextItem_HeaderData_CurrencyCodeTextBox.User.TypeText(accountCode, 50);

            //Press Tab
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);

            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on element 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem4.Wait.ForExists(Globals.timeOut);

            //Click on element
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem4.User.Click();

            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.Wait.ForExists(Globals.timeOut);

            //Click on element 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.User.Click();

            //Press Tab
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_TaskRecipient_TextBox.Wait.ForExists(Globals.timeOut);

            //Enter recipientName 
            SharedElement.P2P_Application.SilverlightApp.P2P_TaskRecipient_TextBox.User.TypeText(recipientName, 50);

            //Press Tab
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Go back to first tab
            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem1.Wait.ForExists(Globals.timeOut);

            //Click on element
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem1.User.Click();
        }

        //Method to Create Data in Task Management Tab
        public void P2PPurchasingCategoryCreateTaskManagementData(string taskRecipients, string taskRecipientsValue)
        {
            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_TaskManagement_Radgridview.Wait.ForExists(Globals.timeOut);

            //Get Grid in RadGridView declared variable
            RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_TaskManagement_Radgridview;

            //If Add Row Button is available then if block will execute
            if (rgv.Rows.Count.Equals(0))
            {
                //Wait for element to Exists in DOM 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.Wait.ForExists(Globals.timeOut);

                //Click on AddRows button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.User.Click();
            }
            else
            {
                //Set Focus on grid
                rgv.SetFocus();

                //Press enter for add new row
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);
            }

            //Wait for grid to load
            rgv.Wait.ForExists(Globals.timeOut);

            //Refresh Grid
            rgv.Refresh();
            
            //Check value in Exact Column using Column Header Name Value
            GridViewHeaderCell header = rgv.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent.Contains(taskRecipients));

            //Count all rows and save it in index variable
            int index = header.Index; 

            //Use foreach loop for entering the Data in all Coding Rows
            foreach (GridViewRow row in rgv.Rows)
            {
                //wait for net Sum Cell                 
                row.Cells[index].Wait.ForExists(Globals.timeOut);

                //Left Double Click in net Sum Cell                 
                row.Cells[index].User.Click();

                //Select the String in Text Box
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

                //Generate a Keyboard Event to clear search
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

                //Enter the Data in Net Sum Cell
                row.Cells[index].User.TypeText(taskRecipientsValue.ToString(), 50);

                //Click Delete key
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
            }

            ArtOfTest.WebAii.Silverlight.UI.CheckBox checkBox = (ArtOfTest.WebAii.Silverlight.UI.CheckBox)rgv.Find.AllByType<ArtOfTest.WebAii.Silverlight.UI.CheckBox>().Cast<ArtOfTest.WebAii.Silverlight.UI.CheckBox>();

            checkBox.SetFocus();

            checkBox.Check(true);
        }
    }
}
