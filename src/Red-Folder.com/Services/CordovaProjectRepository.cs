using RedFolder.ViewModels;
using System;
using System.Collections.Generic;

namespace RedFolder.Services
{
    public class CordovaProjectRepository
    {
        public static List<SummaryTile> GetCordovaInfo()
        {
            return new List<SummaryTile>()
            {
                new SummaryTile(
                    "Background Service Plugin",
                    new Paragraphs() 
                    {
                        "My Background Service Plugin provides a framework for other developers to produce background services."
                    },
                    "BackgroundServicePlugin",
                    "Cordova",
                    "Find out more >>>"),
                new SummaryTile(
                    "GPS Service Plugin",
                    new Paragraphs() 
                    {
                        "GPS Background Service Plugin for Cordova/ Phonegap.  This builds apon the Background Serivce Plugin.",
                        "Note: Project is Work in Progress and should be used for example code only."
                    },
                    "GPSServicePlugin",
                    "Cordova",
                    "Find out more >>>"),
                new SummaryTile(
                    "Scheduler Plugin",
                    new Paragraphs() 
                    {
                        "A plugin for Cordova to provide scheduling for Cordova.",
                        "Note: Project is Work in Progress and should be used for example code only."
                    },
                    "SchedulerPlugin",
                    "Cordova",
                    "Find out more >>>"),
                new SummaryTile(
                    "Availability Monitor Plugin",
                    new Paragraphs() 
                    {
                        "Availability Monitor Plugin for Phonegap/ Cordova.",
                        "Note: Project is Work in Progress and should be used for example code only."
                    },
                    "AvailabilityMonitorPlugin",
                    "Cordova",
                    "Find out more >>>"),
                new SummaryTile(
                    "SMS Handler Plugin",
                    new Paragraphs() 
                    {
                        "SMSHandler Plugin for Cordova.",
                        "Note: Project is Work in Progress and should be used for example code only."
                    },
                    "SMSHandlerPlugin",
                    "Cordova",
                    "Find out more >>>")
            };
        }

        public static CordovaProject GetBackgroundServicePluginInfo()
        {
            return new CordovaProject(
                            "Background Service Plugin",
                            new Paragraphs()
                            {
                                "My Background Service Plugin provides a framework for other developers to produce background services."
                            },
                            new List<BlogArticle>()
                            {
                                new BlogArticle(
                                    "Android Background Service for Phonegap/ Cordova",
                                    new Uri("/blog/android-background-service-for-phonegap", UriKind.Relative),
                                    new Paragraphs()
                                    {
                                        "Summary of the history behind the plugin"
                                    }),
                                new BlogArticle(
                                    "Getting Started",
                                    new Uri("/blog/phonegap-android-background-service", UriKind.Relative),
                                    new Paragraphs()
                                    {
                                        "An article describing how to integrate the plugin into an existing Phonegap project.",
                                        "Note: This article is dated - both Phonegap and the plugin have moved on.  It does however introduce some background principals."
                                    }),
                                new BlogArticle(
                                    "Plugin explained",
                                    new Uri("/blog/phonegap-android-background-service_11", UriKind.Relative),
                                    new Paragraphs()
                                    {
                                        "An article going into depth on how the plugin works"
                                    }),
                                new BlogArticle(
                                    "Tutorial - Part 1",
                                    new Uri("/blog/phonegap-service-tutorial-part-1", UriKind.Relative),
                                    new Paragraphs()
                                    {
                                        "Part 1 of a series of 3 which takes you through the steps to create a Phonegap application which will monitor Tiwtter for phonegap mentions, fire a notification and allow the user to select the notification to see the tweets.",
                                        "Note: This article is dated - both Phonegap and the plugin have moved on.  It does however introduce some background principals."
                                    }),
                                new BlogArticle(
                                    "Tutorial - Part 2",
                                    new Uri("/blog/phonegap-service-tutorial-part-2", UriKind.Relative),
                                    new Paragraphs()
                                    {
                                        "Part 2 of a series of 3 which takes you through the steps to create a Phonegap application which will monitor Tiwtter for phonegap mentions, fire a notification and allow the user to select the notification to see the tweets.",
                                        "Note: This article is dated - both Phonegap and the plugin have moved on.  It does however introduce some background principals."
                                    }),
                                new BlogArticle(
                                    "Tutorial - Part 3",
                                    new Uri("/blog/phonegap-service-tutorial-part-3", UriKind.Relative),
                                    new Paragraphs()
                                    {
                                        "Part 3 of a series of 3 which takes you through the steps to create a Phonegap application which will monitor Tiwtter for phonegap mentions, fire a notification and allow the user to select the notification to see the tweets.",
                                        "Note: This article is dated - both Phonegap and the plugin have moved on.  It does however introduce some background principals."
                                    }),
                                new BlogArticle(
                                    "Automated Build - Part 1",
                                    new Uri("/blog/automated-build-part-1", UriKind.Relative),
                                    new Paragraphs()
                                    {
                                        "Part 1 of 6 - How I've automated a lot of the Cordova Background Service plugin process"
                                    }),
                                new BlogArticle(
                                    "Automated Build - Part 2 - The build job",
                                    new Uri("/blog/automated-build-part-2-build-job_11", UriKind.Relative),
                                    new Paragraphs()
                                    {
                                        "Part 2 of 6 - How I've automated a lot of the Cordova Background Service plugin process"
                                    }),
                                new BlogArticle(
                                    "Automated Build - Part 3 - The testing basics",
                                    new Uri("/blog/automated-build-part-3-testing-basics", UriKind.Relative),
                                    new Paragraphs()
                                    {
                                        "Part 3 of 6 - How I've automated a lot of the Cordova Background Service plugin process"
                                    }),
                                new BlogArticle(
                                    "Automated Build - Part 4 - The testing job",
                                    new Uri("/blog/automated-build-part-4-testing-job", UriKind.Relative),
                                    new Paragraphs()
                                    {
                                        "Part 4 of 6 - How I've automated a lot of the Cordova Background Service plugin process"
                                    }),
                                new BlogArticle(
                                    "Automated Build - Part 5 - The master job",
                                    new Uri("/blog/automated-build-part-5-master-job", UriKind.Relative),
                                    new Paragraphs()
                                    {
                                        "Part 5 of 6 - How I've automated a lot of the Cordova Background Service plugin process"
                                    }),
                                new BlogArticle(
                                    "Automated Build - Part 6 - The output job",
                                    new Uri("/blog/automated-build-part-6-output-job", UriKind.Relative),
                                    new Paragraphs()
                                    {
                                        "Part 6 of 6 - How I've automated a lot of the Cordova Background Service plugin process"
                                    }),
                            },
                            new List<SourceCode>()
                            {
                                new SourceCode(
                                    "bgs-core",
                                    new Uri("https://github.com/Red-Folder/bgs-core"),
                                    new Paragraphs()
                                    {
                                        "Core code for the Cordova Background Service"
                                    }),
                                new SourceCode(
                                    "bgs-sample",
                                    new Uri("https://github.com/Red-Folder/bgs-sample"),
                                    new Paragraphs()
                                    {
                                        "Sample Background Service for Cordova"
                                    })
                            });

        }

