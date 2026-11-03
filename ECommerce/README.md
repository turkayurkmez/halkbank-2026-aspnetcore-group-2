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

## Öğrenilen Kavramlar

- ASP.NET Core request pipeline mantığı
- Middleware zinciri ve `await next(context)` davranışı
- `OnStarting` ile response header yönetimi
- Minimal API ve Controller yaklaşımının birlikte kullanımı
- Geliştirme ortamına özel OpenAPI yayınlama

## Öğrenme Çıktıları

Bu demo sonunda:

- Katılımcılar, bir ASP.NET Core uygulamasında pipeline sırasının önemini açıklayabilir.
- Katılımcılar, özel middleware yazarak istek/yanıt akışına müdahale edebilir.
- Katılımcılar, işlem süresini ölçerek response header üzerinden istemciye iletebilir.
- Katılımcılar, hem `/ping` gibi basit endpoint hem de controller tabanlı endpoint yapısını aynı projede konumlandırabilir.
- Katılımcılar, OpenAPI yapılandırmasını development ortamına göre koşullu olarak etkinleştirebilir.

## Not

Bu README, eğitim sırasında yapılan bugünkü demo kapsamında öğrenilen kavramları ve hedeflenen kazanımları özetlemek amacıyla hazırlanmıştır.
