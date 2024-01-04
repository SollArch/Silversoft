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
using Core.Utilities.Mailing;
using Core.Utilities.Security.Jwt;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            
            builder.RegisterType<AdminPasswordManager>().As<IAdminPasswordService>();
            builder.RegisterType<EfAdminPasswordDal>().As<IAdminPasswordDal>();
            
            builder.RegisterType<AuthManager>().As<IAuthService>();
            
            builder.RegisterType<BlogActivateManager>().As<IBlogActivateService>();
            
            builder.RegisterType<BlogImageManager>().As<IBlogImageService>();
            builder.RegisterType<EfBlogImageDal>().As<IBlogImageDal>();
            
            builder.RegisterType<BlogManager>().As<IBlogService>();
            builder.RegisterType<EfBlogDal>().As<IBlogDal>();

            builder.RegisterType<CloudinaryConnectionManager>().As<ICloudinaryConnectionService>();
            builder.RegisterType<EfCloudinaryConnectionDal>().As<ICloudinaryConnectionDal>();
            
            builder.RegisterType<CloudinaryManager>().As<ICloudinaryService>();

            builder.RegisterType<CtfManager>().As<ICtfService>();
            builder.RegisterType<EfCtfDal>().As<ICtfDal>();
            
            builder.RegisterType<CtfSolveManager>().As<ICtfSolveService>();
            builder.RegisterType<EfCtfSolveDal>().As<ICtfSolveDal>();
            
            builder.RegisterType<UserPointManager>().As<IUserPointService>();
            builder.RegisterType<EfUserPointDal>().As<IUserPointDal>();
            
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            
            builder.RegisterType<LikeManager>().As<ILikeService>();
            builder.RegisterType<EfLikeDal>().As<ILikeDal>();
            
            builder.RegisterType<MailKitMailService>().As<IMailService>();
            
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();
            
            builder.RegisterType<OtpManager>().As<IOtpService>();
            builder.RegisterType<EfOtpDal>().As<IOtpDal>();
            
            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();
            
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            
            
            builder.RegisterType<AdminPasswordRules>().As<IAdminPasswordRules>();
            
            builder.RegisterType<BlogImageRules>().As<IBlogImageRules>();
            
            builder.RegisterType<BlogRules>().As<IBlogRules>();
            
            builder.RegisterType<CloudinaryConnectionRules>().As<ICloudinaryConnectionRules>();
            
            builder.RegisterType<CloudinaryRules>().As<ICloudinaryRules>();
            
            builder.RegisterType<CtfRules>().As<ICtfRules>();
            
            builder.RegisterType<LikeRules>().As<ILikeRules>();
            
            builder.RegisterType<OperationClaimRules>().As<IOperationClaimRules>();
            
            builder.RegisterType<OtpRules>().As<IOtpRules>();
            
            builder.RegisterType<UserRules>().As<IUserRules>();
            

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}