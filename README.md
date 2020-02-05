Makale.Api
Bu api üzerinden makale listeleme, arama, ekleme, güncelleme ve silme işlemlerinin REST çağrıları ile yapılabiliyor

add-migration LibraryDB ,update-database
ViewModel nesnelerinin modellediği gerçekci model entityleri ile eşleştirilmesini dinamik bir şekilde sağlayan AutoMapper kütüphanesi tercih edilmiştir.
Linux platform uyumluluğu olduğu için .net core tercih edilmiştir.
Sorular:

Projede kullanıdığınız tasarım desenleri hangileridir? Bu desenleri neden kullandınız?

Proje repository pattern ve Mvc ile örneklendirilmiştir.
Kullandığınız teknoloji ve kütüphaneler hakkında daha önce tecrübeniz oldu mu? Tek tek yazabilir misiniz?

kullanılan teknolojilerde büyük ve kurumsal firmaların hizmet aldığı api yazma tecrübelerim oldu.
Daha geniş vaktiniz olsaydı projeye neler eklemek isterdiniz?

Detaylı bir loglama ve Cache'leme mekanizması üzerinde dururdum.
Eklemek istediğiniz bir yorumunuz var mı?

 
örnek kullanımlar 
=> https://localhost:44371/api/authors/25320c5e-f58a-4b1f-b63a-8ee07a840bdf/articles
=> https://localhost:44371/api/authors
