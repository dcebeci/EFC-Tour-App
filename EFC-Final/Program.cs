using System;
using System.Collections.Generic;
using System.Linq;
using EFC_Final.Mapping;
using EFC_Final.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace EFC_Final
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            TourAppDbContext db = new TourAppDbContext();

            #region Raporlar

            //1 En çok gezilen yer/yerler neresidir?

            var query1 =
                from p in db.ToVisits
                join v in db.Customer
                on p.VisitId equals v.CustomerId
                group p by p.LocationName into g
                select new
                {
                    LocationName = g.Key,
                    Count = g.Count()
                };
            var result = query1.OrderByDescending(x => x.Count).FirstOrDefault();
           
            foreach (var item in query1)
            {
                Console.WriteLine($"En cok ziyaret edilen yer {result.LocationName} ve {result.Count} .kez ziyaret edilmiştir.");
            }



            //2 Ağustos ayında en çok çalışan rehber/rehberler kimdir/kimlerdir?

            var query2 =
                from guide in db.Guides
                join tourist in db.Customer
                on guide.GuideId equals tourist.CustomerId
                where tourist.ArrivingDate.Month == 8
                group tourist by guide.GuideId into g
                select new
                {
                    GuideId = g.Key,
                    WorkCount = g.Count()
                };
            foreach (var item in query2) { 
                Console.WriteLine("{0} - {1}", query2.Max(a => a.WorkCount), query2.Max(a => a.GuideId)); 
            }


            //3 Kadın turistlerin gezdiği yerleri, toplam ziyaret edilme sayılarıyla beraber listeleyin.

            var query3 =
                from t in db.Customer
                join f in db.Genders on t.CustomerId equals f.GenderId
                join v in db.ToVisits on t.CustomerId equals v.VisitId
                where t.Genders.GenderId == 1
                group t by v.VisitId into g
                
                select new
                {
                    
                    VisitId = g.Key,
                    Count = g.Count(),
                   
                    
                };
            
            foreach (var item in query3)
            {

                Console.WriteLine("{0} - {1}  ", item.VisitId, item.Count );
            }

            //4 İngiltere’den gelip de Kız Kulesi’ni gezen turistler kimlerdir?

            var query4 = from v in db.ToVisits
                         join c in db.Customer on v.VisitId equals c.CustomerId
                         join co in db.Countries on v.VisitId equals co.CountryId
                         where v.LocationName == "Kız kulesi" && co.CName == "İngiltere"
                         select new
                         {   
                             CustomerName = c.FirstName + " " + c.LastName,
                         };
        
            foreach (var item in query4)
            {
                Console.WriteLine(item.CustomerName);
            }



            //5 Gezilen yerler hangi yılda kaç defa gezilmiştir?

          
            
            var query5 =
                    from v in db.ToVisits
                    join c in db.Customer on v.VisitId equals c.CustomerId
                    orderby c.ArrivingDate.Year descending // en yakın zamandan en eski tarihe göre sıralamak için.
                    group v by v.LocationName into g
                    select new
                    {
                        
                        Year = g.Key,
                        Count = g.Count()
                    };
            foreach (var item in query5)
            {
                Console.WriteLine($"{item.Year} : {item.Count}");
            }





            //6 2’den fazla tura rehberlik eden rehberlerin en çok tanıttıkları yerler nelerdir?
            var query6 =
                from gu in db.ToVisits
                join v in db.Guides on gu.VisitId equals v.GuideId
                group gu by v.GuideId into g
                where g.Count() > 2
                select new
                {
                    
                    GuideId = g.Key,
                    Count = g.Count(),
                    LName = g.Select(x=>x.LocationName)
                };
                  foreach (var item in query6)
            {
                Console.WriteLine(item.LName + " " + item.Count);
            }


            //7 İtalyan turistler en çok nereyi gezmiştir?


            var query7 = from c in db.ToVisits
                           join ct in db.Countries on c.VisitId equals ct.CountryId
                           join cu in db.Customer on c.VisitId equals cu.CustomerId    
                           where ct.CName == "İtalya"
                           group c by c.LocationName into g
                           select new
                           {
                               LocationName = g.Key,
                               Count = g.Count()
                           }; 
              foreach (var item in query7)
                  {
                        Console.WriteLine("{0} - {1} ",item.LocationName,item.Count);
                    }



            //8 Kapalı Çarşı’yı gezen en yaşlı turist kimdir?


            var query8 = from v in db.ToVisits
                             join co in db.Customer on v.VisitId equals co.CustomerId
                             where v.LocationName == "Kapalı Çarşı"
                             orderby co.Bday descending 
                             select new
                             {
                                 yasli = (co.FirstName + " " + co.LastName).FirstOrDefault()
                             };
     
                foreach (var item in query8)
                {

                    Console.WriteLine("Kapalı Çarşıdaki en yaşlı turist :" + item.yasli);
                }


            //9 Yunanistan’dan gelen Finlandiyalı turistin gezdiği yerler nerelerdir?


            var query9 = db.ToVisits.
                    Where(v => v.Customers.Any(c => c.Countries.CName == "Yunanistan" && c.Citizenship.CName == "Finlandiya"));
                    foreach (var item in query9)
                    {
                        Console.WriteLine(item.LocationName);
                    }



            //10 Dolmabahçe Sarayı’na en son giden turistler ve rehberi listeleyin.

            var query10 = from v in db.ToVisits
                          join c in db.Customer on v.VisitId equals c.CustomerId
                          join g in db.Guides on v.VisitId equals g.GuideId
                          where v.LocationName == "Dolmabahçe"
                          orderby c.ArrivingDate descending
                          select new 
                          {
                             Id=  v.VisitId,
                              Tname = c.FirstName + " " + c.LastName,
                              Recent = (c.ArrivingDate).ToString("dd/MM/yyyy"),
                              Guide = g.FirstName + " " + g.LastName
                            };
            foreach (var item in query10)
            {
                Console.WriteLine("VisitId: {0}", item.Id);
                Console.WriteLine("Customer: {0} ", item.Tname);
                Console.WriteLine("Guide: {0} ", item.Guide);
                Console.WriteLine("ArrivingDate: {0}", item.Recent.FirstOrDefault());
            }



            #endregion
            
            
            #region Data Girdisi
            
            //--------------------------------------------------------------------
            Guide gu1 = new Guide()
            {
                FirstName = "Ozan",
                LastName = "Temiz",
                PhoneNumber = "0555654443",


            };
            Guide gu2 = new Guide()
            {
                FirstName = "Bahar",
                LastName = "Sevgin",
                PhoneNumber = "0555654444",
            };

            Guide gu3 = new Guide()
            {
                FirstName = "Ömer",
                LastName = "Uçar",
                PhoneNumber = "0555654445",
            };

            Guide gu4 = new Guide()
            {
                FirstName = "Sevgi",
                LastName = "Çakmak",
                PhoneNumber = "0555654446",
            };

            Guide gu5 = new Guide()
            {
                FirstName = "Linda",
                LastName = "Callahan",
                PhoneNumber = "0555654449",
            };



            //------------------------------------------------------------------------
            Gender g1 = new Gender()
            {
                GenderId=1,
                GenderType="Erkek",
                
                
            };
            Gender g2 = new Gender()
            {
                GenderId=2,
                GenderType="Kadın",     
            };
            //------------------------------------------------------------------------------
            Country c1 = new Country()
            {
                CountryId = 1,
                CName = "İtalya",
                

            };
            Country c2 = new Country()
            {
                CountryId = 2,
                CName = "Japonya",

            };
            Country c3 = new Country()
            {
                CountryId = 3,
                CName = "İngiltere",

            };
            Country c4 = new Country()
            {
                CountryId = 4,
                CName = "Almanya",

            };
            Country c5 = new Country()
            {
                CountryId = 5,
                CName = "Finlandiya",

            };
            Country c6 = new Country()
            {
                CountryId = 6,
                CName = "Yunanistan",

            };
            Country c7 = new Country()
            {
                CountryId = 7,
                CName = "Ukrayna",

            };

            //---------------------------------------------------------------------------
            Visits v1 = new Visits() { 
                VisitId = 1,
                LocationName = "Ayasofya",
                
                
            };
            Visits v2 = new Visits()
            {
                VisitId = 2,
                LocationName = "Yerebatan Sanrıcı",

            };
            Visits v3 = new Visits()
            {
                VisitId = 3,
                LocationName = "Pierre Lotti",

            };
            Visits v4 = new Visits()
            {
                VisitId = 4,
                LocationName = "Kız Kulesi",

            };
            Visits v5 = new Visits()
            {
                VisitId = 5,
                LocationName = "Adalar",

            };
            Visits v6 = new Visits()
            {
                VisitId = 6,
                LocationName = "Dolmabahçe Sarayı",

            };
            Visits v7 = new Visits()
            {
                VisitId = 7,
                LocationName = "Miniatürk",

            };
            Visits v8 = new Visits()
            {
                VisitId = 8,
                LocationName = "Sultan Ahmet Camii",

            };
            Visits v9 = new Visits()
            {
                VisitId = 9,
                LocationName = "Rumeli Hisarı",

            };
            Visits v10 = new Visits()
            {
                VisitId = 10,
                LocationName = "Atatürk Arboretumu",

            };
            Visits v11 = new Visits()
            {
                VisitId = 11,
                LocationName = "Mısır Çarşısı",

            };
            Visits v12 = new Visits()
            {
                VisitId = 12,
                LocationName = "Kapalı Çarşı",

            };
            Visits v13 = new Visits()
            {
                VisitId = 13,
                LocationName = "Anadolu Hisarı",

            };


            //----------------------------------------
            List<Visits> c1v = new List<Visits>()
            {
                v1,
                v2
            };
            List<Visits> c2v = new List<Visits>()
            {
                v3,
                v4
            };
            
            List<Visits> c3v = new List<Visits>()
            {
                v1,
                v5,
                v6
            };
            List<Visits> c4v = new List<Visits>()
            {
                v7,
                v8
            };
            List<Visits> c5v = new List<Visits>()
            {
               v9
            };
            List<Visits> c6v = new List<Visits>()
            {
                v6,
                v11
            };
            List<Visits> c7v = new List<Visits>()
            {
                v9,
                v4
               
            };
            List<Visits> c8v = new List<Visits>()
            {
                v13,
                v8
            };
            List<Visits> c9v = new List<Visits>()
            {
               v3,
               v4
            };
            List<Visits> c10v = new List<Visits>()
            {
                v10,
                v6
            };
            List<Visits> c11v = new List<Visits>()
            {
                v12,
                v11
            };
            List<Visits> c12v = new List<Visits>()
            {
                v10
            };
            List<Visits> c13v = new List<Visits>()
            {
                v3,
                v4
            };
            List<Visits> c14v = new List<Visits>()
            {
                v11,
                v10
            };
            List<Visits> c15v = new List<Visits>()
            {
                v7,
                v10
            };









            List<Customer> customers = new List<Customer>()
        {
                new Customer() {CustomerId=1,FirstName="Levi",LastName="Acavedo", Genders=g2 ,Bday=new DateTime(06/11/91),Countries=c1,Citizenship=c2,Guides=gu1, ArrivingDate=new DateTime(01/11/12),ToVisits= c1v },
                new Customer() {CustomerId=2,FirstName="Basil",LastName="Aguilar", Genders=g1 ,Bday=new DateTime(04/22/94),Countries=c6,Citizenship=c6,Guides=gu2, ArrivingDate=new DateTime(08/11/14),ToVisits= c2v },
                new Customer() {CustomerId=3,FirstName="Zenaida",LastName="Holder", Genders=g1 ,Bday=new DateTime(01/09/90),Countries=c6,Citizenship=c5,Guides=gu3, ArrivingDate=new DateTime(02/04/14),ToVisits= c3v },
                new Customer() {CustomerId=4,FirstName="Illana",LastName="Browning", Genders=g2 ,Bday=new DateTime(01/28/91),Countries=c3,Citizenship=c6,Guides=gu4, ArrivingDate=new DateTime(05/01/14),ToVisits= c4v },
                new Customer() {CustomerId=5,FirstName="Raja",LastName="Duke", Genders=g1 ,Bday=new DateTime(07/27/83),Countries=c4,Citizenship=c4,Guides=gu2, ArrivingDate=new DateTime(09/08/14),ToVisits= c5v },
                new Customer() {CustomerId=6,FirstName="Isaiah",LastName="Valdez", Genders=g1 ,Bday=new DateTime(01/16/98),Countries=c5,Citizenship=c5,Guides=gu1, ArrivingDate=new DateTime(08/28/12),ToVisits= c6v },
                new Customer() {CustomerId=7,FirstName="Gray",LastName="Marshall", Genders=g2 ,Bday=new DateTime(11/21/80),Countries=c2,Citizenship=c2,Guides=gu5, ArrivingDate=new DateTime(08/27/13),ToVisits= c7v },
                new Customer() {CustomerId=8,FirstName="Ora",LastName="Flechter", Genders=g2 ,Bday=new DateTime(01/19/94),Countries=c3,Citizenship=c3,Guides=gu2, ArrivingDate=new DateTime(08/23/14),ToVisits= c8v },
                new Customer() {CustomerId=9,FirstName="Lavinia",LastName="Lloyd", Genders=g2 ,Bday=new DateTime(10/26/86),Countries=c3,Citizenship=c3,Guides=gu1, ArrivingDate=new DateTime(03/26/12),ToVisits= c9v },
                new Customer() {CustomerId=10,FirstName="Jenna",LastName="williams", Genders=g2 ,Bday=new DateTime(05/01/82),Countries=c6,Citizenship=c6,Guides=gu3, ArrivingDate=new DateTime(11/26/14),ToVisits= c10v },
                new Customer() {CustomerId=11,FirstName="Christian",LastName="Nash", Genders=g1 ,Bday=new DateTime(08/09/80),Countries=c3,Citizenship=c3,Guides=gu3, ArrivingDate=new DateTime(02/15/13),ToVisits= c11v },
                new Customer() {CustomerId=12,FirstName="Basil",LastName="Aguilar", Genders=g1 ,Bday=new DateTime(04/22/94),Countries=c6,Citizenship=c6,Guides=gu1, ArrivingDate=new DateTime(09/09/14),ToVisits= c12v },
                new Customer() {CustomerId=13,FirstName="Brianna",LastName="Everett", Genders=g1 ,Bday=new DateTime(09/03/78),Countries=c2,Citizenship=c2,Guides=gu2, ArrivingDate=new DateTime(04/19/13),ToVisits= c13v },
                new Customer() {CustomerId=14,FirstName="Geoffrey",LastName="Knowles", Genders=g1 ,Bday=new DateTime(02/17/85),Countries=c7,Citizenship=c7,Guides=gu5, ArrivingDate=new DateTime(01/14/26),ToVisits= c14v },
                new Customer() {CustomerId=15,FirstName="Quinn",LastName="Hamilton", Genders=g1 ,Bday=new DateTime(07/10/90),Countries=c3,Citizenship=c3,Guides=gu4, ArrivingDate=new DateTime(02/15/13),ToVisits= c15v },


            };

           db.SaveChanges();

            #endregion
        }

    }
}
