using Bulutay.JwtApp.Front.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Bulutay.JwtApp.Front.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> List()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "AccessToken")?.Value;
            if(token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync("http://localhost:5220/api/categories");

                if(response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<List<CategoryListModel>>(jsonData, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    return View(result);
                }
            }
            return View();
        }

        public async Task<IActionResult> Remove(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "AccessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                await client.DeleteAsync($"http://localhost:5220/api/categories/{id}");
            }
            return RedirectToAction("List", "Category");
        }

        public IActionResult Create()
        { 
            return View(new CategoryCreateModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateModel model)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "AccessToken")?.Value;
            var content = new StringContent(JsonSerializer.Serialize<CategoryCreateModel>(model), Encoding.UTF8, "application/json");
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.PostAsync("http://localhost:5220/api/categories", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("List", "Category");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            CategoryUpdateModel model = new CategoryUpdateModel();
            var token = User.Claims.FirstOrDefault(x => x.Type == "AccessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync($"http://localhost:5220/api/categories/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<CategoryListModel>(jsonData, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    model.Id = result.Id;
                    model.Definition = result.Definition;
                    return View(model);
                }
            }
            return RedirectToAction("List", "Category");
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateModel model)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "AccessToken")?.Value;
            var content = new StringContent(JsonSerializer.Serialize<CategoryUpdateModel>(model), Encoding.UTF8, "application/json");
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.PutAsync("http://localhost:5220/api/categories", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("List", "Category");
                }
            }
            return View(model);
        }
    }
}
