using EIST.Entities;
using EIST.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EIST.Web.Models
{
    [NotMapped]
    public class CompanyModel : Company
    {
        private CompanyService _companyService;

        [Required]
        [Remote("IsCompanyNameExist", "Company", AdditionalFields = "InitialName",
            ErrorMessage = "Company already Exist")]
        [Display(Name = "Name")]
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        public CompanyModel()
        {
            _companyService = new CompanyService();
        }

        public CompanyModel(int id) : this()
        {
                var companyEntry = _companyService.GetCompanyById(id);
            if(companyEntry != null)
            {
                Id = companyEntry.Id;
                Name = companyEntry.Name;
                Address = companyEntry.Address;
                Phone = companyEntry.Phone;
                Email = companyEntry.Email;
                MobileNo = companyEntry.MobileNo;

                CreatedAt = companyEntry.CreatedAt;
                CreatedBy = companyEntry.CreatedBy;
                CreatedByUser = companyEntry.CreatedByUser;
                UpdatedAt = companyEntry.UpdatedAt;
                UpdatedBy = companyEntry.UpdatedBy;
                UpdatedByUser = companyEntry.UpdatedByUser;
            } 

        }
        public IEnumerable<Company> GetAllCompanies()
        {
            return _companyService.GetAllCompanies();
        }
        public Company GetCompanies()
        {
            return _companyService.GetCompanies();
        }
        public void AddCompany()
        {
            
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            _companyService.AddCompany(this);
        }
        public void EditCompany()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            _companyService.EditCompany(this);
        }
        public void DeleteCompany(int id)
        {
            _companyService.DeleteCompany(id, AuthenticatedUser.GetUserFromIdentity().UserId.ToString());
        }

        public bool IsCompanyNameExist(string Name, string InitialName)
        {
            return _companyService.IsCompanyNameExist(Name, InitialName);
        }

        public void Dispose()
        {
            _companyService.Dispose();
        }
    }
}