Thank you for purchasing Trivia Framework

Before Reimporting/updating make sure you backup the databases under resources folder as it may overwrite them with the example.

Support email: AgileReaction@gmail.com

Check online documents for more up to date at:
http://agilereaction.com/assetsdocs/triviaframework/index.html


#Quick Start
1. Click AG > TriviaEditor > Trivia to start adding Trivia Questions.

2. You can add as many Trivia Questions and fake answers as you want.

3. By Default it creates a Default Category on start however you can change its name and add other categories by going to Categories under the AG menu.

4. Create a new GameObject

5. Click add Component and add “Trivia Database Access” Script.

6. Then just use TriviaDatabaseAccess.instance to access the various functions to get Trivia questions and answers along with some functions to make things easier like randomizing answers.

7. Load TriviaDatabase by running TriviaDatabaseAccess.instance.LoadTrivia(true) which will load trivia and randomize it.

8. Get the first question with TriviaDatabaseAccess.instance.GetQuestion() which will return a string of the first question and similarly for answers and fake answers.

9. After you are done with the trivia question use TriviaDatabaseAccess.instance.PopTrivia() function to remove it from the list to prevent repeating the question.