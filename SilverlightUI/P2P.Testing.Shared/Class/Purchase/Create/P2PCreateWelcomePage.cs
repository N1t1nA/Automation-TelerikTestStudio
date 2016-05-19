using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using System.Windows.Forms;
using Telerik.WebAii.Controls.Xaml;
using ArtOfTest.WebAii.Silverlight;
using System.Globalization;


namespace P2P.Testing.Shared.Class.Purchase.Create
{
   
    public class P2PCreateWelcomePage : BaseWebAiiTest
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

        //Method to Create New Welcome Page
        public void P2PCreateNewWelcomePage(string pageName, string companyName, string description, string validFlag,string inheritFlag, string[] panelTitle, string[] panelComboBoxValue, int panelSize, string layoutComboBoxValue)
        {
        
            //Wait for Create button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_CreateButton.Wait.ForExists(Globals.timeOut);

            //Click on create button
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_CreateButton.User.Click();

            //Wait for page to load
            //System.Threading.Thread.Sleep(Globals.timeOut);
            P2PNavigation.CallBusyIndicator();

            //Wait for Page Name Textbox to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_NameTextBox.Wait.ForExists(Globals.timeOut);

            //Enter the Data in Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_NameTextBox.User.TypeText(pageName, 50);

            //Wait for Organisation Textbox to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_NameTextBox.Wait.ForExists(Globals.timeOut);

            //enter value in Organisation Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.User.TypeText(companyName, 50);

            //Press Tab
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

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
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_ShoppingBasket_SubmitPurchaseRequisition_DescriptionTextBox.Wait.ForExists(Globals.timeOut);

            //Enter the Data in Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_ShoppingBasket_SubmitPurchaseRequisition_DescriptionTextBox.User.TypeText(description, 50);

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

                //Enter Valid from and Valid until dates
                
                //Wait for date textbox Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_DateValidFrom_Textbox.Wait.ForExists(Globals.timeOut);

                //Set Focus
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_DateValidFrom_Textbox.SetFocus();

                //Press Ctrl A
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(ArtOfTest.WebAii.Win32.KeyBoard.KeysFromString("Ctrl+A"));

                //Generate a Keyboard Event to clear search
                Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);
                
                //Enter Value in Date textbox
                string validFrom = System.DateTime.Today.ToShortDateString().ToString(CultureInfo.InvariantCulture.NumberFormat);
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_DateValidFrom_Textbox.User.TypeText(validFrom, 50);
                
                //Press Tab
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

                //Wait for date textbox Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_DateValidUntil_Textbox.Wait.ForExists(Globals.timeOut);

                //Set Focus
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_DateValidUntil_Textbox.SetFocus();
               
                //Enter Value in Date textbox
                string validTo = System.DateTime.Today.AddDays(2).ToShortDateString().ToString(CultureInfo.InvariantCulture.NumberFormat);
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_DateValidUntil_Textbox.User.TypeText(validTo, 50);

