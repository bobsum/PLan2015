namespace Plan2015.Data.Entities
{
    public class LessonPoint
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        public int HouseId { get; set; }
        public virtual House House { get; set; }
    }
}