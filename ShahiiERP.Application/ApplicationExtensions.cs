using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using ShahiiERP.Application.Mappings;

namespace ShahiiERP.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(cfg =>
        {
            Assembly.GetExecutingAssembly();
            cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzk5ODg0ODAwIiwiaWF0IjoiMTc2ODM4NTIxNSIsImFjY291bnRfaWQiOiIwMTliYmJmODI1NWM3NjI5YjJlYTU5NGQ5YjUyNTIxMyIsImN1c3RvbWVyX2lkIjoiY3RtXzAxa2V4emh5cTM5dzg1MTd4Zzg4MGpndHlzIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.r5QlKDUfuYkv-CAL0F84nqJvTGDAg0EyI1gAFNmUwo34nGIznYHqJXJ8FfkKAckzbzxOzvziIu1KKdEa3MioopoN0d4rlo0VaUq1aarcNpzEWtiQCJC_5RkwRKCkRC3OIW5LTWvfc1I_FymfomRkOgEiSYiA1ABb7uFAapE1V5wfOw52MS1fOBaGmVw7VAtzPkG0AFwpqh1YiEw6fGtEE9EUdV5ba2Hjb-joLmshvMjOOoqpgyKAOMyqjm9iGzQ8Oq8rux6Gvh99vcDomfgrb6sKKRG83oYyJosoPcQ5uGTCD_wT0wA23zMaFYrtNzLwXgI2BxAtbY181Pyy3rjxdA";
        });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
       
        return services;
    }
}
