using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using System.Windows.Forms;
using Telerik.WebAii.Controls.Xaml;
using ArtOfTest.WebAii.Silverlight;

namespace P2P.Testing.Shared.Class.Purchase.Create
{
    public class P2PCreatePurchaseItems : BaseWebAiiTest
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
        
        //Method to Create New Purchase Item
        public void P2PCreateNewPurchaseItem(string purchaseItemType, string purchaseItemName, string companyName, string categoryName, string description, string validFlag ,string inheritFlag, string internalDesc = null, string keywords = null, string unsavedDataWarning = null)
        {
          
            //Wait for Create button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_CreateButton.Wait.ForExists(Globals.timeOut);

            //Click on create button
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_CreateButton.User.Click();

            //Wait for page to load            
            P2PNavigation.CallBusyIndicator();

            //Wait for Name Textbox to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_NameTextBox.Wait.ForExists(Globals.timeOut);

            //Enter the Data in Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_NameTextBox.User.TypeText(purchaseItemName, 50);

            //Wait for Organisation Textbox to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.Wait.ForExists(Globals.timeOut);

            //enter value in Organisation Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.User.TypeText(companyName, 50);

            //Press Tab
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Wait for Purchasing Category Textbox to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseItems_PurchasingCategoryTextbox.Wait.ForExists(Globals.timeOut);

            //enter value in Purchasing Category Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseItems_PurchasingCategoryTextbox.User.TypeText(categoryName, 50);

            //Press Tab
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Wait for checkbox to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_AlwaysValidCheckBox.Wait.ForExists(Globals.timeOut);

            if (validFlag.ToUpper() == "TRUE")
            {
                //Check if valid checkbox is not checked
                if (SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_AlwaysValidCheckBox.IsChecked.Equals(false))
                {
                    //Click on the Valid Checkbox to select it
                    SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_AlwaysValidCheckBox.User.Click();
                }
            }
            else
            {
                
                //Check if valid checkbox is checked
                if (SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_AlwaysValidCheckBox.IsChecked.Equals(true))
                {
                    //Uncheck the Valid Checkbox
                    SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_AlwaysValidCheckBox.User.Click();
                }

                //Wait for date textbox Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_DateValidFrom_Textbox.Wait.ForExists(Globals.timeOut);

                //Set Focus
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_DateValidFrom_Textbox.SetFocus();

                //Press Ctrl A
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(ArtOfTest.WebAii.Win32.KeyBoard.KeysFromString("Ctrl+A"));

                //Generate a Keyboard Event to clear search
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

                //Enter Value in Date textbox
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_DateValidFrom_Textbox.Text = System.DateTime.Today.ToShortDateString();

                //Press Tab
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

                //Wait for date textbox Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_DateValidUntil_Textbox.Wait.ForExists(Globals.timeOut);

                //Set Focus
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_DateValidUntil_Textbox.SetFocus();

                //Enter Value in Date textbox
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_DateValidUntil_Textbox.Text = System.DateTime.Today.AddDays(2).ToShortDateString();

                //Press Tab
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
            }

            //Wait for checkbox to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_InheritCheckBox.Wait.ForExists(Globals.timeOut);

            if (inheritFlag.ToUpper() == "TRUE")
            {
                //Check if Inherit checkbox is not checked
                if (SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_InheritCheckBox.IsChecked.Equals(false))
                {
                    //Click on the Inherit Checkbox to select it
                    SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_InheritCheckBox.User.Click();
                }
            }
            else
            {
                //Check if Inherit checkbox is checked
                if (SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_InheritCheckBox.IsChecked.Equals(true))
                {
                    //Uncheck the Inherit Checkbox
                    SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_InheritCheckBox.User.Click();
                }
            }

            //Wait for Description Text Box to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);

            //Enter the Data in Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(description, 50);

            if (internalDesc != "")
            {
                //Wait for Internal Description Text Box to Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseItems_InternalDescriptionTextBox.Wait.ForExists(Globals.timeOut);

                //Enter the Data in Textbox
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseItems_InternalDescriptionTextBox.User.TypeText(internalDesc, 50);
            }

            if (keywords != "")
            {
                //Wait for Keywords Text Box to Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseItems_KeywordsTextBox.Wait.ForExists(Globals.timeOut);

                //Enter the Data in Textbox
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseItems_KeywordsTextBox.User.TypeText(keywords, 50);
            }

            if (unsavedDataWarning == null)
            {
                //Wait for element to Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_SaveButton.Wait.ForExists(Globals.timeOut);

                //Click on save button
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_SaveButton.User.Click();
            }
        }

        //Method to Edit Purchase Item and check Mandatory fields
        public void P2PMandatoryFieldsPurchaseItem(string updatePurchaseItemName = null, string unsavedDataWarning = null)
        {
            //Wait for Name Textbox to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_NameTextBox.Wait.ForExists(Globals.timeOut);

            //Enter the Data in Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_NameTextBox.SetFocus();

            //Click inside textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_NameTextBox.User.Click();
            

            //Keyboard event to press 'Ctrl+A' key for selecting all text
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(ArtOfTest.WebAii.Win32.KeyBoard.KeysFromString("Ctrl+A"));

            //Generate a Keyboard Event to clear search
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

            if (updatePurchaseItemName != null)
            {
                //Input data in textbox
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_NameTextBox.User.TypeText(updatePurchaseItemName, 50);
            }

            if (unsavedDataWarning == null)
            {
                //Wait for element to Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_SaveButton.Wait.ForExists(Globals.timeOut);

                //Click on save button
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_SaveButton.User.Click();
            }
        }

        //Method to Edit Purchase Item 
        public void P2PEditPurchaseItem(string purchaseItemName)
        {
            //Wait for Name Textbox to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_NameTextBox.Wait.ForExists(Globals.timeOut);

            //Enter the Data in Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_NameTextBox.User.TypeText(purchaseItemName, 50);

            //Wait for Back Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_BackButton.Wait.ForExists(Globals.timeOut);

            //Click on Back Button to go Back to Transfer Page            
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_BackButton.User.Click();
        }       
    }



}
