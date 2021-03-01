using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace SVPP.Validations
{
    public class CurrentDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {

            DateTime dateTime = Convert.ToDateTime(value);
            return dateTime <= DateTime.Now;

        }
    }
}