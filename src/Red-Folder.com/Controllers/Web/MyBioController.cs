using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedFolder.ViewModels;

namespace RedFolder.Controllers.Web
{
    public class MyBioController : Controller
    {
        public IActionResult Index()
        {
            var employment = new List<Employment>()
            {
                new Employment(
                        "Sep 2016",
                        "Present",
                        "Technical Consultant (Contract), Betfred",
                        "Providing technical implementation and troubleshooting services across all IT disciplines – architecture, software development, infrastructure, networking and third party engagement.",
                        new List<string>()
                        {
                            "C#",
                            "ASP.Net Core",
                            "React",
                            "Redux",
                            "Team City",
                            "Octopus",
                            "AWS",
                            "AWS - CloudFormation",
                            "AWS - Lambda",
                            "Jest",
                            "xUnit",
                            "SpecFlow"
                        }
                    ),
                new Employment(
                        "Oct 2015",
                        "Aug 2016",
                        "Lead Developer (Contract), Travelbag/ dnata Travel",
                        "Providing interim leadership to a cross-functional development team responsible for the Travelbag eCommerce websites.",
                        new List<string>()
                        {
                            "C#",
                            "ASP.Net MVC/ Web Forms/ WebAPI",
                            "SQL Server",
                            "HTML/ CSS/ JavaScript/ AJAX/ jQuery"
                        }
                    ),
                new Employment(
                        "Jul 2012",
                        "Aug 2015",
                        "Head of IT, Eazyfone Ltd/ Head of IT (Acting), Redeem Group",
                        "Responsible for the internal software products to operate the Redeem Group business of processing second hand mobile phone through testing, payment, warehousing and bulk sales.",
                        new List<string>()
                        {
                            "C#",
                            "ASP.Net MVC/ Web Forms/ WebAPI",
                            "SQL Server",
                            "HTML/ CSS/ JavaScript/ AJAX/ jQuery"
                        }
                    ),
                new Employment(
                        "Jun 2012",
                        "Jul 2012",
                        "Senior .Net Developer (Contract), Ipes",
                        "Responsible for maintenance and development of a bespoke fund management system.",
                        new List<string>()
                        {
                            "C#",
                            "ASP.Net Web Forms",
                            "SQL Server",
                            "SharePoint"
                        }
                   ),
                new Employment(
                        "Sep 2000",
                        "Mar 2012",
                        "Head of IT, The Alternative Parcels Company Ltd",
                        "Responsible for the internal software products to operate the APC’s nationwide overnight courier network through its 120 partner depots.  Oversaw the activities of 13 staff and responsible for a £1m annual budget.",
                        new List<string>()
                        {
                            "C#",
                            "WCF",
                            "SQL Server",
                            "MySQL",
                            "PHP"
                        }
                    ),

                new Employment(
                        "Jun 2000",
                        "Aug 2008",
                        "IT Manager to Head of Development (Acting), Caudwell Group/ Pipex/ Tiscali",
                        "Responsible for various development teams & software products during series of acquisitions and mergers.",
                        new List<string>()
                        {
                            "C#",
                            "ASP.Net Web Forms",
                            "SQL Server",
                            "Oracle",
                            "MySQL",
                            "PHP",
                            "VB.Net",
                            "Informix 4gl"
                        }
                    ),
                new Employment(
                        "Jun 1999",
                        "Jun 2000",
                        "IT Infrastructure, George S. Hall Ltd."
                    ),
                new Employment(
                        "Nov 1995",
                        "Jun 1999",
                        "Analyst & Developer, Caudwell Group."
                    ),
                new Employment(
                        "Aug 1993",
                        "Nov 1995",
                        "Developer, PFS Computer Consultancy (Sussex) Ltd."
                    )
            };
            return View(employment);

        }
    }
}