namespace Plan2015.Data.Entities
{
    public class QuizPoint
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public virtual QuizQuestion Question { get; set; }
        public int HouseId { get; set; }
        public virtual House House { get; set; }
    }
}