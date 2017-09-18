using ContainerDASample.Database.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace ContainerDASample.Database.Repository
{
    public interface IEmpRepository
    {
         Task<List<Employees>> GetAllEmployees();
         Task<int> AddEmployee(Employees emp);
        Task<bool> UpdateEmployee(Employees emp);
        Task<bool> DeleteEmployee(int employeeID);
    }
}
