using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Core;

namespace P2P.Testing.Shared.Class.Purchase.ToolbarActions
{
   public class P2PPurchaseOrdersToolbarActions
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

        //Method to Click Confirm Button
        public void P2PMyTasksPurchaseOrdersConfirm()
        {
            //Wait for Check Button to load in Dom
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_CheckButton.Wait.ForExists(Globals.timeOut);

            //Set Focus on check Button
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_CheckButton.SetFocus();

            //Click on Check Button
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_CheckButton.User.Click();
        }

        //Method to Click Remove Button
        public void P2PMyTasksPurchaseOrdersRemove()
        {
            //Wait for Remove Button to load in Dom
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_RemoveButton.Wait.ForExists(Globals.timeOut);

            //Set Focus on Remove Button
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_RemoveButton.SetFocus();

            //Click on Remove Button
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseOrder_RemoveButton.User.Click();
        }
    }
}
