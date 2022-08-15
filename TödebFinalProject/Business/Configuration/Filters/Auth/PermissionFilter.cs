using Business.Contants.Message;
using Core.Utilities.Results;
using Dto.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;


namespace Business.Configuration.Filters.Auth
{
    public class PermissionFilter : IAuthorizationFilter
    {
        private String[] _permissions;
        public PermissionFilter(String permissions)
        {
            _permissions = permissions.Split(',');
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var roleClaims = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
    
            if (roleClaims is null)
            {
                context.Result = new BadRequestObjectResult(new ErrorResult(ResultMessage.AuthorizationDenied));
            }
            else
            {
                foreach (var permission in _permissions)
                {
                    if (roleClaims.Contains(permission))
                    {
                        return;
                    }
                }
                context.Result = new BadRequestObjectResult(new ErrorResult(ResultMessage.AuthorizationDenied));
            }
        }
    }
}

