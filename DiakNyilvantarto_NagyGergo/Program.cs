using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace DiakNyilvantarto_NagyGergo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "server=localhost;user=root;password=;database=diak_gyak;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    int menupont;
                    //Console.WriteLine("kapcsolodas sikeres");
                    //MENU
                    Console.WriteLine("======lorem ipsum =======");
                    Console.WriteLine("\n1. Diákok listázása: ");
                    Console.WriteLine("2. Új diák felvétele: ");
                    Console.WriteLine("3. Diák adatainak módosítása: ");
                    Console.WriteLine("4. Diák törlése: ");
                    Console.WriteLine("5. Kilépés: ");
                    Console.Write("\nVálasztás: ");

                    menupont = int.Parse(Console.ReadLine());
                    //while (menupont!=5)
                    //{
                        switch (menupont)
                        {
                            case 1:
                            //MEGJELENIT---------------------------
                            Console.Clear();
                          
                                conn.Open();
                                string sqlCommand = "select * from diakok;";
                                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                                MySqlDataReader reader = cmd.ExecuteReader();

                            Console.WriteLine("ID   Név                 Kor       Osztály\n-------------------------------------------");
                                while (reader.Read())
                                {
                                    Console.WriteLine($"{reader["id"],-3} - {reader["nev"],-20} - {reader["eletkor"],-4} - {reader["osztaly"]}");
                                }
                                conn.Close();
                                break;

                            case 2:
                            //BEVITEL---------------------------
                            Console.Clear();
                            conn.Open();
                            Console.WriteLine("Új diak felvetele ");
                            Console.Write("\nNev: ");
                            string nev=Console.ReadLine();
                            Console.Write("Életkor: ");
                            int kor =int.Parse(Console.ReadLine());
                            Console.Write("Osztály: ");
                            string osztaly = Console.ReadLine();

                            string insertSql = $"insert into diakok (nev,eletkor,osztaly) values (@nev,@kor,@osztaly);";
                    
                            MySqlCommand insertCmd = new MySqlCommand(insertSql, conn);
                            insertCmd.Parameters.AddWithValue("@nev", nev);
                            insertCmd.Parameters.AddWithValue("@kor", kor);
                            insertCmd.Parameters.AddWithValue("@osztaly", osztaly);
                            insertCmd.ExecuteNonQuery();
                            Console.WriteLine("Diák sikeresen hozzáadva");
                            conn.Close();
                                break;

                            case 3:
                            //MODOSIT---------------------------
                            Console.Clear();
                            Console.WriteLine("Diák módosítása:");
                            Console.Write("\nMódosítando diák ID-ja: ");
                            int modositID = int.Parse(Console.ReadLine());
                            Console.Write("\nNev: ");
                            string ujnev = Console.ReadLine();
                            Console.Write("Életkor: ");
                            int ujkor = int.Parse(Console.ReadLine());
                            Console.Write("Osztály: ");
                            string ujosztaly = Console.ReadLine();
                            conn.Open();

                            string updateSql = $"Update diakok set nev=@ujnev ,eletkor=@ujkor,osztaly=@ujosztaly where id={modositID}";
                            MySqlCommand updateCmd = new MySqlCommand(updateSql, conn);
                            updateCmd.Parameters.AddWithValue("@ujnev", ujnev);
                            updateCmd.Parameters.AddWithValue("@ujkor", ujkor);
                            updateCmd.Parameters.AddWithValue("@ujosztaly", ujosztaly);
                            updateCmd.ExecuteNonQuery();
                            
                            Console.WriteLine("Diak mdodistva!");
                            conn.Close();
                            break;

                            case 4:
                            //TOROL---------------------------
                            Console.Clear();
                            conn.Open();
                            Console.WriteLine("Diák Törlése");
                            Console.Write("\nTörlendő diák ID-je: ");
                            int torolID=int.Parse(Console.ReadLine());
                            Console.Write("Biztosan törlöd? (i/n): ");
                            string torolin=Console.ReadLine();
                            if (torolin == "i")
                            {
                                string deleteSql = $"Delete from diakok where id={torolID}";
                                MySqlCommand deleteCmd = new MySqlCommand(deleteSql, conn);
                                deleteCmd.ExecuteNonQuery();
                                Console.WriteLine("Diák törölve!");
                            }
                            else if (torolin == "n")
                            {
                                Console.WriteLine("nincs törlés");   
                            }
                            else 
                            {
                                Console.WriteLine("nem megfelelő válasz!");
                            }
                            conn.Close();
                            break;
                            case 5:
                            //PROGEND---------------------------
                            return;

                            default:
                                Console.WriteLine("nincs ilyen menupont");
                                break;
                        }
                    //}


                    /**/


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hiba, {ex.Message}");
                }
            }

            Console.ReadKey();
        }
    }
}
