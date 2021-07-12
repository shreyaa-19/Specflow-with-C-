Feature: PostProfile
	Test Post request with response

@mytag
Scenario: Verify Post request operation
	Given Perform Post operation for "posts" with body
	| author | id | title |
	| abcd   | 16 | test  |
	Then I should see "author" name as "abcd"