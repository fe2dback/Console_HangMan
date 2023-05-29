using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_H
{
    class check_draw
    {
        private static StringGroup text = new StringGroup();//생성
        private static User_input key = new User_input();//입력
        private static Score score = new Score();//점수기록
        private static string word = text.text(); //첫 텍스트 생성
        private static string word_category = text.text_category();//텍스트의 카테고리 생성
        private static bool Dead = false; //죽음 유무
        private static bool multi = false; //멀티플레이 유무
        private char[] char_A = new char[word.Length];//텍스트 스펠링쪼개기
        private List<char> char_L = new List<char>();//스펠링 리스트
        private List<char> char_count = new List<char>();//정답개수 리스트         
        public int diff = 0; //난이도
        public int diff_next = 0; //재시작시 난이도


        public static void Setuser(bool user)
        {
            multi = user;
        }
        public static void SetWord(string newWord)
        {
            word = newWord;
        }

        public static void SetWord_category(string newWord_category)
        {
            word_category = newWord_category;
        }

        public void arr()
        {
            

            if (multi == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"SCORE : {score.result}");
                Console.ForegroundColor = ConsoleColor.White;
            }

            //카테고리
            Console.SetCursorPosition(0, 3);
            Console.WriteLine($"[{word_category}]");


            //빈칸만들기
            for (int i = 0; i < word.Length; i++)
            {
                Console.SetCursorPosition(1+i, 6);
                Console.Write("_");
            }



            //비교용 알파벳 배열
            for (int i = 0; i < char_A.Length; i++)
            {
                //단어 스펠링 배열
                char_A[i] = word[i];
                //단어 스펠링 리스트
                char_L.Add(char_A[i]);
                //정답 확인 리스트
                char_count.Add(char_A[i]);

            }
            #region 알파벳 생성
            Console.SetCursorPosition(0, 9); // 알파벳 위치
            string[] words = File.ReadAllLines(@"../../../../text/alphabet.txt");
            string[] value = new string[words.Length];
            if (words.Length > 0)
            {
                for (int i = 0; i < words.Length; i++)
                {
                    value[i] = words[i];
                }
            }

            // 출력
            for (int i = 0; i < words.Length - 5; i++)
            {
                Console.Write("  ");
                Console.Write(value[i]);

                if ((i + 1) % 7 == 0) // 7개 배열후 행변환
                {
                    Console.WriteLine();
                }
            }
            Console.Write("   ");//4번째 라인부터
            for (int i = words.Length - 5; i < words.Length; i++)
            {
                Console.Write("  ");
                Console.Write(value[i]);
            }
            #endregion
            HangMan(1);//행맨그리기 초기화
        }
        static void PrintAlphabet(char letter)
        {
            // 알파벳
            Console.SetCursorPosition(0, 9); // 알파벳 위치
            string[] words = File.ReadAllLines(@"../../../../text/alphabet.txt");
            string[] value = new string[words.Length];
            if (words.Length > 0)
            {
                for (int i = 0; i < words.Length; i++)
                {
                    value[i] = words[i];
                }
            }

            // 출력
            ConsoleColor defaultColor = ConsoleColor.White; // 기본 색상 설정
            for (int i = 0; i < words.Length - 5; i++)
            {
                Console.Write("  ");//1첫번째 라인부터
                if (value[i] == letter.ToString())
                {
                    Console.ForegroundColor = ConsoleColor.Red; // 입력한 값과 일치하는 알파벳의 색상 변경
                }
                else
                {
                    Console.ForegroundColor = defaultColor; // 기본 색상 유지
                }
                Console.Write(value[i]);

                if ((i + 1) % 7 == 0) // 7개 배열후 행변환
                {
                    Console.WriteLine();
                }
            }
            Console.Write("   ");//4번째 라인부터
            for (int i = words.Length - 5; i < words.Length; i++)
            {
                Console.Write("  ");
                if (value[i] == letter.ToString())
                {
                    Console.ForegroundColor = ConsoleColor.Red; // 입력한 값과 일치하는 알파벳의 색상 변경
                }
                else
                {
                    Console.ForegroundColor = defaultColor; // 기본 색상 유지
                }
                Console.Write(value[i]);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }


        public void check()
        {

            if (Dead == false) //살아있을때만 실행
            {
                string value = key.key_Value();
                Console.SetCursorPosition(0, 4);
                //입력값과 단어 스펠링 비교
                if (char_L.Contains(char.Parse(value)))
                {
                    for (int i = 0; i < char_A.Length; i++)
                    {
                        //입력한값과 저장된 텍스트가 같으면 빈칸을채우고 단어 스펠링 리스트를 지움

                        if (char_A[i].ToString().Equals(value))
                        {
                            Console.SetCursorPosition(1+i, 6);
                            Console.Write(value);
                            char_count.Remove(char_A[i]);
                        }

                        //리스트에 모든 스펠링리스트가 0이 다음단계 
                        if (char_count.Count <= 0)
                        {
                            if (multi == true)
                            {
                                GameManager.complete(diff_next, word, true);
                                GameManager.multi();
                            }
                            else
                            {
                                score.score();
                                GameManager.complete(diff_next, word, false);
                            }

                        }
                    }
                }
                else //입력값이 단어에 포함되지않으면 한획을 그림
                {
                    HangMan(1);
                }
                PrintAlphabet(char.Parse(value));
            }

        }

        public void HangMan(int chance)
        {
            #region console
            //교수대-밧줄-머리-팔-손-몸통-다리-발
            //교수대그리기
            Console.SetCursorPosition(40, 1);
            Console.Write("┬");
            Console.SetCursorPosition(39, 1);
            Console.Write("┌");
            for (int i = 2; i < 30; i++)
            {
                Console.SetCursorPosition(40, i);
                Console.Write("│");
                Console.SetCursorPosition(39, i);
                Console.Write("│");
            }
            #endregion
            diff -= chance;

            switch (diff)
            {

                case 7:
                    for (int i = 1; i < 20; i++)
                    {
                        Console.SetCursorPosition(40+i, 1);
                        Console.Write("─");
                    }
                    break;
                case 6:
                    Console.SetCursorPosition(60, 1);
                    Console.Write("┐");
                    for (int i = 1; i < 3; i++)
                    {
                        Console.SetCursorPosition(60, i+1);
                        Console.Write("│");
                    }
                    break;
                case 5:
                    Console.SetCursorPosition(58, 4);
                    Console.Write("─────");
                    Console.SetCursorPosition(57, 5);
                    Console.Write("/");
                    Console.SetCursorPosition(63, 5);
                    Console.Write("\\");
                    Console.SetCursorPosition(56, 6);
                    Console.Write("/");
                    Console.SetCursorPosition(64, 6);
                    Console.Write("\\");
                    Console.SetCursorPosition(55, 7);
                    Console.Write("|");
                    Console.SetCursorPosition(65, 7);
                    Console.Write("|");
                    Console.SetCursorPosition(56, 8);
                    Console.Write("\\");
                    Console.SetCursorPosition(64, 8);
                    Console.Write("/");
                    Console.SetCursorPosition(57, 9);
                    Console.Write("\\");
                    Console.SetCursorPosition(63, 9);
                    Console.Write("/");
                    Console.SetCursorPosition(58, 10);
                    Console.Write("──┬──");
                    break;
                case 4:
                    for (int i = 1; i < 2; i++)
                    {
                        Console.SetCursorPosition(60, 10+i);
                        Console.Write("│");
                    }
                    Console.SetCursorPosition(60, 12);
                    Console.Write("┼");
                    for (int i = 1; i < 6; i++)
                    {
                        Console.SetCursorPosition(60, 12+i);
                        Console.Write("│");
                    }
                    break;
                case 3:
                    for (int i = 1; i < 10; i++)
                    {
                        Console.SetCursorPosition(50+i, 12);
                        Console.Write("─");
                    }
                    break;
                case 2:
                    for (int i = 1; i < 10; i++)
                    {
                        Console.SetCursorPosition(60+i, 12);
                        Console.Write("─");
                    }
                    break;
                case 1:
                    for (int i = 1; i < 8; i++)
                    {
                        Console.SetCursorPosition(52+i, 25-i);
                        Console.Write("/");
                    }
                    break;
                case 0:
                    for (int i = 1; i < 8; i++)
                    {
                        Console.SetCursorPosition(60+i, 17+i);
                        Console.Write("\\");
                    }
                    Console.SetCursorPosition(1, 6);
                    Console.WriteLine(word);
                    Thread.Sleep(2000);
                    score.dead(); //킬 스코어, 스코어 기록
                    GameManager.Die();//죽음처리
                    break;
                default:
                    break;
            }


        }
    }
}
