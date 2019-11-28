using EIST.Service;
using EIST.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EIST.Web.Models
{
    public class DashboardModel
    {
        public DashboardSummary dashboardSummary;
        private ProjectService _projectService; 
        private UserService _userService; 
        private IssueService _issueService; 
        private TicketAssignService _ticketAssignService; 

        public DashboardModel()
        {
            _projectService = new ProjectService();
            _userService = new UserService();
            _issueService = new IssueService();
            _ticketAssignService = new TicketAssignService();
            dashboardSummary = new DashboardSummary();

            dashboardSummary.TotalProjects = _projectService.GetProjectCount();
            dashboardSummary.TotalCustomers = _userService.GetCustomerUserCount();
            dashboardSummary.TotalDevelopers = _userService.GetDeveloperUserCount();
            dashboardSummary.TotalUsers = _userService.GetUserCount();
            dashboardSummary.TotalIssues = _issueService.GetIssueCount();
            dashboardSummary.TotalActiveIssues = _issueService.GetAvtiveIssueCount();
            dashboardSummary.TotalCloseIssues = _issueService.GetCloseIssueCount();
            dashboardSummary.TotalTickets = _ticketAssignService.GetTicketCount();
            dashboardSummary.TotalAssignTickets = _ticketAssignService.GetAssignTicketCount();
            dashboardSummary.TotalUnAssignTickets = _ticketAssignService.GetUnAssignTicketCount();
        }
    }
   
    public class DashboardSummary
    {
        public int TotalProjects { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalDevelopers { get; set; }
        public int TotalUsers { get; set; }
        public int TotalIssues { get; set; }
        public int TotalActiveIssues { get; set; }
        public int TotalCloseIssues { get; set; }
        public int TotalTickets { get; set; }
        public int TotalAssignTickets { get; set; }
        public int TotalUnAssignTickets { get; set; }
    }
}