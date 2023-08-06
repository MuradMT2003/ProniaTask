using ProniaTask.Models;

namespace ProniaTask.ViewModels.HomeVMs
{
    public record HomeVm
    {
        public ICollection<Slider> Sliders { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
