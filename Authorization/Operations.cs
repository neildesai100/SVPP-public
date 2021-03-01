using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization.Infrastructure;



namespace SVPP.Authorization
{
    public class Operations
    {
        public static class ModelOperations
        {
            public static OperationAuthorizationRequirement Create =

              new OperationAuthorizationRequirement { Name = Constants.CreateOperationName };

            public static OperationAuthorizationRequirement Read =

              new OperationAuthorizationRequirement { Name = Constants.ReadOperationName };

            public static OperationAuthorizationRequirement Update =

              new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };


            public static OperationAuthorizationRequirement Delete =

              new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };



            /*public static OperationAuthorizationRequirement Approve =
              new OperationAuthorizationRequirement { Name = Constants.ApproveOperationName };

            public static OperationAuthorizationRequirement Reject =
              new OperationAuthorizationRequirement { Name = Constants.RejectOperationName };*/

            // for future reference -- if want to implement approving and rejecting applications and the such 

            // https://docs.microsoft.com/en-us/aspnet/core/security/authorization/secure-data?view=aspnetcore-3.1

        }



        public class Constants
        {

            public static readonly string CreateOperationName = "Create";

            public static readonly string ReadOperationName = "Read";

            public static readonly string UpdateOperationName = "Update";

            public static readonly string DeleteOperationName = "Delete";

            //public static readonly string ApproveOperationName = "Approve";

            //public static readonly string RejectOperationName = "Reject";

            public static readonly string AdminsRole = "Admins";

            public static readonly string PartnersRole = "Partners";

            public static readonly string NonproftRole = "Nonprofits";

        }

    }

}