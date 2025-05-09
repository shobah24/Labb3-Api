
                                                  Uppgift:
------------------------------------------------------------------------------------------------------------
1. Hämta alla personer i systemet:

-Request url: https://localhost:7112/api/Person

Response:
[
  {
    "id": 1,
    "firstName": "Shokran",
    "lastName": "Bahram",
    "age": 21,
    "phoneNumber": "0734567890"
  },
  {
    "id": 2,
    "firstName": "Jay",
    "lastName": "Loe",
    "age": 34,
    "phoneNumber": "0737654321"
  },
  {
    "id": 3,
    "firstName": "John",
    "lastName": "Doe",
    "age": 25,
    "phoneNumber": "0731234567"
  }
----------------------------------------------------------------------------------------------------------------------


2. Hämta alla intressen kopplade till en specifik person:

-Request url:https://localhost:7112/api/Person/1/interests

-Response:{
  "firstName": "Shokran",
  "lastName": "Bahram",
  "age": 21,
  "interests": [
    {
      "name": "Traveling",
      "description": "Exploring new places and cultures"
    }
  ]
------------------------------------------------------------------------------------------------------------
3. Hämta alla länkar kopplade till en specifik person:

-Request url: https://localhost:7112/api/Person/1/links

-Response: {
  "firstName": "Shokran",
  "lastName": "Bahram",
  "age": 21,
  "links": [
    {
      "url": "https://www.bucketlisttravels.com/round-up/100-bucket-list-destinations"
    },
    {
      "url": "https://www.youtube.com/"
    }

------------------------------------------------------------------------------------------------------------
4. Koppla en person till ett nytt intresse:

-Request url: https://localhost:7112/api/Person/3/interests/3

-Response: {
  "message": "Person med idt 3 är nu kopplad till intresse med idt 3!"}

------------------------------------------------------------------------------------------------------------
5. Lägga till nya länkar för en specifik person och ett specifikt intresse:

-Request url:https://localhost:7112/api/Person/1/interests/3/links

-Response:{
  "id": 6,
  "url": "https://www.travelmarket.se/",
  "interestId": 3,
  "personId": 1,
  "personInterest": null
}
