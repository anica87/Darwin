using System;
using PostSharp.Aspects;
using PostSharp.Reflection;
using PostSharp.Extensibility;

namespace Shared.Common
{
    [Serializable]
    public sealed class NotifyingDependencyAttribute : LocationInterceptionAspect
    {
        public string DependencyProperty { get; set; }

        #region Build-Time Logic

        public NotifyingDependencyAttribute()
        {
        }

        public NotifyingDependencyAttribute(string dp)
        {
            DependencyProperty = dp;
        }

        //public override bool CompileTimeValidate(LocationInfo location)
        //{
        //    // TODO: Check that the aspect has been applied on a proper field or property.

        //    if (false)
        //    {
        //        Message.Write(location, SeverityType.Error, "MY001", "Cannot apply NotifyDependencyAttribute to '{0}'.", location);
        //        return false;
        //    }

        //    return true;
        //}

        //public override void CompileTimeInitialize(LocationInfo location, AspectInfo aspectInfo)
        //{
        //    // TODO: Initialize any instance field whose value only depends on the field or property to which the aspect is applied.
        //}

        #endregion

        //public override void RuntimeInitialize(LocationInfo location)
        //{
        //    // This method is invoked once at run time.
        //}

        //public override void OnGetValue(LocationInterceptionArgs args)
        //{
        //    // args.Instance contains the object whose property or field is loaded (null if the location is static).    

        //    base.OnGetValue(args);      // This is equivalent to doing args.ProceedGetValue

        //    // After you call base.OnGetValue, args.Value contains the current value of the underlying field or property.
        //    // You can change args.Value to another value as long as it is compatible with the type of the underlying field or property.

        //}

        //public override void OnSetValue(LocationInterceptionArgs args)
        //{
        //    // args.Instance contains the object whose property or field is loaded (null if the location is static).
        //    // args.Value contains the new value that needs to be assigned to the underlying field or property.
        //    // args.GetCurrentValue to get the current value of the field of property.

        //    // Before calling base.OnSetValue, you can change args.Value to another value as long as it is compatible with the type of 
        //    // the underlying field or property.

        //    base.OnSetValue(args);      // This is equivalent to doing args.ProceedSetValue
        //}

        // TODO: For better performance, delete any method that the aspect does not use.
    }
}
