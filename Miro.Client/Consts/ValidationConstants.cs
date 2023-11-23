namespace Miro.Client.Consts
{
    public static class ValidationConstants
    {
        public const string UsernameRequired = "Username is required.";
        public const string PasswordRequired = "Password is required.";
        public const string EmailRequired = "Email is required.";
        public const string InvalidEmailFormat = "Invalid email format.";

        public const int MinUsernameLength = 3;
        public const int MaxUsernameLength = 20;
        public const int MinPasswordLength = 6;
        public const int MaxPasswordLength = 1500;

        public const string PasswordPattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{6,}$";
        public const string EmailPattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
    }
}
