using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;
using System.Linq;
using System;
using P2P.Testing.Shared.Class;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArtOfTest.Common.Exceptions;

namespace P2P.Testing.Shared
{
    public class P2PLogin : BaseTest
    {
        private Browser _browser;
        private Pages _loginPage = new Pages(Manager.Current);

        public P2PLogin(Browser browser)
        {
            _browser = browser;
        }

        //Method for Login into P2P
        public void LoginToSilverlightClient(string username, string password)
        {

            //Maximize the Browser
            _browser.Window.Maximize();
            try
            {
                //Launch the URL 
                _browser.NavigateTo("/Portal?forms=1&sl=1");
                //Wait for UserName Textbox Exists in DOM
                _loginPage.P2P_Login.P2P_Login_Username.Wait.ForExists(Globals.timeOut);
                //Enter UserName
                _loginPage.P2P_Login.P2P_Login_Username.Text = username;
                //Wait for Password Textbox Exists in DOM
                _loginPage.P2P_Login.P2P_Login_Password.Wait.ForExists(Globals.timeOut);
                //Enter Password
                _loginPage.P2P_Login.P2P_Login_Password.Text = password;
            }
            catch (Exception ex)
            {
                 throw new Exception(LogType.Information + "Login Failed" + ex.Message);
            }

            //Click on Login Button
            _loginPage.P2P_Login.P2P_Login_Submit.Click();

            //Wait for Action to be Perform
            System.Threading.Thread.Sleep(Globals.timeOut);
        }

        //Method for Logout from P2P
        public void LogoutFromSilverlightClient()
        {
            //Wait for the logout Button Exists in DOM
            _loginPage.P2P_Application.SilverlightApp.P2P_LogoutButton.Wait.ForExists(Globals.timeOut);
            //Click on Logout Button
            _loginPage.P2P_Application.SilverlightApp.P2P_LogoutButton.User.Click();
            //Wait for Action to be Perform
            System.Threading.Thread.Sleep(Globals.pause);
            //Wait for the logout User Button Exists in DOM
            _loginPage.P2P_Application.SilverlightApp.P2P_LogoutUser.Wait.ForExists(Globals.timeOut);
            //Click on Logout User Button
            _loginPage.P2P_Application.SilverlightApp.P2P_LogoutUser.User.Click();
            ////Wait for Action to be Perform
            //System.Threading.Thread.Sleep(Globals.pause);
        }

        //Method to get Full User name
        public string GetFullUserName(string loginString)
        {
            //declare varaible and assign it default value
            string fullName = "";

            try
            {
                //Create Connection
                SqlConnection thisConnection = new SqlConnection("Data Source=(local); Initial Catalog=IA; Integrated Security=true;User Id=sa;Password=basware");
                //Open Connection
                thisConnection.Open();

                //Create an SQL command
                SqlCommand thisCommand = thisConnection.CreateCommand();

                //Create Parameter and assign it value
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@login_account";
                param.Value = loginString;

                //SQL command 
                thisCommand.CommandText = "SELECT TOP 1 First_Name,Surname FROM [IA].[dbo].[ADM_USER_DATA] where login_account = @login_account";
                //Add Parameter to SQL
                thisCommand.Parameters.Add(param);

                //Execute the Query
                SqlDataReader thisReader = thisCommand.ExecuteReader();

                //Read the Query result
                while (thisReader.Read())
                {
                    fullName = ((String)thisReader["Surname"] + " " + (String)thisReader["First_Name"]);
                }

                //Close all connections
                thisReader.Close();
                thisConnection.Close();
            }
            catch (Exception ex)
            {
                //Log Error if DB access returned an error
                Manager.Current.Log.WriteLine(LogType.Error, " Error while reading full user name from SQL Database. " + ex.Message);
            }

            //Return value
            return fullName;
        }
        
        //Method to Login into the Application
        public void LoginToSilverlightClient2(string username, string password)
        {
            //Check browser is of Alusta and is not session ended.            
            if (_browser.Url.ToUpper().Contains("P2PPORTAL") && !(_browser.Url.ToUpper().Contains("ENDED")))
            {
                try
                {
                    //Wait for the logout Button Exists in DOM
                    _loginPage.P2P_Application.SilverlightApp.P2P_LogoutButton.Wait.ForExists(Globals.timeOut);
                    //Click on Logout Button
                    _loginPage.P2P_Application.SilverlightApp.P2P_LogoutButton.User.Click();

                    //Verify P2P_LogoutButton button is visible or not
                    //Grey Area Special Case : When any script fails on a step which has a popup open, main window is greyed out. And Logout button is not accessible. Second script needs to force login.
                    Assert.AreEqual(ArtOfTest.WebAii.Silverlight.UI.Visibility.Visible, _loginPage.P2P_Application.SilverlightApp.P2P_LogoutUser.ComputedVisibility, "Element visibility does not match expected value");

                    //After clicking on Logout link check If session has not expired 
                    if (!(_browser.Url.ToUpper().Contains("ENDED")))
                    {
                        //Get Current user logged in from screen
                        string currentUser = _loginPage.P2P_Application.SilverlightApp.P2P_Logout_UserName_TextBlock.Text.ToUpper();
                        //Get Complete name of user and store variable expectedUser
                        string expectedUser = GetFullUserName(username).ToUpper();

                        //Check Current User
                        if (expectedUser == "")
                        {
                            //Log Error if user name not returned
                            Manager.Current.Log.WriteLine(LogType.Error, " Incorrect Login String: Could not find a the valid user name for string: " + username);
                        }
                        else
                        {
                            //Check if correct user is logged in
                            if (currentUser == expectedUser)
                            {
                                //Log the results when correct user logged in
                                Manager.Current.Log.WriteLine(LogType.Information, " Continue with current user logged in: " + currentUser);

                                //Goto Label to end the function     
                                goto Over;
                            }
                            else
                            {
                                //Log the results when correct user logged in
                                Manager.Current.Log.WriteLine(LogType.Information, " Current User logged in: (" + currentUser + "). Need to login with User: (" + expectedUser + "). Logout and login again.");

                                //Click on Logout Button to close pop up
                                _loginPage.P2P_Application.SilverlightApp.P2P_LogoutButton.User.Click();

                                //Logout of application if current user is different
                                LogoutFromSilverlightClient();
                            }
                        }

                    }
                }
                catch (Exception)
                {
                    Manager.Current.Log.WriteLine(LogType.Information, " Some Exception occured. Force Login.");
                }
            }
            //Call the method to login into application
            //LoginToAlutsaApplication(username, password);

            //Label :End of function  
        Over: ;
        }
    }
}
