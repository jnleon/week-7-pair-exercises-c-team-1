using CatCards.Models;
using RestSharp;

namespace CatCards.Services
{
    public class CatPicService : ICatPicService
    {
        static readonly RestClient client = new RestClient();
        public CatPic GetPic()
        {
            RestRequest request = new RestRequest("https://random-cat-image.herokuapp.com/");
            IRestResponse<CatPic> response = client.Get<CatPic>(request);
            return response.Data;
        }
    }
}
