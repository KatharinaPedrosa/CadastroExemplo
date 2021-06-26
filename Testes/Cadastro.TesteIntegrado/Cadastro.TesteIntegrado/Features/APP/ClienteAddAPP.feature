@APP
Feature: ClienteAddAPP
	Clients add page

@mytag
Scenario: Client add page
	Given That I'm logged on the app, with user "admin" and password "admin"
	When I click on menu "MenuClientEdit"
	Then the label "CurrentLocation" shows "Adicionar Cliente"
	And the label "ClientId" shows "0"

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


Scenario: Check required clients fields
	Given That I'm logged on the app, with user "admin" and password "admin"
	And that I'm on menu "MenuClientEdit"
	When I click on button "Salvar"
	Then the following validation fields are shown
	| field                     | value                        |
	| ClientName                | Campo Nome obrigatório       |
    | ClientCPF                 | Campo CPF obrigatório        |
	| ClientOccupation          | Campo Ocupação obrigatório   |
	| ClientDateOfBirth         | Data de nascimento inválida  |
	| ClientPhoneNumber         | Campo Telefone obrigatório   |
	| ClientAddressStreet       | Campo logradouro obrigatório |
	| ClientAddressNeighborhood | Campo bairro obrigatório     |
	| ClientAddressCity         | Campo cidade obrigatório     |
	| ClientAddressState        | Campo estado obrigatório     |
	| ClientAddressCountry      | Campo país obrigatório       |

Scenario: Check CPF validation
	Given That I'm logged on the app, with user "admin" and password "admin"
	And that I'm on menu "MenuClientEdit"
	When I fill the fields with the following data
	| field     | value     |
	| ClientCPF | 417523698 |
	And I click on button "Salvar"
	Then the following validation fields are shown
	| field     | value        |
	| ClientCPF | CPF inválido |
	
Scenario: Check data of birth validation(actual date)
	Given That I'm logged on the app, with user "admin" and password "admin"
	And that I'm on menu "MenuClientEdit"
	When I type the current date on the field ClientDateOfBirth 
	And I click on button "Salvar"
	Then the following validation fields are shown
	| field             | value                       |
	| ClientDateOfBirth | Data de nascimento inválida |
	
Scenario: Check data of birth validation(200 years old)
	Given That I'm logged on the app, with user "admin" and password "admin"
	And that I'm on menu "MenuClientEdit"
	When I type the current date minus 200 years on the field ClientDateOfBirth 
	And I click on button "Salvar"
	Then the following validation fields are shown
	| field             | value                       |
	| ClientDateOfBirth | Data de nascimento inválida |

Scenario: Clients edit
	Given That I'm logged on the app, with user "admin" and password "admin"
	And that the following clients data are on the database
	| Name   | PhoneNumber | CPF   |
	| <nome> | <telefone>  | <cpf> |	
	When I click on edit on client "<nome>"
	Then the label "CurrentLocation" shows "Editar Cliente"
	And the input "ClientName" shows "<nome>"
	And the input "ClientPhoneNumber" shows "<telefone>"
	And the input "ClientCPF" shows "<cpf>"
	Examples: 
	| nome    | telefone  | cpf            |
	| Matheus | 6389-5241 | 576.292.670-29 |