        public static CordovaProject GetGPSServiceInfo()
        {
            return new CordovaProject(
                            "GPS Service Plugin",
                            new Paragraphs()
                            {
                                "GPS Background Service Plugin for Cordova/ Phonegap.  This builds apon the Background Serivce Plugin.",
                                "Note: Project is Work in Progress and should be used for example code only."
                            },
                            null,
                            new List<SourceCode>()
                            {
                                new SourceCode(
                                    "GPS-Service-Plugin",
                                    new Uri("https://github.com/Red-Folder/GPS-Service-Plugin"),
                                    null)
                            });
        }

        public static CordovaProject GetSchedulerInfo()
        {
            return new CordovaProject(
                            "Scheduler Plugin",
                            new Paragraphs()
                            {
                                "A plugin for Cordova to provide scheduling for Cordova.",
                                "Note: Project is Work in Progress and should be used for example code only."
                            },
                            null,
                            new List<SourceCode>()
                            {
                                new SourceCode(
                                    "Scheduler-Plugin",
                                    new Uri("https://github.com/Red-Folder/Scheduler-Plugin"),
                                    null)
                            });
        }

        public static CordovaProject GetAvailabilityMonitorInfo()
        {
            return new CordovaProject(
                            "Availability Monitor Plugin",
                            new Paragraphs()
                            {
                                "Availability Monitor Plugin for Phonegap/ Cordova.",
                                "Note: Project is Work in Progress and should be used for example code only."
                            },
                            null,
                            new List<SourceCode>()
                            {
                                new SourceCode(
                                    "Availability-Monitor-Plugin",
                                    new Uri("https://github.com/Red-Folder/Availability-Monitor-Plugin"),
                                    null)
                            });
        }


        public static CordovaProject GetSMSHandlerInfo()
        {
            return new CordovaProject(
                            "SMS Handler Plugin",
                            new Paragraphs()
                            {
                                "SMSHandler Plugin for Cordova.",
                                "Note: Project is Work in Progress and should be used for example code only."
                            },
                            null,
                            new List<SourceCode>()
                            {
                                new SourceCode(
                                    "Cordova-Plugin-SMSHandler",
                                    new Uri("https://github.com/Red-Folder/Cordova-Plugin-SMSHandler"),
                                    null)
                            });
        }

    }
}