namespace Business.Constants
{
    public class Messages
    {
        // User
        public const string UserDeleted = "Kullanıcı silindi.";
        public const string UserUpdated = "Kullanıcı güncellendi.";
        
        
        // User Rules
        public const string ThisEmailAlreadyExists = "Bu e-posta adresi zaten mevcut.";
        public const string ThisUserNameAlreadyExists = "Bu kullanıcı adı zaten mevcut.";
        public const string ThisStudentNumberAlreadyExists = "Bu öğrenci numarası zaten mevcut.";
        

        // Password Validation
        public const string PasswordMustBeAtLeast8Characters = "En az 8 karakter olmalıdır.";
        public const string PasswordsDoNotMatch = "Şifreler eşleşmiyor.";
        
        
        // Auth
        public const string PasswordError = "Şifre hatalı.";
        public const string SuccessfulLogin = "Giriş başarılı.";
        public const string UserNotFound = "Kullanıcı bulunamadı.";
        public const string AuthorizationDenied = "Yetkiniz yok.";
        public const string UserRegistered = "Kullanıcı kayıt edildi.";
        public const string AccessTokenCreated = "Access token oluşturuldu.";
    }
}