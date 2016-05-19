using System;
using System.Linq;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Silverlight.UI;
using System.Windows.Forms;
using System.Threading;
using P2P.Testing.Shared.Class;

namespace P2P.Testing.Shared.Class.InvoiceAdministration.Support
{
   public class P2PCollaborate: BaseWebAiiTest
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

        //Method to Collaborate - New Announcement 
        public void CollaborateNewAnnouncement(string announcementText, string company)
        {
            //Create object of Framework Element Class 
            FrameworkElement fe = null;
            //variable for loop count
            int loopCounter =0 ;

            do
            {
                //increment counter
                loopCounter = loopCounter + 1;

                //Wait for New Announcement Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Collaborate_NewAnnouncement_Button.Wait.ForExists(Globals.timeOut);

                //Set Focus on New Announcement Link Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Collaborate_NewAnnouncement_Button.SetFocus();

                //Click on New Announcement Link to Start New Announcement
                SharedElement.P2P_Application.SilverlightApp.P2P_Collaborate_NewAnnouncement_Button.User.Click(MouseClickType.LeftClick);

                //Get the framework element ie textbox
                fe = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=BroadCastMessageText", "XamlTag=textbox"));

                //break out loop
                if (loopCounter == 10)
                {
                    //Write the log if Verification Fail
                    Manager.Current.Log.WriteLine(LogType.Error, " Collaborate New Announcement fails!! TextBox not visible after clicking on New announcement link.");
                }               
            } while (fe == null);                   

            //Wait for BroadCast Message Textbox Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_Collaborate_BroadCastMessageTextbox.Wait.ForExists(Globals.handleTime);

            //Set Focus on New Announcement Link Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_Collaborate_BroadCastMessageTextbox.SetFocus();
            
            //Enter the Announcement Text
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_Collaborate_BroadCastMessageTextbox.User.TypeText(announcementText, 50);

            //Press Enter Button
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);
                       
            do
            {
                //Wait for Organization Tree to Exists in Select Organization Unit Dialog
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_OrganisationUnitTreeView.Wait.ForVisible(Globals.timeOut);

                //Find the Company from the Organization Unit Picker and Select 
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_OrganisationUnitTreeView.Find.ByTextContent(company).User.Click();

                //click checkbox
                SharedElement.P2P_Application.SilverlightApp.P2P_SelectOrganization_CheckBox.User.Click();

            } while (SharedElement.P2P_Application.SilverlightApp.P2P_SelectOrganization_CheckBox.IsChecked == false);

            //Wait for OK Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnit_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK Button on Select Organization Unit Dialog
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Company_SelectOrganizationUnit_OKButton.User.Click();
        }

        //Method to Collaborate - New Discussion 
        public void CollaborateNewDiscussion(string selectUser, string discussionText)
        {
            //Wait for link to appear
            P2PNavigation.CallBusyIndicator();

            //Wait for New Discussion Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_Collaborate_NewDiscussionLink.Wait.ForExists(Globals.timeOut);

            //Click on New Discussion Link to Start New Discussion
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_Collaborate_NewDiscussionLink.User.Click();

            //Wait for Grid to load. Use navigationTimeOut as grid takes time to load
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_Collaborate_SelectUsersGrid.Wait.ForExists(Globals.navigationTimeOut);

            //Wait for Grid to load 
            P2PNavigation.CallBusyIndicator();   
            
            //Wait for Search User Text Box Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_SearchUserTextbox.Wait.ForExists(Globals.timeOut);

            //Enter User name in Search User Text Box44
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Collaborate_SelectUsers_SearchUserTextbox.User.TypeText(selectUser, 70);

            //Wait for Search Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SelectRecipient_SearchButton.Wait.ForExists(Globals.timeOut);
            
            //Click on Search User Button to Search User            
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SelectRecipient_SearchButton.User.Click();

            //Wait for Grid to load 
            //P2PNavigation.CallBusyIndicator();           
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_Collaborate_SelectUsersGrid.Wait.ForExists(Globals.timeOut);         

            //Find the User Name and Select From the Grid 
            TextBlock userCell = SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer_Collaborate_SelectUsersGrid.Find.ByTextContent(selectUser).As<TextBlock>();

            //Select the User
            userCell.User.Click(MouseClickType.LeftDoubleClick);
           
            //Wait for exists
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK Button to Close the Select Users Pop-Up
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();

            //Wait after Pressing Enter
            P2PNavigation.CallBusyIndicator();

            int i = 0;
            do
            {
                i = i + 1;

                //Wait for Discussion Textbox for Exists
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CollaboratePro_NewDiscussionChatMessageTextbox.Wait.ForExists(Globals.navigationTimeOut);               
                
            }while ((SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CollaboratePro_NewDiscussionChatMessageTextbox.IsVisible.Equals(false)) || (i == 9));

            //Enter the Discussion Text
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_CollaboratePro_NewDiscussionChatMessageTextbox.User.TypeText(discussionText, 50);
                    
            //Press Enter Button
            Manager.Current.Desktop.KeyBoard.KeyPress(Keys.Enter);

            //Wait after Pressing Enter
            P2PNavigation.CallBusyIndicator();            
        }
    }
}
