using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CatApi.Model;

namespace CatApi.Services
{
    public interface IRestService
    {
        Task<List<CatModel>> GetCatsAsync();
        Task SaveCatAsync(CatModel cat, bool isNewItem);
        Task DeleteCatAsync(CatModel cat);
    }
}
