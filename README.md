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
