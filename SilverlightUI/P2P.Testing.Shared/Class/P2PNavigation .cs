using System;
using System.Linq;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Silverlight.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using ArtOfTest.WebAii.Controls.Xaml;
using System.Collections.Generic;
using Telerik.WebAii.Controls.Html;
using System.Reflection;
using System.Collections;
using System.Threading;

namespace P2P.Testing.Shared.Class
{


    public class P2PNavigation : BaseWebAiiTest
    {

        //#region [ Dynamic Pages Reference ]

        //private Pages _pages;

        ///// <summary>
        ///// Gets the Pages object that has references
        ///// to all the elements, frames or regions
        ///// in this project.
        ///// </summary>
        //public Pages Pages
        //{
        //    get
        //    {
        //        if (_pages == null)
        //        {
        //            _pages = new Pages(Manager.Current);
        //        }
        //        return _pages;
        //    }
        //}

        //#endregion

        #region [ Dynamic Pages Reference for P2P.Testing.Shared]

        private static P2P.Testing.Shared.Pages _sharedElement;

        /// <summary>
        /// Gets the Pages object that has references
        /// to all the elements, frames or regions
        /// in this project.
        /// </summary>
        public static P2P.Testing.Shared.Pages SharedElement
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

        ////Method for Navigation to ConfigurationTool
        //public void NavigateToConfigurationTool(string page)
        //{
        //    NavigateToConfigurationMode();
        //}

        //Method for Navigation to Invoice Administration - Transfer Module
        public void NavigateInvoiceAdministrationToTransfer()
        {
            //Call the Navigate to Professional Mode Page
            NavigateToProfessionalMode();

            //Click on P2P_Main_Invoice_Administration to Open a Invoice Administratin Page
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_Invoice_Administration.User.Click();

            //Wait for Invoice Administration Transfer Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer.Wait.ForExists(Globals.navigationTimeOut);

            //Click on P2P_Invoice_Administration_Transfer to Open a Tansfer Page
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Transfer.User.Click();
        }

        //Method for Navigation to Invoice Administration - Workflow Module
        public void NavigateInvoiceAdministrationToWorkflow(string navigateProfessionalMode = null)
        {
            if (navigateProfessionalMode == null)
            {
                //Call the Navigate to Professional Mode Page
                NavigateToProfessionalMode();
            }

            //Click on P2P_Main_Invoice_Administration to Open a Invoice Administratin Page
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_Invoice_Administration.User.Click();

            //Wait for P2P_Invoice_Administration_Workflow Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_Button.Wait.ForExists(Globals.navigationTimeOut);

            //Click on P2P_Invoice_Administration_Workflow Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_Button.User.Click();
        }

        //Method for Navigation to Invoice Administration - Received Module
        public void NavigateInvoiceAdministrationToReceived(string navigateProfessionalMode = null)
        {
            if (navigateProfessionalMode == null)
            {
                //Call the Navigate to Professional Mode Page
                NavigateToProfessionalMode();

                //Click on P2P_Main_Invoice_Administration to Open a Invoice Administratin Page
                SharedElement.P2P_Application.SilverlightApp.P2P_Main_Invoice_Administration.User.Click();
            }
                    
            //Wait for P2P_Invoice_Administration_Received_Button Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_Button.Wait.ForExists(Globals.navigationTimeOut);

            //Click on P2P_Invoice_Administration_Received_Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Received_Button.User.Click();
        }

        //Method for Navigation to Invoice Administration - Overview Module
        public void NavigateInvoiceAdministrationToOverview(string navigateProfessionalMode = null)
        {
            if (navigateProfessionalMode == null)
            {
                //Call the Navigate to Professional Mode Page
                NavigateToProfessionalMode();
            }

            //Click on P2P_Main_Invoice_Administration to Open a Invoice Administratin Page
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_Invoice_Administration.User.Click();

            //Wait for P2P_Invoice_Administration_Overview Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview.Wait.ForExists(Globals.navigationTimeOut);

            //Click on P2P_Invoice_Administration_Overview Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Overview.User.Click();

        }

        //Method for Navigation to Invoice Administration - Create Module
        public void NavigateInvoiceAdministrationToCreate(string navigateProfessionalMode = null)
        {
            if (navigateProfessionalMode == null)
            {
                //Call the Navigate to Professional Mode Page
                NavigateToProfessionalMode();
            }

            //Click on P2P_Main_Invoice_Administration to Open a Invoice Administratin Page
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_Invoice_Administration.User.Click();

            //Wait for P2P_Invoice_Administration_Create Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Create.Wait.ForExists(Globals.navigationTimeOut);

            //Click on P2P_Invoice_Administration_Create Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Create.User.Click();
        }

        //Method for Navigation to Invoice Administration - Matching Module
        public void NavigateInvoiceAdministrationToMatching(string navigateProfessionalMode = null)
        {
            if (navigateProfessionalMode == null)
            {
                //Call the Navigate to Professional Mode Page
                NavigateToProfessionalMode();
            }

            //Click on P2P_Main_Invoice_Administration to Open a Invoice Administratin Page
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_Invoice_Administration.User.Click();

            //Wait for P2P_Invoice_Administration_Matching Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching.Wait.ForExists(Globals.navigationTimeOut);

            //Click on P2P_Invoice_Administration_Matching Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Matching.User.Click();
        }

        //Method for Navigation to Personal Mode Page and click on MySettings Module
        public void NavigateInvoiceAdministrationToMySettings(string navigateMySetting = null)
        {
            if (navigateMySetting == null)
            {
                //Call the Navigate to Personal Mode Page
                NavigateToPersonnelMode();
            }

            //Wait for Exists in P2P_Main_MySettings Exist in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_MySettings.Wait.ForExists(Globals.navigationTimeOut);

            //Click on P2P_Main_MySettings Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_MySettings.User.Click();
        }

