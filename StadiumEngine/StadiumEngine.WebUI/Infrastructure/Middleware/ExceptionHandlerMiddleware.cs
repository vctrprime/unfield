using System.IO;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StadiumEngine.Common.Exceptions;

namespace StadiumEngine.WebUI.Infrastructure.Middleware;

/// <summary>
/// Глобальная обработка исключений
/// </summary>
public static class ExceptionHandlerMiddleware
{
    /// <summary>
    /// Сконфигурировать
    /// </summary>
    /// <param name="app"></param>
    /// <param name="logger"></param>
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                
                //если исключение не предметно, а системное - запись в лог и возврат 500
                if (contextFeature is { Error: not DomainException })
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    logger.LogError(contextFeature.Error, "Ошибка сервера при обработке запроса");

                    await context.Response.WriteAsync(new ErrorDetails
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Internal Server Error."
                    }.ToString());
                }
                //иначе возврат 400 и возврат текста сообщения
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";
                    
                    await context.Response.WriteAsync(new ErrorDetails
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature?.Error.Message
                    }.ToString());
                }
            });
        });
    }
    
    /// <summary>
    /// Объект ошибки
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// Код
        /// </summary>
        public int StatusCode { get; set; }
        
        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Строковое представление
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}