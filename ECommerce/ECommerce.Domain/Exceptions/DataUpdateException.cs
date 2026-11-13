using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Exceptions
{
    public class DataUpdateException(Exception innerException) : Exception("Veritabanı değişikliği kaydedilemedi",innerException)
    {
    }
}
