using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Win32.Dialogs;
using System.Windows.Forms;

namespace P2P.Testing.Shared.Class
{
    public class P2PExceptionHandler
    {
        //Declared the Global variables
        private readonly string configFilePath = "C:\\TestAutomation\\ci\\scripts\\config.ini";
        private readonly string uITestDirPreFix = "BVT_";
        private readonly string capturedImage = "\\CapturedImage";
        //private readonly string localExecutionCaptureImageLocation = "C:\\TestAutomation\\LocalTestExecution\\CapturedImage\\";
        private readonly string configfileWorkingDir = "workingdir";
        private readonly string buildInfoSearchKeyword = "Build: ";
        private readonly string configfileLogsDir = "logsdir";
        private readonly string buildInfo = "\\build_info.txt";
        private IList<string> configurationKeyWords = null;

        public P2PExceptionHandler()
        {
            configurationKeyWords = new List<string> { configfileWorkingDir, configfileLogsDir };
        }

        public void AddDialogHandler()
        {
            //Change the Current Settings 
            Manager.Current.Settings.UnexpectedDialogAction = UnexpectedDialogAction.HandleAndContinue;
            //Initialize the Dialog 
            OnBeforeUnloadDialog LeaveThisPageDialog = OnBeforeUnloadDialog.CreateOnBeforeUnloadDialog(Manager.Current.ActiveBrowser, DialogButton.OK);

            //Add Dialog 
            Manager.Current.DialogMonitor.AddDialog(LeaveThisPageDialog);
            //Monitor the Dialog if Found and Handle this 
            Manager.Current.DialogMonitor.Start();

        }

        private bool configurationSelector(string Filelocation)
        {
            var workingDirInformation = Filelocation.Split('=').ToArray();
            var location = workingDirInformation.Count<string>() > 1 ? workingDirInformation[1].ToString().Trim() : null;
            return location != null && configurationKeyWords.Contains(workingDirInformation[0].ToString());
        }

        public bool VerifyConfigurationFileExists()
        {
            if (new FileInfo(configFilePath).Exists)
            {
                var readConfigurationFile = File.ReadAllLines(configFilePath).Where(configurationSelector).ToList();
                if (readConfigurationFile.Count == 0)
                    throw new ApplicationException(readConfigurationFile + " : can not be Null or Empty");

                foreach (var configElement in readConfigurationFile)
                {
                    var workingDirInformation = configElement.Split('=').ToArray();
                    var configElementName = workingDirInformation[0].ToString();
                    var configElementValue = workingDirInformation.Count<string>() > 1 ? workingDirInformation[1].ToString().Trim() : null;
                    switch (configElementName)
                    {
                        case "workingdir":
                            alustaPackageCopiedLocation = configElementValue;
                            break;
                        case "logsdir":
                            logDir = configElementValue;
                            break;
                    }
                }

                string buildInfoTextFilePath = alustaPackageCopiedLocation + buildInfo;
                if (new FileInfo(buildInfoTextFilePath).Exists)
                {
                    var readBuildInfoTextFile = File.ReadAllLines(buildInfoTextFilePath).Where(x => x.Contains(buildInfoSearchKeyword)).FirstOrDefault();
                    if (string.IsNullOrEmpty(readBuildInfoTextFile))
                        throw new ApplicationException(readBuildInfoTextFile + " : can not be Null or Empty");

                    var buildName = readBuildInfoTextFile.Split(':').ToArray();
                    buildExecutionName = buildName.Count<string>() > 1 ? buildName[1].ToString().Trim() : null;

                    return !string.IsNullOrEmpty(buildExecutionName);
                }
                else
                {
                    Manager.Current.Log.WriteLine(LogType.Information, buildInfo + ": File DOES NOT Exists");
                }
            }
            else
            {
                //Manager.Current.Log.WriteLine(LogType.Information, configFilePath + ": File DOES NOT Exists");
            }
            return !string.IsNullOrEmpty(alustaPackageCopiedLocation);
        }

        private string workingDir;
        public string alustaPackageCopiedLocation
        {
            get { return workingDir; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception(alustaPackageCopiedLocation + ":Working Dir can't be Null or Empty");
                workingDir = value;
            }
        }

        private string build;
        public string buildExecutionName
        {
            get { return build; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception(buildExecutionName + ":Build Name can't be Null or Empty");
                build = value;
            }
        }

        private string log;
        public string logDir
        {
            get { return log; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception(logDir + ":Log Directory Can't be Null or Empty");
                log = value;
            }
        }

        public bool VerifyDirExists(string dirPath, bool createDirIfNotExist)
        {
            var dirInfo = new DirectoryInfo(dirPath);
            if (!dirInfo.Exists && createDirIfNotExist)
            {
                try
                {
                    dirInfo.Create();
                    return true;
                }
                catch
                {
                    throw new Exception(LogType.Error + dirPath + ": DOES NOT Exists");
                }
            }
            else
            {
                return dirInfo.Exists;
            }
        }

        public void CapturedImage(Exception ex, string scriptName)
        {
            //FolderName and Path for storing captured snapshots
            var captureImageloc = "..\\..\\CapturedImage\\";
            //Combine ScriptName and Coded step name
            var capturedImageName = scriptName + new StackTrace().GetFrame(1).GetMethod().Name;
            //Log 
            Manager.Current.Log.WriteLine(LogType.Information, "****Captured Images****");
            //Create the CaptureImage folder if not existing
            if (!Directory.Exists(captureImageloc))
                Directory.CreateDirectory(captureImageloc);
            //Capture the desktop
            Manager.Current.Log.CaptureDesktop(captureImageloc + capturedImageName);
            //Log the error
            Manager.Current.Log.WriteLine(ex.Message);
            //Add Dialog Handler if Found While Execution. 
            AddDialogHandler();
            throw new Exception(LogType.Error + ex.StackTrace);
        }

        //old code of CI
        public void CapturedImage2(Exception ex, string scriptName)
        {
            VerifyConfigurationFileExists();
            var captureImageloc = logDir + "\\" + uITestDirPreFix + buildExecutionName + capturedImage + "\\";
            var codeStepName = new StackTrace().GetFrame(1).GetMethod().Name;

            var capturedImageName = scriptName + codeStepName;
            if (new FileInfo(configFilePath).Exists)
            {
                VerifyDirExists(captureImageloc, true);
                Manager.Current.Log.WriteLine(LogType.Information, "****Captured Images are Saved on Execution Server****");
                Manager.Current.Log.CaptureDesktop(captureImageloc + capturedImageName);
            }
            else
            {
                //VerifyDirExists(localExecutionCaptureImageLocation, true);
                //Manager.Current.Log.WriteLine(LogType.Information, "****Captured Images are Saved on Local System****");
                //Manager.Current.Log.CaptureDesktop(localExecutionCaptureImageLocation + capturedImageName);
                Manager.Current.Log.CaptureDesktop("\\" + capturedImageName);
            }

            Manager.Current.Log.WriteLine(ex.Message);
            //Add Dialog Handler if Found While Execution. 
            AddDialogHandler();
            throw new Exception(LogType.Error + ex.StackTrace);
        }
    }
}