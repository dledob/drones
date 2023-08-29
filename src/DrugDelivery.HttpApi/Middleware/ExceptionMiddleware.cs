using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DrugDelivery.Core.Exceptions;
using DrugDelivery.Shared.Models;

namespace DrugDelivery.HttpApi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);        
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        if (exception is DuplicateException duplicationException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode.ToString(),
                Message = duplicationException.Message
            }.ToString());
        }
        else if (exception is ValidateModelException modelException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = modelException.ErrorCode ?? context.Response.StatusCode.ToString(),
                Message = modelException.Message
            }.ToString());
        }
        else if (exception is DrugDeliveryException drugDeliveryException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = drugDeliveryException.ErrorCode ?? context.Response.StatusCode.ToString(),
                Message = drugDeliveryException.Message
            }.ToString());
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode.ToString(),
                Message = exception.Message
            }.ToString());
        }
    }
}
