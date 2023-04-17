
Requirements:
-------------

The card management module includes three API endpoints: 
* Create card (card format is 15 digits) 
* Pay (using the created card, and update balance) 
* Get card balance 

The payment fees module is calculating the payment fee for each payment. 
* Every hour, the Universal Fees Exchange (UFE) randomly selects a decimal between 0 and 2. 
* The new fee price is the last fee amount multiplied by the recent random decimal. 
* Develop a Singleton to simulate the UFE service and the fee should be applied to every payment 

Bonus:
* Improve your API performance and throughput using multithreading. 
* Improve the authentication so we can make our Authorization system secure. 
* Make the shared resources thread safe
  - using a design pattern in case you are storing the data in the memory. 
  - improving the database design and the usage of the ORM framework in case you are using a database to persist the cards and transaction.


Assumptions:
------------
* Balance will always be positive and decreasing on each payment.
* At the moment there is no possible way to add more money to the already created Card (amount on Pay method must be greater than zero)
* There is no relation between a user an its cards (Authorization), so Every logged in user can GetBalance, Create or pay using any card.
* It doesn't say if the fee is flat/fixed or if its a percentage. I will assume it's flat.
* There is a flaw on the requirement regarding fee calculation or maybe I missunderstood it. but as I read it, by multiplying the actual fee multiple times (after a couple of hours) for a random decimal between 0 and 2 it will Always lead to Zero because numbers like this 0.001 have a lot of influence against the major random number that is just a 2, but I didnt want to provide any own intelligence to the logic because it wasn't requested)


Usage:
------
* Clone or download the Repo.
* I've transfered all hardcoded stuff to a file named ***Constants.cs***. So, if you want to change any parameter or configuration go there and set for example the UFE schedule to 5 seconds so you can see it in action without any absurd delay on testing side.
* Build Solution
* Run Migrations:
  a)running **Update-Database**  from Visual Studio Package Manager Console or
  b)running **dotnet ef database update** command on a console prompt under your project's directory (it requires to have **dotnet** previously installed)
* Run Solution
* Hit the following endpoints using postman or any other REST client

Endpoints:
----------
Create:  	POST:  https://localhost:7124/CreditCard/				// Sample Body Payload: { "CardNumber" : "123456789012345", "Balance": 1000 }

Pay:   	  	POST:  https://localhost:7124/CreditCard/				// Sample Body Payload: { "CardNumber" : "123456789012345", "Amount": 10.24 }

GetBalance: GET:   https://localhost:7124/CreditCard/{cardNumber}


User: Jose
Pass: Enser

Just For convinience... 
Postman collections (version 2 and 2.1) are free to be donwloaded on the root of the repo.
