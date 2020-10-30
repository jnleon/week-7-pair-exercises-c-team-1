using CatCards.Models;
using RestSharp;

namespace CatCards.Services
{
    public class CatFactService : ICatFactService
    {
        static readonly RestClient client = new RestClient();
        public CatFact GetFact()
        {
            RestRequest request = new RestRequest("https://cat-fact.herokuapp.com/facts/random");
            IRestResponse<CatFact> response = client.Get<CatFact>(request);
            return response.Data;
        }
    }
}
