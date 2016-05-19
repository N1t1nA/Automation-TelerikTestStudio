using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using System.Windows.Forms;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Controls.Xaml.Wpf;
using Telerik.WebAii.Controls.Xaml.GridView;
using Telerik.WebAii.Controls.Html;
using ArtOfTest.Common.UnitTesting;
using Telerik.WebAii.Controls.Xaml;

namespace P2P.Testing.Shared.Class.DataManagement.PaymentPlans.Create
{
    public class P2PPaymentPlanCreateDataManagement : BaseWebAiiTest
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

        //Method to Create New Reapproval Settings for Payment Plan
        public void P2PCreatePaymentPlanReapprovalSettings(string organizationUnit, string[] checkBox)
        {
            //Wait for Create Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_IDM_PP_ReapprovalSettings_CreateButton.Wait.ForExists(Globals.timeOut);

            //Click on Create Button
            SharedElement.P2P_Application.SilverlightApp.P2P_IDM_PP_ReapprovalSettings_CreateButton.User.Click();

            //Wait for Textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.Wait.ForExists(Globals.timeOut);

            //Enter Value
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectedOrganizationsTextbox.User.TypeText(organizationUnit, 50);

            ////Press tab to Activate InvoiceType ComboBox
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
            //Wait for pop up to open
            System.Threading.Thread.Sleep(Globals.pause);

            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);

            //Wait for OK Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.handleTime);

            //Click on OK Button on Select Organization Unit Dialog
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            //Select Checkbox : Currency , Coding and Payment Schedule            
            foreach (string checkBoxSelection in checkBox)
            {
                TextBlock selectCheckbox = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(checkBoxSelection).As<TextBlock>();

                //Select the checkBox by the User
                selectCheckbox.User.Click();
            }

            //Wait for Save Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_RefreshButton.Wait.ForExists(Globals.timeOut);

            //Click on Save Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_RefreshButton.User.Click();

            //Hanlde busy indicator
            P2PNavigation.CallBusyIndicator();

            //Wait for Back Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_BackButton.Wait.ForExists(Globals.navigationTimeOut);

            //Click on Back Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_BackButton.User.Click();
        }

        //Method to Delete Reapproval Settings for Payment Plan
        public void P2PDeletePaymentPlanReapprovalSettings()
        {
            //Wait for Delete Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_IDM_PP_ReapprovalSettings_DeleteButton.Wait.ForExists(Globals.timeOut);

            //Click on Delete Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_IDM_PP_ReapprovalSettings_DeleteButton.User.Click();

            //Wait for OK Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK Button on Select Organization Unit Dialog
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
        }

        //Method to Select Organization for adding Payment Plan Group
        public void P2PSelectOrganization(string company)
        {
            //Wait for grid to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_Organization_RadTreeView.Wait.ForExists(Globals.timeOut);

            //Get the node as a textblock
            TextBlock item = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(company).As<TextBlock>();

            //Select the organisation
            item.User.Click();
        }

        //Method to Create Payment Plan Group
        public void P2PCreatePaymentPlanGroup(string groupName, bool inherited)
        {
            //Wait for Add Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PaymentPlans_AddGroupButton.Wait.ForExists(Globals.timeOut);

            //Click on Add Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PaymentPlans_AddGroupButton.User.Click();

            //Wait for grid to Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PaymentPlans_PlanGroup_RadGridView.Wait.ForExists(Globals.timeOut);

            //Take reference of grid                   
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PaymentPlans_PlanGroup_RadGridView;
            
            if(!(grid.Rows.Count.Equals(0)))
            {
                //Create an object for GridViewRow class
                GridViewRow row;

                //Get Latest Row
                row = grid.Rows[grid.Rows.Count - 1];

                //Enter Group Name 
                row.Cells[1].User.TypeText(groupName,50);

               //Call method to update inherited checkbox
                P2PPaymentPlanGroupInheritance(inherited);
            }
            else
            {
                //Throw error. Row not added to create group
                Manager.Current.Log.WriteLine(LogType.Error, " No New row added for creating Plan Group");
            }

            //Wait for Save Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.Wait.ForExists(Globals.timeOut);

            //Click on Save Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.User.Click();
        }

        //Method to Make Payment Plan Group Inherited or Non Inherited
        public void P2PPaymentPlanGroupInheritance(bool inherited, string selectedRow =null)
        {
            //Wait for grid to Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PaymentPlans_PlanGroup_RadGridView.Wait.ForExists(Globals.timeOut);

            //Find the Row using AutomationID                   
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PaymentPlans_PlanGroup_RadGridView;

            //Create an object for GridViewRow class
            GridViewRow row;

            if (selectedRow == null)
            {
                //Get Last Row
                row = grid.Rows[grid.Rows.Count - 1];
            }
            else
            {
                //Get selected row                
                row = grid.Rows.FirstOrDefault(y => y.IsSelected.Equals(true));
            }

            //Get Value of Checkbox 
            bool inheritedValue = row.Cells[2].Checkbox.IsChecked.Value;           

            //Make the Group inherited or not
            if (inherited.Equals(true))
            { 
                //if checkbox is not checked (creating new row)
                if (inheritedValue.Equals(false))
                    //Check the checkbox                 
                    row.Cells[2].User.Click(MouseClickType.LeftDoubleClick);

                //Edit Group inheritance
                if (selectedRow != null)
                    //Check the checkbox
                    row.Cells[2].User.Click(MouseClickType.LeftClick);
            }
            else
            {
                //if checkbox is checked
                if (inheritedValue.Equals(true))
                    //UnCheck the checkbox                  
                    row.Cells[2].User.Click(MouseClickType.LeftDoubleClick);
            }                
        }

        //Method to Delete Payment Plan Group
        public void P2PDeletePaymentPlanGroup(string groupName)
        {
            //Wait for Delete Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PaymentPlans_DeleteGroupButton.Wait.ForExists(Globals.timeOut);

            //Click on Delete Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PaymentPlans_DeleteGroupButton.User.Click();

            //Wait for Ok Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on Ok Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            //Wait for Save button exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.Wait.ForExists(Globals.timeOut);

            //Click on Save Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.User.Click();
        }

        //Method to Select Payment Plan Group
        public void P2PSelectPaymentPlanGroup(string groupName)
        {
            //Wait for grid to Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PaymentPlans_PlanGroup_RadGridView.Wait.ForExists(Globals.timeOut);

            //Take reference of grid          
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PaymentPlans_PlanGroup_RadGridView.CastAs<RadGridView>();

            //Find the cell Value in Grid
            TextBlock planGroupName = grid.Find.ByTextContent(groupName).As<TextBlock>();

            //Click on Cell 
            planGroupName.User.Click();

            //Press Escape key to move focus out of groupName Textbox but still keep the row selected
            Manager.Current.ActiveBrowser.Desktop.KeyBoard.KeyPress(Keys.Escape);
        }
    }
}
