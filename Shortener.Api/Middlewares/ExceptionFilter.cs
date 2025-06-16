using System.Net;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Shortener.Shared.Exceptions;

namespace Shortener.Api.Middlewares;

public class ExceptionFilter : IExceptionFilter
{
	ILogger<ExceptionFilter> _logger;

	public ExceptionFilter(ILogger<ExceptionFilter> logger)
	{
		_logger = logger;
	}

	public void OnException(ExceptionContext context)
	{
		object ex = context.Exception;
		_logger.LogError((ex as Exception)?.Message + "\n" + (ex as Exception)?.StackTrace);
		if(ex is ShortenerUsedException usEx)
		{
			context.Result = new ObjectResult(new
			{
				StatusCode = HttpStatusCode.BadRequest,
				Message = usEx.FieldName + " is already used",
				usEx.FieldName
			})
			{
				StatusCode = (int)HttpStatusCode.BadRequest
			};
		}
		else if(ex is ShortenerArgumentException argEx)
		{
			context.Result = new ObjectResult(new
			{
				StatusCode = HttpStatusCode.BadRequest,
				Message = argEx.FieldName + " is invalid",
				argEx.FieldName
			})
			{
				StatusCode = (int)HttpStatusCode.BadRequest
			};
		}
		else if(ex is ShortenerPermissionException permEx)
		{
			context.Result = new ObjectResult(new
			{
				StatusCode = HttpStatusCode.Forbidden,
				Message = permEx.Restriction + " does not allow you this action",
				permEx.Restriction
			})
			{
				StatusCode = (int)HttpStatusCode.Forbidden
			};
		}
		else if(ex is ShortenerNotFoundException notFoundEx)
		{
			context.Result = new ObjectResult(new
			{
				StatusCode = HttpStatusCode.NotFound,
				Message = notFoundEx.EntityName + " with id " + notFoundEx.EntityId + " not found",
				notFoundEx.EntityName
			})
			{
				StatusCode = (int)HttpStatusCode.NotFound
			};
		}
		else if(ex is ShortenerException shortEx)
		{
			context.Result = new ObjectResult(new
			{
				StatusCode = HttpStatusCode.BadRequest,
				Message = shortEx.Message
			})
			{
				StatusCode = (int)HttpStatusCode.BadRequest
			};
		}
		else
		{
			context.Result = new ObjectResult(new
			{
				StatusCode = HttpStatusCode.InternalServerError,
				Message = "Something went wrong"
			})
			{
				StatusCode = (int)HttpStatusCode.InternalServerError
			};
		}
		context.ExceptionHandled = true;
	}
}
