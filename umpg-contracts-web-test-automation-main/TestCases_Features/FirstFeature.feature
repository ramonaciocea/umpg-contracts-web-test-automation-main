Feature: FirstFeature

@mytag
Scenario: Test 1
    Given I navigate to application
	And the first number is 50
	And the second number is 70
	And the third number is 70
	When the two numbers are added
	Then the result should be 1

@mytag
Scenario: Test 2
    Given I navigate to application
	And the first number is 50
	And the second number is 70
	When the two numbers are added
	Then the result should be 4

@mytag
Scenario: Test 3
    Given I navigate to application
	And the first number is 50
	And the second number is 70
	When the two numbers are added
	Then the result should be 1