RapidPay API
------------

Requirements:

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
* Balance will always be positive and decreasing on each payment.
* At the moment there is no possibe way to add more money to the already created Card (amount on Pay method must be greater than zero)
* There is no relation between a user an its cards (Authorization), so Every logged in user can GetBalance, Create or pay using any card.
* It doesn't say if the fee is flat/fixed or if its a percentage. I will assume it's flat.
* I wil limit myself to deliver what was being requested but there are a lot of stuff I would change to this solution like separation of concerns adding a data layer (another project file), maybe adding unit Tests and so on.
* There is a flaw on the requirement regarding fee calculation or maybe I missunderstood it. but as it was read, by multiplying the actual fee multiple times (a bunch of hours) for a random decimal between 0 and 2 it will Always lead to Zero)

Usage:
* Clone or download the Repo.
* Build and run the Solution
* Hit the following endpoints using postman or any other REST client

Endpoints:
Create:  	POST:  https://localhost:7124/CreditCard/
Pay:   	  	PUT:	https://localhost:7124/CreditCard/{cardNumber}
GetBalance: GET:	https://localhost:7124/CreditCard/{cardNumber}


User: Jose
Pass: Enser


Postman collection (version 2 and 2.1 are free to be donwloaded on the root of the repo.