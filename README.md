

# Drones (Drug Delivery)
To develop the solution we used **Clean Architecture**. Some patterns used are:
 - Specification pattern
 - Repository pattern
 - Minimal api  

The resources exposed through REST API are:
1. api/authenticate (POST)
2. api/drones (POST)
3. api/drones/{droneId}/loading (POST)
4. api/drones/available-for-loading (GET)
5. api/drones/{droneId}/battery-level (GET)
6. api/drones/{droneId} (GET)
7. api/drones/{droneId}/loaded-medications (GET)

Only for the cases of (2) and (3) it is necessary to authenticate and obtain the **Bearer** token. The administrator user is *admin@drugdelivery.com* and the password is *Pa$W0rd*.
The periodic task runs every 30 seconds using a custom attribute defined in *DrugDelivery.Infrastructure.Jobs.CronScheduleAttribute.cs*

# Build and Run
Open a command prompt in the solution folder and execute the following command
> dotnet run --project src/DrugDelivery.HttpApi

# Test
Open a command prompt in the solution folder and execute the following command
> dotnet test tests/DrugDelivery.UnitTests/DrugDelivery.UnitTests.csproj