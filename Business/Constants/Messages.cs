namespace Business.Constants
{
    public class Messages
    {
        // User
        public const string UserDeleted = "Sana veda etmek üzücü. Eğer yeniden aramıza katılmak istersen hesabını senin için burada tutacağız. Bizimle iletişime geçmen yeterli.";
        public const string UserUpdated = "Kullanıcı güncellendi.";
        public const string UserWasBlocked = "Hesabın askıya alınmış görünüyor. Bir hata olduğunu düşünüyorsan lütfen bizimle iletişime geç.";
        public const string PasswordSendToYourMail = "Giriş şifreniz mail adresinize gönderilmiştir. Lütfen kontrol ediniz.";
        
        // User Rules
        public const string ThisEmailAlreadyExists = "Bu e-posta adresi zaten mevcut. Eğer sana aitse giriş yapabilirsin.";
        public const string ThisUserNameAlreadyExists = "Bu kullanıcı adı zaten mevcut. Lütfen başka bir tane almayı dene.";
        public const string ThisStudentNumberAlreadyExists = "Bu öğrenci numarası zaten mevcut. Lütfen kendine ait bir öğrenci numarası gir.";
        
        
        // Operation Claim
        public const string OperationClaimDeleted = "Yetki silindi.";
        public const string OperationClaimUpdated = "Yetki güncellendi.";
        public const string OperationClaimAdded = "Yetki eklendi.";
        public const string ThisOperationClaimNameAlreadyExists = "Bu yetki adı zaten mevcut.";
        
        
        // User Operation Claim
        public const string UserOperationClaimAdded = "Kullanıcı yetkisi eklendi.";
        public const string UserOperationClaimDeleted = "Kullanıcı yetkisi silindi.";
        public const string UserOperationClaimUpdated = "Kullanıcı yetkisi güncellendi.";
        
        
        // Auth
        public const string PasswordError = "Şifren hatalı, lütfen farklı bir şifre ile tekrar dene.";
        public const string SuccessfulLogin = "Başarıyla giriş yaptın. Hoş geldin.";
        public const string UserNotFound = "Girdiğin bilgiler ile eşleşen bir kullanıcı bulunamadık.";
        public const string AuthorizationDenied = "Boyundan büyük işlere kalkışmış gibi görünüyorsun. Bu işlem için yetkin yok.";
        public const string UserRegistered = "Kaydın oluşturuldu. Aramıza hoş geldin.";
        public const string PasswordsSame = "Yeni şifren eskisi ile aynı olamaz.";
        public const string PasswordChanged = "Şifren değiştirildi.";
        
        // Otp
        public const string OtpNotFound = "Sisteme kayıtlı tek kullanımlık bir şifre bulunamadı. Lütfen tekrar göndermeyi dene.";
        public const string OtpExpired = "Bu kodun süresi dolmuş görünüyor. Lütfen tekrar dene.";
        public const string OtpNotMatch = "Bu kod doğru görünmüyor. Lütfen tekrar dene.";
        public const string OtpSended = "Doğrulama kodun mail adresine gönderildi. Lütfen kontrol et.";
        public const string UserHasOtp = "Sana daha önce bir kod göndermişiz gibi duruyor. Lütfen mail adresini kontrol et.";
        
        //Validaiton Messages
            //OpertaionClaim
        public const string OperationClaimNameMinimumLength = "Yetki adı en az 3 karakter olmalıdır.";
        
    }
}