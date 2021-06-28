cd..
cd nginx
start nginx.exe

cd ..
cd Valuator
dotnet build
start dotnet run --no-build --urls "http://localhost:5001/"
start dotnet run --no-build --urls "http://localhost:5002/"

