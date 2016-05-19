using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtOfTest.WebAii.Controls.Xaml.Wpf;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Design;

namespace P2P.Testing.Shared.Class.Purchase.ToolbarActions
{
    public class P2PPersonalModeSearch : BaseWebAiiTest
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

        //Method to Verify Open Searched Purchase Orders
        public void P2POpenSearchedPurchaseOrder(string purchaseOrderNumber)
        {
            if (SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PurchaseRequisitions_VerticalMenuItem.IsChecked.Equals(true))
            {
                // Wait for P2P_PersonalModeSearch_PurchaseRequisitions_SearchGridView Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PurchaseRequisitions_SearchGridView.Wait.ForExists(Globals.timeOut);
            }

            else
            {
                // Wait for P2P_MyTasks_PurchaseOrders_GridViewControl Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrders_GridViewControl.Wait.ForExists(Globals.timeOut);
            }
                
            //Find Purchase Order as TextBlock
            TextBlock selectPurchaseOrder = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(purchaseOrderNumber).As<TextBlock>();

            //Select the Purchas eOrder by the User
            selectPurchaseOrder.User.Click();

            //Wait for PO Open Selected
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_OpenSelectedButton.Wait.ForExists(Globals.timeOut);

            //Set focus on PO Open Selected
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_OpenSelectedButton.SetFocus();

            //Click on PO Open Selected
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_OpenSelectedButton.User.Click();
        }
    }
}
