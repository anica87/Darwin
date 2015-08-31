using System;
using PostSharp.Aspects;
using PostSharp.Reflection;
using PostSharp.Extensibility;

namespace Shared.Common
{
    [Serializable]
    public sealed class BackupDataAttribute : Attribute
    {
        public BackupDataAttribute()
        {
        }
    }
}
