using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using Telerik.WebAii.Controls.Xaml.Wpf;


namespace P2P.Testing.Shared.Class.DataManagement
{
    public class P2PDataManagementVerifications : BaseWebAiiTest
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

        //Method to Verify Delete Button is disabled for Inherited Plan Groups in Child Company's
        public void P2PVerifyDeleteDisabled()
        {
            //Wait for Delete Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PaymentPlans_DeleteGroupButton.Wait.ForExists(Globals.timeOut);

            //Get state of delete button
            bool enabled = SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PaymentPlans_DeleteGroupButton.IsEnabled;

            //Verify button is disabled
            if (enabled.Equals(false))
            {
                //Write the Result in log file when Verification is Pass
                Manager.Current.Log.WriteLine(LogType.Information, " Delete Button is disabled for Inherited Parent Plan Group. Verification Passes !");
            }
            else
            {
                //Write the Result in log file when Verification is Fail
                Manager.Current.Log.WriteLine(LogType.Error, "Error: Delete Button is enabled for Inherited Parent Plan Group. Verification Fails !!");
            }
        }

        //Method to Verify Plan Group is deleted
        public void P2PVerifyPlanDeleted(string planName, string headerName)
        {
            //Wait for grid to Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PaymentPlans_PlanGroup_RadGridView.Wait.ForExists(Globals.timeOut);

            //Take reference of grid          
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PaymentPlans_PlanGroup_RadGridView.CastAs<RadGridView>();

            if (grid.Rows.Count.Equals(0))
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, " Plan Group Grid is empty. Plan Group : (" + planName + ") has been deleted. Verification Passes !");
            }
            else
            {
                //Check value in Exact Column using Column Header Name Value
                GridViewHeaderCell header = grid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == headerName);
                //Count all rows and save it in index variable
                int index = header.Index;

                //Verify Results in each row in grid
                foreach (GridViewRow row in grid.Rows)
                {
                    //Compare Each Row Value for all grids
                    if (row.Cells[index].TextBlockContent == planName)
                    {
                        //Write the log if Verification Fail
                        Manager.Current.Log.WriteLine(LogType.Error, " Error: Plan Group (" + planName + "), still exists under column: (" + headerName + "). Verification Failed!!");                       
                    }
                }

                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Information, " Plan Group (" + planName + "), doesnt exist in grid. Verification Passes !");
            }
        }

        //Method to Verify Plan Group shows
        public void P2PVerifyPlanShows(string planName, string headerName)
        {
            //Wait for grid to Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PaymentPlans_PlanGroup_RadGridView.Wait.ForExists(Globals.timeOut);

            //Take reference of grid          
            RadGridView grid = SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PaymentPlans_PlanGroup_RadGridView.CastAs<RadGridView>();

            if (grid.Rows.Count.Equals(0))
            {
                //Write the log if Verification Pass
                Manager.Current.Log.WriteLine(LogType.Error, " Error: Plan Group Grid is empty. Plan Group : (" + planName + ") doesnt show. Verification Fails !!");
            }
            else
            {
                //Check value in Exact Column using Column Header Name Value
                GridViewHeaderCell header = grid.HeaderRow.HeaderCells.FirstOrDefault(headerElement => headerElement.TextBlockContent == headerName);
                //Count all rows and save it in index variable
                int index = header.Index;

                //Verify Results in each row in grid
                foreach (GridViewRow row in grid.Rows)
                {
                    //Compare Each Row Value for all grids
                    if (row.Cells[index].TextBlockContent == planName)
                    {
                        //Write the log if Verification Pass
                        Manager.Current.Log.WriteLine(LogType.Information, " Plan Group (" + planName + "), shows in grid. Verification Passes !");

                        //return to calling function
                        return;                        
                    }
                }
                //Write the log if Verification Fail
                Manager.Current.Log.WriteLine(LogType.Error, " Error: Plan Group (" + planName + "), doesnt show under column: (" + headerName + "). Verification Fails !!");                               
            }
        }
    }
}
