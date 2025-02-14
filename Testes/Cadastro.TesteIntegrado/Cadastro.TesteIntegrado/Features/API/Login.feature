﻿Feature: Login
	Login to the API

@mytag
Scenario: Simple success login 
	Given I'm not logged on the API
	When I'm send a login request with the user "admin" and the password "admin"
	Then the result should be 200
	And the logged user is a system administrator on the API

	@mytag
Scenario: Simple failed login
	Given I'm not logged on the API
	When I'm send a login request with the user "admin" and the password "ola"
	Then the result should be 401