# 📀 IMDb

The Internet Music Database, "IMDb" is a music reveiwing platform. This project is an enterprise backend service that uses an azure-hosted MySql database. 

**📓 For common tasks such as testing or running the app in dev mode, use the bash scripts in the'scripts' folder.**

## 🔧 Setup

### Getting started

1) Clone the repo: `git clone https://github.com/vaughnman/CST391-IMDb.git`
2) Download dependencies: `dotnet restore`

### Host App Locally

In a bash terminal, run `sh scripts/dev.sh`. In the console output, you should see "Using Stub Database", meaning the server is running off of an in-memory database.

### Use Live Database

To use the live database, first add your ip to the firewall whitelist at [this page](
https://portal.azure.com/#@mygcuedu6961.onmicrosoft.com/resource/subscriptions/2eeb0447-ad23-40ef-9ab1-dc2772eff1fb/resourceGroups/IMDb/providers/Microsoft.DBforMySQL/flexibleServers/imdb-database/networking). 

Next, add the mysql server's database connection string to your .env file:
```env
DATABASE_CONNECTION_STRING="server=imdb-database.mysql.database.azure.com;uid=VBauer1;database=imdb;password=<PASSWORD HERE>;"
```

Run `sh scripts/dev.sh` again, and you should see that the console now outputs "Using Live Database".

## 🧑‍🔬 Testing

### Unit

If you haven't already, run `dotnet restore` in the Tests directory. 

Next, run the test script, `sh scripts/test.sh`.  

**The AlbumDatabase and ReviewDatabase tests will fail if the live database is offline or not configured properly.**

## Postman

In postman click import on the collections panel. Navigate to this project's Postman folder, and import each collection file.

Go into postman settings, go to `File > Settings` and ensure that SSL Certificate Verification is disabled if you want to test locally.

You will need to create an environment variable in postman called 'url':

```
Local url: https://localhost:7037.
Azure url: https://internetmusicdatabase.azurewebsites.net
```

To run the tests, it is recommended to use the collection runner as the collections clean up after themselves. Click on a collection and click the 'Run' button.