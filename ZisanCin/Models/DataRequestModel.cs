using ZisanCin.Entities;

namespace ZisanCin.Models
{
    public class DataRequestModel
    {
        //public static List<Service> Services { get; set; } = new List<Service>();
        //public static About About { get; set; } = new About();
        public static SiteSetting SiteSetting { get; set; } = new SiteSetting();
        public static void ClearData()
        {
            //Services = new List<Service>();
            /*About = new About();        */         // null yapma
            
            SiteSetting = new SiteSetting();     // null yapma
           
        }
    }
}
