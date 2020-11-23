using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Person : IValidatableObject
{
    public string Name { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        throw new System.NotImplementedException();
    }
}

public interface IPerson
{
    string Name { get; set; }
}

abstract class Employee : IPerson
{
    public string Name { get => "kennedy"; set => throw new System.NotImplementedException(); }
}

class Developer : Employee
{

}

public class User
{
    public int Id { get; set; }
    public TiposDeChave tipoChave { get; set; }
    public string chave { get; set; }

    public IEnumerable<ValidationResult> Validate()
    {
        if (Id < 0)
            yield return new ValidationResult("O id está errado");
        if (chave.Length < 9)
            yield return new ValidationResult("Chave está incorreta");
    }
}

public enum TiposDeChave
{
    ChavePix = 1,
    ChaveLari = 2,
    ChaveKennedy = 3
}