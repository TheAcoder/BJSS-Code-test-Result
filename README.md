BJSS-Code-test-Result
=====================
PriceBasket. This is the solution for the BJSS technical test for software developer. The problem can be found in ping2ravi/bjss
In any case, if you land here, I would assume that you already know the problem to solve.
=========
A console basket with TDD approach
Approach

First I wrote the entities.

Price entity has the product Id, line price and quantity. In reality, there could be three entities, one each for Product, Price and Basket. But for this exercise this will do.
Then there is Promotion entity, which has the promotion details including the promotion type and the Buckets. The buckets really define what consists the promotion and the discount.

Next comes the repositories. One each to get Price and Promotion data. In real life this will come from a data store. I have mocked it for this. Repositories are created using repository pattern.

Next comes Basketservice. Basket takes concrete implementations of PriceRepository and PromotionRepository.

public BasketService(IPriceRepository priceRepository, IPromotionRepository promoRepository)

It has two public methods for setting the basket and calculating promotion.

This is the point I started writting Unit tests for basketservice before writting the implementation details for basket service.
First thing is to check Setbasket for null and zero items, and then create two mocks for Price and Promotion Repositories to test other secnarios.
From that point on, it's all test, code, test, refactor, test. I have also included the testresults folder.

Once basket service is done, I set about writting the method for passing the input and writting down the output which takes an implementation of writer and repositories. The Writer is a consolewriter in the real application, and a mock writer from test which is testing whether it's being called 3 times.
Any question, please do come back to me.
