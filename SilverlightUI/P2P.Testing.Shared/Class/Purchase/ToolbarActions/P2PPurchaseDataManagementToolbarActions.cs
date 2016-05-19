using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using Telerik.WebAii.Controls.Xaml;

namespace P2P.Testing.Shared.Class.Purchase.ToolbarActions
{
    public class P2PPurchaseDataManagementToolbarActions : BaseWebAiiTest
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

        //Method to delete welcome page
        public void P2PWelcomePageAdditionalActionsDelete()
        {
            //Wait for Additional actions dropdown to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Click on Additional actions dropdown
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            //wait for delete button to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_MoreActions_DeleteButton.Wait.ForExists(Globals.timeOut);

            //Click on delete button
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_MoreActions_DeleteButton.User.Click();

            //wait for yes button to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on yes button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
        }

        //Method to Click on Save button 
        public void P2PPurchaseItemSaveButton()
        {
            //Wait for element to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_SaveButton.Wait.ForExists(Globals.timeOut);

            //Click on save button
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_DetailsPage_SaveButton.User.Click();
        }


        //Obselete: Now Common method used of IA (Back, Refresh, open selected button)
        //Method to click on Back Button of the Welcome Page
        //public void P2PPurchaseDataManagementBackButton()
        //{
        //    //Wait for back button to Exists in DOM
        //    SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_BackButton.Wait.ForExists(Globals.timeOut);

        //    //Click on back button
        //    SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_BackButton.User.Click();
        //}

        //Obselete: Method to click on Back Button of the Purchase Items
        //public void P2PPurchaseItemBackButton()
        //{
            //Wait for back button to Exists in DOM
            //SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseItems_BackButton.Wait.ForExists(Globals.timeOut);

            //Click on back button
            //SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseItems_BackButton.User.Click();
        //}

        //Method to click on Open Selected button: Welcome Page
        //public void P2PWelcomePageOpenSelected()
        //{
        //    //Wait for Open Selected button to Exists in DOM
        //    SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_OpenSelectedButton.Wait.ForExists(Globals.timeOut);

        //    //Click on Open Selected button
        //    SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_OpenSelectedButton.User.Click();
        //}

        ////Method to click on Open Selected button :Purchase Items
        //public void P2PPurchaseItemOpenSelected()
        //{
        //    //Wait for Open Selected button to Exists in DOM
        //    SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_OpenSelectedButton.Wait.ForExists(Globals.timeOut);

        //    //Click on Open Selected button
        //    SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_OpenSelectedButton.User.Click();
        //}

        //  //Method for  Refresh the Grid  
        //public void P2PWelcomePageRefreshGrid()
        //{
        //    // Wait for Refresh Button Exist in DOM
        //    SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_RefreshButton.Wait.ForExists(Globals.timeOut);

        //    //Click on Refresh Button
        //    SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_RefreshButton.User.Click();
        //}

        ////Method for  Refresh the Grid  
        //public void P2PPurchaseItemRefreshGrid()
        //{
        //    // Wait for Refresh Button Exist in DOM
        //    SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_RefreshButton.Wait.ForExists(Globals.timeOut);

        //    //Click on Refresh Button
        //    SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_ToolBar_RefreshButton.User.Click();
        //}

       
    }
}
