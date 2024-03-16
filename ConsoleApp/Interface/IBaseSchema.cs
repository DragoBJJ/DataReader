
namespace ConsoleApp.Interface
{
    internal interface IBaseSchema
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public abstract string CleanString(string input);

    }
}
