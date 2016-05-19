using System;
using System.Linq;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Silverlight.UI;
using Telerik.WebAii.Controls.Xaml;
using System.Windows.Forms;
using ArtOfTest.WebAii.Controls.Xaml;
using P2P.Testing.Shared.Class;
using ArtOfTest.Common.UnitTesting;

namespace P2P.Testing.ConfigTool.Class
{
    public class P2PConfigToolConfigurationArea : BaseWebAiiTest
    {
        private Browser _browser;

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

        public P2PConfigToolConfigurationArea(Browser browser)
        {
            _browser = browser;
        }

        //Method for select the Configutratoion Area in ConfigTool
        public void P2P_ConfigurationTool_ConfigurationArea(string configurationArea, string page)
        {
            //Create object of Framework Element Class 
            FrameworkElement fe = null;
            //variable for loop count
            int loopCounter = 0;

            do
            {
                //increment counter
                loopCounter = loopCounter + 1;

                //Get the framework element ie combobox
                fe = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=configurationComboBox", "XamlTag=combobox"));

                if ((fe != null) && (configurationArea != page))
                {
                    do
                    {
                        //Wait for Dropdown to load
                        SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_ConfigurationArea_ComboBox.Wait.ForExists(Globals.navigationTimeOut);

                    } while (SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_ConfigurationArea_ComboBox.IsEnabled.Equals(false));

                   
                    // Open ComboBox drop down
                    SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_ConfigurationArea_ComboBox.OpenDropDown(true);

                    // Wait for Exists
                    SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_ConfigurationArea_ComboBox.Wait.ForExists(Globals.timeOut);
                    
                    // Select  Schema from the "P2P_ConfigurationTool_Configuration_ComboBox"            
                    SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_ConfigurationArea_ComboBox.SelectItemByText(true, configurationArea, true);
                }
                //break out loop
                if (loopCounter == 10)
                {
                    //Write the log if  Failure
                    Manager.Current.Log.CaptureDesktop(Globals.capturedImageLocation + "Click on Configuration Area Combobox");                    
                    Manager.Current.Log.WriteLine(LogType.Error, " ComboBox : Configuration Area not found. Items not visible after clicking on combobox. Please check snapshot");
                }
            } while (fe == null);                   
        }

        //Method for select the Configutratoion Area in ConfigTool
        public void P2PConfigurationTool_ConfigurationArea(string configurationArea, string page)
        {
            //Create object of Framework Element Class 
            FrameworkElement fe = null;
            //variable for loop count
            int loopCounter = 0;

            do
            {
                //increment counter
                loopCounter = loopCounter + 1;

                //Get the framework element ie combobox
                fe = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=configurationComboBox", "XamlTag=combobox"));

                if ((fe != null) && (configurationArea != page))
                {
                    do
                    {
                        //Wait for Dropdown to load
                        SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_ConfigurationArea_ComboBox.Wait.ForExists(Globals.navigationTimeOut);

                    } while (SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_ConfigurationArea_ComboBox.IsEnabled.Equals(false));


                    // Open ComboBox drop down
                    SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_ConfigurationArea_ComboBox.OpenDropDown(true);

                    // Wait for Exists
                    SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_ConfigurationArea_ComboBox.Wait.ForExists(Globals.timeOut);

                    // Select  Schema from the "P2P_ConfigurationTool_Configuration_ComboBox"            
                    SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_ConfigurationArea_ComboBox.SelectItemByText(true, configurationArea, true);
                }
                //break out loop
                if (loopCounter == 10)
                {
                    //Write the log if  Failure
                    Manager.Current.Log.CaptureDesktop(Globals.capturedImageLocation + "Click on Configuration Area Combobox");
                    Manager.Current.Log.WriteLine(LogType.Error, " ComboBox : Configuration Area not found. Items not visible after clicking on combobox. Please check snapshot");
                }
            } while (fe == null);                   
        }

        //Method for select the Schema Selection in ConfigTool
        public void P2PConfigurationTool_SchemaSelection(string schemaSelection, string page)
        {
            //Wait for exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_SchemaSelection_ComboBox.Wait.ForExists(Globals.navigationTimeOut);

            if (schemaSelection != page)
            {
                // Open ComboBox drop down
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_SchemaSelection_ComboBox.OpenDropDown(true);

                // Wait for Exists
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_SchemaSelection_ComboBox.Wait.ForExists(Globals.timeOut);

                // Select  Schema from the "P2P_ConfigurationTool_SchemaSelection_ComboBox"            
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_SchemaSelection_ComboBox.SelectItemByText(true, schemaSelection, true);
            }
            else
            {
                //Wait for Elements Exists in DOM
                P2PNavigation.CallBusyIndicator();
            }
        }

