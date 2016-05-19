using System;
using System.Linq;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using Telerik.WebAii.Controls.Xaml;
using System.Windows.Forms;
using ArtOfTest.WebAii.Silverlight;
using System.Globalization;
using E2E.Class;
using ArtOfTest.WebAii.Controls.Xaml.Wpf;


namespace P2P.Testing.Shared.Class.PaymentPlans.MyTasks
{
    public class P2PPaymentPlansMyTasks : BaseWebAiiTest
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

        //Method to Click n Pending/Info Tab : Payment Plan tab
        public void P2PMyTasksPaymentPlanClickOnTab(string tabName)
        {
            switch (tabName.ToUpper())
            {
                case "PENDING":
                    {
                        //Wait for element to Exists in DOM Tree
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MyTasks_Pending_RadioButton.Wait.ForExists(Globals.timeOut);

                        //Click on Pending Tab
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MyTasks_Pending_RadioButton.User.Click();

                        break;
                    }
                case "INFO":
                    {
                        //Wait for element to Exists in DOM Tree
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MyTasks_Info_RadioButton.Wait.ForExists(Globals.timeOut);

                        //Click on Info Tab
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MyTasks_Info_RadioButton.User.Click();

                        break;
                    }
                case "ONHOLD":
                    {
                        //Wait for element to Exists in DOM Tree
                        SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=OnHold")).Wait.ForExists(Globals.timeOut);

                        //Click on Info Tab
                        SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=OnHold")).User.Click();
                        
                        break;
                    }
                default:
                    {
                        //Log Failure for Tab item not found
                        Manager.Current.Log.WriteLine(LogType.Error, "My Tasks: Payment Plan Tab: Pending/OnHold Tab not found, Verification Failed!!!!");
                        
                        break;
                    }
            }
        }
        
        //Method to Get Count of Tab
        public int P2PMyTasksPaymentPlanGetTabCount(string tabName, string count = null)
        {
            //Decalre and intialize count
            int countTab = 0;

            switch (tabName.ToUpper())
            {
                case "PENDING":
                    {
                        //Wait for element to Exists in DOM Tree
                        SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MyTasks_Pending_RadioButton.Wait.ForExists(Globals.timeOut);
                        
                        //Get Count                   
                        countTab = Convert.ToInt32(SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MyTasks_Pending_RadioButton.Text.Trim());
                        
                        //Get Pending button
                        ArtOfTest.WebAii.Silverlight.UI.RadioButton fe = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_MyTasks_Pending_RadioButton.CastAs<ArtOfTest.WebAii.Silverlight.UI.RadioButton>();
                        
                        //Calling Fetching Data Method
                        P2P_Utility.FetchingDataFromUI(fe, count);

                        break;
                    }
                case "ON HOLD":
                    {
                        //Wait for Tab
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_OnHoldRadioButton.Wait.ForExists(Globals.timeOut);
                        
                        //Get Count                   
                        countTab = Convert.ToInt32(SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_OnHoldRadioButton.Text.Trim());

                        //Get Pending button
                        ArtOfTest.WebAii.Silverlight.UI.RadioButton fe1 = SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_OnHoldRadioButton.CastAs<ArtOfTest.WebAii.Silverlight.UI.RadioButton>();

                        //Calling Fetching Data Method
                        P2P_Utility.FetchingDataFromUI(fe1, count);
                     
                        break;
                    }
                default:
                    {
                        //Log Failure for Tab item not found
                        Manager.Current.Log.WriteLine(LogType.Error, "My Tasks: Payment Plan Tab: Pending/OnHold Tab not found, Verification Failed!!!!");
                     
                        break;
                    }
            }
            return countTab;
        }

        //Method to Perform actions under "More Actions"
        public void P2PMoreActionsDropdown(string actionMode, string userNameOrComments = null)
        {
            //Wait for Dropdown to load in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Set Focus on Dropdown
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.SetFocus();

            //Click on Dropdown
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            switch (actionMode.ToUpper())
            {
                case "ADD COMMENT":
                    {


                        //break out of loop
                        break;
                    }
                case "FORWARD":
                    {
                        //Wait for button to load in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_MoreActions_ForwardButton.Wait.ForExists(Globals.timeOut);

                        //Click on button
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_MoreActions_ForwardButton.User.Click();                        

                        //Call Method to Select recipient 
                        P2PSelectRecipient(userNameOrComments);

                        //break out of loop
                        break;
                    }
                case "PUT ON HOLD":
                    {
                        //Wait for button to load in DOM
                        SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=PP.Views.MyTasksToolbar.OnHold_Button_Text")).Wait.ForExists(Globals.timeOut);

                        //Click on button
                        SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=PP.Views.MyTasksToolbar.OnHold_Button_Text")).User.Click();
                        //break out of loop
                        break;
                    }
                case "REQUEST PAYMENT PLAN":
                    {
                        //Wait for button to load in DOM
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_Toolbar_RequestPaymentPlanButton.Wait.ForExists(Globals.timeOut);

                        //Click on button
                        SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_Toolbar_RequestPaymentPlanButton.User.Click();
                        //break out of loop
                        break;                    
                    }


                default:
                    {
                        //Log Failure for action not found
                        Manager.Current.Log.WriteLine(LogType.Error, " More Actions Dropdown 'item' requested not found. Item being searched under dropdown: " + actionMode + ". Failure !!");
                        break;
                    }
            }
        }

