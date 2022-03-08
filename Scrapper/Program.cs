using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using CsvHelper;
using System.IO;
using System.Globalization;

namespace Scrapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string url = "https://www.dawn.com/business";
            var links = GetPostLinks(url);
            List<Post> posts = GetPosts(links);
            Console.WriteLine("hello");

            var lst = GetPostLinks(url);
             foreach(string l in lst)
             {
                 Console.WriteLine(l);
             }
            Export(posts);
        }

        //This blocks Export to CSV 
        private static void Export(List<Post> posts)
        {
            

            using (var writer = new StreamWriter("./news.csv")) 
            using(var csv = new CsvWriter(writer,CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(posts);
            }
        }
        //Open the Links Generated, Find the Title and Description through Xpath
        private static List<Post> GetPosts(List<string> links)
        {
            var posts = new List<Post>(); // This blocks gives error
           /* foreach (var link in links)
            {
                var doc = GetDocument(link);
                var post = new Post();
                post.Title = doc.DocumentNode.SelectSingleNode("body div[class='container mx-auto px-2 sm:px-0'] div[class='flex'] div[class='w-full max-w-screen-md xl:pr-4 '] article[class='story font-georgia bg-white'] div:nth-child(2) h2:nth-child(1) a:nth-child(1)").InnerText;
                post.Description = doc.DocumentNode.SelectSingleNode("body > div:nth-child(3) > div:nth-child(1) > div:nth-child(1) > article:nth-child(1) > div:nth-child(3) > div:nth-child(1) > p:nth-child(2)").InnerText;
                posts.Add(post);
            }*/

            return posts;
        }
        //
        private static HtmlDocument GetDocument(string url)
        {

            var web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            return doc;
        }

        static List<string> GetPostLinks(string url)
        {
            var doc = GetDocument(url);
            var linkNodes = doc.DocumentNode.SelectNodes("//h2/a");

            var baseUri = new Uri(url);
            var links = new List<string>(); 
            foreach (var node in linkNodes)
            {
                var link = node.Attributes["href"].Value;
                link = new Uri(baseUri, link).AbsoluteUri;
                links.Add(link);
            }
            return links;
        }

    }


}
