Create an executable web server application:

    implemented in any language
    offering an endpoint to process submitted json data (see below)
    writes the result to a database

Detailed Requirements

Before you start

    Choose framework of your preference

Keep in mind to

    Keep your code a clean as possible
    Create meaningful tests (unit/integration) and reach a sufficient test coverage

There will be some kind of crawler up and running and submitting data to our system. The crwaler is not part of the assignment but can be considered being the consumer of our (future) service.

The submission data is sent to the endpoint in the following (JSON) format:

    submitter: text
    content: text (plain)
    comment: text
    source
        url: text
        date: timestamp

As not all information is needed at the moment, the data is processed prior to being stored in a database: The content is considered to be plain text without any special characters but spaces (e.g. "this is a very important text"). In this object, the three most frequent words are determined and counted.

Example:
for
"tree tree map may tree object game game object tree tree may game may"
tree: 5
may: 3
game: 3
would be the result

If there are no three different words, only as many different words as existing are stored.\

Example:
for
"cat cat you cat you"
cat: 3
you: 2
would be the result as there is no third word that could be counted.

If some frequencies are equal, choose the entries by the frequency and then alphabetically.

Example:
for
"tree tree tree map map some some object object"
tree: 3
map: 2
object: 2
would be the result even though the word "some" is used just as often.

The result is stored together with the current timestamp, the submitter and the source information.
All other information (comment, original content) may be discarded.
Please store this information in an in memory database.

Please make sure the database can be replaced easily as soon as a different system is available.

Please provide information on how to start the application locally.