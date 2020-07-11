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
    }
}
