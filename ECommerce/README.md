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

## Gün Gün Öğrenilen Kavramlar (Tarih ve Konu)

### 2026-11-02

- ASP.NET Core Web API proje iskeleti ve başlangıç yapısı
- Template temizliği (WeatherForecast kaldırımı) ve sade başlangıç
- OpenAPI yapılandırması, uygulama URL ayarı ve `/ping` endpoint’i
- Temel middleware akışının kurulması

### 2026-11-03

- Middleware sınıflarının ayrıştırılması (`RequestLogging`, `RequestTiming`)
- MVC/Controller desteği ve merkezi exception handling middleware
- Ortama göre konfigürasyon (Development/Production appsettings)
- Options Pattern (`ShopOption`) ve `IConfiguration` ile ayar okuma

### 2026-11-04

- E-ticaret domain modellerinin (`Product`, `Category`, `Order`, `OrderItem`) eklenmesi
- In-memory store ile veri katmanı başlangıcı
- Katmanlı ürün API’si: Controller + Service + Repository akışı
- Scalar/OpenAPI dokümantasyon deneyimi
- SOLID kavramlarının mimari dokümantasyona yansıtılması

### 2026-11-05

- Service ve repository soyutlamalarının genişletilmesi (`IProductService`, `IOrderService`, `IRepository<T>`, `IProductRepository`)
- Sipariş ve bildirim süreci için SOLID odaklı yapı
- DI lifetime deneyi (`Singleton`, `Scoped`) ve bağımlılıkların container üzerinden yönetimi
- Çoklu bildirim kanalı yaklaşımı (`Email`, `SMS`, `WhatsApp`) ve `IEnumerable<INotificationChannel>` kullanımı
- LSP uyumlu tasarım demosu (kare/dikdörtgen örneği)

### 2026-11-06

- EF Core ile ürün verilerinin kalıcı hale getirilmesi (`AppDbContext`, `EFProductRepository`)
- Entity modellerinin genişletilmesi (`IEntity`, `Customer`, `Review`) ve ilişkilerin güncellenmesi (`Product`, `Category`, `Order`, `OrderItem`)
- Migration yönetimi: initial migration ve Product-Category ilişki düzeltmesi
- Başlangıç verisi için `DataSeeder` ve bağlantı ayarlarının (`appsettings.json`, `Program.cs`) yapılandırılması
- `ProductService` ve `IProductService` yapısının EF tabanlı repository ile uyumlu hale getirilmesi

## SOLID Bağlantısı (Bugünün Çalışmalarına Göre)

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

- Katılımcılar, ASP.NET Core uygulamasını katmanlı şekilde (Controller -> Service -> Repository/Store) yapılandırabilir.
- Katılımcılar, middleware zincirini doğru sırada kurgulayıp loglama, süre ölçümü ve hata yakalama davranışlarını yönetebilir.
- Katılımcılar, `Options Pattern` ve `IConfiguration` ile ortama bağlı ayarları güvenli biçimde okuyabilir.
- Katılımcılar, `DI` container üzerinden uygun yaşam süresiyle (`Singleton`, `Scoped`) bağımlılık kaydı yapabilir.
- Katılımcılar, birden fazla bildirim kanalını (`Email`, `SMS`, `WhatsApp`) ortak arayüz üzerinden genişletilebilir şekilde çalıştırabilir.
- Katılımcılar, SRP, OCP ve ISP ilkelerini somut sınıf ve arayüz tasarımlarında uygulayabilir.

## Not

Bu README, eğitim sırasında yapılan bugünkü demo kapsamında öğrenilen kavramları ve hedeflenen kazanımları özetlemek amacıyla hazırlanmıştır.
