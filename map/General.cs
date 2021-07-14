using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.Common;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;

namespace map
{
    public partial class General : Form
    {
        MySqlConnectionStringBuilder Connect = new MySqlConnectionStringBuilder();
        dbworker db = new dbworker(bd_CON_VAL.server, bd_CON_VAL.user, bd_CON_VAL.pass, "Liorkin");
        public General()//главное окно выбора города
        {
            InitializeComponent();
            //InitializeComp(this);
            comboBox1.DataSource = db.getTableInfo("SELECT Coordinates_City.id, City.Name FROM Coordinates_City JOIN City ON City_id = City.id;");
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "id";
            gMap.Visible = true;// карта
            button2.Visible = false;//назад
            button3.Visible = false;// сохранить
            groupBox1.Visible = false; //разделы
            groupBox2.Visible = false; //список
            textBox1.Visible = false; //описание города
            button6.Visible = false;
            checkBox3.Visible = false;
            gMap.Position = new PointLatLng(54.313928, 48.40341);
            gMap.Zoom = 11;
            this.ClientSize = new System.Drawing.Size(975, 460);
            
        }
        private void button1_Click(object sender, EventArgs e)// погнали  made with quwin
        {
            this.ClientSize = new System.Drawing.Size(1330, 650);
            comboBox1.Visible = false;//список 7городов и его выбор
            button1.Visible = false;  // погнали
            label1.Visible = false;//выбери город текст
            gMap.Visible = true;// карта
            button2.Visible = true;//назад
            button3.Visible = true;// сохранить
            groupBox1.Visible = true; //разделы
            groupBox2.Visible = true; //список
            button6.Location = new System.Drawing.Point(863, 411);
            button5.Visible = false;
            button4.Visible = false;
            if (User.id == 0) { checkBox3.Visible = false; } else { checkBox3.Visible = true; }
            gMap.Zoom = 11;
            textBox1.Visible = true; //Описание города
            User.City_id = Convert.ToInt32(comboBox1.SelectedValue);
            checkBox3.Checked = db.Visited_Check_City(User.City_id);
            gMap.Position = db.Cordinates(Convert.ToInt32(comboBox1.SelectedValue));
            Information_City();
            
        }
        private void gMap_Load(object sender, EventArgs e)// загрузка карты и ее настройка
        {
            // Настройки для компонента GMap
            gMap.Bearing = 0;
            // Перетаскивание левой кнопки мыши
            gMap.CanDragMap = true;
            // Перетаскивание карты левой кнопкой мыши
            gMap.DragButton = MouseButtons.Left;
            gMap.GrayScaleMode = true;
            // Все маркеры будут показаны
            gMap.MarkersEnabled = true;
            // Максимальное приближение
            gMap.MaxZoom = 17;
            // Минимальное приближение
            gMap.MinZoom = 2;
            // Курсор мыши в центр карты
            gMap.MouseWheelZoomType = MouseWheelZoomType.MousePositionWithoutCenter;
            // Отключение нигативного режима
            gMap.NegativeMode = false;
            // Разрешение полигонов
            gMap.PolygonsEnabled = true;
            // Разрешение маршрутов
            gMap.RoutesEnabled = true;
            // Скрытие внешней сетки карты
            gMap.ShowTileGridLines = false;
            // При загрузке 10-кратное увеличение
            gMap.Zoom = 11;
            // Убрать красный крестик по центру
            gMap.ShowCenter = false;
            // Чья карта используется
            gMap.MapProvider = GMapProviders.GoogleMap;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            // Корды при запуске карыт
        }    
        private void button2_Click(object sender, EventArgs e) // кнопка назад вернутся к меню выбора города
        {
            this.ClientSize = new System.Drawing.Size(970, 460);
            gMap.Visible = true;// карта
            comboBox1.Visible = true; //список 7городов и его выбор
            button1.Visible = true; // погнали
            label1.Visible = true; //выбери город текст
            button2.Visible = false;//назад
            button3.Visible = false;// сохранить
            groupBox1.Visible = false; //разделы
            groupBox2.Visible = false; //список 
            checkBox3.Visible = false;
            button5.Visible = true;
            button4.Visible = true;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox6.Checked = false;
            checkBox9.Checked = false;
            checkBox5.Checked = false;
            button6.Location = new System.Drawing.Point(863, 348);
            gMap.Overlays.Remove(ListOfFood);
            ListOfFood.Clear();
            gMap.Overlays.Remove(ListOfFlat);
            ListOfFlat.Clear();
            gMap.Overlays.Remove(ListOfPlace);
            ListOfPlace.Clear();
            gMap.Overlays.Remove(ListOfHotel);
            ListOfHotel.Clear();
            gMap.Overlays.Remove(ListOfPoster);
            ListOfPoster.Clear();
            for (int i = panel1.Controls.Count - 1; i >= 0; i += -1)
            {
                if (panel1.Controls[i].Tag.ToString() == "Place")
                {
                    panel1.Controls.Remove(panel1.Controls[i]);
                }
            }
            for (int i = panel1.Controls.Count - 1; i >= 0; i += -1)
            {
                if (panel1.Controls[i].Tag.ToString() == "Food")
                {
                    panel1.Controls.Remove(panel1.Controls[i]);
                }
            }
            for (int i = panel1.Controls.Count - 1; i >= 0; i += -1)
            {
                if (panel1.Controls[i].Tag.ToString() == "Flat")
                {
                    panel1.Controls.Remove(panel1.Controls[i]);
                }
            }
            for (int i = panel1.Controls.Count - 1; i >= 0; i += -1)
            {
                if (panel1.Controls[i].Tag.ToString() == "Hotel")
                {
                    panel1.Controls.Remove(panel1.Controls[i]);
                }
            }
            for (int i = panel1.Controls.Count - 1; i >= 0; i += -1)
            {
                if (panel1.Controls[i].Tag.ToString() == "Poster")
                {
                    panel1.Controls.Remove(panel1.Controls[i]);
                }
            }
            textBox1.Visible = false;
            pictureBox2.Image = null;
            textBox1.Clear();
        }


