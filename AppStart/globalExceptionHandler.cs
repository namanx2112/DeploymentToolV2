﻿using DeploymentTool.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace DeploymentTool.AppStart
{
    public class globalExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var exception = context.Exception;
            TraceUtility.ForceWriteException("globalExceptionHandler", "Exception Occured Message:{0}, InnerException:{1}", exception.Message, (exception.InnerException != null) ? exception.InnerException.Message : "");
            //log the exception here
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An error occurred while processing your request." + exception),
                ReasonPhrase = "Internal Server Error"
            };
            context.Response = response;
        }
    }
}