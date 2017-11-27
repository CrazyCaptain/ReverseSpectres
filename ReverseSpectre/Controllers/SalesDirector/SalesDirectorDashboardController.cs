using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReverseSpectre.Models;

namespace ReverseSpectre.Controllers
{
    public class SalesDirectorDashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            List<BranchDashboardModel> model = new List<BranchDashboardModel>();
            foreach (var b in db.Banks)
            {
                // Bank Branch
                BranchDashboardModel branch = new BranchDashboardModel()
                {
                    Name = b.BranchName,
                    BusinessManagers = new List<BusinessManagerDashboardModel>()
                };

                foreach (var bm in b.BusinessManagers)
                {
                    // Business Manager
                    BusinessManagerDashboardModel businessManager = new BusinessManagerDashboardModel()
                    {
                        FirstName = bm.User.FirstName,
                        MiddleName = bm.User.FirstName,
                        LastName = bm.User.LastName
                    };

                    foreach (var rm in bm.RelationshipManagers)
                    {
                        // Relationship Manager
                        businessManager.RelationshipManagers.Add(new RelationshipManagerDahboardModel()
                        {
                            Id = rm.RelationshipManagerId,
                            FirstName = rm.User.FirstName,
                            MiddleName = rm.User.MiddleName,
                            LastName = rm.User.LastName,
                            Clients = new List<ClientDashboardModel>()
                        });
                    }

                    branch.BusinessManagers.Add(businessManager);
                }

                model.Add(branch);
            }

            // Get all loans
            var loans = db.LoanApplications.ToList();
            
            // Sort loans into Banks > BusinessManager > RelationshipManager > Client > Loan
            foreach (var bank in model)
            {
                foreach (var bm in bank.BusinessManagers)
                {
                    foreach (var rm in bm.RelationshipManagers)
                    {
                        foreach (var loan in loans.Where(m => m.Client.RelationshipManagerId == rm.Id))
                        {
                            var client = rm.Clients.FirstOrDefault(m => m.Id == loan.ClientId);
                            if (client != null)
                            {
                                client.Loans.Add(new LoanDashboardModel()
                                {
                                    Amount = loan.Amount,
                                    LoanStatus = loan.LoanStatus,
                                    Term = loan.Term
                                });
                            }
                            else
                            {
                                ClientDashboardModel c = new ClientDashboardModel()
                                {
                                    Id = loan.ClientId,
                                    Address = loan.Client.BusinessAddress,
                                    BusinessName = loan.Client.BusinessName,
                                    Loans = new List<LoanDashboardModel>()
                                };

                                c.Loans.Add(new LoanDashboardModel()
                                {
                                    Amount = loan.Amount,
                                    LoanStatus = loan.LoanStatus,
                                    Term = loan.Term
                                });

                                rm.Clients.Add(c);
                            }
                        }
                    }
                }
            }
            
            return View(model);
        }
    }
}