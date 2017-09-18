REM
REM file for creating dockerimages and run
docker rm containerda --force
docker rm postgres-server -- force
docker rmi -f dbpostgres:latest
docker rmi -f image containerda-img:latest
docker build -t dbpostgres .
docker build -t containerda-img .
REM docker run -p 5000:80 --name containerda containerda-img
REM docker run -p 5000:80  --add-host=parent-host:10.0.75.1 --name containerda containerda-img
docker run -p 5000:80 --network="bridge" --name containerda containerda-img
REM docker run -v d:/venkatesh/aspnetcore/2.0/containerdasample/publish/containerapp/keys:/root/.aspnet/dataprotection-keys -p 5000:80 --name containerda containerda-img
REM docker run -d -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=damienbod --name postgres-server -p 5600:5432 -v pgdata:/var/lib/postgresql/data --restart=always postgres
docker run --name postgres-server -p 5600:5432 -v pgdata:/var/lib/postgresql/data --restart=always postgres


