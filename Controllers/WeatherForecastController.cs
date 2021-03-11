using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lovepdf.Models;
using lovepdf.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace lovepdf.Controllers
{
    /*
     * 使用 ApiController 默认支持以下特性
     * 1. 属性路由要求
     * 2. 自动 HTTP 400 响应
     * 3. 绑定源参数推理
     * 4. Multipart/form-data 请求推理
     * 5. 错误状态代码的问题详细信息
     *
     * 这里通过定义 ApiControllerBase 基类来自动应用到派生出的控制器。
     */   
   
    public class WeatherForecastController : ApiControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IWeatherForecastService _forecastService;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMemoryCache _cache;
        public WeatherForecastController(
            IWeatherForecastService forecastService,
            ILogger<WeatherForecastController> logger,
            IMemoryCache memoryCache
            )
        {
            _forecastService = forecastService;
            _logger = logger;
            _cache = memoryCache;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var result = _forecastService.GetWeatherForecasts();
            return result;
        }
    }
}
