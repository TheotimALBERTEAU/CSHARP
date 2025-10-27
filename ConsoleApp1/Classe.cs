namespace ConsoleApp1;

public class Classe
{
    private List<Profil> students;
    private string className;
    private string school;
    private int level;

    public Classe(List<Profil> students, string className, string school, int level)
    {
        this.students = students;
        this.className = className;
        this.school = school;
        this.level = level;
    }

    public int Level
    {
        get => level;
        set => level = value;
    }

    public string School
    {
        get => school;
        set => school = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string ClassName
    {
        get => className;
        set => className = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<Profil> Students
    {
        get => students;
        set => students = value ?? throw new ArgumentNullException(nameof(value));
    }
}