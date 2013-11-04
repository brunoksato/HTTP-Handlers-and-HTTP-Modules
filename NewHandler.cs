using System;
using System.Web;
using System.Web.SessionState;

namespace MyHandler
{
	/// <summary>
	/// Summary description for NewHandler.
	/// </summary>
	public class NewHandler : IHttpHandler
	{
		public NewHandler()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Implementation of IHttpHandler
		public void ProcessRequest(System.Web.HttpContext context)
		{
			HttpResponse objResponse = context.Response ;
			HttpSessionState objSession = context.Session ;
			objResponse.Write("<html><body><h1>Hello 15Seconds Reader") ;
			objResponse.Write("</body></html>") ;
		}

		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
		#endregion
	}
}
