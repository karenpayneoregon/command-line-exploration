namespace PaynePromptForUserNamePassword.LanguageExtensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Capitalize first character in the string
        /// </summary>
        /// <param name="input">string to capitalize</param>
        /// <returns>Proper cased string</returns>
        public static string FirstCharToUpper(this string input) => input switch {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input[0].ToString().ToUpper() + input[1..]
            };
    }
}