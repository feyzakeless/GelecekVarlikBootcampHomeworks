# Pharmacy App

1. Web katmaný eklendi.
2. Redis kurulumu saðlandý ve önceki haftalardaki memCacheler DistributedCache ile deðiþtirildi.
3. Hangfire kuruldu.
4. Web katmanýnda arayüz tasarýmlarý yapýldý.
5. Web katmanýnda cache den veri çekilerek yetki kontrolü saðlandý.
6. Mail gönderimi için service katmanýnda method tanýmlandý ve hangfire ile çalýþtýrýldý.

## Arayüzdan Örnekler

### Login Sayfasý

<div align="center"><img src="Pharmacy/Pharmacy.WEB/wwwroot/img/login.PNG"></div>

### SignUp Sayfasý

<div align="center"><img src="Pharmacy/Pharmacy.WEB/wwwroot/img/signup.PNG"></div>

### Ýlaç Listeleme Sayfasý

<div align="center"><img src="Pharmacy/Pharmacy.WEB/wwwroot/img/medicineList.PNG"></div>

### Ýlaç Ekleme Sayfasý

* Giren kullanýcý eczacý ise ilaç ekleme yapabilir.

<div align="center"><img src="Pharmacy/Pharmacy.WEB/wwwroot/img/insertMedicine.PNG"></div>

### Ýlaç Güncelleme Sayfasý

* Giren kullanýcý eczacý ise ilaç güncelleme yapabilir.

<div align="center"><img src="Pharmacy/Pharmacy.WEB/wwwroot/img/updateMedicine.PNG"></div>

### Liste Sayfasýnýn Yetkisiz Eleman Giriþ Yaptýðýnda Görünümü

* Giren kullanýcý eczacý deðilse, güncelleme ve silme butonlarý kaldýrýldý. 
* Giren kullanýcý doktor ise, kullanýcýlarý görüntüleyebileceði alan eklendi.

<div align="center"><img src="Pharmacy/Pharmacy.WEB/wwwroot/img/unauthMedicineList.PNG"></div>

### Kullanýcý Listeleme Sayfasý

<div align="center"><img src="Pharmacy/Pharmacy.WEB/wwwroot/img/userList.PNG"></div>

### Kullanýcýnýn Sistemden Çýkýþ Yapabileceði Alan

<div align="center"><img src="Pharmacy/Pharmacy.WEB/wwwroot/img/logOut.PNG"></div>
