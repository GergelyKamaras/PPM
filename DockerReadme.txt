Certification: 
from user root directory run:
dotnet dev-certs https -ep .aspnet\https\aspnetapp.pfx -p Password
dotnet dev-certs https --trust

Always Run docker build commands from root directory to enable context access to dependent projects, example command:

docker build -t imagename -f Directory/Dockerfile .