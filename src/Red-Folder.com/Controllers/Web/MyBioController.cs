using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RedFolder.ViewModels;

namespace RedFolder.Controllers.Web
{
    public class MyBioController : Controller
    {
        public IActionResult Index()
        {
            var employment = new Employment[]
            {
                new Employment(
                        "Sep 2016",
                        "Apr 2022",
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

            var certifications = new Certification[]
            {
                new Certification(
                    "Microsoft Certified: Azure Developer Associate",
                    "March 2019, March 2021 (renewal), April 2022 (renewal)",
                    "Earning Azure Developer Associate certification validates the skills and knowledge to design and build cloud solutions such as applications and services. Candidates participate in all phases of development, from solution design, to development and deployment, to testing and maintenance. They partner with cloud solution architects, cloud DBAs, cloud administrators, and clients to implement the solution.",
                    "/images/acclaim/microsoft-certified-azure-developer-associate.png",
                    "https://www.youracclaim.com/badges/6025d4d7-9c2b-48d8-93f1-ee5cb6b51f50/public_url"
                ),
                new Certification(
                    "Microsoft Certified: Azure Administrator Associate",
                    "July 2019, March 2021 (renewal), March 2022 (renewal)",
                    "Earning Azure Administrator Associate certification validates the skills and knowledge to manage cloud services that span storage, security, networking, and compute cloud capabilities. Candidates have a deep understanding of each service across the full IT lifecycle, and take requests for infrastructure services, applications, and environments. They make recommendations on services to use for optimal performance and scale, as well as provision, size, monitor, and adjust resources as appropriate.",
                    "/images/acclaim/microsoft-certified-azure-administrator-associate.png",
                    "https://www.youracclaim.com/badges/b724082b-ccdb-4035-ab65-b7d702fc5b5f/public_url"
                ),
                new Certification(
                    "MCSD: App Builder — Certified 2018",
                    "July 2018",
                    "Earners of the MCSD: App Builder certification have demonstrated the skills required to build modern mobile and/or web applications and services. Earning this certification qualifies an individual for a position as an application developer.",
                    "/images/acclaim/mcsd-app-builder-certified-2018.png",
                    "https://www.youracclaim.com/badges/e2c276ef-e5ed-4b70-b52d-312ea8595aff/public_url"
                ),
                new Certification(
                    "MCSA: Cloud Platform - Certified 2018",
                    "July 2018",
                    "Earners of the MCSA: Cloud Platform certification have demonstrated the skills required to reduce IT costs and deliver more value for the modern business by implementing Microsoft cloud-related technologies. They are qualified for a position as a cloud administrator or architect.",
                    "/images/acclaim/mcsa-cloud-platform-certified-2018.png",
                    "https://www.youracclaim.com/badges/7cc84a0c-228d-40a9-8c88-7ee29722334c/public_url"
                ),
                new Certification(
                    "Exam 532: Developing Microsoft Azure Solutions",
                    "July 2018",
                    "This exam is for candidates who are experienced in designing, programming, implementing, automating, and monitoring Microsoft Azure solutions. Passing this exam validates a candidate’s ability to create and manage Azure Resource Manager Virtual Machines, design and implement storage and data strategy, manage identity, application, and network services, and design and implement Azure PaaS compute and web and mobile services.",
                    "/images/acclaim/exam-532-developing-microsoft-azure-solutions.1.png",
                    "https://www.youracclaim.com/badges/fb55d904-c066-45a1-90cd-fe86a5218acc/public_url"
                ),
                new Certification(
                    "MCSD: App Builder — Certified 2017",
                    "August 2017",
                    "Earners of the MCSD: App Builder certification have demonstrated the skills required to build modern mobile and/or web applications and services. Earning this certification qualifies an individual for a position as an application developer.",
                    "/images/acclaim/mcsd-app-builder-certified-2017.png",
                    "https://www.youracclaim.com/badges/7dcebf78-4de1-4c5f-b783-7dfa9fd9dc12/public_url"
                ),
                new Certification(
                    "Exam 534: Architecting Microsoft Azure Solutions",
                    "August 2017",
                    "This exam is for candidates who are interested in validating their Microsoft Azure solution design skills. Passing this exam validates a candidate’s ability to identify tradeoffs and make decisions for designing public and hybrid cloud solutions.  Earners of this badge are able to define the appropriate infrastructure and platform solutions to meet the required functional, operational, and deployment requirements through the solution lifecycle.",
                    "/images/acclaim/exam-534-architecting-microsoft-azure-solutions.png",
                    "https://www.youracclaim.com/badges/f01e8e5a-7bc7-4496-a570-dd1c367aebc2/public_url"
                ),
                new Certification(
                    "MCSD: App Builder — Certified 2016",
                    "September 2016",
                    "Earners of the MCSD: App Builder certification have demonstrated the skills required to build modern mobile and/or web applications and services. Earning this certification qualifies an individual for a position as an application developer.",
                    "/images/acclaim/mcsd-app-builder-certified-2016.png",
                    "https://www.youracclaim.com/badges/f57a78d2-31ae-42d5-a39d-48eaa1bd06cd/public_url"
                ),
                new Certification(
                    "MCSA: Web Applications - Certified 2016",
                    "September 2016",
                    "Earners of the MCSA: Web Applications certification have demonstrated the skills required to implement modern web apps. They are qualified for a position as a web developer or web administrator.",
                    "/images/acclaim/mcsa-web-applications-certified-2016.png",
                    "https://www.youracclaim.com/badges/9ffb89e1-1f12-4382-9033-028aaebe793b/public_url"
                ),
                new Certification(
                    "Scrum.Org: Professional Scrum Master (PSM I)",
                    "February 2016",
                    "The Professional Scrum Master level I (PSM I) assessment validates the depth of knowledge of the Scrum framework and its application.",
                    "/images/PSMI.png",
                    "https://www.scrum.org/Assessments/Professional-Scrum-Master-Assessments/PSM-I-Assessment"
                ),
                new Certification(
                    "Exam 487: Developing Microsoft Azure and Web Services",
                    "December 2013",
                    "Passing Exam 487: Developing Microsoft Azure and Web Services validates the skills and knowledge necessary to develop Web Services. Candidates demonstrate the ability to assess Azure data, manipulate and query data using Entity Framework, as well as designing and implementing WCF and Web API-based services.",
                    "/images/acclaim/exam-487-developing-microsoft-azure-and-web-services.png",
                    "https://www.youracclaim.com/badges/c985d6a7-c36d-491e-84c7-d793eb7fd2d9/public_url"
                ),
                new Certification(
                    "70-486: Developing Asp.net MVC Applications",
                    "June 2013",
                    "Passing Exam 486: Developing ASP.NET MVC Web Applications validates a candidate’s ability to design the user experience, security solutions, and application architecture as well as troubleshoot and debug web applications.",
                    "/images/acclaim/exam-486-developing-asp-net-mvc-web-applications.png",
                    "https://www.youracclaim.com/badges/25418790-497e-4884-a618-170d3e0c6299/public_url"
                ),
                new Certification(
                    "Exam 480: Programming in HTML5 with JavaScript and CSS3",
                    "February 2013",
                    "Passing Exam 480: Programming in HTML5 with JavaScript and CSS3  validates a candidate’s ability to access and secure data as well as implement document structures, objects, and program flow.",
                    "/images/acclaim/exam-480-programming-in-html5-with-javascript-and-css3.png",
                    "https://www.youracclaim.com/badges/cecbbf3b-81cf-489e-8dad-8dff10653350/public_url"
                )
            };

            var viewModel = new MyBio
            {
                Employment = employment,
                Certifications = certifications
            };

            return View(viewModel);
        }
    }
}
