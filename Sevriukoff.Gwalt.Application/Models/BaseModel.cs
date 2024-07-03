namespace Sevriukoff.Gwalt.Application.Models;

public abstract class BaseModel
{
    public int Id { get; set; }
    
    protected BaseModel(int id)
    {
        Id = id;
    }

    public BaseModel()
    {
        
    }
}