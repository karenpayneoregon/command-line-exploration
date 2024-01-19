namespace PaynePromptForUserNamePassword.Classes
{
    /// <summary>
    /// Mocked information that would come from perhaps
    /// a database, encrypted file etc.
    /// </summary>
    public class Secrets
    {
        /// <summary>
        /// User name and password which are their first and last names
        /// keys are case insensitive, in real world we would be case sensitive
        /// </summary>
        /// <returns>case insensitive</returns>
        public static Dictionary<string, string> UsersInformation() =>
            new(StringComparer.InvariantCultureIgnoreCase)
            {
                { "karen", "pass1" },
                { "vincent", "pass2" },
                { "amelia", "pass3" },
                { "garen", "pass4" },
                { "lisa", "pass5" },
                { "james", "pass6" },
                { "yelena", "pass7" },
                { "francis", "pass8" },
                { "dino", "pass9" },
                { "bill", "pass10" },
                { "bick", "pass11" },
                { "fred", "pass11" }
            };
        }
}
