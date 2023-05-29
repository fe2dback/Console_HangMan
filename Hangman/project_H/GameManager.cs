using Figgle;
using System;
using static System.Net.Mime.MediaTypeNames;


namespace project_H
{
    class GameManager
    {
        public static bool Dead = false;
        static int Round = 0;
        static Score score = new Score();
        static void Main(string[] args)
        {
            //시작시 난이도를 설정합니다
            Console.CursorVisible = false;
            selete_mode();

        }

        
        static void OnGame(int diff)
        {
            check_draw start_Game = new check_draw();
            start_Game.arr();//단어를 생성합니다
            start_Game.diff = diff;//입력받은 난이도를 설정합니다
            start_Game.diff_next = diff;//재시작할때 난이도를 저장합니다
            while (true)
            {
                start_Game.check(); //플레이어의 입력을받고 이를 체크합니다
            }
        }
        public static void selete_mode()
        {

            #region console
            string title = FiggleFonts.Standard.Render("H A N G - M A N");
            Console.Write(title);
            Thread.Sleep(500);
            Console.WriteLine("\n게임 모드를 선택하세요 . . .\n");
            Thread.Sleep(500);
            Console.WriteLine("[1]Classic\n");
            Thread.Sleep(500);
            Console.WriteLine("[2]Multiplay");
            ConsoleKeyInfo difficulty = Console.ReadKey();
            Console.Clear();
            #endregion
            switch (difficulty.Key)
            {
                case ConsoleKey.D1:
                    check_draw.Setuser(false);
                    OnGame(8);
                    break;
                case ConsoleKey.D2:
                    check_draw.Setuser(true);
                    multi();
                    break;
                default:
                    selete_mode();
                    break;
            }   
        }
        public static void multi()
        {
            #region console
            check_draw.Setuser(true);
            Console.Clear();
            Console.Write("MultiPlay MODE\n\n");
            Console.Write("word? : ");
            #endregion
            string text = Console.ReadLine();
            if(text.Length <= 0)
            {
                Console.WriteLine("To short!");
                multi();
            }
            else
            {
                check_draw.SetWord(text.ToUpper());
                check_draw.SetWord_category("answer");
                Console.Clear();
                OnGame(8);
            }
            
        }


        public static void complete(int difficulty, string answer, bool multiplay)
        {
            #region console
            Console.Clear();       
            string asciiArt = FiggleFonts.Standard.Render("COMPLETE");
            Console.Write(asciiArt);
            Console.SetCursorPosition(1, 7);
            Console.WriteLine(answer);
            Thread.Sleep(2000);
            #endregion
            if (multiplay == false)
            {
                #region 초기화 그룹
                StringGroup init = new StringGroup();
                check_draw.SetWord(init.text());
                check_draw.SetWord_category(init.text_category());
                #endregion
                Console.Clear();
                OnGame(difficulty);
            }
        }

        public static void Die()
        {
            while (true)
            {
                #region console
                Console.Clear();
                string die = FiggleFonts.Standard.Render("DEAD . . .");
                Console.Write(die);
                Thread.Sleep(500);
                string best_score = FiggleFonts.Standard.Render($"BEST SCORE : {Score.bestscore}");
                Console.Write(best_score);
                Thread.Sleep(500);
                Console.WriteLine("\nREPLAY?\n");
                Console.WriteLine("CONTINUE PRESS : R");
                #endregion
                ConsoleKeyInfo repl = Console.ReadKey();
                if (repl.Key == ConsoleKey.R)
                {
                    Console.Clear();
                    #region 초기화 그룹
                    StringGroup init = new StringGroup();
                    check_draw.SetWord(init.text());
                    check_draw.SetWord_category(init.text_category());
                    #endregion
                    selete_mode();
                    break;
                }
            }
            
        }
    }
}