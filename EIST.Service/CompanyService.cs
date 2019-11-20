using EIST.Entities;
using EIST.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Service
{
    public class CompanyService
    {
        private EISTDbContext _context;
        private CompanyUnitOfWork _companyUnitOfWork;

        public CompanyService()
        {
            _context = new EISTDbContext();
            _companyUnitOfWork = new CompanyUnitOfWork(_context);
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            var Company = _companyUnitOfWork.CompanyRepository.GetAll();
            return Company.OrderBy(x => x.Name);
        }
        public Company GetCompanies()
        {
            return _companyUnitOfWork.CompanyRepository.GetAll().FirstOrDefault();
        }
        public Company GetCompanyById(int id)
        {
            return _companyUnitOfWork.CompanyRepository.GetById(id);
        }

        public void AddCompany(Company company)
        {
            var newCompany = new Company
            {
                Name = company.Name,
                Address = company.Address,
                Phone = company.Phone,
                Email = company.Email,
                MobileNo = company.MobileNo,

                CreatedBy = company.CreatedBy
            };

            _companyUnitOfWork.CompanyRepository.Add(newCompany);
            _companyUnitOfWork.Save();
        }
        public void EditCompany(Company company)
        {
            var companyEntry = GetCompanyById(company.Id);
            companyEntry.Name = company.Name;
            companyEntry.Address = company.Address;
            companyEntry.Phone = company.Phone;
            companyEntry.Email = company.Email;
            companyEntry.MobileNo = company.MobileNo;

            companyEntry.UpdatedAt = company.UpdatedAt;
            companyEntry.UpdatedBy = company.UpdatedBy;
            _companyUnitOfWork.CompanyRepository.Update(companyEntry);
            _companyUnitOfWork.Save();
        }

        public void DeleteCompany(int id, string currUserId)
        {
            _companyUnitOfWork.CompanyRepository.Disable(id);
            _companyUnitOfWork.Save(currUserId);
        }
        public bool IsCompanyNameExist(string Name, string InitialName)
        {
            return _companyUnitOfWork.CompanyRepository.IsCompanyNameExist(Name, InitialName);
        }

        public void Dispose()
        {
            _companyUnitOfWork.Dispose();
        }
    }
}
