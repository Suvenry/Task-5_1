using System;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        string faylAdi = "qeydler.txt";

        while (true)
        {
            Console.WriteLine("\n--- Qeydlər Dəftəri ---");
            Console.WriteLine("1) Yeni qeyd");
            Console.WriteLine("2) Bütün qeydlər");
            Console.WriteLine("3) Qeyd axtar (açar söz ilə)");
            Console.WriteLine("4) Bütün qeydləri sil");
            Console.WriteLine("5) Çıxış");
            Console.Write("Seçiminiz (1-5): ");

            string secim = Console.ReadLine();

            if (secim == "5")
            {
                Console.WriteLine("Proqramdan çıxılır...");
                break;
            }

            switch (secim)
            {
                case "1":
                    Console.Write("Yeni qeydi daxil edin: ");
                    string yeniQeyd = Console.ReadLine();
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(faylAdi, append: true, Encoding.UTF8))
                        {
                            string tarixVaxt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            sw.WriteLine($"[{tarixVaxt}] {yeniQeyd}");
                        }
                        Console.WriteLine("Qeyd uğurla əlavə edildi.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Yazma xətası: {ex.Message}");
                    }
                    break;

                case "2":
                    try
                    {
                        if (!File.Exists(faylAdi))
                        {
                            Console.WriteLine("Hələ heç bir qeyd yoxdur.");
                        }
                        else
                        {
                            Console.WriteLine("\n--- Bütün Qeydlər ---");
                            using (StreamReader sr = new StreamReader(faylAdi, Encoding.UTF8))
                            {
                                string setir;
                                while ((setir = sr.ReadLine()) != null)
                                {
                                    Console.WriteLine(setir);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Oxuma xətası: {ex.Message}");
                    }
                    break;

                case "3":
                    Console.Write("Axtarılacaq açar sözü daxil edin: ");
                    string acarSoz = Console.ReadLine();
                    try
                    {
                        if (!File.Exists(faylAdi))
                        {
                            Console.WriteLine("Qeyd faylı tapılmadı.");
                        }
                        else
                        {
                            bool tapildi = false;
                            Console.WriteLine("\n--- Axtarış Nəticələri ---");
                            using (StreamReader sr = new StreamReader(faylAdi, Encoding.UTF8))
                            {
                                string setir;
                                while ((setir = sr.ReadLine()) != null)
                                {
                                    if (setir.ToLower().Contains(acarSoz.ToLower()))
                                    {
                                        Console.WriteLine(setir);
                                        tapildi = true;
                                    }
                                }
                            }
                            if (!tapildi)
                            {
                                Console.WriteLine("Uyğun qeyd tapılmadı.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Axtarış xətası: {ex.Message}");
                    }
                    break;

                case "4":
                    Console.Write("Bütün qeydləri silmək istədiyinizə əminsiniz? (y/n): ");
                    string tesdiq = Console.ReadLine()?.Trim().ToLower();

                    if (tesdiq == "y")
                    {
                        try
                        {
                            if (File.Exists(faylAdi))
                            {
                                File.Delete(faylAdi);
                                Console.WriteLine("Bütün qeydlər silindi.");
                            }
                            else
                            {
                                Console.WriteLine("Silinəcək fayl onsuz da yoxdur.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Silmə xətası: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Silmə əməliyyatı ləğv edildi.");
                    }
                    break;

                default:
                    Console.WriteLine("Yanlış seçim! Zəhmət olmasa 1 ilə 5 arasında rəqəm daxil edin.");
                    break;
            }
        }
    }
}