using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Business.Rules;
using Business.Rules.Abstract;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Core.Mailing;
using Core.Utilities.Mailing;
using Core.Utilities.Security.Jwt;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();

            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            
            builder.RegisterType<OtpManager>().As<IOtpService>();
            builder.RegisterType<EfOtpDal>().As<IOtpDal>();

            builder.RegisterType<PasswordManager>().As<IPasswordService>();
            builder.RegisterType<EfPasswordDal>().As<IPasswordDal>();
            
            builder.RegisterType<UserRules>().As<IUserRules>();
            builder.RegisterType<OperationClaimRules>().As<IOperationClaimRules>();
            builder.RegisterType<OtpRules>().As<IOtpRules>();
            
            builder.RegisterType<MailKitMailService>().As<IMailService>();
            

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}