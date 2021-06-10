using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;
using UpliftUdemy.DataAccess.Data;

namespace Uplift.DataAccess.Data.Repository
{
    public class FrequencyRepository : Repository<Frequency>, IFrequencyRepository
    {
        private readonly ApplicationDbContext _db;
        public FrequencyRepository(ApplicationDbContext db)
            :base(db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetFrequencyListForDropDown()
        {
            return _db.Frequencies.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }
            );
        }

        public void Update(Frequency frequency)
        {
            var dbFrequency = _db.Frequencies.FirstOrDefault(f => f.Id == frequency.Id);
            dbFrequency.Name = frequency.Name;
            dbFrequency.FrequencyCount = frequency.FrequencyCount;

            _db.SaveChanges();
        }
    }
}
