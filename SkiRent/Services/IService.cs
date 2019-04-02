using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiRent.Services
{
	public interface IService<T>
	{
		List<T> GetAll();
		T Get(int id);
		void Update(T item);
		void Delete(T item);
		void Add(T item);
	}
}
