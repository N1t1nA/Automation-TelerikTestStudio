using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using Telerik.WebAii.Controls.Xaml;
using System.Windows.Forms;

namespace P2P.Testing.Shared.Class.Purchase.GoodsReceipts
{
    public class P2PGoodsReceiptsSearch : BaseWebAiiTest
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

        //Method to Click n Pending/Partly Received Tab
        public void P2PMyTasksGRClickOnTab(string tabName)
        {
            switch (tabName.ToUpper())
            {
                case "PENDING":
                    //Wait for element to Exists in DOM Tree           
                    SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PendingFilterButton.Wait.ForExists(Globals.timeOut);

                    //Click on Pending Tab
                    SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PendingFilterButton.User.Click();
                    
                    break;
                case "PARTLY RECEIVED":         
                    //Wait Button to Load in DOM
                    SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=rdoReceived")).Wait.ForExists(Globals.timeOut);

                    //Click on PARTLY RECEIVED Tab
                    SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_PartlyReceived_RadioButton.User.Click();
                    
                    break;
                default:
                    //Throw Execption if Verification Fail
                    throw new Exception("My Tasks: Goods Receipts : Pending/ Partly Received Tab not found, Verification Failed!!");
            }
        }
    }
}