        //Method to Click on Search button and Wait data contains in a grid
        public void P2PConfigurationTool_OMConfigurationSearchButton()
        {
            //Wait for Search button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_OMConfiguration_SearchConfigurationButton.Wait.ForExists(Globals.timeOut);

            //Click on Button
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_OMConfiguration_SearchConfigurationButton.User.Click();
        }

        //Method to Click on Search button and Wait data contains in a grid
        public void P2PConfigurationTool_OMCategorySearchButton()
        {
            //Wait for Search button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_OMCategory_SearchCategoriesButton.Wait.ForExists(Globals.timeOut);

            //Click on Button
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_OMCategory_SearchCategoriesButton.User.Click();
        }

        //Method to Click on Search button and Wait data contains in a grid
        public void P2PConfigurationTool_LookupListSearchButton()
        {
            //Wait for Search button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigTool_LookupList_SearchLookupListButton.Wait.ForExists(Globals.timeOut);

            //Click on Button
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigTool_LookupList_SearchLookupListButton.User.Click();
        }

        //Method to Click on Search button and Wait data contains in a grid
        public void P2PConfigurationTool_ProcessSearchButton()
        {
            //Wait for Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_SearchButton.Wait.ForExists(Globals.timeOut);

            //Click on Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_SearchButton.User.Click();
        }

        //Method to Delete DateRule from the Entity Configuration
        public void P2PConfigurationTool_DeleteDateRule(string entityConfigCategory, string entityConfigSchemas, string ruleName)
        {
            //Wait for Schema Selection Combobox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_SchemaSelection_ComboBox.Wait.ForExists(Globals.timeOut);

            //Select  Schema from the "P2P_ConfigurationTool_Configuration_ComboBox"            
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_SchemaSelection_ComboBox.SelectItemByText(true, entityConfigCategory, true);

            //Wait for Entity Type Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_EntityTypeConfigurationListGridViewControl.Wait.ForExists(Globals.timeOut);

            //Select the Schema (Invoice) from where Rule is to be Deleted
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_EntityTypeConfigurationListGridViewControl.Find.ByTextContent(entityConfigSchemas).User.Click();

            //Wait for Rules Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_RulesEntityTypeConfigurationButton.Wait.ForExists(Globals.timeOut);

            //Click on Rules Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_RulesEntityTypeConfigurationButton.User.Click();

            //Wait for Rules Grid Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_RulesGrid.Wait.ForExists(Globals.timeOut);

            //Find the InvoiceDate Rule
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_RulesGrid.Find.ByTextContent(ruleName).User.Click();

            //Wait for Delete Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_DeleteAttachmentButton.Wait.ForExists(Globals.timeOut);

            //Click on Delete Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_DeleteAttachmentButton.User.Click();

            //Wait for Yes Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on Yes Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            //Wait for OK Button exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_RulesGrid_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_RulesGrid_OKButton.User.Click();

            //Wait for Save Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_SaveButton.Wait.ForExists(Globals.timeOut);

            //Save the Entity Configuration
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_SaveButton.User.Click();
        }

        //Method to Create a  New Process
        public void AddNewProcess(string documentType, string organizationUnit, string processName)
        {
            //Wait for AddNewProcess Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigTool_Process_AddProcessToolBarButton.Wait.ForExists(Globals.timeOut);

            //Click on "P2P_ConfigTool_Process_AddProcessToolBarButton"
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigTool_Process_AddProcessToolBarButton.User.Click();

            // Open "P2P_ConfigurationTool_Process_DocumentTypeCombobox"  dropdown 
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_DocumentTypeCombobox.OpenDropDown(true);

            //Select DocumentType from the "P2P_ConfigurationTool_Configuration_ComboBox"            
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_DocumentTypeCombobox.SelectItemByText(true, documentType, true);

            //Wait for SelectOrganizationUnit Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnitButton.Wait.ForExists(Globals.navigationTimeOut);

            //Click on "SelectOrganizationUnit" Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnitButton.User.Click();

            //Wait for toggle button to exists
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_AddProcess_SelectOrganization_ExpanderTogglebutton.Wait.ForExists(Globals.navigationTimeOut);

            // Verify P2P_ConfigurationTool_AddProcess_SelectOrganization_ExpanderTogglebutton is unchecked
            if (SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_AddProcess_SelectOrganization_ExpanderTogglebutton.IsChecked.Equals(false))
            {
                //Wait for Organization Tree to Exists in Select Organization Unit Dialog
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_AddProcess_SelectOrganization_ExpanderTogglebutton.User.Click();
            }

            //Wait for Organization Tree to Exists in Select Organization Unit Dialog
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_OrganisationUnitTreeView.Wait.ForVisible(Globals.timeOut);

            //Find the Company from the Organization Unit Picker and Select 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_OrganisationUnitTreeView.Find.ByTextContent(organizationUnit).User.Click();

            //Wait for OK Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnit_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK Button on Select Organization Unit Dialog
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnit_OKButton.User.Click();

            //Enter Process Name in "P2P_ConfigurationTool_SchemaName_Textbox"
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_SchemaName_Textbox.Text = processName.ToString();

            //Wait for Active_Checkbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Schema_Active_Checkbox.Wait.ForExists(Globals.timeOut);

            //Using If condition for active check box
            if (SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Schema_Active_Checkbox.IsChecked != true)
            {
                // Check the "P2P_ConfigurationTool_Schema_Active_Checkbox" to Activate the Schema
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Schema_Active_Checkbox.Check(true);
            }

            //Wait for Save Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Schema_Save_Button.Wait.ForExists(Globals.timeOut);

            //Click on "P2P_ConfigurationTool_Schema_Save_ToolbarButton"
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Schema_Save_Button.User.Click();
        }

