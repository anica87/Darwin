using System;
using PostSharp.Aspects;
using PostSharp.Reflection;
using PostSharp.Extensibility;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;

namespace Shared.Common
{
    [Serializable]
    public sealed class NotifyPropertyChangedAttribute : LocationInterceptionAspect
    {
        private List<string> propertyNames = new List<string>();
        private MethodInfo propertyChangedMethodInfo = null;
        private bool initialized = false;

        private bool isUserEntry = false;

        private MethodInfo backupMethodInfo = null;
        private bool hasBackupData = false; 

        #region Build-Time Logic

        public NotifyPropertyChangedAttribute()
        {
        }

        //public override bool CompileTimeValidate(LocationInfo location)
        //{
        //    // TODO: Check that the aspect has been applied on a proper field or property.

        //    if (false)
        //    {
        //        Message.Write(location, SeverityType.Error, "MY001", "Cannot apply NotifyPropertyChangedAttribute to '{0}'.", location);
        //        return false;
        //    }

        //    return true;
        //}

        public override void CompileTimeInitialize(LocationInfo location, AspectInfo aspectInfo)
        {
            // TODO: Initialize any instance field whose value only depends on the field or property to which the aspect is applied.

            // get the DependencyAttributes of the method
            Attribute[] atts = new Attribute[] { };

            string propertyName = "";
            Type propertyType = null;

            switch (location.LocationKind)
            {
                case LocationKind.Field:
                    atts = Attribute.GetCustomAttributes(location.FieldInfo);
                    propertyName = location.FieldInfo.Name;
                    propertyType = location.FieldInfo.FieldType;
                    break;

                case LocationKind.Parameter:
                    atts = Attribute.GetCustomAttributes(location.ParameterInfo);
                    propertyName = location.ParameterInfo.Name;
                    propertyType = location.ParameterInfo.ParameterType;
                    break;

                case LocationKind.Property:
                    atts = Attribute.GetCustomAttributes(location.PropertyInfo);
                    propertyName = location.PropertyInfo.Name;
                    propertyType = location.PropertyInfo.PropertyType;
                    break;

                case LocationKind.ReturnValue:
                    break;

                default:
                    break;
            }

            if (!string.IsNullOrEmpty(propertyName))
            {
                if (propertyName.StartsWith("<") && propertyName.EndsWith("_BakingField"))
                {
                    propertyName = propertyName.Substring(1, propertyName.IndexOf(">") - 1);
                }

                propertyNames.Add(propertyName);
            }

            foreach (Attribute att in atts)
            {
                if (att is NotifyingDependencyAttribute)
                {
                    string dp = (att as NotifyingDependencyAttribute).DependencyProperty;
                    if (dp != null)
                        propertyNames.AddRange(dp.Split(new char[] { ',', ' ', '|' }));
                }
                else if (att is UserEntryAttribute)
                {
                    isUserEntry = true;
                }
                else if (att is BackupDataAttribute)
                {
                    hasBackupData = true;
                }
            }

            // clear names from spaces
            for (int i = 0; i < propertyNames.Count; i++)
                propertyNames[i] = propertyNames[i].Trim();

        }

        #endregion

        //public override void RuntimeInitialize(LocationInfo location)
        //{
        //    //// This method is invoked once at run time.

        //    //Attribute[] atts = new Attribute[] { };

        //    //switch (location.LocationKind)
        //    //{
        //    //    case LocationKind.Field:
        //    //        atts = Attribute.GetCustomAttributes(location.FieldInfo, typeof(NotifyingDependencyAttribute));
        //    //        this.propertyNames.Add(location.FieldInfo.Name);
        //    //        break;

        //    //    case LocationKind.Parameter:
        //    //        atts = Attribute.GetCustomAttributes(location.ParameterInfo, typeof(NotifyingDependencyAttribute));
        //    //        this.propertyNames.Add(location.PropertyInfo.Name);
        //    //        break;

        //    //    case LocationKind.Property:
        //    //        atts = Attribute.GetCustomAttributes(location.PropertyInfo, typeof(NotifyingDependencyAttribute));
        //    //        break;

        //    //    case LocationKind.ReturnValue:
        //    //        break;

        //    //    default:
        //    //        break;
        //    //}

        //    //foreach (Attribute att in atts)
        //    //    propertyNames.Add((att as NotifyingDependencyAttribute).DependencyProperty);

        //}

        //public override void OnGetValue(LocationInterceptionArgs args)
        //{
        //    // args.Instance contains the object whose property or field is loaded (null if the location is static).    

        //    base.OnGetValue(args);      // This is equivalent to doing args.ProceedGetValue

        //    // After you call base.OnGetValue, args.Value contains the current value of the underlying field or property.
        //    // You can change args.Value to another value as long as it is compatible with the type of the underlying field or property.

        //}

        public override void OnSetValue(LocationInterceptionArgs args)
        {
            // args.Instance contains the object whose property or field is loaded (null if the location is static).
            // args.Value contains the new value that needs to be assigned to the underlying field or property.
            // args.GetCurrentValue to get the current value of the field of property.

            // Before calling base.OnSetValue, you can change args.Value to another value as long as it is compatible with the type of 
            // the underlying field or property.

            if (!initialized)
            {
                this.propertyChangedMethodInfo = args.Instance.GetType().GetMethod("NotifyPropertyChanged");
                this.backupMethodInfo = args.Instance.GetType().GetMethod("SaveDataToBackup");
                initialized = true;
            }

            // do backup if marked for backup or user entry
            if ((hasBackupData || isUserEntry) && backupMethodInfo != null)
            {
                backupMethodInfo.Invoke(
                    args.Instance,
                    new object[] 
                    { 
                        args.LocationName, 
                        args.GetCurrentValue()
                    });
                
            }
              
            base.OnSetValue(args);      // This is equivalent to doing args.ProceedSetValue


            // fire the Notify on the invoking Instance
            if (this.propertyChangedMethodInfo != null)
            {
                
                foreach (string prop in propertyNames)
                    propertyChangedMethodInfo.Invoke(
                        args.Instance, 
                        new object[] 
                        { 
                            prop, 
                            args.LocationName.Equals(prop) 
                        });
            }
        }

        // TODO: For better performance, delete any method that the aspect does not use.
    }
}
