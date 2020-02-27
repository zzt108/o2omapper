using System;
using O2OMapper.Base;

namespace O2OMapper.Mappers
{
    public class MapperUnoptimized : ObjectCopyReflection
    {

        public override void Copy(object source, object target)
        {
            var sourceType = source.GetType();
            var targetType = target.GetType();
            var propMap = GetMatchingProperties(sourceType, targetType);
           
            foreach (var prop in propMap)
            {
                var sourceValue = prop.SourceProperty.GetValue(source, null);
                prop.TargetProperty.SetValue(target, sourceValue, null);
            }
        }
    }
}
