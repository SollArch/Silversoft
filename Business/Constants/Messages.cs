namespace Business.Constants
{
    public class Messages
    {
        // User
        public const string UserDeleted = "Kullanıcı silindi.";
        public const string UserUpdated = "Kullanıcı güncellendi.";
        public const string UserWasBlocked = "Hesabınız askıya alınmış görünüyor. Bir hata olduğunu düşünüyorsanız lütfen iletişime geçiniz.";
        public const string PasswordSendToYourMail = "Giriş şifreniz mail adresinize gönderilmiştir. Lütfen kontrol ediniz.";
        
        // User Rules
        public const string ThisEmailAlreadyExists = "Bu e-posta adresi zaten mevcut.";
        public const string ThisUserNameAlreadyExists = "Bu kullanıcı adı zaten mevcut.";
        public const string ThisStudentNumberAlreadyExists = "Bu öğrenci numarası zaten mevcut.";
        
        
        // Operation Claim
        public const string OperationClaimDeleted = "Yetki silindi.";
        public const string OperationClaimUpdated = "Yetki güncellendi.";
        public const string OperationClaimAdded = "Yetki eklendi.";
        public const string ThisOperationClaimNameAlreadyExists = "Bu yetki adı zaten mevcut.";
        
        
        // User Operation Claim
        public const string UserOperationClaimAdded = "Kullanıcı yetkisi eklendi.";
        public const string UserOperationClaimDeleted = "Kullanıcı yetkisi silindi.";
        public const string UserOperationClaimUpdated = "Kullanıcı yetkisi güncellendi.";
        

        // Password Validation
        public const string PasswordMustBeAtLeast8Characters = "En az 8 karakter olmalıdır.";
        public const string PasswordsDoNotMatch = "Şifreler eşleşmiyor.";
        
        
        // Auth
        public const string PasswordError = "Şifre hatalı.";
        public const string SuccessfulLogin = "Giriş başarılı.";
        public const string UserNotFound = "Kullanıcı bulunamadı.";
        public const string AuthorizationDenied = "Yetkiniz yok.";
        public const string UserRegistered = "Kullanıcı kayıt edildi.";
        
        // Otp
        public const string OtpNotFound = "Otp bulunamadı.";
        public const string OtpExpired = "Otp süresi geçmiş.";
        public const string OtpNotMatch = "Otp eşleşmiyor.";
        public const string OtpSended = "Otp gönderildi.";
    }
}