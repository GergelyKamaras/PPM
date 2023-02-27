Always Run docker build commands from root directory to enable context access to dependent projects, example command:

docker build -t imagename -f Directory/Dockerfile .