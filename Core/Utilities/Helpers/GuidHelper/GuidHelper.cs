using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Helpers.GuidHelper
{
    public static class GuidHelper
    {
        public static Guid CreateGuid()
        {
            return Guid.NewGuid();
        }
    }
}
