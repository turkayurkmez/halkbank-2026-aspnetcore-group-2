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

## SOLID Bağlantısı (Bugünün Commit’lerine Göre)

Bugünkü commit akışında öne çıkan başlıklar:

- `Introduce service and repository abstractions`
- `Dependency Injection lifetime POC in asp.net core`
- `Add SOLID order processing and notifications`

Bu değişikliklerde özellikle aşağıdaki ilkeler vurgulanmıştır:

- **SRP (Single Responsibility Principle)**
  - `ProductsController` ve `OrdersController` HTTP katmanına odaklanır.
  - İş akışı `ProductService` ve `OrderService` sınıflarında yürütülür.
  - Bildirim gönderimi `NotificationService` içinde toplanır.
  - Veri erişimi `InMemoryStore`/repository tarafında izole edilir.

- **OCP (Open/Closed Principle)**
  - `IRepository<T>` ve `IProductRepository` ile veri erişimi soyutlandı.
  - `INotificationChannel` sayesinde yeni kanal (ör. push, webhook) eklemek, mevcut iş akışını bozmadan mümkündür.
  - `Program.cs` içinde çoklu kanal kaydı (`Email`, `SMS`, `WhatsApp`) ile genişletilebilirlik gösterilmiştir.

- **DI (Dependency Injection)**
  - Servis kayıtları `Program.cs` içinde yaşam süresiyle birlikte tanımlandı (`AddSingleton`, `AddScoped`).
  - `IProductService` ve `IOrderService` arayüzleri üzerinden somut sınıflar enjekte edilir.
  - `NotificationService`, `IEnumerable<INotificationChannel>` alarak tüm kayıtlı kanalları otomatik tüketir.

- **ISP (Interface Segregation Principle)**
  - Tek ve şişkin bir servis arayüzü yerine amaca özel arayüzler kullanıldı (`IProductService`, `IOrderService`).
  - Bildirim tarafında da dar ve net bir kontrat tanımlandı (`INotificationChannel`).
  - Bu sayede tüketiciler yalnızca ihtiyaç duydukları metotlara bağımlı kalır.

## Öğrenme Çıktıları

Bu demo sonunda:

- Katılımcılar, bir ASP.NET Core uygulamasında pipeline sırasının önemini açıklayabilir.
- Katılımcılar, özel middleware yazarak istek/yanıt akışına müdahale edebilir.
- Katılımcılar, işlem süresini ölçerek response header üzerinden istemciye iletebilir.
- Katılımcılar, hem `/ping` gibi basit endpoint hem de controller tabanlı endpoint yapısını aynı projede konumlandırabilir.
- Katılımcılar, OpenAPI yapılandırmasını development ortamına göre koşullu olarak etkinleştirebilir.

## Not

Bu README, eğitim sırasında yapılan bugünkü demo kapsamında öğrenilen kavramları ve hedeflenen kazanımları özetlemek amacıyla hazırlanmıştır.
