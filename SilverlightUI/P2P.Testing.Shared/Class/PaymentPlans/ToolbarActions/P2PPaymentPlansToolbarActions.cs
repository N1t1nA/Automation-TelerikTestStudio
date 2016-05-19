using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Silverlight.UI;
using System.Windows.Forms;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Win32.Dialogs;
using ArtOfTest.WebAii.Design;
using System.Threading;

namespace P2P.Testing.Shared.Class.PaymentPlans.ToolbarActions
{
    public class P2PPaymentPlansToolbarActions : BaseWebAiiTest
    {
        private Browser _browser;
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

        public P2PPaymentPlansToolbarActions(Browser browser)
        {
            _browser = browser;
        }

        //Method to Select Multiple Invoices
        public void P2PPaymentPlansSelectMultiplePaymentPlans(string headerCell, string invoiceNum1, string invoiceNum2, string invoiceNum3, Boolean singlePaymentPlans, string sortingGrid =null)
        {
            //Global Wait 
            //System.Threading.Thread.Sleep(Globals.timeOut);
            P2PNavigation.CallBusyIndicator();

            //Use condition for sorting the Grid if required
            if (sortingGrid != null)
            {
                //Wait for Grid Control Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control.Wait.ForExists(Globals.timeOut);

                //Find the Header cell Value as Invoice Number
                TextBlock invoiceNumberHeaderCell = SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlans_Approved_Grid_Control.Find.ByTextContent(headerCell).As<TextBlock>();
                //Click on Plan Name  Header Cell for Sorting the Invoice
                invoiceNumberHeaderCell.User.Click();
            }

            //If condition is true then execute if block
            if (singlePaymentPlans == true)
            {
                //Find the Invoice Numbers in Transfer Grid Control 
                TextBlock invoice1 = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(invoiceNum1).As<TextBlock>();
                //Click One Invoice to Clear the Default Selection
                invoice1.User.Click();

            }

            else
            {
                //Find the Invoice Numbers in Transfer Grid Control 
                TextBlock invoice1 = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(invoiceNum1).As<TextBlock>();
                TextBlock invoice2 = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(invoiceNum2).As<TextBlock>();
                TextBlock invoice3 = SharedElement.P2P_Application.SilverlightApp.Find.ByTextContent(invoiceNum3).As<TextBlock>();


                //Click One Invoice to Clear the Default Selection
                invoice1.User.Click();

                //Press the Control Key down
                Manager.Current.Desktop.KeyBoard.KeyDown(Keys.Control);

                //Click on the Multiple rows you want to select 
                invoice2.User.Click();

                //Click on the Multiple rows you want to select 
                invoice3.User.Click();

                //Release the Control Key Up
                Manager.Current.Desktop.KeyBoard.KeyUp(Keys.Control);
            }
        }

        //Method to to perform Additonal Actions Drop Down
        public void P2PPaymentPlansAdditonalActionDropDown(string addComment, string removePaymentPlans = null)
        {
            //Wait for Additional Actions Drop Down Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.Wait.ForExists(Globals.timeOut);

            //Setting Focus on Additional Actions Drop Down
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.SetFocus();

            //Click on Additional Actions Drop Down button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AdditionalActionsDropdown.User.Click();

            if (removePaymentPlans != null)
            {
                //Wait for Add Comment Additional Actions Drop Down Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_Payment_Plans_RemovePaymentPlan_DropdownButton.Wait
                             .ForExists(Globals.timeOut);

                //Click on Add Comment Additional Actions Drop Down  button
                SharedElement.P2P_Application.SilverlightApp.P2P_Payment_Plans_RemovePaymentPlan_DropdownButton.User
                             .Click();
            }
            else
            {
                //Wait for Add Comment Additional Actions Drop Down Exists in DOM
                SharedElement.P2P_Application.SilverlightApp
                             .P2P_Invoice_Administration_InWorkFlow_AddComment_DropdownButton.Wait.ForExists(
                                 Globals.timeOut);

                //Click on Add Comment Additional Actions Drop Down  button
                SharedElement.P2P_Application.SilverlightApp
                             .P2P_Invoice_Administration_InWorkFlow_AddComment_DropdownButton.User.Click();
            }

            //Wait for Add Comment Text Box Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.Wait.ForExists(Globals.timeOut);

            //Set focus on Add Comment Text Box Exists in DOM

            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.SetFocus();

            //Type Comment on Add Comment Text Box
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_TextBox.User.TypeText(addComment, 50);

            //Wait for OK Button Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.Wait.ForExists(Globals.timeOut);

            //Click on OK Button
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_AddComment_OKButton.User.Click();
            
        }        

        //Method to Click on Next Button 
        public void P2PPaymentPlanClickOnNextButton()
        {
           
            //Wait for NextButton Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_InvoiceDetails_NAVNextButton.Wait.ForExists(Globals.timeOut);

            //Navigate One Step Forward
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_InvoiceDetails_NAVNextButton.User.Click();
        }

        //Method to Click on Previous Button 
        public void P2PPaymentPlanClickOnPreviousButton()
        {
            
            //Wait for PreviousButton Exists in DOM
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_InvoiceDetails_NAVPreviousButton.Wait.ForExists(Globals.timeOut);

            //Navigate One Step Back
            SharedElement.P2P_Application.SilverlightApp.P2P_Invoice_Administration_InWorkFlow_InvoiceDetails_NAVPreviousButton.User.Click();
        }

        //Method to Click on Validate Button 
        public void P2PPaymentPlanClickOnValidateButton(string paymentPlansValidateButton = null)
        {
            //Delete the button when bug fix:
            if (paymentPlansValidateButton != null)
            {              
                //Wait for Search Button exist in DOM                
                SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=actiontoolbar", "Name=ActionToolbar")).Wait.ForExists();
                //Create an object for paymentPlanToolBarActions
                ArtOfTest.WebAii.Silverlight.UI.UserControl paymentPlanToolBarActions = SharedElement.P2P_Application.SilverlightApp.Find.ByExpression(new ArtOfTest.WebAii.Silverlight.XamlFindExpression("XamlTag=actiontoolbar", "Name=ActionToolbar")).CastAs<ArtOfTest.WebAii.Silverlight.UI.UserControl>();
                //Click on tool bar button
                paymentPlanToolBarActions.Find.ByTextContent(paymentPlansValidateButton).User.Click();
            }

            else
            {
                //Wait for Button Exists in DOM
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_SendToValidation_ToolBarButton.Wait.ForExists(Globals.timeOut);
                //Click on Tool Bar Button
                SharedElement.P2P_Application.SilverlightApp.P2P_PaymentPlan_SendToValidation_ToolBarButton.User.Click();
            }            
        }

    }
}
