Feature: SecondFeature

@mytag
Scenario: Test 3
    Given I navigate to application
	And the first number is 50
	And the second number is 70
	When the two numbers are added
	Then the result should be 120

@mytag
Scenario: Test 4
    Given I navigate to application
	And the first number is 50
	And the second number is 70
	When the two numbers are added
	Then the result should be 1