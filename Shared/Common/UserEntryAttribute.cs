using System;
using PostSharp.Aspects;
using PostSharp.Reflection;
using PostSharp.Extensibility;

namespace Shared.Common
{
    [Serializable]
    public sealed class UserEntryAttribute : Attribute
    {
        public string FieldGroup { get; set; }

        public UserEntryAttribute()
        {
        }

        public UserEntryAttribute(string fieldGroup)
        {
            FieldGroup = fieldGroup;
        }
    }
}
