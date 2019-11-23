using EIST.Entities;
using EIST.Service;
using EIST.Services;
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
        public UserService _userService;
        public IEnumerable<User> DeveloperList { get; set; }
        public IEnumerable<User> CustomerList { get; set; }
     
        public IEnumerable<Company> companyList { get; set; }
        public List<int> CustomerUserIds { get; set; }
        [Required]
        [Remote("IsCompanyProjectNameExist", "Project", AdditionalFields = "InitialName",
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
            _userService = new UserService();
            companyList = _companyService.GetAllCompanies();
            DeveloperList = _userService.GetAllUserAsDeveloper();
            CustomerList = _userService.GetAllCustomerUser();
           

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
                PmId = companyProjectEntry.PmId;
                ProjectManager = companyProjectEntry.ProjectManager;
                SuperVisorId = companyProjectEntry.SuperVisorId;
                SuperVisor = companyProjectEntry.SuperVisor;
                CustomerUserProjectCollections = companyProjectEntry.CustomerUserProjectCollections;
                CustomerUserIds = companyProjectEntry.CustomerUserProjectCollections.Select(x => x.UserId).ToList();

                CreatedAt = companyProjectEntry.CreatedAt;
                CreatedBy = companyProjectEntry.CreatedBy;
                CreatedByUser = companyProjectEntry.CreatedByUser;
                UpdatedAt = companyProjectEntry.UpdatedAt;
                UpdatedBy = companyProjectEntry.UpdatedBy;
                UpdatedByUser = companyProjectEntry.UpdatedByUser;
                TicketCollections = companyProjectEntry.TicketCollections;
                CustomerUserIds = companyProjectEntry.CustomerUserProjectCollections.Select(x => x.UserId).ToList();            } 

        }
        public IEnumerable<Project> GetAllCompanyProjects()
        {
            return _companyProjectService.GetAllCompanyProjects();
        }
        
        public void AddCompanyProject()
        {
            
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
           var returnId =  _companyProjectService.AddCompanyProject(this);

            foreach( var cuId in CustomerUserIds)
            {
                var newCustomerUser = new CustomerUserProject()
                {
                    ProjectId= returnId,
                    UserId= cuId
                };
                _companyProjectService.AddCustomerUserProject(newCustomerUser);
            }

        }
        public void EditCompanyProject()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            var customerUserIdforEdits = new List<int>();
            foreach(var CUId in CustomerUserIds)
            {
                customerUserIdforEdits.Add(CUId);
            }
            base.CustomerUserProjectCollections = customerUserIdforEdits.Select(x => new CustomerUserProject() { UserId = x }).ToList();
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