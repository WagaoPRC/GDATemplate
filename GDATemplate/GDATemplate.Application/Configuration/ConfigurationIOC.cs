using Autofac;
using AutoMapper;
using GDATemplate.Application.Interfaces;
using GDATemplate.Application.Mapping;
using GDATemplate.Application.Services;
using GDATemplate.Application.Services.Entities;
using GDATemplate.Data;
using GDATemplate.Data.Entities;
using GDATemplate.Data.Interfaces;

namespace GDATemplate.Application.Configuration
{
    public class ConfigurationIOC
    {
        public static void Load(ContainerBuilder builder)
        {
            #region Registra IOC

            #region IOC Services

            builder.RegisterType<DemoService>();
            builder.RegisterType<GenericService>().As<IGenericService>();

            #endregion

            #region IOC Repositorys SQL

            builder.RegisterType<DemoRepository>().As<IDemoRepository>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            #endregion

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperSetup());
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper())
            .As<IMapper>()
            .InstancePerLifetimeScope();

            #endregion


        }
    }
}
