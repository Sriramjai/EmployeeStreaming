# EmployeeStreaming
A web application which produces messages to kafka topic and consumes the messages from topic and inserts the data in database. An Asp.Net Core REST API that allows you to present the employee details from the table in pages with a configurable page size.

EmployeeInfoProducer - This is an ASP .NET Core Web API which acts as the producer and sends message to the kafka topic. Users can use the swagger UI interface to send data to kafka topic.

EmployeeInfoConsumer - This is project will consume the message as soon as they appear in the kafka topic it is subscribed to. Once the message is consumed, the data is inserted into a table in SQL server. I also tried to set up SignalR client and I have created the signalR client and wired it up to consume the message from topic but was not able to test it. Ran into web socket issues when runnning the application.

EmployeeInfoAPI - This is an ASP .NET Core REST API project which will fetch the employee data from the database and displays it to the users. You can use postman to view the data or swagger when you run the application. I have set up pagination for this so that only 50 records will be shown to the user. The user can pass this value through query parameters in URL to fetch the set of records.

Setup with screenshots - Contains the setup instructions along with supporting screenshots.
