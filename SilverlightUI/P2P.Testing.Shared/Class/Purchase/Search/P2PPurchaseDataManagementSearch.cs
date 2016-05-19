using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using Telerik.WebAii.Controls.Xaml;
using ArtOfTest.WebAii.Controls.Xaml.Wpf;

namespace P2P.Testing.Shared.Class.Purchase.Search
{
    public class P2PPurchaseDataManagementSearch : BaseWebAiiTest
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

        //Global Wait Time 
        readonly int timeOut = 3000;

        //Method to select filters on Welcome Page
        public void P2PPDMWelcomePageSelectFilter(string filterName)
        {
            //Wait for Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_InActiveCheckBox.Wait.ForExists(timeOut);
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_PersonalMode_SearchFilters_PendingCheckBox.Wait.ForExists(timeOut);

            //Condition for the "Uncheck" All CheckBoxes           
            if (SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_InActiveCheckBox.IsChecked.Equals(true))
            {
                // Click the invalid Check Box to uncheck it
                SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_InActiveCheckBox.User.Click();
            }

            if (SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_PersonalMode_SearchFilters_PendingCheckBox.IsChecked.Equals(true))
            {
                // Click the active Check Box to unselect it
                SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_PersonalMode_SearchFilters_PendingCheckBox.User.Click();
            }

            switch (filterName.ToUpper())
            {
                case "ACTIVE":
                    {
                        if (SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_PersonalMode_SearchFilters_PendingCheckBox.IsChecked.Equals(false))
                        {
                            // Click the active Check Box to select it
                            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_PersonalMode_SearchFilters_PendingCheckBox.User.Click();
                        }

                        break;
                    }
                case "INACTIVE":
                    {
                        if (SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_InActiveCheckBox.IsChecked.Equals(false))
                        {
                            // Click the inactive Check Box to select it
                            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePage_InActiveCheckBox.User.Click();
                        }

                        break;
                    }
                case "NO FILTER":
                    {
                        break;
                    }
                default:
                    {
                        //Log error if filter not found on page.
                        Manager.Current.Log.WriteLine(LogType.Error, "  Filter on Welcome Page Not Found. Please Check!!");

                        break;
                    }
            }

        }

        //Method to Click on  Grid Cell 
        public void P2PPDMPurchaseItemsGridClickInTheCell(string gridCell)
        {
            //Wait for Grid Control Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PurchaseDataManagement_PurchaseItems_GridViewControl.Wait.ForExists(timeOut);

            //Find the cell Value in Grid
            TextBlock gridCellValue = SharedElement.P2P_Application.SilverlightApp.P2P_PurchaseDataManagement_PurchaseItems_GridViewControl.Find.ByTextContent(gridCell).As<TextBlock>();

            //Click on Cell 
            gridCellValue.User.Click();

        }

    }
}
