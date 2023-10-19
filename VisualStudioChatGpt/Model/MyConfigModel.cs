namespace Vs_ChatGpt.Model
{

    public class MyConfigModel
    {
        /// <summary>
        /// Service Name: OpenAI/Azure Cloud
        /// </summary>
        public string serviceName { get; set; }

        /// <summary>
        /// API URL
        /// </summary>
        public string apiurl { get; set; }

        /// <summary>
        /// API Key
        /// </summary>
        public string apikey { get; set; }

        /// <summary>
        /// Proxy Address
        /// For Azure Cloud, you can just delete it instead of adding
        /// </summary>
        public string proxy { get; set; }

        /// <summary>
        /// Maximum Tokens
        /// Generally, 500 should be sufficient
        /// </summary>
        public string maxtoken { get; set; }

        /// <summary>
        /// Temperature Randomness
        /// Usually set to 0, the smaller the stricter. Recommended not to exceed 0.3
        /// </summary>
        public string temperature { get; set; }

        /// <summary>
        /// Conversation Model
        /// </summary>
        public string model { get; set; }

        /// <summary>
        /// Default Timeout Time
        /// Generally, 30 seconds should be enough, unless you have a particularly large piece of code
        /// </summary>
        public string timeout { get; set; }
    }
}