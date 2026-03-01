using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SQLiteDemos.System.Helpers
{
    //these methods can be used anywhere in the library or by a 
    //  outside project that has access to the library

    //the class has been method static so the user does NOT need to
    //  instanitate an instance of the class, just call the method
    public static class ValidatorHelper
    {
        //even though there is validation on the entities, the validatin
        //  is not always called automatically for your application
        //The automatic call is dependent on the type of application you are running
        //console apps do not automatically call the validation annotation
        //Passing your entity to this method will cause the annotation validaiton to execute
        public static void Validate(object entity)
        {
            var context = new ValidationContext(entity);
            Validator.ValidateObject(entity, context, true);
        }
    }
}
