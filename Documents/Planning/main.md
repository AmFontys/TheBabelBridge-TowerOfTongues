# Planning

This document is about what I'm planning todo and what kind of research I could do.

## What I don't know

### Language

- How to handle the mixing of language
- How to handle multiple alphabets
  - Focused on Japanese Katakana and Hiragana
    - Half-width and Full-width
  - Latin Alphabet
- How do I validate the input with foreign alphabets
- Would using a transliteration library be something for my application and what can it help me with
- How will I handle the language switching
  - First getting language based on pc
  - getting language based on ip
  - getting language based on browser
  - Having a standard one and should be switched
  - At the top always available to switch the language or in settings
  - ResX file or Json files
- Homograph attacks what are they and why are they important for my project

### Project non-functional

- Best strategy to-do benchmarking of the code
  - What projects?
  - How much?
  - Why do benchmarking?
- What is the aspire architecture and how to hold to it
  - What modules will I have
  - For what should I look out for
  - How many Api projects will I have at the end
- How in depth should documentation be.
  - Should all projects enable documentation
  - Do we push everything to github pages or should some be hidden
  - What XML tags should be done for each level of method or type
- What are all my must haves for accessability
  - Keyboard navigation
  - Colors
  - Clarity
- What Ethical Considerations needs to be considered
  - Inclusive
  - Copyright

### Project functionality

- What do I need for multiplayer
  - Real-time communication
  - CRDTs
  - Private match
  - Public match
  - handle 100 people in one room
- Should I add an SQL Database Projects template to my application
  - Keeps the database versioned
  - can be exported to other databases
- Should I add EF Core, Dapper, ADO.NET or something else
- What would be the best way with validation classes or adding annotations to database objects
- How can users add words to the crossword puzzle
  - Choosing from a dictionary/encyclopedia
  - Typing their own words
- What's my testing strategy
  - First writing tests then making the code
  - For each module their own test projects
  - Is integration test separate from the rest of the tests
- How do I make a crossword puzzle grid
- How should I save the puzzle
- What community features are important
  - Social media'
  - Ranking board
  - Match / tournament
  - Sharing puzzles
- Best way to include dependency injection (DI) into the project.
  - Just use Microsoft's DI
  - Use others DI
- What should I use XUnit, NUnit or MSTest
- Should I use 2 different authorization mechanisms
  - Complexity worth it
  - Better security or to much
  - Have a general role (Customer, Employee) which can be divided further ("Hall-of-fame", User manager, official PuzzleMakers)
  - internal / external or manage access to services
- How to handle email services (smtp4dev)
  - For banning players
  - Email verification
- What to do with logging
  - Regular logging or W3C logging
  - What to log
  - Save the logs?
  - Where to save them
  - OpenTelemetry
- What's a good way to visualize the puzzle with hints/text and the multiplayer expect
- Use SignalR or something else?

## Features (in-short)

- Real-time Collaboration
- Language Support
- User Authentication
- Save Puzzle
- Leaderboard and Achievements
- Custom Puzzle Creation
- Hints and Help
- User configuration
- Employee black list users (banning)
- Employee management
