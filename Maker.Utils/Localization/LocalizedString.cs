namespace Maker.Utils.Localization
{
    public class LocalizedString
    {
        private string _id;
        public LocalizedString(string id)
        {
            _id = id;
        }

        public string Get()
        {
            return Localizer.GetText(_id);
        }
    }
}