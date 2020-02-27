using System;
using System.Collections.Generic;
using System.Linq;

namespace O2OMapper.Base
{
    public abstract class ObjectCopyBase
    {

        protected abstract string GetMapKey(Type sourceType, Type targetType);
        public abstract void Copy(object source, object target);

        protected abstract IList<PropertyMap> GetMatchingProperties(Type sourceType, Type targetType);


    }

    public abstract class ObjectCopyReflection : ObjectCopyBase
    {
        protected abstract void MapTypes(Type source, Type target);

        protected override IList<PropertyMap> GetMatchingProperties
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
        protected override string GetMapKey(Type sourceType, Type targetType)
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
