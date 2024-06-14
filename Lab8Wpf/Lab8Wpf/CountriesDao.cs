using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8Wpf
{
    public class CountriesDao : IDao<Countries>
    {
        private readonly MediaLibraryDataContext _context;

        public CountriesDao(MediaLibraryDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Countries> GetAll()
        {
            return _context.Countries;
        }

        public Countries GetById(int id)
        {
            return _context.Countries.SingleOrDefault(c => c.CountryID == id);
        }

        public void Add(Countries country)
        {
            _context.Countries.InsertOnSubmit(country);
            _context.SubmitChanges();
        }

        public void Update(Countries country)
        {
            var existingCountry = _context.Countries.SingleOrDefault(c => c.CountryID == country.CountryID);
            if (existingCountry != null)
            {
                existingCountry.CountryName = country.CountryName;
                _context.SubmitChanges();
            }
        }

        public void Delete(int id)
        {
            var country = _context.Countries.SingleOrDefault(c => c.CountryID == id);
            if (country != null)
            {
                _context.Countries.DeleteOnSubmit(country);
                _context.SubmitChanges();
            }
        }
    }

}
