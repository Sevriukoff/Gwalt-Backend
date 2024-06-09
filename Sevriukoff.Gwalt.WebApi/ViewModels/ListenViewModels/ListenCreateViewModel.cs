using Sevriukoff.Gwalt.Application.Enums;

namespace Sevriukoff.Gwalt.WebApi.ViewModels;

public class ListenCreateViewModel
{
    public ListenableType ListenableType { get; set; }
    public int ListenableId { get; set; }
    
    public int TotalDuration { get; set; } // Общая длительность прослушанного объекта в секундах
    public int EndTime  { get; set; } // Конечное время воспроизведения
    public int ActiveListeningTime { get; set; } // Время активного прослушивания в секундах
    public int SeekCount { get; set; } // Количество перемоток
    public int PauseCount { get; set; } // Количество пауз
    public int Volume { get; set; } // Макс. уровень громкости при прослушивании
}