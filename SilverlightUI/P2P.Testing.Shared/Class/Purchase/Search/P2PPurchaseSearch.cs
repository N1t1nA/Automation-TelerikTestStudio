using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using Telerik.WebAii.Controls.Xaml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArtOfTest.Common.Exceptions;
using System.Windows.Forms;

namespace P2P.Testing.Shared.Class.Purchase.Search
{
    public class P2PPurchaseSearch : BaseWebAiiTest
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
               
        //Method to select filters on PurchaseRequisitions Page
        public void P2PPurchaseRequisitionsSelectFilter(string filterName = null)
        {
            //Wait for checkbox to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_DraftCheckBox.Wait.ForExists(Globals.timeOut);
            SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_WaitingForApprovalCheckBox.Wait.ForExists(Globals.timeOut);
            SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_InOrderingCheckBox.Wait.ForExists(Globals.timeOut);
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_TransferCheckBox.Wait.ForExists(Globals.timeOut);
            SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_RemovedCheckBox.Wait.ForExists(Globals.timeOut);

            //Condition for the "Uncheck" All CheckBoxes
            if (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_DraftCheckBox.IsChecked.Equals(true))
            {
                // Click the Check Box to uncheck it
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_DraftCheckBox.User.Click();
            }

            if (SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_WaitingForApprovalCheckBox.IsChecked.Equals(true))
            {
                // Click the Check Box to unselect it
                SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_WaitingForApprovalCheckBox.User.Click();
            }

            if (SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_InOrderingCheckBox.IsChecked.Equals(true))
            {
                // Click the Check Box to uncheck it
                SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_InOrderingCheckBox.User.Click();
            }

            if (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_TransferCheckBox.IsChecked.Equals(true))
            {
                // Click the Check Box to uncheck it
                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_TransferCheckBox.User.Click();
            }

            if (SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_RemovedCheckBox.IsChecked.Equals(true))
            {
                // Click the Check Box to uncheck it
                SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_RemovedCheckBox.User.Click();
            }

            if (filterName != null)
            {
                switch (filterName.ToUpper())
                {
                    case "DRAFT":
                        {
                            //Condition for the "Uncheck" All CheckBoxes expect DRAFT checkbox
                            if (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_DraftCheckBox.IsChecked.Equals(false))
                            {
                                // Click the Check Box to select it
                                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_DraftCheckBox.User.Click();
                            }
                            break;
                        }
                    case "WAITINGFORAPPROVAL":
                        {
                            //Condition for the "Uncheck" All CheckBoxes expect RETURNED checkbox
                            if (SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_WaitingForApprovalCheckBox.IsChecked.Equals(false))
                            {
                                // Click the Check Box to select it
                                SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_WaitingForApprovalCheckBox.User.Click();
                            }
                            break;
                        }
                    case "INORDERING":
                        {
                            //Condition for the "Uncheck" All CheckBoxes expect IN WORKFLOW checkbox
                            if (SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_InOrderingCheckBox.IsChecked.Equals(false))
                            {
                                // Click the Check Box to select it
                                SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_InOrderingCheckBox.User.Click();
                            }
                            break;
                        }
                    case "PROCESSED":
                        {
                            //Condition for the "Uncheck" All CheckBoxes expect APPROVED checkbox
                            if (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_TransferCheckBox.IsChecked.Equals(false))
                            {
                                // Click the Check Box to select it
                                SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search_TransferCheckBox.User.Click();
                            }
                            break;
                        }
                    case "REMOVED":
                        {
                            //Condition for the "Uncheck" All CheckBoxes expect REJECTED checkbox
                            if (SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_RemovedCheckBox.IsChecked.Equals(false))
                            {
                                // Click the Check Box to select it
                                SharedElement.P2P_Application.SilverlightApp.P2P_Search_PurchaseRequisition_RemovedCheckBox.User.Click();
                            }
                            break;
                        }  
                    default:
                        {
                            //Log error if filter not found on page.
                            Manager.Current.Log.WriteLine(LogType.Error, "  Filter on Purchase Requisitions Not Found. Please Check!!");

                            break;
                        }
                }
            }

        }
    }
}
