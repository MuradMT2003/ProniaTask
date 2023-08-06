using ProniaTask.Models;
using ProniaTask.ViewModels;
using ProniaTask.ViewModels.SliderVMs;

namespace ProniaTask.Services.Interfaces
{
    public interface ISliderService
    {
        Task Create(CreateSliderVM sliderVM);
        Task Update(UpdateSliderVM sliderVM);
        Task Delete(int? id);
        Task<ICollection<Slider>> GetAll();
        Task<Slider> GetById(int? id);
    }
}
