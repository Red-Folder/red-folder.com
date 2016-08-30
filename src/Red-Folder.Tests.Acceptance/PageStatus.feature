Feature: PageStatus
	In order to avoid accidentally removing pages
	As a site owner
	I want to ensure that pages are in a known state

Scenario: Going to the homepage is valid
	Given I access /
	Then I should receive Ok response

Scenario: Going to the Services page is valid
	Given I access /Services
	Then I should receive Ok response

Scenario: Going to the Projects page is valid
	Given I access /Projects
	Then I should receive Ok response

Scenario: Going to the ROI page is valid
	Given I access /Projects/ROI
	Then I should receive Ok response

Scenario: Going to the AspNetCore page is valid
	Given I access /Projects/AspNetCore
	Then I should receive Ok response

Scenario: Going to the Microservices page is valid
	Given I access /Projects/Microservices
	Then I should receive Ok response

Scenario: Going to the Cordova page is valid
	Given I access /Projects/Cordova
	Then I should receive Ok response

Scenario: Going to the Cordova Background Service Plugin page is valid
	Given I access /Projects/Cordova/BackgroundServicePlugin
	Then I should receive Ok response

Scenario: Going to the Cordova GPS Service Plugin page is valid
	Given I access /Projects/Cordova/GPSServicePlugin
	Then I should receive Ok response

Scenario: Going to the Cordova Scheduler Plugin page is valid
	Given I access /Projects/Cordova/SchedulerPlugin
	Then I should receive Ok response

Scenario: Going to the Cordova Availability Monitor Plugin page is valid
	Given I access /Projects/Cordova/AvailabilityMonitorPlugin
	Then I should receive Ok response

Scenario: Going to the Cordova SMS Handler Plugin page is valid
	Given I access /Projects/Cordova/SMSHandlerPlugin
	Then I should receive Ok response

Scenario: Going to the MyBio page is valid
	Given I access /MyBio
	Then I should receive Ok response

Scenario: Going to the legacy recentprojects page, we are redirected to the projects page
	Given I access /home/recentprojects
	Then I should receive Permanent Redirect response
	And location of /projects

@ignore
Scenario: Going to the Cordova shortcut page is valid and we get correct cannonical
	Given I access /cordova
	Then I should receive Ok response
	And the cannonical is /projects/cordova

Scenario: Going to the fake exception page will provide an error
	Given I access /home/throw
	Then I should receive Error response

Scenario: Going to the non-existant page will provide a not found
	Given I access /home/idontexist
	Then I should receive Not Found response
				