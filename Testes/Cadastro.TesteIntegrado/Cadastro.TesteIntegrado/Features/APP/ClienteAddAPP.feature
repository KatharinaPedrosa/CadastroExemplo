@APP
Feature: ClienteAddAPP
	Clients add page

@mytag
Scenario: Client add page
	Given That I'm logged on the app, with user "admin" and password "admin"
	When I click on menu "MenuClientEdit"
	Then the label "CurrentLocation" shows "Adicionar Cliente"

Scenario: Add new client
	Given That I'm logged on the app, with user "admin" and password "admin"
	And that I'm on menu "MenuClientEdit"
	When I fill the fields with the following data
		| field                     | value            |
		| ClientName                | <nome>           |
		| ClientCPF                 | <cpf>            |
		| ClientOccupation          | <ocupacao>       |
		| ClientDateOfBirth         | <dataNascimento> |
		| ClientPhoneNumber         | <telefone>       |
		| ClientAddressStreet       | <rua>            |
		| ClientAddressNumber       | <numero>         |
		| ClientAddressComplement   | <complemento>    |
		| ClientAddressNeighborhood | <bairro>         |
		| ClientAddressCity         | <cidade>         |
		| ClientAddressState        | <estado>         |
		| ClientAddressCountry      | <pais>           |
	And I click on button "Salvar"
	Then the label "CurrentLocation" shows "Início"
	And the client with name "<nome>" and phone number "<telefone>" is on the client grid 
	Examples:
		| nome    | cpf            | ocupacao  | dataNascimento | telefone  | rua                 | numero | complemento | bairro     | cidade | estado | pais   |
		| juvenal | 018.640.670-39 | professor | 19/08/1980     | 2351-4789 | rua dos caranguejos | 150    | casa        | san martin | Recife | PE     | Brasil |
		| josefa  | 640.126.050-54 | advogada  | 23/05/1985     | 7894-5623 | rua da alegria      | 300    | casa        | setubal    | Recife | PE     | Brasil |