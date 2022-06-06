using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OfferCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationController : ControllerBase
    {

        [HttpPost]
        public ActionResult<CalculationResult> Post(Calculation calculation)
        {
            float totalTimeBasedCosts = calculation.PricePerHour * calculation.Hours;
            float totalDistanceBasedConst = calculation.PricePerKilometre * calculation.Kilometers;

            return Ok(new CalculationResult {
                TotalTimeBasedCosts = totalTimeBasedCosts,
                TotalDistanceBasedConsts = totalDistanceBasedConst,
                Income = calculation.Income,
                TimeBasedProfitability = calculation.Income - totalTimeBasedCosts,
                DistanceBasedProfitability = calculation.Income - totalDistanceBasedConst
            });
        }
    }
}
