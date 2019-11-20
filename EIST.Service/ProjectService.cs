﻿using EIST.Entities;
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

        public ProjectService()
        {
            _context = new EISTDbContext();
            _companyProjectUnitOfWork = new ProjectUnitOfWork(_context);
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

        public void AddCompanyProject(Project companyProject )
        {
            var newCompanyProject = new Project
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
        public void EditCompanyProject(Project companyProject)
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