# Türkçe

Bu proje Türkçe anahtar sözüklere sahip basit bir yorumlayıcı uygulaması ve programlama dili içerir.

## Değişken Tanımlama
Örnek:
```
değişken sayı = 5;
değişken metin = "merhaba";
```
diyerek değişken tanımlanabilir. Tek bir satırda sadece 1 değişken tanımlaması yapılmalıdır.

## Bool İfadeler
```
değişken bayrak = doğru;
değişken bayrak = yanlış;
```
doğru ve yanlış anahtar sözcükleri diğer dillerdeki true ve false'a karşılık gelir.

## Koşul İfadesi
Örnek:
```
eğer (sayı <= 5)
{
    sayı = sayı + 1;
}
değilse
{
    sayı = sayı - 1;
}
```
Koşullu ifadeler yukarıdaki gibi olup eğer ve değilse ifadelerinden sonra blok içinde tek bir ifade varsa bile küme parantezleri koymak '{' zorunludur. Değilse kısmı isteğe bağlıdır.

Diğer dillerdeki ve (&&) ve veya (||) operatörleri için ve ve veya anahtar sözcükleri kullanılır.

```
eğer (sayı <= 5 ve sayı >= 10)
{
    sayı = sayı + 1;
}
```


## Döngü
Örnek:
```
oldukça (sayı < 5)
{
    sayı = sayı + 1;
}
```
Döngüler oldukça anahtar sözcüğü ile yapılır ve eğer-değilse ifadelerindeki gibi blok tek bir ifade içerecekse bile küme parantezleri koymak zorunludur.

## Fonksiyon Tanımlama ve Çağırma
Örnek:
```
fonk topla(a, b)
{
  değişken c = a + b;
  dön c;
}
```
Fonksiyon tanımlama ve tanımlanmış fonksiyonu çağırma yukarıdaki örnekteki gibi yapılır.

## Kısıtlar
* Henüz rekürsif (özyinelemeli) fonksiyon çağrısı desteklenmemektedir.
* Sadece 5, 124 gibi tamsayılar destekleniyor. 12.3 gibi kesirli sayılar desteklenmemektedir. Dolayısıyla bölme işleminin sonucu da tamsayıdır.
* Dilde şu an için dizi veri tipi yoktur.
* Dilde şu an için bölümden kalanı veren (diğer dillerdeki % gibi) bir operatör yoktur.

# English
