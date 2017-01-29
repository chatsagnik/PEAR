using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace PEAR
{
    public class ProcessReply
    {
        public string replyToWeather(string city)
        {
            WebRequest req2 = WebRequest.Create($"http://api.openweathermap.org/data/2.5/weather?q={city}&APPID=your-app-id");
            WebResponse res2 = req2.GetResponse();
            StreamReader rd2 = new StreamReader(res2.GetResponseStream());
            string s2 = rd2.ReadToEnd();
            RootObjectWeather rweather = JsonConvert.DeserializeObject<RootObjectWeather>(s2);
            string result = "The best description of the weather would be " + rweather.weather[0].description + ". The maximum and minimum temperatures are " + ((rweather.main.temp_max) - 273.15) + " degree Celsius, and" + ((rweather.main.temp_min) - 273.15) + " degree Celsius.";
            Debug.WriteLine(result);
            return result;
        }
        public string replyToMovie(string movieName)
        {
            movieName = movieName.Replace(" ", "+");
            WebRequest req2 = WebRequest.Create($"http://www.omdbapi.com/?t={movieName}");
            WebResponse res2 = req2.GetResponse();
            StreamReader rd2 = new StreamReader(res2.GetResponseStream());
            string s2 = rd2.ReadToEnd();
            RootObjectMovie rmovie = JsonConvert.DeserializeObject<RootObjectMovie>(s2);
            string result = "The plot of the " + rmovie.Type + " is as follows: " + rmovie.Plot + " It was directed by " + rmovie.Director + " and the cast includes the following stars: " + rmovie.Actors + ". The IMDb rating is:" + rmovie.imdbRating + ". ";
            double value;
            double.TryParse(rmovie.imdbRating, out value);
            if (value < 8.0)
            {
                result = result + "Since the rating is low, watch it at your own risk.";
            }
            else
            {
                result = result + "You might try watching it. It seems like a good " + rmovie.Type + ".";
            }
            Debug.WriteLine(result);
            return result;
        }
        public string replyToPnrStatus(string pnrno)
        {
            WebRequest req2 = WebRequest.Create($"http://api.railwayapi.com/pnr_status/pnr/{pnrno}/apikey/your-api-key/");
            WebResponse res2 = req2.GetResponse();
            StreamReader rd2 = new StreamReader(res2.GetResponseStream());
            string s2 = rd2.ReadToEnd();
            Debug.WriteLine(s2);
            RootObjectPNR rpnr = JsonConvert.DeserializeObject<RootObjectPNR>(s2);
            int status = rpnr.response_code;
            if (status == 204) {
                string s = "The IRCTC Website is experiencing heavy traffic. Please try after some time.";
                return s;
            }
            string result = "Your train name is "+rpnr.train_name+". Your destination is "+rpnr.to_station.name+". The Passenger Status are given as follows: "+Environment.NewLine;
            for (var i = 0; i < rpnr.passengers.Count; i++)
            {
                result = result + "Passenger number"+(i+1)+" "+rpnr.passengers[i].current_status+Environment.NewLine;
            }
            return result;
        }
        public string replyToGreeting()
        {
            TimeSpan morningstart = new TimeSpan(6, 0, 0); //6 am
            TimeSpan morningend = new TimeSpan(11, 59, 0); //11.59 am
            TimeSpan afternoonstart = new TimeSpan(12, 0, 0); //12 pm
            TimeSpan afternoonend = new TimeSpan(16, 59, 0); //4.59 pm
            TimeSpan eveningstart = new TimeSpan(17, 0, 0); //5 pm
            TimeSpan eveningend = new TimeSpan(23, 59, 0); //11.59 pm
            TimeSpan now = DateTime.Now.TimeOfDay;
            if (now <= morningend && now >= morningstart) {
                string result = "Good Morning! How may I be of service to you? Type \"help\" if you need it :)";
                return result;
            }
            else if (now <= afternoonend && now >= afternoonstart)
            {
                string result = "Good Afternoon! How may I be of service to you? Type \"help\" if you need it :)";
                return result;
            }
            else if (now <= eveningend && now >= eveningstart)
            {
                string result = "Good Evening! How may I be of service to you? Type \"help\" if you need it :)";
                return result;
            }
            else {
                string result = "Behold the Witchin'hours! How may I be of service to you? Type \"help\" if you need it :)";
                return result;
            }
        }
        public string replyDefault()
        {
            string result = "I am sorry but I could not understand your query. It sounded kinda funny tbh :p Type help if you need it!";
            return result;
        }

        public string replyToHelp()
        {
            StringBuilder sb = new StringBuilder();
            string reply = "Here are some of the functionalities I provide and some of the formats I am most comfortable in: ";
            sb.AppendLine(reply);
            sb.AppendLine();
            reply = "1. To get the weather for a city: whats it like in <city name>?";
            sb.AppendLine(reply);
            sb.AppendLine();
            reply = "2. To get a brief summary for a particular movie or series: tell me about <series name / movie name>";
            sb.AppendLine(reply);
            sb.AppendLine();
            reply = "3. To check the pnr status: check pnr status for <pnr_no>";
            sb.AppendLine(reply);
            sb.AppendLine();
            reply = "4. To get the headlines from leading news channels: get me the headlines from <news channel name>. Note: \"The Hindu\" should be given as \"the-hindu\"";
            sb.AppendLine(reply);
            sb.AppendLine();
            reply = "5. To send a demo email: send mail to <email id>. Body:<body of text>.";
            sb.AppendLine(reply);
            sb.AppendLine();
            reply = "6. To check the cricket matches for today: get me today's matches";
            sb.AppendLine(reply);
            sb.AppendLine();
            reply = "7. To check a list of restaurants in a particular city: get me a list of restaurants in <city name>";
            sb.AppendLine(reply);
            sb.AppendLine();
            reply = "8. To search for a particular topic: search for <query>";
            sb.AppendLine(reply);
            sb.AppendLine();
            reply = "9. To search video for a particular topic: search video for <query>";
            sb.AppendLine(reply);
            sb.AppendLine();
            reply = sb.ToString();
            return reply;
        }

        public void replyToMail(string toAdd,string body)
        {
            Debug.WriteLine("1");
            Email e = new Email();
            string sub = "This is a message from PEAR";
            e.getContent(toAdd, body, sub);
        }

        public string replyToCricketMatches()
        {
            string ul = string.Empty;
            ul += "http://cricscore-api.appspot.com/csa";
            WebRequest request = WebRequest.Create(ul);
            WebResponse respoe = request.GetResponse();
            Stream dataStream = respoe.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            StringBuilder sb = new StringBuilder();
            string ss = "These are the matches for today\n";
            sb.AppendLine(ss);
            sb.AppendLine();
            dynamic blogPosts = JArray.Parse(responseFromServer);
            for (int i = 0; i < blogPosts.Count; i++)
            {
                ss = (i+1)+". "+blogPosts[i].t2 + " vs " + blogPosts[i].t1;
                sb.AppendLine(ss);
                sb.AppendLine();
            }
            string res = string.Empty;
            res = sb.ToString();
            return res;
        }

        public string replyToNews(string newschannel)
        {
            Debug.WriteLine(newschannel);
            newschannel = newschannel.Replace(" ","");
            WebRequest req = WebRequest.Create($"https://newsapi.org/v1/articles?source={newschannel}&sortBy=top&apiKey=your-api-key");
            WebResponse res = req.GetResponse();
            StreamReader rd = new StreamReader(res.GetResponseStream());
            string s1 = rd.ReadToEnd();
            RootObjectNews rnews = JsonConvert.DeserializeObject<RootObjectNews>(s1);
            StringBuilder sb = new StringBuilder();
            string reply = "The headlines from "+newschannel+" are: ";
            sb.AppendLine(reply);
            sb.AppendLine();
            for (var i = 0; i < rnews.articles.Count; i++)
            {
                reply = rnews.articles[i].description;
                sb.AppendLine(reply);
                sb.AppendLine();
                sb.AppendLine();
            }
            reply = sb.ToString();
            return reply;
        }

        public string replyToQuery(string query)
        {
            WebRequest request = WebRequest.Create($"https://api.cognitive.microsoft.com/bing/v5.0/search?q={query}");
            request.Method = "GET";
            request.Headers.Add("Ocp-Apim-Subscription-Key", "your_subscription_key");
            request.ContentType = "application/json";
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string s1 = reader.ReadToEnd();
            RootObjectQuery rq = JsonConvert.DeserializeObject<RootObjectQuery>(s1);
            StringBuilder sb = new StringBuilder();
            string reply = "Some of the associated links in response to the query: " + query + " are: ";
            sb.AppendLine(reply);
            sb.AppendLine();

            for (var i = 0; i < 4; i++)
            {
                reply = (i+1)+". "+rq.relatedSearches.value[i].text + "  ,link: " + rq.relatedSearches.value[i].webSearchUrl;
                sb.AppendLine(reply);
                sb.AppendLine();
            }
            reply = sb.ToString();
            return reply;
        }

        public string replyToVideoQuery(string query)
        {
            WebRequest request = WebRequest.Create($"https://api.cognitive.microsoft.com/bing/v5.0/search?q={query}");
            request.Method = "GET";
            request.Headers.Add("Ocp-Apim-Subscription-Key", "your_subscription_key");
            request.ContentType = "application/json";
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string s1 = reader.ReadToEnd();
            RootObjectQuery rq = JsonConvert.DeserializeObject<RootObjectQuery>(s1);
            StringBuilder sb = new StringBuilder();
            string reply = "Some of the associated links in response to the query: " + query + " are: ";
            sb.AppendLine(reply);
            sb.AppendLine();

            for (var i = 0; i < 4; i++)
            {
                reply = (i + 1) + ". " + rq.videos.value[i].name + "  ,link: " + rq.videos.value[i].webSearchUrl;
                sb.AppendLine(reply);
                sb.AppendLine();
            }
            reply = sb.ToString();
            return reply;
        }

        public string replyToRest(string cityname)
        {
            WebRequest request = WebRequest.Create($"https://developers.zomato.com/api/v2.1/search?entity_type=city&q={cityname}&sort=rating&order=desc");
            request.Method = "GET";
            request.Headers.Add("user-key", "your_user_key");
            request.ContentType = "application/json";
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string s1 = reader.ReadToEnd();
            RootObjectRest rrest = JsonConvert.DeserializeObject<RootObjectRest>(s1);
            StringBuilder sb = new StringBuilder();
            string reply = "The top restaurants in " + cityname + " are: ";
            sb.AppendLine(reply);
            sb.AppendLine();
            int max = (rrest.restaurants.Count < 10) ? rrest.restaurants.Count : 10;
            for (var i = 0; i < max; i++)
            {
                reply = (i+1)+"."+ rrest.restaurants[i].restaurant.name + ", " + rrest.restaurants[i].restaurant.location.address;
                sb.AppendLine(reply);
                sb.AppendLine();
                sb.AppendLine();
            }
            reply = sb.ToString();
            return reply;
        }

        public string getReply(string s)
        {
            if (s.ToLower().Equals("help"))
                return replyToHelp();
            string s1;
            WebRequest req = WebRequest.Create($"https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/{your_app_id}?subscription-key={your_subscriptionzkey}&q={s}&verbose=true");
            WebResponse res = req.GetResponse();
            StreamReader rd = new StreamReader(res.GetResponseStream());
            s1 = rd.ReadToEnd();
            RootObjectLuis rluis = JsonConvert.DeserializeObject<RootObjectLuis>(s1);
            string intent1 = rluis.intents[0].intent;

            if ((rluis.intents[0].intent.Equals("Greeting")))
                return replyToGreeting();

            else if ((rluis.intents[0].intent.Equals("getCricketDetails")))
                return replyToCricketMatches();

            else if ((rluis.intents[0].intent.Equals("sendMail"))) {
                if (rluis.entities[0].type.Equals("builtin.email"))
                    replyToMail(rluis.entities[0].entity, rluis.entities[1].entity);
                else
                    replyToMail(rluis.entities[1].entity, rluis.entities[0].entity);
                Debug.WriteLine("1");
                return "Message Sent";
            }

            else if ((rluis.intents[0].intent.Equals("None")))
                return replyDefault();

            string entity1 = rluis.entities[0].entity;
            if (intent1.Equals("getWeather"))
                return replyToWeather(entity1);

            else if (intent1.Equals("getMovie"))
                return replyToMovie(entity1);

            else if ((rluis.intents[0].intent.Equals("getPnrStatus")))
                return replyToPnrStatus(entity1);

            else if ((rluis.intents[0].intent.Equals("getNews")))
                return replyToNews(entity1);

            else if ((rluis.intents[0].intent.Equals("getRestaurants")))
                return replyToRest(entity1);

            else if ((rluis.intents[0].intent.Equals("getQuery")))
                return replyToQuery(entity1);

            else if ((rluis.intents[0].intent.Equals("getVideoQuery")))
                return replyToVideoQuery(entity1);

            else
                return replyDefault();
        }
    }
}