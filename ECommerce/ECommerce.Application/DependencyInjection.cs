using ECommerce.Application.Mapping;
using ECommerce.Application.Notifications;
using ECommerce.Application.Requests;
using ECommerce.Application.Services;
using ECommerce.Infrastructure.UnitOfWorkPattern;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddAplication(this IServiceCollection services) {

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<INotificationChannel, EmailChannel>();
            services.AddSingleton<INotificationChannel, SMSChannel>();
            services.AddSingleton<INotificationChannel, WhatsAppChannel>();
            services.AddScoped<NotificationService>();

            services.AddValidatorsFromAssemblyContaining<CreateProductRequest>();

            //Not: aşağıda yazılacaklar, Mapster Config yazdıysanız gereklidir:
            TypeAdapterConfig.GlobalSettings.Default.RequireDestinationMemberSource(true); //bu satır
            TypeAdapterConfig.GlobalSettings.Scan(typeof(ProductMappingConfig).Assembly); //bu satır
            services.AddMapster();
            TypeAdapterConfig.GlobalSettings.Compile(); //bu satır...

            return services;
        }
    }
}
