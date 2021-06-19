@APP
Feature: Login
	Login to the APP

Scenario: Simple success login
	Given That I'm not logged on the app
	When I type "admin" on "Login" field 
	And I type "admin" on "Password" field
	And I click on button "SignIn" 
	Then the login was successful
	And the logged user "admin" it's shown on the top of the screen  

Scenario: Simple failed login
	Given That I'm not logged on the app
	When I type "admin" on "Login" field 
	And I type "ola" on "Password" field
	And I click on button "SignIn" 
	Then the login was not successful
	  
Scenario: Simple logout
	Given That I'm logged on the app, with user "admin" and password "admin"
	When I click on button "SignOut"
	Then the logout was successful
	