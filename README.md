# HTTPModulesAndHandlers


### Tasks

> Написать логику по добавлению кастомных заголовков с общим временем работы запроса и IP адресом пользователя

**URLs to test:**

http://localhost:50889/api/values

http://localhost:50889/api/values/5

<br/>

> Написать логику по кэшированию картинки из папки в проекте и возвращать 304, если картинка на диске не была изменена. Иначе возвращать новую картинку

**Test HTML-page:** ImageCacheTest.html

**URL to test:**

http://localhost:50889/cacheImages/diagram.jpg (any image from CacheImages folder can be used)

![](img/Mozilla%20Firefox.jpg)

<br/>

> Написать логику по автоматическому обрезанию картинок в зависимости от query параметров, которые написаны в query string

**Test HTML-page:** HtmlTest.html

**URL to test:**

http://localhost:63497/images/flower.jpg?size=20 (any image from Images folder can be used)
