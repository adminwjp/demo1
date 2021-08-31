namespace Adverts
{
    public class AdvertMapp: BaseEntityMapp<Advert>
    {
        public AdvertMapp():base(Advert.TableName)
        {
		   
        }

        protected override void Set()
        {
            Property(x => x.StartDate);
            Property(x => x.Remark);
            Property(x => x.Status);
            Property(x => x.Html);
            Property(x => x.Title);
            Property(x => x.EndDate);
            Property(x => x.UseImagesRandom);
            Property(x => x.Code);
        }
    }
}