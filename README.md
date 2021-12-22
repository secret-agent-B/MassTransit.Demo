# MassTransit.Demo

A sample project to experiment with MassTransit Saga and for service to service communication patterns. Feel free to post an issue if you have improvements you'd like to propose.

### Prereq

1. Docker 🐋
2. .NET 6
3. Visual Studio 2022 (recommended) or VS Code for debugging

### APIs & Background Services

1. Customers (Web API)
2. Orders (Web API)
3. Notifications (Hosted Service)

### Quick Links & API Hosts

1. Seq (http://localhost:5341/)
2. RabbitMQ (http://localhost:15672/)
3. NoSQL Client (http://localhost:5000/)
4. Customers API (http://localhost:5001/)
5. Orders API (http://localhost:5002/)

### How to run the demo app

There's a `docker-compose.yml` file within the root of the repository. Just fire up your command-line then type in `docker-compose up -d` to run everything. _This repo should be ready to run and debug locally out of the box._

### How to connect to MongoDb using NoSQL client

1. Go to http://localhost:5000/
2. Hit `Connect` from the top-right corner of the page
3. The `Connections` window will show up, click the `Create New` button
4. Enter a connection name
5. Copy and paste `mongodb://masstransit.demo.mongo/customersdb?retryWrites=true&w=majority` into the `Connection URL` text field
6. Click on `Host/Port` textbox to parse the connection URL into the text fields

Know that the `collections` dropdown will be empty but once you register a new user, you should see a `customers` collection right below it if expanded.

### API Endpoints & CURL commands.

> There are currently 2 commands that you can execute for now; Registration and (fake) activation. I plan to add more as I find more time to code.

#### User Registration

For user registration use the curl command below. When a user registers a `CustomerRegisteredEvent` gets published to the bus (through MassTransit + RabbitMQ) for `Notifications` service to be consumed by `CustomerRegisteredConsumer`. In a real-world scenario; this service should send out an email or SMS to the user containing the activation code.

```
curl --location --request POST 'https://localhost:5001/customers/register' \
--header 'Content-Type: application/json' \
--data-raw '{
    "FirstName": "Joe",
    "LastName": "Smith",
    "Email": "joe.smith@gmail.com",
    "Phone": "1-314-123-4567"
}'
```

#### User Activation

Then after user registration, the account will be in a `PendingActivation` state. Use the curl command below to activate a user. **Do not forget t change the `<customer>` id to the new user's customer id.**

```
curl --location --request GET 'https://localhost:5001/customers/activate/<customer-id>/test-activation'
```
