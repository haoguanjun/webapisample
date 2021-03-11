using System.Collections.Generic;
using lovepdf.Models;

namespace lovepdf.Services {
     public interface IWeatherForecastService {
         IEnumerable<WeatherForecast> GetWeatherForecasts();
     }
}