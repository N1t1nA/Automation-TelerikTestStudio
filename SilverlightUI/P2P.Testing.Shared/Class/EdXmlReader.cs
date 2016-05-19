using System;
using System.Linq;
using System.Data;

namespace P2P.Testing.Shared.Class
{

    public class EdXmlReader
    {
        public DataRow Read_xml_file(string xmlFileName, string tableName, string primaryColumnName, string primaryColumnValue)
        {
            //create new dataset   
            DataSet dataset = new DataSet();

            //Define the variable for getting output directory
            //var dir = AppDomain.CurrentDomain.DynamicDirectory;
            string dir = !string.IsNullOrEmpty(AppDomain.CurrentDomain.SetupInformation.DynamicBase) ? AppDomain.CurrentDomain.SetupInformation.DynamicBase : AppDomain.CurrentDomain.BaseDirectory;
            
            //Locate extra string to remove it
            int indexof = dir.IndexOf("eb4396e6");
            dir = dir.Remove(indexof);

            //Logic to remove extra "\" at the end of dir. 
            //Local execution has "\" at the end, on BVT path doesnt show "\" at the end            
            while (dir.Substring(dir.Length - 1, 1).Equals(@"\"))
            {
                dir = dir.Remove(dir.Length - 1);
            }

            //Print the directory 
            //Manager.Current.Log.WriteLine(LogType.Information, "Path returned: " + dir);

            // read xml file to dataset variable
            dataset.ReadXml(dir + @"\Data\" + xmlFileName);

            // retrieve and set primary key column(s)
            DataColumn[] dc = { dataset.Tables[tableName].Columns[primaryColumnName] };

            // set primary key
            dataset.Tables[tableName].PrimaryKey = dc;

            // search datarow by given value of primary key
            DataRow dr = dataset.Tables[tableName].Rows.Find(primaryColumnValue);

            //return datarow
            return dr;

        }

        public string Upload_file(string fileName)
        {
            //Define the variable for getting output directory
            //var dir = AppDomain.CurrentDomain.BaseDirectory;                        
            //var dir = AppDomain.CurrentDomain.DynamicDirectory;
            string dir = !string.IsNullOrEmpty(AppDomain.CurrentDomain.SetupInformation.DynamicBase) ? AppDomain.CurrentDomain.SetupInformation.DynamicBase : AppDomain.CurrentDomain.BaseDirectory;

            //Locate extra string to remove it
            int indexof = dir.IndexOf("eb4396e6");
            dir = dir.Remove(indexof); 

            //Logic to remove extra "\" at the end of dir. 
            //Local execution has "\" at the end, on BVT path doesnt show "\" at the end            
            while (dir.Substring(dir.Length - 1, 1).Equals(@"\"))
            {
                dir = dir.Remove(dir.Length - 1);
            }

            //Print the directory 
            //Manager.Current.Log.WriteLine(LogType.Information, "Path returned: " + dir);

            //Save the upload Path
            string uploadPath = (dir + @"\UploadFiles\" + fileName);

            //this is function returning the string type value
            return uploadPath;

        }
    }

    public static class Globals
    {
        //Global Wait Time 
        public static int timeOut = 4000;

        //Wait for Busy Indicator to appear 
        public static int pause = 3000;

        //Handle time for Dialogs Handling 
        public static int handleTime = 10000;

        //Navigation class timeout
        public static int navigationTimeOut = 40000;

        //Captured Image Location 
        public static string capturedImageLocation = "C:\\TestAutomation\\CapturedImages\\";

        public static string testScriptName = null;
    }
}
