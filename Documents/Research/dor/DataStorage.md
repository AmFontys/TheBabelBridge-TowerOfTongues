# How can data be stored and processed to accommodate multiple alphabets?

I researched this document following the Guideline conformity analysis to follow best practices and Literature study to search and use well used websites. Community research is more specif to the database postgres due to being it openSource and the community can change things to this product.

## What is needed for the writing systems?

As said in GeeksforGeeks (2024) for a multi lingual database you need to have that are also important for this project:

- Language Management: Supporting multiple languages and language-specific data storage.
- Translation Management: Facilitating the translation of content between different languages.
- Character Encoding: Using appropriate character encoding schemes to handle different languages' character sets.
- Localization: Adapting content and user interfaces to specific languages and cultural preferences.
- Content Delivery: Efficiently delivering language-specific content to users based on their language preferences.
- Search and Indexing: Ensuring language-aware search and indexing capabilities to retrieve relevant content across different languages.

This means that in general if we want to make a multi-lingual database the best one out of these is that it needs to support the specif character that needs to be encoded, this can be a reason to have multiple databases because it needs to be encoded outside of the UTF-8 or it's just cheaper to safe it in one and in the application itself transforming it into the specif character encoding.  
This brings you to the point of Content Delivery how do you do this in principle you need to it so you can search per language and then per culture for it. Due to the limited time that I have for this project the culture will be global for the language and not specific to a region for the language.

Another aspect is the search and indexing part as with as example hiragana characters it is more commonly to search based on brush order which is something that you need to think about with showcasing the data but this is more focused on frontend functionality.  
Localization is also one of the details that can be more frontend then specif to the database.

Then the translation management for the website itself I need to make a separate set of tables to facilitate this like for example:

- Language: where I specify a language code like NL, JP or EN so it can be searched on other tables to get the correct language
- Content: where I set the content string like "welcomeMSG" which has a connection to the language table so I can search it based on either the content string or the language.  
This is helpful if I want to know what languages do I have for this content string and then the language for give me the correct translated version to display on the frontend.

## Does the Postgres accommodate it?

As what said in the previous paragraph character support is one of the only settings that you need to see if it need to be changed to allow multiple writing scripts.  
so following (23.3. Character Set Support, 2025) this resource I see that I can create a table/database to only support Japanese with EUC_JIS_2004 to get extended support of UNIX Code-JP, JIS X 0213. In reality because I only specified languages with UTF support I can use the support of UTF-8 to support all the writing scripts that I have. Although this can be less cost effective due to the storage variance of 1–4 bytes to save a particular character in the database.  
But if I used ISO_8859_6 then I got the latin and arabic parts of the writing systems so I can either transform everything to either of them and later transform it back to save cost in the db storage due to it only having 1 byte bytes/char needed.

## Test of the data

## Conclusion

In conclusion I can use my postgres database, I only need to specify it to have the UTF-8 encoding and need to look out for that the bytes are more then specific encoding like ISO_8859_6.

## Resources

[GeeksforGeeks. (2024, May 16). How to design a database for MultiLanguage Data? GeeksforGeeks.](https://www.geeksforgeeks.org/dbms/how-to-design-a-database-for-multi-language-data/)

[23.3. Character Set support. (2025, May 8). PostgreSQL Documentation.](https://www.postgresql.org/docs/current/multibyte.html)
