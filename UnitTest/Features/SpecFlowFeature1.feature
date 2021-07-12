Feature: SpecFlowFeature1
	Simple calculator for adding two numbers

@mytag
Scenario: Test get request author
	Given Perform get operation for "posts/{postid}"
	When perform operation for post "1"
	Then I should see "author" name as "Karthik KK"