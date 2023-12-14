using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
namespace Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        private readonly string _patterns;
        private readonly ICacheManager _cacheManager;

        public CacheRemoveAspect(string patterns)
        {
            _patterns = patterns;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            foreach (var pattern in _patterns.Split(','))
            {
                _cacheManager.RemoveByPattern(pattern.Trim());
            }
            
        }
    }
}
