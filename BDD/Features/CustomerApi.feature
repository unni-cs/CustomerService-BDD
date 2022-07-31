Feature: Customer API
@customer

@smoketest
Scenario: Fetch customer details using a valid customer name
	Given I have a valid customer 'jay'
	And I have a valid token for authentication
	When I call the customer api
	Then i should get customer name as 'jay' and credit details as 4300


Scenario: Fetch customer details using a customer name that does not exist
	Given I have customer 'jay123' that does not exist
	And I have a valid token for authentication
	When I call the customer api
	Then I Should get customer not found 404 status