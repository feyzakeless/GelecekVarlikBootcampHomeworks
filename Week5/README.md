# Pharmacy App

1. Web katman� eklendi.
2. Redis kurulumu sa�land� ve �nceki haftalardaki memCacheler DistributedCache ile de�i�tirildi.
3. Hangfire kuruldu.
4. Web katman�nda aray�z tasar�mlar� yap�ld�.
5. Web katman�nda cache den veri �ekilerek yetki kontrol� sa�land�.
6. Mail g�nderimi i�in service katman�nda method tan�mland� ve hangfire ile �al��t�r�ld�.

## Aray�zdan �rnekler

### Login Sayfas�

<div align="center"><img src="Pharmacy/Pharmacy.WEB/wwwroot/img/login.PNG"></div>

### SignUp Sayfas�

<div align="center"><img src="Pharmacy/Pharmacy.WEB/wwwroot/img/signup.PNG"></div>

### �la� Listeleme Sayfas�

<div align="center"><img src="Pharmacy/Pharmacy.WEB/wwwroot/img/medicineList.PNG"></div>

### �la� Ekleme Sayfas�

* Giren kullan�c� eczac� ise ila� ekleme yapabilir.

<div align="center"><img src="Pharmacy/Pharmacy.WEB/wwwroot/img/insertMedicine.PNG"></div>

### �la� G�ncelleme Sayfas�

* Giren kullan�c� eczac� ise ila� g�ncelleme yapabilir.

<div align="center"><img src="Pharmacy/Pharmacy.WEB/wwwroot/img/updateMedicine.PNG"></div>

### Liste Sayfas�n�n Yetkisiz Eleman Giri� Yapt���nda G�r�n�m�

* Giren kullan�c� eczac� de�ilse, g�ncelleme ve silme butonlar� kald�r�ld�. 
* Giren kullan�c� doktor ise, kullan�c�lar� g�r�nt�leyebilece�i alan eklendi.

<div align="center"><img src="Pharmacy/Pharmacy.WEB/wwwroot/img/unauthMedicineList.PNG"></div>

### Kullan�c� Listeleme Sayfas�

<div align="center"><img src="Pharmacy/Pharmacy.WEB/wwwroot/img/userList.PNG"></div>

### Kullan�c�n�n Sistemden ��k�� Yapabilece�i Alan

<div align="center"><img src="Pharmacy/Pharmacy.WEB/wwwroot/img/logOut.PNG"></div>
