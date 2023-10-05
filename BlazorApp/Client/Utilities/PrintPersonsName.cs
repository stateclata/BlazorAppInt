using BlazorApp.Shared;
using MudBlazor;

namespace BlazorApp.Client.Utilities
{
    public class PrintPersonsName
    {
        private readonly ISnackbar _snackbar;

        public PrintPersonsName(ISnackbar snackbar)
        {
            _snackbar = snackbar;
        }

        public void PrintName<T>(T person) where T : class
        {
            switch (person)
            {
                case Employee:
                    var employee = person as Employee;
                    if (!string.IsNullOrEmpty(employee.Name))
                        _snackbar.Add($"Employee Name is: {employee.Name}", Severity.Info);
                    else
                        _snackbar.Add("Employee without a name!", Severity.Info);
                    break;
                case Manager:
                    var manager = person as Manager;
                    if (!string.IsNullOrEmpty(manager.Name))
                        _snackbar.Add($"Manager's name is: {manager.Name}", Severity.Info);
                    else
                        _snackbar.Add("Manager without a name!", Severity.Info);
                    break;
                default:
                    _snackbar.Add("Unknown type!", Severity.Info);
                    break;
            }
        }
    }
}
