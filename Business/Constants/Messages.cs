namespace Business.Constants
{
    public class Messages
    {
        // Admin Password
        public const string AdminPassswordAlreadyExists = "Zaten bir admin şifresi var. Lütfen şifreyi değiştir.";
        public const string AdminPasswordAdded = "Admin şifresi eklendi.";


        // User
        public const string UserDeleted =
            "Sana veda etmek üzücü. Eğer yeniden aramıza katılmak istersen hesabını senin için burada tutacağız. Bizimle iletişime geçmen yeterli.";

        public const string UserUpdated = "Kullanıcı güncellendi.";

        public const string UserWasBlocked =
            "Hesabın askıya alınmış görünüyor. Bir hata olduğunu düşünüyorsan lütfen bizimle iletişime geç.";

        public const string PasswordSendToYourMail =
            "Giriş şifreni mail adresine gönderdik. Daha sonraki girşlerinde bu şifreyi kullanabilirsin.";

        public const string PointAdded = "Puan eklendi.";
        
        public const string UserPointNotEnough = "Puanın ipucu almak için yetersiz. Daha fazla puan kazanmalısın.";
        
        // User Rules
        public const string ThisEmailAlreadyExists =
            "Bu e-posta adresi zaten mevcut. Eğer sana aitse giriş yapabilirsin.";

        public const string ThisUserNameAlreadyExists =
            "Bu kullanıcı adı zaten mevcut. Lütfen başka bir tane almayı dene.";

        public const string ThisStudentNumberAlreadyExists =
            "Bu öğrenci numarası zaten mevcut. Lütfen kendine ait bir öğrenci numarası gir.";


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

        public const string AuthorizationDenied =
            "Boyundan büyük işlere kalkışmış gibi görünüyorsun. Bu işlem için yetkin yok.";

        public const string UserRegistered = "Kaydın oluşturuldu. Aramıza hoş geldin.";
        public const string PasswordsSame = "Yeni şifren eskisi ile aynı olamaz.";
        public const string PasswordChanged = "Şifren değiştirildi.";

        // Otp
        public const string OtpNotFound =
            "Sisteme kayıtlı tek kullanımlık bir şifre bulunamadı. Lütfen tekrar göndermeyi dene.";

        public const string OtpExpired = "Bu kodun süresi dolmuş görünüyor. Lütfen tekrar dene.";
        public const string OtpNotMatch = "Bu kod doğru görünmüyor. Lütfen tekrar dene.";
        public const string OtpSended = "Doğrulama kodun mail adresine gönderildi. Lütfen kontrol et.";

        public const string UserHasOtp =
            "Sana daha önce bir kod göndermişiz gibi duruyor. Lütfen mail adresini kontrol et.";

        public const string OtpSendedBefore =
            "Bu mail adresine daha önce bir kod göndermiştik. Lütfen mail adresini kontrol et.";

        // Blog
        public const string BlogDeleted = "Blog silindi.";

        public const string BlogUpdated =
            "Blog yazın başarıyla güncellendi. Onayımızın ardından yeniden yayında olacak.";

        public const string BlogAdded = "Blog yazın onayımızın ardından yayına alınacak. Lütfen beklemede kal.";
        public const string BlogNotFound = "Üzgünüm bu blog yazısını bulamadık.";
        public const string BlogActivated = "Blog yazısı yayına alındı.";
        public const string BlogDeactivated = "Blog yazısı yayından kaldırıldı.";
        public const string BlogAlreadyActive = "Bu blog yazısı zaten yayında.";


        // Blog Image
        public const string BlogImageAdded = "Blog resmi eklendi.";
        public const string BlogImageDeleted = "Blog resmi silindi.";
        public const string BlogImageNotFound = "Üzgünüm bu blog resmini bulamadık.";
        public const string BlogHasImageAlreadyExist = "Bu blog yazısına ait bir resim zaten mevcut.";
        public const string BlogImageNotAddedByTheAuthor = "Bu bloğun sahibi sen değilsin gibi görünüyor.";

        // Like
        public const string LikeNotFound = "Üzgünüm bu beğeniyi bulamadık.";
        public const string UserLikedBefore = "Bu blog yazısını daha önce beğenmişsin.";
        public const string BlogNotActive = "Bu blog yazısı yayında değil.";


        // Cloudinary
        public const string CloudinarySettingsAdded = "Cloudinary ayarları eklendi.";
        public const string CloudinarySettingsUpdated = "Cloudinary ayarları güncellendi.";
        public const string CloudinarySettingsDeleted = "Cloudinary ayarları silindi.";
        public const string CloudinarySettingsAlreadyExists = "Cloudinary bağlantısı zaten mevcut.";
        public const string CloudinarySettingsDoesNotExist = "Cloudinary bağlantısı bulunamadı.";
        public const string ImageDestroyedFromCloud = "Resim bulut üzerinden silindi.";


        // Ctf
        public const string CtfAdded = "CTF sorusu eklendi.";
        public const string CtfUpdated = "CTF sorusu güncellendi";
        public const string CtfClosed = "CTF sorusu kapatıldı.";
        public const string CtfNotFound = "CTF sorusu bulunamadı.";
        public const string WrongAnswer = "Emeklerine saygı duyuyoruz ama cevabın hatalı. Biraz daha düşün.";
        public const string CtfAlreadySolved = "Bu isteği göndermiş olman hoşumuza gitti ancak soruyu zaten çözdün.";
        public const string CtfHintAddUserIsNotCtfOwner =
            "Bu soruyu sen eklememişsin gibi görünüyor. Soruyu ekleyen kişi ipucu ekleyebilir.";
        public const string CtfSolved =
            "Soruya verdiğin cevap doğru. Tebrikler. Puanın hesabına eklendi. Unutma CTF daha fazla kişi tarafından çözüldükçe puanın azalacak.";
        public const string CtfIsNotActive =
            "Bu soru için artık cevap kabul edilmiyor. Başka bir soruyu çözebilir ya da yeni bir soru için beklemede kalabilirsin.";

        public const string CtfSolverIsCtfOwner = "Kendi sorunu çözmeye çalışmak senin gibi bir admine yakışıyo mu";

        // ------------------   Validaiton Messages --------------------
        //OpertaionClaim
        public const string OperationClaimNameMinimumLength = "Yetki adı en az 3 karakter olmalıdır.";

        //Blog
        public const string BlogTitleMinimumLength = "Blog başlığın en az 3 karakter olmalıdır.";
        public const string BlogContentMinimumLength = "Blog içeriğin en az 50 karakter olmalıdır.";
        public const string BlogTitleMaximumLength = "Blog başlığın en fazla 100 karakter olmalıdır.";
        public const string BlogAuthorIdInvalid = "Blog yazarı geçersiz.";
    }
}