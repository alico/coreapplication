

namespace CoreApplication.Common
{
    public class ItemNotFoundException : CustomApiException
    {
        public ItemNotFoundException(string message = "Couldn't find this item!") : base(message)
        {

        }

    }
}
