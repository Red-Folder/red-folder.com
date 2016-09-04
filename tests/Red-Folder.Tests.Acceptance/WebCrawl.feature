﻿Feature: WebCrawl
	In order to avoid SEO or user problems
	As a site owner
	I want to be sure that there aren't any dead links

Scenario: Check for deadlinks
	Given I have a web crawler
	And I populate it with http://www.red-folder.com
	When I run it
	Then there should be no invalid urls
