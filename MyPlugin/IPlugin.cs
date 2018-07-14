using System;
using System.Threading.Tasks;

namespace MyPlugin
{
    public interface IPlugin
    {
        string Name { get; set; }
        Task<string> Do(string info);
    }
}
