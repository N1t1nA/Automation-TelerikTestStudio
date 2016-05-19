using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Controls.Xaml.Wpf;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Silverlight;

namespace P2P.Testing.Shared.Class.Purchase.ToolbarActions
{
    public class P2PPurchaseLineRquisitionToolBarActions
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

        //Method to Select and Approve Requisition Line
        public void P2PMyTasksPurchaseRequisitionApproveAction(string purchaseRequisitionLine= null)
        {
            //select a single PR Line
            if (purchaseRequisitionLine != null)
            {
                //Wait for P2P_MyTasks_PurchaseRequisition_LinesListViewGridControl to Exists in DOM Tree
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_LinesListViewGridControl.Wait.ForExists(Globals.timeOut);

                //Find the Header cell Value as PR Line
                TextBlock prHeaderCell = SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_LinesListViewGridControl.Find.ByTextContent(purchaseRequisitionLine).As<TextBlock>();
                                
                //Click on PR Title Header Cell
                prHeaderCell.User.Click();
            }

            //Wait for P2P_MyTasks_PurchaseRequisition_ApproveToolbarButton to Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_ApproveToolbarButton.Wait.ForExists(Globals.timeOut);

            //Click on P2P_MyTasks_PurchaseRequisition_ApproveToolbarButton
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_ApproveToolbarButton.User.Click();
        }

        //Method to Select and Reject Requisition Line
        public void P2PMyTasksPurchaseRequisitionRejectAction(string purchaseRequisitionLine, string rejectionComment = null)
        {
            //Wait for P2P_MyTasks_PurchaseRequisition_LinesListViewGridControl to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_LinesListViewGridControl.Wait.ForExists(Globals.timeOut);

            //Find the Header cell Value as PR Line
            TextBlock prHeaderCell = SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_LinesListViewGridControl.Find.ByTextContent(purchaseRequisitionLine).As<TextBlock>();

            //Click on PR Title Header Cell
            prHeaderCell.User.Click();

            if (rejectionComment != null)
            {
                //Wait for P2P_MyTasks_PurchaseRequisition_RejectToolbarButton to Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_RejectToolbarButton.Wait.ForExists(Globals.timeOut);

                //Click on P2P_MyTasks_PurchaseRequisition_RejectToolbarButton
                SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchaseRequisition_RejectToolbarButton.User.Click();

                //Wait for P2P_Invoice_Administration_AddComment_TextBox to Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);

                //Set Focus for P2P_Invoice_Administration_AddComment_TextBox
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.SetFocus();

                //Input Description for P2P_Invoice_Administration_AddComment_TextBox
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(rejectionComment, 50);

                //Wait for P2P_Invoice_Administration_AddComment_OKButton to Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

                //Click on P2P_Invoice_Administration_AddComment_OKButton button
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
            }
        }

        //Method to Confirm Purchase Requisition
        public void P2PMyTasksPurchaseRequisitionConfirmAction()
        {
            //Wait for P2P_WebShop_PurchaseRequisition_ConfirmButton to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_ConfirmButton.Wait.ForExists(Globals.timeOut);

            //Click on P2P_WebShop_PurchaseRequisition_ConfirmButton
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_ConfirmButton.User.Click();
        }

        //Method to Return Purchase Requisition
        public void P2PMyTasksPurchaseRequisitionReturn(string comment)
        {
            //Wait for More actions dropdown
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Click on More actions dropdown
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            //Wait for P2P_MyTasks_PurchasRequisition_MoreActions_ReturnButton to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchasRequisition_MoreActions_ReturnButton.Wait.ForExists(Globals.timeOut);

            //Click on P2P_MyTasks_PurchasRequisition_MoreActions_ReturnButton
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchasRequisition_MoreActions_ReturnButton.User.Click();

            //Wait for Textbox to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);

            //enter comments in Textbox
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(comment, 50);

            //Wait for Ok Button to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on Ok Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
        }
       
    }
}
