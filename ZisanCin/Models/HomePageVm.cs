using ZisanCin.Entities;

namespace ZisanCin.Models
{
    public class HomePageVm
    {
        public IEnumerable<Service> Services { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public About About { get; set; }
        public BodyMassIndex BodyMass { get; set; }
        public HomePageVm()
        {
            Services = new List<Service>();
            Blogs = new List<Blog>();
            About = new About();
            BodyMass = new BodyMassIndex();
        }
    }
}
