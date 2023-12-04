## Hi, VSG! 👋 Welcome to my Binance feed solution. ##

A few quick words: The main focus was on the infrastructure and the functionality. Abstraction and code quality became 2nd priority due to time limitations. Working app > well abstracted unfinished app.

## How to get you started? ##

Prerequisite:
- Install Docker on the running machine (skip if not applicable);
- If you're running the docker compose on Apple M1/M2 Silicon, please go to Docker Settings->General and check Use Rosetta for x86/amd64 emulation on Apple Silicon.

Next:
- Open the root folder of the project and execute `docker compose up` through the Console/Terminal.

The Docker compose will spin up 5 containers that are configured to work in a network.\
The containers are:

- BinanceFeed.API - web API project with the 2 required GET endpoints;
- BinanceFeed.Console - console app with a simple UI to interact with (just to show code reusability);
- BinanceFeed.DataSeeder - worker app/background service that opens a socket connection to Binance streams for the provided tickers/symbols and saves the data in the db;
- MsSqlServer - db server to store the events' information;
- Redis - caching server.

## How to play with it? ##

**Web API:**
To play with the web API go to Swagger's page at `http://localhost:5272/swagger/index.html`. **IMPORTANT!** There is output caching implemented for `GET api/{symbol}/SimpleMovingAverage` endpoint which is currently set at 20 sec (timeout age is visible in the response header under the '**age**' attribute).

**Console:**
To play with the Console app you could open Terminal/Console at root folder of the project and run `docker compose run -it binancefeedconsole`. This should spin up the console app container and attach an *interactive* mode to the Terminal/Console so you could interact with the it.

## How to run _unit_ + _integration_ tests? ##
I have written a few unit tests + a few integration tests. To run them, open the root folder of the project and execute `dotnet test` through the Console/Terminal.

## How to access the _DB_ & _Cache_? ##

**DB:**
To access the DB - "host": "127.0.0.1", "port": 5433, "user": "sa", "password": "PassWord42";

**Redis:**
To access the Redis cache - "host": "127.0.0.1", "port": 6379.
