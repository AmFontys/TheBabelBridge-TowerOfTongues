# Feature Planning

In this document I will specify each feature that I'm going to implement from top to bottom it will be the importance.  
It can be that some other need to be implemented first to have it available on the frontend but for the backend it won't matter.  

| Category | Name | Done | Description |
|----------|------|------|-------------|
|Generation| Crossword Generation | &#x2611; | You can generate a simple crossword puzzle |
|Generation|Multi-lang Generation|&#x2611;|You can create a crossword with multiple languages and alphabets but not together|
|Database|Save Crossword|&#x2611;| Save the generated crossword into the database|
|Database|Retrieve Crossword|&#x2611;| Get the saved generated crossword|
|Crossword|Show Crosswords|&#x2611;| Show all possible crosswords that can be played|
|Crossword|Play Crossword|&#x2611;| Play a specif crossword|
|Crossword|Finish Crossword|&#x2611;| Make it able to finish the crossword when everything is filled in |
|Crossword| Multiplayer small|&#x2611;| Be able to at least connect with 2 people on the same crossword|
|Lang| Multi lang app|&#x2612;| Be able to switch languages of the interface/Frontend|
|Crossword|Hints on the crossword|&#x2611;| Be able to see the hints on the crossword with the numbers|
|Cloud|Cloud site|&#x2611;| Have the entire application be able to run in the cloud|
|Generation|File Generation|&#x2611;| A player can give a (csv) file with all the words and directions they specify and based on that generate the crossword |
|Security|Only auth functions|&#x2611;|Make it so only when logged in or being an employee certain functions can be executed|
|Database|Database Que|&#x2612;|When adding or retrieving data wait for it on a que so downtown of entering data won't happen easily|
|Security|Two-Factor Authentication|&#x2612;|Add two-factor authentication for enhanced account security|
|Multiplayer|Real-time Collaboration|&#x2611;|Improve the multiplayer experience so the host is more important and with accidental changes of both(or more) players that host changes will be accepted|
|Multiplayer|CRDTs|&#x2611;|Add CRDTs to solve the conflicts better then `Real-time Collaboration` and also more graceful|
|Employee|Employee Crossword|&#x2611;|An employee crossword should standout and give the impression that it differs from the user generation|
|Employee|Player management|&#x2612;|An employee can help a player if they want to know what is saved for them (GDPR) and if they have an account|
|Leaderboard|Leaderboard of speed|&#x2612;|A player can come on the leaderboard with the time they took|
|Employee| Roles & Roles|&#x2611;|Make it so only a specif employee role can do something and doesn't cross what they should be able to do(ex. HR nothing with players or Crossword makers nothing with finance)|
|Employee|Role management|&#x2611;| Add specif roles that only some employees can do|
|Crossword |Puzzle Difficulty Levels|&#x2611;|Offer different difficulty levels for crosswords (easy, medium, hard)|
|Social|Chat System|&#x2612;|Implement a chat system for players to communicate during multiplayer games|
|Support |Help Center|&#x2612;|Provide a help center with FAQs and support options|
|Generation| Clear all|&#x2615;|Make it easy to delete all words from the list and start over|
|Generation|Word finder|&#x2612;| A player can put in their begin of a word and it will look for closest match words to speed up the entering of words|
|Employee| Send email to player|&#x2612;|Make it so an employee can send an email to a player like a ban or changes to privacy statement|
|Cloud|Kubernetes support|&#x2611;|Make it so the application is run in Kubernetes|
|Employee|Ban list|&#x2612;|An employee can ban a player for whatever reason and they shouldn't be able to make a new account or play for sometime (soft-ban) |
|Employee|Ban list extended|&#x2612;|An employee can ban a player and they shouldn't be able to make a new account and can't register with the same MAC/IP-address (hard-ban) (this is here for the extra challenge that this can give) |
|Account|Login & register|&#x2611;| Be able to login & register as a person|
|Account| Statements|&#x2612;| Have a page(s) with the privacy statement and terms of service so the person knows what the website will do with their information (GDPR) |
|Account|Account settings|&#x2612;| Make the user be able to do settings like recommended language, color of background, etc.|
|Generation|Multi-Alp Generation|&#x2611;| You can use multiple alphabets to generate a crossword|
|Crossword|Tournament style|&#x2612;| Make a tournament where people can sign up for or only specif people/users and play a tournament where a series of crosswords will be played against each other in one-vs-one style|
|Accessability|Make website accessible|&#x2615;|Make sure that the entire website is accessible|
|Database| Save game|&#x2611;|Save a players crossword that they played|
|Database|Retrieve game|&#x2611;| Retrieve a players crossword that they played|
|Cloud|Auto shutdown|&#x2612;| Make it so the cloud shutdowns automatically|
|Social|Share via social media|&#x2612;|Make it so you can share a picture of the finished crossword or link to play it via social media |
|Integration|Third-party Integrations|&#x2612;|Integrate with third-party services like Google, Facebook, or Apple for login and sharing|
|Tournament| User made tournament|&#x2612;|An user can organize their own tournaments and specify who they want to be able to enter|
| ||||
