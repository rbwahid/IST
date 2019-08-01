using IST.Entities;
using IST.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Service
{
    public class CompanyProjectService
    {
        private ISTDbContext _context;
        private CompanyProjectUnitOfWork _companyProjectUnitOfWork;

        public CompanyProjectService()
        {
            _context = new ISTDbContext();
            _companyProjectUnitOfWork = new CompanyProjectUnitOfWork(_context);
        }

        public IEnumerable<CompanyProject> GetAllCompanyProjects()
        {
            var Company = _companyProjectUnitOfWork.CompanyProjectRepository.GetAll();
            return Company.OrderBy(x => x.Name);
        }
        public IEnumerable<CompanyProject> GetAllProjectByCompanyId(int companyId)
        {
            return _companyProjectUnitOfWork.CompanyProjectRepository.GetAllProjectByCompanyId(companyId);
        }
        public CompanyProject GetCompanyProjectById(int id)
        {
            return _companyProjectUnitOfWork.CompanyProjectRepository.GetById(id);
        }

        public void AddCompanyProject(CompanyProject companyProject )
        {
            var newCompanyProject = new CompanyProject
            {
                Name = companyProject.Name,
                CompanyId = companyProject.CompanyId,                
                Description = companyProject.Description,
               

                CreatedBy = companyProject.CreatedBy,
                CreatedAt = companyProject.CreatedAt,
                IsDeleted = companyProject.IsDeleted,
            };

            _companyProjectUnitOfWork.CompanyProjectRepository.Add(newCompanyProject);
            _companyProjectUnitOfWork.Save();
        }
        public void EditCompanyProject(CompanyProject companyProject)
        {
            var companyProjectEntry = GetCompanyProjectById(companyProject.Id);

            companyProjectEntry.Name = companyProject.Name;
            companyProjectEntry.Description = companyProject.Description;
            companyProjectEntry.CompanyId = companyProject.CompanyId;

            companyProjectEntry.UpdatedAt = companyProject.UpdatedAt;
            companyProjectEntry.UpdatedBy = companyProject.UpdatedBy;
            _companyProjectUnitOfWork.CompanyProjectRepository.Update(companyProjectEntry);
            _companyProjectUnitOfWork.Save();
        }

        public void DeleteCompanyProject(int id, string currUserId)
        {
            _companyProjectUnitOfWork.CompanyProjectRepository.Disable(id);
            _companyProjectUnitOfWork.Save(currUserId);
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
