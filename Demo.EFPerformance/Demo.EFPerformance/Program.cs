using ECommerce.API.Data;
using ECommerce.API.Services;


using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


const string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=HalkECommerce1Training;Integrated Security=True";
var queryCount = 0;

AppDbContext Create(bool lazyLoading = false)
{
    var options = new DbContextOptionsBuilder<AppDbContext>()
                      .UseSqlServer(connectionString)
                      .LogTo(message =>
                      {
                          queryCount++;
                          Console.WriteLine($"      SQL #{queryCount}: {message.Split("\n").Skip(2).FirstOrDefault()?.Trim()}");
                      }, Microsoft.Extensions.Logging.LogLevel.Information);

    if (lazyLoading)
    {
        options.UseLazyLoadingProxies();
    }
    return new AppDbContext(options.Options);
}

async Task<int> MeasureAsync(string title, Func<Task> action)
{
    queryCount = 0;
    var watch = Stopwatch.StartNew();
    await action();
    watch.Stop();
    Console.WriteLine($"{title} işlemi, {queryCount} SQL, {watch.ElapsedMilliseconds} ms sürdü");
    return queryCount;
}


var nPlusOne = await MeasureAsync("KÖTÜ: N+1 anti patten:", async () =>
{
    await using var db = Create();
    var categories = await db.Categories.Include(c=>c.Products).ToListAsync();

    foreach (var item in categories)
    {
        var products = item.Products.Select(p=>p.Name).ToList();

    }
});

Console.WriteLine(nPlusOne);