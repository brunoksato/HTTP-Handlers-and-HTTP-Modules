using System;
using System.Web;
using System.Security.Principal;

namespace SecurityModules
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class CustomAuthenticationModule : IHttpModule
	{
		public CustomAuthenticationModule()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public void Init(HttpApplication r_objApplication)
		{
			r_objApplication.AuthenticateRequest += new EventHandler(this.AuthenticateRequest) ;
		}

		public void Dispose()
		{
		}

		private void AuthenticateRequest(object r_objSender, EventArgs r_objEventArgs)
		{
			HttpApplication objApp = (HttpApplication) r_objSender ;
			HttpContext objContext = (HttpContext) objApp.Context ;

			if ( (objApp.Request["userid"] == null) || (objApp.Request["password"] == null) )
			{
				objContext.Response.Write("<H1>Credentials not provided</H1>") ;
				objContext.Response.End() ;
			}

			string userid = "" ;
			userid = objApp.Request["userid"].ToString() ;

			string password = "" ;
			password = objApp.Request["password"].ToString() ;
			
			string[] strRoles ;
			strRoles = AuthenticateAndGetRoles(userid, password) ;
			if ((strRoles == null) || (strRoles.GetLength(0) == 0))
			{
				objContext.Response.Write("<H1>We are sorry but we could not find this user id and password in our database</H1>") ;
				objApp.CompleteRequest() ;
			}
			
			GenericIdentity objIdentity = new GenericIdentity(userid, "CustomAuthentication") ;
			objContext.User = new GenericPrincipal(objIdentity, strRoles) ;
		}

		private string[] AuthenticateAndGetRoles(string r_strUserID, string r_strPassword)
		{
			string[] strRoles = null ;
			if ((r_strUserID.Equals("Steve")) && (r_strPassword.Equals("15seconds")))
			{
				strRoles = new String[1] ;
				strRoles[0] = "Administrator" ;
			}
			else if ((r_strUserID.Equals("Mansoor")) && (r_strPassword.Equals("mas")))
			{
				strRoles = new string[1] ;
				strRoles[0] = "User" ;				
			}
			return strRoles ;
		}
	}
}
