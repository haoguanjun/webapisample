using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
