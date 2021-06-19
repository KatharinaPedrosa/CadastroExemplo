Feature: Usuario
	Testing user features, insertion, query, exclusion 

@mytag
Scenario Outline: Insert a user 
	Given I'm logged on the API, with the user "admin" and the password "admin"
	When insert a user with the following data 
	| field    | data       |
	| login    | <login>    |
	| password | <password> |
	Then the result should be 200
	And a user with login "<login>" was inserted
	Examples: 
	| login  | password |
	| ariel  | ariel@10 |
	| ursula | ursula!7 |


Scenario Outline: Consult a user by id
	Given I'm logged on the API, with the user "admin" and the password "admin"
	When insert a user with the following data 
	| field    | data       |
	| login    | <login>    |
	| password | <password> |
	Then the result should be 200
	When consult a user with login "<login>" by id  
	Then the result should be 200
	Examples: 
	| login  | password |
	| ariel  | ariel@10 |
	| ursula | ursula!7 |


Scenario Outline: Delete a user 
	Given  I'm logged on the API, with the user "admin" and the password "admin"
	When insert a user with the following data
	 | field    | data     |
	 | login    | ariel    |
	 | password | ariel@10 |
	Then the result should be 200
	When insert a user with the following data
	| field    | data     |
	| login    | ursula   |
	| password | ursula!7 |
	Then the result should be 200
	When send a request to delete the user with login "ariel"
	Then the result should be 200
	And a user with login "ariel" it's not present on the database 
	And a user with login "ursula" it's present on the database 

Scenario Outline: Login with a new user 
	Given  I'm logged on the API, with the user "admin" and the password "admin"
	When insert a user with the following data
	 | field    | data     |
	 | login    | ariel    |
	 | password | ariel@10 |
	Then the result should be 200
	When I'm send a login request with the user "ariel" and the password "ariel@10"
	Then the result should be 200
	

