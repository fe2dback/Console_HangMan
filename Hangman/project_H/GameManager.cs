using Figgle;
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
            check_draw isGame = new check_draw();
            isGame.arr();//단어를 생성합니다
            isGame.diff = diff;//입력받은 난이도를 설정합니다
            isGame.diff_next = diff;//재시작할때 난이도를 저장합니다
            while (true)
            {
                isGame.check(); //플레이어의 입력을받고 이를 체크합니다
            }
        }
        public static void selete_mode()
        {
            string title = FiggleFonts.Standard.Render("H A N G - M A N");
            Console.Write(title);
            Thread.Sleep(500);
            Console.WriteLine("\n게임 모드를 선택하세요 . . .\n");
            Thread.Sleep(500);
            Console.WriteLine("[1]Classic\n");
            Thread.Sleep(500);
            Console.WriteLine("[2]Multiplay");
            ConsoleKeyInfo diff = Console.ReadKey();
            Console.Clear();
            switch (diff.Key)
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
            check_draw.Setuser(true);
            Console.Clear();
            Console.Write("MultiPlay MODE\n\n");
            Console.Write("word? : ");
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


        public static void complete(int diff, string answer, bool multi)
        {
            Console.Clear();
            
            string asciiArt = FiggleFonts.Standard.Render("COMPLETE");
            Console.Write(asciiArt);
            Console.SetCursorPosition(1, 7);
            Console.WriteLine(answer);
            Thread.Sleep(1000);
            if(multi == false)
            {
                //초기화 그룹-------------------------------------
                StringGroup init = new StringGroup();
                check_draw.SetWord(init.text());
                check_draw.SetWord_category(init.text_category());
                //------------------------------------------------

                Console.Clear();

                OnGame(diff);
            }
        }

        public static void Die()
        {
            while (true)
            {
                Console.Clear();
                string die = FiggleFonts.Standard.Render("DEAD . . .");
                Console.Write(die);
                Thread.Sleep(500);
                string best_score = FiggleFonts.Standard.Render($"BEST SCORE : {Score.bestscore}");
                Console.Write(best_score);
                Thread.Sleep(500);
                Console.WriteLine("\nREPLAY?\n");
                Console.WriteLine("CONTINUE PRESS : R");


                ConsoleKeyInfo repl = Console.ReadKey();
                if (repl.Key == ConsoleKey.R)
                {
                    Console.Clear();
                    //초기화 그룹-------------------------------------
                    StringGroup init = new StringGroup();
                    check_draw.SetWord(init.text());
                    check_draw.SetWord_category(init.text_category());
                    //------------------------------------------------
                    selete_mode();
                    break;
                }
            }
            
        }
    }
}