namespace WebApplication3;

public class Animal {
    public Animal(int id, string name, string category, string weight, string furColor)
    {
        IdAnimal = id;
        Name = name;
        Category = category;
        Weight = weight;
        FurColor = furColor;
        
    }

    public Animal()
    {
    }

    public int IdAnimal { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Weight { get; set; }
    public string FurColor { get; set; }
}