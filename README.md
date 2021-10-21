# homework-4-team-1-hepsiorada

# Hepsiorada Api

Veritabanında users, products, orders, orderdetails tabloları bulunmaktadır.
Order endpointine POST ile istek atılırken order ve orderdetails listesi, JSON formatında verilir. 
Verilen order ve orderdetails, öncelikle ilişkisel veritabanına yazılır. 
Bu işlemin hemen ardından bir ordersummary nesnesi mongoDb'ye yazılır. 
Order endpointine GET isteği geldiğinde mongoDb'deki ordersummary'lerin bir listesi geri dönülülür.

#### SQL Database Diagram

> <img src="https://github.com/burcMnt/HepsioradaAPI-HepsiBurada-HW-4/blob/master/Hepsiorada.Api/Images/Adsız.png"/>

#### Swagger

> <img src="https://github.com/burcMnt/HepsioradaAPI-HepsiBurada-HW-4/blob/master/Hepsiorada.Api/Images/Swagger.png"/>

> <img src="https://github.com/burcMnt/HepsioradaAPI-HepsiBurada-HW-4/blob/master/Hepsiorada.Api/Images/Swagger2.png"/>

#### MongoDb

> <img src="https://github.com/burcMnt/HepsioradaAPI-HepsiBurada-HW-4/blob/master/Hepsiorada.Api/Images/MongoDbpng.png"/>

