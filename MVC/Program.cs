using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ninject.Web.Common;
using Service.Interfaces;
using Service.Mapper;
using Project.Service.Services;
using Ninject.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


builder.Host.UseServiceProviderFactory(new NinjectServiceProviderFactory(kernel =>
{
    kernel.Bind<DbContextOptions<VehicleFactoryContext>>()
        .ToMethod(_ =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<VehicleFactoryContext>();
            optionsBuilder.UseSqlServer(
                builder.Configuration.GetConnectionString("AppConnStr"));
            return optionsBuilder.Options;
        })
        .InSingletonScope();

    kernel.Bind<VehicleFactoryContext>().ToSelf().InRequestScope();

    kernel.Bind<IVehicleService>().To<VehicleService>().InTransientScope();

    kernel.Bind<ILoggerFactory>().ToConstant(builder.Logging.Services.BuildServiceProvider().GetRequiredService<ILoggerFactory>());

    kernel.Bind<IMapper>().ToMethod(ctx =>
    {
        var loggerFactory = ctx.Kernel.GetService(typeof(ILoggerFactory)) as ILoggerFactory;

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<VehicleFactoryAutoMapperProfile>();
        }, loggerFactory);

        config.AssertConfigurationIsValid();
        return config.CreateMapper();
    }).InSingletonScope();
}));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Make}/{action=Index}/{id?}");

app.Run();