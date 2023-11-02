using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miro.Shared.Map
{
    public class GenericMapper<TSource, TDestination>
    {
        public TDestination Map(TSource source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Source object is null.");
            }

            var destination = Activator.CreateInstance<TDestination>();
            var sourceProperties = typeof(TSource).GetProperties();
            var destinationProperties = typeof(TDestination).GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                var destinationProperty = destinationProperties.FirstOrDefault(p => p.Name == sourceProperty.Name);
                if (destinationProperty != null && destinationProperty.PropertyType == sourceProperty.PropertyType)
                {
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                }
            }

            return destination;
        }
    }
}
