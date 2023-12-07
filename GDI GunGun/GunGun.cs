using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GDI_GunGun
{
    public partial class GunGun : Form
    {
        int index = 0;
        Timer timer = new Timer();
        Timer timer2 = new Timer();
        Bitmap backBuffer;
        Bitmap sprite;
        Graphics graphics;
        int curFrameColumn;
        int curFrameRow;
        List<Rock> RockList = new List<Rock>();
        Image potato = Image.FromFile("Images/pomatoo.png");
        Image spaceship = Image.FromFile("Images/spaceship.png");
        Image bul = Image.FromFile("Images/bullet.png");
        Random random = new Random();

        // khoi tao 1 spaceship
        SpaceShip space = new SpaceShip(200, 450, 100, 80);
        
        List<Bullet> bulletList = new List<Bullet>();
        public GunGun() // hàm khởi tạo 
        {

            InitializeComponent();
            // sự kiện keyboard
            KeyDown += Keyboard.OnKeyDown;
            KeyUp += Keyboard.OnKeyUp;

            // giải quyết nháy hình
            DoubleBuffered = true;

            // khởi tạo đồng hồ

            timer.Enabled = true;
            timer.Interval = 100;
            timer.Tick += new EventHandler(timer_Tick);
          
            timer.Start();

            // KHỞI TẠO ĐỒNG HỒ 2
            timer2.Enabled = true;
            timer2.Interval = 3000;
            timer2.Tick += new EventHandler(timer2_Tick);

            

            // khởi tạo graphics
            graphics = this.CreateGraphics();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            backBuffer = new Bitmap( (this.ClientSize.Width),  (this.ClientSize.Height));
            sprite = new Bitmap("Images/stockExploision-removebg-preview.png");


            



        }
      
        
        private void timer_Tick(object sender, EventArgs e)
        {

            /* Render(60, 60);
             graphics.DrawImage(backBuffer ,0, 0);*/

            Move();
            Refresh();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
           
             
            RockList.Add(new Rock(random.Next(5,this.Width-40), -30));

        }

      
     



        private void GunGun_Paint(object sender, PaintEventArgs e)
        {
            // repaint potato
            foreach (Rock rock in RockList)
            {
                rock.y += rock.speed;
                // delete out window potato
                  

                e.Graphics.DrawImage(potato, (float)rock.x, (float)rock.y,55,75);

                

            }
            // repaint ship
        
            e.Graphics.DrawImage(spaceship, (float)space.x, (float)space.y, 100, 80);
          
          // paint bullet
            foreach( Bullet bullet in bulletList)
            {
                bullet.y-= bullet.speed;
                e.Graphics.DrawImage(bul, (float)bullet.x,(float) bullet.y, 70, 70);
            }    
        }
        private void GunGun_Load(object sender, EventArgs e)
        {
            
        }
        private void Render(int x, int y)
        {
            // Lấy đối tượng graphics để vẽ lên back buffer
            Graphics g = Graphics.FromImage(backBuffer);
            // g.Clear(Color.White);

            // Xác dịnh số dòng, cột của một frame trên ảnh sprite

            curFrameColumn = index % 5;
            curFrameRow = index / 5;
            // Vẽ lên buffer

            g.DrawImage(sprite, x, y, new Rectangle(curFrameColumn * 158, curFrameRow * 158, 158, 158), GraphicsUnit.Pixel);
            g.Dispose();


            // Tăng thứ tự frame để lấy frame tiếp theo

            index++;

        }
        private void Exploision()
        {

            // kiểm tra va chạm . 
            // kiểm tra tọa độ của các rock so với ship 
            foreach (Rock rock in RockList)
            {
                if (rock.y > space.y-35 && Math.Abs(rock.x -space.x)<30)
                {

                    Render((int)space.x-20, (int)space.y-20);
                    graphics.DrawImageUnscaled(backBuffer, 0, 0);

                    return;


                }

            }

            foreach (Bullet bul in bulletList)
            {
                foreach (Rock rock in RockList)
                {
                    if (bul.x > rock.x-10 &&  bul.x < rock.x+10 && bul.y < rock.y+5)
                    {
                        Render((int)rock.x, (int)rock.y);
                        graphics.DrawImageUnscaled(backBuffer, 0, 0);
                        RockList.Remove(rock);
                        return;
                    }
                }
            }

        }
        private void Move()
        {
            int x = 9;

            if( Keyboard.IsKeyDown(Keys.Up) )
            {
                if ( space.y >0  )
                {
                    space.y -=x;
                }    
            }    
            if( Keyboard.IsKeyDown(Keys.Down) )
            {
                if(space.y < this.Height )
                {
                    space.y+=x;
                }    
            }    
            if(Keyboard.IsKeyDown(Keys.Left ))
             {
                if( space.x > 0)
                {
                    space.x-=x;
                }    
            }
            if(Keyboard.IsKeyDown (Keys.Right ))
            {
                if(space.x<this.Width)
                {
                    space.x+=x;
                }    
            } 
            if(Keyboard.IsKeyDown(Keys.Space))
            {
                Bullet but = new Bullet(space.x, space.y);
                bulletList.Add(but);
            }    
        }

        /// <summary>
        /// Bỏ cái key vô hashset để xử lí sự kiện keyboard mượt hơn 
        /// </summary>
        public static class Keyboard
        {
            private static readonly HashSet<Keys> keys = new HashSet<Keys>();

            public static void OnKeyDown(object sender, KeyEventArgs e)
            {
                if (keys.Contains(e.KeyCode) == false)
                {
                    keys.Add(e.KeyCode);
                }
            }

            public static void OnKeyUp(object sender, KeyEventArgs e)
            {
                if (keys.Contains(e.KeyCode))
                {
                    keys.Remove(e.KeyCode);
                }
            }

            public static bool IsKeyDown(Keys key)
            {
                return keys.Contains(key);
            }
        }

        private void GunGun_KeyUp(object sender, KeyEventArgs e)
        {
            Keyboard.OnKeyUp(sender, e);
        }

        private void GunGun_KeyDown(object sender, KeyEventArgs e)
        {
            Keyboard.OnKeyDown(sender, e);
        }
        
    }
        
}
