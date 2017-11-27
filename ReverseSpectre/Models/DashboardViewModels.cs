using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReverseSpectre.Models
{
    public class SalesDirectorDashboardModel
    {
        public List<BranchDashboardModel> Banks { get; set; }
    }

    public class BranchDashboardModel
    {
        public string Name { get; set; }
        public List<BusinessManagerDashboardModel> BusinessManagers { get; set; }
    }

    public class BusinessManagerDashboardModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public List<RelationshipManagerDahboardModel> RelationshipManagers { get; set; }
    }

    public class RelationshipManagerDahboardModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public List<ClientDashboardModel> Clients { get; set; }
    }

    public class ClientDashboardModel
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string Address { get; set; }
        public List<LoanDashboardModel> Loans { get; set; }
    }

    public class LoanDashboardModel
    {
        public int Term { get; set; }
        public double Amount { get; set; }
        public LoanStatusType LoanStatus { get; set; }
    }
}