# PEAR
A Conversational Chatbot built on the Microsoft Bot Framework which provides 9 different functionalities:

* To get the weather for a city: _whats it like in city_name?_
* To get a brief summary for a particular movie or series: _tell me about series_name / movie_name_
* To check the pnr status: _check pnr status for pnr_no_
* To get the headlines from leading news channels: _get me the headlines from news_channel_name. Note: "The Hindu" should be given as "the-hindu"_
* To send a demo email: _send mail to email_id. Body:body_of_text._
* To check the cricket matches for today: _get me today's matches_
* To check a list of restaurants in a particular city: _get me a list of restaurants in city_name_
* To search for a particular topic: _search for query_
* To search video for a particular topic: _search video for query_

## Disclaimers
* The Bot is completely dependent on various publicly APIs for providing the services mentioned above. If anything were to happen to the APIs (such as changes in the JSON format being returned, or a previously public API becoming paid) that would render the Bot unable to access them, then one or more of the above functionalities would become defunct. The same disclaimer applies to Microsoft's LUIS (Language Understanding Intelligent System) which is a free service as of 29/01/2017.
* You will need to train your own LUIS app in order for this chatbot to function properly.

## The list of APIs used by this bot
* [Open Weather Map API](http://openweathermap.org/)
* [Open Movie Database API](https://www.omdbapi.com/)
* [Railway API](http://railwayapi.com/)
* [News API](https://newsapi.org/)
* [Cricscore API](http://cricscore-api.appspot.com/)
* [Zomato Developers API](https://developers.zomato.com/)
* [Bing Search API](https://azure.microsoft.com/en-in/services/cognitive-services/search/)
