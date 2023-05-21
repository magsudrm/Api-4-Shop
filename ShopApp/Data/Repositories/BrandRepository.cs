using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
	public class BrandRepository :Repository<Brand>, IBrandRepository
	{
		public BrandRepository(ShopDbContext context):base(context)
		{
		}
	}
}
