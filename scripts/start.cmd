cd..
cd nginx
start nginx.exe

cd ..
cd Valuator
start dotnet run --urls "http://localhost:5001/"
start dotnet run --urls "http://localhost:5002/"

cd..
cd..
start nats\nats-server.exe

cd DisturbedProgramming
cd EventsLogger
dotnet build
start dotnet run --no-build
start dotnet run --no-build

cd..
cd RankCalculator
dotnet build
start dotnet run --no-build
start dotnet run --no-build



pause
