namespace ASPnetComponentPipeline
{
    internal class ComponentProvider : IComponentProvider
    {
        public string GetString(string key)
        {
            return key + " === Some Data";
        }
    }
}