        //Method for Navigation to Collaborate
        public void NavigateToCollaborate(string navigateCollaborate = null)
        {
            if (navigateCollaborate == null)
            {
                //Call the Navigate to Professional Mode Page
                NavigateToProfessionalMode();
            }

            //Click on Collaborate Tab
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_CollaborateTab.User.Click();
        }

        //Method for Navigation to Search Page
        public void NavigateInvoiceAdministrationToSearch(string invoiceAdministrationSearch = null)
        {
            if (invoiceAdministrationSearch == null)
            {
                //Call the Navigate to Professional Mode Page
                NavigateToProfessionalMode();
            }

            //Click on P2P_Main_Invoice_Administration to Open a Invoice Administratin Page
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_Invoice_Administration.User.Click();

            //Wait for P2P_Invoice_Administration_Search Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search.Wait.ForExists(Globals.navigationTimeOut);

            //Click on P2P_Invoice_Administration_Search Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_Search.User.Click();
        }

        //Method for Navigate to PersonaltoProfessional and ProfessionaltoPersonal Mode both
        public void NavigateToPersonaltoProfessionalandProfessionaltoPersonal(string page)
        {

            //Logic for Toggle Switch
            if (page == "Professional Mode")
            {
                //Call the Navigate to Personnel Mode Page
                NavigateToProfessionalMode();
            }
            else

                //Call the Navigate to Personnel Mode Page
                NavigateToPersonnelMode();

        }

        //Method for Navigate to Professional Mode
        public void NavigateToProfessionalMode(string mode = null)
        {
            //Wait for P2P_ChangeMode_ToggleButton Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.Wait.ForExists(Globals.timeOut);

            //Set Focus on Toggle Button
            SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.SetFocus();

            //Click on P2P_ChangeMode_ToggleButton to Open a ConfigurationTool Page
            SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.User.Click();

            //Refresh the visual Tree
            SharedElement.P2P_Application.SilverlightApp.OwnerApp.RefreshVisualTrees();

            FrameworkElement rcm = null;

            rcm = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=radcontextmenu", "AutomationId=ButtonContextMenu"));

            if (rcm == null)
            {
                //Create object of Framework Element Class 
                FrameworkElement fe = null;
                //switch case for if user has limited rights
                //mode = "Configuration";
                switch (mode)
                {
                    case "Configuration":
                        {

                            //Checking Object present on the screen or not
                            fe = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=configurationComboBox", "XamlTag=combobox"));
                            while (fe == null)
                            {
                                //Wait for P2P_ChangeMode_ToggleButton Exists in DOM
                                SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.Wait.ForExists(Globals.timeOut);

                                //Set Focus on Toggle Button
                                SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.SetFocus();
                                
                                //Click on P2P_ChangeMode_ToggleButton to Open a ConfigurationTool Page
                                SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.User.Click();
                                //call the Busy indicator
                                //P2PNavigation.CallBusyIndicator();
                                fe = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=configurationComboBox", "XamlTag=combobox"));
                            }

                            // Write the log if verification is Pass
                            Manager.Current.Log.WriteLine(LogType.Information, "Welcome to  Configuration Mode Page Verification Passed, User has Limited Rigths.");
                            break;
                        }

                    case "Personnel":
                        {
                            //Checking Object present on the screen or not
                            fe = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=My Tasks", "XamlTag=applicationradiobutton"));
                            while (fe == null)
                            {
                                //Wait for P2P_ChangeMode_ToggleButton Exists in DOM
                                SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.Wait.ForExists(Globals.timeOut);

                                //Set Focus on Toggle Button
                                SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.SetFocus();
                                
                                //Click on P2P_ChangeMode_ToggleButton to Open a ConfigurationTool Page
                                SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.User.Click();
                                //call the Busy indicator
                                //P2PNavigation.CallBusyIndicator();
                                //Again Checking the Object present on the screen or not
                                fe = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=My Tasks", "XamlTag=applicationradiobutton"));
                            }
                            // Write the log if verification is Pass
                            Manager.Current.Log.WriteLine(LogType.Information, "Welcome to  Personnel Mode Page Verification Passed, User has Limited Rigths.");
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            }
            else
            {
                //Check the Personal Mode Text exist in Text Block
                bool personalMode = rcm.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains("Personal Mode"));
                //Check the Configuration Mode Text exist in Text Block
                bool configurationMode = rcm.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains("Configuration Mode"));

                //If condition  is true then execute if block
                if ((personalMode && configurationMode) == true)
                {
                    //Wait for P2P_Main_Invoice_Administration Button Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Main_Invoice_Administration.Wait.ForExists(Globals.timeOut);
                }
                else
                {

                    //Wait for P2P_ChangeMode_ToggleButton Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.Wait.ForExists(Globals.timeOut);

                    //Set Focus on Toggle Button
                    SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.SetFocus();

                    //Click on P2P_ChangeMode_ToggleButton to Open a ConfigurationTool Page
                    SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.User.Click();

                    //Wait for P2P_ToConfigurationMode_RadMenuItem Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_ToConfigurationMode_RadMenuItem.Wait.ForExists(Globals.timeOut);

                    //Click on Professional Mode 
                    TextBlock professionalMode = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent("Professional Mode").As<TextBlock>();

                    //Click Invoice Administration Text Block
                    professionalMode.User.Click();

                    //Wait for P2PApplication Switch to Professional Page
                    P2PNavigation.CallBusyIndicator();
                }
                try
                {

                    //Wait for P2P_Main_Invoice_Administration Button Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Main_Invoice_Administration.Wait.ForExists(Globals.timeOut);

                    //Verify the P2P_Main_Invoice_Administration Button is Disabled or not       
                    if (SharedElement.P2P_Application.SilverlightApp.P2P_Main_Invoice_Administration.IsVisible == true)

                        // Write the log if verification is Pass
                        Manager.Current.Log.WriteLine(LogType.Information, "Welcome to  Professional Mode Page Verification Passed.");
                }
                catch
                {
                    // Write the log if verification is Fail
                    throw new Exception(LogType.Information + " " + "Your are not on Professional Mode Page Verification failed!!!!!");
                }
            }
        }

