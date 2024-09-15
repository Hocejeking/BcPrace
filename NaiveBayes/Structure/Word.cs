public class Word
{
    public string Text { get; set;}
    private decimal _posteriorProp;
    private decimal _apriorProp;
    public Word(string Text)
    {
        this.Text = Text;
    }

    public decimal getPosteriorProp(){
        return this._posteriorProp;
    }

    public decimal getApriorProp(){
        return this._apriorProp;
    }

    public void setApriorProp(decimal prop){
        _apriorProp = prop;
    }

    public void setPosteriorProp(decimal prop){
        _posteriorProp = prop;
    }
}