        //Method to Add New Invoice Process in ConfigTool
        public void AddNewActivities(string activityName, string activityHandler, string indexNumber, string activityExecutionType, Boolean recipientRequired, string indexCell, Boolean commentRequired, Boolean recipientResolver)
        {
            //Wait for Save Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_AddActivitiesButton.Wait.ForExists(Globals.timeOut);

            //Click on "P2P_ConfigurationTool_Process_AddActivitiesButton"
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_AddActivitiesButton.User.Click();

            //Refresh the Visual Tree
            SharedElement.P2P_Application.SilverlightApp.OwnerApp.RefreshVisualTrees();

            //Get the grid into a variable
            RadGridView rgv1 = SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_ActivitiesBaslistviewgridcontrol;

            //Count Rows and find the recently added row
            GridViewRow gvr = rgv1.Rows[rgv1.Rows.Count - 1];

            //for select the value in combo box
            foreach (Telerik.WebAii.Controls.Xaml.GridViewRow r in rgv1.Rows)
            {
                //If block executes if Cells[2] is empty
                if (r.Cells[2].Text.Equals(String.Empty))
                {
                    gvr = r;
                    break;
                }
            }

            try
            {
                //Click on cell for focusing
                gvr.Cells[2].User.Click(MouseClickType.LeftDoubleClick);

                //Wait for grid cell
                gvr.Cells[2].Wait.ForExists(Globals.timeOut);

                //Refresh the Grid
                gvr.Refresh();

                //Type input in a Cell
                gvr.Cells[2].User.TypeText(activityName, 50);

                //Press the Tab Key to move to next Column
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

                //Wait for grid cell
                gvr.Cells[3].Wait.ForExists(Globals.timeOut);

                //Select the Value in Activity "Handler Dropdown" Column
                gvr.Cells[3].User.Click(MouseClickType.LeftClick);

                //Wait for grid cell
                gvr.Cells[3].Wait.ForExists(Globals.timeOut);

                //Refresh the GridViewRow
                gvr.Refresh();

                //Get the RadComboBox into a variable
                Telerik.WebAii.Controls.Xaml.RadComboBox rcb = gvr.Cells[3].Find.ByType<Telerik.WebAii.Controls.Xaml.RadComboBox>();

                //open the drop down
                rcb.ToggleDropDown();

                //Type input in a Cell
                rcb.SelectItem(activityHandler, false);

                //null the variable
                rcb = null;

                //Click on cell for focusing
                gvr.Cells[4].User.Click(MouseClickType.LeftDoubleClick);

                //Wait for grid cell
                gvr.Cells[4].Wait.ForExists(Globals.timeOut);

                //Refresh the Grid
                gvr.Refresh();

                //Type input in a Cell
                gvr.Cells[4].User.TypeText(indexNumber, 50);

                //Select the Value in Activity "Handler Dropdown" Column
                gvr.Cells[9].User.Click(MouseClickType.LeftDoubleClick);

                //Wait for grid cell
                gvr.Cells[9].Wait.ForExists(Globals.timeOut);

                //Refresh the GridViewRow
                gvr.Refresh();

                //Get the RadComboBox into in a variable
                RadComboBox rcb2 = gvr.Cells[9].Find.ByType<RadComboBox>();

                //open the drop down
                rcb2.ToggleDropDown();

                //Select the Type
                rcb2.SelectItem(activityExecutionType, false);

                //null the variable
                rcb2 = null;

                //If condition is true the execute if block
                if (recipientRequired == true)
                {
                    //Select the Checkbox in Activity "Recipient Required" Column
                    gvr.Cells[10].User.Click(MouseClickType.LeftDoubleClick);

                    //Wait for grid cell
                    gvr.Cells[10].Wait.ForExists(Globals.timeOut);

                    //Refresh the Grid
                    gvr.Refresh();

                    //Get the ICheckBox into in a variable
                    ArtOfTest.WebAii.Controls.Xaml.ICheckBox chkBoxRecipientRequired = gvr.Cells[10].Find.ByType<ArtOfTest.WebAii.Controls.Xaml.ICheckBox>();

                    //Check the check box
                    chkBoxRecipientRequired.Check(true);
                }

                //If condition is true the execute if block
                if (commentRequired == true)
                {
                    //Select the Checkbox in Activity "Comment Required" Column
                    gvr.Cells[11].User.Click(MouseClickType.LeftDoubleClick);

                    //Wait for grid cell
                    gvr.Cells[11].Wait.ForExists(Globals.timeOut);

                    //Refresh the Grid
                    gvr.Refresh();

                    //Get the ICheckBox into in a variable
                    ArtOfTest.WebAii.Controls.Xaml.ICheckBox chkBoxComment = gvr.Cells[11].Find.ByType<ArtOfTest.WebAii.Controls.Xaml.ICheckBox>();

                    //Check the check box
                    chkBoxComment.Check(true);
                }
            }

            finally
            {
                rgv1 = null;
                gvr = null;
            }
        }

