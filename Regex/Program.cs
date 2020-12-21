using System;
using System.Text.RegularExpressions;

namespace RegexToYoutubeVimeo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string[] l_urls = {"http://vimeo.com/36888803",
                      "http://vimeo.com/36888803?title=0",
                      "http://player.vimeo.com/video/36888803",
                      "https://vimeo.com/36888803?title=0",
                      "https://www.vimeo.com/36888803?title=0",
                      "www.vimeo.com/36888803?title=0",
                      "https://player.vimeo.com/video/36888803",
                      "http://player.vimeo.com/video/36888803?title=0&amp;byline=0&amp;portrait=0",
                      "http://youtu.be/i4fjHzCXg6c",
                      "http://youtu.be/i4fjHzCXg6c?t=9s",
                      "http://www.youtube.com/watch?v=i4fjHzCXg6c",
                      "www.youtube.com/watch?v=i4fjHzCXg6c",
                      "youtu.be/i4fjHzCXg6c?t=9s",
                      "youtube.com/watch?v=i4fjHzCXg6c",
                      "https://www.youtube.com/watch?v=i4fjHzCXg6c&feature=g-logo&t=28s",
                      "www.youtube.com",
                      "youtube.pt",
                      "www.youtu.be/i4fjHzCXg6c?t=9s"};
					  

					  //essa eh minha modificação


            foreach (string l_url in l_urls)
            {
                Console.WriteLine($"é yt ou viemo: {IsYouTubeOrVimeo(l_url)}");
                Console.WriteLine($"video yt: {GetYouTubeVideo(l_url)}");
                Console.WriteLine($"video vimeo: {GetVimeoVideo(l_url)}");
            }
            Console.ReadLine();
        }

        private static bool IsYouTubeOrVimeo(string testUrl)
        {
            return TestUrl(@"^(" +
                                    //youtube
                                    @"http://youtube\.com/watch\?v=([a-zA-Z0-9]|_)+($|&).*|" +
                                    @"https://youtube\.com/watch\?v=([a-zA-Z0-9]|_)+($|&).*|" +
                                    @"http://www\.youtube\.com/watch\?v=([a-zA-Z0-9]|_)+($|&).*|" +
                                    @"https://www\.youtube\.com/watch\?v=([a-zA-Z0-9]|_)+($|&).*|" +
                                    @"www\.youtube\.com/watch\?v=([a-zA-Z0-9]|_)+($|&).*|" +
                                    @"youtube\.com/watch\?v=([a-zA-Z0-9]|_)+($|&).*|" +
                                    //youtube shorturl
                                    @"http://youtu\.be/([a-zA-Z0-9]|_)+($|\?.*)|" +
                                    @"https://youtu\.be/([a-zA-Z0-9]|_)+($|\?.*)|" +
                                    @"youtu\.be/([a-zA-Z0-9]|_)+($|\?.*)|" +
                                    //vimeo
                                    @"http://vimeo\.com/([a-zA-Z0-9]|_)+($|\?.*)|" +
                                    @"https://vimeo\.com/([a-zA-Z0-9]|_)+($|\?.*)|" +
                                    @"http://www\.vimeo\.com/([a-zA-Z0-9]|_)+($|\?.*)|" +
                                    @"https://www\.vimeo\.com/([a-zA-Z0-9]|_)+($|\?.*)|" +
                                    @"www\.vimeo\.com/([a-zA-Z0-9]|_)+($|\?.*)|" +
                                    @"vimeo\.com/([a-zA-Z0-9]|_)+($|\?.*)|" +
                                    //play vimeo
                                    @"http://player\.vimeo\.com/video/([a-zA-Z0-9]|_)+($|\?.*)|" +
                                    @"https://player\.vimeo\.com/video/([a-zA-Z0-9]|_)+($|\?.*)|" +
                                    @"http://www\.player\.vimeo\.com/video/([a-zA-Z0-9]|_)+($|\?.*)|" +
                                    @"https://www\.player\.vimeo\.com/video/([a-zA-Z0-9]|_)+($|\?.*)|" +
                                    @"www\.player\.vimeo\.com/video/([a-zA-Z0-9]|_)+($|\?.*)|" +
                                    @"player\.vimeo\.com/video/([a-zA-Z0-9]|_)+($|\?.*)" +
                           @")", testUrl);
        }

        private static bool TestUrl(string pattern, string testUrl)
        {
            Regex l_expression = new Regex(pattern, RegexOptions.IgnoreCase);

            return l_expression.Matches(testUrl).Count > 0;
        }

        private static string GetYouTubeVideo(string testUrl)
        {
            return GetVideo(@"(/[^watch]|=)([a-zA-z0-9]|_)+($|(\?|&))", @"([a-zA-z0-9]|_)+", testUrl);
        }

        private static string GetVimeoVideo(string testUrl)
        {
            return GetVideo(@"/[0-9]+($|\?)", @"[0-9]+", testUrl);
        }

        private static string GetVideo(string overallPattern, string videoPattern, string testUrl)
        {
            Regex l_overallExpression = new Regex(overallPattern, RegexOptions.IgnoreCase);
            MatchCollection l_overallMatches = l_overallExpression.Matches(testUrl);

            if (l_overallMatches.Count > 0)
            {
                Regex l_videoExpression = new Regex(videoPattern, RegexOptions.IgnoreCase);

                return l_videoExpression.Matches(l_overallMatches[0].Value)[0].Value;
            }
            else
            {
                return "";
            }
        }
    }
}