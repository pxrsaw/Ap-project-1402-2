using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace UserManagementSystem
{
    public class user
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string? gender { get; set; }
        public string? postalCode { get; set; }
        public static List<user> AllUsers = new List<user>();

        public user(string username, string password, string email, string phoneNumber)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.phoneNumber = phoneNumber;

        }
        public static user Login(string usn, string pw)
        {
            foreach (adm ad in adm.AllAdmins)
            {
                if (ad.username == usn && ad.password == pw)
                {
                    return ad;
                }
            }
            foreach (RegularUser ad in RegularUser.AllRegularUsers)
            {
                if (ad.username == usn && ad.password == pw)
                {
                    return ad;
                }
            }
            foreach (Restaurant ad in Restaurant.AllRestaurants)
            {
                if (ad.username == usn && ad.password == pw)
                {
                    return ad;
                }
            }
            return null;
        }
        public void Edit(string emaill,string postalc)
        {
            email= emaill;
            phoneNumber= postalc;
        }


    }
    public class adm : user
    {
        public static List<adm> AllAdmins = new List<adm>();
        public static List<FeedBack> AllFeedBacks= new List<FeedBack>();
        public adm(string username, string password, string email, string phoneNumber) : base(username, password, email, phoneNumber)
        {
            AllAdmins.Add(this);
            AllUsers.Add(this);
        }
        //public static adm Sign_Up(string usernamee, string passwordd,string emaill,string pn)
        //{
        //    foreach(user u1 in AllUsers)
        //    {
        //        if(u1.username==usernamee || u1.phoneNumber==pn)
        //        {
        //            return null;
        //        }
        //    }
        //    return new adm(usernamee,passwordd,emaill,pn);

        //}

    }
    public class RegularUser : user
    {
        
        public static List<RegularUser> AllRegularUsers = new List<RegularUser>();
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int userType { get; set; }
        public int AvailableReserves { get; set; }
        public List<Order> AllUserOrders { get; set; }
        public List<Reserve> AllUserReserves { get; set; }
        public Reserve ActiveReserve { get; set; }
        public List<FeedBack> feedBacks { get; set; }
        public DateTime? PremiumExpiration { get; set; }
        public Dictionary<Food,float> ScoredFoods { get; set; }
        //public float Wallet;
        public RegularUser(string fn, string ln,string username, string password, string email, string phoneNumber) : base(username, password, email, phoneNumber)
        {
            firstName= fn;
            lastName= ln;
            AllUserReserves = new List<Reserve>();
            userType = 0;
            PremiumExpiration = null;
            ActiveReserve = null;
            AvailableReserves = 0;
            AllUserOrders = new List<Order>();
            feedBacks = new List<FeedBack>();
            ScoredFoods= new Dictionary<Food,float>();
            //Wallet = 0;
            AllRegularUsers.Add(this);
            AllUsers.Add(this);
        }
        //func Profile()
        //public static RegularUser Sign_Up(string fn,string ln,string usernamee, string passwordd, string emaill, string pn)
        //{
        //    foreach (user u1 in AllUsers)
        //    {
        //        if (u1.username == usernamee || u1.phoneNumber == pn)
        //        {
        //            return null;
        //        }
        //    }
        //    return new RegularUser(fn,ln,usernamee, passwordd, emaill, pn);

        //}
        public void UpgradeReserves()
        {
            if(DateTime.Now>ActiveReserve.reserveDate)
            {
                ActiveReserve = null;
            }
            if(AvailableReserves==0&&ActiveReserve==null)
            {
                PremiumExpiration = null;
                userType = 0;
            }
        }
        //public void ChargeWallet(float money)
        //{
        //   //Wallet += money;
        //}
        public static List<Restaurant> SearchByCity(List<Restaurant> rs, string cty)
        {

            foreach (Restaurant r1 in rs.ToList())
            {
                if (r1.city.ToLower() != cty)
                {
                    rs.Remove(r1);
                }
            }
            return rs;
        }
        public static List<Restaurant> SearchByName(List<Restaurant> rs, string nm)
        {
            
            foreach (Restaurant r1 in rs.ToList())
            {
                if (r1.Name.ToLower().Contains(nm.ToLower()) == false)
                {
                    rs.Remove(r1);
                }
            }
            return rs;
        }
        public static List<Restaurant> SearchByDineIn(List<Restaurant> rs)
        {

            foreach (Restaurant r1 in rs.ToList())
            {
                if (r1.isDine_in == false)
                {
                    rs.Remove(r1);
                }
            }
            return rs;
        }
        public static List<Restaurant> SearchByDelivery(List<Restaurant> rs)
        {

            foreach (Restaurant r1 in rs.ToList())
            {
                if (r1.isDelivery == false)
                {
                    rs.Remove(r1);
                }
            }
            return rs;
        }
        public static List<Restaurant> SearchByScore(double sc, List<Restaurant> rs)
        {

            foreach (Restaurant r1 in rs.ToList())
            {
                if (r1.restaurantScore < sc)
                {
                    rs.Remove(r1);
                }
            }
            return rs;
        }
        public void orderFood(Dictionary<Food, int> ordf, Restaurant r2,bool ic)
        {
            Order o1 = new Order(this, r2, ordf,ic);
            r2.orders.Add(o1);
            AllUserOrders.Add(o1);
        }
        public void AddFeedback(Restaurant rs, string ti, string des)
        {
            FeedBack fb = new FeedBack(rs,this, ti, des);
            adm.AllFeedBacks.Add(fb);
            //feedBacks.Add(fb);
        }
    }
    public class Restaurant : user
    {
        public string Name { get; set; }
        public List<Food> Menu { get; set; }
        //public List<Food> AvailableFoods;
        public List<Order> orders { get; set; }
        public List<Reserve> reserves { get; set; }
        public bool IsReservable { get; set; }
        public string city { get; set; }
        public bool isDine_in { get; set; }
        public bool isDelivery { get; set; }
        public string address { get; set; }
        public float restaurantScore { get; set; }
        public static List<Restaurant> AllRestaurants = new List<Restaurant>();
        public float Wallet { get; set; }
        public List<float> AllScores { get; set; }
        public Restaurant(string nm, string username, string password, string email, string phoneNumber,string city, bool idi, bool idl, string addr) : base(username, password, email, phoneNumber)
        {
            Name = nm;
            Menu = new List<Food>();
            //AvailableFoods = new List<Food>();
            reserves = new List<Reserve>();
            orders = new List<Order>();
            IsReservable = false;
            this.city = city;
            isDine_in = idi;
            isDelivery = idl;
            address = addr;
            restaurantScore = 0;
            AllRestaurants.Add(this);
            AllUsers.Add(this);
            AllScores=new List<float>();
            Wallet = 0;
        }
        public void UpdateScore(float f)
        {
            AllScores.Add(f);
            float sum = 0;
            foreach(float f1 in AllScores)
            {
                sum+= f1;
            }
            sum/=AllScores.Count;
            restaurantScore=sum;
            if(restaurantScore >= 4.5)
            {
                IsReservable=true;
            }
            else
            {
                IsReservable=false;
            }
        }

        public static Restaurant Sign_Up(string nm,string usernamee, string passwordd, string emaill, string pn,string cty,bool idii,bool idll,string addrr)
        {
            foreach (user u1 in AllUsers)
            {
                if (u1.username == usernamee || u1.phoneNumber == pn)
                {
                    return null;
                }
            }
            return new Restaurant(nm,usernamee, passwordd, emaill, pn,cty,idii,idll,addrr);

        }
        public void RemoveFood(Food fd3)
        {
            //Menu.Remove(fd3);
            foreach(var mndc in Menu)
            {
                
                if (mndc == fd3)
                {
                    Menu.Remove(fd3);
                    break;
                }
            }
        }
        public void AddFood(Food fd3)
        {
            Menu.Add(fd3);
            //int ch = 0;
            //foreach(var menurow in Menu)
            //{
            //    if(menurow.Key==fd3.Type)
            //    {
            //        menurow.Value.Add(fd3);
            //        ch = 1;
            //        break;
            //    }
            //}
            //if(ch == 0)
            //{
            //    Food[] fcv= new Food[1];
            //    fcv[0] = fd3;
            //    Menu.Add(fd3.Type, new List<Food>(fcv));
            //}
        }
        public void ChangeFood(Food fd3)
        {
            //inja
        }
        public void ChangeInventory(Food fd3,int num)
        {
            fd3.Inventory = num;
        }

    }
    
    public class Order
    {
        public RegularUser rus { get; set; }
        public Restaurant res { get; set; }
        public Dictionary<Food,int> orderFoods { get; set; }
        public float Price { get; set; }
        public float? Score { get; set; }
        public DateTime orderDate { get; set; }
        public List<Comment> comments { get; set; }
        public bool IsCash { get; set; }
        public Order(RegularUser rus, Restaurant res, Dictionary<Food,int> orderFoods, bool isCash)
        {
            this.rus = rus;
            this.res = res;
            this.orderFoods = orderFoods;
            comments = new List<Comment>();
            orderDate = DateTime.Now;
            IsCash = isCash;
            Price = 0;
            foreach(var row in orderFoods)
            {
                Price += ((row.Key.Price)*(row.Value));
            }
            res.Wallet += Price;
            //rus.Wallet-=Price;
        }
        public void AddScore(float f2)
        {
            Score = f2;
        }
        public void AddComment(string cm)
        {
            Comment cm2 = new Comment(cm, rus, this);
            comments.Add(cm2);
        }
        
    }
    public class Reserve
    {
        public RegularUser rus { get; set; }
        public Restaurant res { get; set; }
        public float? Score { get; set; }
        public DateTime reserveDate { get; set; }
        public Reserve(RegularUser rus, Restaurant res,DateTime reserveDate)
        {
            this.rus = rus;
            this.res = res;
            Score = null;
            this.reserveDate = reserveDate;
        }
        public string AddReserve(RegularUser rus, Restaurant res, DateTime reserveDate)
        {
            rus.UpgradeReserves();
            if(res.IsReservable==false)
            {
                return "restaurant not reservable";
            }
            else if(rus.ActiveReserve!=null)
            {
                return "you have active reservation";
            }
            else if(rus.userType==0)
            {
                return "you are not premium user";
            }
            else if(rus.AvailableReserves==0)
            {
                return "you are not premium user";
            }
            else
            {
                Reserve rs1=new Reserve(rus, res, reserveDate);
                rus.ActiveReserve=rs1;
                rus.AvailableReserves--;
                rus.UpgradeReserves();
                rus.AllUserReserves.Add(rs1);
                res.reserves.Add(rs1);
                return "reserved successfully";
            }
        }
        public string CancelReserve()
        {
            rus.UpgradeReserves();
            if(rus.ActiveReserve==null)
            {
                return "no active reserve";
            }
            TimeSpan ts1= rus.ActiveReserve.reserveDate-DateTime.Now;
            if(rus.userType==1)
            {
                if(ts1.Minutes>30)
                {
                    res.Wallet -= 15;
                   // rus.Wallet += 15;
                    rus.ActiveReserve = null;
                    return "reserve canceled successfully";
                }
                else
                {
                    rus.ActiveReserve = null;
                    return "reserve canceled successfully";
                }
            }
            if(rus.userType==2)
            {
                if (ts1.Minutes > 30)
                {
                    res.Wallet -= 9;
                   // rus.Wallet += 9;
                    rus.ActiveReserve = null;
                    return "reserve canceled successfully";
                }
                else
                {
                    rus.ActiveReserve = null;
                    return "reserve canceled successfully";
                }
            }
            if (rus.userType == 3)
            {
                if (ts1.Minutes > 15)
                {
                    res.Wallet -= 6;
                    //rus.Wallet += 6;
                    rus.ActiveReserve = null;
                    return "reserve canceled successfully";
                }
                else
                {
                    rus.ActiveReserve = null;
                    return "reserve canceled successfully";
                }
            }
            else
            {
                return "";
            }
        }
    }
    public class Food
    {
        public string Name { get; set; }
        public string Materials { get; set; }
        public string Type { get; set; }
        //inja-image
        public float Score { get; set; }
        public int Inventory { get; set; }
        public float Price { get; set; }
        public List<Comment> Comments { get; set; }
        public List<float> Prices { get; set; }
        public void UpdateScores(float f2)
        {
            Prices.Add(f2);
            float sum = Prices.Sum(r => r);
            Score = sum / Prices.Count;
        }
        public Food(string name, string materials, string type, int inventory,float price)
        {
            Name = name;
            Materials = materials;
            Type = type;
            Score = 0;
            Comments = new List<Comment>();
            Inventory = inventory;
            Price = price;
            Prices=new List<float>();
        }
        public void AddComment(string cm,RegularUser ru1,float user_score)
        {
            float us1=user_score;
            foreach(Comment c in Comments)
            {
                if(c.ru==ru1)
                {
                    us1 = (float)c.userScore;
                    
                    break;
                }
            }
            Comment newcm1 = new Comment(cm, ru1, this, us1);
            Comments.Add(newcm1);
        }
    }
    public class Comment
    {
        public string cm { get; set; }
        public RegularUser ru { get; set; }
        public Food fd { get; set; }
        public Order ordercm { get; set; }
        public float? userScore { get; set; }
        public bool isEdited { get; set; }
        public DateTime AddedTime { get; set; }
        public List<Comment> answerComments { get; set; }
        public Comment(string cm, RegularUser ru, Food fdd, float us)
        {
            this.cm = cm;
            this.ru = ru;
            fd = fdd;
            ordercm = null;
            userScore = us;
            isEdited = false;
            AddedTime=DateTime.Now;
            answerComments = new List<Comment>();
        }
        public Comment(string cm, RegularUser ru, Order orderr)
        {
            this.cm = cm;
            this.ru = ru;
            fd = null;
            ordercm = orderr;
            userScore = null;
            isEdited = false;
            AddedTime = DateTime.Now;
            
        }

        public void EditComment(string newcm)
        {
            cm=newcm;
            isEdited=true;
        }
        public void DeleteComment()
        {
            answerComments=new List<Comment>();
            fd.Comments.Remove(this);
        }
    }
    public class FeedBack
    {
        public Restaurant restaurant { get; set; }
        public RegularUser Feedbackuser { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool isAnswered { get; set; }
        public string? answer { get; set; }
        public FeedBack(Restaurant restaurant,RegularUser regular, string title, string description)
        {
            this.restaurant = restaurant;
            this.title = title;
            this.description = description;
            isAnswered= false;
            answer=null;
            this.Feedbackuser = regular;
            regular.feedBacks.Add(this);
            adm.AllFeedBacks.Add(this);
        }
        public void answerFeedback(string feedback) 
        {
            isAnswered = true;
            answer = feedback;
        }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            RegularUser ru1 = new RegularUser("", "", "parsa", "parsa", "pxrsaaw@gmail.com", "09");
            new adm("parsa2", "parsa2", "", "");
            var res1 = new Restaurant("hello1", "res1", "res1", "", "", "tehran", false, false, "");
            Food fd4 = new Food("hh", "jj", "Food", 6, 78);
            Food fd7=new Food("hh", "jj", "Drink", 6, 78);
            Food fd8 = new Food("hh", "jj", "Appetizer", 6, 78);
            Food fd9 = new Food("hh", "jj", "Dessert", 6, 78);

            res1.Menu.Add(fd4);
            res1.Menu.Add(fd7);
            res1.Menu.Add(fd8);
            res1.Menu.Add(fd9);
            FeedBack fb1 = new FeedBack(res1, ru1, "razi", "nmd");
            //Food[] fd5 = new Food[1];
            //fd5[0] = fd4;
            //res1.Menu.Add(fd4);
            InitializeComponent();
        }

        

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            //Login
            //string a = UsernameTextBox.Text;
            //NavigationService.Navigate(new RestaurantMenuWindow());
            //int n = 10;
            //var window = new Customer();
            //Close();
            //window.Show();
            
            
            string UserName_ = UsernameTextBox.Text;
            string Password_ = PasswordTextBox.Text;
            var us1=user.Login(UserName_, Password_);
            if (us1 is RegularUser)
            {
                RegularUser ru2 = us1 as RegularUser;
                var window = new Customer(ru2);
                Close();
                window.Show();
            }
            if (us1 is adm)
            {
                adm ru2 = us1 as adm;
                var window = new AdminPanelWindow(ru2);
                Close();
                window.Show();
            }
            if (us1 is Restaurant)
            {
                Restaurant ru3 = us1 as Restaurant;
                var window = new RestaurantMenuWindow(ru3);
                Close();
                window.Show();
            }
            if(us1 is null)
            {
                MessageBox.Show("incorrect username/Password");
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            var signwind = new signup();
            Close();
            signwind.Show();
        }
    }
}
