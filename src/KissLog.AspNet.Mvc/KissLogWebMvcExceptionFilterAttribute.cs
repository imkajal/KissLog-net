﻿using System.Net;
using System.Web;
using System.Web.Mvc;
using KissLog.AspNet.Web;

namespace KissLog.AspNet.Mvc
{
    public class KissLogWebMvcExceptionFilterAttribute : HandleErrorAttribute
    {
        static KissLogWebMvcExceptionFilterAttribute()
        {
            PackageInit.Init();
        }

        public override void OnException(ExceptionContext filterContext)
        {
            ILogger logger = Logger.Factory.Get();
            logger.Log(LogLevel.Error, filterContext.Exception);

            if (filterContext.Exception is HttpException)
            {
                HttpException httpException = (HttpException)filterContext.Exception;
                HttpStatusCode statusCode = (HttpStatusCode)httpException.GetHttpCode();

                logger.SetHttpStatusCode(statusCode);
            }

            base.OnException(filterContext);
        }
    }
}