                //Press Tab
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
            }

            //Wait for LayoutType combobox to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_LayoutType_Combobox.Wait.ForExists(Globals.timeOut);

            //Set Focus
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_LayoutType_Combobox.SetFocus();

            //Open Combobox
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_LayoutType_Combobox.OpenDropDown(true);

            //Select Value for LayoutType combobox
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_LayoutType_Combobox.SelectItemByText(true, layoutComboBoxValue, true);
        
            //Enter required fields(Title, PanelType) in each panel (max there can be 5 panels in the application)
            for (int i = 0; i < panelSize; i++)
                {
                        switch(i)
                        {
                        case 0:
                            //Wait for element to Exists in DOM
                            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem1.Wait.ForExists(Globals.timeOut);

                            //click on Panel tab
                            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem1.User.Click();

                            break;
                        case 1:
                            //Wait for element to Exists in DOM
                            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem2.Wait.ForExists(Globals.timeOut);

                            //click on Panel tab
                            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem2.User.Click();

                            break;
                        case 2:
                            //Wait for element to Exists in DOM
                            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem3.Wait.ForExists(Globals.timeOut);

                            //click on Panel tab
                            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem3.User.Click();

                            break;
                        case 3:
                            //Wait for element to Exists in DOM
                            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem4.Wait.ForExists(Globals.timeOut);

                            //click on Panel tab
                            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem4.User.Click();

                            break;
                        case 4:
                            //Wait for element to Exists in DOM
                            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem5.Wait.ForExists(Globals.timeOut);

                            //click on Panel tab
                            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTabItem5.User.Click();

                            break;
                         default:
                            
                            //Log Error
                            Manager.Current.Log.WriteLine(LogType.Error, " WelcomePage: Create: Panel Tab not found !!.");
                            break;
                        }
                                        
                        //Wait for element to Exists in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_Panel_TitleTextBox.Wait.ForExists(Globals.timeOut);

                        //Enter the Data in Textbox
                        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_Panel_TitleTextBox.User.TypeText(panelTitle[i], 50);

                        //Wait for element to Exists in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTypeComboBox.Wait.ForExists(Globals.timeOut);

                        //Click on Combobox
                        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTypeComboBox.User.Click();

                        //Select Value for PanelType combobox
                        SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_PanelTypeComboBox.SelectItemByText(true, panelComboBoxValue[i]);
                 }

            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_SaveButton.Wait.ForExists(Globals.timeOut);

            //Set Focus
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_SaveButton.SetFocus();

            //Click on save button
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_SaveButton.User.Click(MouseClickType.LeftDoubleClick);
        }

        //Method to Add Rows to Welcome Page 
        public void P2PWelcomePageAddRows()
        {
            //Wait for element to Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.Wait.ForExists(Globals.timeOut);

            //Click on AddRows button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddRowButton.User.Click();
            
            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_Create_AddRowsGrid.Wait.ForExists(Globals.timeOut);            

            //Find the Row using AutomationID                   
            RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_Create_AddRowsGrid;

            //Create an object for GridViewRow class
            GridViewRow gvr;            

            //Get Latest Row
            gvr = rgv.Rows[rgv.Rows.Count - 1];
            
            //Click on Cell to set the focus on a rows
            gvr.Cells[2].User.Click(MouseClickType.LeftClick);

            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_LinkTypeComboBox.Wait.ForExists(Globals.timeOut);

            //Select Value for link type combobox    
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_LinkTypeComboBox.SelectItem(1);

            //Press tab
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_SelectOrganizationUnitButton.Wait.ForExists(Globals.timeOut);

            //Click on button
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_SelectOrganizationUnitButton.User.Click();

            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PuchasingCategory_Tree.Wait.ForExists(Globals.timeOut);

            //Click on button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_Create_AddRowsGrid.SetFocus();

            //Click on Link Title Column
            gvr.Cells[5].User.Click(MouseClickType.LeftClick);

            //Entering Value in Link Title Column
            gvr.Cells[5].User.TypeText("title", 50);
            
            //Wait for Back button to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_BackButton.Wait.ForExists(Globals.timeOut);

            //Click on Back button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_BackButton.User.Click();

            //Wait for message pop up to show;
            P2PNavigation.WaitForPopUpOkButtonEnabled();            

            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on ok button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

        }
        
        //Method to edit Welcome Page as inherited or noninherited
        public void P2PWelcomePageSetInheritance(string inheritFlag)
        {
            if (inheritFlag.ToUpper() == "TRUE")
            {
                //Wait for checkbox to Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_InheritCheckBox.Wait.ForExists(Globals.timeOut);

                //Check if Inherit checkbox is not checked
                if (SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_InheritCheckBox.IsChecked.Equals(false))
                {
                    //Click on the Inherit Checkbox to select it
                    SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_InheritCheckBox.User.Click();
                }
            }
            else
            {
                //Wait for checkbox to Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_InheritCheckBox.Wait.ForExists(Globals.timeOut);

                //Check if Inherit checkbox is checked
                if (SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_InheritCheckBox.IsChecked.Equals(true))
                {
                    //Uncheck the Inherit Checkbox
                    SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_InheritCheckBox.User.Click();
                }
            }

            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_SaveButton.Wait.ForExists(Globals.timeOut);

            //Click on save button
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_SaveButton.User.Click();
        }

        //Method to Activate the Welcome Page
        public void P2PActivateWelcomePage()
        {
            //Wait for Activate button to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_ActivateButton.Wait.ForExists(Globals.timeOut);

            //Click on Activate button
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_ActivateButton.User.Click();
        }

        //Method to edit Welcome Page as Deactivated
        public void P2PDeactivateWelcomePage(string otherModule = null)
        {
            //Wait for DeActivate button to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_DeactivateButton.Wait.ForExists(Globals.timeOut);

            //Click on DeActivate button
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_DeactivateButton.User.Click();

            if (otherModule != null)
            {
                //Click on ok button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
            }
        }
    }
}
