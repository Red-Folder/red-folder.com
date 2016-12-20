This is part of my automated build series.  The aim is to automate a lot of the Cordova Background Service plugin process.

In this blog I'll create a master job to make use of the parameterized build and test jobs created previously in this series.

## Summary 
This step is probably the easiest.  It simply calls the jobs already created for the different versions I want to build.

Note that I need to manually add any new versions to this job - but this is a small price to pay for the time saved in the actual build and test.

## Tools 
For this part, I'll be using the following tools:

* Jenkins Version 1.522 (http://jenkins-ci.org/)
* CloudBees Build Flow plugin 0.10 (https://wiki.jenkins-ci.org/display/JENKINS/Build+Flow+Plugin)

## The Job
To create the job;

1. Create a new "Build Flow" project called "BackgroundServicePlugin Build All"
2. Set Source Code Management to None (we will run this manually)
3. Add the following to the Define build flow:
%[https://gist.github.com/Red-Folder/6642918.js] 

And save the Job - yes that's it.

## Wrap up
This really is the easier job to create because all of the heavy lifting is done by the build &amp; test projects.

We will add some additional steps to this job in the next and final part to help prepare for GitHub upload.
