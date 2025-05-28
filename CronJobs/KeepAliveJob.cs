using Quartz;

namespace OrderUp_API.CronJobs;

public class KeepAliveJob : IJob
{
    
    private readonly RestaurantService _restaurantService;
    
    public KeepAliveJob(RestaurantService restaurantService) {
        _restaurantService = restaurantService;
    }
    
    public async Task Execute(IJobExecutionContext context)
    {
        // To keep the DB alive
        try
        {
            await _restaurantService.GetOneRestaurant();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
        
    }
}