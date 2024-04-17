namespace WebApplication3;

public class Animal
{
    public int IdAnimal { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string category { get; set; }
    public string area { get; set; }

    public Animal()
    {
    }

    public Animal(int idAnimal, string name, string description, string category, string area)
    {
        IdAnimal = idAnimal;
        this.name = name;
        this.description = description;
        this.category = category;
        this.area = area;
    }
    
    public override string ToString()
    {
        return $"IdAnimal: {IdAnimal}, Name: {name}, Description: {description}, Category: {category}, Area: {area}";
    }
    
}