        //Method for Navigate to Personnel Mode
        public void NavigateToPersonnelMode(string mode = null)
        {
            //Wait for P2P_ChangeMode_ToggleButton Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.Wait.ForExists(Globals.timeOut);

            //Set Focus on Toggle Button
            SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.SetFocus();

            //Click on P2P_ChangeMode_ToggleButton to Open a ConfigurationTool Page
            SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.User.Click();
            //Refresh the visual Tree
            SharedElement.P2P_Application.SilverlightApp.OwnerApp.RefreshVisualTrees();

            //Initilize Rad Context Menu
            FrameworkElement rcm = null;

            rcm = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=radcontextmenu", "AutomationId=ButtonContextMenu"));

            if (rcm == null)
            {
                //Create object of Framework Element Class 
                FrameworkElement fe = null;
                //switch case for if user has limited rights
                //mode = "Configuration";
                switch (mode)
                {
                    case "Configuration":
                        {

                            //Checking Object present on the screen or not
                            fe = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=configurationComboBox", "XamlTag=combobox"));
                            while (fe == null)
                            {
                                //Wait for P2P_ChangeMode_ToggleButton Exists in DOM
                                SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.Wait.ForExists(Globals.timeOut);

                                //Set Focus on Toggle Button
                                SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.SetFocus();
                                
                                //Click on P2P_ChangeMode_ToggleButton to Open a ConfigurationTool Page
                                SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.User.Click();
                                //call the Busy indicator
                                //P2PNavigation.CallBusyIndicator();
                                fe = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=configurationComboBox", "XamlTag=combobox"));
                            }

                            // Write the log if verification is Pass
                            Manager.Current.Log.WriteLine(LogType.Information, "Welcome to  Configuration Mode Page Verification Passed, User has Limited Rigths.");
                            break;
                        }

                    case "Personnel":
                        {

                            fe = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=configurationComboBox", "XamlTag=combobox"));

                            while (fe != null)
                            {
                                //Wait for P2P_ChangeMode_ToggleButton Exists in DOM
                                SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.Wait.ForExists(Globals.timeOut);

                                //Set Focus on Toggle Button
                                SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.SetFocus();

                                //Click on P2P_ChangeMode_ToggleButton to Open a ConfigurationTool Page
                                SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.User.Click();
                                //Call the Busy indicator
                                //P2PNavigation.CallBusyIndicator();
                                //Again Checking the Object present on the screen or not
                                fe = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=configurationComboBox", "XamlTag=combobox"));
                            }
                            // Write the log if verification is Pass
                            Manager.Current.Log.WriteLine(LogType.Information, "Welcome to  Personnel Mode Page Verification Passed, User has Limited Rigths.");
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            }
            else
            {
                //Wait for P2P_ChangeMode_ToggleButton Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.Wait.ForExists(Globals.timeOut);

                //Set Focus on Toggle Button
                SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.SetFocus();

                //Click on P2P_ChangeMode_ToggleButton to Open a ConfigurationTool Page
                SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.User.Click();

                if (SharedElement.P2P_Application.SilverlightApp.P2P_ToPersonalMode_RadMenuItem.Text.ToString() == "Professional Mode" && SharedElement.P2P_Application.SilverlightApp.P2P_ToConfigurationMode_RadMenuItem.Text.ToString() == "Configuration Mode")
                {
                    //Wait for P2P_MyTask_HeaderDataTabItem Button Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_MyTaskButton.Wait.ForExists(Globals.timeOut);
                }

                else
                {

                    //Wait for P2P_ChangeMode_ToggleButton Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.Wait.ForExists(Globals.timeOut);

                    //Set Focus on Toggle Button
                    SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.SetFocus();

                    //Click on P2P_ChangeMode_ToggleButton to Open a ConfigurationTool Page
                    SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.User.Click();

                    //Wait for P2P_ToConfigurationMode_RadMenuItem Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_ToConfigurationMode_RadMenuItem.Wait.ForExists(Globals.timeOut);

                    //Click on Personal Mode 
                    TextBlock personalMode = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent("Personal Mode").As<TextBlock>();

                    //Click on Personal Mode Text Block
                    personalMode.User.Click();

                    //Wait for P2PApplication Switch to Professional Page
                    P2PNavigation.CallBusyIndicator();
                }

                try
                {

                    //Wait for P2P_MyTask_HeaderDataTabItem Button Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_MyTaskButton.Wait.ForExists(Globals.timeOut);

                    //Verify the P2P_MyTask_HeaderDataTabItem Button is Disabled or not       
                    if (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_MyTaskButton.IsVisible == true)

                        // Write the log if verification is Pass
                        Manager.Current.Log.WriteLine(LogType.Information, "Welcome to  Personal Mode Page Verification Passed.");
                }
                catch
                {
                    // Write the log if verification is Fail
                    throw new Exception(LogType.Information+ " " + "Your are not on Personal Mode Page Verification failed!!!!");
                }
            }
        }

        //Method to Navigate to MyTasks
        public void NavigateToMyTasks()
        {
            //Wait for My Tasks Tab Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_MyTaskButton.Wait.ForExists(Globals.navigationTimeOut);

            //SetFocus on My Tasks Tab
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_MyTaskButton.SetFocus();

            //Click on My Tasks Tab
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_WorkFlow_InvoiceDetails_TaskManagement_MyTaskButton.User.Click();
        }

        //Method for Navigate to Personnel Mode(Use optional Parameter "mode" if User has Limited Rights)
        public void NavigateToConfigurationMode(string mode = null)
        {

            //Wait for P2P_ChangeMode_ToggleButton Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.Wait.ForExists(Globals.navigationTimeOut);

            //Click on P2P_ChangeMode_ToggleButton to Open a ConfigurationTool Page
            SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.User.Click(MouseClickType.LeftDoubleClick);

            //call the Busy indicator
            P2PNavigation.CallBusyIndicator();

            //Refresh the visual Tree
            SharedElement.P2P_Application.SilverlightApp.OwnerApp.RefreshVisualTrees();

            FrameworkElement rcm = null;

            rcm = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=radcontextmenu", "AutomationId=ButtonContextMenu"));
            if (rcm == null)
            {
                //Create object of Framework Element Class 
                FrameworkElement fe = null;
                //switch case for if user has limited rights

                switch (mode)
                {
                    case "Configuration":
                        {

                            //Checking Object present on the screen or not
                            fe = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=configurationComboBox", "XamlTag=combobox"));
                            while (fe == null)
                            {
                                //Wait for P2P_ChangeMode_ToggleButton Exists in DOM
                                SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.Wait.ForExists(Globals.navigationTimeOut);

                                //Click on P2P_ChangeMode_ToggleButton to Open a ConfigurationTool Page
                                SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.User.Click();
                                //call the Busy indicator
                                P2PNavigation.CallBusyIndicator();
                                fe = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=configurationComboBox", "XamlTag=combobox"));
                            }

                            // Write the log if verification is Pass
                            Manager.Current.Log.WriteLine(LogType.Information, "Welcome to  Configuration Mode Page Verification Passed, User has Limited Rigths.");
                            break;
                        }

                    case "Personnel":
                        {
                            //Checking Object present on the screen or not
                            fe = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=My Tasks", "XamlTag=applicationradiobutton"));
                            while (fe == null)
                            {
                                //Wait for P2P_ChangeMode_ToggleButton Exists in DOM
                                SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.Wait.ForExists(Globals.navigationTimeOut);

                                //Click on P2P_ChangeMode_ToggleButton to Open a ConfigurationTool Page
                                SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.User.Click(MouseClickType.LeftDoubleClick);
                                //call the Busy indicator
                                P2PNavigation.CallBusyIndicator();
                                //Again Checking the Object present on the screen or not
                                fe = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=My Tasks", "XamlTag=applicationradiobutton"));
                            }
                            // Write the log if verification is Pass
                            Manager.Current.Log.WriteLine(LogType.Information, "Welcome to  Personnel Mode Page Verification Passed, User has Limited Rigths.");
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            }
            else
            {
                //Check the Personal Mode Text exist in Text Block
                bool personalMode = rcm.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains("Personal Mode"));
                //Check the Professional Mode Text exist in Text Block
                bool professionalMode = rcm.Find.AllByType<TextBlock>().Any((tb) => tb.Text.Contains("Professional Mode"));

                //If condition  is true then execute if block
                if ((personalMode && professionalMode) == true)
                {
                    //Wait for P2P_Main_Configuration_Tool Button Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Main_Configuration_Tool.Wait.ForExists(Globals.navigationTimeOut);
                    //Wait for P2PApplication Switch to Configuration Page
                }

                else
                {

                    //Wait for P2P_ChangeMode_ToggleButton Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.Wait.ForExists(Globals.navigationTimeOut);
                    //Wait for P2P_ChangeMode_ToggleButton Exist in UI
                    //System.Threading.Thread.Sleep(Globals.pause);

                    //Click on P2P_ChangeMode_ToggleButton to Open a ConfigurationTool Page
                    SharedElement.P2P_Application.SilverlightApp.P2P_ChangeMode_ToggleButton.User.Click();

                    //Click on Personal Mode 
                    TextBlock configurationMode = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent("Configuration Mode").As<TextBlock>();

                    //Click One Invoice to Clear the Default Selection
                    configurationMode.User.Click();

                    //Wait for P2PApplication Switch to Personal Page
                    P2PNavigation.CallBusyIndicator();
                }
                try
                {
                    //Wait for P2P_Main_Configuration_Tool Button Exists in DOM
                    SharedElement.P2P_Application.SilverlightApp.P2P_Main_Configuration_Tool.Wait.ForExists(Globals.navigationTimeOut);

                    //Verify the P2P_Main_Configuration_Tool Button is Disabled or not       
                    if (SharedElement.P2P_Application.SilverlightApp.P2P_ConfigurationToolButton.IsVisible == true)

                        // Write the log if verification is Pass
                        Manager.Current.Log.WriteLine(LogType.Information, "Welcome to  Configuration Mode Page Verification Passed.");
                }
                catch
                {
                    Manager.Current.Log.CaptureDesktop("c:\\TestAutomation\\CapturedImages\\ConfigTool");
                    // Write the log if verification is Fail
                    throw new Exception(LogType.Information + " " + "Your are not on Configuration Mode Verification failed!!!!!");
                }
            }
        }

        //Method to Navigate for Personal Mode Search
        public void NavigateToPersnalModeSearch()
        {
            //Wait for Search Tab Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_PersonalMode_SearchTab.Wait.ForExists(Globals.navigationTimeOut);

            //Click on Search Tab
            SharedElement.P2P_Application.SilverlightApp.P2P_InvoiceAdministration_PersonalMode_SearchTab.User.Click();
        }

        //Method to Navigate to Personal Mode Search Payment Plans Silo
        public void NavigateToPersonalModeSearchPaymentPlans()
        {

            //Wait for PaymentPlansVerticalmenu Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PaymentPlansVerticalmenuitem.Wait.ForExists(Globals.navigationTimeOut);

            //Click on PaymentPlansVerticalmenu
            SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PaymentPlansVerticalmenuitem.User.Click();

        }

        //Method to Navigate to WebShop
        public void NavigateToWebShop()
        {
            //Wait for WebShop Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShopApplication_Button.Wait.ForExists(Globals.navigationTimeOut);

            //Click on WebShop Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShopApplication_Button.User.Click();
        }

        //Method to Navigate to WebShop Product Search Page
        public void NavigateToWebShopProductSearchPage()
        {
            //Wait for WebShop Product Search Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_ProductSearch_VerticalMenuItem.Wait.ForExists(Globals.timeOut);

            //Click on WebShop Product Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_ProductSearch_VerticalMenuItem.User.Click(MouseClickType.LeftDoubleClick);
        }

        //Method to Navigate to WebShop Shopping Basket Page
        public void NavigateToWebShopShoppingBasket()
        {

            //Wait for WebShop Product Search Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_ShoppingBasket_VerticalMenuItem.Wait.ForExists(Globals.navigationTimeOut);

            //Set focus on Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_ShoppingBasket_VerticalMenuItem.SetFocus();

            //Click on WebShop Product Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_ShoppingBasket_VerticalMenuItem.User.Click();

            //Wait for Page Load Successfully
            //System.Threading.Thread.Sleep(Globals.timeOut);
        }

        //Method to Navigate to PurchaseDataManagement
        public void NavigateToPurchaseDataManagement()
        {
            //Call the Navigate to Professional Mode Page
            NavigateToProfessionalMode();

            //Wait for PurchaseDataManagement Button Exists in DOM 
            //SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_Button.Wait.ForExists(Globals.navigationTimeOut);

            //Wait for Data Management Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_Settings.Wait.ForExists(Globals.navigationTimeOut);

            //Click on WebShop Button
            //SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_Button.User.Click();

            //Click on Data Management Tab
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_Settings.User.Click();

        }

        //Method to Navigate to PurchaseDataManagement - Welcome Page
        public void NavigatePurchaseDataManagementToWelcomePage()
        {

            //Wait for Button Exists in DOM
            //SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePages_Button.Wait.ForExists(Globals.navigationTimeOut);

            //Click on button 
            //SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_WelcomePages_Button.User.Click();

            //Wait for Data Management Purchase Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PurchaseMenuButton.Wait.ForExists(Globals.navigationTimeOut);

            //Click on button 
            SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PurchaseMenuButton.User.Click();

            //FrameworkElement welcomePages = SharedElement.P2P_Application.SilverlightApp.Find.ByType("RadMenuItem");            
            //FrameworkElement welcomePages = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=radmenuitem"));

            //Click on the Welcome Pages RadMenuItem 
            //welcomePages.Find.ByTextContent("Welcome Pages").User.Click();  

            //Wait for Welcome Pages RadMenuItem Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_Purchase_WelcomePagesRadMenu.Wait.ForExists(Globals.navigationTimeOut);

            //Click on Welcome Pages RadMenuItem
            SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_Purchase_WelcomePagesRadMenu.User.Click();

        }

        //Method to Navigate to PurchaseDataManagement - Purchase Items
        public void NavigatePurchaseDataManagementToPurchaseItems()
        {

            //Wait for Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseItems_Button.Wait.ForExists(Globals.navigationTimeOut);

            //Click on button 
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_PurchaseItems_Button.User.Click();

        }

        //Method to Navigate to PurchaseDataManagement - Purchasing Categories
        public void NavigatePurchaseDataManagementToPurchasingCategories()
        {
            //Wait for Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_Purchasing_Categories_Button.Wait.ForExists(Globals.navigationTimeOut);

            //Click on button  
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_Data_Management_Purchasing_Categories_Button.User.Click();

        }

        //Method for Navigate to Payment Plans - Received Module
        public void NavigatePaymentPlansToReceived()
        {
            //Wait for Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Button.Wait.ForExists(Globals.navigationTimeOut);

            //Click on P2P_Payment_Plans_Received_Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Received_Button.User.Click();
        }

        //Method for Navigate to Payment Plans - Overview Module
        public void NavigatePaymentPlansToOverview()
        {
            //Wait for Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Overview_Button.Wait.ForExists(Globals.navigationTimeOut);

            //Click on P2P_Payment_Plans_Overview_Button Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Overview_Button.User.Click();
        }

        //Method for Navigate to Payment Plans - Approved Module
        public void NavigatePaymentPlansToApproved()
        {
            //Wait for P2P_PaymentPlans_Approved_Button Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Button.Wait.ForExists(Globals.navigationTimeOut);

            //Click on P2P_PaymentPlans_Approved_Button Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Button.User.Click();
        }

        //Method for Navigate to PaymentPlans
        public void NavigateToPaymentPlans(string navigateMySetting = null)
        {
            if (navigateMySetting == null)
            {
                //Call the Navigate to Professional Mode Page
                NavigateToProfessionalMode();
            }

            //Wait for Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_PaymentPlans.Wait.ForExists(Globals.navigationTimeOut);

            //Click on P2P_Main_PaymentPlans Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_PaymentPlans.User.Click();
        }

        //Method for Navigate To PaymentPlans Search Page 
        public void NavigatePaymentPlansToSearch()
        {
            //Wait for PaymentPlans Search Button Exist in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Button.Wait.ForExists(Globals.navigationTimeOut);

            //Click on PaymentPlans Search Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Search_Button.User.Click();

        }

        //Method for Navigate To PaymentPlans Create Page 
        public void NavigateToPaymentPlansCreatePage()
        {

            //Wait for PaymentPlans Create Button Exist in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Create_Button.Wait.ForExists(Globals.navigationTimeOut);

            //Click on P2P_Main_Invoice_Administration to Open a Invoice Administratin Page 
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Create_Button.User.Click();
        }

        //Method for Navigate to GoodReceipt - MyTasks Module
        public void NavigateToMyTasksGoodReceiptPage()
        {
            //Wait for Goods Receipt Vertical Menu Item Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_MyTasks_GoodsReceiptVerticalMenuItem.Wait.ForExists(Globals.navigationTimeOut);

            //Click on Goods Receipt Vertical Menu Item Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Purchase_MyTasks_GoodsReceiptVerticalMenuItem.User.Click(MouseClickType.LeftDoubleClick);
        }

        //Method for Navigate to WebShop - My Purchase Requisition Page
        public void NavigateToWebShopPurchaseRequisitionPage()
        {
            //Handle Busy Indicator
            P2PNavigation.CallBusyIndicator();

            //Wait for WebShop Purchase Requisitions Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_VerticalMenuItem.Wait.ForExists(Globals.navigationTimeOut);

            //Click on WebShop Purchase Requisitions Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisitions_VerticalMenuItem.User.Click(MouseClickType.LeftDoubleClick);
        }

        //Method for Navigate from Personal Mode Search to Purchase Requisition Page
        public void NavigatePersonalModeSearchToPurchaseRequisitionPage()
        {
            //Wait for Purchase Requisitions Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PurchaseRequisitions_VerticalMenuItem.Wait.ForExists(Globals.navigationTimeOut);

            //Click on Purchase Requisitions Button
            SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PurchaseRequisitions_VerticalMenuItem.User.Click(MouseClickType.LeftDoubleClick);
        }

        //Method for Navigate from Personal Mode Search to Purchase Orders Page
        public void NavigateToSearchPurchaseOrdersPage()
        {
            //Wait for Purchase Orders Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PurchaseOrders_VerticalMenuItem.Wait.ForExists(Globals.navigationTimeOut);

            //Set Focus on Purchase Orders Button
            SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PurchaseOrders_VerticalMenuItem.SetFocus();

            //Click on Purchase Orders Button
            SharedElement.P2P_Application.SilverlightApp.P2P_PersonalModeSearch_PurchaseOrders_VerticalMenuItem.User.Click(MouseClickType.LeftDoubleClick);
        }

        //Global Busy Indicator Implementation
        public static void CallBusyIndicator()
        {
            var startTime = DateTime.UtcNow;

            //Busy Indictor Load Time Mandatory Sleep
            System.Threading.Thread.Sleep(Globals.pause);

            System.Collections.Generic.Dictionary<string, bool?> busyIndicatorCollection = new System.Collections.Generic.Dictionary<string, bool?>();

            do
            {
                int counter = 0;

                busyIndicatorCollection.Clear();
                if (SharedElement.P2P_Application.SilverlightApp.Find.AllByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=busyindicator")).Equals(null))
                {
                    Manager.Current.Log.WriteLine(LogType.Information, " No Element Found of Type BusyIndicator in the Application");
                    break;
                }

                try
                {
                    foreach (FrameworkElement fe in SharedElement.P2P_Application.SilverlightApp.Find.
                        AllByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=busyindicator")))
                    {

                        PropertyInfo[] properties = fe.GetType().GetProperties();

                        Nullable<bool> isBusy = (Nullable<bool>)fe.GetProperty(new AutomationProperty("IsBusy", typeof(Nullable<bool>)));

                        string name = fe.ParsedElement.Attributes["Name"];

                        if (name == null || name == "myIndicator")
                            name = "BusyIndicator" + counter++;

                        busyIndicatorCollection.Add(name, isBusy);
                        //Manager.Current.Log.WriteLine(LogType.Information + " " + name + " :" + isBusy);
                    }

                }
                catch (Exception)
                {
                    break;
                }
            }


            while (busyIndicatorCollection.ContainsValue(true) && (DateTime.UtcNow - startTime).TotalSeconds < 120.00);


            double interval = (DateTime.UtcNow - startTime).TotalSeconds;
            //System.Threading.Thread.Sleep(Globals.pause);
            if (interval >= 120.00)
            {
                Manager.Current.Log.WriteLine(LogType.Error, " Busy Indicator Took Maximum Time Limit " + (DateTime.UtcNow - startTime).TotalSeconds + "  Seconds, There is a Problem in the Application!");
            }
            //else
            //{
            //    //Manager.Current.Log.WriteLine(LogType.Information + " Busy Indicator Took " + (DateTime.UtcNow - startTime).TotalSeconds + "  Seconds");
            //    //Manager.Current.Log.WriteLine(LogType.Information + " Busy Indicator is Working Please Wait!");
            //}

            //System.Threading.Thread.Sleep(Globals.pause);

        }

        //Wait For PopUp Ok Button Enabled
        public static void WaitForPopUpOkButtonEnabled()
        {
            int i = 500;
            //Wait for InvoiceType ComboBox Exists in DOM
            while (SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.IsEnabled == false)
            {

                //Wait for Page load Properly
                System.Threading.Thread.Sleep(i);

                i++;
                if (i == 510)
                {
                    break;
                }
            }
        }

        //Method for Navigate to My Task Purchase Order Page
        public void NavigateToMyTaskPurchaseOrderPage()
        {
            //Wait for P2P_WebShop_PurchaseOrder_VerticalMenuItem Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseOrder_VerticalMenuItem.Wait.ForExists(Globals.navigationTimeOut);

            //Set Focus on P2P_WebShop_PurchaseOrder_VerticalMenuItem Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseOrder_VerticalMenuItem.SetFocus();

            //Click on P2P_WebShop_PurchaseOrder_VerticalMenuItem Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseOrder_VerticalMenuItem.User.Click();
        }

        //Method for Navigate to Related Document Page
        public void NavigateToPRRelatedDocumentPage()
        {
            //Wait for My Tasks Goods Receipts Related Documents Tab to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_RelatedDocumentsTab.Wait.ForExists(Globals.navigationTimeOut);

            //Click on My Tasks Goods Receipts Related Documents Tab
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_GoodsReceipts_RelatedDocumentsTab.User.Click();
        }

        //Method for Navigate to Lines Page
        public void NavigateToPRLinePage()
        {
            //Wait for P2P_MyTask_HeaderDataTabItem to Exists in DOM Tree
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_HeaderDataTabItem.Wait.ForExists(Globals.navigationTimeOut);

            //Click on P2P_MyTask_HeaderDataTabItem
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTask_HeaderDataTabItem.User.Click();

            //Wait for Page load Successfully.
            P2PNavigation.CallBusyIndicator();
        }

        //Method for Navigate to My Task Purchase Requsition Page
        public void NavigateToMyTaskPurchaseRequisitionPage()
        {
            //Wait for P2P_WebShop_PurchaseRequisition_VerticalMenuItem Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchasRequisition_VerticalMenuItem.Wait.ForExists(Globals.navigationTimeOut);

            //Set Focus on P2P_WebShop_PurchaseRequisition_VerticalMenuItem Button
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchasRequisition_VerticalMenuItem.SetFocus();

            //Click on P2P_WebShop_PurchaseRequisition_VerticalMenuItem Button
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PurchasRequisition_VerticalMenuItem.User.Click();
        }

        //Method for Navigate to My Task Payment Plan Page
        public void NavigateToMyTaskPaymentPlanPage()
        {
            //Wait for Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PaymentPlans_VerticalMenuItem.Wait.ForExists(Globals.navigationTimeOut);

            //Set Focus on Button
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PaymentPlans_VerticalMenuItem.SetFocus();

            //Click on Button
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PaymentPlans_VerticalMenuItem.User.Click();
        }

        //Method for Navigate To PaymentPlans Search Page 
        public void NavigatePaymentPlansToInWorkflow()
        {
            //Wait for PaymentPlans Search Button Exist in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_InWorkflow_Button.Wait.ForExists();

            //Click on PaymentPlans Search Button 
            SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_InWorkflow_Button.User.Click();
        }

        //Method for Navigate To MyTask CreateInvoice
        public void NavigateToMyTaskCreateInvoice()
        {
            //Wait for My Task Create Invoice Button Exist in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_CreateInvoiceVerticalMenuItem.Wait.ForExists(Globals.navigationTimeOut);

            //Set focus on My Task Create Invoice Button
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_CreateInvoiceVerticalMenuItem.SetFocus();

            //Click on My Task Create Invoice Button
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_CreateInvoiceVerticalMenuItem.User.Click(MouseClickType.LeftClick);
        }

        //Method for Navigate To Personal Mode Search Invoices
        public void NavigateToPersonalModeSearchInvoices()
        {
            //Wait for Search Invoices Button Exist in DOM 
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=Search/Invoice")).Wait.ForExists(Globals.navigationTimeOut);

            //Set focus on Search Invoices Button
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=Search/Invoice")).SetFocus();

            //Click on Search Invoices Button
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=Search/Invoice")).User.Click(MouseClickType.LeftClick);
        }

        //Method for Navigate To WebShop My PR Draft filter
        public void NavigateToWebShopMyPRDraftFilter()
        {
            //Wait for Draft filter Button Exist in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_DraftFilterButton.Wait.ForExists(Globals.navigationTimeOut);

            //Set focus on Draft filter Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_DraftFilterButton.SetFocus();

            //Click on Draft filter Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_DraftFilterButton.User.Click(MouseClickType.LeftClick);
        }

        //Method for Navigate To WebShop My PR Pending filter
        public void NavigateToWebShopMyPRPendingFilter()
        {
            //Wait for Pending filter Button Exist in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_PendingFilterButton.Wait.ForExists(Globals.navigationTimeOut);

            //Set focus on Pending filter Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_PendingFilterButton.SetFocus();

            //Click on Pending filter Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_PendingFilterButton.User.Click(MouseClickType.LeftClick);
        }

        //Method for Navigate To WebShop My PR Info filter
        public void NavigateToWebShopMyPRInfoFilter()
        {
            //Wait for Info filter Button Exist in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_InfoFilterButton.Wait.ForExists(Globals.navigationTimeOut);

            //Set focus on Info filter Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_InfoFilterButton.SetFocus();

            //Click on Info filter Button
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_PurchaseRequisition_InfoFilterButton.User.Click(MouseClickType.LeftClick);
        }

        //Method for Navigate To My Task Invoices
        public void NavigateToMyTasksInvoice()
        {
            //Wait for My tasks Invoice Button Exist in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_InvoicesVerticalMenuItem.Wait.ForExists(Globals.navigationTimeOut);

            //Set focus on My tasks Invoice Button
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_InvoicesVerticalMenuItem.SetFocus();

            //Click on My tasks Invoice Button
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_InvoicesVerticalMenuItem.User.Click(MouseClickType.LeftClick);
        }

        //Method for Navigate To WebShop My PR Pending filter
        public void NavigateToMyTasksPaymentPlan()
        {
            //Wait for MyTasks_PaymentPlans Button Exist in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PaymentPlans_VerticalMenuItem.Wait.ForExists(Globals.navigationTimeOut);

            //Set focus on MyTasks_PaymentPlans Button
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PaymentPlans_VerticalMenuItem.SetFocus();

            //Click on MyTasks_PaymentPlansr Button
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_PaymentPlans_VerticalMenuItem.User.Click(MouseClickType.LeftClick);
        }

        //Method for Navigate To My Purchases Pricing Tab
        public void NavigateToMyPurchasesPricingTab(string selectTab)
        {
            //Wait for Tab Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_MyPurchases_TabControlRows.Wait.ForExists(Globals.timeOut);

            //Find and save element in FrameWorkElement fe
            SharedElement.P2P_Application.SilverlightApp.P2P_WebShop_MyPurchases_TabControlRows.Find.ByTextContent(selectTab).User.Click();
        }

        //Method for Navigate to DataManagement
        public void NavigateToDataManagement()
        {
            //Call the Navigate to Professional Mode Page
            NavigateToProfessionalMode();

            //Wait for Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_Settings.Wait.ForExists(Globals.navigationTimeOut);

            //Click on P2P_Main_PaymentPlans Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Main_Settings.User.Click();
        }

        //Method for Navigate to PaymentPlans Tab Item : (under DataManagement)
        public void NavigateToDataManagementPaymentPlansTab()
        {
            //Wait for Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PaymentPlans_RadioButton.Wait.ForExists(Globals.navigationTimeOut);

            //Click on DataManagement:PaymentPlans Tab
            SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PaymentPlans_RadioButton.User.Click();
        }

        //Method for Navigate to PaymentPlans Tab Item : Plan Groups (under DataManagement)
        public void NavigateToPaymentPlansTabItemPlanGroup()
        {
            //Call the Navigate to PaymentPlansTabItem
            NavigateToDataManagementPaymentPlansTab();

            //Wait for Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_ToConfigurationMode_RadMenuItem.Wait.ForExists(Globals.navigationTimeOut);

            //Click on Plan Groups( under DataManagement:PaymentPlans Tab)
            SharedElement.P2P_Application.SilverlightApp.P2P_ToConfigurationMode_RadMenuItem.User.Click();
        }

        //Method for Navigate Invoice Data Management to PP Reapproval Settings Tab
        public void NavigateToPaymentPlansTabItemReapprovalSettings()
        {
            //Method for Navigate To Invoice Data Management
            NavigateToDataManagementPaymentPlansTab();

            //Wait for PP Reapproval Settings Tab Button Exist in DOM 
            SharedElement.P2P_Application.SilverlightApp.P2P_ToPersonalMode_RadMenuItem.Wait.ForExists(Globals.navigationTimeOut);

            //Click on PP Reapproval Settings Tab Button
            SharedElement.P2P_Application.SilverlightApp.P2P_ToPersonalMode_RadMenuItem.User.Click(MouseClickType.LeftClick);
        }

        //Method for Navigate Purchase Data Management To Purchase
        public void NavigatePurchaseDataManagementToPurchase()
        {
            //Wait for Data Management Purchase Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PurchaseMenuButton.Wait.ForExists(Globals.navigationTimeOut);

            //Click on button 
            SharedElement.P2P_Application.SilverlightApp.P2P_DataManagement_PurchaseMenuButton.User.Click();
        }

        //Method for Navigate Purchase Data Management Purchase Purchasing Category
        public void NavigatePurchaseDataManagementToPurchasePurchasingCategory(string automationID)
        {
            //Wait for Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(automationID)).Wait.ForExists(Globals.navigationTimeOut);

            //Click on button 
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression(automationID)).User.Click();
        }

        //Method for Navigate Purchase Data Management To Purchase
        public void NavigateMyTasksToInvoice()
        {
            //Wait for Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_InvoicesVerticalMenuItem.Wait.ForExists(Globals.timeOut);

            //Click on button 
            SharedElement.P2P_Application.SilverlightApp.P2P_MyTasks_InvoicesVerticalMenuItem.User.Click();
        }

        //Method to Navigate to WebShop Product Search Page
        public void NavigateToWebShopMyShop()
        {
            //Wait for MyShop Search Button Exists in DOM 
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=WebShop/MyShop", "XamlTag=verticalmenuitem")).Wait.ForExists(Globals.timeOut);
            SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("AutomationId=WebShop/MyShop", "XamlTag=verticalmenuitem")).User.Click();

            //Wait for Page Load Successfully
            P2PNavigation.CallBusyIndicator();

            //Click on Product Search Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.Wait.ForExists(Globals.timeOut);
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_SearchButton.User.Click();
        }

        //Logic to Wait Until Page is Ready       
        public static void WaitUntilPageisReady(double executionTime, string printUIMeasurementLogs = null)
        {

            var startTime = DateTime.UtcNow;

            System.Collections.Generic.Dictionary<string, bool?> busyIndicatorCollection = new System.Collections.Generic.Dictionary<string, bool?>();

            do
            {
                int counter = 0;

                busyIndicatorCollection.Clear();
                if (SharedElement.P2P_Application.SilverlightApp.Find.AllByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=busyindicator")).Equals(null))
                {
                    Manager.Current.Log.WriteLine(LogType.Information + " No Element Found of Type BusyIndicator in the Application");
                    break;
                }

                try
                {
                    foreach (FrameworkElement fe in SharedElement.P2P_Application.SilverlightApp.Find.
                        AllByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=busyindicator")))
                    {

                        PropertyInfo[] properties = fe.GetType().GetProperties();

                        Nullable<bool> isBusy = (Nullable<bool>)fe.GetProperty(new AutomationProperty("IsBusy", typeof(Nullable<bool>)));

                        string name = fe.ParsedElement.Attributes["Name"];

                        if (name == null || name == "myIndicator")
                            name = "BusyIndicator" + counter++;

                        busyIndicatorCollection.Add(name, isBusy);
                    }


                }
                catch (Exception)
                {
                    break;
                }
            }


            while (busyIndicatorCollection.ContainsValue(true) && (DateTime.UtcNow - startTime).TotalSeconds < 120.00);

            if (printUIMeasurementLogs != null)
            {
                double interval = (DateTime.UtcNow - startTime).TotalSeconds;
                if (interval >= executionTime)
                {
                    Manager.Current.Log.WriteLine(LogType.Information + " Page Load Took " + (DateTime.UtcNow - startTime).TotalSeconds + "  Seconds, There is a Problem in the Application!");
                }
                else
                {
                    Manager.Current.Log.WriteLine(LogType.Information + " Page Load Took " + (DateTime.UtcNow - startTime).Seconds + "  Seconds");
                }
            }
            Thread.Sleep(Globals.timeOut);
        }
    }
}
