@APP
Feature: UsuarioAPP
	Testing user features, insertion, edit, delete

@mytag
Scenario: Insert a user
	Given That I'm logged on the app, with user "admin" and password "admin"
	When I click on menu "MenuUsers"
	Then the label "CurrentLocation" shows "Usuários"
	When I type "aladin" on "UserLogin" field
	And I type "jasmine" on "UserPassword" field
	And I click on button "Salvar"
	Then the user with login "aladin" is on the user grid

Scenario: Login with a new user
	Given That I'm logged on the app, with user "admin" and password "admin"
	When I click on menu "MenuUsers"
	And I type "<User>" on "UserLogin" field
	And I type "<Passowrd>" on "UserPassword" field
	And I click on button "Salvar"
	And I click on button "SignOut"
	And I type "<User>" on "Login" field
	And I type "<Passowrd>" on "Password" field
	And I click on button "SignIn"
	Then the logged user "<User>" it's shown on the top of the screen
	And the menu "MenuUsers" it's not shown
	Examples: 
	| User   | Password |
	| aladin | jasmine  |

Scenario: Edit a user
	Given That I'm logged on the app, with user "admin" and password "admin"	
	And that the following users data are on the database	
	| User   | Password   |
	| <user> | <password> |
	When I click on menu "MenuUsers"		
	And I click on edit on user "<user>"
	And I type "<new user>" on "UserLogin" field
	And I type "<new password>" on "UserPassword" field
	And I click on button "Salvar"
	Then the user with login "<new user>" is on the user grid
	When I click on button "SignOut"
	And I type "<new user>" on "Login" field
	And I type "<new password>" on "Password" field
	And I click on button "SignIn"
	Then the logged user "<new user>" it's shown on the top of the screen
	And the menu "MenuUsers" it's not shown
	Examples:
	| user   | password | new user | new password |
	| aladin | jasmine  | peterPan | sininho      |


	Scenario Outline: Delete a user 
	Given That I'm logged on the app, with user "admin" and password "admin"
	And that the following users data are on the database	
	| User   | Password   |
	| <user> | <password> |
	When I click on menu "MenuUsers"		
	And I click on remove on user "<user>"
	Then a user with login "<user>" it's not present on the database 	
	Examples:
	| user  | password |
	| luffy | monkey   |