        //Method to Edit the Process
        public void EditActivity(string findExistingProcess, string headerCell, string activityName, int count, Boolean recipientRequired, Boolean commentRequired, Boolean mandatoryRecipient, Boolean resolverType, string indexCell = null)
        {
            //Wait for Grid Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigTool_Process_GridViewControl.Wait.ForExists(Globals.timeOut);

            // Select Existing Process in Grid
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigTool_Process_GridViewControl.Find.ByTextContent(findExistingProcess).User.Click();

            // Wait for openselected Button Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_OpenSelectedToolBarButton.Wait.ForExists(Globals.timeOut);

            // Click on openselected Button
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_OpenSelectedToolBarButton.User.Click();

            //Wait for Grid Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_ActivitiesBaslistviewgridcontrol.Wait.ForExists(Globals.timeOut);

            //Click on Index Header Cell for Sorting the Activity
            TextBlock index = SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_ActivitiesBaslistviewgridcontrol.Find.ByTextContent(headerCell).As<TextBlock>();
            
            //Click on Index Header Cell for Sorting the Invoice
            index.User.Click();
            
            //Wait for Grid Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_ActivitiesBaslistviewgridcontrol.Wait.ForExists(Globals.timeOut);

            //Select the Reveiw Activity in a Grid            
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_ActivitiesBaslistviewgridcontrol.Find.ByTextContent(activityName).User.Click(MouseClickType.LeftDoubleClick);

            //Put the grid into a variable
            RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_ActivitiesBaslistviewgridcontrol;

            //Put the row into a variable
            GridViewRow gvr = rgv.Rows[rgv.Rows.Count - 1];

            //Wait for Grid Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_ActivitiesBaslistviewgridcontrol.Wait.ForExists(Globals.timeOut);

            //Use for loop here to focus and click the check box in a grid
            for (int i = 0; i < count; i++)
            {
                //Wait for Grid Exist in DOM
                gvr.Wait.ForExists(Globals.timeOut);

                //Refresh the grid
                gvr.Refresh();

                //Press tab to move the location in grid
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
            }

            //Refresh the grid
            gvr.Refresh();

            //If conditon is true then exectute if block
            if (recipientRequired == true)
            {
                //Put Icheckbox into a variable
                ICheckBox chkBox = gvr.Cells[10].Find.ByType<ICheckBox>();

                //Press space bar to check the check box
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Space);
            }

            //If conditon is true then exectute if block
            if (commentRequired == true)
            {
                //Put the grid into a variable
                RadGridView rgv1 = SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_ActivitiesBaslistviewgridcontrol;

                //Put the row into a variable
                GridViewRow gvr1 = rgv1.Rows[rgv.Rows.Count - 1];

                //Refresh the grid
                gvr1.Refresh();

                //Wait for Grid Exist in DOM
                gvr1.Wait.ForExists(Globals.timeOut);

                //Press tab to move the location in grid
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

                //Wait for Grid Exist in DOM
                gvr1.Wait.ForExists(Globals.timeOut);

                //Refresh the grid
                gvr1.Refresh();

                //Press Arrow key up to move the location in grid
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Up);

