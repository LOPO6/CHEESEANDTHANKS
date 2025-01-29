using Newtonsoft.Json;

namespace CheeseAndThankYou
{
    public static class SessionExtensions
    {
        //session extension lib for complex session vars: https://www.talkingdotnet.com/store-complex-objects-in-asp-net-core-session/ 
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
