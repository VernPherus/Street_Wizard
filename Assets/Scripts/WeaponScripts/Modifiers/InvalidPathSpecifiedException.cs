using System;

namespace WeaponsScripts.Modifiers
{
    public class InvalidPathSpecifiedException : Exception
    {
        public InvalidPathSpecifiedException(string AttributeName) : base($"{AttributeName} does not exist at the provided path!") { }
    }
}
