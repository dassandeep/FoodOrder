
namespace MenuPublisher
{
    using System;

    public class Food
    {
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public string HotelName { get; set; }
        //public FoodType foodType { get; set; }
    }
    public enum FoodType
    {
        Italian, 
        French, 
        Greek, 
        Turkish, 
        Azian
    }
}
