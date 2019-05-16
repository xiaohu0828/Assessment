# Introduction 
Blue Harvest Backend Code Assessment

The assessment consists of an API to be used for opening secondary current accounts of already existent customers.

The API will expose an endpoint which accepts the user information (customerID, initialCredit).

Once the endpoint is called, a new account will be opened connected to the user whose ID is customerID.

Also, if initialCredit is not 0, a transaction will be sent to the new account.

Another Endpoint will output the user information showing Name, Surname, balance, and transactions of the accounts.

Accounts and Transactions are different services.

# Solution
Using Azure Function to create two service 
    - Assessment.Accounts.Service
    - Assessment.Transactions.Service

# Getting Started

API references

     CreateAccount: [POST] http://localhost:5860/api/v1/accounts

     GetCustomerInfoById: [GET] http://localhost:5860/api/v1/customers/{id}

     CreateTransaction: [POST] http://localhost:7010/api/v1/transactions

     GetTransactionsByAccountId: [GET] http://localhost:7010/api/v1/transactions/{accountId}

# Build and Test
Build 
    1 Visual studio 2019. 
    2 Rebuild the whole solution
    3 Debug solution with Azure Simulator: Running two Azure Functions at the same time.

TEST
    1 Using Rest Client in Visual Studio Code
        Open file: tests\example calls\local.http
        Click each API requests
    2 Using PostMan 
        Import file: tests\example calls\Assess.postman_collection.json to PostMan
        Run four APIs in PostMan

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://www.visualstudio.com/en-us/docs/git/create-a-readme). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
