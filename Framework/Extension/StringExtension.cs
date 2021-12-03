using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Framework.Extension
{
    public static class StringExtension
    {
        public static string CombineWith(this string basePath, string relative)
        {
            if (string.IsNullOrWhiteSpace(basePath))
                throw new ArgumentNullException(nameof(basePath));

            if (string.IsNullOrWhiteSpace(relative))
                return basePath;

            basePath = basePath.TrimEnd('/');
            relative = relative.TrimStart('/');

            return $"{basePath}/{relative}";
        }
    }
}
