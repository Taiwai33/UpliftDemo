using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;
using UpliftUdemy.DataAccess.Data;

namespace Uplift.DataAccess.Data.Repository
{
   public class ServiceRepository: Repository<Service>, IServiceRepository
    {
        private readonly ApplicationDbContext _db;

        public ServiceRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }
        public void Update(Service service)
        {
            var objFromDb = _db.Services.FirstOrDefault(s => s.Id == service.Id);
            objFromDb.Name = service.Name;
            objFromDb.Price = service.Price;
            objFromDb.LongDescription = service.LongDescription;
            objFromDb.ImageUrl = service.ImageUrl;
            objFromDb.FrequencyId = service.FrequencyId;
            objFromDb.CategoryId = service.CategoryId;
                       
            _db.SaveChanges();
        }
    }
}
