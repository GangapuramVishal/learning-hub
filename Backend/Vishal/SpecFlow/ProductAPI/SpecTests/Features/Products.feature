Feature: Products API

To test the Products API endpoints

@tag1
Scenario: Get all products
    Given the API is running
    When I request all products
    Then the response should contain the following products
      | Id | Name     | Price |
      | 1  | Product1 | 10.00 |
      | 2  | Product2 | 20.00 |

