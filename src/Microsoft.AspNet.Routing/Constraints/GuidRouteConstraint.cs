// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNet.Http;

namespace Microsoft.AspNet.Routing.Constraints
{
    /// <summary>
    /// Constrains a route parameter to represent only <see cref="Guid"/> values.
    /// Matches values specified in any of the five formats "N", "D", "B", "P", or "X",
    /// supported by Guid.ToString(string) and Guid.ToString(String, IFormatProvider) methods.
    /// </summary>
    public class GuidRouteConstraint : IRouteConstraint
    {
        /// <inheritdoc />
        public bool Match(
            HttpContext httpContext,
            IRouter route,
            string routeKey,
            IDictionary<string, object> values,
            RouteDirection routeDirection)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            if (route == null)
            {
                throw new ArgumentNullException(nameof(route));
            }

            if (routeKey == null)
            {
                throw new ArgumentNullException(nameof(routeKey));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            object value;
            if (values.TryGetValue(routeKey, out value) && value != null)
            {
                if (value is Guid)
                {
                    return true;
                }

                Guid result;
                var valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
                return Guid.TryParse(valueString, out result);
            }

            return false;
        }
    }
}