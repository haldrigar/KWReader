﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using KW_Tools;
using OfficeOpenXml;

namespace KWReader
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Jako parametr podaj ścieżkę do plików *.html");
                return;
            }

            FileInfo xlsFile = new FileInfo(args[0].TrimEnd('\\') + "\\!KW.xlsx");
            if (xlsFile.Exists) xlsFile.Delete();

            ExcelPackage xlsWorkbook = new ExcelPackage(xlsFile);

            xlsWorkbook.Workbook.Properties.Title = "Raport KW";
            xlsWorkbook.Workbook.Properties.Author = "Grzegorz Gogolewski";
            xlsWorkbook.Workbook.Properties.Comments = "Raport KW";
            xlsWorkbook.Workbook.Properties.Company = "GISNET";

            ExcelWorksheet xlsSheetKw = xlsWorkbook.Workbook.Worksheets.Add("KW");

            xlsSheetKw.Cells[1, 1].Value = "NazwaPliku";
            xlsSheetKw.Cells[1, 2].Value = "NumerKsiegi";
            xlsSheetKw.Cells[1, 3].Value = "LiczbaDzialek";
            xlsSheetKw.Cells[1, 4].Value = "LiczbaBudynkow";
            xlsSheetKw.Cells[1, 5].Value = "LiczbaLokali";
            xlsSheetKw.Cells[1, 6].Value = "Zamknieta";

            ExcelWorksheet xlsSheetDzialki = xlsWorkbook.Workbook.Worksheets.Add("Działki");

            xlsSheetDzialki.Cells[1, 1].Value = "NumerKsiegi";
            xlsSheetDzialki.Cells[1, 2].Value = "ChwilaZamkniecia";
            xlsSheetDzialki.Cells[1, 3].Value = "PodstawaZamkniecia";

            xlsSheetDzialki.Cells[1, 4].Value = "PolozenieMulti";
            xlsSheetDzialki.Cells[1, 5].Value = "Gmina";
            xlsSheetDzialki.Cells[1, 6].Value = "Miejscowosc";

            xlsSheetDzialki.Cells[1, 7].Value = "IdDzialki";
            xlsSheetDzialki.Cells[1, 8].Value = "NumerDzialki";
            xlsSheetDzialki.Cells[1, 9].Value = "NumerObrebuEwid";
            xlsSheetDzialki.Cells[1, 10].Value = "NazwaObrebuEwid";
            xlsSheetDzialki.Cells[1, 11].Value = "UlicaMulti";
            xlsSheetDzialki.Cells[1, 12].Value = "Ulica";
            xlsSheetDzialki.Cells[1, 13].Value = "SposobKorzystania";

            xlsSheetDzialki.Cells[1, 14].Value = "LiczbaDZwKW";
            xlsSheetDzialki.Cells[1, 15].Value = "PowObszaru";

            ExcelWorksheet xlsSheetBudynki = xlsWorkbook.Workbook.Worksheets.Add("Budynki");

            xlsSheetBudynki.Cells[1, 1].Value = "NumerKsiegi";
            xlsSheetBudynki.Cells[1, 2].Value = "ChwilaZamkniecia";
            xlsSheetBudynki.Cells[1, 3].Value = "PodstawaZamkniecia";

            xlsSheetBudynki.Cells[1, 4].Value = "PolozenieMulti";
            xlsSheetBudynki.Cells[1, 5].Value = "Gmina";
            xlsSheetBudynki.Cells[1, 6].Value = "Miejscowosc";

            xlsSheetBudynki.Cells[1, 7].Value = "IdBudynku";
            xlsSheetBudynki.Cells[1, 8].Value = "IdDzialkiMulti";
            xlsSheetBudynki.Cells[1, 9].Value = "IdDzialki";
            xlsSheetBudynki.Cells[1, 10].Value = "NazwaUlicy";
            xlsSheetBudynki.Cells[1, 11].Value = "NumerPorzadkowy";
            xlsSheetBudynki.Cells[1, 12].Value = "LiczbaKondygnacji";
            xlsSheetBudynki.Cells[1, 13].Value = "LiczbaLokali";
            xlsSheetBudynki.Cells[1, 14].Value = "PowierzchniaUzytkowa";
            xlsSheetBudynki.Cells[1, 15].Value = "Przeznaczenie";
            xlsSheetBudynki.Cells[1, 16].Value = "DalszyOpis";
            xlsSheetBudynki.Cells[1, 17].Value = "Nieruchomosc";
            xlsSheetBudynki.Cells[1, 18].Value = "Odrebnosc";

            ExcelWorksheet xlsSheetLokale = xlsWorkbook.Workbook.Worksheets.Add("Lokale");

            xlsSheetLokale.Cells[1, 1].Value = "NumerKsiegi";
            xlsSheetLokale.Cells[1, 2].Value = "ChwilaZamkniecia";
            xlsSheetLokale.Cells[1, 3].Value = "PodstawaZamkniecia";

            xlsSheetLokale.Cells[1, 4].Value = "PolozenieMulti";
            xlsSheetLokale.Cells[1, 5].Value = "Gmina";
            xlsSheetLokale.Cells[1, 6].Value = "Miejscowosc";

            xlsSheetLokale.Cells[1, 7].Value = "IdLokalu";
            xlsSheetLokale.Cells[1, 8].Value = "Ulica";
            xlsSheetLokale.Cells[1, 9].Value = "NumerBudynku";
            xlsSheetLokale.Cells[1, 10].Value = "NumerLokalu";
            xlsSheetLokale.Cells[1, 11].Value = "PrzeznaczenieLokalu";
            xlsSheetLokale.Cells[1, 12].Value = "OpisLokalu";
            xlsSheetLokale.Cells[1, 13].Value = "OpisPomPrzyn";
            xlsSheetLokale.Cells[1, 14].Value = "Kondygnacja";
            xlsSheetLokale.Cells[1, 15].Value = "Nieruchomosc";
            xlsSheetLokale.Cells[1, 16].Value = "Odrebnosc";

            string [] fileEntries = Directory.GetFiles(args[0], "*.html");

            int i = 0;
            int dzialkaCounter = 2;
            int budynekCounter = 2;
            int lokalCounter = 2;
            int kwCounter = 2;

            List<string> listaKwDzialki = new List<string>();
            List<string> listaKwBudynki = new List<string>();
            List<string> listaKwLokale = new List<string>();
            List<string> listaKwZamkniete = new List<string>();

            foreach (string file in fileEntries)
            {
                i++;

                StreamReader htmlFile = new StreamReader(new FileStream(file, FileMode.Open), Encoding.UTF8);

                KwFromHtml kw = new KwFromHtml(htmlFile.ReadToEnd());
            
                htmlFile.Close();

                kw.ParseKw();

                xlsSheetKw.Cells[kwCounter, 1].Value = file;
                xlsSheetKw.Cells[kwCounter, 2].Value = kw.KwInformacjePodstawowe.NumerKsiegi;
                xlsSheetKw.Cells[kwCounter, 3].Value = kw.KwDzialkaList.Count;
                xlsSheetKw.Cells[kwCounter, 4].Value = kw.KwBudynekList.Count;
                xlsSheetKw.Cells[kwCounter, 5].Value = kw.KwLokalList.Count;

                // dodaj do listy numery ksiąg wieczystych, które mają działki
                if (kw.KwDzialkaList.Count > 0)
                {
                    listaKwDzialki.Add(kw.KwInformacjePodstawowe.NumerKsiegi);
                }

                // dodaj do listy numery ksiąg wieczystych, które mają budynki
                if (kw.KwBudynekList.Count > 0)
                {
                    listaKwBudynki.Add(kw.KwInformacjePodstawowe.NumerKsiegi);
                }

                // dodaj do listy numery ksiąg wieczystych, które mają lokale
                if (kw.KwLokalList.Count > 0)
                {
                    listaKwLokale.Add(kw.KwInformacjePodstawowe.NumerKsiegi);
                }

                // dodaj do listy numery ksiąg wieczystych, które są zamknięte
                if (kw.KwZamkniecieKsiegi.ChwilaZamkniecia != "- - -" || kw.KwZamkniecieKsiegi.PodstawaZamkniecia != "- - -")
                {
                    listaKwZamkniete.Add(kw.KwInformacjePodstawowe.NumerKsiegi);
                    xlsSheetKw.Cells[kwCounter, 6].Value = "TAK";
                }
                else
                {
                    xlsSheetKw.Cells[kwCounter, 6].Value = "NIE";
                }



                kwCounter++;

                Console.WriteLine("[{0, 6}/{1, 6}]: {2}, Liczba działek: {3, 3}, Liczba budynków: {4, 3}, Liczba lokali: {5, 3}",  
                                  i, fileEntries.Length, kw.KwInformacjePodstawowe.NumerKsiegi, kw.KwDzialkaList.Count, kw.KwBudynekList.Count, kw.KwLokalList.Count);

                foreach (Dzialka dzialka in kw.KwDzialkaList)
                {
                    xlsSheetDzialki.Cells[dzialkaCounter, 1].Value = kw.KwInformacjePodstawowe.NumerKsiegi;
                    xlsSheetDzialki.Cells[dzialkaCounter, 2].Value = kw.KwZamkniecieKsiegi.ChwilaZamkniecia;
                    xlsSheetDzialki.Cells[dzialkaCounter, 3].Value = kw.KwZamkniecieKsiegi.PodstawaZamkniecia;

                    xlsSheetDzialki.Cells[dzialkaCounter, 4].Value = dzialka.PolozenieMulti;
                    xlsSheetDzialki.Cells[dzialkaCounter, 5].Value = kw.GetPolozenie(dzialka, PolozenieTyp.Gmina);
                    xlsSheetDzialki.Cells[dzialkaCounter, 6].Value = kw.GetPolozenie(dzialka, PolozenieTyp.Miejscowosc);

                    xlsSheetDzialki.Cells[dzialkaCounter, 7].Value = dzialka.IdentyfikatorDzialki;
                    xlsSheetDzialki.Cells[dzialkaCounter, 8].Value = dzialka.NumerDzialki;
                    xlsSheetDzialki.Cells[dzialkaCounter, 9].Value = dzialka.NumerObrebuEwidencyjnego;
                    xlsSheetDzialki.Cells[dzialkaCounter, 10].Value = dzialka.NazwaObrebuEwidencyjnego;
                    xlsSheetDzialki.Cells[dzialkaCounter, 11].Value = dzialka.UlicaMulti;
                    xlsSheetDzialki.Cells[dzialkaCounter, 12].Value = kw.GetUlicaForDzialka(dzialka);
                    xlsSheetDzialki.Cells[dzialkaCounter, 13].Value = dzialka.SposobKorzystania;

                    xlsSheetDzialki.Cells[dzialkaCounter, 14].Value = kw.KwDzialkaList.Count;
                    xlsSheetDzialki.Cells[dzialkaCounter, 15].Value = kw.KwObszar.ObszarHA;

                    dzialkaCounter++;

                }

                foreach (Budynek budynek in kw.KwBudynekList)
                {
                    xlsSheetBudynki.Cells[budynekCounter, 1].Value = kw.KwInformacjePodstawowe.NumerKsiegi;
                    xlsSheetBudynki.Cells[budynekCounter, 2].Value = kw.KwZamkniecieKsiegi.ChwilaZamkniecia;
                    xlsSheetBudynki.Cells[budynekCounter, 3].Value = kw.KwZamkniecieKsiegi.PodstawaZamkniecia;

                    xlsSheetBudynki.Cells[budynekCounter, 4].Value = budynek.PolozenieMulti;
                    xlsSheetBudynki.Cells[budynekCounter, 5].Value = kw.GetPolozenie(budynek, PolozenieTyp.Gmina);
                    xlsSheetBudynki.Cells[budynekCounter, 6].Value = kw.GetPolozenie(budynek, PolozenieTyp.Miejscowosc);

                    xlsSheetBudynki.Cells[budynekCounter, 7].Value = budynek.IdentyfikatorBudynku;
                    xlsSheetBudynki.Cells[budynekCounter, 8].Value = budynek.IdentyfikatorDzialkiMulti;
                    xlsSheetBudynki.Cells[budynekCounter, 9].Value = kw.GetIdentyfikatorDzialkiForBudynek(budynek);
                    xlsSheetBudynki.Cells[budynekCounter, 10].Value = budynek.NazwaUlicy;
                    xlsSheetBudynki.Cells[budynekCounter, 11].Value = budynek.NumerPorzadkowy;
                    xlsSheetBudynki.Cells[budynekCounter, 12].Value = budynek.LiczbaKondygnacji;
                    xlsSheetBudynki.Cells[budynekCounter, 13].Value = budynek.LiczbaLokali;
                    xlsSheetBudynki.Cells[budynekCounter, 14].Value = budynek.PowierzchniaUzytkowa;
                    xlsSheetBudynki.Cells[budynekCounter, 15].Value = budynek.Przeznaczenie;
                    xlsSheetBudynki.Cells[budynekCounter, 16].Value = budynek.DalszyOpis;
                    xlsSheetBudynki.Cells[budynekCounter, 17].Value = budynek.Nieruchomosc;
                    xlsSheetBudynki.Cells[budynekCounter, 18].Value = budynek.Odrebnosc;

                    budynekCounter++;
                }

                foreach (Lokal lokal in kw.KwLokalList)
                {
                    xlsSheetLokale.Cells[lokalCounter, 1].Value = kw.KwInformacjePodstawowe.NumerKsiegi;
                    xlsSheetLokale.Cells[lokalCounter, 2].Value = kw.KwZamkniecieKsiegi.ChwilaZamkniecia;
                    xlsSheetLokale.Cells[lokalCounter, 3].Value = kw.KwZamkniecieKsiegi.PodstawaZamkniecia;

                    xlsSheetLokale.Cells[lokalCounter, 4].Value = lokal.PolozenieMulti;
                    xlsSheetLokale.Cells[lokalCounter, 5].Value = kw.GetPolozenie(lokal, PolozenieTyp.Gmina);
                    xlsSheetLokale.Cells[lokalCounter, 6].Value = kw.GetPolozenie(lokal, PolozenieTyp.Miejscowosc);

                    xlsSheetLokale.Cells[lokalCounter, 7].Value = lokal.IdentyfikatorLokalu;
                    xlsSheetLokale.Cells[lokalCounter, 8].Value = lokal.Ulica;
                    xlsSheetLokale.Cells[lokalCounter, 9].Value = lokal.NumerBudynku;
                    xlsSheetLokale.Cells[lokalCounter, 10].Value = lokal.NumerLokalu;
                    xlsSheetLokale.Cells[lokalCounter, 11].Value = lokal.PrzeznaczenieLokalu;
                    xlsSheetLokale.Cells[lokalCounter, 12].Value = lokal.OpisLokalu;
                    xlsSheetLokale.Cells[lokalCounter, 13].Value = lokal.OpisPomieszczenPrzynależnych;
                    xlsSheetLokale.Cells[lokalCounter, 14].Value = lokal.Kondygnacja;
                    xlsSheetLokale.Cells[lokalCounter, 15].Value = lokal.Nieruchomosc;
                    xlsSheetLokale.Cells[lokalCounter, 16].Value = lokal.Odrebnosc;


                    lokalCounter++;
                }

                if (kw.KwLog.Count != 0)
                {
                    StreamWriter logFile = new StreamWriter(new FileStream(file.Substring(0, file.LastIndexOf(".", StringComparison.Ordinal)) + ".log", FileMode.Create), Encoding.UTF8);

                    foreach (string log in kw.KwLog)
                    {
                        logFile.WriteLine(log);
                    }

                    logFile.Close();
                }

            }

            // ------------------------------------------------------------------------------------
            // lista ksiąg które mają działki
            // ------------------------------------------------------------------------------------
            StreamWriter outputFile = new StreamWriter(new FileStream(args[0].TrimEnd('\\') + "\\!listaKW_Dzialki.txt", FileMode.Create), Encoding.UTF8);
            foreach (string ksiega in listaKwDzialki)
            {
                outputFile.WriteLine(ksiega);
            }
            outputFile.Close();
            // ------------------------------------------------------------------------------------

            // ------------------------------------------------------------------------------------
            // lista ksiąg które mają budynki
            // ------------------------------------------------------------------------------------
            outputFile = new StreamWriter(new FileStream(args[0].TrimEnd('\\') + "\\!listaKW_Budynki.txt", FileMode.Create), Encoding.UTF8);
            foreach (string ksiega in listaKwBudynki)
            {
                outputFile.WriteLine(ksiega);
            }
            outputFile.Close();
            // ------------------------------------------------------------------------------------

            // ------------------------------------------------------------------------------------
            // lista ksiąg które mają lokale
            // ------------------------------------------------------------------------------------
            outputFile = new StreamWriter(new FileStream(args[0].TrimEnd('\\') + "\\!listaKW_Lokale.txt", FileMode.Create), Encoding.UTF8);
            foreach (string ksiega in listaKwLokale)
            {
                outputFile.WriteLine(ksiega);
            }
            outputFile.Close();
            // ------------------------------------------------------------------------------------

            // ------------------------------------------------------------------------------------
            // lista ksiąg zamkniętych
            // ------------------------------------------------------------------------------------
            outputFile = new StreamWriter(new FileStream(args[0].TrimEnd('\\') + "\\!listaKW_Zamkniete.txt", FileMode.Create), Encoding.UTF8);
            foreach (string ksiega in listaKwZamkniete)
            {
                outputFile.WriteLine(ksiega);
            }
            outputFile.Close();
            // ------------------------------------------------------------------------------------

            Console.WriteLine("Formatowanie arkusza KW...");

            xlsSheetKw.Cells["A1:F" + Convert.ToString(kwCounter - 1)].AutoFilter = true;
            xlsSheetKw.View.FreezePanes(2, 2);
            xlsSheetKw.Cells.Style.Font.Size = 10;
            xlsSheetKw.Cells.AutoFitColumns(0);


            Console.WriteLine("Formatowanie arkusza działek...");

            xlsSheetDzialki.Cells["A1:O" + Convert.ToString(dzialkaCounter - 1)].AutoFilter = true;
            xlsSheetDzialki.View.FreezePanes(2, 2);
            xlsSheetDzialki.Cells.Style.Font.Size = 10;
            xlsSheetDzialki.Cells.AutoFitColumns(0);
            xlsSheetDzialki.Column(3).Width = 24;
            xlsSheetDzialki.Column(13).Width = 50;

            Console.WriteLine("Formatowanie arkusza budynków...");

            xlsSheetBudynki.Cells["A1:R" + Convert.ToString(budynekCounter - 1)].AutoFilter = true;
            xlsSheetBudynki.View.FreezePanes(2, 2);
            xlsSheetBudynki.Cells.Style.Font.Size = 10;
            xlsSheetBudynki.Cells.AutoFitColumns(0);
            xlsSheetBudynki.Column(3).Width = 24;

            Console.WriteLine("Formatowanie arkusza lokali...");

            xlsSheetLokale.Cells["A1:P" + Convert.ToString(lokalCounter - 1)].AutoFilter = true;
            xlsSheetLokale.View.FreezePanes(2, 2);
            xlsSheetLokale.Cells.Style.Font.Size = 10;
            xlsSheetLokale.Cells.AutoFitColumns(0);
            xlsSheetLokale.Column(3).Width = 24;

            Console.WriteLine("Zapisywanie pliku...");

            xlsWorkbook.Save();

            Console.WriteLine("Gotowe!");
            Console.ReadKey();

        }

    }
}

