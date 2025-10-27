namespace ConsoleApp1;

public class Detail
{
    private string rue1;

    public Detail(string rue1, int code, string ville)
    {
        this.rue1 = rue1;
        this.Codepostal = code;
        this.Ville = ville;
    }

    public string Rue
    {
        get => rue1;
        set => rue1 = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Codepostal { get; set; }

    public string Ville { get; set; }
}