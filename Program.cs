// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

internal class Program
{
    private enum Gender
    {
        Male = 'E',
        Female = 'K'
    }

    private static int CalculateAge(DateTime birthYear)
    {
        return DateTime.Now.Year - birthYear.Year;
    }

    private static bool ValidateApplicationCriteria(string name, Gender gender, int age)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("İsim boş olamaz.");

        return gender switch
        {
            Gender.Male when age >= 40 => true,
            Gender.Female when age >= 30 => true,
            _ => false
        };
    }

    private static void ProcessApplication(string name, char genderChar, int birthYear)
    {
        try
        {
            if (birthYear > DateTime.Now.Year)
                throw new ArgumentException("Doğum yılı gelecek bir tarih olamaz.");

            var gender = char.ToUpper(genderChar) switch
            {
                'E' => Gender.Male,
                'K' => Gender.Female,
                _ => throw new ArgumentException("Cinsiyet 'E' veya 'K' olmalıdır.")
            };

            int age = CalculateAge(new DateTime(birthYear, 1, 1));
            bool isAccepted = ValidateApplicationCriteria(name, gender, age);

            Console.WriteLine($"Sayın {name},");
            Console.WriteLine(isAccepted 
                ? "Başvurunuz kabul edildi." 
                : "Başvurunuz kabul edilmedi.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Hata: {ex.Message}");
        }
        catch (Exception)
        {
            Console.WriteLine("Beklenmeyen bir hata oluştu. Lütfen tekrar deneyiniz.");
        }
    }

    public static void Main(string[] args)
    {
        try
        {
            Console.Write("Adınızı giriniz: ");
            string name = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Cinsiyetinizi giriniz (E/K): ");
            char gender = char.ToUpper(Console.ReadLine()?.FirstOrDefault() ?? ' ');

            Console.Write("Doğum yılınızı giriniz: ");
            if (!int.TryParse(Console.ReadLine(), out int birthYear))
            {
                throw new ArgumentException("Geçerli bir doğum yılı giriniz.");
            }

            ProcessApplication(name, gender, birthYear);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata: {ex.Message}");
        }
    }
}