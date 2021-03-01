using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

using SVPP.Models;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Authorization.Infrastructure;

using Microsoft.AspNetCore.Identity;

using static SVPP.Authorization.Operations;



namespace SVPP.Authorization

{

    public class NonprofitIsOwnerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Nonprofit>

    {

        UserManager<IdentityUser> _userManager;



        public NonprofitIsOwnerAuthorizationHandler(UserManager<IdentityUser> userManager)

        {

            _userManager = userManager;

        }



        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Nonprofit resource)

        {

            if (context.User == null || resource == null)

            {

                return Task.CompletedTask;

            }

            // If not asking for CRUD permission, return.

            if (requirement.Name != Constants.CreateOperationName &&

                requirement.Name != Constants.ReadOperationName &&

                requirement.Name != Constants.UpdateOperationName &&

                requirement.Name != Constants.DeleteOperationName)

            {

                return Task.CompletedTask;

            }

            var allegedly = _userManager.GetUserId(context.User);

            if (resource.OwnerId == _userManager.GetUserId(context.User))

            {

                context.Succeed(requirement);

            }



            return Task.CompletedTask;

        }

    }

}