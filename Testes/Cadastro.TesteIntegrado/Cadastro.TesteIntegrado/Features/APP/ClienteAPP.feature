@APP
Feature: ClienteAPP
	Clients visualization page

@mytag
Scenario: Clients visualization page
	Given That I'm logged on the app, with user "admin" and password "admin"
	When I click on menu "MenuClients"
	Then the label "CurrentLocation" shows "Início"

Scenario: Clients visualization grid
	Given That I'm logged on the app, with user "admin" and password "admin"
	And that the following clients data are on the database
	| Name     | PhoneNumber | CPF            |
	| Matheus  | 6389-5241   | 576.292.670-29 |
	| Luiza    | 7418-9541   | 714.812.560-09 |
	| Joana    | 8524-9632   | 030.683.800-14 |
	| Frida    | 4152-9638   | 229.987.910-26 |
	| Abelardo | 1478-3695   | 123.367.040-99 |
	When I click on menu "MenuClients"
	Then the label "CurrentLocation" shows "Início"
	And the grid must have the following clients
	| Name     | PhoneNumber | 
	| Matheus  | 6389-5241   | 
	| Luiza    | 7418-9541   | 
	| Joana    | 8524-9632   | 
	| Frida    | 4152-9638   | 
	| Abelardo | 1478-3695   | 
	And the grid must have 5 clients

	


