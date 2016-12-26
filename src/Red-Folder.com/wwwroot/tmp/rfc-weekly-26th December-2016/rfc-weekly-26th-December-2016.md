## .Net Core, React & DevOps
Made great progress in the last week.  I now have a TeamCity instance monitoring multiple git feature branches and automatically building, testing and deploying (via Octopus) to a server.

This is even setup so that each website is names after the feature branch.  Useful trick to allow testing and validation by feature branch.

(although I'd prefer to avoid feature branches - but I can also see teh benefits of it).

Also used [dotnet-win32-service](https://github.com/dasMulli/dotnet-win32-service) to generate a template windows service.  This too I've set up to go through TeamCity for Continuous Integration with it being automatically deployed to a dev server.

## Octopus deploy & Azure Websites
I've recently been having problems with Octopus being able to deploy to automatically.  The issue is described [here](http://help.octopusdeploy.com/discussions/problems/49838-enable-appoffline-is-true-azure-web-app-still-fails-with-error_file_in_use) and this seems to be a common problem that is arising.

I found that adding an Azure Powershell task before attempting to deploy which restarted the website has resolved the problem:

```
if ("#{Octopus.Environment.Name}" -like "Production")
{
    Restart-AzureWebSite -Name [Name of Production Site]
}
else
{
    Restart-AzureWebSite -Name [Name of Staging Site]
}
```
I'm not completely happy with this aprroach - a restart seems rather heavy.  But seems to work for now.

## Ethical Hacking progress
Better progress on the [Pluralsight Ethical Hacking](https://app.pluralsight.com/paths/certificate/ethical-hacking) Path this week.

Now 25% through:
![Ethical Hacking Progress](/media/blog/rfc-weekly-26th-December-2016/PluralsightEHPath.PNG)

I've completed the [Enumeration](https://app.pluralsight.com/library/courses/ethical-hacking-enumeration/table-of-contents), for which I got 10/10
![Enumeration Learning Check](/media/blog/rfc-weekly-26th-December-2016/enumeration-LearningCheck.PNG)

##Shameless self-promotion
Final ROI post for the year.  [ROI of Spotify](https://www.linkedin.com/pulse/roi-spotify-mark-taylor) is a festive treat highlighting 2 videos from Spotify talking about thier engineering processes ... a real treat.

This is the last RFC Weekly for the year.  I'm likely to have a couple of weeks off and should be back mid January.
