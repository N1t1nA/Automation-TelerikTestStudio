using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using Telerik.WebAii.Controls.Xaml;

namespace P2P.Testing.Shared.Class.Purchase.GoodsReceipts
{
    public class P2PGoodsReceiptsToolbarActions : BaseWebAiiTest
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

        //Method for Open Selected GR
        public void P2PGoodsReceiptsOpenSelected()
        {
            //Wait for Grid Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_ListViewGridcontrol.Wait.ForExists(Globals.timeOut);

            //Click on selected GR item
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_ListViewGridcontrol.User.Click();

        }
    }
}