                //Wait for Grid Exist in DOM
                gvr1.Wait.ForExists(Globals.timeOut);

                //Refresh the grid
                gvr1.Refresh();

                //Put Icheckbox into a variable
                ICheckBox chkbox = gvr.Cells[11].Find.ByType<ICheckBox>();

                //Press space bar to check the check box
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Space);
            }

            //If conditon is true then exectute if block
            if (mandatoryRecipient == true)
            {
                //Wait for Add Resolver button Exist in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_AddRecipientResolversButton.Wait.ForExists(Globals.timeOut);

                //Click on Resolver button
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_AddRecipientResolversButton.User.Click();

                //Wait for Resolver Grid Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_RecipientResolverGridControl.Wait.ForExists(Globals.timeOut);

                //Wait for Add New Recipeint Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_AddNewRecipientResolverTypeButton.Wait.ForExists(Globals.timeOut);

                //Click on Add New Recipeint Button
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_AddNewRecipientResolverTypeButton.User.Click();
            }

            //If conditon is true then exectute if block
            if (resolverType == true)
            {
                //Wait for Recipeint Grid Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_RecipientResolverGridControl.Wait.ForExists(Globals.timeOut);

                //Select the index cell to focus on a Recipeint Grid           
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_RecipientResolverGridControl.Find.ByTextContent(indexCell).User.Click(MouseClickType.LeftDoubleClick);

                //Press tab to move the location in grid
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
                
                //Press tab to move the location in grid
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
                
                //Press space bar to check the check box
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Space);
            }
                        
            //Wait for OK Button Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_RulesGrid_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_RulesGrid_OKButton.User.Click();

            //Wait for Save Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Schema_Save_Button.Wait.ForExists(Globals.timeOut);

            //Click on Save Button
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Schema_Save_Button.User.Click();
        }

        //Method to Add New Invoice Process in ConfigTool
        public void AddNewTransitionRow(string sourceActivityName, string targetActivityName)
        {
            if (targetActivityName == "Validation")
            {
                //Changing Value in Sencond Row of Activity "Handler Dropdown" Column to Automatic
                //Find the Schema items Grid using AutomationID                
                RadGridView rgvTransition = SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_InvoiceProcess_TransitionsGridControl;

                //Count Rows and find the recently added row
                Telerik.WebAii.Controls.Xaml.GridViewRow gvrTransition = rgvTransition.Rows[rgvTransition.Rows.Count - 1];

                //Select the Value in Activity "Handler Dropdown" Column
                gvrTransition.Cells[4].User.Click(MouseClickType.LeftDoubleClick);
                gvrTransition.Cells[2].User.Click(MouseClickType.LeftDoubleClick);
                
                //Wait for Grid Exist in DOM
                gvrTransition.Wait.ForExists(Globals.timeOut);

                //Refresh the GridViewRow
                gvrTransition.Refresh();

                //Telerik.WebAii.Controls.Xaml.RadComboBox rcb3 = gvr1.Cells[2].Find.ByType<Telerik.WebAii.Controls.Xaml.RadComboBox>();
                RadComboBox rcb3 = gvrTransition.Cells[2].Find.ByType<RadComboBox>();

                rcb3.ToggleDropDown();

                rcb3.SelectItem(sourceActivityName, false);

                gvrTransition.Cells[3].User.Click(MouseClickType.LeftDoubleClick);

                //Wait for Grid Exist in DOM
                gvrTransition.Wait.ForExists(Globals.timeOut);

                //Refresh the GridViewRow
                gvrTransition.Refresh();

                //Telerik.WebAii.Controls.Xaml.RadComboBox rcb4 = gvr1.Cells[3].Find.ByType<Telerik.WebAii.Controls.Xaml.RadComboBox>();
                RadComboBox rcb4 = gvrTransition.Cells[3].Find.ByType<RadComboBox>();

                rcb4.ToggleDropDown();

                //System.Threading.Thread.Sleep(Globals.timeOut);
                rcb4.SelectItem(targetActivityName, false);
            }

            else
            {
                //Wait for P2P_ConfigurationTool_Process_AddNewTransitionsButton Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_AddNewTransitionsButton.Wait.ForExists(Globals.timeOut);

                //Click on P2P_ConfigurationTool_Process_AddNewTransitionsButton
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_AddNewTransitionsButton.User.Click();

                //Find the Schema items Grid using AutomationID
                Telerik.WebAii.Controls.Xaml.RadGridView rgv1 = SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_InvoiceProcess_TransitionsGridControl;
                
                //Count Rows and find the recently added row
                Telerik.WebAii.Controls.Xaml.GridViewRow gvr = rgv1.Rows[rgv1.Rows.Count - 1];

                //Select the Value in Activity "Handler Dropdown" Column
                //gvr.Cells[4].User.Click(MouseClickType.LeftDoubleClick);
                gvr.Cells[2].User.Click(MouseClickType.LeftDoubleClick);

                //Wait for Grid Exist in DOM
                gvr.Wait.ForExists(Globals.timeOut);

                //Refresh the GridViewRow
                gvr.Refresh();

                Telerik.WebAii.Controls.Xaml.RadComboBox rcb = gvr.Cells[2].Find.ByType<Telerik.WebAii.Controls.Xaml.RadComboBox>();

                rcb.ToggleDropDown();

                rcb.SelectItem(sourceActivityName, false);

                rcb = null;

                //Select the Value in Activity "Handler Dropdown" Column
                gvr.Cells[4].User.Click(MouseClickType.LeftDoubleClick);

                gvr.Cells[3].User.Click(MouseClickType.LeftDoubleClick);

                //Wait for Grid Exist in DOM
                gvr.Wait.ForExists(Globals.timeOut);

                //Refresh the GridViewRow
                gvr.Refresh();

                Telerik.WebAii.Controls.Xaml.RadComboBox rcb1 = gvr.Cells[3].Find.ByType<Telerik.WebAii.Controls.Xaml.RadComboBox>();

                rcb1.ToggleDropDown();

                rcb1.SelectItem(targetActivityName, false);

                rcb1 = null;

                if (targetActivityName == "FinalActivity")
                {
                    //Wait for Save Button Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Schema_Save_Button.Wait.ForExists(Globals.timeOut);

                    //Click on "P2P_ConfigurationTool_Schema_Save_ToolbarButton"
                    SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Schema_Save_Button.User.Click();
                }
            }
        }

        //Method to Select Entity Configuration
        public void P2PConfigurationTool_SelectEntityTypeConfiguration(string entityTypeConfig)
        {
            //Wait for Entity Type Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_EntityTypeConfigurationListGridViewControl.Wait.ForExists(Globals.timeOut);

            //Select the Schema (Invoice) from where Rule is to be Deleted
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_EntityTypeConfigurationListGridViewControl.Find.ByTextContent(entityTypeConfig).User.Click();
        }

        //Method to Select Entity Configuration Items
        public void P2PConfigurationTool_SelectEntityConfigurationItems(string entityConfigItem)
        {
            //Wait for Entity Type Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_EntityConfigurationItemListGridViewControl.Wait.ForExists(Globals.timeOut);

            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_EntityConfigurationItemListGridViewControl.SetFocus();

            FrameworkElement fe;

            bool found;

            do
            {
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.PageDown);

                fe = SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_EntityConfigurationItemListGridViewControl;

                found = fe.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(entityConfigItem));                    

            } while (found == false);

            //Select the Schema (Invoice) from where Rule is to be Deleted
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_EntityConfigurationItemListGridViewControl.Find.ByTextContent(entityConfigItem).User.Click();
        }

        //Method to Add New rule
        public void P2PConfigurationTool_AddNewEntityConfigurationItemRule(int rowIndex, string entityConfigItemRule, string expressionEditorRule, string swissReferenceNuumberResourceID, Boolean saveButton)
        {
            //Wait for Rules Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_RulesEntityConfigurationItemButton.Wait.ForExists(Globals.timeOut);

            //Click on Rules Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_RulesEntityConfigurationItemButton.User.Click();

            //Wait for Entity Configuration Type Item Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_AddNewRecipientResolverTypeButton.Wait.ForExists(Globals.timeOut);

            //Click on Entity Configuration Type Item Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_AddNewRecipientResolverTypeButton.User.Click();

            //Wait for Entity Configuration Type Rule Grid Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_RulesGrid.Wait.ForExists(Globals.timeOut);

            //Put the grid into a variable
            RadGridView rgv = SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_RulesGrid;

            //Click on Row
            rgv.Rows[rowIndex].User.Click();

            //Put the row into a variable
            GridViewRow gvr = rgv.Rows[rgv.Rows.Count - 1];

            //Wait for Cell
            gvr.Cells[3].Wait.ForExists(Globals.timeOut);

            //Click on Cell
            gvr.Cells[3].User.Click(MouseClickType.LeftDoubleClick);

            //Input data in cell
            gvr.Cells[3].User.TypeText(entityConfigItemRule, 5);

            //Refresh Grid
            rgv.Refresh();

            //Refresh Cell
            gvr.Cells[5].Refresh();

            //Wait for Cell
            gvr.Cells[5].Wait.ForExists(Globals.timeOut);

            //Click on Cell
            gvr.Cells[5].User.Click(MouseClickType.LeftDoubleClick);

            //Set focus in cell
            gvr.Cells[5].SetFocus();

            //Getting Row Count in grid
            int gridRowCount = rgv.Rows.Count;

            if (gridRowCount == 1)
            {
                rowIndex = gridRowCount - gridRowCount;
            }

            else
            {
                rowIndex = gridRowCount;
            }

            if (rowIndex == 0)
            {
                //Wait for Open Button Exists in DOM 
                SharedElement.P2P_Application.SilverlightApp.P2P_Configuration_Tool_Entity_Configuration_Expression_OpenButton.Wait.ForExists(Globals.timeOut);

                //Click on Open Button 
                SharedElement.P2P_Application.SilverlightApp.P2P_Configuration_Tool_Entity_Configuration_Expression_OpenButton.User.Click();
            }

            else
            {
                //Keyboard action
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

                //Keyboard action
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);
            }

            //Wait for TextBox Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Configuration_Tool_Entity_Configuration_Filter_Expression_TextBox.Wait.ForExists(Globals.navigationTimeOut);

            //Set Focus on TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_Configuration_Tool_Entity_Configuration_Filter_Expression_TextBox.SetFocus();

            //Click on TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_Configuration_Tool_Entity_Configuration_Filter_Expression_TextBox.User.Click();

            //Input Text in Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Configuration_Tool_Entity_Configuration_Filter_Expression_TextBox.User.TypeText(expressionEditorRule, 50);

            //Wait for OK Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            //Wait for Entity Configuration Type Rule Grid Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_RulesGrid.Wait.ForExists(Globals.timeOut);

            //Click on Cell
            gvr.Cells[5].User.Click(MouseClickType.LeftDoubleClick);

            //Keyboard action
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Keyboard action
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Keyboard action
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

            //Refresh Grid
            rgv.Refresh();

            //Refresh Cell
            gvr.Cells[7].Refresh();

            //Wait for Cell
            gvr.Cells[7].Wait.ForExists(Globals.timeOut);

            //Click on Cell
            gvr.Cells[7].User.Click(MouseClickType.LeftDoubleClick);

            //Set focus in cell
            gvr.Cells[5].SetFocus();

            if (rowIndex == 0)
            {
                //Wait for Open Button Exists in DOM 
                SharedElement.P2P_Application.SilverlightApp.P2P_Configuration_Tool_ResourceID_OpenButton.Wait.ForExists(Globals.timeOut);

                //Click on Open Button 
                SharedElement.P2P_Application.SilverlightApp.P2P_Configuration_Tool_ResourceID_OpenButton.User.Click();
            }

            else
            {
                //Keyboard action
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);

                //Keyboard action
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);
            }

            //Declaring RadGridView variable
            RadGridView gridControl;

            bool noSearchText;

            do
            {
                //Assigning Grid to RadGridView variable
                gridControl = SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Screen_ResourcesIDGridControl;

                //Refresh Grid
                gridControl.Refresh();

                //find Text
                noSearchText = gridControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains("No search results"));

                //Wait for Resource ID Grid Exists in DOM 
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Screen_ResourcesIDGridControl.Wait.ForExists(Globals.timeOut);
            } while (noSearchText == false);

            //Wait for Resource ID TextBox Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_ResourceIDSearch_TextBox.Wait.ForExists(Globals.timeOut);

            //Set Focus on TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_ResourceIDSearch_TextBox.SetFocus();

            //Click on Resource ID TextBox
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_ResourceIDSearch_TextBox.User.Click();

            //Input Text in Resource ID Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_ResourceIDSearch_TextBox.User.TypeText(swissReferenceNuumberResourceID, 0);

            //Wait for OK Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_SearchButton.Wait.ForExists(Globals.timeOut);

            //Click on OK Button
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Process_SearchButton.User.Click();

            //Wait for OK Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InvoiceDetails_Attachment_OKButton.User.Click();

            //Wait for Entity Configuration Type Rule Grid Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_RulesGrid.Wait.ForExists(Globals.timeOut);

            //Wait for OK Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_RulesGrid_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK Button
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_RulesGrid_OKButton.User.Click();

            if (saveButton.Equals(true))
            {
                //Wait for Save Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_SaveButton.Wait.ForExists(Globals.timeOut);

                //Click on Save Button
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EntityConfiguration_SaveButton.User.Click();
            }
        }

        //Method to Delete Association Mapping from the OM Configuration
        public void P2PConfigurationTool_DeleteAssociationMapping(string adminSite, string invoiceLineField)
        {
            //Wait for Rad Grid to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_OMConfiguration_OMConfigurationsGridViewControl.Wait.ForExists(Globals.timeOut);

            //Click on Invoice Line Field
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_OMConfiguration_OMConfigurationsGridViewControl.Find.ByTextContent(adminSite).As<TextBlock>().User.Click();

            //Wait for Button to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_OMConfiguration_OpenSelectedToolBarButton.Wait.ForExists(Globals.timeOut);

            //Click on Button
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_OMConfiguration_OpenSelectedToolBarButton.User.Click();

            //Calling Handle Busy Indicator
            P2PNavigation.CallBusyIndicator();

            //Wait for Rad Grid to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Configuration_Tool_OM_Mapping_RadGridViewControl.Wait.ForExists(Globals.timeOut);

            if (SharedElement.P2P_Application.SilverlightApp.P2P_Configuration_Tool_OM_Mapping_RadGridViewControl.Rows.Count != 0)
            {
                //Click on Invoice Line Field
                SharedElement.P2P_Application.SilverlightApp.P2P_Configuration_Tool_OM_Mapping_RadGridViewControl.Find.ByTextContent(invoiceLineField).As<TextBlock>().User.Click();

                //Wait for Delete Button to Exists in DOM 
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_OMConfiguration_Mapping_DeleteButton.Wait.ForExists(Globals.timeOut);

                //Set Focus on Delete Button
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_OMConfiguration_Mapping_DeleteButton.SetFocus();

                //Click on Delete Button
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_OMConfiguration_Mapping_DeleteButton.User.Click();

                //Calling busy indicator
                P2PNavigation.CallBusyIndicator();

                //Wait for Button to Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Schema_Save_Button.Wait.ForExists(Globals.timeOut);

                //Click on Button
                SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_Schema_Save_Button.User.Click(MouseClickType.LeftDoubleClick);

                //Added wait in miliseconds to page get load successfully
                //Calling busy indicator              
                System.Threading.Thread.Sleep(60000);
            }
        }

        //Method to Select Edit Type View
        public void P2PConfigurationTool_SelectEditTypeConfiguration(string editTypeConfig)
        {
            //Wait for Entity Type Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EditView_EditTypeViewGridControl.Wait.ForExists(Globals.timeOut);

            //Select the Schema (Invoice) from where Rule is to be Deleted
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EditView_EditTypeViewGridControl.Find.ByTextContent(editTypeConfig).User.Click();
        }

        //Method to make Text box enable
        public void P2PConfigurationTool_EditViewItemConfiguration(string entityConfigItem, string readonlyValue)
        {
            //Declare 'found' as boolean variable
            bool found = false;

            //Declare 'gridControl' as RadGridView variable
            RadGridView gridControl = null;

            //Wait for Grid Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EditView_EditViewItemsGridViewControl.Wait.ForExists(Globals.timeOut);

            //Set focus on grid
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EditView_EditViewItemsGridViewControl.SetFocus();
                          
            //Keyboard Action
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.PageUp);
            
            //Declaring RadGridView variable
            gridControl = SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EditView_EditViewItemsGridViewControl;

            //Click on 2nd Row
            gridControl.Rows[3].User.Click();   

            do
            {
                //Keyboard Action
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.PageDown);

                //Wait for grid to load
                gridControl.Wait.ForExists(Globals.timeOut);

                //Refresh Grid
                gridControl.Refresh();
                
                //Find and Save result in boolean variable
                found = gridControl.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains(entityConfigItem));               

            } while (found == false);

            //Select the Schema (Invoice) from where Rule is to be Deleted
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EditView_EditViewItemsGridViewControl.Find.ByTextContent(entityConfigItem).User.Click(MouseClickType.LeftDoubleClick);

            //Put the row into a variable
            GridViewRow gvr = null;
            
            gvr = gridControl.Rows[gridControl.Rows.Count - 1];

            //Use for loop here to focus and click the check box in a grid
            for (int i = 0; i < 7; i++)
            {
                //Wait for Grid Exist in DOM
                gvr.Wait.ForExists(Globals.timeOut);

                //Press tab to move the location in grid
                Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Tab);
            }
            
            //Press F to move the location in grid
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.F);
            
            //Wait for Save Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EditView_SaveEditViewToolBarButton.Wait.ForExists(Globals.timeOut);

            //Click on Save Button
            SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationTool_EditView_SaveEditViewToolBarButton.User.Click();
        }
    }
}