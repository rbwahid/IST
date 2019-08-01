using IST.Entities;
using IST.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IST.Web.Models
{
    [NotMapped]
    public class CompanyProjectModel : CompanyProject
    {
        private CompanyProjectService _companyProjectService;
        public CompanyService _companyService;
        public IEnumerable<Company> companyList { get; set; }

        [Required]
        [Remote("IsCompanyProjectNameExist", "CompanyProject", AdditionalFields = "InitialName",
            ErrorMessage = "Project already Exist")]
        [Display(Name = "Project Name")]
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        public CompanyProjectModel()
        {
            _companyProjectService = new CompanyProjectService();
            _companyService = new CompanyService();
            companyList = _companyService.GetAllCompanies();
        }

        public CompanyProjectModel(int id) : this()
        {
                var companyProjectEntry = _companyProjectService.GetCompanyProjectById(id);
            if(companyProjectEntry != null)
            {
                Id = companyProjectEntry.Id;
                Name = companyProjectEntry.Name;
                Description = companyProjectEntry.Description;
                CompanyId = companyProjectEntry.CompanyId;
                Company = companyProjectEntry.Company;
                

                CreatedAt = companyProjectEntry.CreatedAt;
                CreatedBy = companyProjectEntry.CreatedBy;
                CreatedByUser = companyProjectEntry.CreatedByUser;
                UpdatedAt = companyProjectEntry.UpdatedAt;
                UpdatedBy = companyProjectEntry.UpdatedBy;
                UpdatedByUser = companyProjectEntry.UpdatedByUser;
                TicketCollections = companyProjectEntry.TicketCollections;
            } 

        }
        public IEnumerable<CompanyProject> GetAllCompanyProjects()
        {
            return _companyProjectService.GetAllCompanyProjects();
        }
        
        public void AddCompanyProject()
        {
            
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            _companyProjectService.AddCompanyProject(this);
        }
        public void EditCompanyProject()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            _companyProjectService.EditCompanyProject(this);
        }
        public void DeleteCompanyProject(int id)
        {
            _companyProjectService.DeleteCompanyProject(id, AuthenticatedUser.GetUserFromIdentity().UserId.ToString());
        }

        public bool IsCompanyProjectNameExist(string Name, string InitialName)
        {
            return _companyProjectService.IsCompanyProjectNameExist(Name, InitialName);
        }

        public void Dispose()
        {
            _companyService.Dispose();
        }
    }
}