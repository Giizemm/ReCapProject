using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarAdded = "Araba Eklendi";
        public static string CarUpdated = "Araba Güncellendi";
        public static string CarDeleted = "Araba Silindi";
        public static string BrandAdded = "Araba Modeli Eklendi";
        public static string BranUpdated = "Araba Modeli Güncellendi";
        public static string BrandDeleted = "Araba Modeli Silindi";
        public static string ColorAdded = "Araba Rengi Eklendi";
        public static string ColorUpdated = "Araba Rengi Güncellendi";
        public static string ColorDeleted = "Araba Rengi Silindi";
        public static string BrandNameInvalid = "Araba Markası Geçersiz.";
        public static string CarMessage = "Bilgileri Kontrol Ediniz.";
        public static string Listed = "Bilgiler Listelendi.";
        public static string Added = "Eklendi";
        public static string Deleted = "Silindi";
        public static string Updated = "Güncellendi";
        public static string FailAddedImageLimit = "Resim ekleme limitiniz dolmuştur.";
        public static string ImagesAdded = "Resim eklendi.";
        public static string CarImageListed = "Araba Resimleri Listelendi.";
        public static string AuthorizationDenied = "Yetkiniz Bulunamadı.";
        public static string UserRegistered = "Kayıt oldu.";
        public static string UserNotFound = "Kullanıcı Bulunamadı.";
        public static string PasswordError = "Şifre Hatası";
        public static string SuccessfulLogin = "Başarılı Giriş";
        public static string UserAlreadyExists = "Kullanıcı mevcut.";
        public static string AccessTokenCreated="Token oluşturuldu.";

        public static string CarNotInStock = "Araba kiralanmak istenen tarihler arasında müsait değil!";

        public static string PaymentSuccessful = "Ödeme tamamlandı.";
    }
}
