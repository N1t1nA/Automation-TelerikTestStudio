using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Silverlight.UI;
using ArtOfTest.WebAii.Win32.Dialogs;
using P2P.Testing.Shared.Class;
using Telerik.WebAii.Controls.Xaml;

namespace E2E.Class
{
    public static class P2P_Utility
    {
        //Saving Generated number in Gross Total
        public static double grossTotal;

        //Saving Generated number in Supplier Name
        public static string supplierName;

        //Saving Generated number in Received Data
        public static double receivedQuantity;
        
        //Saving Generated number in PO Number
        public static string poNumber;

        //Delcared String variable
        public static string invoice = "INV_FTA_";

        //Saving Generated number in Invoice Number
        public static string invoiceNumber;

        //Saving Generated number in Purpose Number
        public static string purposeNumber;

        //Declared 'FileUploadDialog' variable
        public static FileUploadDialog fileUploadDialog;

        //Declared 'tabCount' variable
        public static string tabCount;
        
        //Declared 'tabCount' variable
        public static string tabCount1;

        //File Path Saved
        public static string fileUploadPath;

        //Method to Generate Random Number
        public static void GenerateNumber(string invoiceWorkflow=null)
        {
            //Delcared String variable
            string name = "PUR_FTA_";

            //Delcared Int variable
            const int MIN_LIMIT = 0;

            //Delcared Int variable
            const int MAX_LIMIT = 9999;

            //Delcared Double number which saves 4 digit Random Generated Number
            Double number = new Random().Next(MIN_LIMIT, MAX_LIMIT);

            if (invoiceWorkflow != null)
            {
                invoiceNumber = invoice + number;
            }
            else
            {
                //Saving name + number into declared String purposeNumber
                purposeNumber = name + number;
            }           
        }

        //Method for Fetching Data on run-Time and Saving in Global Static Variable
        public static void FetchingDataFromUI(object obj, string data)
        {
            switch (data)
            {
                case "Purchase Order":
                    {
                        FrameworkElement fe = (FrameworkElement)obj;
                        poNumber = fe.Find.ByTextContent("~" + data).TextLiteralContent.Remove(0, 15);
                        Manager.Current.Log.WriteLine(LogType.Information, "Purchase Order number fetched from Related Document is '" + poNumber + "'");
                        invoiceNumber = invoice + poNumber;
                        fe.Refresh();
                        break;
                    }

                case "Gross Total":
                    {
                        TextBox tb = (TextBox)obj;
                        grossTotal = Convert.ToDouble(tb.Text);
                        Manager.Current.Log.WriteLine(LogType.Information, "Gross Total fetched is '" + grossTotal + "'");
                        tb.Refresh();
                        break;
                    }

                case "Supplier Name":
                    {
                        TextBox tb = (TextBox)obj;
                        supplierName = tb.Text;
                        Manager.Current.Log.WriteLine(LogType.Information, "Supplier Name fetched is '" + supplierName + "'");
                        tb.Refresh();
                        break;
                    }

                case "Received Quantity":
                    {
                        double tb = (double)obj;
                        receivedQuantity = tb;
                        Manager.Current.Log.WriteLine(LogType.Information, "Received Quantity fetched is '" + receivedQuantity + "'");
                        break;
                    }

                case "File Upload Dialog":
                    {
                        fileUploadPath = obj.ToString();
                        fileUploadDialog = new FileUploadDialog(Manager.Current.ActiveBrowser, fileUploadPath, DialogButton.OPEN, "Open");
                        break;
                    }

                case "OnHold Tab Count":
                    {
                        ArtOfTest.WebAii.Silverlight.UI.RadioButton tb = (ArtOfTest.WebAii.Silverlight.UI.RadioButton)obj;
                        tabCount = tb.Text;
                        Manager.Current.Log.WriteLine(LogType.Information, "Tab count is '" + tabCount + "'");
                        break;
                    }

                case "Pending Tab Count":
                    {
                        ArtOfTest.WebAii.Silverlight.UI.RadioButton tb = (ArtOfTest.WebAii.Silverlight.UI.RadioButton)obj;
                        tabCount1 = tb.Text;
                        Manager.Current.Log.WriteLine(LogType.Information, "Tab count is '" + tabCount1 + "'");
                        break;
                    }

                

                default:
                    {
                        //Log error if no case found.
                        Manager.Current.Log.WriteLine(LogType.Error, "  No Case Found. Please Check!!");

                        //Write the log if no case found.
                        throw new Exception(LogType.Error + ": No Case Found. Please Check!!");
                    }
            }
        }
    }
}
