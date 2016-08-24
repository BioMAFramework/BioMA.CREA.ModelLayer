namespace CRA.ModelLayer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Utility class to manage the clone of objects, including object of complex types like Dictionary, Arrays, List.
    /// </summary>
    public static class CloneHelper
    {
        /// <summary>
        /// Clones an object of type T and returns the copy of the object.
        /// </summary>
        /// <typeparam name="T">The Type of the object to clone</typeparam>
        /// <param name="item">The object to clone</param>
        /// <returns>The cloned copy of the object</returns>
        public static T CloneElement<T>(T item)
        {
            if (typeof(ICloneable).IsAssignableFrom(typeof(T)))
            {
                return (T) ((ICloneable) item).Clone();
            }
            if (typeof(ValueType).IsAssignableFrom(typeof(T)))
            {
                return item;
            }
            return item;
        }

        

        /// <summary>
        /// Executes a deep clone of an object and returns the copy of the object.
        /// </summary>        
        /// <param name="obj">The object to clone</param>
        /// <returns>The cloned copy of the object</returns>
        public static object DeepClone(object obj)
        {
            System.Type type;
            if (obj.GetType().IsArray)
            {
                return GetDeepCloneForArray(obj.GetType().GetElementType()).Invoke(null, new object[] { obj });
            }
            System.Type[] genericArguments = obj.GetType().GetGenericArguments();
            if (genericArguments.Length == 2)
            {
                type = typeof(Dictionary<,>);
                if (type.MakeGenericType(genericArguments).IsAssignableFrom(obj.GetType()))
                {
                    return GetDeepCloneForDictionary(genericArguments[0], genericArguments[1]).Invoke(null, new object[] { obj });
                }
            }
            if (genericArguments.Length == 1)
            {
                type = typeof(List<>);
                if (type.MakeGenericType(genericArguments).IsAssignableFrom(obj.GetType()))
                {
                    return GetDeepCloneForList(genericArguments[0]).Invoke(null, new object[] { obj });
                }
            }
            return obj;
        }

        /// <summary>
        /// Executes a deep clone an object of type List&lt;T&gt; and returns the copy of the object.
        /// </summary>
        /// <typeparam name="T">The Type of the items of the list to clone</typeparam>
        /// <param name="original">The object to clone</param>
        /// <returns>The cloned copy of the object</returns>
        public static List<T> DeepClone<T>(List<T> original)
        {
            return Enumerable.Select<T, T>(original, new Func<T, T>(CloneHelper.CloneElement<T>)).ToList<T>();
        }

        /// <summary>
        /// Executes a deep clone an object of type 'array of T'; and returns the copy of the object.
        /// </summary>
        /// <typeparam name="T">The Type of the items of the array to clone</typeparam>
        /// <param name="original">The object to clone</param>
        /// <returns>The cloned copy of the object</returns>
        public static T[] DeepClone<T>(T[] original)
        {
            return Enumerable.Select<T, T>(original, new Func<T, T>(CloneHelper.CloneElement<T>)).ToArray<T>();
        }

        /// <summary>
        /// Executes a deep clone an object of type Dictionary&lt;K,V&gt; and returns the copy of the object.
        /// </summary>
        /// <typeparam name="K">The Type of the keys of the items of the dictionary to clone</typeparam>
        /// <typeparam name="V">The Type of the values of the items of the dictionary to clone</typeparam>
        /// <param name="original">The object to clone</param>
        /// <returns>The cloned copy of the object</returns>
        public static Dictionary<K, V> DeepClone<K, V>(Dictionary<K, V> original)
        {
            return Enumerable.ToDictionary<KeyValuePair<K, V>, K, V>(original.AsEnumerable<KeyValuePair<K, V>>(), (Func<KeyValuePair<K, V>, K>) (kvp => CloneElement<K>(kvp.Key)), (Func<KeyValuePair<K, V>, V>) (kvp => CloneElement<V>(kvp.Value)));
        }

        internal static MethodInfo GetDeepCloneForArray(System.Type elementType)
        {
            MethodInfo info = null;
            foreach (MethodInfo info2 in from m in typeof(CloneHelper).GetMethods()
                where m.Name.Equals("DeepClone") && (m.GetGenericArguments().Length == 1)
                select m)
            {
                MethodInfo info3 = info2.MakeGenericMethod(new System.Type[] { elementType });
                ParameterInfo info4 = info3.GetParameters()[0];
                System.Type type = typeof(List<>);
                System.Type[] typeArguments = new System.Type[] { elementType };
                System.Type c = typeof(Enumerable).GetMethod("ToArray").MakeGenericMethod(new System.Type[] { elementType }).Invoke(null, new object[] { Activator.CreateInstance(type.MakeGenericType(typeArguments)) }).GetType();
                if (info4.ParameterType.IsAssignableFrom(c))
                {
                    info = info3;
                }
            }
            return info;
        }

        internal static MethodInfo GetDeepCloneForDictionary(System.Type typeKey, System.Type typeValue)
        {
            return (from m in typeof(CloneHelper).GetMethods()
                where m.Name.Equals("DeepClone") && (m.GetGenericArguments().Length == 2)
                select m).Single<MethodInfo>().MakeGenericMethod(new System.Type[] { typeKey, typeValue });
        }

        internal static MethodInfo GetDeepCloneForList(System.Type elementType)
        {
            MethodInfo info = null;
            foreach (MethodInfo info2 in from m in typeof(CloneHelper).GetMethods()
                where m.Name.Equals("DeepClone") && (m.GetGenericArguments().Length == 1)
                select m)
            {
                MethodInfo info3 = info2.MakeGenericMethod(new System.Type[] { elementType });
                ParameterInfo info4 = info3.GetParameters()[0];
                System.Type type = typeof(List<>);
                System.Type[] typeArguments = new System.Type[] { elementType };
                System.Type c = type.MakeGenericType(typeArguments);
                if (info4.ParameterType.IsAssignableFrom(c))
                {
                    info = info3;
                }
            }
            return info;
        }
    }
}

