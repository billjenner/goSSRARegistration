using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace goSSRA.Registration.Models
{
    public class RegistrationDB : DbContext
    {
        public RegistrationDB()
        {
        }

        public RegistrationDB(string cxString)
            : base(cxString)
        {
        }

        public DbSet<Athlete> Athletes { get; set; }

        public DbSet<Enrollment> Enrollment { get; set; }

        public DbSet<Program> Programs { get; set; }

        public DbSet<VolunteerRoles> VolunteerRoles { get; set; }

        public DbSet<VolunteerEvents> VolunteerEvents { get; set; }

        public DbSet<VolunteerCommitments> VolunteerCommitments { get; set; }

        public DbSet<ReceiptData> ReceiptDatas { get; set; }

        public DbSet<PaymentBtns> PaymentBtns { get; set; }
    }


    // You can add custom code to this file. Changes will not be overwritten.
    // 
    // If you want Entity Framework to drop and regenerate your database
    // automatically whenever you change your model schema, add the following
    // code to the Application_Start method in your Global.asax file.
    // Note: this will destroy and re-create your database with every model change.

    //DropCreateDatabaseAlways<RegistrationDB>
    //DropCreateDatabaseIfModelChanges<RegistrationDB> 
    public class RegistrationDBInitializer : DropCreateDatabaseAlways<RegistrationDB>
    {

        protected override void Seed(RegistrationDB context)
        {
            context.PaymentBtns.Add(new PaymentBtns { PaymentBtnsID = 1, Desc = "", Amount = 1,  btnDesc = "Donation Button", hRefLink = "https://www.paypal.com/us/cgi-bin/webscr?cmd=_flow&SESSION=SgLov7CMG4YlD1Zh9z1hYGG1xWvy7Xzmohinxgfqg4TpOXM2nMJtyNi370u&dispatch=5885d80a13c0db1f8e263663d3faee8d48a116ba977951b3435308b8c4dd4ef1" });
            context.Programs.Add(new Program { ProgramID = 1, Title = "Intro to Ski Racing-SAT", Description = "Introduction to Ski Racing is designed for first-time participants ages 5-11. This program offers over 80 hours of coaching on Saturdays OR Sundays (must choose one or the other) from Dec 1st or 2nd to Mar 23rd-24th. Groups meet at 9am and ski together until 2:30pm. Program includes 5-day Holiday Camp (Dec. 27-31). Skiers compete in Mt. Spokane home series races and select EEYSL races.", Price = 745.00, Discount = 35.00, DiscountDesc = "PreSeason", Active = true });
            context.Programs.Add(new Program { ProgramID = 2, Title = "Intro to Ski Racing-SUN", Description = "Introduction to Ski Racing is designed for first-time participants ages 5-11. This program offers over 80 hours of coaching on Saturdays OR Sundays (must choose one or the other) from Dec 1st or 2nd to Mar 23rd-24th. Groups meet at 9am and ski together until 2:30pm. Program includes 5-day Holiday Camp (Dec. 27-31). Skiers compete in Mt. Spokane home series races and select EEYSL races.", Price = 745.00, Discount = 35.00, DiscountDesc = "PreSeason", Active = true });
            context.Programs.Add(new Program { ProgramID = 3, Title = "Youth Ski League", Description = "Our most popular and successful program! The YSL is designed for skiers 5-11 who want to have fun becoming all-mountain skiers while developing abilities to compete in ski racing. The program offers over 165 hours of coaching on Saturdays AND Sundays, December 1st-March 24th and includes Holiday Camp (Dec. 27-31)! Groups meet at 9am and ski until 2:30pm. Skiers compete in the local EEYSL race series and Mt. Spokane home series.", Price = 1035.00, Discount = 55.00, DiscountDesc = "PreSeason", Active = true });
            context.Programs.Add(new Program { ProgramID = 4, Title = "Full Time Youth Ski League", Description = "This program offers everything that our popular YSL program offers with the addition of Wednesday night training during January-Mid March. The Full Time Youth Ski League is a great option for those who want to have more opportunities for improvement and something fun to look forward to after school!", Price = 1315.00, Discount = 65.00, DiscountDesc = "PreSeason", Active = true });
            context.Programs.Add(new Program { ProgramID = 5, Title = "Alpine Team Development", Description = "Alpine Team Development (DEVO) is designed for athletes 10-13 years old who want to take their skiing to the next level. Participating in the Alpine Team Development program takes a serious commitment to excel...and to have fun in the process! Although perfect attendance is not mandatory, it is expected that athletes will do their best to attend as often as possible, make the most of each training session, and to be daily contributors to the team. Dryland training begins October 16th (Tu/Th). On-snow training begins with a November camp (11/17-11/25) in Canada and continues at Mt. Spokane on Saturdays and Sundays through April. DEVO athletes have access to night training on Wed/Thurs nights (JAN 3- MAR 7) and speed training before the lifts open to the public. DEVO athletes participate in Club Races, EEYSL, Buddy Werner Championships, select PNSA scored races.", Price = 2045.00, Discount = 100.00, DiscountDesc = "PreSeason", Active = true });
            context.Programs.Add(new Program { ProgramID = 6, Title = "U16 Alpine Team", Description = "The program is designed for committed athletes (ages 14-15) who want the necessary preparation to succeed in PNSA racing and regional championship races. Athletes should love to ski, be willing to employ a strong work ethic, and desire to contribute to the team. SSRA's U16 Alpine Team is an eight month, comprehensive program. Team dryland begins October 2nd and is offered on Tuesdays, Wednesdays, Thursdays, and Fridays. On-snow training starts with fall training camps at Mt. Hood (11/9-11/12) and Banff, Alberta, Canada (11/17-11/25) and continues at Mt. Spokane from the opening to the closing of the resort. U16 training takes place from 9am-2:30pm on weekends or from 7am-1pm when speed training. Mid-week training takes place on Wednesday and Thursday nights (Jan. 3 - Mar. 7) from 5pm-8pm. Athletes enrolled in the SSRA U16 Alpine Team program compete in PNSA divisional racing (DEC-APR) and may qualify for the Western Region U16 Championsips or the CanAm project.", Price = 2680.00, Discount = 130.00, DiscountDesc = "PreSeason", Active = true });
            context.Programs.Add(new Program { ProgramID = 7, Title = "FIS Alpine Team", Description = "The FIS Team is designed for those preparing for PNSA, Western Region, Canadian, and NorAm FIS competition. Competition schedules are tailored to meet the needs of each individual athlete. This program is periodized over 9 months following summer training camps. Dryland training begins Oct. 2nd and is offered on Tuesdays, Wednesdays, Thursdays, and Fridays. On-snow training begins with fall camps at Mt. Hood (11/9-11/12) and Banff, Alberta, Canada (11/17-11/25) and continues at Mt. Spokane from the opening to the closing of the resort. FIS Alpine Team training takes place from 9am-2:30pm on weekends, or from 7am-1pm when speed training. Mid-week training is offered on Wednesday/Thursday nights, from Jan. 3 - Mar 7. SSRA's FIS Alpine Team is for athletes 16+ who want to realize their full potential as scholar athletes.", Price = 3020.00, Discount = 150.00, DiscountDesc = "PreSeason", Active = true });
            context.Programs.Add(new Program { ProgramID = 8, Title = "HOLIDAY CAMP (only)", Description = "", Price = 385.00, Discount = 20.00, DiscountDesc = "PreSeason", Active = true });
            context.Programs.Add(new Program { ProgramID = 9, Title = "MASTERS TRAINING CENTER", Description = "The Masters Training Center has been created for racers 25+ who are interested in refining skills and preparing for PNSA Masters racing. Unlimited gate training is offered on Saturdays, Sundays and Wednesday/Thursday nights...whenever gates are set! Coach feedback is provided by SSRA Junior and Junior Development coaches.", Price = 300.00, Discount = 0.00, DiscountDesc = "", Active = true });

            context.Athletes.Add(new Athlete { 
                AthleteID = 1, 
                FirstName = "John",
                LastName = "Doe", 
                Gender = "M",
                Birthday = DateTime.Now.AddYears(-8), 
                Email = "john.doe@nowhere.com",
                Address = "121 E. Main Ave",
                City = "Spokane",
                State = "WA",
                Postcode = "99201",
                ParentName1 = "Lisa Sampson",
                Parent1Phone1 = "509-222-1111",
                Parent1Phone1Type = "Cell",
                Parent1Phone2 = "571-362-1357",
                Parent1Phone2Type = "Work",
                Grade = "3rd",
                School = "SJV",
                MedicalConditions = "None",
                PresentMedications = "Prozac",
                Allergies = "Minor food Allergies",
                EmergenyContact = "Lisa Sampson",
                EmergenyContactPhone = "509-222-1111",
                MedicalInsurance = "Blue Cross",
                PolicyNum = "A1B2C3D4-11",
                InsurancePhone = "800-372-7722",
                Physician = "Dr Gimbol",
                PhysicianPhone = "509-355-1411"
            });

            context.Athletes.Add(new Athlete
            {
                AthleteID = 2,
                FirstName = "Jan",
                LastName = "Doe",
                Gender = "F",
                Birthday = DateTime.Now.AddMonths(-117),
                Email = "john.doetwo@nowhere.com",
                Address = "1291 S. Riverton St",
                City = "Spokane",
                State = "WA",
                Postcode = "99207",
                ParentName1 = "Andy DoeTwo",
                Parent1Phone1 = "509-111-1222",
                Parent1Phone1Type = "Cell",
                Parent1Phone2 = "509-482-6327",
                Parent1Phone2Type = "Work",
                ParentName2 = "Brenda DoeTwo",
                Parent2Phone1 = "509-482-6328",
                Parent2Phone1Type = "Cell",
                Parent2Phone2 = "509-411-8906",
                Parent2Phone2Type = "Home",
                Grade = "4th",
                School = "SJV",
                MedicalConditions = "N/A",
                PresentMedications = "N/A",
                Allergies = "N/A",
                EmergenyContact = "Andy DoeTwo",
                EmergenyContactPhone = "509-111-1222",
                MedicalInsurance = "Aetna",
                PolicyNum = "2134-33331A1",
                InsurancePhone = "800-654-2222",
                Physician = "Dr Lister",
                PhysicianPhone = "509-475-1782"
            });
            context.VolunteerRoles.Add(new VolunteerRoles { RoleID = 1, Role = "Field Judge #1", Desc = "Judge who wins.", RaceRole = true, Active = true });
            context.VolunteerRoles.Add(new VolunteerRoles { RoleID = 2, Role = "Field Judge #2", Desc = "Judge who wins. Lane 2.", RaceRole = true, Active = true });
            context.VolunteerRoles.Add(new VolunteerRoles { RoleID = 3, Role = "Golf Participant", Desc = "Compete on Golf Course.", OtherRole = true, Active = true });
            context.VolunteerRoles.Add(new VolunteerRoles { RoleID = 4, Role = "Ski Prep/Waxer", Desc = "Prep Skis on Race Day.", RaceRole = true, Active = true });
            context.VolunteerRoles.Add(new VolunteerRoles { RoleID = 5, Role = "Treasurer", Desc = "Fund Management", AdminRole = true, Active = true });
            context.VolunteerRoles.Add(new VolunteerRoles { RoleID = 6, Role = "General Help", Desc = "Avaliable for any commitment", AdminRole = true, RaceRole = true, OtherRole = true, Active = true });
            context.VolunteerRoles.Add(new VolunteerRoles { RoleID = 7, Role = "President", Desc = "Program Head Administration", AdminRole = true, Active = false });

            context.VolunteerEvents.Add(new VolunteerEvents { EventID = 1, EventName = "Golf Tournament", Date = new DateTime(2013, 6, 10), Desc = "Starts 11:30am Compete on the Greens.", OtherRole = true, Active = true });
            context.VolunteerEvents.Add(new VolunteerEvents { EventID = 2, EventName = "Ski-A-Thon", Date = new DateTime(2014, 1, 4), Desc = "Starts 7:00am Compete in the field.", OtherRole = true, Active = true });
            context.VolunteerEvents.Add(new VolunteerEvents { EventID = 3, EventName = "Dinner/Auction", Date = new DateTime(2014, 3, 8), Desc = "Starts 5:00pm A night of fun.", OtherRole = true, Active = true });
            context.VolunteerEvents.Add(new VolunteerEvents { EventID = 4, EventName = "Home Series - Tues, Dec. 31 st", Date = new DateTime(2013, 12, 31), Desc = "", RaceRole = true, Active = true });
            context.VolunteerEvents.Add(new VolunteerEvents { EventID = 5, EventName = "SL Giant Slalom - Sat., Jan. 11 th", Date = new DateTime(2014, 1, 11), Desc = "", RaceRole = true, Active = true });
            context.VolunteerEvents.Add(new VolunteerEvents { EventID = 6, EventName = "SL Giant Slalom - Sun., Jan. 12 th", Date = new DateTime(2014, 1, 12), Desc = "", RaceRole = true, Active = true });
            context.VolunteerEvents.Add(new VolunteerEvents { EventID = 7, EventName = "U16 Champs Qual. #1 - Sat., Jan. 18 th", Date = new DateTime(2014, 1, 18), Desc = "", RaceRole = true, Active = true });
            context.VolunteerEvents.Add(new VolunteerEvents { EventID = 8, EventName = "U16 Champs Qual. #1 - Sun., Jan 19 th", Date = new DateTime(2014, 1, 19), Desc = "", RaceRole = true, Active = true });
            context.VolunteerEvents.Add(new VolunteerEvents { EventID = 9, EventName = "U16 Champs Qual. #1 - Mon., Jan. 20th", Date = new DateTime(2014, 1, 20), Desc = "", RaceRole = true, Active = true });
            context.VolunteerEvents.Add(new VolunteerEvents { EventID = 10, EventName = "Home Series - Sat., Feb. 1 st", Date = new DateTime(2014, 2, 1), Desc = "", RaceRole = true, Active = true });
            context.VolunteerEvents.Add(new VolunteerEvents { EventID = 11, EventName = "Home Series - Sun., Mar. 16 th", Date = new DateTime(2014, 3, 16), Desc = "", RaceRole = true, Active = true });

            context.ReceiptDatas.Add(new ReceiptData
            {
                ReceiptDataID = 1,
                EmailRecipient1 = "leslie.jenner@gmail.com",
                EmailRecipient2 = "zach.jenner@gmail.com",
                HeaderInfo = "Confirmation of Enrollment",
                Footer1Info = "To complete your enrollment, please print, sign, date, and mail this document to the registrar listed on the contact page of this website. Once the registrar receives and records this information, enrollment is complete.",
                Footer2Info = "Sign:  ____________________",
                Footer3Info = "Date: ___/___/______"
            });

            base.Seed(context);

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                string message = e.Message;
            }
        }
    }
}