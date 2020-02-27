using System;
using System.Collections.Generic;
using System.Linq;

namespace O2OMapper.Base
{
    public abstract class ObjectCopyBase
    {

        public abstract void Copy(object source, object target);


    }

    public abstract class ObjectCopyReflection : ObjectCopyBase
    {
        protected virtual IList<PropertyMap> GetMatchingProperties
            (Type sourceType, Type targetType)
        {
            var sourceProperties = sourceType.GetProperties();
            var targetProperties = targetType.GetProperties();

            var properties = (from s in sourceProperties
                              from t in targetProperties
                              where s.Name == t.Name &&
                                    s.CanRead &&
                                    t.CanWrite &&
                                    s.PropertyType == t.PropertyType
                              select new PropertyMap
                              {
                                  SourceProperty = s,
                                  TargetProperty = t
                              }).ToList();
            return properties;
        }
        /// <summary>
        /// generates the type map key for the mappings cache in Optimized version
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        protected virtual string GetMapKey(Type sourceType, Type targetType)
        {
            //todo move to optimized class, if not used in other Mapper variants
            var keyName = "Copy_";
            keyName += sourceType.FullName.Replace(".", "_").Replace("+", "_");
            keyName += "_";
            keyName += targetType.FullName.Replace(".", "_").Replace("+", "_");

            return keyName;
        }
    }
}
