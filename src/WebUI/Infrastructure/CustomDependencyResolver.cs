using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Services;
using Ninject;
using WebUI.Services;


namespace WebUI.Infrastructure
{
	//http://metanit.com/sharp/mvc5/21.2.php
	public class CustomDependencyResolver : IDependencyResolver
	{
		private IKernel kernel;

		public CustomDependencyResolver(IKernel kernelParam)
		{
			kernel = kernelParam;
			AddBindings();
		}

		public object GetService(Type serviceType)
		{
			return kernel.TryGet(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return kernel.GetAll(serviceType);
		}

		private void AddBindings()
		{
            ///////////////////
			kernel.Bind<ICartService>().To<CartService>();
			kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
		}
	}
}
