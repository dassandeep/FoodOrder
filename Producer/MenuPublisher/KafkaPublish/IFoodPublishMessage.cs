namespace MenuPublisher
{

    using System.Threading.Tasks;
    public interface IFoodPublishMessage
    {
        Task WriteMessage(Food  food);
    }
}
