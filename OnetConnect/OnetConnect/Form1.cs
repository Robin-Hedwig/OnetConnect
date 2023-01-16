using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;

namespace OnetConnect
{
    public partial class Form1 : Form
    {
        System.Timers.Timer timer;
        int hr, min, sec;
        private static string previousColor = "";
        private static string currentColor = "";
        private static string previousButton = "";
        private static string currentButton = "";
        public Form1()
        {
            InitializeComponent();
            timer= new System.Timers.Timer();
            timer.Interval= 1000;
            timer.Elapsed += OnTimeEvent;
        }

        private void OnTimeEvent(object sender, ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                sec += 1;
                if (sec == 60)
                {
                    sec = 0;
                    min += 1;
                }
                if (min == 60)
                {
                    min = 0;
                    hr += 1;
                }
                finishTime.Text=string.Format("{0}:{1}:{2}",hr.ToString().PadLeft(2,'0'),min.ToString().PadLeft(2,'0'),sec.ToString().PadLeft(2,'0'));
            }));
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void generateBoard()
        {
            Color color;
            String colorName;
            int number;
            Random rnd = new Random();
            for (int j = 1; j <= 21; j++)
            {
                number = rnd.Next(1, 7);
                Console.WriteLine("random num:" + number);
                if (number == 1)
                {
                    color = Color.SkyBlue;
                    colorName = color.Name;
                }
                else if (number == 2)
                {
                    color = Color.DarkRed;
                    colorName = color.Name;
                }
                else if (number == 3)
                {
                    color = Color.Orange;
                    colorName = color.Name;
                }
                else if (number == 4)
                {
                    color = Color.Yellow;
                    colorName = color.Name;
                }

                else if (number == 5)
                {
                    color = Color.Green;
                    colorName = color.Name;
                }
                else if (number == 6)
                {
                    color = Color.DarkViolet;
                    colorName = color.Name;
                }
                else
                {
                    color = Color.Pink;
                    colorName = color.Name;
                }


                String buttonName = "button" + j;
                Console.WriteLine("button name: " + buttonName);
                this.Controls[buttonName].BackColor = Color.FromName(colorName);
                int l = 43 - j;
                String buttonName2 = "button" + l;
                Console.WriteLine("button name2: " + buttonName2);
                this.Controls[buttonName2].BackColor = Color.FromName(colorName);
            }
        }
        public void win()
        {
            Boolean flag=false;
            for(int i=1; i <= 42; i++)
            {
                String buttonName = "button" + i;
                String C = this.Controls[buttonName].BackColor.Name;
                if(C != "White")
                {
                    break;
                }
                if (i == 42 && C == "White")
                {
                    flag = true;
                }
            }
            if (flag == true)
            {
                timer.Stop();
                String completionTime = finishTime.Text;
                MessageBox.Show("You won!  Your Completion time is "+completionTime, "We have a winner", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                generateBoard();
                finishTime.Text = "00:00:00";
                
            }
        }
        public int getButtonNum(String buttonName)
        {
            int buttonNum;
            String address = "";
            address = buttonName.Remove(0,6);
            Console.WriteLine("address " + address);
            buttonNum=Convert.ToInt32(address);
            Console.WriteLine("buttonNum " + buttonNum);

            return buttonNum;
        }

        public int findLocationOfAButton(int buttonIndex)
        {
            int loc,i,j;
            if (buttonIndex % 7 != 0)
            {
                i = (buttonIndex / 7) + 1;
                j = (buttonIndex % 7);
            }
            else
            {
                i = (buttonIndex / 7);
                j = 7;
            }
            
            loc = (i * 10) + j;
            Console.WriteLine("location " + loc);
            return loc;
        }
        
        public Boolean ifPossible(int location1,int location2)
        {
            int i1 = location1 / 10;
            int i2= location2 / 10;
            int j1 = location1 % 10;
            int j2= location2 % 10;
            if (ifExternal(i1, j1, i2, j2))
            {
                return true;
            }
            Console.WriteLine("if possible check i1j1 i2j2 : " + i1+j1+" ,"+i2+j2);
            if (i1==i2+1 || i1==i2-1)
            {
                if(j1==j2)
                {
                    return true;   
                }
            }
            if(j1==j2+1 || j1 == j2 - 1)
            {
                if(i1==i2)
                {
                    return true;
                }
            }
            int buttonNum1;
            if(i1 == i2)
            {
                if (j2 > j1)
                {
                    for(int a = j1 + 1; a < j2; a++)
                    {
                        buttonNum1 = ((i1 - 1) * 7) + a;
                        String button=Convert.ToString(buttonNum1);
                        String tempButtonName = "button" + button;
                        String middleColor = this.Controls[tempButtonName].BackColor.Name;
                        if (middleColor == "White")
                        {
                            if (a == j2 - 1)
                            {
                                return true;
                            }
                            continue;
                        }
                        if (middleColor != "White")
                        {
                            break;
                        }
                    }
                }
                if (j1 > j2)
                {
                    for(int a = j2 + 1; a < j1; a++)
                    {
                        buttonNum1 = ((i1 - 1) * 7) + a;
                        String button = Convert.ToString(buttonNum1);
                        String tempButtonName = "button" + button;
                        String middleColor = this.Controls[tempButtonName].BackColor.Name;
                        if (middleColor == "White")
                        {
                            if (a == j1 - 1)
                            {
                                return true;
                            }
                            continue;
                        }
                        if (middleColor != "White")
                        {
                            break;
                        }
                    }
                }

            }
            if (j1 == j2)
            {
                if (i2 > i1)
                {
                    for (int a = i1 + 1; a < i2; a++)
                    {
                        buttonNum1 = ((a - 1) * 7) + j1;
                        String button = Convert.ToString(buttonNum1);
                        String tempButtonName = "button" + button;
                        String middleColor = this.Controls[tempButtonName].BackColor.Name;
                        if (middleColor == "White")
                        {
                            if (a == i2 - 1)
                            {
                                return true;
                            }
                            continue;
                        }
                        if (middleColor != "White")
                        {
                            break;
                        }
                    }
                }
                if (i1 > i2)
                {
                    for (int a = i2 + 1; a < i1; a++)
                    {
                        buttonNum1 = ((a - 1) * 7) + j1;
                        String button = Convert.ToString(buttonNum1);
                        String tempButtonName = "button" + button;
                        String middleColor = this.Controls[tempButtonName].BackColor.Name;
                        if (middleColor == "White")
                        {
                            if (a == i1 - 1)
                            {
                                return true;
                            }
                            continue;
                        }
                        if (middleColor != "White")
                        {
                            break;
                        }
                    }
                }
            }
            int e=0, f=0, g=0, h=0;
            if (i1 != i2 || j1 != j2)
            {
                Boolean blocked = false, turnLeft = false, turnRight = false;
                int i=i1,j=j1,k=i2,l=j2;
                if (i2 > i1)
                {
                    e= i1;
                    f = i2;
                }
                if (i1 > i2)
                {
                    e= i2; f = i1;
                }
                if (j2 > j1)
                {
                    g= j1;
                    h = j2;
                }if(j1> j2)
                {
                    g = j2;
                    h = j1;
                }
                j2 = h;
                j1 = g;
                i1 = e; i2=f;
                if ((j2 > j1 &&  i2 > i1)||(j2 < j1 && i2 < i1))
                {
                    for(int a = j1 + 1; a <= j2; a++)
                    {
                        buttonNum1 = ((i1 - 1) * 7) + a;
                        String button = Convert.ToString(buttonNum1);
                        String tempButtonName = "button" + button;
                        String middleColor = this.Controls[tempButtonName].BackColor.Name;
                        if (middleColor == "White")
                        {
                            if (a == j2)
                            {
                                turnRight = true;
                                blocked = false;
                            }
                            continue;
                        }
                        if (middleColor != "White")
                        {
                            blocked = true;
                            turnRight=false;
                            break;
                        }
                    }
                    if (turnRight == true&& blocked == false)
                    {
                        if (i1 + 1 == i2)
                        {
                            return true;
                        }
                        for (int a = i1 + 1; a <= i2; a++)
                        {
                            buttonNum1 = ((a - 1) * 7) + j2;
                            String button = Convert.ToString(buttonNum1);
                            String tempButtonName = "button" + button;
                            String middleColor = this.Controls[tempButtonName].BackColor.Name;
                            if (middleColor == "White")
                            {
                                if (a == i2-1)
                                {
                                    return true;
                                }
                                continue;
                            }
                            if (middleColor != "White")
                            {
                                blocked = true;
                                turnRight = false;
                                break;
                            }
                        }
                    }
                    if (blocked == true && turnRight == false)
                    {
                        for (int a = i1 + 1; a <= i2; a++)
                        {
                            buttonNum1 = ((a - 1) * 7) + j1;
                            String button = Convert.ToString(buttonNum1);
                            String tempButtonName = "button" + button;
                            String middleColor = this.Controls[tempButtonName].BackColor.Name;
                            if (middleColor == "White")
                            {
                                if (a == i2)
                                {
                                    turnLeft = true;
                                    blocked = false;
                                }
                                continue;
                            }
                            if (middleColor != "White")
                            {
                                blocked = true;
                                turnLeft = false;
                                break;
                            }
                        }
                        if (blocked == false && turnLeft == true)
                        {
                            if (j1 + 1 == j2)
                            {
                                return true;
                            }
                            for (int a = j1 + 1; a <= j2; a++)
                            {
                                buttonNum1 = ((i2 - 1) * 7) + a;
                                String button = Convert.ToString(buttonNum1);
                                String tempButtonName = "button" + button;
                                String middleColor = this.Controls[tempButtonName].BackColor.Name;
                                if (middleColor == "White")
                                {
                                    if (a == j2-1)
                                    {
                                        blocked = false;
                                        return true;
                                    }
                                    continue;
                                }
                                if (middleColor != "White")
                                {
                                    blocked = true;
                                    break;
                                }
                            }
                        }
                    }

                }
                if((i < k && j > l)||(i > k && l > j ))
                {
                    Console.WriteLine("i1:"+i+"j1:"+j+"i2:"+k+"j2:"+l);
                    Console.WriteLine("inside todays code");
                    blocked = false; turnLeft = false; turnRight = false;
                    for (int a = k - 1; a >= i; a--)
                    {
                        Console.WriteLine("inside todays code: inside first for loop");
                        buttonNum1 = ((a -1 )* 7) + l;
                        Console.WriteLine("inside todays code, current button"+buttonNum1);
                        String button = Convert.ToString(buttonNum1);
                        String tempButtonName = "button" + button;
                        String middleColor = this.Controls[tempButtonName].BackColor.Name;
                        if (middleColor == "White")
                        {
                            Console.WriteLine("inside todays code: current button is white");
                            if (a == i)
                            {
                                Console.WriteLine("inside todays code: turn right is true");
                                turnRight = true;
                                blocked = false;
                                break;
                            }
                        }
                        if (middleColor != "White")
                        {
                            blocked = true;
                            turnRight = false;
                            break;
                        }
                    }
                    if (turnRight == true && blocked == false)
                    {
                        if (l + 1 == j)
                        {
                            return true;
                        }
                        for (int a = l + 1; a <= j; a++)
                        {
                            buttonNum1 = ((i - 1) * 7) + a;
                            String button = Convert.ToString(buttonNum1);
                            String tempButtonName = "button" + button;
                            String middleColor = this.Controls[tempButtonName].BackColor.Name;
                            if (middleColor == "White")
                            {
                                if (a == j-1)
                                {
                                    return true;
                                }
                                continue;
                            }
                            if (middleColor != "White")
                            {
                                blocked = true;
                                turnRight = false;
                                break;
                            }
                        }
                    }
                    if (blocked == true && turnRight == false)
                    {
                        Console.WriteLine("inside todays code: first way blocked");
                        for (int a = l + 1; a <= j; a++)
                        {
                            Console.WriteLine("inside todays code 2: inside for");
                            buttonNum1 = ((k - 1) * 7) + a;
                            String button = Convert.ToString(buttonNum1);
                            Console.WriteLine("inside todays code2 buttonnum"+buttonNum1);
                            String tempButtonName = "button" + button;
                            String middleColor = this.Controls[tempButtonName].BackColor.Name;
                            if (middleColor == "White")
                            {
                                Console.WriteLine("inside todays code 2, is white");
                                if (a == j)
                                {
                                    Console.WriteLine("inside todays code 2: turn left true");
                                    turnLeft = true;
                                    blocked = false;
                                    break;
                                }
                                continue;
                            }
                            if (middleColor != "White")
                            {
                                blocked = true;
                                turnLeft = false;
                                break;
                            }
                        }
                        if (blocked == false && turnLeft == true)
                        {
                            if (k-1 == i)
                            {
                                return true;
                            }
                            for (int a = k - 1; a >= i; a--)
                            {
                                Console.WriteLine("inside todays code 2: turn left true: inside for");
                                buttonNum1 = ((a - 1) * 7) + j;
                                Console.WriteLine("inside todays code 2: turn left true, button num"+buttonNum1);
                                String button = Convert.ToString(buttonNum1);
                                String tempButtonName = "button" + button;
                                String middleColor = this.Controls[tempButtonName].BackColor.Name;
                                if (middleColor == "White")
                                {
                                    Console.WriteLine("inside todays code 2: turn left true, is white");
                                    if (a == i+1)
                                    {
                                        blocked = false;
                                        return true;
                                    }
                                    continue;
                                }
                                if (middleColor != "White")
                                {
                                    blocked = true;
                                    break;
                                }
                            }
                        }
                    }

                }
            }
                return false;
        }

        public Boolean ifExternal(int i1, int j1, int i2, int j2)
        {
            if (i1 == 1 && i2 == 1)
            {
                return true;
            }
            if (i1 == 6 && i2 == 6)
            {
                return true;
            }
            if (j1 == 7 && j2 == 7)
            {
                return true;
            }
            if (j1 == 1 && j2 == 1)
            {
                return true;
            }
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            currentButton = "button1";
            currentColor= button1.BackColor.Name;
            if (previousButton != "" && previousColor!="" && currentButton!=previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1=getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1=findLocationOfAButton(buttonNum1);
                    int location2=findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button1";
            }
            win();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentButton = "button2";
            currentColor = button2.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1=getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1=findLocationOfAButton(buttonNum1);
                    int location2=findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button2";
            }
            win();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentButton = "button3";
            currentColor = button3.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button3";
            }
            win();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            currentButton = "button4";
            currentColor = button4.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button4";
            }
            win();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            currentButton = "button5";
            currentColor = button5.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button5";
            }
            win();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            currentButton = "button6";
            currentColor = button6.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button6";
            }
            win();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            currentButton = "button7";
            currentColor = button7.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button7";
            }
            win();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            currentButton = "button8";
            currentColor = button8.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button8";
            }
            win();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            currentButton = "button9";
            currentColor = button9.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button9";
            }
            win();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            currentButton = "button10";
            currentColor = button10.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button10";
            }
            win();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            currentButton = "button11";
            currentColor = button11.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button11";
            }
            win();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            currentButton = "button12";
            currentColor = button12.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button12";
            }
            win();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            currentButton = "button13";
            currentColor = button13.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button13";
            }
            win();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            currentButton = "button14";
            currentColor = button14.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button14";
            }
            win();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            currentButton = "button15";
            currentColor = button15.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button15";
            }
            win();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            currentButton = "button16";
            currentColor = button16.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button16";
            }
            win();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            currentButton = "button17";
            currentColor = button17.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button17";
            }
            win();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            currentButton = "button18";
            currentColor = button18.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button18";
            }
            win();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            currentButton = "button19";
            currentColor = button19.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button19";
            }
            win();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            currentButton = "button20";
            currentColor = button20.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button20";
            }
            win();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            currentButton = "button21";
            currentColor = button21.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button21";
            }
            win();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            currentButton = "button22";
            currentColor = button22.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button22";
            }
            win();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            currentButton = "button23";
            currentColor = button23.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button23";
            }
            win();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            currentButton = "button24";
            currentColor = button24.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button24";
            }
            win();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            currentButton = "button25";
            currentColor = button25.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button25";
            }
            win();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            currentButton = "button26";
            currentColor = button26.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button26";
            }
            win();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            currentButton = "button27";
            currentColor = button27.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button27";
            }
            win();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            currentButton = "button28";
            currentColor = button28.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button28";
            }
            win();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            currentButton = "button29";
            currentColor = button29.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button29";
            }
            win();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            currentButton = "button30";
            currentColor = button30.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button30";
            }
            win();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            currentButton = "button31";
            currentColor = button31.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button31";
            }
            win();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            currentButton = "button32";
            currentColor = button32.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button32";
            }
            win();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            currentButton = "button33";
            currentColor = button33.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button33";
            }
            win();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            currentButton = "button34";
            currentColor = button34.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button34";
            }
            win();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            currentButton = "button35";
            currentColor = button35.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button35";
            }
            win();
        }

        private void button36_Click(object sender, EventArgs e)
        {
            currentButton = "button36";
            currentColor = button36.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button36";
            }
            win();
        }

        private void button37_Click(object sender, EventArgs e)
        {
            currentButton = "button37";
            currentColor = button37.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button37";
            }
            win();
        }

        private void button38_Click(object sender, EventArgs e)
        {
            currentButton = "button38";
            currentColor = button38.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button38";
            }
            win();
        }

        private void button39_Click(object sender, EventArgs e)
        {
            currentButton = "button39";
            currentColor = button39.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button39";
            }
            win();
        }

        private void button40_Click(object sender, EventArgs e)
        {
            currentButton = "button40";
            currentColor = button40.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button40";
            }
            win();
        }

        private void button41_Click(object sender, EventArgs e)
        {
            currentButton = "button41";
            currentColor = button41.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button41";
            }
            win();
        }

        private void button42_Click(object sender, EventArgs e)
        {
            currentButton = "button42";
            currentColor = button42.BackColor.Name;
            if (previousButton != "" && previousColor != "" && currentButton != previousButton)
            {
                if (previousColor == currentColor)
                {
                    int buttonNum1 = getButtonNum(previousButton);
                    int buttonNum2 = getButtonNum(currentButton);
                    int location1 = findLocationOfAButton(buttonNum1);
                    int location2 = findLocationOfAButton(buttonNum2);
                    if (ifPossible(location1, location2))
                    {
                        this.Controls[previousButton].BackColor = Color.White;
                        this.Controls[currentButton].BackColor = Color.White;
                    }
                }
                currentColor = "";
                previousColor = "";
                previousButton = "";
            }
            else
            {
                previousColor = currentColor;
                previousButton = "button42";
            }
            win();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            finishTime.Text = "00:00:00";
            timer.Start();
            generateBoard();
        }
    }
}
