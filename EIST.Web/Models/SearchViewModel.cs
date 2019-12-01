using EIST.Common;
using EIST.Entities;
using EIST.Service;
using EIST.Services;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EIST.Web.Models
{
    public class IssueSearchModel
    {
        [Display(Name = "Date Range")]
        public String SDateFrom { get; set; }
        public String SDateTo { get; set; }

        [Display(Name = "Code")]
        public string SCode { get; set; }

        [Display(Name = "Issue Title")]
        public string SIssueTitle { get; set; }

        [Display(Name = "Project")]
        public int? SProjectId { get; set; }

        public Int32 SPage { get; set; }
        public Int32 SPageSize { get; set; }


        public String Sort { get; set; }
        public String SortDir { get; set; }
        public Int32 TotalRecords { get; set; }
        private UserService _userService;
        private ProjectService _projectService;
        int authenticatedUserId = AuthenticatedUser.GetUserFromIdentity().UserId;

        public IPagedList<Issue> IssuePagedList;
        public IEnumerable<SelectListItem> ProjectList { get; set; }

        public IssueSearchModel()
        {
            SPage = 1;
            SPageSize = 20;
            Sort = "Code";
            SortDir = "ASC";

            _userService = new UserService();
            _projectService = new ProjectService();
            //ProjectList = new SelectList(new ProjectModel().GetAllCompanyProjects(), "Id", "Name");
            bool isCustomerUser = _userService.IsUserAsCustomer(authenticatedUserId, EnumUserType.Customer.ToString());
            if (isCustomerUser == true)
            {
                var userAsCustomer = _userService.GetCustomerByUserId(authenticatedUserId);
               ProjectList= new SelectList(_projectService.GetAllProjectByCompanyId(userAsCustomer.CompanyId.Value), "Id", "Name");
            }
            else
            {
                ProjectList = new SelectList(new ProjectModel().GetAllCompanyProjects(), "Id", "Name");
            }
        }
    }
}