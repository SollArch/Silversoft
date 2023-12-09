using System;

namespace Core.Utilities.Security.Password
{
    public abstract class PasswordHelper
    {
        public static string GeneratePassword()
        {
            int substringStart = new Random().Next(0, 24);
            return new Guid().ToString("D").Substring(substringStart, 8).ToUpper();
        }
    }
}