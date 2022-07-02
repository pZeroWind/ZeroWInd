using System.ComponentModel.DataAnnotations;

namespace ZeroWindApi.Models.RequestModels
{
    public class SearchModel
    {
        //页面
        public int Page { get; set; } = 1;

        //每页大小
        public int Size { get; set; } = 5;

        //分类
        public int Type { get; set; }

        //排序
        public int Order { get; set; }

        //关键词
        public string? search { get; set; } = "";

        //标签列表
        public List<string> Tags { get; set; } = new List<string>();
    }
}
