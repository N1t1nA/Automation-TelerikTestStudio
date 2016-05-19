using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using Telerik.WebAii.Controls.Xaml;
using System.Windows.Forms;
using ArtOfTest.WebAii.Win32;
using ArtOfTest.WebAii.Controls.Xaml.Wpf;

namespace P2P.Testing.Shared.Class.InvoiceAdministration.Search
{
    public class P2PInvoiceAdministrationSearch : BaseWebAiiTest
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
        
        //Method to Search the Invoice and Use Free Text Search for Searching Invoices
        public void P2PInvoiceAdministrationFreeTextSearchToSearchInvoice(string invoiceId, string myTaskSearch=null,string search=null)
        {

            if(search==null)
            {
            //Wait for Search Textbox to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.Wait.ForExists(Globals.timeOut);

            //Set Focus on Search Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.SetFocus();

            //Click on Search Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.User.Click();
            
            //Select the String in Text Box
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(KeyBoard.KeysFromString("Ctrl+A"));

            //Generate a Keyboard Event to clear search
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Back);

            //Enter the Search Term to search for invoice
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchTextbox.User.TypeText(invoiceId,50);
            }

            if(myTaskSearch !=null)
            {
                //Wait for Search Button MyTask Page Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_MyTask_InvoiceSearchButton.Wait.ForExists(Globals.timeOut);

                //Click on Search Button MyTask Page
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_MyTask_InvoiceSearchButton.User.Click();

            }
            else
            {           
                //Wait for Search Button to Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.Wait.ForExists(Globals.timeOut);

                //Click on Search button 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();
            }
            //Type searched Plan number
            Manager.Current.Log.WriteLine(LogType.Information, "Search Results '" + invoiceId + "'.");
        }

        //Method to Use Company Picker for Searching Invoices
        public void P2PInvoiceAdministrationCompanyPickerToSearchInvoice(string company, string personalModeScreen = null)
        {
            //Wait for Text box to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.Wait.ForExists(Globals.timeOut);

            //Clear the Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.Text = string.Empty;

            //Wait for Select Organization Unit Button Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnitButton.Wait.ForExists(Globals.timeOut);

            //Set focus on the Org Unit Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnitButton.SetFocus();

            //Click on Select Organization Unit Button to Select the Company or Organization Unit
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnitButton.User.Click();

            //Wait for Organization Tree to Exists in Select Organization Unit Dialog
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_OrganisationUnitTreeView.Wait.ForVisible(Globals.handleTime);

            //Find the Company from the Organization Unit Picker and Select 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_OrganisationUnitTreeView.Find.ByTextContent(company).User.Click();

            if (personalModeScreen != null)
            {
                //Check the Check Box of Organization Unit Picker
                SharedElement.P2P_Application.SilverlightApp.P2P_SelectOrganization_CheckBox.User.Click();
            }
            else
            {
                //Check the Check Box of Organization Unit Picker
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_OrganisationUnit_CheckBox.User.Click();
            }

            //Wait for OK Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnit_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK Button on Select Organization Unit Dialog
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnit_OKButton.User.Click();
        }

        //Method to Filter the Organizations from Overview Page
        public void P2PInvoiceAdministrationOverviewFilterOrganization(string organizationCode)
        {
            //Wait for Text box to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.Wait.ForExists(Globals.timeOut);

             //Clear the Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.Text = string.Empty;
            
            //Set focus on Organization Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.SetFocus();

            //Enter the Organization Name in Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.User.TypeText(organizationCode, 50);
            
            //Wait for Refresh Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_RefreshButton.Wait.ForExists(Globals.timeOut);

            //Click on Refresh Button to get the Page Updated 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview_RefreshButton.User.Click();    
        }

        //Method to Select Company from the Picker
        public void P2PInvoiceAdministrationSearchByCompany(string company, string otherScreen = null)
        {
            //Wait for Text box to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.Wait.ForExists(Globals.timeOut);

            //Clear the Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.Text = string.Empty;

            //Wait for Select Organization Unit Button Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnitButton.Wait.ForExists(Globals.timeOut);

            //Set focus on the Org Unit Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnitButton.SetFocus();

            //Click on Select Organization Unit Button to Select the Company or Organization Unit
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnitButton.User.Click();

            //Wait for Organization Tree to Exists in Select Organization Unit Dialog
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_OrganisationUnitTreeView.Wait.ForVisible(Globals.timeOut);
                        
            //Create a RadTreeview Item            
            RadTreeViewItem item=SharedElement.P2P_Application.SilverlightApp.Find.ByType<RadTreeView>().FindNodeByText(company);

            //Create a Checkbox 
            ArtOfTest.WebAii.Silverlight.UI.CheckBox check = item.Find.ByType<ArtOfTest.WebAii.Silverlight.UI.CheckBox>();
            
            //Click on that Checkbox
            check.User.Click();

            if (otherScreen != null)
            {
                //Wait for OK Button Exists in DOM 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

                //Click on OK Button on Select Organization Unit Dialog
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
            }
            else
            {
                //Wait for OK Button Exists in DOM 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnit_OKButton.Wait.ForExists(Globals.timeOut);

                //Click on OK Button on Select Organization Unit Dialog
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnit_OKButton.User.Click();
            }
        }

        //Method to Select Company from the Picker (without checkbox)
        public void P2PSearchByCompany(string company)
        {
            //Wait for Text box to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.Wait.ForExists(Globals.timeOut);

            //Clear the Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.Text = string.Empty;

            //Wait for Select Organization Unit Button Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnitButton.Wait.ForExists(Globals.timeOut);

            //Set focus on the Org Unit Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnitButton.SetFocus();

            //Click on Select Organization Unit Button to Select the Company or Organization Unit
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnitButton.User.Click();

            //Wait for Organization Tree to Exists in Select Organization Unit Dialog
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_OrganisationUnitTreeView.Wait.ForVisible(Globals.timeOut);

            //Copy the RadTreeView into a local variable
            RadTreeView companyTreeView = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_OrganisationUnitTreeView;

            //Find Company in tree and store in Textblock
            TextBlock companyTextBlock = companyTreeView.Find.ByTextContent(company).CastAs<TextBlock>();

            //Click on TextBlock
            companyTextBlock.User.Click();            

            //Wait for OK Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnit_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK Button on Select Organization Unit Dialog
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnit_OKButton.User.Click();
        }

        //Method to click on Advance Search
        public void P2PInvoiceAdministrationAdvanceSearch()
        {
            //Wait for Advance Search button exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_AdvancedSearchButton.Wait.ForExists(Globals.timeOut);
            //Click on Advance Search button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Workflow_AdvancedSearchButton.User.Click();            
        }

        //Method to click on Search Button : of Advance Search
        public void P2PAdvanceSearchButtonClick()
        {
            //Wait for Search Button exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Search_AdvanceSearch_SearchButton.Wait.ForExists(Globals.timeOut);

            //Click On Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_Search_AdvanceSearch_SearchButton.User.Click();
        }

        //Method to Select Invoice Activity
        public void P2PAdvanceSearchInvoiceActivity(string comboBoxItem)
        {
            //Wait for button to load
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchAdvanceSearch_InvoiceActivityComboBox.Wait.ForExists(Globals.timeOut);
            
            //Click On button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchAdvanceSearch_InvoiceActivityComboBox.OpenDropDown(true);

            //Select Item from Combo Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchAdvanceSearch_InvoiceActivityComboBox.SelectItemByText(true, comboBoxItem, true);

            //Wait for button to load
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchAdvanceSearch_InvoiceActivityComboBox.Wait.ForExists(Globals.timeOut);
        }

        //Method to click on Clear ALL Search Button 
        public void P2PInvoiceAdministrationClearAllSearch()
        {
            //Wait for Search Button exist in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=ClearButton", "XamlTag=button")).Wait.ForExists(Globals.timeOut);

            //Click On Search Button
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=ClearButton", "XamlTag=button")).User.Click();
        }

    }
}
