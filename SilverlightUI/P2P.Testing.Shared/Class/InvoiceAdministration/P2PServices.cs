using System;
using System.Linq;
using ArtOfTest.WebAii.TestTemplates;
using System.ServiceProcess;
using ArtOfTest.WebAii.Core;

namespace P2P.Testing.Shared.Class.InvoiceAdministration
{
   public class P2PServices : BaseTest
    {
        //Method to Start and Stop the anyERP Service
        public void StartAndStopAnyERPServices(string serviceName, string serviceStatus, System.TimeSpan timespan, string anyERP = null)
        {
            //Create an object for ServiceController class
            ServiceController myService = new ServiceController();

            //Get the Service Name from the XML
            myService.ServiceName = serviceName;

            //Get the Current Status of the Specific Service
            string svcStatus = myService.Status.ToString();

            if (anyERP != null)
            {
                if (svcStatus != "Running")
                {
                    //Write log When Service is Start
                    Manager.Current.Log.WriteLine("Before Starting AnyErp Services status is: '" + svcStatus + "'.");

                    //Start the anyERP Service
                    myService.Start();

                    //Wait for the Services Start
                    System.Threading.Thread.Sleep(Globals.handleTime);

                    //Refresh Services
                    myService.Refresh();

                    //Write log When Service is Start
                    Manager.Current.Log.WriteLine("'" + serviceName + "' Start Successfully'" + myService.Status.ToString() + "'");
                }
                else
                {
                    //Write log When Service is Stopped
                    Manager.Current.Log.WriteLine("'" + serviceName + "' Already Running.'" + svcStatus + "'");
                }
            }
            else
            {
                //If Service is Running then execute if Block for Stop the Services
                if (svcStatus == serviceStatus)
                {
                    //Stop the anyERP Service
                    myService.Stop();
                    //Wait for the Services Stopped
                    System.Threading.Thread.Sleep(Globals.handleTime);
                    //Write log When Service is Stopped
                    Manager.Current.Log.WriteLine(serviceName + " " + "Stopped Successfully");
                }
                //If Service is Stop then execute else block and Start Service
                else
                {
                    //Start the anyERP Service
                    myService.Start();
                    //Wait for the Services Start
                    System.Threading.Thread.Sleep(Globals.handleTime);
                    //Write log When Service is Start
                    Manager.Current.Log.WriteLine(serviceName + " " + "Start Successfully");
                }
            }
        }
    }
}
