using System;
using System.Collections;


namespace GameLib
{
    /// <summary>
    /// Static class used for making clones of objects.
    /// </summary>
    static class CloneMaker
    {

        public static T Clone<T>(T obj)
        {
            if (obj == null)
                return default;

            Type type = obj.GetType();
            if (type.IsValueType || type == typeof(string))
                return obj;

            object clone = Activator.CreateInstance(type);

            if (IsList(obj))
                return (T)CloneList(obj, clone);

            foreach (var property in type.GetProperties())
            {
                if (property.GetMethod == null || property.SetMethod == null)
                    continue;

                object propertyValue = property.GetValue(obj);
                object cloneValue = Clone(propertyValue);
                property.SetValue(clone, cloneValue);
            }

            return (T)clone;
        }

        private static bool IsList(object obj)
            => obj as IList != null;

        private static object CloneList(object obj, object clone)
        {
            IList objList = (IList)obj;
            IList cloneList = (IList)clone;

            foreach (var item in objList)
            {
                object clonedItem = Clone(item);
                cloneList.Add(clonedItem);
            }

            return cloneList;
        }

    }
}
