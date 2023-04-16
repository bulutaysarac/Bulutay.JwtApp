using Bulutay.JwtApp.Front.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Bulutay.JwtApp.Front.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> List()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "AccessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync("http://localhost:5220/api/products");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<List<ProductListModel>>(jsonData, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    return View(result);
                }
            }
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "AccessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync("http://localhost:5220/api/categories");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<List<CategoryListModel>>(jsonData, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    return View(new ProductCreateModel()
                    {
                        CategoryList = result
                    });
                }
            }
            return RedirectToAction("List", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateModel model)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "AccessToken")?.Value;
            var content = new StringContent(JsonSerializer.Serialize<ProductCreateModel>(model), Encoding.UTF8, "application/json");
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.PostAsync("http://localhost:5220/api/products", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("List", "Product");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Remove(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "AccessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                await client.DeleteAsync($"http://localhost:5220/api/products/{id}");
            }
            return RedirectToAction("List", "Product");
        }

        public async Task<IActionResult> Update(int id)
        {
            ProductUpdateModel model = new ProductUpdateModel();
            var token = User.Claims.FirstOrDefault(x => x.Type == "AccessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync("http://localhost:5220/api/categories");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<List<CategoryListModel>>(jsonData, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    model.CategoryList = result;
                }
                var productResponse = await client.GetAsync($"http://localhost:5220/api/products/{id}");
                if (productResponse.IsSuccessStatusCode)
                {
                    var jsonData = await productResponse.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ProductListModel>(jsonData, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    model.Name = result.Name;
                    model.Stock = result.Stock;
                    model.Price = result.Price;
                    model.CategoryId = result.CategoryId;
                    model.Id = result.Id;
                }
                return View(model);
            }
            return RedirectToAction("List", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateModel model)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "AccessToken")?.Value;
            var content = new StringContent(JsonSerializer.Serialize<ProductUpdateModel>(model), Encoding.UTF8, "application/json");
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.PutAsync("http://localhost:5220/api/products", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("List", "Product");
                }
            }
            return View(model);
        }
    }
}
