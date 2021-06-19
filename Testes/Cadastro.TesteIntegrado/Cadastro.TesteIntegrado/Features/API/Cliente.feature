Feature: Cliente
	Testing client features, insertion, query, exclusion

@mytag
Scenario Outline: Insert a client
	Given  I'm logged on the API, with the user "admin" and the password "admin"
	When insert a client with the following data
	 | field         | value            |
	 | name          | <name>           |
	 | cpf           | <cpf>            |
	 | occupation    | padeiro          |
	 | date of birth | 1980-06-15       |
	 | phone number  | 8888-9999        |
	 | street        | rua sao jeronimo |
	 | number        | 130              |
	 | complement    | casa             |
	 | neighborhood  | fortaleza        |
	 | city          | blumenau         |
	 | state         | SC               |
	 | country       | Brasil           |
	Then the result should be 200
	And a client with name "<name>" and cpf "<cpf>" was inserted
	Examples:
	| name  | cpf            |
	| joão  | 018.640.670-39 |
	| maria | 640.126.050-54 |


Scenario Outline: Find a client by id
	Given  I'm logged on the API, with the user "admin" and the password "admin"
	When insert a client with the following data
	 | field         | value            |
	 | name          | <name>           |
	 | cpf           | <cpf>            |
	 | occupation    | padeiro          |
	 | date of birth | 1980-06-15       |
	 | phone number  | 8888-9999        |
	 | street        | rua sao jeronimo |
	 | number        | 130              |
	 | complement    | casa             |
	 | neighborhood  | fortaleza        |
	 | city          | blumenau         |
	 | state         | SC               |
	 | country       | Brasil           |
	Then the result should be 200
	And get the id of the inserted client with name "<name>"
	And find a client by id
	Examples:
	| name  | cpf            |
	| joão  | 018.640.670-39 |
	| maria | 640.126.050-54 |


Scenario Outline: Delete a client 
	Given  I'm logged on the API, with the user "admin" and the password "admin"
	When insert a client with the following data
	 | field         | value            |
	 | name          | joao             |
	 | cpf           | 018.640.670-39   |
	 | occupation    | padeiro          |
	 | date of birth | 1980-06-15       |
	 | phone number  | 8888-9999        |
	 | street        | rua sao jeronimo |
	 | number        | 130              |
	 | complement    | casa             |
	 | neighborhood  | fortaleza        |
	 | city          | blumenau         |
	 | state         | SC               |
	 | country       | Brasil           |
	And insert a client with the following data
	 | field         | value            |
	 | name          | maria            |
	 | cpf           | 640.126.050-54   |
	 | occupation    | padeiro          |
	 | date of birth | 1980-06-15       |
	 | phone number  | 8888-9999        |
	 | street        | rua sao jeronimo |
	 | number        | 130              |
	 | complement    | casa             |
	 | neighborhood  | fortaleza        |
	 | city          | blumenau         |
	 | state         | SC               |
	 | country       | Brasil           |
	Then the result should be 200
	And get the id of the inserted client with name "joao"
	And send a request to delete the found client
	And the result should be 200
	And a client with name "joao" it's not present on the database 
	And a client with name "maria" it's present on the database 
