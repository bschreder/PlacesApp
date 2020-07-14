namespace Library.Extensions
{
    /// <summary>
    /// Helper extension for Url string manipulation
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Append a url string to an existing url string (manages '/')
        /// </summary>
        /// <param name="root">    </param>
        /// <param name="append"> url string to append to root </param>
        /// <returns> combined url string </returns>
        public static string AppendUrl(this string root, string append)
        {
            string retvalue = root.EndsWith("/")
                                ? append.StartsWith("/")
                                    ? root + append.Substring(1)
                                    : root + append
                                : append.StartsWith("/")
                                    ? root + append
                                    : root + "/" + append;
            return retvalue;
        }

        /// <summary>
        /// Convert string to a nullable boolean
        /// </summary>
        /// <param name="value"> "true" or "false" string </param>
        /// <returns> boolean? - text is "true" returns true else returns false </returns>
        public static bool? ToNullableBool(this string value) =>
            bool.TryParse(value, out bool boolValue) ? boolValue : (bool?)null;

        /// <summary>
        /// Convert string to nullable Int32
        /// </summary>
        /// <param name="value"> value to be converted to interger </param>
        /// <returns> interger value or null </returns>
        public static int? ToNullableInt(this string value) => 
            int.TryParse(value, out int intValue) ? intValue : (int?)null;
    }
}
