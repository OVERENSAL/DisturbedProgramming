cd..
cd nginx
start nginx.exe

cd..
cd Valuator
dotnet build
start dotnet run --no-build --urls "http://localhost:5001/"
start dotnet run --no-build --urls "http://localhost:5002/"

cd ..
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
