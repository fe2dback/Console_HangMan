using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace project_H
{
    class StringGroup
    {
        static int groups = 4;//카테고리의수
        public string category = "";
        public string text()
        {
            return pick_tag();    
        }

        public string text_category()
        {
            return category;
        }
       

        public string pick_tag()// 랜덤한 카테고리를 고릅니다.
        {
            switch (random(1, groups + 1))
            {
                case 1:
                    this.category = "fruits";
                    return fruits();
                    break;
                case 2:
                    this.category = "worlds";
                    return worlds();
                    break;
                case 3:
                    this.category = "sports";
                    return sports();
                    break;
                case 4:
                    this.category = "c#language";
                    return keywords();
                    break;
                case 100:
                    this.category = "debug";
                    return testfile();
                default:
                    return "";
                    break;
            }

        }

        static string testfile()
        {
            string path = @"../../../../text/test.txt";
            return pick_word(path);
        }

        static string fruits()
        {
            string path = @"../../../../text/fruit.txt";
            return pick_word(path);

        }
        static string worlds()
        {
            string path = @"../../../../text/world.txt";
            return pick_word(path);
        }

        static string sports()
        {
            string path = @"../../../../text/sport.txt";
            return pick_word(path);
        }

        static string keywords()
        {
            string path = @"../../../../text/language.txt";
            return pick_word(path);
        }



        static string pick_word(string path) //랜덤한 단어를 고릅니다
        {
            string[] words = File.ReadAllLines(path);//path의 파일을 줄단위로 읽어옵니다.
            string[] value = new string[words.Length];
            if (words.Length > 0)
            {
                for (int i = 0; i < words.Length; i++)
                {
                    value[i] = words[i];
                }
            }
            return value[random(0, words.Length)].ToUpper();
        }
        
        static int random(int start, int end) //시작점과 종료점을 쉽게 파악하는 랜덤 메소드
        {
            Random random = new Random();
            int value = random.Next(start, end);
            return value;
        }
        
    }
    
}
