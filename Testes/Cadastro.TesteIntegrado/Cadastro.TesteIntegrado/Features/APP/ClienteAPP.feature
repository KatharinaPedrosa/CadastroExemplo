@APP
Feature: ClienteAPP
	Clients visualization page

@mytag
Scenario: Clients visualization page
	Given That I'm logged on the app, with user "admin" and password "admin"
	When I click on menu "MenuClients"
	Then the label "CurrentLocation" shows "Início"