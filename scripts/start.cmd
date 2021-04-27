cd ..
cd Valuator
start dotnet run --urls "http://localhost:5001/"
start dotnet run --urls "http://localhost:5002/"

cd..
cd..
start nats\nats-server.exe

cd DisturbedProgramming
cd EventsLogger
start dotnet run --no-build
start dotnet run --no-build

cd..


cd RankCalculate
start dotnet run --no-build
start dotnet run --no-build

cd..
start nginx\nginx.exe

pause
