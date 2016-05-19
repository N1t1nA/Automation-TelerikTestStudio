using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Design.Execution;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Silverlight.UI;
using Telerik.TestingFramework.Controls.KendoUI;
using Telerik.WebAii.Controls.Html;
using Telerik.WebAii.Controls.Xaml;
using P2P.Testing.Shared.Class;

namespace P2P.Testing.Shared.Script
{

    public class busyindicatortest : BaseWebAiiTest
    {
        #region [ Dynamic Pages Reference ]

        private Pages _pages;

        /// <summary>
        /// Gets the Pages object that has references
        /// to all the elements, frames or regions
        /// in this project.
        /// </summary>
		public Pages Pages
		{
			get
			{
				if (_pages == null)
				{
					_pages = new Pages(Manager.Current);
				}
				return _pages;
			}
        }

        #endregion

        [CodedStep("Login into P2P")]
        public void LoginintoP2P()
        {

            var timeout=10000;
            //Maximize the Browser
            ActiveBrowser.Window.Maximize();
            //Launch the URL 
            ActiveBrowser.NavigateTo("/Portal?forms=1&sl=1");
            //Wait for UserName Textbox Exists in DOM
            Pages.P2P_Login.P2P_Login_Username.Wait.ForExists(timeout);
            //Enter UserName
            Pages.P2P_Login.P2P_Login_Username.Text = "basware\\ilkka";
            //Wait for Password Textbox Exists in DOM
            Pages.P2P_Login.P2P_Login_Password.Wait.ForExists(timeout);
            //Enter Password
            Pages.P2P_Login.P2P_Login_Password.Text = "";
            //Wait for Login Button Exists in DOM
            //_loginPage.P2P_Login.P2P_Login_Submit.Wait.ForExists(timeout);
            //Click on Login Button
            Pages.P2P_Login.P2P_Login_Submit.Click();

          
        }

        [CodedStep("Navigate to Transfer", RequiresSilverlight = true)]
        public void NavigateToTransfer()
        {
            //Create object for P2PNavigation class
            P2PNavigation navigateToTransfer = new P2PNavigation();

            //Call the Method to Navigate To Transfer Page 
            navigateToTransfer.NavigateInvoiceAdministrationToTransfer();
        }

        [CodedStep(@"Wait for StartupBusyIndicatorBusyindicator.IsBusy 'Equal' 'False'", RequiresSilverlight = true)]
        public void busyindicatortest_CodedStep()
        {

            // Wait for StartupBusyIndicatorBusyindicator.IsBusy 'Equal' 'False'
            //Wait.For<ArtOfTest.WebAii.Silverlight.UI.ContentControl>((a_) => ((a_.IsBusy == null) ? (false == null) : a_.IsBusy.Equals(false)), Pages.P2P_Application.SilverlightApp.StartupBusyIndicatorBusyindicator, false, 10000);

            //wait for busyindicator to exist not in the UI
            Wait.For<ArtOfTest.WebAii.Silverlight.UI.ContentControl>(
                (a_) =>
                {
                    bool? result = (bool?)a_.GetProperty(new AutomationProperty("IsBusy", typeof(bool?)));
                    return result.HasValue && !result.Value;
                },
                Pages.P2P_Application.SilverlightApp.StartupBusyIndicatorBusyindicator, false, 10000);

         
        }
    }
}