        public void Information_City()
        {
            Bitmap image;
            Bitmap image1;
            switch (comboBox1.Text)
            { 
                case "Ульяновск":
                    textBox1.Text = "Ульяновск — административный центр Ульяновской области Российской Федерации; важный промышленный и культурный центр Поволжья;" +
                    " узел железных и автомобильных дорог (на Москву, Казань, Уфу, Саратов); речной порт; авиалиниями связан со многими " +
                    "городами страны и зарубежья. Расположен на берегу Куйбышевского водохранилища, в 893 км к юго-востоку от Москвы." +
                    " Население – около 700 тыс. человек. Разделен на 4 района. Имеет радиально-прямоугольную планировку.\nСтоимость проезда на маршрутке - от 20 до 27 рублей. Стоимость автобуса - 24 рубля.";

                    image = new Bitmap(@"Icon/Ulsk1.jpg");
                    pictureBox2.Image = image;
                    pictureBox2.Invalidate();

                    break;
                case "Москва":
                    textBox1.Text = "Москва – один из самых привлекательных в туристическом плане городов мира, поражающий гостей своей историей, неимоверным количеством всевозможных архитектурных" +
                        " достопримечательностей, музеев, театров, галерей, которые не сможет перечислить ни один искусствовед. В то же время Москва – город глубоких контрастов. Здесь можно увидеть " +
                        "очередь за «Бэнтли» и нищих в переходах метро, деревянные избы в тупиках Китай-города и стеклянные небоскрёбы Москва-Сити, ощутить деревенскую тишину переулков Замоскворечья " +
                        "и шум мегаполиса в деловых кварталах.\nСтоимость проезда на маршрутке - от 25 до 30 рублей. Стоимость метро - 40 рублей"; 
                    
                    image1 = new Bitmap(@"Icon/Moscow.png");
                    pictureBox2.Image = image1;
                    pictureBox2.Invalidate();

                    break;
                case "Санкт-Петербург":   
                default:
                    textBox1.Text = "Санкт-Петербург - самый северный из числа крупнейших городов мира. Его географические координаты - 59°57' северной широты и 30°19' " +
                        "восточной долготы. 60-я параллель, на которой находится северная столица России, проходит через Гренландию, Аляску и столицу Норвегии Осло. Площадь " +
                        "города составляет 606 кв. км, а с пригородами -1451 кв. км. Петербург делится на 19 административных районов и 111 муниципальных образований.\nСтоимость проезда на маршрутке - от 30 до 35 рублей. Стоимость метро - 40 рублей";

                    image1 = new Bitmap(@"Icon/Peterburg.jpg");
                    pictureBox2.Image = image1;
                    pictureBox2.Invalidate();

                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e) // скрин карты в текушем месте
        {
            try
            {
                using (SaveFileDialog dialogforsavemap = new SaveFileDialog())
                {
                    // Формат картинки
                    dialogforsavemap.Filter = "PNG (*.png)|*.png";

                    // Название картинки
                    dialogforsavemap.FileName = "Текущее положение карты";

                    Image image = gMap.ToImage();

                    if (image != null)
                    {
                        using (image)
                        {
                            if (dialogforsavemap.ShowDialog() == DialogResult.OK)
                            {
                                string fileName = dialogforsavemap.FileName;
                                if (!fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                                    fileName += ".png";

                                image.Save(fileName);
                                MessageBox.Show("Карта успешно сохранена в директории: " + Environment.NewLine + dialogforsavemap.FileName, "GMap.NET", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                        }
                    }
                }
            }

            // Если ошибка
            catch (Exception exception)
            {
                MessageBox.Show("Ошибка при сохранении карты: " + Environment.NewLine + exception.Message, "GMap.NET", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        GMapOverlay ListOfFood = new GMapOverlay("Eда");//еда
        List<Coord> ListWithPoinsOfFood = new List<Coord>();
       
        private void checkBox1_Click(object sender, EventArgs e)
        {
            gMap.Overlays.Add(ListOfFood);
            if (checkBox1.Checked)
            {
                ListWithPoinsOfFood = db.Food(1);

                for (int i = 0; i < ListWithPoinsOfFood.Count; i++)
                {
                    GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(ListWithPoinsOfFood[i].x, ListWithPoinsOfFood[i].y), new Bitmap(@"Icon/food.png"));
                    marker.ToolTip = new GMapRoundedToolTip(marker);

                    marker.ToolTipText = ListWithPoinsOfFood[i].name;
                    ListOfFood.Markers.Add(marker);

                    Point a = new Point(ListWithPoinsOfFood[i], gMap);
                    a.Tag = "Food";
                    panel1.Controls.Add(a);
                }
            }
            else
            {
                gMap.Overlays.Remove(ListOfFood);
                ListOfFood.Clear();
                for(int i = panel1.Controls.Count - 1; i >= 0; i += -1)
                {
                    if (panel1.Controls[i].Tag.ToString() == "Food")
                    {
                        panel1.Controls.Remove(panel1.Controls[i]);
                    }
                }

            }
        }

        GMapOverlay ListOfPlace = new GMapOverlay("Места");//места
        List<Coord> ListWithPoinsOfPlace = new List<Coord>();

        private void checkBox6_Click(object sender, EventArgs e)
        {
            gMap.Overlays.Add(ListOfPlace);
            if (checkBox6.Checked)
            {
                ListWithPoinsOfPlace = db.Food(2);

                for (int i = 0; i < ListWithPoinsOfPlace.Count; i++)
                {
                    GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(ListWithPoinsOfPlace[i].x, ListWithPoinsOfPlace[i].y), new Bitmap(@"Icon/statue.png"));
                    marker.ToolTip = new GMapRoundedToolTip(marker);

                    marker.ToolTipText = ListWithPoinsOfPlace[i].name;
                    ListOfPlace.Markers.Add(marker);

                    Point a = new Point(ListWithPoinsOfPlace[i], gMap);
                    a.Tag = "Place";
                    panel1.Controls.Add(a);
                }
            }
            else
            {

                gMap.Overlays.Remove(ListOfPlace);
                ListOfPlace.Clear();
                for (int i = panel1.Controls.Count - 1; i >= 0; i += -1)
                {
                    if (panel1.Controls[i].Tag.ToString() == "Place")
                    {
                        panel1.Controls.Remove(panel1.Controls[i]);
                    }
                }
            }
        }

        GMapOverlay ListOfFlat = new GMapOverlay("Квартиры");//квартиры
        List<Coord> ListWithPoinsOfFlat = new List<Coord>();

        private void checkBox9_Click(object sender, EventArgs e)
        {
            gMap.Overlays.Add(ListOfFlat);
            if (checkBox9.Checked)
            {
                ListWithPoinsOfFlat = db.Food(4);

                for (int i = 0; i < ListWithPoinsOfFlat.Count; i++)
                {
                    GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(ListWithPoinsOfFlat[i].x, ListWithPoinsOfFlat[i].y), new Bitmap(@"Icon/house.png"));
                    marker.ToolTip = new GMapRoundedToolTip(marker);

                    marker.ToolTipText = "Квартира";
                    ListOfFlat.Markers.Add(marker);

                    Point a = new Point(ListWithPoinsOfFlat[i], gMap);
                    a.Tag = "Flat";
                    panel1.Controls.Add(a);
                }
            }
            else
            {
                gMap.Overlays.Remove(ListOfFlat);
                ListOfFlat.Clear();
                for (int i = panel1.Controls.Count - 1; i >= 0; i += -1)
                {
                    if (panel1.Controls[i].Tag.ToString() == "Flat")
                    {
                        panel1.Controls.Remove(panel1.Controls[i]);
                    }
                }
            }
        }

        GMapOverlay ListOfHotel = new GMapOverlay("Гостиница");//гостиница
        List<Coord> ListWithPoinsOfHotel = new List<Coord>();

        private void checkBox5_Click(object sender, EventArgs e)
        {
            gMap.Overlays.Add(ListOfHotel);
            if (checkBox5.Checked)
            {
                ListWithPoinsOfHotel = db.Food(3);

                for (int i = 0; i < ListWithPoinsOfHotel.Count; i++)
                {
                    GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(ListWithPoinsOfHotel[i].x, ListWithPoinsOfHotel[i].y), new Bitmap(@"Icon/hotel.png"));
                    marker.ToolTip = new GMapRoundedToolTip(marker);

                    marker.ToolTipText = ListWithPoinsOfHotel[i].name;
                    ListOfHotel.Markers.Add(marker);

                    Point a = new Point(ListWithPoinsOfHotel[i], gMap);
                    a.Tag = "Hotel";
                    panel1.Controls.Add(a);
                }
            }
            else
            {
                gMap.Overlays.Remove(ListOfHotel);
                ListOfHotel.Clear();
                for (int i = panel1.Controls.Count - 1; i >= 0; i += -1)
                {
                    if (panel1.Controls[i].Tag.ToString() == "Hotel")
                    {
                        panel1.Controls.Remove(panel1.Controls[i]);
                    }
                }
            }
        }

        GMapOverlay ListOfPoster = new GMapOverlay("Афиша");//афиша
        List<Coord> ListWithPoinsOfPoster = new List<Coord>();

        private void checkBox2_Click(object sender, EventArgs e)
        {
            gMap.Overlays.Add(ListOfPoster);
            if (checkBox2.Checked)
            {
                ListWithPoinsOfPoster = db.Food(5);

                for (int i = 0; i < ListWithPoinsOfPoster.Count; i++)
                {
                    GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(ListWithPoinsOfPoster[i].x, ListWithPoinsOfPoster[i].y), new Bitmap(@"Icon/party.png"));
                    marker.ToolTip = new GMapRoundedToolTip(marker);

                    marker.ToolTipText = ListWithPoinsOfPoster[i].name;
                    ListOfPoster.Markers.Add(marker);

                    Point a = new Point(ListWithPoinsOfPoster[i], gMap);
                    a.Tag = "Poster";
                    panel1.Controls.Add(a);
                }
            }
            else
            {
                gMap.Overlays.Remove(ListOfPoster);
                ListOfPoster.Clear();
                for (int i = panel1.Controls.Count - 1; i >= 0; i += -1)
                {
                    if (panel1.Controls[i].Tag.ToString() == "Poster")
                    {
                        panel1.Controls.Remove(panel1.Controls[i]);
                    }
                }
            }
        }
        private void gMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if ((User.id != 0) && (User.lvl != 1) && button2.Visible)
            {
                Markers a = new Markers(gMap.FromLocalToLatLng(e.X, e.Y).Lng, gMap.FromLocalToLatLng(e.X, e.Y).Lat);
                a.ShowDialog();
            }
            else 
            {
                MessageBox.Show("Для размещения объекта необходимо оформить подписку \"Арендодатель\" или \"Бизнес\" ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            gMap.Position = db.Cordinates(Convert.ToInt32(comboBox1.SelectedValue));
            gMap.Zoom = 11;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            register reg = new register();
            reg.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.ShowDialog();
            if (User.id != 0) 
            {
                button6.Visible=true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UsrCab lk = new UsrCab(User.id);
            lk.ShowDialog(); 
        }

        private void checkBox3_Click(object sender, EventArgs e)
        {
            
            db.Visited_City(User.City_id, checkBox3.Checked);
        }
    }
}
