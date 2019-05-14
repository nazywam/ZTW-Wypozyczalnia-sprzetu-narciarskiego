using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using SkiRent.Extensions;

namespace SkiRent.Services
{
	public abstract class BaseService<T> where T : DbContext
	{
		protected T m_Context;

		public BaseService(T context)
		{
			m_Context = context;
		}

		protected ServiceResult SaveChanges()
		{
			try
			{
				m_Context.SaveChanges();
				return new ServiceResult(true);
			}
			catch (DbUpdateException ex) 
			when (ex.InnerException?.InnerException is SqlException sqlEx)
			{
				bool status = false;
				string message = "Nieznany błąd.";
				int errNumber = sqlEx.Number;
				if (errNumber == 1451)
					message = "Nie można usunąć obiektu, ponieważ istnieją powiązane z nim obiekty.";
				return  new ServiceResult(status, message);
			}
		}
	}
}