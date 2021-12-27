using Autofac;
using DataImporter.Common.Utilities;

namespace DataImporter.Common
{
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DateTimeUtility>().As<IDateTimeUtility>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmailService>().As<IEmailService>()
                .WithParameter("host", "smtp.gmail.com")
                .WithParameter("port", 465)
                .WithParameter("username", "aspnetdeveloper2021@gmail.com")
                .WithParameter("password", "********")
                .WithParameter("useSSL", true)
                .WithParameter("from", "aspnetdeveloper2021@gmail.com")
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
