namespace Kogane
{
    public sealed class CheckBoxWindowData : ICheckBoxWindowData
    {
        public string Name      { get; set; }
        public bool   IsChecked { get; set; }

        public CheckBoxWindowData
        (
            string name,
            bool   isChecked
        )
        {
            Name      = name;
            IsChecked = isChecked;
        }
    }
}