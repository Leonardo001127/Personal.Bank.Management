using Personal.BankManagement.Domain;

namespace Personal.BankManagement.Infraestructure;

public class PersonRepository : IPersonRepository
{
    private readonly BankManagementContext Context;

    public PersonRepository(BankManagementContext context){
        Context = context;
    }

    public void Add(Person person)
    {
        Context.Persons.Add(person); 
    }

    public void Update(Person person)
    {
        Context.Persons.Update(person); 
    }
}