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
        Timer timer3 = new Timer();
        Bitmap backBuffer;
        Bitmap sprite = new Bitmap("Images/stockExploision .png");
        Graphics graphics;
        int curFrameColumn;
        int curFrameRow;
        List<Rock> RockList = new List<Rock>();
        Image potato = Image.FromFile("Images/pomatoo.png");
        Image spaceship = Image.FromFile("Images/spaceship.png");
        Image bul = Image.FromFile("Images/bullet.png");
        Random random = new Random();
        // khởi tạo vị trí vụ nổ 
        int x, y, flag=0;

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
            timer.Stop();
            timer.Interval = 100;
            timer.Tick += new EventHandler(timer_Tick);
          
            timer.Start();

            // KHỞI TẠO ĐỒNG HỒ 2
            timer2.Enabled = true;
            
            timer2.Interval = 3000;
            timer2.Tick += new EventHandler(timer2_Tick);

            // KHỞI TẠO ĐỒNG HỒ 3
         
            timer3.Interval = 60;
 
            timer3.Tick += new EventHandler(timer3_Tick);

            // khởi tạo graphics
            graphics=this.CreateGraphics();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            backBuffer = new Bitmap( (this.ClientSize.Width),  (this.ClientSize.Height));
            


            



        }
      
        
        private void timer_Tick(object sender, EventArgs e)
        {

         
            Exploision();
            Move();
            Refresh();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
           
             
            RockList.Add(new Rock(random.Next(5,this.Width-80),-10));

            

        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            Render();
            graphics.DrawImageUnscaled(backBuffer, 0, 0);
             flag++;
            if (flag>10)
            {
                flag=0;
                timer3.Stop();
            }


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
                
                {
                    bullet.y-= bullet.speed;
                    e.Graphics.DrawImage(bul, (float)bullet.x, (float)bullet.y, 70, 70);
                }
                if ( bullet.y<0)
                {
                     
                }    

                
                
            }   
            


        }
        private void GunGun_Load(object sender, EventArgs e)
        {
            
        }
        private void Render( )
        {
            // Lấy đối tượng graphics để vẽ lên back buffer
          
            Graphics g = Graphics.FromImage(backBuffer);
             g.Clear(Color.Empty);

            // Xác dịnh số dòng, cột của một frame trên ảnh sprite

            curFrameColumn = index % 5;
            curFrameRow = index / 5;
            // Vẽ lên buffer

            g.DrawImage(sprite, x, y, new Rectangle(curFrameColumn * 79, curFrameRow *79, 79, 79), GraphicsUnit.Pixel);
            g.Dispose();


            // Tăng thứ tự frame để lấy frame tiếp theo


            if (index >9)
                index =0;
            else
                index++;

        }
        private void Exploision()
        {

            // kiểm tra va chạm . 
            // kiểm tra tọa độ của các rock so với ship 
            foreach (Rock rock in RockList)
            {
                if (rock.y > space.y-45 && Math.Abs(rock.x -space.x)<25) // xay ra va cham
                {

                    x =(int) space.x; y =(int) space.y-10;

                    timer3.Start();
                    
                    timer.Stop();
                    MessageBox.Show("Game over ");
                    return; 


                }

            }

            foreach (Bullet bul in bulletList)
            {
                foreach (Rock rock in RockList)
                {
                    if (bul.x > rock.x-20 &&  bul.x < rock.x+20 && bul.y-20 < rock.y+40) // xay ra va cham 
                    {
                        x =(int)rock.x; y =(int)rock.y;

                        
                         timer3.Start();

                        RockList.Remove(rock);
                        bulletList.Remove(bul);
                        return;
                    }
                }
            }

        }
        private void Move()
        {
            int x = 10;

            if( Keyboard.IsKeyDown(Keys.Up) )
            {
                if ( space.y >0  )
                {
                    space.y -=x;
                }    
            }    
            if( Keyboard.IsKeyDown(Keys.Down) )
            {
                if(space.y < this.Height-120 )
                {
                    space.y+=x;
                }    
            }    
            if(Keyboard.IsKeyDown(Keys.Left ))
             {
                if( space.x > -10)
                {
                    space.x-=x;
                }    
            }
            if(Keyboard.IsKeyDown (Keys.Right ))
            {
                if(space.x<this.Width -90)
                {
                    space.x+=x;
                }    
            } 
            if(Keyboard.IsKeyDown(Keys.Space))
            {
                Bullet but = new Bullet(space.x +15, space.y-40);
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
