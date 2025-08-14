using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ninject;
using Ninject.Web.Common;
using Service.DataAccess;
using Service.Interfaces;
using Service.Mapper;
using Project.Service.Services;
using Ninject.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VehicleFactoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile<VehicleFactoryAutoMapperProfile>();
});


builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IVehicleService, VehicleService>();


IKernel kernel = new StandardKernel();
kernel.Bind<VehicleFactoryContext>().ToSelf().InRequestScope();
kernel.Bind<IVehicleService>().To<VehicleService>().InTransientScope();
kernel.Bind<IMapper>().ToMethod(ctx =>
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<VehicleProfile>();
    });
    config.AssertConfigurationIsValid();
    return config.CreateMapper();
}).InSingletonScope();


builder.Host.UseServiceProviderFactory(
    new NinjectServiceProviderFactory(kernel),
    k => { }
);

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