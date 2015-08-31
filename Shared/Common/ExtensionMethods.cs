using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Shared.Common
{
    public static class ExtensionMethods
    {
        public static IEnumerable<XmlNode> AsEnumerable(this XmlNodeList nodeList)
        {
            foreach (XmlNode node in nodeList)
                yield return node;
        }

        /// <summary>
        /// Perform a deep Copy of the object.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(this T source)
        {
            Type type = typeof(T);
            if (!type.IsSerializable && !type.IsInterface)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }


        // *******************************************************************************************************
        // NOT READY YET !!!! error with collections

        ///// <summary>
        ///// Clone all property values from source to target object
        ///// </summary>
        ///// <param name="objSource"></param>
        ///// <param name="objTarget"></param>
        ///// <returns></returns>
        //public static object CloneToObject(this object objSource, object objTarget)
        //{
        //    //Get the type of source object and create a new instance of that type
        //    Type typeSource = objSource.GetType();
            
        //    bool isNewObject = false;
        //    if (objTarget == null)
        //    {
        //        isNewObject = true;
        //        objTarget = Activator.CreateInstance(typeSource);
        //    }

        //    //Get all the properties of source object type
        //    PropertyInfo[] propertyInfo = typeSource.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        //    //Assign all source property to taget object 's properties
        //    foreach (PropertyInfo property in propertyInfo)
        //    {
        //        //Check whether property can be written to
        //        if (property.CanWrite)
        //        {
        //            //check whether property type is value type, enum or string type
        //            if (property.PropertyType.IsValueType || property.PropertyType.IsEnum || property.PropertyType.Equals(typeof(System.String)))
        //            {
        //                property.SetValue(objTarget, property.GetValue(objSource, null), null);
        //            }
        //            //else property type is object/complex types, so need to recursively call this method until the end of the tree is reached
        //            else
        //            {
        //                object objPropertyValue = property.GetValue(objSource, null);
        //                if (objPropertyValue == null)
        //                {
        //                    property.SetValue(objTarget, null, null);
        //                }
        //                else
        //                {
        //                    // if target object is new then set value else copy properties to target
        //                    if(isNewObject)
        //                    {
        //                        property.SetValue(objTarget, objPropertyValue.CloneToObject(null), null);
        //                    }
        //                    else
        //                    {
        //                        object objTargetPropertyValue = property.GetValue(objTarget, null);
        //                        objPropertyValue.CloneToObject(objTargetPropertyValue);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return objTarget;
        //}



        ///// <summary>
        ///// Delegate handler that's used to compile the IL to.
        ///// (This delegate is standard in .net 3.5)
        ///// </summary>
        ///// <typeparam name="T1">Parameter Type</typeparam>
        ///// <typeparam name="TResult">Return Type</typeparam>
        ///// <param name="arg1">Argument</param>
        ///// <returns>Result</returns>
        //public delegate TResult Func<T1, TResult>(T1 arg1);
        ///// <summary>
        ///// This dictionary caches the delegates for each 'to-clone' type.
        ///// </summary>
        //static Dictionary<Type, Delegate> _cachedIL = new Dictionary<Type, Delegate>();


        ///// <summary>
        ///// Generic cloning method that clones an object using IL.
        ///// Only the first call of a certain type will hold back performance.
        ///// After the first call, the compiled IL is executed.
        ///// </summary>
        ///// <typeparam name="T">Type of object to clone</typeparam>
        ///// <param name="myObject">Object to clone</param>
        ///// <returns>Cloned object</returns>
        //private static T CloneObjectWithIL<T>(T myObject)
        //{
        //    Delegate myExec = null;
        //    if (!_cachedIL.TryGetValue(typeof(T), out myExec))
        //    {
        //        // Create ILGenerator
        //        DynamicMethod dymMethod = new DynamicMethod("DoClone", typeof(T), new Type[] { typeof(T) }, true);
        //        ConstructorInfo cInfo = myObject.GetType().GetConstructor(new Type[] { });

        //        ILGenerator generator = dymMethod.GetILGenerator();

        //        LocalBuilder lbf = generator.DeclareLocal(typeof(T));
        //        //lbf.SetLocalSymInfo("_temp");

        //        generator.Emit(OpCodes.Newobj, cInfo);
        //        generator.Emit(OpCodes.Stloc_0);
        //        foreach (FieldInfo field in myObject.GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic))
        //        {
        //            // Load the new object on the eval stack... (currently 1 item on eval stack)
        //            generator.Emit(OpCodes.Ldloc_0);
        //            // Load initial object (parameter)          (currently 2 items on eval stack)
        //            generator.Emit(OpCodes.Ldarg_0);
        //            // Replace value by field value             (still currently 2 items on eval stack)
        //            generator.Emit(OpCodes.Ldfld, field);
        //            // Store the value of the top on the eval stack into the object underneath that value on the value stack.
        //            //  (0 items on eval stack)
        //            generator.Emit(OpCodes.Stfld, field);
        //        }

        //        // Load new constructed obj on eval stack -> 1 item on stack
        //        generator.Emit(OpCodes.Ldloc_0);
        //        // Return constructed object.   --> 0 items on stack
        //        generator.Emit(OpCodes.Ret);

        //        myExec = dymMethod.CreateDelegate(typeof(Func<T, T>));
        //        _cachedIL.Add(typeof(T), myExec);
        //    }
        //    return ((Func<T, T>)myExec)(myObject);
        //}
    }
}
