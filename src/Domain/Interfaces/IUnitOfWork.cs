using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Domain.Services
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository<Product> Products { get; }

        // TODO: add reps

		void Save();
	}
}
