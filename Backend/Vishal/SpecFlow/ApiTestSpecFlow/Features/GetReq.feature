Feature: to test the get request

A short summary of the feature

@tag1
Scenario: Get request testing
	Given The user send a get result with url as "https://reqres.in/api/users?page=2"
	Then request should be a success with 200 Statuscode
