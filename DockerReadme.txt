Certification: 
	from user root directory run:
	dotnet dev-certs https -ep .aspnet\https\aspnetapp.pfx -p Password
	dotnet dev-certs https --trust

	Save the password in a .env file, include the file in the solution's gitignore file.
	import the env file in the compose under the image name like so: 
	imagename:
		env_file:
        - .env
	then use the environment variable like so : 
	environment:
		ASPNETCORE_Kestrel__Certificates__Default__Password: ${KEY_NAME_IN_ENV_FILE}

Ports: 
	Dockerfiles expose their ports within the network, docker compose port mapping reveals them outside as well

Aspnet core docker images use port 80 as the default access point. To overwrite change the environment variable ASPNETCORE_URLS: "http://+:portnum" to a new port
Via: https://stackoverflow.com/questions/48669548/why-does-aspnet-core-start-on-port-80-from-within-docker

Always Run docker build commands from root directory to enable context access to dependent projects, example command:

docker build -t imagename -f Directory/Dockerfile .