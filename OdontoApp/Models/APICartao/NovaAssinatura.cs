
namespace OdontoApp.Models.APICartao
{
    public class NovaAssinatura
    {
        public string api_key { get; set; }
        public string card_hash { get; set; }
        public Customer customer { get; set; }
        public Phone phone { get; set; }
        public string payment_method { get; set; }
        public string plan_id { get; set; }
        public string postback_url { get; set; }
    }
}
