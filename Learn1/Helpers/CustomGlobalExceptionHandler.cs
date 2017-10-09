using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Learn1.Helpers
{
    public class CustomGlobalExceptionHandler : IExceptionFilter {
		public void OnException(ExceptionContext context) {
			//HttpStatusCode status = HttpStatusCode.InternalServerError;
			//String message = String.Empty;

			Log.Error(context.Exception,"Test Message 1");
			//var exceptionType = context.Exception.GetType();
			//if (exceptionType == typeof(UnauthorizedAccessException)) {
			//	message = "Unauthorized Access";
			//	status = HttpStatusCode.Unauthorized;
			//} else if (exceptionType == typeof(NotImplementedException)) {
			//	message = "A server error occurred.";
			//	status = HttpStatusCode.NotImplemented;
			//} else if (exceptionType == typeof(MyAppException)) {
			//	message = context.Exception.ToString();
			//	status = HttpStatusCode.InternalServerError;
			//} else {
			//	message = context.Exception.Message;
			//	status = HttpStatusCode.NotFound;
			//}
			//HttpResponse response = context.HttpContext.Response;
			//response.StatusCode = (int)status;
			//response.ContentType = "application/json";
			//var err = message + " " + context.Exception.StackTrace;
			//response.WriteAsync(err);
		}
	}
	public class MyAppException : Exception {
		public MyAppException() { }

		public MyAppException(string message)
			 : base(message) { }

		public MyAppException(string message, Exception innerException)
			 : base(message, innerException) { }
	}
}
