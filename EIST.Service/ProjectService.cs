using EIST.Entities;
using EIST.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Service
{
    public class ProjectService
    {
        private EISTDbContext _context;
        private ProjectUnitOfWork _companyProjectUnitOfWork;
        private CustomerUserProjectUnitOfWork _customerUserProjectUnitOfWork;
        public ProjectService()
        {
            _context = new EISTDbContext();
            _companyProjectUnitOfWork = new ProjectUnitOfWork(_context);
            _customerUserProjectUnitOfWork = new CustomerUserProjectUnitOfWork(_context);
        }

        public int GetProjectCount()
        {
            return _companyProjectUnitOfWork.CompanyProjectRepository.GetCount();
        }

        public IEnumerable<Project> GetAllCompanyProjects()
        {
            var Company = _companyProjectUnitOfWork.CompanyProjectRepository.GetAll();
            return Company.OrderBy(x => x.Name);
        }
        public IEnumerable<Project> GetAllProjectByCompanyId(int companyId)
        {
            return _companyProjectUnitOfWork.CompanyProjectRepository.GetAllProjectByCompanyId(companyId);
        }
        public Project GetCompanyProjectById(int id)
        {
            return _companyProjectUnitOfWork.CompanyProjectRepository.GetById(id);
        }

        public int AddCompanyProject(Project companyProject )
        {
            var newCompanyProject = new Project
            {
                Name = companyProject.Name,
                CompanyId = companyProject.CompanyId,                
                Description = companyProject.Description,
                PmId = companyProject.PmId,
                SuperVisorId = companyProject.SuperVisorId,
               

                CreatedBy = companyProject.CreatedBy,
                CreatedAt = companyProject.CreatedAt,
                IsDeleted = companyProject.IsDeleted,
            };

            _companyProjectUnitOfWork.CompanyProjectRepository.Add(newCompanyProject);
            _companyProjectUnitOfWork.Save();
            return newCompanyProject.Id;
        }
        public void EditCompanyProject(Project companyProject)
        {
            var companyProjectEntry = GetCompanyProjectById(companyProject.Id);

            companyProjectEntry.Name = companyProject.Name;
            companyProjectEntry.Description = companyProject.Description;
            companyProjectEntry.CompanyId = companyProject.CompanyId;
            companyProjectEntry.PmId = companyProject.PmId;
            companyProjectEntry.SuperVisorId = companyProject.SuperVisorId;

            companyProjectEntry.UpdatedAt = companyProject.UpdatedAt;
            companyProjectEntry.UpdatedBy = companyProject.UpdatedBy;
            _companyProjectUnitOfWork.CompanyProjectRepository.Update(companyProjectEntry);
            _companyProjectUnitOfWork.Save();

           
            foreach (var customerUser in companyProjectEntry.CustomerUserProjectCollections.ToList())
            {
                _customerUserProjectUnitOfWork.CustomerUserProjectRepository.DeleteFromDb(customerUser.Id);
                _customerUserProjectUnitOfWork.Save();
            }
            foreach (var customerUser in companyProject.CustomerUserProjectCollections.ToList())
            {
                customerUser.ProjectId = companyProjectEntry.Id;
                _customerUserProjectUnitOfWork.CustomerUserProjectRepository.Add(customerUser);
                _customerUserProjectUnitOfWork.Save();
            }

        }

        public void DeleteCompanyProject(int id, string currUserId)
        {
            _companyProjectUnitOfWork.CompanyProjectRepository.Disable(id);
            _companyProjectUnitOfWork.Save(currUserId);
        }
        public void AddCustomerUserProject(CustomerUserProject customerUserProject)
        {
            var newCustomerUserProject = new CustomerUserProject()
            {              
                UserId= customerUserProject.UserId,
                ProjectId=customerUserProject.ProjectId
            };         
            _customerUserProjectUnitOfWork.CustomerUserProjectRepository.Add(newCustomerUserProject);
            _customerUserProjectUnitOfWork.Save();
        }
        public bool IsCompanyProjectNameExist(string Name, string InitialName)
        {
            return _companyProjectUnitOfWork.CompanyProjectRepository.IsCompanyProjectNameExist(Name, InitialName);
        }

        public void Dispose()
        {
            _companyProjectUnitOfWork.Dispose();
        }

       
    }
}
