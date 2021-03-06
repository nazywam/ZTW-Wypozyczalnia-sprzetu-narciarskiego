﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiRent.Extensions;

namespace SkiRent.Services
{
	public interface IService<T>
	{
		List<T> GetAll();
		T Get(int id);
		ServiceResult Update(T item);
		ServiceResult Delete(T item);
		ServiceResult Add(T item);
	}
}
