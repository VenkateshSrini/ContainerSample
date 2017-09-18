FROM microsoft/aspnetcore:2.0

# Set Environment variables

# ENV ASPNETCORE_URLS="http://*:5000"


#It is assumed that the dockerfile is in published dirctory and henc 

# we just need to copy the current directory content to docker

COPY . /app
WORKDIR /app
EXPOSE 80
EXPOSE 5432
ENTRYPOINT ["dotnet", "ContainerDASample.dll"]
