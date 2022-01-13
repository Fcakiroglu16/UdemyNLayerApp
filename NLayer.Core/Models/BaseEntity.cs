namespace NLayer.Core
{
    public abstract class BaseEntity
    {


        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
