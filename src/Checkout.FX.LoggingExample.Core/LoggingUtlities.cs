using System.Collections.Generic;

namespace Checkout.FX.LoggingExample.Core
{
    public static class LoggingUtlities
    {
        public static IEnumerable<KeyValuePair<string, object>> AddAttribute(params (string key, object? value)[] attributes)
        {
            var eventAttributes = new List<KeyValuePair<string, object>>();
            foreach (var attribute in attributes)
            {
                eventAttributes.Add(new KeyValuePair<string, object>(attribute.key, attribute.value ?? "(null)"));
            }
            return eventAttributes;
        }
    }
}