        //Method to select Recipient
        public void P2PSelectRecipient(string selectUser)
        {
            //Wait for Search User Text Box Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_Collaborate_SelectUsersGrid.Wait.ForExists(Globals.timeOut);
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_SearchUserTextbox.Wait.ForExists(Globals.timeOut);

            //Enter User name in Search User Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_SearchUserTextbox.User.TypeText(selectUser, 50, true);

            //Click on Search User Button to Search User
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SelectRecipient_SearchButton.User.Click();

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_Collaborate_SelectUsersGrid.Refresh();

            TextBlock userCell = null;

            //Find the User Name and Select From the Grid 
            userCell = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(selectUser).As<TextBlock>();

            //Press Enter the Key
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);

            //Click on OK Button to Close the Select Users Pop-Up
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
        }

        //Method to Select My Tasks Toolbar Button
        public void P2PMyTasksSelectToolBarButtons(string button)
        {
            switch (button)
            {
                case "Collaborate":
                    //Wait for element to Exists in DOM Tree
                    SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_Toolbar_CollaborateButton.Wait.ForExists(Globals.timeOut);

                    //Click on Collaborate Button
                    SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_Toolbar_CollaborateButton.User.Click();
                    break;
                case "Save":
                    //Wait for element to Exists in DOM Tree
                    SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_Toolbar_SaveButton.Wait.ForExists(Globals.timeOut);

                    //Click on Save Button
                    SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_Toolbar_SaveButton.User.Click();
                    break;

                case "Review":
                    //Wait for element to Exists in DOM Tree
                    SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_Toolbar_ReviewButton.Wait.ForExists(Globals.timeOut);
                    //Click on Review Button 
                    SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_Toolbar_ReviewButton.User.Click();
                    break;

                case "PaymentPlan":
                    //Click on Save Invoice button Button
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.Wait.ForExists(Globals.timeOut);
                    //Click on Save Invoice button Button
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Save_Button.User.Click();
                    break;                    

                default:
                    //Log Failure for Tab item not found
                    Manager.Current.Log.WriteLine(LogType.Error, "ToolBar Buttons Not Found, Verification Failed!!!!");
                    break;
            }             
        }

        //Method to Select Tabs from the Tab Controls
        public void P2PMyTasksTabControl(string tabItem)
        {
            switch (tabItem)
            {
                case "Payment Schedule":
                    //Wait for element to Exists in DOM Tree
                    SharedElement.P2P_Application.SilverlightApp.P2P_Mytask_HistoryAndCommentsTabItem.Wait.ForExists(Globals.timeOut);

                    //Click on Pending Tab
                    SharedElement.P2P_Application.SilverlightApp.P2P_Mytask_HistoryAndCommentsTabItem.User.Click();
                    break;                
                default:
                    //Log Failure for Tab item not found
                    Manager.Current.Log.WriteLine(LogType.Error, "Tab Item Not Found, Verification Failed!!!!");
                    break;
            }       
        
        }

        //Method to Request the Payment Plan and Set recurrence 
        public void P2PMyTasksRequestPaymentPlan(string comment, string repeat, double range)
        {

            //Wait for element to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_Toolbar_RequestPaymentPlan_RecurrenceButton.Wait.ForExists(Globals.timeOut);

            //Click on Recurrence Button
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_Toolbar_RequestPaymentPlan_RecurrenceButton.User.Click();

            //Wait for Dropdown Exists in DOM
             SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_Toolbar_RequestPaymentPlan_RecurrenceRepeatCombobox.Wait.ForExists(Globals.timeOut);

             //Select Value from the Dropdowm             
             SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_Toolbar_RequestPaymentPlan_RecurrenceRepeatCombobox.OpenDropDown(true);
            
            //Select Value from the Dropdowm 
             SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_Toolbar_RequestPaymentPlan_RecurrenceRepeatCombobox.SelectItemByText(true, repeat, true);
            
            //Wait for P2P_MyTasks_Toolbar_RequestPaymentPlan_RecurrenceRepeatEndAfterTextbox Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_Toolbar_RequestPaymentPlan_RecurrenceRepeatEndAfterTextbox.Wait.ForExists(Globals.timeOut);

            //Set Focus on End After Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_Toolbar_RequestPaymentPlan_RecurrenceRepeatEndAfterTextbox.SetFocus();
            //Enter the Value in Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_Toolbar_RequestPaymentPlan_RecurrenceRepeatEndAfterTextbox.Text = range.ToString();

            //Wait for OK Button to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_Toolbar_RequestPaymentPlan_RecurrenceOKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_Toolbar_RequestPaymentPlan_RecurrenceOKButton.User.Click();

            //Wait for Comment Box Exists on DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);

            //Enter the Comments in the Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(comment, 50);

            //Wait for OK Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
        }


        //************** Below functions are uses in E2E Test Scripts, Please do not modify**********
        //Method to Add Comments In A Task
        public void P2PMyTaskAddNewComment(string comment, string addComment)
        {
            //Wait for button is load successfully.
            System.Threading.Thread.Sleep(Globals.timeOut);
            //Wait for Tab Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("Name=toolbarItemsControl", "XamlTag=itemscontrol")).Wait.ForExists(Globals.timeOut);
            // Store the tab control into a variable
            ItemsControl tabControl = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("Name=toolbarItemsControl", "XamlTag=itemscontrol")).CastAs<ItemsControl>();
            //Find the Add Comment button to click
            tabControl.Find.ByTextContent(addComment).User.Click();

            //Set Focus on Add comment textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.SetFocus();

            //Entering  the Comment into Add Comment Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(comment.ToString(), 100, true);

            //Wait for OK Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK after adding Comment
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
        }

        //Method to Unhold Task under Workflow Task management Tab
        public void P2PMyTaskPutOnHold(string comment, string putOnHold)
        {
            //Wait for Additional Actions Dropdown button Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Click the Additional Actions drop down
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            //Click on Hold Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Find.ByTextContent(putOnHold).User.Click();

            //Verify That P2P_Invoice_Administration_AddComment_OKButton is enabled or not
            if (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.IsEnabled == false)
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, "P2P_Invoice_Administration_AddComment_OKButton Is  Not enabled verification Successful");
            }

            else
            {
                //Write the log if Verification Fail
                throw new Exception("P2P_Invoice_Administration_AddComment_OKButton Is enabled verification Failed!");
            }

            //If condition is true then execute if block
            // if(putOnHold==null)
            // {
            //Wait for Cancel Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_CancelButton.Wait.ForExists(Globals.timeOut);

            //Click on Cancel Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_CancelButton.User.Click();
            // }

            //Wait for Additional Actions Dropdown button Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Click the Additional Actions drop down
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            //Click on Hold Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Find.ByTextContent(putOnHold).User.Click();

            //Wait for Add Comment Textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);

            //Set focus on Add Comment Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.SetFocus();

            //Click to Add Comment while Task Put On Hold and Entering Text
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(comment.ToString(), 100, true);

            //Wait for OK Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK after adding Comments
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            //Calling busy indicator
            P2PNavigation.CallBusyIndicator();
        }

        //Method to Release From Hold Task
        public void P2PMyTaskReleaseFromHold(string comment, string removePR = null)
        {
            if (removePR == null)
            {
                //Wait for Personnel Mode My Task button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_ReviewButton.Wait.ForExists(Globals.timeOut);

                //Set Focus on My Task button
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_ReviewButton.SetFocus();

                //Click on Personnel Mode My Task button
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_ReviewButton.User.Click();
            }
            else
            {
                //Wait for Personnel Mode My Task button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_OnHoldRadioButton.Wait.ForExists(Globals.timeOut);

                //Set Focus on My Task button
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_OnHoldRadioButton.SetFocus();

                //Click on Personnel Mode My Task button
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_OnHoldRadioButton.User.Click();


                //Wait for ReleaseFromhold button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.Find.ByAutomationId(removePR).Wait.ForExists(Globals.timeOut);
                //Click on ReleaseFromhold button
                SharedElement.P2P_Application.SilverlightApp.Find.ByAutomationId(removePR).User.Click();

            }

            //Wait for Add Comment Textbox to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);

            //Set Focus to Add Comment
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.SetFocus();

            //Click to Add Comment while Task Put On Hold
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(comment.ToString(), 50, true);

            //Wait for OK Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            //Set Focus on Ok button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.SetFocus();

            //Click on OK after adding Comments
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            //Calling busy indicator
            P2PNavigation.CallBusyIndicator();

        }

        //**************
    }
}
