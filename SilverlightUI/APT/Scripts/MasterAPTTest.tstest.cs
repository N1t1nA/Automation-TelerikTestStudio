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

namespace APT.Scripts
{

    public class MasterAPTTest : BaseWebAiiTest
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

       
        [CodedStep(@"Execute 'Edit_Validate_DraftInvoice' Test", RequiresSilverlight = true)]
        public void TestCase_88873()
        {
            // Execute tests 
            this.ExecuteTest("Scripts\\Edit_Validate_DraftInvoice.tstest");
        }

        [CodedStep(@"Execute 'Edit_Validate_InvalidInvoice' Test", RequiresSilverlight = true)]
        public void TestCase_88874()
        {
            // Execute tests 
            this.ExecuteTest("Scripts\\Edit_Validate_InvalidInvoice.tstest");
        }

        [CodedStep(@"Execute 'EditAndMatch_NoPOFoundInvoices_Header_ROW_GR' Test", RequiresSilverlight = true)]
        public void TestCase_88876()
        {
            // Execute tests 
            this.ExecuteTest("Scripts\\EditAndMatch_NoPOFoundInvoices_Header_ROW_GR.tstest");
        }

        [CodedStep(@"Execute 'EditInvoicesInWorkflow_BatchReview' Test", RequiresSilverlight = true)]
        public void TestCase_88877()
        {
            // Execute tests 
            this.ExecuteTest("Scripts\\EditInvoicesInWorkflow_BatchReview.tstest");
        }

        [CodedStep(@"Execute 'CompleteInvoiceCoding_ReTransferInvoice' Test", RequiresSilverlight = true)]
        public void TestCase_88878()
        {
            // Execute tests 
            this.ExecuteTest("Scripts\\CompleteInvoiceCoding_ReTransferInvoice.tstest");
        }

        [CodedStep(@"Execute 'Edit_Validate_SendToProcess_DraftPaymentPlan' Test", RequiresSilverlight = true)]
        public void TestCase_88884()
        {
            // Execute tests 
            this.ExecuteTest("Scripts\\Edit_Validate_SendToProcess_DraftPaymentPlan.tstest");
        }      

        [CodedStep(@"Execute 'ChangeRecepient_HeaderReviewAndApproval_PaymentPlans' Test", RequiresSilverlight = true)]
        public void TestCase_88885()
        {
            // Execute tests 
            this.ExecuteTest("Scripts\\ChangeRecepient_HeaderReviewAndApproval_PaymentPlans.tstest");
        }

        [CodedStep(@"Execute 'ReviewAndApprove_PaymentPlan' Test", RequiresSilverlight = true)]
        public void TestCase_88886()
        {
            // Execute tests 
            this.ExecuteTest("Scripts\\ReviewAndApprove_PaymentPlan.tstest");
        }

        [CodedStep(@"Execute 'CancelApprovalTask_PaymentPlan' Test", RequiresSilverlight = true)]
        public void TestCase_88887()
        {
            // Execute tests 
            this.ExecuteTest("Scripts\\CancelApprovalTask_PaymentPlan.tstest");
        }

        [CodedStep(@"Execute 'Activate_ExpiredAndDeactivatePaymentPlan' Test", RequiresSilverlight = true)]
        public void TestCase_88888()
        {
            // Execute tests 
            this.ExecuteTest("Scripts\\Activate_ExpiredAndDeactivatePaymentPlan.tstest");
        }

        [CodedStep(@"Execute 'Resend_AutomaticMatching_PaymentPlan' Test", RequiresSilverlight = true)]
        public void TestCase_88889()
        {
            // Execute tests 
            this.ExecuteTest("Scripts\\Resend_AutomaticMatching_PaymentPlan.tstest");
        }

        [CodedStep(@"Execute 'ManualPaymentPlanMatching' Test", RequiresSilverlight = true)]
        public void TestCase_88890()
        {
            // Execute tests 
            this.ExecuteTest("Scripts\\ManualPaymentPlanMatching.tstest");
        }

        [CodedStep(@"Execute 'Cancel_PaymentPlanMatching_RemoveInvoice' Test", RequiresSilverlight = true)]
        public void TestCase_88891()
        {
            // Execute tests 
            this.ExecuteTest("Scripts\\Cancel_PaymentPlanMatching_RemoveInvoice.tstest");
        }

        [CodedStep(@"Execute 'RequestManualApprovalPaymentPlan_ProcessInvoiceManually' Test", RequiresSilverlight = true)]
        public void TestCase_88892()
        {
            // Execute tests 
            this.ExecuteTest("Scripts\\RequestManualApprovalPaymentPlan_ProcessInvoiceManually.tstest");
        }

        [CodedStep(@"Execute 'TransferInvoices_SingleAndBatch' Test", RequiresSilverlight = true)]
        public void TestCase_88893()
        {
            // Execute tests 
            this.ExecuteTest("Scripts\\TransferInvoices_SingleAndBatch.tstest");
        }
        
    }
}
