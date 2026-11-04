# ECommerce

## Bugün Ele Alınan Konular

- **Minimal API başlangıç yapısı** (`WebApplication.CreateBuilder`, `builder.Build`)
- **Controller desteği eklenmesi** (`AddControllers`, `MapControllers`)
- **OpenAPI entegrasyonu** (`AddOpenApi`, development ortamında `MapOpenApi`)
- **Custom middleware kullanımı**
  - İstek giriş loglaması
  - `next()` öncesi/sonrası akışın gözlemlenmesi
- **Response süresi ölçümü**
  - `Stopwatch` ile geçen süre hesabı
  - `OnStarting` ile `X-Response-Time-MS` header eklenmesi
- **Basit health-check endpoint’i** (`/ping`)
- **HTTP pipeline sıralaması ve middleware yaşam döngüsü**
- **In-memory veri saklama yaklaşımı** (`InMemoryStore`) ve başlangıç seed verisi
- **Service katmanı kullanımı** (`ProductService`) ile iş kurallarının controller dışına taşınması
- **REST endpoint’leri ile ürün işlemleri** (`GET`, `GET by id`, `POST`)
- **`CreatedAtAction` ile doğru 201 Created yanıtı üretme**
- **Options Pattern ve konfigürasyon okuma** (`IOptions<ShopOption>`, `IConfiguration`)
- **Ortam bazlı davranış** (`IWebHostEnvironment` ile development kontrolü)

## Öğrenilen Kavramlar

- ASP.NET Core request pipeline mantığı
- Middleware zinciri ve `await next(context)` davranışı
- `OnStarting` ile response header yönetimi
- Minimal API ve Controller yaklaşımının birlikte kullanımı
- Geliştirme ortamına özel OpenAPI yayınlama
- In-memory store ile kalıcı olmayan veri yönetimi
- Katmanlı yapı: Controller -> Service -> Store akışı
- Options Pattern ile strongly-typed ayar erişimi

## SOLID Bağlantısı (OCP ve SRP)

- **SRP (Single Responsibility Principle)**
  - `ProductsController` yalnızca HTTP istek/yanıt yönetir.
  - Ürünlere ait iş akışı `ProductService` içinde tutulur.
  - Veri saklama sorumluluğu `InMemoryStore` sınıfındadır.
  - Böylece her sınıfın tek bir değişim nedeni olur.

- **OCP (Open/Closed Principle)**
  - `IRepository<T>` soyutlaması sayesinde veri kaynağı genişletilebilir.
  - Bugün `InMemoryStore` kullanılırken, yarın farklı bir repository (ör. veritabanı tabanlı) eklenebilir.
  - Bu genişleme, mevcut controller/service kodunu minimum değişiklikle koruyarak yapılır.
  - Yani yapı, **genişletmeye açık**, mevcut davranışı bozacak değişikliklere karşı **kapalı** olacak şekilde tasarlanır.

## Öğrenme Çıktıları

Bu demo sonunda:

- Katılımcılar, bir ASP.NET Core uygulamasında pipeline sırasının önemini açıklayabilir.
- Katılımcılar, özel middleware yazarak istek/yanıt akışına müdahale edebilir.
- Katılımcılar, işlem süresini ölçerek response header üzerinden istemciye iletebilir.
- Katılımcılar, hem `/ping` gibi basit endpoint hem de controller tabanlı endpoint yapısını aynı projede konumlandırabilir.
- Katılımcılar, OpenAPI yapılandırmasını development ortamına göre koşullu olarak etkinleştirebilir.

## Not

Bu README, eğitim sırasında yapılan bugünkü demo kapsamında öğrenilen kavramları ve hedeflenen kazanımları özetlemek amacıyla hazırlanmıştır.
