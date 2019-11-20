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
    public class ProjectModel : Project
    {
        private ProjectService _companyProjectService;
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

        public ProjectModel()
        {
            _companyProjectService = new ProjectService();
            _companyService = new CompanyService();
            companyList = _companyService.GetAllCompanies();
        }

        public ProjectModel(int id) : this()
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
        public IEnumerable<Project> GetAllCompanyProjects()
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