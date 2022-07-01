using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CatApi.Model;

namespace CatApi.Services
{
    public class RequestManager
    {
        IRestService restService;
        public RequestManager(IRestService service)
        {
            restService = service;
        }

        public Task<List<CatModel>> GetCats()
        {
            return restService.GetCatsAsync();
        }

        public Task DeleteCatAsync(CatModel cat)
        {
            return restService.DeleteCatAsync(cat);
        }

        public Task SaveCatAsync(CatModel cat, bool isNewCat = false)
        {
            return restService.SaveCatAsync(cat, isNewCat);
        }
    }
}
