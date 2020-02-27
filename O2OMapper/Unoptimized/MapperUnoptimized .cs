using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2OMapper.Unoptimized
{
    public class MapperUnoptimized : ObjectCopyBase
    {
        public override void MapTypes(Type source, Type target)
        {
        }

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
