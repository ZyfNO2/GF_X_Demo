
public class UnitEntity : SampleEntity
{
    private Camp camp;
    
    public UnitEntity SetCamp(Camp camp)
    {
        this.camp = camp;
        return this;
    }

    public Camp GetCamp()
    {
        return this.camp;
    }

}
    