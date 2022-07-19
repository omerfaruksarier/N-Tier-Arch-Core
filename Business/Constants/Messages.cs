using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";


        public static string ProducNameInvalid = "Ürün ismi geçersiz";

        public static string ProductCountOfCategoryError = "Bu kategoriye ürün ekleyemezsiniz";

        public static string ProductNameAlreadyExist = "Bu isimde zaten bir ürün var";

        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor";

        public static  string AuthorizationDenied = "Yetkiniz yok";

        public static string UserAvailable = "Bu kullanıcı adı kullanılıyor";

        public static string CreateToken = "Token oluşturuldu";

        public static string SuccessLogin = "Giriş başarılı";

        public static string SuccessRegister = "Kayıt başarılı";

        public static string ErrorPassword = "Parola hatalı";

        public static string UserNotFound = "Kullanıcı bulunamadı";

    }
}
