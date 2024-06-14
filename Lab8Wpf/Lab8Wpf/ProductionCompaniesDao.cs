using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8Wpf
{
    public class ProductionCompaniesDao : IDao<ProductionCompanies>
    {
        private readonly MediaLibraryDataContext _context;

        public ProductionCompaniesDao(MediaLibraryDataContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductionCompanies> GetAll()
        {
            return _context.ProductionCompanies;
        }
          //linq
        public ProductionCompanies GetById(int id)
        {
            return _context.ProductionCompanies.SingleOrDefault(c => c.CompanyID == id);
        }
        //linq
        public void Add(ProductionCompanies company)
        {
            _context.ProductionCompanies.InsertOnSubmit(company);
            _context.SubmitChanges();
        }

        public void Update(ProductionCompanies company)
        {
            var existingCompany = _context.ProductionCompanies.SingleOrDefault(c => c.CompanyID == company.CompanyID);
            if (existingCompany != null)
            {
                existingCompany.CompanyName = company.CompanyName;
                _context.SubmitChanges();
            }
        }

        public void Delete(int id)
        {
            var company = _context.ProductionCompanies.SingleOrDefault(c => c.CompanyID == id);
            if (company != null)
            {
                _context.ProductionCompanies.DeleteOnSubmit(company);
                _context.SubmitChanges();
            }
        }
    }
}
