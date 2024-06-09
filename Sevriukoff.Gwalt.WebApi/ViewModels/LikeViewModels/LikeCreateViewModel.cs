using Sevriukoff.Gwalt.Application.Enums;

namespace Sevriukoff.Gwalt.WebApi.ViewModels;

public class LikeCreateViewModel
{
    public LikeableType LikeableType { get; set; }
    public int LikeableId { get; set; }
}