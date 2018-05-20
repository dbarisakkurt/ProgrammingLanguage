
# English
This is an interpreter/programming language implementation project. The language has Turkish keywords. Download the interpreter [here](https://drive.google.com/open?id=0B-0eXZXRmKFLckJ0REZqWGIySW8)

Open a text file e.g. hello.txt and write your program. Give the command and you can see the result of your program.

```
ProgrammingLanguage C:\Users\xxxx\hello.txt
```

## Variable Declaration
Örnek:
```
değişken num 5;
değişken text"merhaba";
```
One variable declaration per line.

## Operators
+, -, *, /, %, ve, veya, ==, !=, <, >, <=, >=


## Bool Statements
```
değişken bayrak = doğru;
değişken bayrak = yanlış;
```
doğru is the keyword for true and yanlış is the keyword for false.

## Conditionals
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
Curly brackets are must even if you have only one statement after eğer or değilse like Go language. ve keywords is for && and veya keyword is for ||.

```
eğer (sayı <= 5 ve sayı >= 10)
{
    sayı = sayı + 1;
}
```


## Loops
Örnek:
```
oldukça (sayı < 5)
{
    sayı = sayı + 1;
}
```
Curly brackets are must even if you have only one statement after oldukça

## Function Declaration and Call
Örnek:
```
fonk topla(a, b)
{
  değişken c = a + b;
  dön c;
}
```
Above is the example

## Restrictions
* Only integers are supported. No float or double support.
* No array data type.

# Türkçe

Bu proje Türkçe anahtar sözüklere sahip basit bir yorumlayıcı uygulaması ve programlama dili içerir. [Buradan](https://drive.google.com/open?id=0B-0eXZXRmKFLckJ0REZqWGIySW8) ilgili yorumlayıcıyı indirebilirsiniz. Zip dosyasını indirdiktan sonra bilgisayarınıza açın. Açtığınız dizine gidin.

Bir txt dosyasına -diyelim ki merhaba.txt- programınızı yazın.

```
ProgrammingLanguage C:\Users\xxxx\merhaba.txt
```
komutu ile yorumlayıcıyı çalıştırıp programınızın sonucunu görebilirsiniz.

## Değişken Tanımlama
Örnek:
```
değişken sayı = 5;
değişken metin = "merhaba";
```
diyerek değişken tanımlanabilir. Tek bir satırda sadece 1 değişken tanımlaması yapılmalıdır.

## Operatörler
+, -, *, /, %, ve, veya, ==, !=, <, >, <=, >=

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
* Sadece 5, 124 gibi tamsayılar destekleniyor. 12.3 gibi kesirli sayılar desteklenmemektedir. Dolayısıyla bölme işleminin sonucu da tamsayıdır.
* Dilde şu an için dizi veri tipi yoktur.

